using System.Linq;
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
        public Dictionary<string, IClass> merged = new Dictionary<string, IClass>();

        public Dictionary<string, Taxonomy> taxonomies = new Dictionary<string, Taxonomy>();
        public Dictionary<string, MeasureClass> measureClasses = new Dictionary<string, MeasureClass>();
        public Dictionary<string, MeasureUnit> measureUnits = new Dictionary<string, MeasureUnit>();
        public Dictionary<string, EnumerationList> enumerations = new Dictionary<string, EnumerationList>();

        public Dictionary<string, Dictionary<string, IClass>> classes = new Dictionary<string, Dictionary<string, IClass>>();

        public Dictionary<string, Dictionary<string, IAttribute>> attributes = new Dictionary<string, Dictionary<string, IAttribute>>();
        private List<IAttribute> attributesList = new List<IAttribute>();

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
            attributesList = new List<IAttribute>();
            attributes = new Dictionary<string, Dictionary<string, IAttribute>>();
            /*
            documents = new Dictionary<string, IClass>();
            functionals = new Dictionary<string, IClass>();
            physicals = new Dictionary<string, IClass>();
            */
            classes = new Dictionary<string, Dictionary<string, IClass>>();
            merged = new Dictionary<string, IClass>();

            taxonomies = new Dictionary<string, Taxonomy>();
            measureClasses = new Dictionary<string, MeasureClass>();
            measureUnits = new Dictionary<string, MeasureUnit>();
            enumerations = new Dictionary<string, EnumerationList>();

            AttributesCount = 0;
        }
        public void Clear()
        {
            attributes.Clear();
            attributesList.Clear();
            AttributesCount = 0;
            /*
            documents.Clear();
            functionals.Clear();
            physicals.Clear();
            */
            classes.Clear();
            merged.Clear();

            taxonomies.Clear();
            enumerations.Clear();
            measureUnits.Clear();
            measureClasses.Clear();
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
        public string Presence(IClass cmClass, IAttribute attribute)
        {
            List<IAttribute> permissibleAttributes = PermissibleAttributes(cmClass);
            // List<IAttribute> permissibleAttributes = cmClass.PermissibleAttributes;
            foreach (IAttribute permissibleAttribute in permissibleAttributes)
            {
                if (permissibleAttribute.Id.Equals(attribute.Id))
                {
                    if (!permissibleAttribute.Presence.Equals(""))
                    {
                        return permissibleAttribute.Presence.Substring(0, 1);
                    }
                    return "X";
                }
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

            SetInheritance();

            MergeByNames(); // MergeByAssociations();

            CalculateMaxDepth();
            // DefineClassAttributePresence(functionals);
            // DefineClassAttributePresence(physicals);
            // DefineClassAttributePresence(merged);

            // Get attributes list sorted by group

            foreach (KeyValuePair<string, Dictionary<string, IAttribute>> group in attributes)
            {
                foreach (IAttribute attribute in group.Value.Values)
                {
                    attributesList.Add(attribute);
                }
            }
        }
        private void DefineClassAttributePresence(Dictionary<string, IClass> map)
        {
            List<IAttribute> result = new List<IAttribute>();
            foreach (IClass cmClass in map.Values)
            {
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
                cmClass.PermissibleAttributes = new List<IAttribute>(result);
            }
        }
        private void SetInheritance()
        {
            foreach (Dictionary<string, IClass> map in classes.Values)
            {
                if (map.Count > 0)
                {
                    foreach (IClass cmClass in map.Values)
                    {
                        if (!cmClass.Extends.Equals(""))
                        {
                            map[cmClass.Extends].Children.Add(cmClass);
                        }
                    }
                }
            }
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
            merged.Clear();
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
        private void MergeByNames()
        {
            if (!classes.ContainsKey("physical"))
            {
                merged = classes["functionals"];
                return;
            }

            List<IClass> result = new List<IClass>();
            foreach (IClass functional in classes["functionals"].Values)
            {
                result.Add(functional);
                foreach (IClass physical in classes["physicals"].Values)
                {
                    if (physical.Name.Equals(functional.Name))
                    {
                        for (int childIndex = 0; childIndex < physical.Children.Count; childIndex++)
                        {
                            if (!functional.ContainsChildName(physical.Children[childIndex]))
                            {
                                result.Add(physical.Children[childIndex]);
                                functional.Children.Add(physical.Children[childIndex]);
                            }
                        }
                    }
                }
            }

            merged.Clear();
            foreach (IClass cmClass in result)
            {
                if (cmClass.Extends.Equals(""))
                {
                    cmClass.PermissibleAttributes = PermissibleAttributes(cmClass);
                    merged.Add(cmClass.Id, cmClass);
                    AddClassChildren(cmClass, merged);
                }
            }
        }
        public void AddClassChildren(IClass cmClass, Dictionary<string, IClass> map)
        {
            foreach (IClass child in cmClass.Children)
            {
                if (child != null)
                {
                    child.PermissibleAttributes = PermissibleAttributes(cmClass);
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
        private void WriteClass(KeyValuePair<KeyValuePair<int, IXLRange>, IClass> range)
        // private void WriteClass(IClass cmClass, IXLWorksheet worksheet, int row)
        {
            int count = maxDepth + AttributesCount + 2;
            string mergeRange = "";
            int row = range.Key.Key;
            IClass cmClass = range.Value;
            for (int col = 0; col < count; col++)
            {
                string text = "";
                string cellName = CellName(row, col);
                CellStyle style = CellStyle.Default;

                int classDepth = ClassDepth(cmClass);

                if (col == classDepth) // class name
                {
                    text = $"{cmClass.Name}";
                    style = CellStyle.Class;
                    mergeRange = $"{cellName}";
                }
                if (col == maxDepth) // class children depth
                {
                    if (!mergeRange.Equals(""))
                    {
                        mergeRange = $"";
                    }
                    else
                    {
                        mergeRange = $"{mergeRange}:{cellName}";
                    }
                }

                if (col == maxDepth + 1) // class discipline
                {
                    // text = $"{cmClass}";
                    style = CellStyle.Discipline;
                }

                if (col == maxDepth + 2) // class id
                {
                    text = $"{cmClass.Id}";
                    style = CellStyle.ClassId;
                }

                if (col > maxDepth + 2)
                {
                    string presence = Presence(cmClass, attributesList.ElementAt(col - maxDepth - 2));
                    switch (presence)
                    {
                        case "X":
                            style = CellStyle.PresenceUnselect;
                            break;
                        case "O":
                            style = CellStyle.PresenceOptional;
                            break;
                        case "P":
                            style = CellStyle.PresencePreffered;
                            break;
                        case "R":
                            style = CellStyle.PresenceRequired;
                            break;
                        default:
                            break;
                    }
                    text = presence;
                }

                WriteCell(range.Key.Value, row, col, text, style);

                if (mergeRange.Contains(":"))
                {
                    range.Key.Value.Range(mergeRange).Merge();
                    mergeRange = "";
                }
            }
        }
        private void SetCell(IXLCell cell, CellStyle style, string value)
        {
            cell.Value = value;
            cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            cell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            cell.Style.Border.OutsideBorder = XLBorderStyleValues.None;
            cell.Style.Fill.BackgroundColor = XLColor.White;
            cell.Style.Font.FontColor = XLColor.Black;

            switch (style)
            {
                case CellStyle.Class:
                    cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                    break;

                case CellStyle.ClassId:
                    cell.Style.Fill.BackgroundColor = XLColor.Green;
                    cell.Style.Font.FontColor = XLColor.Black;
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
                    cell.Style.Font.FontColor = XLColor.Black;
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
                    cell.Style.Fill.BackgroundColor = XLColor.SkyMagenta;
                    break;

                case CellStyle.PresenceNonApplicable:
                    cell.Style.Fill.BackgroundColor = XLColor.Red;
                    cell.Style.Font.FontColor = XLColor.White;
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
        // private void WriteCell(IXLWorksheet worksheet, int row, int col, string text, CellStyle style)
        private void WriteCell(IXLRange range, int row, int col, string text, CellStyle style)
        {
            string cellName = CellName(row, col);
            IXLCell cell = range.Cell(cellName);
            SetCell(cell, style, text);
        }
        public void GetPermissibleGrid(string filename)
        {
            using (XLWorkbook workbook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workbook.Worksheets.Add($"Permissible Grid");

                int count = maxDepth + AttributesCount + 2;

                // write header

                Queue<string> rangesForMerging = new Queue<string>();
                rangesForMerging.Enqueue($"{CellName(0, 0)}:{CellName(1, maxDepth + 2)}");

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
                        worksheet.Column(col).AdjustToContents();
                    }
                    rangesForMerging.Enqueue($"{mergedCell}:{CellName(0, col - 1)}");
                }

                for(col = 0; col < maxDepth + 3; col++)
                {
                    string value = "";
                    string name = CellName(2, col);
                    mergedCell = $"{name}";
                    
                    if (col == 0)
                    {
                        value = $"Classes ({merged.Count})";
                    }

                    if (col == maxDepth)
                    {
                        rangesForMerging.Enqueue($"{mergedCell}:{name}");
                    }

                    if (col == maxDepth + 1)
                    {
                        value = $"Discipline";
                    }

                    if (col == maxDepth + 2)
                    {
                        value = $"Class ID";
                    }

                    SetCell(worksheet.Cell($"{name}"), CellStyle.Header, value);
                }

                
                foreach (string range in rangesForMerging)
                {
                    worksheet.Range(range).Merge();
                }
                
                // write permissible grid

                ConcurrentDictionary<KeyValuePair<int, IXLRange>, IClass> ranges = new ConcurrentDictionary<KeyValuePair<int, IXLRange>, IClass>();
                int row = 3;
                foreach(IClass cmClass in merged.Values)
                {
                    ranges.TryAdd(new KeyValuePair<int, IXLRange>(row, worksheet.Range($"{CellName(row, 0)}:{CellName(row, count)}")), cmClass);
                    row++;
                }

                var parallelOptions = new ParallelOptions()
                {
                    MaxDegreeOfParallelism = 2
                };
                Parallel.ForEach(ranges, parallelOptions, singleRange =>
                {
                    WriteClass(singleRange);
                });

                /*
                foreach(var singleRange in ranges)
                {
                    WriteClass(singleRange);
                }
                */
                
                for(col = 1; col <= worksheet.ColumnCount(); col++)
                {
                    worksheet.Column(col).AdjustToContents();
                }
                
                workbook.SaveAs(filename);
            }
        }
    }
}
