using System.Collections.Generic;
using System.Windows.Forms;
using ClassLibraryTreeView.Classes;
using System;
using ClosedXML.Excel;
using ClassLibraryTreeView.Classes.CellStyle;
using CellStyle = ClassLibraryTreeView.Classes.CellStyle.CellStyle;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Data;
using System.Linq;
using ClassLibraryTreeView.Interfaces;

namespace ClassLibraryTreeView
{
    public partial class ConceptualModel
    {
        public partial class ConceptualModelExcelExporter
        {
            public event EventHandler<int> ExportProgress;
            private CellStyleFactory cellStyleFactory = new CellStyleFactory();
            public ConceptualModelExcelExporter()
            {

            }
            public static string CellName(int row, int col)
            {
                int number = col + 1;
                string name = "";
                while (number > 0)
                {
                    int modul = (number - 1) % 26;
                    name = Convert.ToChar('A' + modul) + name;
                    number = (number - modul) / 26;
                }
                return $"{name}{row + 1}";
            }
            private ConcurrentDictionary<int, ConceptualModelClass> AddClassToClassRows(Dictionary<string, ConceptualModelClass> map, int startRowIndex)
            {
                ConceptualModelClass cmClassRoot = null;
                foreach (ConceptualModelClass cmClass in map.Values)
                {
                    if (cmClass.Parent == null)
                    {
                        cmClassRoot = cmClass;
                        break;
                    }
                }

                ConcurrentDictionary<int, ConceptualModelClass> classRows = new ConcurrentDictionary<int, ConceptualModelClass>();
                Queue<ConceptualModelClass> queueOfClasses = new Queue<ConceptualModelClass>();
                List<ConceptualModelClass> listOfClasses = new List<ConceptualModelClass>();
                queueOfClasses.Enqueue(cmClassRoot);
                listOfClasses.Add(cmClassRoot);

                Func<ConceptualModelClass, List<ConceptualModelClass>, int>FindIndex = (cmClass, cmList) =>
                {
                    int cmIndex = 0;
                    foreach(ConceptualModelClass cmItem in cmList)
                    {
                        if (cmItem.Name.Equals(cmClass.Name))
                        {
                            break;
                        }
                        cmIndex++;
                    }
                    return cmIndex;
                };

                while (queueOfClasses.Count > 0)
                {
                    ConceptualModelClass cmClass = queueOfClasses.Dequeue();

                    if (cmClass.Children.Count > 0)
                    {
                        int parentIndex = FindIndex(cmClass, listOfClasses) + 1;
                        int addingIndex = 0;
                        foreach (ConceptualModelClass cmClassChild in cmClass.Children.Values)
                        {
                            queueOfClasses.Enqueue(cmClassChild);
                            listOfClasses.Insert(parentIndex + addingIndex, cmClassChild);
                            addingIndex++;
                        }
                    }
                }

                int index = startRowIndex;
                foreach (ConceptualModelClass cmClass in listOfClasses)
                {
                    if (classRows.TryAdd(index, cmClass))
                    {
                        index++;
                    }
                }

                return classRows;
            }
            private void InsertClassIntoWorksheet(KeyValuePair<int, ConceptualModelClass> classRow,
                IXLWorksheet worksheet,
                int classRowsCount,
                int maxDepth,
                ConceptualModel model,
                Queue<KeyValuePair<string, IXLStyle>> mergedRanges)
            {
                Action<IXLCell, IXLStyle, string> SetCellValue = (cellReference, cellStyle, cellValue) =>
                {
                    cellReference.Style = cellStyle;
                    cellReference.Value = cellValue;
                };

                IXLStyle headerCellStyle = cellStyleFactory.CreateCellStyle(CellStyle.Header);
                IXLStyle attributesGroupCellStyle = cellStyleFactory.CreateCellStyleForAttributesGroup();
                IXLStyle attributeCellStyle = cellStyleFactory.CreateCellStyleForAttribute();
                IXLStyle classCellStyle = cellStyleFactory.CreateCellStyleForClass();
                IXLStyle defaultCellStyle = cellStyleFactory.CreateCellStyleDeafult();
                IXLStyle classIdCellStyle = cellStyleFactory.CreateCellStyleForClassId();
                IXLStyle disciplineCellStyle = cellStyleFactory.CreateCellStyleForDiscipline();

                ConceptualModelClass cmClass = classRow.Value;
                int classDepth = ConceptualModelClass.Depth(cmClass);

                SetCellValue(worksheet.Cell(CellName(classRow.Key, maxDepth + 2)), classIdCellStyle, cmClass.Id); // set class id cell
                SetCellValue(worksheet.Cell(CellName(classRow.Key, classDepth)), classCellStyle, cmClass.Name); // set class name cell
                SetCellValue(worksheet.Cell(CellName(classRow.Key, maxDepth + 1)), disciplineCellStyle, ""); // set class discipline cell

                if (maxDepth != classDepth) // merge subclass cells
                {
                    mergedRanges.Enqueue(new KeyValuePair<string, IXLStyle>($"{CellName(classRow.Key, classDepth)}:{CellName(classRow.Key, maxDepth)}",
                        classCellStyle));
                }

                int col = maxDepth + 3;
                foreach (string group in model.attributes.Keys)
                {
                    foreach (ConceptualModelAttribute attribute in model.attributes[group].Values)
                    {
                        // SetCellValue(worksheet.Cell($"{CellName(row, col)}"), attributeCellStyle, $"{attribute.Id} : {attribute.Name}");
                        string presence = cmClass.PermissibleAttributePresence(attribute.Id);
                        var cellStyle = cellStyleFactory.CreateCellStyleForAttributePresence(presence);
                        SetCellValue(worksheet.Cell(CellName(classRow.Key, col)), cellStyle, presence);
                        col++;
                    }
                }

                int progress = (classRow.Key * 100) / classRowsCount;
                this.ExportProgress?.Invoke(this, progress);
            }
            public DataTable ExportPermissibleGrid(ConceptualModel model)
            {
                DataTable dataTable = new DataTable();
                // calculate maximum depth of model classes

                int maxDepth = 0;
                foreach (Dictionary<string, ConceptualModelClass> map in model.classes.Values)
                {
                    foreach (ConceptualModelClass cmClass in map.Values)
                    {
                        int depth = ConceptualModelClass.Depth(cmClass);
                        if (depth > maxDepth)
                        {
                            maxDepth = depth;
                        }
                    }
                }

                using (XLWorkbook workbook = new XLWorkbook())
                {
                    IXLWorksheet worksheet = workbook.Worksheets.Add($"Permissible Grid");

                    int count = maxDepth + model.AttributesCount + 2;

                    CellStyleFactory cellStyleFactory = new CellStyleFactory();

                    Action<IXLCell, IXLStyle, string> SetCellValue = (cellReference, cellStyle, cellValue) =>
                    {
                        cellReference.Style = cellStyle;
                        cellReference.Value = cellValue;
                    };

                    // define classes attributes rows
                    Dictionary<string, ConceptualModelClass> map = model.MergedClasses;

                    // fill dictionary of pairs <row number, conceptual model class>
                    ConcurrentDictionary<int, ConceptualModelClass> classRows = AddClassToClassRows(map, 3);

                    // define cell styles
                    IXLStyle headerCellStyle = cellStyleFactory.CreateCellStyle(CellStyle.Header);
                    IXLStyle attributesGroupCellStyle = cellStyleFactory.CreateCellStyleForAttributesGroup();
                    IXLStyle attributeCellStyle = cellStyleFactory.CreateCellStyleForAttribute();
                    IXLStyle classCellStyle = cellStyleFactory.CreateCellStyleForClass();
                    IXLStyle defaultCellStyle = cellStyleFactory.CreateCellStyleDeafult();
                    IXLStyle classIdCellStyle = cellStyleFactory.CreateCellStyleForClassId();
                    IXLStyle disciplineCellStyle = cellStyleFactory.CreateCellStyleForDiscipline();

                    // write header
                    SetCellValue(worksheet.Cell(CellName(2, 0)), headerCellStyle, $"Classes ({map.Count})"); // set classes count header cell
                    SetCellValue(worksheet.Cell(CellName(2, maxDepth + 1)), headerCellStyle, $"Discipline"); // set class discipline header cell
                    SetCellValue(worksheet.Cell(CellName(2, maxDepth + 2)), headerCellStyle, $"Class ID"); // set class id header cell

                    Queue<KeyValuePair<string, IXLStyle>> mergedRanges = new Queue<KeyValuePair<string, IXLStyle>>();
                    mergedRanges.Enqueue(new KeyValuePair<string, IXLStyle>($"{CellName(0, 0)}:{CellName(1, maxDepth + 2)}", defaultCellStyle));
                    mergedRanges.Enqueue(new KeyValuePair<string, IXLStyle>($"{CellName(2, 0)}:{CellName(2, maxDepth)}", defaultCellStyle));

                    for (int columnIndex = 0; columnIndex <= maxDepth + 2; columnIndex++)
                    {
                        dataTable.Columns.Add($"");
                    }

                    string mergedCell = "";
                    int col = maxDepth + 3;
                    foreach (string group in model.attributes.Keys)
                    {
                        mergedCell = $"{CellName(0, col)}";
                        IXLCell cell = worksheet.Cell($"{CellName(0, col)}");
                        SetCellValue(cell, attributesGroupCellStyle, group);
                        foreach (ConceptualModelAttribute attribute in model.attributes[group].Values)
                        {
                            cell = worksheet.Cell($"{CellName(1, col)}");
                            SetCellValue(cell, attributeCellStyle, $"{attribute.Id} : {attribute.Name}");
                            col++;
                            // dataTable.Columns.Add($"{attribute.Id} : {attribute.Name}");
                        }
                        mergedRanges.Enqueue(new KeyValuePair<string, IXLStyle>($"{mergedCell}:{CellName(0, col - 1)}", attributesGroupCellStyle));
                    }

                    // write permissible grid
                    Parallel.ForEach
                    (
                        classRows,
                        new ParallelOptions()
                        {
                            MaxDegreeOfParallelism = 2
                        },
                        classRow =>
                        {
                            InsertClassIntoWorksheet(classRow, worksheet, classRows.Count, maxDepth, model, mergedRanges);
                        }
                    );

                    // merging selected cells
                    foreach (KeyValuePair<string, IXLStyle> range in mergedRanges)
                    {
                        if (range.Value != null)
                        {
                            worksheet.Range(range.Key).Select();
                            worksheet.Range(range.Key).Style = range.Value;
                            worksheet.Range(range.Key).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.CenterContinuous;
                            // worksheet.Range(range.Key).Merge();
                            // worksheet.Range(range.Key).Style = range.Value;
                        }
                    }

                    // adjust columns width
                    for (col = 1; col <= worksheet.ColumnCount(); col++)
                    {
                        worksheet.Column(col).AdjustToContents();
                    }

                    worksheet.Row(2).AdjustToContents();

                    string filename = model.FullPathXml;
                    filename = filename.Remove(filename.LastIndexOf("."), filename.Length - filename.LastIndexOf("."));
                    filename += ".xlsx";

                    try
                    {
                        workbook.SaveAs(filename);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    foreach (var classRow in classRows)
                    {
                        ConceptualModelClass cmClass = classRow.Value;
                        int classDepth = ConceptualModelClass.Depth(cmClass);
                        List<string> rowItems = new List<string>();

                        for (int columnIndex = 0; columnIndex < maxDepth + 1; columnIndex++)
                        {
                            if (columnIndex == classDepth)
                            {
                                rowItems.Add($"{cmClass.Name}");
                            }
                            else
                            {
                                rowItems.Add("");
                            }
                        }

                        foreach (string group in model.attributes.Keys)
                        {
                            foreach (ConceptualModelAttribute attribute in model.attributes[group].Values)
                            {
                                string presence = cmClass.PermissibleAttributePresence(attribute.Id);
                                // rowItems.Add($"{presence}");
                            }
                        }

                        dataTable.Rows.Add(rowItems.ToArray());

                        int progress = (classRow.Key * 100) / classRows.Count;
                        this.ExportProgress?.Invoke(this, progress);
                    }
                }
                return dataTable;
            }
            public void ExportClassAttributes(ConceptualModel model)
            {
                Dictionary<string, ConceptualModelClass> map = model.MergedClasses;

                Action<IXLCell, IXLStyle, string> SetCellValue = (cellReference, cellStyle, cellValue) =>
                {
                    cellReference.Style = cellStyle;
                    cellReference.Value = cellValue;
                };
                int rowIndex = 1;
                List<KeyValuePair<int, string[]>> rows = new List<KeyValuePair<int, string[]>>();
                foreach (ConceptualModelClass cmClass in map.Values)
                {
                    Dictionary<string, ConceptualModelAttribute> attributes = cmClass.PermissibleAttributes;
                    if (attributes.Count > 0)
                    {
                        foreach (ConceptualModelAttribute attribute in attributes.Values)
                        {
                            rows.Add(new KeyValuePair<int, string[]>(rowIndex,
                                new string[] { cmClass.Id, cmClass.Name, attribute.Id, attribute.Name }));
                            rowIndex++;
                        }
                    }
                }

                using (XLWorkbook workbook = new XLWorkbook())
                {
                    IXLWorksheet worksheet = workbook.Worksheets.Add($"MergedClassAttributes");

                    // insert header
                    IXLStyle headerCellStyle = cellStyleFactory.CreateCellStyleForHeader();
                    SetCellValue(worksheet.Cell(CellName(0, 0)), headerCellStyle, $"Class ID");
                    SetCellValue(worksheet.Cell(CellName(0, 1)), headerCellStyle, $"Class Name");
                    SetCellValue(worksheet.Cell(CellName(0, 2)), headerCellStyle, $"Attribute ID");
                    SetCellValue(worksheet.Cell(CellName(0, 3)), headerCellStyle, $"Attribute Name");

                    // insert attributes
                    IXLStyle classCellStyle = cellStyleFactory.CreateCellStyleForClass();
                    IXLStyle classDarkCellStyle = cellStyleFactory.CreateCellStyleForClassDark();

                    foreach (KeyValuePair<int, string[]> row in rows)
                    {
                        IXLStyle cellStyle = classCellStyle;
                        if (row.Key % 2 == 0)
                        {
                            cellStyle = classDarkCellStyle;
                        }

                        for (int col = 0; col < row.Value.Length; col++)
                        {
                            SetCellValue(worksheet.Cell(CellName(row.Key, col)), cellStyle, row.Value[col]);
                        }

                        int progress = (row.Key * 100) / rows.Count;
                        this.ExportProgress?.Invoke(this, progress);
                    }

                    for (int col = 1; col < 5; col++)
                    {
                        worksheet.Column(col).AdjustToContents();
                    }

                    string filename = model.FullPathXml;
                    filename = filename.Remove(filename.LastIndexOf("."), filename.Length - filename.LastIndexOf("."));
                    filename += "_ClassAttributes.xlsx";
                    try
                    {
                        workbook.SaveAs(filename);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
    }
}
