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

namespace ClassLibraryTreeView
{
    public partial class ConceptualModel
    {
        public class ConceptualModelExcelExporter
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
            private ConcurrentDictionary<int, CMClass> AddClassToClassRows(CMClass cmClassRoot, int startRowIndex)
            {
                ConcurrentDictionary<int, CMClass> classRows = new ConcurrentDictionary<int, CMClass>();
                Queue<CMClass> queueOfClasses = new Queue<CMClass>();
                List<CMClass> listOfClasses = new List<CMClass>();
                queueOfClasses.Enqueue(cmClassRoot);
                listOfClasses.Add(cmClassRoot);

                Func<CMClass, List<CMClass>, int>FindIndex = (cmClass, cmList) =>
                {
                    int cmIndex = 0;
                    foreach(CMClass cmItem in cmList)
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
                    CMClass cmClass = queueOfClasses.Dequeue();

                    if (cmClass.Children.Count > 0)
                    {
                        int parentIndex = FindIndex(cmClass, listOfClasses) + 1;
                        int addingIndex = 0;
                        foreach (CMClass cmClassChild in cmClass.Children.Values)
                        {
                            queueOfClasses.Enqueue(cmClassChild);
                            listOfClasses.Insert(parentIndex + addingIndex, cmClassChild);
                            addingIndex++;
                        }
                    }
                }

                int index = startRowIndex;
                foreach (CMClass cmClass in listOfClasses)
                {
                    if (classRows.TryAdd(index, cmClass))
                    {
                        index++;
                    }
                }

                return classRows;
            }
            private void InsertClassIntoWorksheet(KeyValuePair<int, CMClass> classRow, IXLWorksheet worksheet, int classRowsCount, int maxDepth, ConceptualModel model, Queue<string> mergedRanges)
            {
                Action<IXLCell, IXLStyle, string> SetCellValue = (cellReference, cellStyle, cellValue) =>
                {
                    cellReference.Style = cellStyle;
                    cellReference.Value = cellValue;
                };

                IXLStyle attributesGroupCellStyle = cellStyleFactory.CreateCellStyle(CellStyle.AttributesGroup);
                IXLStyle attributeCellStyle = cellStyleFactory.CreateCellStyle(CellStyle.Attribute);
                IXLStyle classCellStyle = cellStyleFactory.CreateCellStyle(CellStyle.Class);
                IXLStyle classIdCellStyle = cellStyleFactory.CreateCellStyle(CellStyle.ClassId);
                IXLStyle disciplineCellStyle = cellStyleFactory.CreateCellStyle(CellStyle.Discipline);

                CMClass cmClass = classRow.Value;
                int classDepth = cmClass.Depth;

                SetCellValue(worksheet.Cell(CellName(classRow.Key, maxDepth + 2)), classIdCellStyle, cmClass.Id); // set class id cell
                SetCellValue(worksheet.Cell(CellName(classRow.Key, classDepth)), classCellStyle, cmClass.Name); // set class name cell
                SetCellValue(worksheet.Cell(CellName(classRow.Key, maxDepth + 1)), disciplineCellStyle, ""); // set class discipline cell

                if (maxDepth != classDepth) // merge subclass cells
                {
                    mergedRanges.Enqueue($"{CellName(classRow.Key, classDepth)}:{CellName(classRow.Key, maxDepth)}");
                }

                int col = maxDepth + 3;
                foreach (string group in model.attributes.Keys)
                {
                    foreach (CMAttribute attribute in model.attributes[group].Values)
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
            public DataTable CreatePermissibleGrid(ConceptualModel model)
            {
                DataTable dataTable = new DataTable();

                Dictionary<string, CMClass> map = null;
                if (model.Physicals != null)
                {
                    map = model.Physicals;
                }
                else
                {
                    if (model.Functionals != null)
                    {
                        map = model.Functionals;
                    }
                }

                if (map == null)
                {
                    return null;
                }
                // calculate maximum depth of model classes

                int maxDepth = 0;
                foreach (Dictionary<string, CMClass> classesMap in model.classes.Values)
                {
                    foreach (CMClass cmClass in classesMap.Values)
                    {
                        int depth = cmClass.Depth;
                        if (depth > maxDepth)
                        {
                            maxDepth = depth;
                        }
                    }
                }

                int count = maxDepth + model.AttributesCount + 2;

                int row = 3;

                ConcurrentDictionary<int, CMClass> classRows = new ConcurrentDictionary<int, CMClass>(); // dictionary of pairs "row number - conceptual model class"

                foreach (CMClass cmClass in map.Values) // fill dictionary of pairs <row number, conceptual model class>
                {
                    if (cmClass.Parent == null)
                    {
                        classRows = AddClassToClassRows(cmClass, row);
                        break;
                    }
                }

                for (int columnIndex = 0; columnIndex <= maxDepth; columnIndex++)
                {
                    dataTable.Columns.Add($"");
                }
                
                /*foreach (string group in model.attributes.Keys)
                {
                    foreach (CMAttribute attribute in model.attributes[group].Values)
                    {
                        dataTable.Columns.Add($"{attribute.Id} : {attribute.Name}");
                    }
                }*/

                foreach(var classRow in classRows)
                {
                    CMClass cmClass = classRow.Value;
                    int classDepth = cmClass.Depth;
                    string[] rowItems = new string[maxDepth + 1];

                    for (int columnIndex = 0; columnIndex < classDepth; columnIndex++)
                    {
                        rowItems[columnIndex] = "";
                    }

                    rowItems[classDepth] = cmClass.Name;

                    dataTable.Rows.Add(rowItems);
                    /*
                    if (maxDepth != classDepth) // merge subclass cells
                    {
                        mergedRanges.Enqueue($"{CellName(classRow.Key, classDepth)}:{CellName(classRow.Key, maxDepth)}");
                    }

                    int col = maxDepth + 3;
                    foreach (string group in model.attributes.Keys)
                    {
                        foreach (CMAttribute attribute in model.attributes[group].Values)
                        {
                            // SetCellValue(worksheet.Cell($"{CellName(row, col)}"), attributeCellStyle, $"{attribute.Id} : {attribute.Name}");
                            string presence = cmClass.PermissibleAttributePresence(attribute.Id);
                            var cellStyle = cellStyleFactory.CreateCellStyleForAttributePresence(presence);
                            SetCellValue(worksheet.Cell(CellName(classRow.Key, col)), cellStyle, presence);
                            col++;
                        }
                    }
                    */
                    int progress = (classRow.Key * 100) / classRows.Count;
                    this.ExportProgress?.Invoke(this, progress);
                }

                return dataTable;
            }
            public void ExportPermissibleGrid(ConceptualModel model)
            {
                // calculate maximum depth of model classes

                int maxDepth = 0;
                foreach (Dictionary<string, CMClass> map in model.classes.Values)
                {
                    foreach (CMClass cmClass in map.Values)
                    {
                        int depth = cmClass.Depth;
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

                    Dictionary<string, CMClass> map = null;
                    if (model.Physicals != null)
                    {
                        map = model.Physicals;
                    }
                    else
                    {
                        if (model.Functionals != null)
                        {
                            map = model.Functionals;
                        }
                    }

                    if (map == null)
                    {
                        return;
                    }

                    // dictionary of pairs "row number - conceptual model class"
                    ConcurrentDictionary<int, CMClass> classRows = new ConcurrentDictionary<int, CMClass>();

                    // fill dictionary of pairs <row number, conceptual model class>
                    foreach (CMClass cmClass in map.Values)
                    {
                        if (cmClass.Parent == null)
                        {
                            classRows = AddClassToClassRows(cmClass, 3);
                        }
                    }

                    // write header

                    IXLStyle headerCellStyle = cellStyleFactory.CreateCellStyle(CellStyle.Header);
                    SetCellValue(worksheet.Cell(CellName(2, 0)), headerCellStyle, $"Classes ({map.Count})"); // set classes count header cell
                    SetCellValue(worksheet.Cell(CellName(2, maxDepth + 1)), headerCellStyle, $"Discipline"); // set class discipline header cell
                    SetCellValue(worksheet.Cell(CellName(2, maxDepth + 2)), headerCellStyle, $"Class ID"); // set class id header cell

                    Queue<string> mergedRanges = new Queue<string>();
                    mergedRanges.Enqueue($"{CellName(0, 0)}:{CellName(1, maxDepth + 2)}");
                    mergedRanges.Enqueue($"{CellName(2, 0)}:{CellName(2, maxDepth)}");

                    IXLStyle attributesGroupCellStyle = cellStyleFactory.CreateCellStyle(CellStyle.AttributesGroup);
                    IXLStyle attributeCellStyle = cellStyleFactory.CreateCellStyle(CellStyle.Attribute);
                    IXLStyle classCellStyle = cellStyleFactory.CreateCellStyle(CellStyle.Class);
                    IXLStyle classIdCellStyle = cellStyleFactory.CreateCellStyle(CellStyle.ClassId);
                    IXLStyle disciplineCellStyle = cellStyleFactory.CreateCellStyle(CellStyle.Discipline);

                    string mergedCell = "";
                    int col = maxDepth + 3;
                    foreach (string group in model.attributes.Keys)
                    {
                        mergedCell = $"{CellName(0, col)}";
                        IXLCell cell = worksheet.Cell($"{CellName(0, col)}");
                        SetCellValue(cell, attributesGroupCellStyle, group);
                        foreach (CMAttribute attribute in model.attributes[group].Values)
                        {
                            cell = worksheet.Cell($"{CellName(1, col)}");
                            SetCellValue(cell, attributeCellStyle, $"{attribute.Id} : {attribute.Name}");
                            col++;
                        }
                        mergedRanges.Enqueue($"{mergedCell}:{CellName(0, col - 1)}");
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
                    foreach (string range in mergedRanges)
                    {
                        if (range != null)
                        {
                            worksheet.Range(range).Merge();
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
                }
            }
            public void ExportClassAttributes(ConceptualModel model)
            {
                Dictionary<string, CMClass> map = null;
                if (model.Physicals != null)
                {
                    map = model.Physicals;
                }
                else
                {
                    if (model.Functionals != null)
                    {
                        map = model.Functionals;
                    }
                }

                if (map != null)
                {
                    Action<IXLCell, IXLStyle, string> SetCellValue = (cellReference, cellStyle, cellValue) =>
                    {
                        cellReference.Style = cellStyle;
                        cellReference.Value = cellValue;
                    };
                    int rowIndex = 1;
                    List<KeyValuePair<int, string[]>> rows = new List<KeyValuePair<int, string[]>>();

                    foreach (CMClass cmClass in map.Values)
                    {
                        Dictionary<string, CMAttribute> attributes = cmClass.PermissibleAttributes;
                        if (attributes.Count > 0)
                        {
                            foreach (CMAttribute attribute in attributes.Values)
                            {
                                rows.Add(new KeyValuePair<int, string[]>(rowIndex, new string[] { cmClass.Id, cmClass.Name, attribute.Id, attribute.Name }));
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

                        foreach (KeyValuePair<int, string[]> row in rows)
                        {
                            if (row.Key % 2 == 0)
                            {
                                // style = CellStyle.ClassId;
                            }

                            for (int col = 0; col < row.Value.Length; col++)
                            {
                                SetCellValue(worksheet.Cell(CellName(row.Key, col)), classCellStyle, row.Value[col]);
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
}
