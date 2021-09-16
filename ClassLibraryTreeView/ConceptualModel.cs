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
        // public Dictionary<string, IAttribute> attributes;
        public Dictionary<string, IClass> documents = new Dictionary<string, IClass>();
        public Dictionary<string, IClass> functionals = new Dictionary<string, IClass>();
        public Dictionary<string, IClass> physicals = new Dictionary<string, IClass>();

        public Dictionary<string, Taxonomy> taxonomies = new Dictionary<string, Taxonomy>();
        public Dictionary<string, MeasureClass> measureClasses = new Dictionary<string, MeasureClass>();
        public Dictionary<string, MeasureUnit> measureUnits = new Dictionary<string, MeasureUnit>();
        public Dictionary<string, EnumerationList> enumerations = new Dictionary<string, EnumerationList>();

        public Dictionary<string, Dictionary<string, IAttribute>> attributes = new Dictionary<string, Dictionary<string, IAttribute>>();
        public List<IClass> merged = new List<IClass>();

        private List<IAttribute> attributesList = new List<IAttribute>();
        private List<string> attributesGroupList = new List<string>(); // format - "name::indexRange"

        public int AttributesCount = 0;

        public int maxDepth = 0;

        private void CalculateMaxDepth()
        {

            maxDepth = MapMaxDepth(functionals);
            int maxDepthAuxiliary = MapMaxDepth(physicals);
            if (maxDepth < maxDepthAuxiliary)
            {
                maxDepth = maxDepthAuxiliary;
            }

            // maxDepth = ListMaxDepth(merged);
        }

        public ConceptualModel()
        {
            Init();
        }
        public void Init()
        {
            attributesList = new List<IAttribute>();
            attributesGroupList = new List<string>();

            attributes = new Dictionary<string, Dictionary<string, IAttribute>>();
            documents = new Dictionary<string, IClass>();
            functionals = new Dictionary<string, IClass>();
            physicals = new Dictionary<string, IClass>();

            taxonomies = new Dictionary<string, Taxonomy>();
            measureClasses = new Dictionary<string, MeasureClass>();
            measureUnits = new Dictionary<string, MeasureUnit>();
            enumerations = new Dictionary<string, EnumerationList>();

            merged = new List<IClass>();

            AttributesCount = 0;
        }
        public void Clear()
        {
            attributes.Clear();
            documents.Clear();
            functionals.Clear();
            physicals.Clear();

            taxonomies.Clear();
            enumerations.Clear();
            measureUnits.Clear();
            measureClasses.Clear();

            merged.Clear();

            attributesGroupList.Clear();
            attributesList.Clear();
            AttributesCount = 0;
        }
        public List<IAttribute> PermissibleAttributes(IClass cmClass)
        {
            Dictionary<string, IClass> map = null;
            string xtype = cmClass.Xtype.ToLower();
            if (xtype.Equals("documents"))
            {
                map = documents;
            }
            if (xtype.Equals("functionals"))
            {
                map = functionals;
            }
            if (xtype.Equals("physicals"))
            {
                map = physicals;
            }
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
        public void AddClass(IClass cmClass, int classConcept)
        {
            switch (classConcept)
            {
                case 0:
                    functionals.Add(cmClass.Id, cmClass);
                    break;
                case 1:
                    physicals.Add(cmClass.Id, cmClass);
                    break;
                case 2:
                    documents.Add(cmClass.Id, cmClass);
                    break;
                default:
                    break;
            }
        }
        public int ClassDepth(IClass cmClass)
        {
            Dictionary<string, IClass> map = null;
            if (cmClass.Xtype.ToLower().Equals("functionals"))
            {
                map = functionals;
            }
            if (cmClass.Xtype.ToLower().Equals("physicals"))
            {
                map = physicals;
            }
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
        public int ListMaxDepth(List<IClass> list)
        {
            if (list.Count == 0)
            {
                return 0;
            }
            int maxDepth = 0;
            foreach (IClass cmClass in list)
            {
                int depth = ClassDepth(cmClass);
                if (depth > maxDepth)
                {
                    maxDepth = depth;
                }
            }
            return maxDepth;
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
                string name = element.Name.LocalName;
                if (name.ToLower().Equals("referencedata"))
                {
                    GetReferenceData(element);
                }
                if (name.ToLower().Equals("functionals"))
                {
                    MapFromXElement(element, functionals);
                }
                if (name.ToLower().Equals("physicals"))
                {
                    MapFromXElement(element, physicals);
                }
                if (name.ToLower().Equals("documents"))
                {
                    MapFromXElement(element, documents);
                }
                if (name.ToLower().Equals("attributes"))
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
                            attributesGroupList.Add(group);
                        }

                        attributes[group].Add(newAttribute.Id, newAttribute);
                        AttributesCount++;
                    }
                }
            }
            SetInheritance(documents);
            SetInheritance(functionals);
            SetInheritance(physicals);
            MergeByNames();
            // MergeByAssociations();

            CalculateMaxDepth();

            // Get attributes list sorted by group

            foreach (KeyValuePair<string, Dictionary<string, IAttribute>> group in attributes)
            {
                foreach (IAttribute attribute in group.Value.Values)
                {
                    attributesList.Add(attribute);
                }
            }
        }
        private void SetInheritance(Dictionary<string, IClass> map)
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
        public IClass GetClass(string id, string xtype)
        {
            if (xtype.ToLower().Equals("functionals"))
            {
                return functionals[id];
            }
            if (xtype.ToLower().Equals("physicals"))
            {
                return physicals[id];
            }
            if (xtype.ToLower().Equals("documents"))
            {
                return documents[id];
            }
            return null;
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
            foreach (IClass cmClass in functionals.Values)
            {
                if (!cmClass.Extends.Equals(""))
                {
                    // continue;
                }

                if (!merged.Contains(cmClass))
                {
                    merged.Add(cmClass);
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
                                merged.Add(functionals[rules[index]]);
                            }
                            if (concept.ToLower().Equals("physical"))
                            {
                                merged.Add(physicals[rules[index]]);
                            }
                        }
                    }
                }
            }
        }
        private void MergeByNames()
        {
            // physicals, functionals
            List<IClass> classes = new List<IClass>();
            foreach (IClass functional in functionals.Values)
            {
                classes.Add(functional);
                foreach (IClass physical in physicals.Values)
                {
                    if (physical.Name.Equals(functional.Name))
                    {
                        for (int index = 0; index < physical.Children.Count; index++)
                        {
                            if (!functional.ContainsChildName(physical.Children[index]))
                            {
                                classes.Add(physical.Children[index]);
                                functional.Children.Add(physical.Children[index]);
                            }
                        }
                    }
                }
            }

            merged.Clear();
            foreach (IClass cmClass in classes)
            {
                if (cmClass.Extends.Equals(""))
                {
                    cmClass.PermissibleAttributes = PermissibleAttributes(cmClass);
                    merged.Add(cmClass);
                    AddClassChildren(cmClass, merged);
                }
            }
        }
        public void AddClassChildren(IClass cmClass, List<IClass> classes)
        {
            foreach (IClass child in cmClass.Children)
            {
                if (child != null)
                {
                    classes.Add(child);
                    AddClassChildren(child, classes);
                }
            }
        }
        private void AddChildrenPresence(IClass cmClass, List<GridCell[]> grid)
        {
            if (cmClass.Children.Count > 0)
            {
                foreach (IClass child in cmClass.Children)
                {
                    AddPresence(child, grid);
                }
            }
        }
        private void AddPresence(IClass cmClass, List<GridCell[]> grid)
        {
            IAttribute[] attributes = attributesList.ToArray();

            GridCell[] row = new GridCell[maxDepth + attributes.Length + 1];
            int classDepth = ClassDepth(cmClass);
            /*
            for (int depth = 0; depth < maxDepth; depth++)
            {
                row[depth].Text = "";
                row[depth].StyleIndex = 6;
            }

            row[classDepth].Text = $"{cmClass.Name}";
            row[maxDepth].Text = $"{cmClass.Id}";

            for (int index = 1; index <= attributes.Length; index++)
            {
                row[index + maxDepth].Text = Presence(cmClass, attributes[index - 1]);
                row[index + maxDepth].StyleIndex = 7;
            }
            */
            grid.Add(row);

            AddChildrenPresence(cmClass, grid);
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
        private void WriteClass(IClass cmClass, IXLWorksheet worksheet, int row)
        {
            int colCount = maxDepth + AttributesCount + 2;
            for (int col = 0; col < colCount; col++)
            {
                string text = "";
                CellStyle style = CellStyle.Default;
                string merge = "";

                int classDepth = ClassDepth(cmClass);

                if (col == classDepth) // class name
                {
                    text = $"{cmClass.Name}";
                    style = CellStyle.Class;
                    merge = "[MS]";
                }
                if (col == maxDepth) // class children depth
                {
                    if (merge == "[MS]")
                    {
                        merge = "";
                    }
                    else
                    {
                        merge = "[MF]";
                    }
                }

                if (col == maxDepth + 1) // class discipline
                {
                    // text = $"{cmClass}";
                    style = CellStyle.Discipline;
                    // merge = GridCellMergeProperty.NoMerging;
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

                WriteCell(worksheet, row, col, text, style);
            }
        }
        private void SetCellStyle(IXLCell cell, CellStyle style)
        {
            switch (style)
            {
                case CellStyle.Default:
                    cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    cell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    cell.Style.Alignment.TextRotation = 0;
                    cell.Style.Fill.BackgroundColor = XLColor.White;
                    cell.Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    cell.Style.Font.FontColor = XLColor.Black;
                    break;

                case CellStyle.Empty:
                    cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    cell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    cell.Style.Alignment.TextRotation = 0;
                    cell.Style.Fill.BackgroundColor = XLColor.White;
                    cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    cell.Style.Font.FontColor = XLColor.Black;
                    break;

                case CellStyle.Class:
                    cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                    cell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    cell.Style.Alignment.TextRotation = 0;
                    cell.Style.Fill.BackgroundColor = XLColor.White;
                    cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    cell.Style.Font.FontColor = XLColor.Black;
                    break;

                case CellStyle.ClassId:
                    cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    cell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    cell.Style.Alignment.TextRotation = 0;
                    cell.Style.Fill.BackgroundColor = XLColor.Green;
                    cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    cell.Style.Font.FontColor = XLColor.Black;
                    break;

                case CellStyle.Attribute:
                    cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    cell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Bottom;
                    cell.Style.Alignment.TextRotation = 90;
                    cell.Style.Fill.BackgroundColor = XLColor.BlueGray;
                    cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    // cell.Style.Border.color
                    cell.Style.Font.FontColor = XLColor.White;
                    break;

                case CellStyle.AttributesGroup:
                    cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    cell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    cell.Style.Alignment.TextRotation = 0;
                    cell.Style.Fill.BackgroundColor = XLColor.BlueViolet;
                    cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    // cell.Style.Border.color
                    cell.Style.Font.FontColor = XLColor.White;
                    break;

                case CellStyle.Discipline:
                    cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    cell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    cell.Style.Alignment.TextRotation = 0;
                    cell.Style.Fill.BackgroundColor = XLColor.Yellow;
                    cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    cell.Style.Font.FontColor = XLColor.Black;
                    break;

                case CellStyle.Header:
                    cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                    cell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    cell.Style.Alignment.TextRotation = 0;
                    cell.Style.Fill.BackgroundColor = XLColor.Blue;
                    cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    cell.Style.Font.FontColor = XLColor.White;
                    break;

                case CellStyle.PresenceUnselect:
                    cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    cell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    cell.Style.Alignment.TextRotation = 0;
                    cell.Style.Fill.BackgroundColor = XLColor.White;
                    cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    cell.Style.Font.FontColor = XLColor.Black;
                    break;

                case CellStyle.PresenceNonApplicable:
                    cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    cell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    cell.Style.Alignment.TextRotation = 0;
                    cell.Style.Fill.BackgroundColor = XLColor.Red;
                    cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    cell.Style.Font.FontColor = XLColor.White;
                    break;

                case CellStyle.PresenceOptional:
                    cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    cell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    cell.Style.Alignment.TextRotation = 0;
                    cell.Style.Fill.BackgroundColor = XLColor.AppleGreen;
                    cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    cell.Style.Font.FontColor = XLColor.Black;
                    break;

                case CellStyle.PresencePreffered:
                    cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    cell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    cell.Style.Alignment.TextRotation = 0;
                    cell.Style.Fill.BackgroundColor = XLColor.AppleGreen;
                    cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    cell.Style.Font.FontColor = XLColor.Black;
                    break;

                case CellStyle.PresenceRequired:
                    cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    cell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    cell.Style.Alignment.TextRotation = 0;
                    cell.Style.Fill.BackgroundColor = XLColor.DarkGreen;
                    cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    cell.Style.Font.FontColor = XLColor.White;
                    break;

                default:
                    break;
            }
        }
        private void WriteCell(IXLWorksheet worksheet, int row, int col, string text, CellStyle style)
        {
            string cellName = CellName(row, col);
            IXLCell cell = worksheet.Cell(cellName);
            SetCellStyle(cell, style);
            cell.Value = $"{text}";
        }
        public void GetPermissibleGrid(string filename)
        {
            using (XLWorkbook workbook = new XLWorkbook())
            {
                // wb.Worksheets.Add(dataTable, "Permissible Grid");
                IXLWorksheet worksheet = workbook.Worksheets.Add($"Permissible Grid");

                int rowCount = merged.Count + 3;
                int colCount = maxDepth + AttributesCount + 2;
                int row = 0;

                // write header

                for (row = 0; row < 3; row++)
                {
                    int col = 0;
                    foreach (string group in attributes.Keys)
                    {
                        bool groupStart = true;
                        int index = 0;
                        foreach (IAttribute attribute in attributes[group].Values)
                        {
                            CellStyle style = CellStyle.Default;
                            string text = "";
                            string merge = "";

                            if (row == 2)
                            {
                                if (col == 0)
                                {
                                    style = CellStyle.Header;
                                    merge = "[MS]";
                                    text = $"Classes ({merged.Count})";
                                }

                                if (col == maxDepth)
                                {
                                    merge = "[MF]";
                                }

                                if (col == maxDepth + 1)
                                {
                                    style = CellStyle.Header;
                                    text = $"Discipline";
                                }

                                if (col == maxDepth + 2)
                                {
                                    style = CellStyle.Header;
                                    text = $"Class ID";
                                }
                            }

                            if (col > maxDepth + 2)
                            {
                                if (row == 0)
                                {
                                    style = CellStyle.Default;
                                    if (groupStart)
                                    {
                                        style = CellStyle.AttributesGroup;
                                        text = group;
                                        groupStart = false;
                                        merge = "[MS]";
                                    }
                                    if (index == attributes[group].Values.Count - 1)
                                    {
                                        merge = "[MF]";
                                    }
                                }
                                if (row == 1)
                                {
                                    style = CellStyle.Attribute;
                                    text = $"{attribute.Id} : {attribute.Name}";
                                }
                            }

                            WriteCell(worksheet, row, col, text, style);

                            index++;
                            col++;
                        }
                    }
                }

                row = 3;

                /*
                Parallel.ForEach(merged, cmClass =>
                {
                    WriteClass(cmClass, worksheet, row);
                    Interlocked.Increment(ref row);
                });
                */
                foreach(IClass cmClass in merged)
                {
                    WriteClass(cmClass, worksheet, row);
                    Interlocked.Increment(ref row);
                    row++;
                }

                workbook.SaveAs(filename);
            }
            // var query = merged.AsParallel().AsQueryable().Select(WriteClass);
            // IEnumerable<DataRow> query = merged.AsParallel().AsQueryable().Select(AddDataRow);
        }
    }
}
