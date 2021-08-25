using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace ClassLibraryTreeView
{
    public class ConceptualModel
    {
        public Dictionary<string, CMClass> functionals;
        public Dictionary<string, CMClass> physicals;
        public Dictionary<string, CMClass> documents;
        public Dictionary<string, CMAttribute> attributes;

        public ConceptualModel()
        {
            Init();
        }
        public void Init()
        {
            functionals = new Dictionary<string, CMClass>();
            physicals = new Dictionary<string, CMClass>();
            documents = new Dictionary<string, CMClass>();
            attributes = new Dictionary<string, CMAttribute>();
        }
        public void Clear()
        {
            functionals.Clear();
            physicals.Clear();
            documents.Clear();
            attributes.Clear();
        }
        public void ImportXml(string fileName)
        {
            Clear();
            XDocument doc = XDocument.Load(fileName);
            foreach(XElement element in doc.Elements().First().Elements())
            {
                string name = element.Name.LocalName.ToLower();
                if (name.Equals("functionals"))
                {
                    functionals = CMClass.FillClassMap(element);
                }
                if (name.Equals("physicals"))
                {
                    physicals = CMClass.FillClassMap(element);
                }
                if (name.Equals("documents"))
                {
                    documents = CMClass.FillClassMap(element);
                }
                if (name.Equals("attributes"))
                {
                    attributes = CMAttribute.FillAttributesMap(element);
                }
            }
            // List<CMClass> classes = MergeClasses();

            List<CMClass> list = new List<CMClass>();
            foreach (CMClass cmClass in functionals.Values)
            {
                MergeRecursive(cmClass, list);
            }
            foreach (CMClass cmClass in physicals.Values)
            {
                MergeRecursive(cmClass, list);
            }

            functionals.Clear();
            functionals = CMClass.FillClassMap(list);

            // functionals.First().Value.Merge(physicals.First().Value);
        }
        private string ColumnName(CMAttribute attribute)
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
        private CMClass GetElement(CMClass source, string id)
        {
            CMClass cmClass = null;
            if(source.Id.Equals(id))
            {
                return source;
            }
            if(source.Descendants.Count == 0)
            {
                return cmClass;
            }
            foreach(CMClass descendant in source.Descendants.Values)
            {
                CMClass cmClassDescendant = GetElement(descendant, id);
                if(cmClassDescendant != null)
                {
                    return cmClassDescendant;
                }
            }
            return cmClass;
        }
        private CMClass GetElement(Dictionary<string, CMClass> source, string id)
        {
            CMClass cmClass = null;
            foreach(CMClass value in source.Values)
            {
                cmClass = GetElement(value, id);
                if (cmClass != null)
                {
                    return cmClass;
                }
            }
            return cmClass;
        }
        private int MaxDepth(Dictionary<string, CMClass> source)
        {
            int maxDepth = 1;
            foreach (CMClass cmClass in source.Values)
            {
                int depth = 1; // PrintNonRecursive(cmClass, maxDepth);
                if (depth > maxDepth)
                {
                    maxDepth = depth;
                }
            }
            return maxDepth;
        }

        private int ClassDepth(CMClass cmClass, Dictionary<string, CMClass> source)
        {
            int depth = 1;
            if (cmClass.ParentId == null)
            {
                return depth;
            }
            CMClass parent = GetElement(source, cmClass.ParentId);
            while (parent != null)
            {
                depth++;
                parent = GetElement(source, parent.ParentId);
            }
            return depth;
        }
        private uint InsertClass(CMClass cmClass, SheetData sheetData, uint beginIndex)
        {
            CMAttribute[] attributesArray = attributes.Values.ToArray();
            uint index = beginIndex;
            Row row = new Row() { RowIndex = index };
            sheetData.Append(row);

            InsertCell(row, 1, $"{cmClass.Name}", CellValues.String, 2);

            for (int columnIndex = 0; columnIndex < attributesArray.Length; columnIndex++)
            {
                CMAttribute attribute = attributesArray[columnIndex];
                string presence = "X";// FindAttribute(attribute, element);
                InsertCell(row, columnIndex + 2, presence, CellValues.String, 0);
            }

            index++;

            if (cmClass.Descendants.Count == 0)
            {
                return index;
            }

            foreach (CMClass descendant in cmClass.Descendants.Values)
            {
                index = InsertClass(descendant, sheetData, index);
            }

            return index;
        }
        private uint InsertClasses(Dictionary<string, CMClass> source, SheetData sheetData, uint beginIndex)
        {
            uint index = beginIndex;
            foreach (CMClass cmClass in source.Values)
            {
                index = InsertClass(cmClass, sheetData, index);
            }
            return index;
        }
        private CMClass ContainsClass(CMClass cmClass, List<CMClass> classes)
        {
            foreach(CMClass value in classes)
            {
                if (value.Name.Equals(cmClass.Name))
                {
                    return value;
                }
            }
            return null;
        }
        private void MergeRecursive(CMClass cmClass, List<CMClass> classes)
        {
            classes.Add(cmClass);
            if (cmClass.Descendants.Count > 0)
            {
                foreach (CMClass descendant in cmClass.Descendants.Values)
                {
                    MergeRecursive(descendant, classes);
                }
            }
        }
        private List<CMClass> MergeClasses(Dictionary<string, CMClass> map)
        {
            List<CMClass> classes = new List<CMClass>();
            foreach(CMClass cmClass in map.Values)
            {
                MergeRecursive(cmClass, classes);
            }
            return classes;
        }
        public int ExportPermissibleGrid(string fileName)
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
                // Вычисляем максимальную вложенность
                /*
                int maxDepth = MaxDepth(functionals);
                int maxDepthPhysicals = MaxDepth(physicals);
                if(maxDepthPhysicals > maxDepth)
                {
                    maxDepth = maxDepthPhysicals;
                }
                */
                int maxDepth = 1;
                CMAttribute[] attributesArray = attributes.Values.ToArray();
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
                uint index = 2;

                index = InsertClasses(functionals, sheetData, index);
                index = InsertClasses(physicals, sheetData, index);

                workbookPart.Workbook.Save();
                document.Close();
            }
            return 0;
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
        static string ReplaceHexadecimalSymbols(string txt)
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
