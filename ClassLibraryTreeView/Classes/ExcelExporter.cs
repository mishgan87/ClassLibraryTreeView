using ClassLibraryTreeView.Classes;
using ClassLibraryTreeView.Interfaces;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ClassLibraryTreeView
{
    public class ExportProgressEventArgs : EventArgs
    {
        public int Progress { get; set; }
        public bool Done { get; set; }
        public string Text { get; set; }
    }
    class ExcelExporter
    {
        // public event EventHandler<ExportProgressEventArgs> GetProgress;
        // public event EventHandler<ExportProgressEventArgs> ExportDone;
        private ConceptualModel model;
        private string fileName;
        public ExcelExporter(string nameOfFile, ConceptualModel modelRef)
        {
            fileName = nameOfFile;
            model = modelRef;
        }
        public void ExportPermissibleGrid()
        {
            // Create a spreadsheet document by using the file name
            using (SpreadsheetDocument document = SpreadsheetDocument.Create(fileName, SpreadsheetDocumentType.Workbook))
            {
                // Add a WorkbookPart and Workbook objects
                WorkbookPart workbookpart = document.AddWorkbookPart();
                workbookpart.Workbook = new Workbook();

                // Add a WorksheetPart
                WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();

                // Create Worksheet and SheetData objects
                worksheetPart.Worksheet = new Worksheet(new SheetData());

                // Add a Sheets object
                Sheets sheets = document.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());

                // Append the new worksheet named "Permissible Grid" and associate it with the workbook
                Sheet sheet = new Sheet()
                {
                    Id = document.WorkbookPart.GetIdOfPart(worksheetPart),
                    SheetId = 1,
                    Name = "Permissible Grid"
                };
                sheets.Append(sheet);

                // Append stylesheets
                WorkbookStylesPart wbsp = workbookpart.AddNewPart<WorkbookStylesPart>();
                wbsp.Stylesheet = GenerateStyleSheet();
                wbsp.Stylesheet.Save();

                // Get the sheetData cell table
                SheetData sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();

                // Вставляем список объединённых ячеек
                /*
                Worksheet worksheet = worksheetPart.Worksheet;
                MergeCells mergeCells = new MergeCells();
                worksheet.InsertAfter(mergeCells, worksheet.Elements<SheetData>().First());
                */
                // Заполняем permissible grid

                List<IClass> merged = model.merged;

                if (merged.Count > 0)
                {
                    int maxDepth = model.MaxDepth + 2;

                    // Заполняем шапку таблицы

                    Row rowFirst = new Row() { RowIndex = 1 };
                    sheetData.Append(rowFirst);
                    Row rowSecond = new Row() { RowIndex = 2 };
                    sheetData.Append(rowSecond);
                    Row rowThird = new Row() { RowIndex = 3 };
                    sheetData.Append(rowThird);

                    AddCell(rowThird, 0, $"Classes ({merged.Count})", 0);
                    AddCell(rowThird, maxDepth, $"Class ID", 0);

                    maxDepth++;
                    int attributeIndex = 0;
                    IAttribute[] attributes = new IAttribute[model.AttributesCount];
                    foreach (KeyValuePair<string, Dictionary<string, IAttribute>> group in model.attributes)
                    {
                        AddCell(rowFirst, maxDepth + attributeIndex, group.Key, 0);
                        foreach (IAttribute attribute in group.Value.Values)
                        {
                            AddCell(rowSecond, maxDepth + attributeIndex, $"{attribute.Name} : {attribute.Id}", 3);
                            attributes[attributeIndex] = attribute;
                            attributeIndex++;
                        }
                    }

                    // Заполняем таблицу классами и присутсвием в них атрибутов

                    uint rowIndex = 4;
                    foreach (IClass cmClass in merged)
                    {
                        Row row = new Row() { RowIndex = rowIndex };
                        sheetData.Append(row);

                        int classDepth = model.ClassDepth(cmClass) + 1;

                        AddCell(row, classDepth, $"{cmClass.Name}", 1);
                        AddCell(row, maxDepth - 1, $"{cmClass.Id}", 2);

                        for(attributeIndex = 0; attributeIndex < model.AttributesCount; attributeIndex++)
                        {
                            IAttribute attribute = attributes[attributeIndex];
                            AddCell(row, maxDepth + attributeIndex, model.Presence(cmClass, attribute), 4);
                        }

                        rowIndex++;
                    }
                }

                /*
                MergeCell mergeCell = new MergeCell()
                {
                    Reference = new StringValue("A1:B1")
                };
                mergeCells.Append(mergeCell);
                worksheet.Save();
                */
                workbookpart.Workbook.Save();
                document.Close();
            }

        }
        private static string GetExcelColumnName(int number)
        {
            string name = "";
            while (number > 0)
            {
                int modul = (number - 1) % 26;
                name = Convert.ToChar('A' + modul) + name;
                number = (number - modul) / 26;
            }
            return name;
        }
        // Add the cell to the cell table
        static void AddCell(Row row, int columnIndex, string text, uint styleIndex)
        {
            Cell refCell = null;
            Cell newCell = new Cell()
            {
                CellReference = $"{GetExcelColumnName(columnIndex)}{row.RowIndex}",
                StyleIndex = styleIndex
            };
            row.InsertBefore(newCell, refCell);

            newCell.CellValue = new CellValue(text);
            newCell.DataType = new EnumValue<CellValues>(CellValues.String);
        }
        // Важный метод, при вставки текстовых значений надо использовать.
        // Убирает из строки запрещенные спец символы.
        // Если не использовать, то при наличии в строке таких символов, вылетит ошибка.
        private string ReplaceHexadecimalSymbols(string txt)
        {
            string r = "[\x00-\x08\x0B\x0C\x0E-\x1F\x26]";
            return Regex.Replace(txt, r, "", RegexOptions.Compiled);
        }
        // Генерирует стили ячеек
        static Stylesheet GenerateStyleSheet()
        {
            return new Stylesheet(

                // Шрифты

                new Fonts(

                    // 0 - Times New Roman - 12 - Чёрный
                    new Font(
                        // new Bold(),
                        new FontSize() { Val = 12 },
                        new Color() { Rgb = new HexBinaryValue() { Value = "000000" } },
                        new FontName() { Val = "Times New Roman" }),

                    // 1 - Times New Roman - 12 - Белый
                    new Font(
                        new FontSize() { Val = 12 },
                        new Color() { Rgb = new HexBinaryValue() { Value = "FFFFFFFF" } },
                        new FontName() { Val = "Times New Roman" })
                ),

                // Заполнение цветом

                new Fills(

                    // 0 - Без заполнения
                    new Fill(
                        new PatternFill() { PatternType = PatternValues.None }
                    ),

                    // 1 - Серый
                    new Fill(
                        new PatternFill( new ForegroundColor() { Rgb = new HexBinaryValue() { Value = "FFAAAAAA" } } )
                        { PatternType = PatternValues.Solid }
                    ),

                    // 2 - Синий
                    new Fill(
                        new PatternFill( new ForegroundColor() { Rgb = new HexBinaryValue() { Value = "0000FF" } } )
                        { PatternType = PatternValues.Solid }
                    )

                ),

                // Границы ячейки
                new Borders(

                    // 0 - Грани
                    new Border(
                        new LeftBorder(),
                        new RightBorder(),
                        new TopBorder(),
                        new BottomBorder(),
                        new DiagonalBorder()),

                    // 1 - Грани
                    new Border(
                        new LeftBorder(
                            new Color() { Auto = true }
                        )
                        { Style = BorderStyleValues.Medium },
                        new RightBorder(
                            new Color() { Indexed = (UInt32Value)64U }
                        )
                        { Style = BorderStyleValues.Medium },
                        new TopBorder(
                            new Color() { Auto = true }
                        )
                        { Style = BorderStyleValues.Medium },
                        new BottomBorder(
                            new Color() { Indexed = (UInt32Value)64U }
                        )
                        { Style = BorderStyleValues.Medium },
                        new DiagonalBorder()
                    ),

                    // 2 - Грани
                    new Border(
                        new LeftBorder(
                            new Color() { Auto = true }
                        )
                        { Style = BorderStyleValues.Thin },
                        new RightBorder(
                            new Color() { Indexed = (UInt32Value)64U }
                        )
                        { Style = BorderStyleValues.Thin },
                        new TopBorder(
                            new Color() { Auto = true }
                        )
                        { Style = BorderStyleValues.Thin },
                        new BottomBorder(
                            new Color() { Indexed = (UInt32Value)64U }
                        )
                        { Style = BorderStyleValues.Thin },
                        new DiagonalBorder())
                ),

                // Формат ячейки
                new CellFormats(

                    // 0 - The default cell style
                    new CellFormat() { FontId = 0, FillId = 0, BorderId = 0 },

                    // 1 - class name
                    new CellFormat(
                        new Alignment() { Horizontal = HorizontalAlignmentValues.Left, Vertical = VerticalAlignmentValues.Center }
                    )
                    { FontId = 0, FillId = 1, BorderId = 2, ApplyAlignment = true, ApplyFill = true, ApplyFont = true },

                    // 2 - class id
                    new CellFormat(
                        new Alignment() { Horizontal = HorizontalAlignmentValues.Left, Vertical = VerticalAlignmentValues.Center }
                    )
                    { FontId = 0, FillId = 0, BorderId = 2, ApplyAlignment = true, ApplyFill = true, ApplyFont = true },

                    // 3 - attribute name
                    new CellFormat(
                        new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Bottom, TextRotation = 90 }
                    )
                    { FontId = 1, FillId = 2, BorderId = 2, ApplyAlignment = true, ApplyFill = true, ApplyFont = true },

                    // 4 - attribute presence
                    new CellFormat(
                        new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center }
                    )
                    { FontId = 0, FillId = 0, BorderId = 2, ApplyAlignment = true, ApplyFill = true, ApplyFont = true }

                )
            );
        }
    }
}
