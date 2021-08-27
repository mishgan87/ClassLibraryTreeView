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
        public static int ExportPermissibleGrid(string fileName, ConceptualModel model)
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

                // Добавляем в документ набор стилей
                wbsp.Stylesheet = GenerateStyleSheet();
                wbsp.Stylesheet.Save();

                //Создаем лист в книге
                Sheets sheets = workbookPart.Workbook.AppendChild(new Sheets());
                Sheet sheet = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = $"New List" };
                sheets.Append(sheet);
                SheetData sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();

                // Задаем колонки и их ширину
                Columns lstColumns = worksheetPart.Worksheet.GetFirstChild<Columns>();
                if (lstColumns == null)
                {
                    lstColumns = new Columns();
                }

                int maxDepth = model.MaxDepth;
                IAttribute[] attributes = model.attributes.Values.ToArray();
                int columnsCount = model.attributes.Count + maxDepth;
                for (int columnIndex = 0; columnIndex < columnsCount; columnIndex++)
                {
                    lstColumns.Append(new Column() { Min = 1, Max = 10, Width = 5, CustomWidth = true });
                }
                worksheetPart.Worksheet.InsertAt(lstColumns, 0);

                Row rowFirst = new Row() { RowIndex = 1 };
                for (int columnIndex = 0; columnIndex < columnsCount; columnIndex++)
                {
                    string columnName = "";
                    if (columnIndex >= maxDepth)
                    {
                        columnName = ColumnName(attributes[columnIndex - maxDepth]);
                    }
                    InsertCell(rowFirst, columnIndex, columnName, CellValues.String, 2);
                }
                sheetData.Append(rowFirst);

                // Заполняем строки именами классов и значениями допустимых атрибутов

                uint rowIndex = 2;
                rowIndex = AddClass(sheetData, rowIndex, maxDepth,attributes, model.functionals);
                rowIndex = AddClass(sheetData, rowIndex, maxDepth, attributes, model.physicals);

                workbookPart.Workbook.Save();
                document.Close();
            }
            return 0;
        }
        static uint WriteClass(SheetData sheetData, uint rowIndex, int maxDepth, IClass cmClass, Dictionary<string, IClass> map, IAttribute[] attributes)
        {
            uint newRowIndex = rowIndex + 1;
            Row row = new Row() { RowIndex = newRowIndex };
            sheetData.Append(row);

            int depthCurrent = ConceptualModel.ClassDepth(cmClass, map);
            for (int depth = 1; depth <= maxDepth; depth++)
            {
                string text = "";
                if (depth == depthCurrent)
                {
                    text = $"{cmClass.Name}_{cmClass.Xtype}";
                }
                InsertCell(row, depth, text, CellValues.String, 1);
            }

            for (int columnIndex = 0; columnIndex < attributes.Length; columnIndex++)
            {
                IAttribute attribute = attributes[columnIndex];
                string presence = ConceptualModel.AttributePresence(attributes[columnIndex].Id, attributes[columnIndex].Presence, cmClass, map);
                InsertCell(row, columnIndex + maxDepth, presence, CellValues.String, 0);
            }

            return newRowIndex;
        }
        static uint AddClass(SheetData sheetData, uint rowIndex, int maxDepth, IAttribute[] attributes, Dictionary<string, IClass> map)
        {
            uint newRowIndex = rowIndex;
            if (map.Count > 0)
            {
                foreach (IClass cmClass in map.Values)
                {
                    if (cmClass.Extends.Equals(""))
                    {
                        newRowIndex = WriteClass(sheetData, newRowIndex, maxDepth, cmClass, map, attributes);
                        newRowIndex = AddChildren(sheetData, newRowIndex, maxDepth, attributes, cmClass, map);
                    }
                }
            }
            return newRowIndex;
        }
        static uint AddChildren(SheetData sheetData, uint rowIndex, int maxDepth, IAttribute[] attributes, IClass cmClass, Dictionary<string, IClass> map)
        {
            uint newRowIndex = rowIndex;
            if (cmClass.Children.Count != 0)
            {
                foreach (IClass child in cmClass.Children)
                {
                    newRowIndex = WriteClass(sheetData, newRowIndex, maxDepth, child, map, attributes);
                    newRowIndex = AddChildren(sheetData, newRowIndex, maxDepth, attributes, child, map);
                }
            }
            return newRowIndex;
        }
        static string ColumnName(IAttribute attribute)
        {
            string columnName = attribute.Id;
            if (!attribute.Name.Equals(""))
            {
                columnName = attribute.Name;
            }
            if (!attribute.Group.Equals(""))
            {
                columnName += $"_{attribute.Group}";
            }
            return columnName;
        }
        //Добавление Ячейки в строку (На вход подаем: строку, номер колонки, тип значения, стиль)
        static void InsertCell(Row row, int cell_num, string val, CellValues type, uint styleIndex)
        {
            Cell refCell = null;
            Cell newCell = new Cell() { CellReference = cell_num.ToString() + ":" + row.RowIndex.ToString(), StyleIndex = styleIndex };
            row.InsertBefore(newCell, refCell);

            // Устанавливает тип значения.
            newCell.CellValue = new CellValue(val);
            newCell.DataType = new EnumValue<CellValues>(type);

        }

        //Важный метод, при вставки текстовых значений надо использовать.
        //Метод убирает из строки запрещенные спец символы.
        //Если не использовать, то при наличии в строке таких символов, вылетит ошибка.
        private string ReplaceHexadecimalSymbols(string txt)
        {
            string r = "[\x00-\x08\x0B\x0C\x0E-\x1F\x26]";
            return Regex.Replace(txt, r, "", RegexOptions.Compiled);
        }

        //Метод генерирует стили для ячеек (за основу взят код, найденный где-то в интернете)
        static Stylesheet GenerateStyleSheet()
        {
            return new Stylesheet(
                new Fonts(
                    new Font(                                                               // Стиль под номером 0 - Шрифт по умолчанию.
                        new FontSize() { Val = 11 },
                        new Color() { Rgb = new HexBinaryValue() { Value = "000000" } },
                        new FontName() { Val = "Calibri" }),
                    new Font(                                                               // Стиль под номером 1 - Жирный шрифт Times New Roman.
                        new Bold(),
                        new FontSize() { Val = 11 },
                        new Color() { Rgb = new HexBinaryValue() { Value = "000000" } },
                        new FontName() { Val = "Times New Roman" }),
                    new Font(                                                               // Стиль под номером 2 - Обычный шрифт Times New Roman.
                        new FontSize() { Val = 11 },
                        new Color() { Rgb = new HexBinaryValue() { Value = "000000" } },
                        new FontName() { Val = "Times New Roman" }),
                    new Font(                                                               // Стиль под номером 3 - Шрифт Times New Roman размером 14.
                        new FontSize() { Val = 14 },
                        new Color() { Rgb = new HexBinaryValue() { Value = "000000" } },
                        new FontName() { Val = "Times New Roman" })
                ),
                new Fills(
                    new Fill(                                                           // Стиль под номером 0 - Заполнение ячейки по умолчанию.
                        new PatternFill() { PatternType = PatternValues.None }),
                    new Fill(                                                           // Стиль под номером 1 - Заполнение ячейки серым цветом
                        new PatternFill(
                            new ForegroundColor() { Rgb = new HexBinaryValue() { Value = "FFAAAAAA" } }
                            )
                        { PatternType = PatternValues.Solid }),
                    new Fill(                                                           // Стиль под номером 2 - Заполнение ячейки красным.
                        new PatternFill(
                            new ForegroundColor() { Rgb = new HexBinaryValue() { Value = "FFFFAAAA" } }
                        )
                        { PatternType = PatternValues.Solid })
                )
                ,
                new Borders(
                    new Border(                                                         // Стиль под номером 0 - Грани.
                        new LeftBorder(),
                        new RightBorder(),
                        new TopBorder(),
                        new BottomBorder(),
                        new DiagonalBorder()),
                    new Border(                                                         // Стиль под номером 1 - Грани
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
                        new DiagonalBorder()),
                    new Border(                                                         // Стиль под номером 2 - Грани.
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
                new CellFormats(
                    new CellFormat() { FontId = 0, FillId = 0, BorderId = 0 },                          // Стиль под номером 0 - The default cell style.  (по умолчанию)
                    new CellFormat(new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center, WrapText = true }) { FontId = 1, FillId = 2, BorderId = 1, ApplyFont = true },       // Стиль под номером 1 - Bold 
                    new CellFormat(new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center, WrapText = true }) { FontId = 2, FillId = 0, BorderId = 2, ApplyFont = true },       // Стиль под номером 2 - REgular
                    new CellFormat() { FontId = 3, FillId = 0, BorderId = 2, ApplyFont = true, NumberFormatId = 4 },       // Стиль под номером 3 - Times Roman
                    new CellFormat() { FontId = 0, FillId = 2, BorderId = 0, ApplyFill = true },       // Стиль под номером 4 - Yellow Fill
                    new CellFormat(                                                                   // Стиль под номером 5 - Alignment
                        new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center }
                    )
                    { FontId = 0, FillId = 0, BorderId = 0, ApplyAlignment = true },
                    new CellFormat() { FontId = 0, FillId = 0, BorderId = 1, ApplyBorder = true },      // Стиль под номером 6 - Border
                    new CellFormat(new Alignment() { Horizontal = HorizontalAlignmentValues.Right, Vertical = VerticalAlignmentValues.Center, WrapText = true }) { FontId = 2, FillId = 0, BorderId = 2, ApplyFont = true, NumberFormatId = 4 }       // Стиль под номером 7 - Задает числовой формат полю.
                )
            ); // Выход
        }
    }
}
