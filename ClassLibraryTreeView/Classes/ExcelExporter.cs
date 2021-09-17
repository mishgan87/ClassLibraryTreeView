using ClassLibraryTreeView.Classes;
using ClassLibraryTreeView.Interfaces;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassLibraryTreeView
{
    class ExcelExporter
    {
        // public event EventHandler<ExportProgressEventArgs> GetProgress;
        // public event EventHandler ExportDone;
        static object syncRoot = new object();
        public ExcelExporter()
        {
        }
        public void ExportPermissibleGrid(string filename, ConceptualModel model)
        {
            try
            {
                // Create a spreadsheet document by using the file name
                using (SpreadsheetDocument document = SpreadsheetDocument.Create(filename, SpreadsheetDocumentType.Workbook))
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

                    // Массив объединённых ячеек
                    MergeCells mergeCells = new MergeCells();

                    // Заполняем permissible grid

                    //List<List<GridCell>> grid = model.GetPermissibleGrid(mergeCells);
                    List<List<GridCell>> grid = new List<List<GridCell>>();
                    /*
                    // Задаем колонки и их ширину
                    Columns columns = worksheetPart.Worksheet.GetFirstChild<Columns>();
                    if (columns == null)
                    {
                        columns = new Columns();
                    }

                    for (int col = 0; col < grid[0].Count; col++)
                    {
                        DoubleValue width = 4.29;
                        
                        if (col > 0 && col <= model.maxDepth)
                        {
                            
                        }

                        if (col == model.maxDepth + 1)
                        {
                            
                        }
                        if (col == model.maxDepth + 2)
                        {
                            width = 50;
                        }

                        if (col > model.maxDepth + 2)
                        {
                            
                        }
                        
                        // columns.Append(new Column() { Width = 4.29, CustomWidth = true });
                        columns.Append(new Column());
                    }
                    worksheetPart.Worksheet.InsertAt(columns, 0);
                    */
                    Cell cell = null;
                    Cell cellStart = null;

                    

                    StringValue stringValue = $"A1:{GetExcelColumnName(model.maxDepth + 3)}2";

                    mergeCells.Append(new MergeCell() { Reference = stringValue});

                    List<Row> sheetDateRows = new List<Row>();
                    uint row = 1;
                    foreach (List<GridCell> gridRow in grid)
                    {
                        Row sheetDataRow = new Row() { RowIndex = row, Height = 21.25, CustomHeight = true };
                        if (row == 2)
                        {
                            sheetDataRow = new Row() { RowIndex = row };
                        }
                        // sheetData.Append(sheetDataRow);
                        row++;

                        int col = 0;
                        foreach (GridCell gridCell in gridRow)
                        {
                            // cell = AddCell(sheetDataRow, col, gridCell.Text, gridCell.StyleIndex);
                            col++;
                            // if(gridCell.Merge == GridCellMergeProperty.MergingStart)
                            {
                                cellStart = cell;
                            }
                            // if (gridCell.Merge == GridCellMergeProperty.MergingFinish)
                            {
                                if (cellStart != null)
                                {
                                    mergeCells.Append(new MergeCell() { Reference = new StringValue($"{cellStart.CellReference.Value}:{cell.CellReference.Value}") });
                                    cellStart = null;
                                }
                            }
                        }

                        sheetDateRows.Add(sheetDataRow);
                    }

                    sheetData.Append(sheetDateRows.ToArray());

                    worksheetPart.Worksheet.InsertAfter(mergeCells, worksheetPart.Worksheet.Elements<SheetData>().First()); // Добавляем к документу список объединённых ячеек
                    worksheetPart.Worksheet.Save();

                    workbookpart.Workbook.Save();
                    document.Close();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
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
        static Cell AddCell(Row row, int columnIndex, string text, uint styleIndex)
        {
            Cell refCell = null;
            Cell newCell = new Cell()
            {
                CellReference = $"{GetExcelColumnName(columnIndex + 1)}{row.RowIndex}",
                StyleIndex = styleIndex
            };
            row.InsertBefore(newCell, refCell);

            newCell.CellValue = new CellValue(text);
            newCell.DataType = new EnumValue<CellValues>(CellValues.String);

            return newCell;
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

                    // 0 - Times New Roman - 10 - Чёрный
                    new Font(
                        // new Bold(),
                        new FontSize() { Val = 10 },
                        new Color() { Rgb = new HexBinaryValue() { Value = "000000" } },
                        new FontName() { Val = "Calibri" }),

                    // 1 - Times New Roman - 10 - Белый
                    new Font(
                        new FontSize() { Val = 10 },
                        new Color() { Rgb = new HexBinaryValue() { Value = "FFFFFFFF" } },
                        new FontName() { Val = "Calibri" }),

                    // 2 - Times New Roman - 10 - Чёрный
                    new Font(
                        // new Bold(),
                        new FontSize() { Val = 11 },
                        new Color() { Rgb = new HexBinaryValue() { Value = "000000" } },
                        new FontName() { Val = "Calibri" }),

                    // 3 - Times New Roman - 11 - Белый
                    new Font(
                        new FontSize() { Val = 11 },
                        new Color() { Rgb = new HexBinaryValue() { Value = "FFFFFFFF" } },
                        new FontName() { Val = "Calibri" })
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
                    ),

                    // 3 - Жёлтый
                    new Fill(
                    new DocumentFormat.OpenXml.Spreadsheet.PatternFill(
                    new DocumentFormat.OpenXml.Spreadsheet.ForegroundColor() { Rgb = new HexBinaryValue() { Value = "FFFFFF00" } }
                    )
                    { PatternType = PatternValues.Solid }),

                    // 4 - Presence Unselect
                    new Fill(
                    new DocumentFormat.OpenXml.Spreadsheet.PatternFill(
                    new DocumentFormat.OpenXml.Spreadsheet.ForegroundColor() { Rgb = new HexBinaryValue() { Value = "00CCCC" } }
                    )
                    { PatternType = PatternValues.Solid }),
                    
                    // 5 - Presence NotApplicable
                    new Fill(
                    new DocumentFormat.OpenXml.Spreadsheet.PatternFill(
                    new DocumentFormat.OpenXml.Spreadsheet.ForegroundColor() { Rgb = new HexBinaryValue() { Value = "00CC66" } }
                    )
                    { PatternType = PatternValues.Solid }),
                    
                    // 6 - Presence Optional
                    new Fill(
                    new DocumentFormat.OpenXml.Spreadsheet.PatternFill(
                    new DocumentFormat.OpenXml.Spreadsheet.ForegroundColor() { Rgb = new HexBinaryValue() { Value = "80FF00" } }
                    )
                    { PatternType = PatternValues.Solid }),

                    // 7 - Presence Preferred
                    new Fill(
                    new DocumentFormat.OpenXml.Spreadsheet.PatternFill(
                    new DocumentFormat.OpenXml.Spreadsheet.ForegroundColor() { Rgb = new HexBinaryValue() { Value = "FF6666" } }
                    )
                    { PatternType = PatternValues.Solid }),

                    // 8 - Presence Required
                    new Fill(
                    new DocumentFormat.OpenXml.Spreadsheet.PatternFill(
                    new DocumentFormat.OpenXml.Spreadsheet.ForegroundColor() { Rgb = new HexBinaryValue() { Value = "6666FF" } }
                    )
                    { PatternType = PatternValues.Solid })

                ),

                // Границы ячейки
                new Borders(

                    // 0 - Граней нет
                    new Border(
                        new LeftBorder(),
                        new RightBorder(),
                        new TopBorder(),
                        new BottomBorder(),
                        new DiagonalBorder()),

                    // 1 - Грани все, белые
                    new Border(
                        new LeftBorder(
                            new Color() { Rgb = new HexBinaryValue() { Value = "FFFFFFFF" } }
                        )
                        { Style = BorderStyleValues.Thin },
                        new RightBorder(
                            new Color() { Rgb = new HexBinaryValue() { Value = "FFFFFFFF" } }
                        )
                        { Style = BorderStyleValues.Thin },
                        new TopBorder(
                            new Color() { Rgb = new HexBinaryValue() { Value = "FFFFFFFF" } }
                        )
                        { Style = BorderStyleValues.Thin },
                        new BottomBorder(
                            new Color() { Rgb = new HexBinaryValue() { Value = "FFFFFFFF" } }
                        )
                        { Style = BorderStyleValues.Thin },
                        new DiagonalBorder()
                    )

                ),

                // Формат ячейки
                new CellFormats(

                    // 0 - The default cell style
                    new CellFormat(
                        new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center }
                        )
                    { FontId = 0, FillId = 0, BorderId = 0 },

                    // 1 - class name
                    new CellFormat(
                        new Alignment() { Horizontal = HorizontalAlignmentValues.Left, Vertical = VerticalAlignmentValues.Center }
                    )
                    { FontId = 0, FillId = 0, BorderId = 0, ApplyAlignment = true, ApplyFill = true, ApplyFont = true },

                    // 2 - class id
                    new CellFormat(
                        new Alignment() { Horizontal = HorizontalAlignmentValues.Left, Vertical = VerticalAlignmentValues.Center }
                    )
                    { FontId = 0, FillId = 3, BorderId = 0, ApplyAlignment = true, ApplyFill = true, ApplyFont = true },

                    // 3 - attribute name
                    new CellFormat(
                        new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Bottom, TextRotation = 90 }
                    )
                    { FontId = 3, FillId = 2, BorderId = 1, ApplyAlignment = true, ApplyFill = true, ApplyFont = true },

                    // 4 - Presence Unselect
                    new CellFormat(
                        new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center }
                    )
                    { FontId = 0, FillId = 4, BorderId = 0, ApplyAlignment = true, ApplyFill = true, ApplyFont = true },

                    // 5 - Presence NotApplicable
                    new CellFormat(
                        new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center }
                    )
                    { FontId = 0, FillId = 5, BorderId = 0, ApplyAlignment = true, ApplyFill = true, ApplyFont = true },

                    // 6 - Presence Optional
                    new CellFormat(
                        new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center }
                    )
                    { FontId = 0, FillId = 6, BorderId = 0, ApplyAlignment = true, ApplyFill = true, ApplyFont = true },

                    // 7 - Presence Preferred
                    new CellFormat(
                        new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center }
                    )
                    { FontId = 0, FillId = 7, BorderId = 0, ApplyAlignment = true, ApplyFill = true, ApplyFont = true },

                    // 8 - Presence Required
                    new CellFormat(
                        new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center }
                    )
                    { FontId = 0, FillId = 8, BorderId = 0, ApplyAlignment = true, ApplyFill = true, ApplyFont = true },

                    // 9 - Other
                    new CellFormat(
                        new Alignment() { Horizontal = HorizontalAlignmentValues.Left, Vertical = VerticalAlignmentValues.Center }
                    )
                    { FontId = 3, FillId = 2, BorderId = 1, ApplyAlignment = true, ApplyFill = true, ApplyFont = true }

                )
            );
        }
    }
}
