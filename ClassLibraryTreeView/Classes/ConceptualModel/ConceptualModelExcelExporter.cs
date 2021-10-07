using System.Collections.Generic;
using System.Windows.Forms;
using ClassLibraryTreeView.Classes;
using System;
using ClosedXML.Excel;
using ClassLibraryTreeView.Classes.CellStyle;
using CellStyle = ClassLibraryTreeView.Classes.CellStyle.CellStyle;

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
                        IXLStyle classCellStyle = cellStyleFactory.CreateCellStyleForClass();

                        SetCellValue(worksheet.Cell(CellName(0, 0)), headerCellStyle, $"Class ID");
                        SetCellValue(worksheet.Cell(CellName(0, 1)), headerCellStyle, $"Class Name");
                        SetCellValue(worksheet.Cell(CellName(0, 2)), headerCellStyle, $"Attribute ID");
                        SetCellValue(worksheet.Cell(CellName(0, 3)), headerCellStyle, $"Attribute Name");

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
