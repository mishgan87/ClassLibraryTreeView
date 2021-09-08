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
    class ExcelExporter
    {
        // public event EventHandler<ExportProgressEventArgs> GetProgress;
        // public event EventHandler ExportDone;
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

                // Массив объединённых ячеек
                MergeCells mergeCells = new MergeCells();

                // Заполняем permissible grid

                List<IClass> merged = model.merged;

                if (merged.Count > 0)
                {
                    int maxDepth = model.MaxDepth + 2;

                    // Заполняем шапку таблицы

                    Cell cell = null;
                    string mergeRange = "";

                    uint rowIndex = 1;

                    Row[] header = new Row[3];
                    for(rowIndex = 1; rowIndex <= 3; rowIndex++)
                    {
                        header[rowIndex - 1] = new Row() { RowIndex = rowIndex };
                        sheetData.Append(header[rowIndex - 1]);
                    }

                    for (int col = 0; col <= maxDepth; col++)
                    {
                        cell = AddCell(header[0], col, "", 0);
                        if (col == 0)
                        {
                            mergeRange = cell.CellReference.Value;
                        }

                        cell = AddCell(header[1], col, "", 0);
                        if (col == maxDepth)
                        {
                            mergeRange += $":{cell.CellReference.Value}";
                        }

                        string text = "";
                        uint styleIndex = 0;
                        if (col == 0)
                        {
                            text = $"Classes ({merged.Count})";
                            styleIndex = 9;
                        }
                        if (col == maxDepth - 1)
                        {
                            text = $"Discipline";
                            styleIndex = 0;
                        }
                        if (col == maxDepth)
                        {
                            text = $"Class ID";
                            styleIndex = 2;
                        }
                        cell = AddCell(header[2], col, text, styleIndex);
                        if (col == maxDepth)
                        {
                            mergeCells.Append(new MergeCell() { Reference = new StringValue($"A3:{GetExcelColumnName(maxDepth - 1)}3") });
                        }
                    }

                    mergeCells.Append(new MergeCell() { Reference = new StringValue(mergeRange) });

                    maxDepth++;
                    int attributeIndex = 0;
                    IAttribute[] attributes = new IAttribute[model.AttributesCount];
                    foreach (KeyValuePair<string, Dictionary<string, IAttribute>> group in model.attributes)
                    {
                        cell = AddCell(header[0], maxDepth + attributeIndex, group.Key, 0);
                        mergeRange = cell.CellReference.Value;
                        int localAttributeIndex = 0;
                        foreach (IAttribute attribute in group.Value.Values)
                        {
                            if (localAttributeIndex > 0)
                            {
                                cell = AddCell(header[0], maxDepth + attributeIndex, "", 0);
                                if (localAttributeIndex == group.Value.Count - 1)
                                {
                                    mergeRange += $":{cell.CellReference.Value}";
                                }
                            }
                            attributes[attributeIndex] = attribute;
                            cell = AddCell(header[1], maxDepth + attributeIndex, $"{attribute.Name} : {attribute.Id}", 3);
                            cell = AddCell(header[2], maxDepth + attributeIndex, "", 0);
                            localAttributeIndex++;
                            attributeIndex++;
                        }
                        mergeCells.Append(new MergeCell() { Reference = new StringValue(mergeRange) });
                    }

                    // Заполняем таблицу классами и присутсвием в них атрибутов
                    rowIndex = 4;
                    foreach (IClass cmClass in merged)
                    {
                        if (cmClass == null)
                        {
                            continue;
                        }

                        Row row = new Row() { RowIndex = rowIndex };
                        sheetData.Append(row);

                        int classDepth = model.ClassDepth(cmClass);

                        for (int col = 0; col <= maxDepth - 1; col++)
                        {
                            string text = "";
                            uint styleIndex = 0;
                            if (col == classDepth)
                            {
                                text = $"{cmClass.Name}";
                                styleIndex = 1;
                            }
                            if (col == maxDepth - 2)
                            {
                                // text = $"{cmClass.Id}";
                                styleIndex = 2;
                            }
                            if (col == maxDepth - 1)
                            {
                                text = $"{cmClass.Id}";
                                styleIndex = 2;
                            }
                            cell = AddCell(row, col, text, styleIndex);
                        }

                        string start = $"{GetExcelColumnName(classDepth + 1)}{rowIndex}";
                        string finish = $"{GetExcelColumnName(maxDepth - 2)}{rowIndex}";
                        string str = $"{start}:{finish}";
                        mergeCells.Append(
                                    new MergeCell()
                                    {
                                        Reference = new StringValue(str)
                                    });

                        for (attributeIndex = 0; attributeIndex < model.AttributesCount; attributeIndex++)
                        {
                            IAttribute attribute = attributes[attributeIndex];
                            string presence = model.Presence(cmClass, attribute);
                            uint styleIndex = 0;
                            
                            switch(presence)
                            {
                                case "X":
                                    styleIndex = 5;
                                    break;
                                case "O":
                                    styleIndex = 6;
                                    break;
                                case "P":
                                    styleIndex = 7;
                                    break;
                                case "R":
                                    styleIndex = 8;
                                    break;
                                default:
                                    break;
                            }
                            
                            cell = AddCell(row, maxDepth + attributeIndex, presence, styleIndex);
                        }

                        rowIndex++;
                    }

                }

                // Добавляем к документу список объединённых ячеек

                // IEnumerable<Sheet> sheetsGrid = document.WorkbookPart.Workbook.Descendants<Sheet>().Where(s => s.Name == "Permissible Grid");
                // WorksheetPart worksheetpart = (WorksheetPart)document.WorkbookPart.GetPartById(sheetsGrid.First().Id);
                // worksheetpart.Worksheet.InsertAfter(mergeCells, worksheetpart.Worksheet.Elements<SheetData>().First());
                
                worksheetPart.Worksheet.InsertAfter(mergeCells, worksheetPart.Worksheet.Elements<SheetData>().First());
                worksheetPart.Worksheet.Save();
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
                    new CellFormat() { FontId = 0, FillId = 0, BorderId = 0 },

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
                        new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Bottom }
                    )
                    { FontId = 3, FillId = 2, BorderId = 1, ApplyAlignment = true, ApplyFill = true, ApplyFont = true }

                )
            );
        }
    }
}
