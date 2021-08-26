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
                CMAttribute[] attributesArray = model.attributes.ToArray();
                for (int columnIndex = 0; columnIndex < attributesArray.Length + maxDepth; columnIndex++)
                {
                    lstColumns.Append(new Column() { Min = 1, Max = 10, Width = 5, CustomWidth = true });
                }
                worksheetPart.Worksheet.InsertAt(lstColumns, 0);

                Row rowFirst = new Row() { RowIndex = 1 };
                for (int columnIndex = 0; columnIndex < attributesArray.Length + maxDepth; columnIndex++)
                {
                    string columnName = "";
                    if (columnIndex >= maxDepth)
                    {
                        columnName = ColumnName(attributesArray[columnIndex - maxDepth]);
                    }
                    InsertCell(rowFirst, columnIndex, columnName, CellValues.String, 2);
                }
                sheetData.Append(rowFirst);

                // Заполняем строки именами классов и значениями допустимых атрибутов

                Dictionary<string, CMClass> func = model.func;
                Dictionary<string, CMClass> phys = model.phys;

                uint rowIndex = 2;
                rowIndex = AddClass(sheetData, rowIndex, maxDepth,attributesArray, func, phys);
                rowIndex = AddClass(sheetData, rowIndex, maxDepth, attributesArray, phys);

                workbookPart.Workbook.Save();
                document.Close();
            }
            return 0;
        }
        static uint WriteClass(SheetData sheetData, uint rowIndex, int maxDepth, CMClass cmClass, CMAttribute[] attributesArray)
        {
            uint newRowIndex = rowIndex + 1;
            Row row = new Row() { RowIndex = newRowIndex };
            sheetData.Append(row);

            int depthCurrent = cmClass.Depth;
            for (int depth = 1; depth <= maxDepth; depth++)
            {
                string text = "";
                if (depth == depthCurrent)
                {
                    text = $"{cmClass.Name}";
                }
                InsertCell(row, depth, text, CellValues.String, 1);
            }

            for (int columnIndex = 0; columnIndex < attributesArray.Length; columnIndex++)
            {
                CMAttribute attribute = attributesArray[columnIndex];
                string presence = cmClass.Presence(attribute.Id, "");
                InsertCell(row, columnIndex + maxDepth, presence, CellValues.String, 0);
            }

            return newRowIndex;
        }
        static CMClass FindSameInChildren(CMClass cmClass, CMClass source)
        {
            CMClass result = null;
            if (source.Descendants.Count > 0)
            {
                foreach (CMClass child in cmClass.Descendants)
                {
                    if (child.Name.Equals(cmClass.Name))
                    {
                        return child;
                    }
                }
            }
            return result;
        }
        static CMClass FindSameName(CMClass cmClass, Dictionary<string, CMClass> source)
        {
            CMClass result = null;
            foreach (CMClass value in source.Values)
            {
                if (!value.HasParent(source))
                {
                    if (value.Name.Equals(cmClass.Name))
                    {
                        return value;
                    }
                    result = FindSameInChildren(cmClass, value);
                }
            }
            return result;
        }
        static uint AddClass(SheetData sheetData, uint rowIndex, int maxDepth, CMAttribute[] attributesArray, Dictionary<string, CMClass> map, Dictionary<string, CMClass> source = null)
        {
            uint newRowIndex = rowIndex;
            if (map.Count > 0)
            {
                foreach (CMClass cmClass in map.Values)
                {
                    if (!cmClass.HasParent(map))
                    {
                        newRowIndex = WriteClass(sheetData, newRowIndex, maxDepth, cmClass, attributesArray);
                        newRowIndex = AddChildren(sheetData, newRowIndex, maxDepth, attributesArray, cmClass);
                        if (source != null)
                        {
                            CMClass sameClass = FindSameName(cmClass, source);
                            if (sameClass != null)
                            {
                                newRowIndex = AddChildren(sheetData, newRowIndex, maxDepth, attributesArray, sameClass, cmClass);
                            }
                        }
                    }
                }
            }
            return newRowIndex;
        }
        static uint AddChildren(SheetData sheetData, uint rowIndex, int maxDepth, CMAttribute[] attributesArray, CMClass cmClass, CMClass source = null)
        {
            uint newRowIndex = rowIndex;
            if (cmClass.Descendants.Count != 0)
            {
                foreach (CMClass child in cmClass.Descendants)
                {
                    if (source != null)
                    {
                        foreach (CMClass childSource in source.Descendants)
                        {
                            if (!childSource.Equals(child))
                            {
                                newRowIndex = WriteClass(sheetData, newRowIndex, maxDepth, child, attributesArray);
                            }
                        }
                    }
                    else
                    {
                        newRowIndex = WriteClass(sheetData, newRowIndex, maxDepth, child, attributesArray);
                        newRowIndex = AddChildren(sheetData, newRowIndex, maxDepth, attributesArray, child);
                    }
                }
            }
            return newRowIndex;
        }
        static string ColumnName(CMAttribute attribute)
        {
            string columnName = attribute.Attributes["id"];
            if (attribute.Attributes.ContainsKey("name"))
            {
                columnName = attribute.Attributes["name"];
            }
            if (attribute.Attributes.ContainsKey("groupId"))
            {
                columnName += $"_{attribute.Attributes["groupId"]}";
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
