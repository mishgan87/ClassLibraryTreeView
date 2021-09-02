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
            using (SpreadsheetDocument document = SpreadsheetDocument.Create(fileName, SpreadsheetDocumentType.Workbook))
            {
                WorkbookPart workbookPart = document.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();
                WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();

                FileVersion fv = new FileVersion();
                fv.ApplicationName = "Microsoft Office Excel";
                worksheetPart.Worksheet = new Worksheet(new SheetData());
                WorkbookStylesPart wbsp = workbookPart.AddNewPart<WorkbookStylesPart>();

                wbsp.Stylesheet = GenerateStyleSheet();
                wbsp.Stylesheet.Save();

                //Создаем лист в книге
                Sheets sheets = workbookPart.Workbook.AppendChild(new Sheets());
                Sheet sheet = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = $"Permissible Grid" };
                sheets.Append(sheet);
                SheetData sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();

                // Создаём permissible grid
                int maxDepth = model.MaxDepth + 1;
                GridCell[,] grid = model.GeneratePermissibleGrid();

                // Задаем колонки

                Columns columnsList = worksheetPart.Worksheet.GetFirstChild<Columns>();
                if (columnsList == null)
                {
                    columnsList = new Columns();
                }

                for (int depth = 0; depth < maxDepth; depth++)
                {
                    columnsList.Append(new Column() { Width = 50, CustomWidth = true });
                }

                for (int depth = maxDepth; depth < grid.GetLength(1); depth++)
                {
                    columnsList.Append(new Column() { Width = 5, CustomWidth = true });
                }

                worksheetPart.Worksheet.InsertAt(columnsList, 0);

                // Пишем таблицу в эксель

                uint rowIndex = 1;
                for (int row = 0; row < grid.GetLength(0); row++)
                {
                    Row sheetRow = new Row() { RowIndex = rowIndex };
                    sheetData.Append(sheetRow);

                    for (int col = 0; col < grid.GetLength(1); col++)
                    {
                        AddCell(sheetRow, col + 1, grid[row, col]);
                    }

                    rowIndex++;
                }

                workbookPart.Workbook.Save();
                document.Close();
            }

            using (SpreadsheetDocument document = SpreadsheetDocument.Open(fileName, true))
            {
                IEnumerable<Sheet> sheets = document.WorkbookPart.Workbook.Descendants<Sheet>().Where(s => s.Name == "Permissible Grid");
                WorksheetPart worksheetPart = (WorksheetPart)document.WorkbookPart.GetPartById(sheets.First().Id);
                Worksheet worksheet = worksheetPart.Worksheet;

                MergeCells mergeCells = new MergeCells();
                worksheet.InsertAfter(mergeCells, worksheet.Elements<SheetData>().First());

                MergeCell mergeCell = new MergeCell()
                {
                    Reference = new StringValue("A1:B1")
                };
                mergeCells.Append(mergeCell);

                worksheet.Save();
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
        // Добавление ячейки в строку
        static void AddCell(Row row, int col, GridCell cell)
        {
            /*
            // Cell refCell = null;
            Cell newCell = new Cell()
            {
                CellReference = $"{GetExcelColumnName(col)}{row.RowIndex}",
                StyleIndex = styleIndex,
                CellValue = new CellValue(value),
                DataType = new EnumValue<CellValues>(CellValues.String)
            };
            row.InsertBefore(newCell, null);
            return newCell;
            */
            Cell refCell = null;
            Cell newCell = new Cell() { CellReference = $"{GetExcelColumnName(col)}{row.RowIndex}", StyleIndex = cell.StyleIndex };
            row.InsertBefore(newCell, refCell);

            newCell.CellValue = new CellValue(cell.Text);
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

                    // 0 - Шрифт по умолчанию
                    new Font(
                        new FontSize() { Val = 11 },
                        new Color() { Rgb = new HexBinaryValue() { Value = "000000" } },
                        new FontName() { Val = "Calibri" }),

                    // 1 - Times New Roman - 11 - Жирный - Чёрный
                    new Font(
                        new Bold(),
                        new FontSize() { Val = 10 },
                        new Color() { Rgb = new HexBinaryValue() { Value = "000000" } },
                        new FontName() { Val = "Times New Roman" }),

                    // 2 - Times New Roman - 12 - Белый
                    new Font(
                        new FontSize() { Val = 12 },
                        new Color() { Rgb = new HexBinaryValue() { Value = "FFFFFFFF" } },
                        new FontName() { Val = "Times New Roman" }),

                    // 3 - Times New Roman - 12 - Чёрный.
                    new Font(
                        new FontSize() { Val = 12 },
                        new Color() { Rgb = new HexBinaryValue() { Value = "000000" } },
                        new FontName() { Val = "Times New Roman" })
                ),

                // Заполнение цветом
                new Fills(

                    // 0 - Без заполнения
                    new Fill(
                        new PatternFill() { PatternType = PatternValues.None }
                    ),

                    // 1 - Зелёный
                    new Fill(
                        new PatternFill( new ForegroundColor() { Rgb = new HexBinaryValue() { Value = "FFFF00" } } )
                        { PatternType = PatternValues.Solid }
                    ),

                    // 2 - Синий
                    new Fill(
                        new PatternFill( new ForegroundColor() { Rgb = new HexBinaryValue() { Value = "0000FF" } } )
                        { PatternType = PatternValues.Solid }
                    ),

                    // 3 - Зелёный
                    new Fill(
                        new PatternFill(new ForegroundColor() { Rgb = new HexBinaryValue() { Value = "FFFF00" } })
                        { PatternType = PatternValues.Solid }
                    ),

                    // 4 - Лайм
                    new Fill(
                        new PatternFill(new ForegroundColor() { Rgb = new HexBinaryValue() { Value = "0080000" } })
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

                    // 1
                    new CellFormat(new Alignment() { Horizontal = HorizontalAlignmentValues.Left, Vertical = VerticalAlignmentValues.Center, WrapText = true })
                    { FontId = 1, FillId = 2, BorderId = 1, ApplyFont = true },

                    // 2
                    new CellFormat(new Alignment() { Horizontal = HorizontalAlignmentValues.Left, Vertical = VerticalAlignmentValues.Center, WrapText = true })
                    { FontId = 2, FillId = 0, BorderId = 2, ApplyFont = true },

                    // 3
                    new CellFormat() { FontId = 3, FillId = 0, BorderId = 2, ApplyFont = true, NumberFormatId = 4 },

                    // 4
                    new CellFormat() { FontId = 0, FillId = 2, BorderId = 0, ApplyFill = true },

                    // 5 - атрибуты в шапке таблицы
                    new CellFormat(
                        new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Bottom , TextRotation = 90 }
                    )
                    { FontId = 2, FillId = 2, BorderId = 2, ApplyAlignment = true },

                    // 6 - имена классов
                    new CellFormat(
                        new Alignment() { Horizontal = HorizontalAlignmentValues.Left, Vertical = VerticalAlignmentValues.Center }
                    )
                    { FontId = 1, FillId = 3, BorderId = 2, ApplyAlignment = true },

                    // 7 - значение presence
                    new CellFormat(
                        new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center }
                    )
                    { FontId = 0, FillId = 0, BorderId = 2, ApplyAlignment = true },

                    // 8 - Задает числовой формат полю
                    new CellFormat(
                        new Alignment() { Horizontal = HorizontalAlignmentValues.Right, Vertical = VerticalAlignmentValues.Center, WrapText = true }
                    )
                    { FontId = 2, FillId = 0, BorderId = 2, ApplyFont = true, NumberFormatId = 4 },

                    // 9 - class id
                    new CellFormat(
                        new Alignment() { Horizontal = HorizontalAlignmentValues.Left, Vertical = VerticalAlignmentValues.Center }
                    )
                    { FontId = 1, FillId = 4, BorderId = 2, ApplyAlignment = true }
                )
            );
        }
    }
}
