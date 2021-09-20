﻿using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using ClassLibraryTreeView.Interfaces;
using ClassLibraryTreeView.Classes;
using System;
using System.Collections.Concurrent;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Threading.Tasks;
using DocumentFormat.OpenXml;
using System.Data;
using ClosedXML.Excel;
using System.Threading;

namespace ClassLibraryTreeView
{
    public enum CellStyle
    {
        Default = 0,
        Empty = 1,
        
        Class = 2,
        ClassId = 3,
        
        Attribute = 4,
        AttributesGroup = 5,

        Discipline = 6,

        Header = 7,

        PresenceUnselect = 8,
        PresenceNonApplicable = 9,
        PresenceOptional = 10,
        PresencePreffered = 11,
        PresenceRequired = 12
    }
    public enum GridCellMergeProperty
    {
        NoMerging = 0,
        MergingStart = 1,
        MergingFinish = 2
    }
    public class ConceptualModel
    {
/*
        public Dictionary<string, IClass> documents = new Dictionary<string, IClass>();
        public Dictionary<string, IClass> functionals = new Dictionary<string, IClass>();
        public Dictionary<string, IClass> physicals = new Dictionary<string, IClass>();
*/
        public Dictionary<string, Taxonomy> taxonomies = new Dictionary<string, Taxonomy>();
        public Dictionary<string, MeasureClass> measureClasses = new Dictionary<string, MeasureClass>();
        public Dictionary<string, MeasureUnit> measureUnits = new Dictionary<string, MeasureUnit>();
        public Dictionary<string, EnumerationList> enumerations = new Dictionary<string, EnumerationList>();

        public Dictionary<string, Dictionary<string, IClass>> classes = new Dictionary<string, Dictionary<string, IClass>>();
        public Dictionary<string, Dictionary<string, IAttribute>> attributes = new Dictionary<string, Dictionary<string, IAttribute>>();

        public int AttributesCount = 0;
        public int maxDepth = 0;

        private void CalculateMaxDepth()
        {
            maxDepth = 0;
            foreach(Dictionary<string, IClass> map in classes.Values)
            {
                foreach (IClass cmClass in map.Values)
                {
                    int depth = ClassDepth(cmClass);
                    if (depth > maxDepth)
                    {
                        maxDepth = depth;
                    }
                }
            }
        }

        public ConceptualModel()
        {
            Init();
        }
        public void Init()
        {
            attributes = new Dictionary<string, Dictionary<string, IAttribute>>();
            classes = new Dictionary<string, Dictionary<string, IClass>>();
            taxonomies = new Dictionary<string, Taxonomy>();
            measureClasses = new Dictionary<string, MeasureClass>();
            measureUnits = new Dictionary<string, MeasureUnit>();
            enumerations = new Dictionary<string, EnumerationList>();

            AttributesCount = 0;
        }
        public void Clear()
        {
            attributes.Clear();
            AttributesCount = 0;
            classes.Clear();
            taxonomies.Clear();
            enumerations.Clear();
            measureUnits.Clear();
            measureClasses.Clear();
        }
        public void DefinePermissibleAttributes(IClass cmClass)
        {
            string xtype = cmClass.Xtype.ToLower();
            Dictionary<string, IClass> map = classes[xtype];
            if (map == null)
            {
                return;
            }
            Dictionary<string, IAttribute> result = new Dictionary<string, IAttribute>();
            foreach (IAttribute attribute in cmClass.PermissibleAttributes)
            {
                if (!result.ContainsKey(attribute.Id))
                {
                    IAttribute newAttribute = new IAttribute(attribute);
                    result.Add(attribute.Id, new IAttribute(newAttribute));
                }
            }

            string parent = cmClass.Extends;
            while (!parent.Equals(""))
            {
                foreach (IAttribute attribute in map[parent].PermissibleAttributes)
                {
                    if (!result.ContainsKey(attribute.Id))
                    {
                        IAttribute newAttribute = new IAttribute(attribute);
                        newAttribute.Presence = "";
                        result.Add(attribute.Id, new IAttribute(newAttribute));
                    }
                }
                parent = map[parent].Extends;
            }

            cmClass.PermissibleAttributesMap = new Dictionary<string, IAttribute>(result);
        }
        public Dictionary<string, IAttribute> GetPermissibleAttributes(IClass cmClass)
        {
            string xtype = cmClass.Xtype.ToLower();
            Dictionary<string, IClass> map = classes[xtype];
            if (map == null)
            {
                return null;
            }
            Dictionary<string, IAttribute> result = new Dictionary<string, IAttribute>();
            foreach(IAttribute attribute in cmClass.PermissibleAttributes)
            {
                if (!result.ContainsKey(attribute.Id))
                {
                    IAttribute newAttribute = new IAttribute(attribute);
                    newAttribute.Presence = "";
                    result.Add(attribute.Id, new IAttribute(newAttribute));
                }
            }

            string parent = cmClass.Extends;
            while (!parent.Equals(""))
            {
                foreach (IAttribute attribute in map[parent].PermissibleAttributes)
                {
                    if (!result.ContainsKey(attribute.Id))
                    {
                        IAttribute newAttribute = new IAttribute(attribute);
                        newAttribute.Presence = "";
                        result.Add(attribute.Id, new IAttribute(newAttribute));
                    }
                }
                parent = map[parent].Extends;
            }

            return result;
        }
        public List<IAttribute> PermissibleAttributes(IClass cmClass)
        {
            Dictionary<string, IClass> map = null;
            string xtype = cmClass.Xtype.ToLower();
            map = classes[xtype];
            if (map == null)
            {
                return null;
            }
            List<IAttribute> result = new List<IAttribute>();
            result.AddRange(cmClass.PermissibleAttributes);

            string parent = cmClass.Extends;
            while (!parent.Equals(""))
            {
                foreach (IAttribute parentAttribute in map[parent].PermissibleAttributes)
                {
                    IAttribute attribute = new IAttribute(parentAttribute);
                    attribute.Presence = "";
                    if (!result.Contains(attribute))
                    {
                        result.Add(attribute);
                    }
                }
                parent = map[parent].Extends;
            }

            return result;
        }
        public string AttributePresence(IClass cmClass, string id)
        {
            if (cmClass.PermissibleAttributesMap.ContainsKey(id))
            {
                if (!cmClass.PermissibleAttributesMap[id].Presence.Equals(""))
                {
                    return cmClass.PermissibleAttributesMap[id].Presence.Substring(0, 1);
                }
                return "X";
            }

            return "";
        }
        public int ClassDepth(IClass cmClass)
        {
            Dictionary<string, IClass> map = classes[cmClass.Xtype.ToLower()];
            if (map.Count == 0)
            {
                return 0;
            }
            int depth = 0;
            string parent = cmClass.Extends;
            while (!parent.Equals(""))
            {
                depth++;
                parent = map[parent].Extends;
            }
            return depth;
        }
        public int MapMaxDepth(Dictionary<string, IClass> map)
        {
            if (map.Count == 0)
            {
                return 0;
            }
            int maxDepth = 0;
            foreach (IClass cmClass in map.Values)
            {
                int depth = ClassDepth(cmClass);
                if (depth > maxDepth)
                {
                    maxDepth = depth;
                }
            }
            return maxDepth;
        }
        private void MapFromXElement(XElement element, Dictionary<string, IClass> map)
        {
            foreach (XElement child in element.Elements())
            {
                if (!child.Name.LocalName.ToLower().Equals("extension"))
                {
                    IClass newClass = new IClass(child);
                    map.Add(newClass.Id, newClass);
                }
            }
        }
        private void GetUoM(XElement referenceDataElement)
        {
            foreach (XElement element in referenceDataElement.Elements())
            {
                string name = element.Name.LocalName.ToLower();

                if (name.Equals("units"))
                {
                    foreach (XElement child in element.Elements())
                    {
                        MeasureUnit measureUnit = new MeasureUnit(child);
                        measureUnits.Add(measureUnit.Id, measureUnit);
                    }
                }

                if (name.Equals("measureclasses"))
                {
                    foreach (XElement child in element.Elements())
                    {
                        MeasureClass measureClass = new MeasureClass(child);
                        measureClasses.Add(measureClass.Id, measureClass);
                    }
                }
            }
        }
        private void GetReferenceData(XElement referenceDataElement)
        {
            foreach (XElement element in referenceDataElement.Elements())
            {
                string name = element.Name.LocalName;
                if (name.ToLower().Equals("enumerations"))
                {
                    foreach (XElement child in element.Elements())
                    {
                        EnumerationList enumerationList = new EnumerationList(child);
                        enumerations.Add(enumerationList.Id, enumerationList);
                    }
                }

                if (name.ToLower().Equals("uom"))
                {
                    GetUoM(element);
                }
                if (name.ToLower().Equals("taxonomies"))
                {
                    foreach (XElement child in element.Elements())
                    {
                        Taxonomy taxonomy = new Taxonomy(child);
                        taxonomies.Add(taxonomy.Id, taxonomy);
                    }
                }
            }
        }
        public void ImportXml(string fileName)
        {
            Clear();
            XDocument doc = XDocument.Load(fileName);

            foreach (XElement element in doc.Elements().First().Elements())
            {
                string name = element.Name.LocalName.ToLower();
                
                if (name.Equals("referencedata"))
                {
                    GetReferenceData(element);
                }

                if (name.Equals("functionals") || name.Equals("physicals") || name.Equals("documents"))
                {
                    classes.Add(name, new Dictionary<string, IClass>());
                    MapFromXElement(element, classes[name]);
                }

                if (name.Equals("attributes"))
                {
                    foreach (XElement child in element.Elements())
                    {
                        IAttribute newAttribute = new IAttribute(child);

                        string group = newAttribute.Group;

                        if (group.Equals(""))
                        {
                            group = "Unset";
                        }

                        if (!attributes.ContainsKey(group))
                        {
                            attributes.Add(group, new Dictionary<string, IAttribute>());
                        }

                        attributes[group].Add(newAttribute.Id, newAttribute);
                        AttributesCount++;
                    }
                }
            }

            foreach (Dictionary<string, IClass> map in classes.Values) // set classes inheritance
            {
                if (map.Count > 0)
                {
                    foreach (IClass cmClass in map.Values)
                    {
                        if (!cmClass.Extends.Equals(""))
                        {
                            map[cmClass.Extends].Children.Add(cmClass.Id, cmClass);
                        }
                    }
                }
            }

            MergeByName(); // MergeByAssociations();

            CalculateMaxDepth();
        }
        public IAttribute GetAttribute(string id)
        {
            foreach (string group in attributes.Keys)
            {
                if (attributes[group].ContainsKey(id))
                {
                    return attributes[group][id];
                }
            }
            return null;
        }
        public string GetAttributeId(int number)
        {
            int col = 0;
            foreach (string group in attributes.Keys)
            {
                foreach (string id in attributes[group].Keys)
                {
                    if (col == number)
                    {
                        return id;
                    }
                    col++;
                }
            }
            return null;
        }
        public IAttribute GetAttribute(int number)
        {
            int col = 0;
            foreach(string group in attributes.Keys)
            {
                foreach(string id in attributes[group].Keys)
                {
                    if (col == number)
                    {
                        return attributes[group][id];
                    }
                    col++;
                }
            }
            return null;
        }
        public IClass GetClass(string id, string xtype)
        {
            return classes[xtype][id];
        }
        public static string[] SplitValidationRules(string validationRule)
        {
            List<string> rules = new List<string>();

            string rule = validationRule;
            rule = rule.Remove(rule.IndexOf("::"), rule.Length - rule.IndexOf("::"));
            rules.Add(rule);

            string concept = validationRule;
            concept = concept.Remove(0, concept.IndexOf("::") + 2);
            concept = concept.Remove(concept.IndexOf("::"), concept.Length - concept.IndexOf("::"));
            rules.Add(concept);

            string ids = validationRule;
            ids = ids.Remove(0, ids.LastIndexOf("::") + 2);

            while (ids.Length > 0)
            {
                string id = ids;
                if (id.Contains("||"))
                {
                    id = id.Remove(id.IndexOf("||"), id.Length - id.IndexOf("||"));

                    ids = ids.Remove(0, ids.IndexOf("||") + 2);
                }
                else
                {
                    ids = ids.Remove(0, ids.Length);
                }

                rules.Add(id);
            }

            return rules.ToArray();
        }
        private void MergeByAssociations()
        {
            if (classes.ContainsKey("merged"))
            {
                classes.Remove("merged");
            }
            classes.Add("merged", new Dictionary<string, IClass>());
            Dictionary<string, IClass> merged = classes["merged"];
            foreach (IClass cmClass in classes["functionals"].Values)
            {
                if (!cmClass.Extends.Equals(""))
                {
                    // continue;
                }

                if (!merged.Values.Contains(cmClass))
                {
                    merged.Add(cmClass.Id, cmClass);
                }

                List<IAttribute> permissibleAttributes = PermissibleAttributes(cmClass);
                foreach (IAttribute attribute in permissibleAttributes)
                {
                    if (attribute.ValidationType.ToLower().Equals("association"))
                    {
                        string[] rules = SplitValidationRules(attribute.ValidationRule);
                        string concept = rules[1];

                        for (int index = 2; index < rules.Length; index++)
                        {
                            if (concept.ToLower().Equals("functional"))
                            {
                                merged.Add(classes["functionals"][rules[index]].Id, classes["functionals"][rules[index]]);
                            }
                            if (concept.ToLower().Equals("physical"))
                            {
                                merged.Add(classes["physicals"][rules[index]].Id, classes["physicals"][rules[index]]);
                            }
                        }
                    }
                }
            }
        }
        private void AddChildren(IClass cmClass, Dictionary<string, IClass> map)
        {
            if (cmClass.Children.Count > 0)
            {
                foreach (IClass child in cmClass.Children.Values)
                {
                    map.Add(child.Id, child);
                    AddChildren(child, map);
                    DefinePermissibleAttributes(child);
                }
            }
        }
        private void MergeByName()
        {
            if (!classes.ContainsKey("merged"))
            {
                classes.Add("merged", new Dictionary<string, IClass>());
            }

            Dictionary<string, IClass> merged = classes["merged"];

            foreach (IClass cmClass in classes["functionals"].Values)
            {
                if (cmClass.Extends.Equals(""))
                {
                    merged.Add(cmClass.Id, cmClass);
                    AddChildren(cmClass, merged);
                    DefinePermissibleAttributes(cmClass);
                }
            }

            if (!classes.ContainsKey("physicals"))
            {
                return;
            }

            foreach (IClass fClass in merged.Values)
            {
                foreach (IClass pClass in classes["physicals"].Values)
                {
                    if (pClass.Name.Equals(fClass.Name))
                    {
                        foreach (IClass child in pClass.Children.Values)
                        {
                            IClass cmClass = fClass.ContainsChildByName(child);

                            if (cmClass == null)
                            {
                                fClass.Children.Add(child.Id, child);
                            }
                        }
                    }
                }
                DefinePermissibleAttributes(fClass);
            }
        }
        public void AddClassChildren(IClass cmClass, Dictionary<string, IClass> map)
        {
            foreach (IClass child in cmClass.Children.Values)
            {
                if (child != null && !map.ContainsValue(child))
                {
                    // child.PermissibleAttributes = PermissibleAttributes(cmClass);
                    DefinePermissibleAttributes(child);
                    map.Add(child.Id, child);
                    AddClassChildren(child, map);
                }
            }
        }
        private string CellName(int row, int col)
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
        static Cell AddCell(Row row, int columnIndex, string text, uint styleIndex)
        {
            Cell refCell = null;
            Cell newCell = new Cell()
            {
                // CellReference = $"{CellName((int)row.RowIndex, columnIndex)}",
                StyleIndex = styleIndex
            };
            row.InsertBefore(newCell, refCell);

            newCell.CellValue = new CellValue(text);
            newCell.DataType = new EnumValue<CellValues>(CellValues.String);

            return newCell;
        }
        private void WriteClass(IXLWorksheet worksheet, KeyValuePair<int, IClass> classRow, Queue<string> mergedRanges)
        {
            int row = classRow.Key;
            IClass cmClass = classRow.Value;
            int classDepth = ClassDepth(cmClass);
            int count = maxDepth + AttributesCount + 2;

            SetCell(worksheet.Cell(CellName(row, maxDepth + 2)), CellStyle.ClassId, cmClass.Id); // set class id cell
            SetCell(worksheet.Cell(CellName(row, classDepth)), CellStyle.Class, cmClass.Name); // set class name cell
            SetCell(worksheet.Cell(CellName(row, maxDepth + 1)), CellStyle.Discipline, ""); // set class discipline cell

            if (maxDepth != classDepth) // merge subclass cells
            {
                mergedRanges.Enqueue($"{CellName(row, classDepth)}:{CellName(row, maxDepth)}");
            }

            for (int col = maxDepth + 3; col < count; col++) // set class attributes presence cells
            {
                string presence = AttributePresence(cmClass, GetAttributeId(col - maxDepth - 2));
                switch (presence)
                {
                    case "":
                        SetCell(worksheet.Cell(CellName(row, col)), CellStyle.PresenceNonApplicable, presence);
                        break;
                    case "X":
                        SetCell(worksheet.Cell(CellName(row, col)), CellStyle.PresenceUnselect, presence);
                        break;
                    case "O":
                        SetCell(worksheet.Cell(CellName(row, col)), CellStyle.PresenceOptional, presence);
                        break;
                    case "P":
                        SetCell(worksheet.Cell(CellName(row, col)), CellStyle.PresencePreffered, presence);
                        break;
                    case "R":
                        SetCell(worksheet.Cell(CellName(row, col)), CellStyle.PresenceRequired, presence);
                        break;
                    default:
                        SetCell(worksheet.Cell(CellName(row, col)), CellStyle.Default, presence);
                        break;
                }
            }
        }
        private void SetCell(IXLCell cell, CellStyle style, string value)
        {
            cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            cell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            cell.Value = value;

            switch (style)
            {
                case CellStyle.Class:
                    cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                    break;

                case CellStyle.ClassId:
                    cell.Style.Fill.BackgroundColor = XLColor.Green;
                    break;

                case CellStyle.Attribute:
                    cell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Bottom;
                    cell.Style.Alignment.TextRotation = 90;
                    cell.Style.Fill.BackgroundColor = XLColor.Blue;
                    cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    cell.Style.Border.BottomBorderColor = XLColor.White;
                    cell.Style.Border.TopBorderColor = XLColor.White;
                    cell.Style.Border.LeftBorderColor = XLColor.White;
                    cell.Style.Border.RightBorderColor = XLColor.White;
                    cell.Style.Font.FontColor = XLColor.White;
                    break;

                case CellStyle.AttributesGroup:
                    cell.Style.Fill.BackgroundColor = XLColor.DarkBlue;
                    cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    cell.Style.Border.BottomBorderColor = XLColor.White;
                    cell.Style.Border.TopBorderColor = XLColor.White;
                    cell.Style.Border.LeftBorderColor = XLColor.White;
                    cell.Style.Border.RightBorderColor = XLColor.White;
                    cell.Style.Font.FontColor = XLColor.White;
                    break;

                case CellStyle.Discipline:
                    cell.Style.Fill.BackgroundColor = XLColor.Yellow;
                    break;

                case CellStyle.Header:
                    cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                    cell.Style.Fill.BackgroundColor = XLColor.Blue;
                    cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    cell.Style.Border.BottomBorderColor = XLColor.White;
                    cell.Style.Border.TopBorderColor = XLColor.White;
                    cell.Style.Border.LeftBorderColor = XLColor.White;
                    cell.Style.Border.RightBorderColor = XLColor.White;
                    cell.Style.Font.FontColor = XLColor.White;
                    break;

                case CellStyle.PresenceUnselect:
                    cell.Style.Fill.BackgroundColor = XLColor.AntiFlashWhite;
                    break;

                case CellStyle.PresenceNonApplicable:
                    break;

                case CellStyle.PresenceOptional:
                    cell.Style.Fill.BackgroundColor = XLColor.AppleGreen;
                    break;

                case CellStyle.PresencePreffered:
                    cell.Style.Fill.BackgroundColor = XLColor.BrightGreen;
                    break;

                case CellStyle.PresenceRequired:
                    cell.Style.Fill.BackgroundColor = XLColor.DarkGreen;
                    cell.Style.Font.FontColor = XLColor.White;
                    break;

                default:
                    break;
            }
        }
        public void ExportPermissibleGrid(string filename)
        {
            using (XLWorkbook workbook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workbook.Worksheets.Add($"Permissible Grid");

                int count = maxDepth + AttributesCount + 2;

                // define classes attributes rows

                int row = 3;
                ConcurrentDictionary<int, IClass> classRows = new ConcurrentDictionary<int, IClass>(); // dictionary of pairs "row number - conceptual model class"
                foreach (IClass cmClass in classes["merged"].Values) // fill dictionary of pairs "row number - conceptual model class"
                {
                    if (classRows.TryAdd(row, cmClass))
                    {
                        row++;
                    }
                }

                // write header

                SetCell(worksheet.Cell(CellName(2, 0)), CellStyle.Header, $"Classes ({classes["merged"].Count})"); // set classes count header cell
                SetCell(worksheet.Cell(CellName(2, maxDepth + 1)), CellStyle.Header, $"Discipline"); // set class discipline header cell
                SetCell(worksheet.Cell(CellName(2, maxDepth + 2)), CellStyle.Header, $"Class ID"); // set class id header cell

                Queue<string> mergedRanges = new Queue<string>();
                mergedRanges.Enqueue($"{CellName(0, 0)}:{CellName(1, maxDepth + 2)}");
                mergedRanges.Enqueue($"{CellName(2, 0)}:{CellName(2, maxDepth)}");

                string mergedCell = "";
                int col = maxDepth + 3;
                foreach (string group in attributes.Keys)
                {
                    mergedCell = $"{CellName(0, col)}";
                    IXLCell cell = worksheet.Cell($"{CellName(0, col)}");
                    SetCell(cell, CellStyle.AttributesGroup, group);
                    foreach (IAttribute attribute in attributes[group].Values)
                    {
                        cell = worksheet.Cell($"{CellName(1, col)}");
                        SetCell(cell, CellStyle.Attribute, $"{attribute.Id} : {attribute.Name}");
                        col++;
                    }
                    mergedRanges.Enqueue($"{mergedCell}:{CellName(0, col - 1)}");
                }

                Parallel.ForEach // write permissible grid
                (
                    classRows,
                    new ParallelOptions()
                    {
                        MaxDegreeOfParallelism = 2
                    },
                    classRow =>
                    {
                        WriteClass(worksheet, classRow, mergedRanges);
                    }
                );

                foreach (string range in mergedRanges) // merging selected cells
                {
                    if (range != null)
                    {
                        worksheet.Range(range).Merge();
                    }
                }

                for (col = 1; col <= worksheet.ColumnCount(); col++) // adjust columns width
                {
                    worksheet.Column(col).AdjustToContents();
                }
                
                workbook.SaveAs(filename);
            }
        }
    }
}
