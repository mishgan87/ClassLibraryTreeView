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
        public event EventHandler<ExportProgressEventArgs> GetProgress;
        public event EventHandler<ExportProgressEventArgs> ExportDone;
        private ConceptualModel model;
        private string fileName;
        public ExcelExporter(string nameOfFile, ConceptualModel modelRef)
        {
            fileName = nameOfFile;
            model = modelRef;
        }
        // public async Task ExportPermissibleGrid()
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

                Sheets sheets = workbookPart.Workbook.AppendChild(new Sheets());
                Sheet sheet = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = $"New List" };
                sheets.Append(sheet);

                AddColumns(worksheetPart);

                workbookPart.Workbook.Save();
                document.Close();
            }
        }
        // Добавление и настройка солбцов
        private void AddColumns(WorksheetPart worksheetPart)
        {
            SheetData sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();
            Columns list = worksheetPart.Worksheet.GetFirstChild<Columns>();
            if (list == null)
            {
                list = new Columns();
            }

            int maxDepth = model.MaxDepth;
            Row firstRow = new Row() { RowIndex = 1 };

            int index = 0;
            for (index = 0; index < maxDepth; index++)
            {
                list.Append(new Column() { Min = 50, Max = 50, Width = 50, CustomWidth = true });
                InsertCell(firstRow, index, "", CellValues.String, 6);
            }

            index = 0;
            List<IAttribute> attributes = new List<IAttribute>();
            foreach (Dictionary<string, IAttribute> group in model.attributes.Values)
            {
                foreach (IAttribute attribute in group.Values)
                {
                    list.Append(new Column() { Min = 5, Max = 5, Width = 5, CustomWidth = true });
                    string name = attribute.Name;
                    if (name.Equals(""))
                    {
                        name = attribute.Id;
                    }
                    attributes.Add(attribute);
                    InsertCell(firstRow, index + maxDepth, name, CellValues.String, 5);
                    index++;
                }
            }

            worksheetPart.Worksheet.InsertAt(list, 0);

            sheetData.Append(firstRow);

            // Заполняем строки именами классов и значениями допустимых атрибутов

            // rowIndex = await AddClass(sheetData, rowIndex, maxDepth, attributes, model.functionals);
            // rowIndex = await AddClass(sheetData, rowIndex, maxDepth, attributes, model.physicals);
            // rowIndex = AddClass(sheetData, rowIndex, maxDepth, attributes.ToArray(), model.functionals);
            AddMerged(sheetData, attributes.ToArray(), model);
        }
        private void AddClassPresence(IClass cmClass, int maxDepth, SheetData sheetData, IAttribute[] attributes)
        {
            Row row = new Row() { RowIndex = (uint)(sheetData.Count() + 1), Height = 5 };
            if (cmClass.Children.Count > 0)
            {
                row = new Row() { RowIndex = (uint)(sheetData.Count() + 1), Height = 5, Collapsed = true };
            }
            sheetData.Append(row);

            int depthCurrent = model.ClassDepth(cmClass);
            for (int depth = 1; depth <= maxDepth; depth++)
            {
                string text = "";
                if (depth == depthCurrent)
                {
                    text = $"{cmClass.Name}";
                }
                InsertCell(row, depth, text, CellValues.String, 6);
            }

            for (int columnIndex = 0; columnIndex < attributes.Length; columnIndex++)
            {
                string presence = model.Presence(cmClass, attributes[columnIndex]);
                InsertCell(row, columnIndex + maxDepth, presence, CellValues.String, 7);
            }
        }
        private void AddChildren(IClass cmClass, int maxDepth, SheetData sheetData, IAttribute[] attributes)
        {
            if (cmClass.Children.Count > 0)
            {
                foreach (IClass child in cmClass.Children)
                {
                    AddClassPresence(child, maxDepth, sheetData, attributes);
                    AddChildren(child, maxDepth, sheetData, attributes);
                }
            }
        }
        private void AddMerged(SheetData sheetData, IAttribute[] attributes, ConceptualModel model)
        {
            if (model.merged.Count > 0)
            {
                int maxDepth = model.MaxDepth;

                foreach (IClass cmClass in model.merged)
                {
                    if (cmClass.Extends.Equals(""))
                    {
                        AddClassPresence(cmClass, maxDepth, sheetData, attributes);
                        AddChildren(cmClass, maxDepth, sheetData, attributes);
                    }
                }
            }

            // ExportProgressEventArgs eventArgs = new ExportProgressEventArgs();
            // eventArgs.Progress = ((int)newRowIndex / model.merged.Count) * 100;
            // GetProgress.Invoke(this, eventArgs);
        }
        private uint WriteClass(SheetData sheetData, uint rowIndex, int maxDepth, IClass cmClass, Dictionary<string, IClass> map, IAttribute[] attributes)
        {
            ExportProgressEventArgs eventArgs = new ExportProgressEventArgs();
            eventArgs.Done = false;

            uint newRowIndex = rowIndex + 1;
            Row row = new Row() { RowIndex = newRowIndex };
            sheetData.Append(row);

            int depthCurrent = model.ClassDepth(cmClass);
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
                string presence = model.Presence(cmClass, attributes[columnIndex]);
                InsertCell(row, columnIndex + maxDepth, presence, CellValues.String, 0);

                // eventArgs.Progress = (attributes.Length / columnIndex) * 100;
                // GetProgress.Invoke(this, eventArgs);
            }

            return newRowIndex;
        }
        private uint AddClass(SheetData sheetData, uint rowIndex, int maxDepth, IAttribute[] attributes, Dictionary<string, IClass> map)
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
        private uint AddChildren(SheetData sheetData, uint rowIndex, int maxDepth, IAttribute[] attributes, IClass cmClass, Dictionary<string, IClass> map)
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
        ///Добавление Ячейки в строку (На вход подаем: строку, номер колонки, тип значения, стиль)
        static void InsertCell(Row row, int cell_num, string val, CellValues type, uint styleIndex)
        {
            Cell refCell = null;
            Cell newCell = new Cell() { CellReference = cell_num.ToString() + ":" + row.RowIndex.ToString(), StyleIndex = styleIndex };
            row.InsertBefore(newCell, refCell);

            // Устанавливает тип значения.
            newCell.CellValue = new CellValue(val);
            newCell.DataType = new EnumValue<CellValues>(type);

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
                    { FontId = 2, FillId = 0, BorderId = 2, ApplyFont = true, NumberFormatId = 4 }
                )
            );
        }
    }
}
