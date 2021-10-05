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
    public partial class ConceptualModel
    {
        public event EventHandler<int> ExportProgress;

        public Dictionary<string, Dictionary<string, CMClass>> classes = new Dictionary<string, Dictionary<string, CMClass>>();
        public Dictionary<string, Dictionary<string, CMAttribute>> attributes = new Dictionary<string, Dictionary<string, CMAttribute>>();

        public Dictionary<string, Taxonomy> taxonomies = new Dictionary<string, Taxonomy>();
        public Dictionary<string, MeasureUnit> measureUnits = new Dictionary<string, MeasureUnit>();
        public Dictionary<string, MeasureClass> measureClasses = new Dictionary<string, MeasureClass>();
        public Dictionary<string, EnumerationList> enumerations = new Dictionary<string, EnumerationList>();

        public int AttributesCount { get; set; }
        public int MaxDepth { get; set; }
        public string FullPathXml { get; set; }
        public string ModelName { get; set; }
        public Dictionary<string, Taxonomy> Taxonomies => taxonomies;
        public Dictionary<string, EnumerationList> Enumerations => enumerations;
        public Dictionary<string, MeasureUnit> MeasureUnits => measureUnits;
        public Dictionary<string, MeasureClass> MeasureClasses => measureClasses;
        public Dictionary<string, CMClass> Functionals
        {
            get
            {
                if (classes.ContainsKey("functionals"))
                {
                    return classes["functionals"];
                }
                return null;
            }
        }
        public Dictionary<string, CMClass> Physicals
        {
            get
            {
                if (classes.ContainsKey("physicals"))
                {
                    return classes["physicals"];
                }
                return null;
            }
        }
        private void CalculateMaxDepth()
        {
            MaxDepth = 0;
            foreach(Dictionary<string, CMClass> map in classes.Values)
            {
                foreach (CMClass cmClass in map.Values)
                {
                    int depth = cmClass.Depth;
                    if (depth > MaxDepth)
                    {
                        MaxDepth = depth;
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
            classes = new Dictionary<string, Dictionary<string, CMClass>>();
            attributes = new Dictionary<string, Dictionary<string, CMAttribute>>();

            taxonomies = new Dictionary<string, Taxonomy>();
            measureClasses = new Dictionary<string, MeasureClass>();
            measureUnits = new Dictionary<string, MeasureUnit>();
            enumerations = new Dictionary<string, EnumerationList>();

            AttributesCount = 0;
        }
        public void Clear()
        {
            AttributesCount = 0;

            classes.Clear();
            attributes.Clear();
            taxonomies.Clear();
            enumerations.Clear();
            measureUnits.Clear();
            measureClasses.Clear();
        }
        public CMAttribute GetAttributeById(string id)
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
        public int MapMaxDepth(Dictionary<string, CMClass> map)
        {
            if (map.Count == 0)
            {
                return 0;
            }
            int MaxDepth = 0;
            foreach (CMClass cmClass in map.Values)
            {
                int depth = cmClass.Depth;
                if (depth > MaxDepth)
                {
                    MaxDepth = depth;
                }
            }
            return MaxDepth;
        }
        private void MapFromXElement(XElement element, Dictionary<string, CMClass> map)
        {
            foreach (XElement child in element.Elements())
            {
                if (!child.Name.LocalName.ToLower().Equals("extension"))
                {
                    CMClass newClass = new CMClass(child);
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
        public bool OpenFile()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = ".";
                openFileDialog.Filter = "XML files (*.xml)|*.xml";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filename = openFileDialog.FileName;
                    FullPathXml = filename;
                    ImportXml(filename);
                    filename = filename.Remove(filename.LastIndexOf("."), filename.Length - filename.LastIndexOf("."));
                    filename = filename.Substring(filename.LastIndexOf("\\") + 1, filename.Length - filename.LastIndexOf("\\") - 1);
                    ModelName = $"{filename}";
                }
            }
            return true;
        }
        private void DefinePermissibleAttributesNames()
        {
            foreach (Dictionary<string, CMClass> map in classes.Values)
            {
                if (map.Count > 0)
                {
                    foreach (CMClass cmClass in map.Values)
                    {
                        if (cmClass.PermissibleAttributes.Count > 0)
                        {
                            foreach (CMAttribute cmClassAttribute in cmClass.PermissibleAttributes.Values)
                            {
                                CMAttribute attribute = GetAttribute(cmClassAttribute.Id);
                                if (attribute != null)
                                {
                                    attribute.AddApplicableClass(cmClass);
                                }
                                if (cmClassAttribute != null)
                                {
                                    cmClassAttribute.AddApplicableClass(cmClass);
                                }
                                if (cmClassAttribute.Name.Equals(""))
                                {
                                    foreach (string group in attributes.Keys)
                                    {
                                        if (attributes[group].ContainsKey(cmClassAttribute.Id))
                                        {
                                            cmClassAttribute.Name = attributes[group][cmClassAttribute.Id].Name;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        private void SetClassesInheritance()
        {
            foreach (Dictionary<string, CMClass> map in classes.Values)
            {
                if (map.Count > 0)
                {
                    foreach (CMClass cmClass in map.Values)
                    {
                        if (!cmClass.Extends.Equals(""))
                        {
                            cmClass.Parent = map[cmClass.Extends];
                            if (cmClass.Parent.PermissibleAttributes.Count > 0)
                            {
                                foreach (CMAttribute parentAttribute in cmClass.Parent.PermissibleAttributes.Values)
                                {
                                    if (!cmClass.PermissibleAttributes.ContainsKey(parentAttribute.Id))
                                    {
                                        CMAttribute newAttribute = new CMAttribute(parentAttribute);
                                        if (parentAttribute.CameFrom != null)
                                        {
                                            newAttribute.CameFrom = parentAttribute.CameFrom;
                                        }
                                        else
                                        {
                                            newAttribute.CameFrom = cmClass.Parent;
                                        }
                                        cmClass.PermissibleAttributes.Add(newAttribute.Id, newAttribute);
                                        CMAttribute attribute = GetAttributeById(newAttribute.Id);
                                        if (attribute != null)
                                        {
                                            attribute.AddApplicableClass(cmClass);
                                        }
                                        if (newAttribute != null)
                                        {
                                            newAttribute.AddApplicableClass(cmClass);
                                        }
                                    }
                                }
                            }
                            map[cmClass.Extends].Children.Add(cmClass.Id, cmClass);
                        }
                    }
                }
            }
        }
        public void ImportXml(string fileName)
        {
            Clear();
            FullPathXml = fileName;
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
                    classes.Add(name, new Dictionary<string, CMClass>());
                    MapFromXElement(element, classes[name]);
                }

                if (name.Equals("attributes"))
                {
                    foreach (XElement child in element.Elements())
                    {
                        CMAttribute newAttribute = new CMAttribute(child);

                        string group = newAttribute.Group;

                        if (group.Equals(""))
                        {
                            group = "Unset";
                        }

                        if (!attributes.ContainsKey(group))
                        {
                            attributes.Add(group, new Dictionary<string, CMAttribute>());
                        }

                        attributes[group].Add(newAttribute.Id, newAttribute);
                        AttributesCount++;
                    }
                }
            }

            DefinePermissibleAttributesNames();
            SetClassesInheritance();

            if (Physicals == null)
            {
                CalculateMaxDepth();
                return;
            }

            Action<CMClass, CMClass> MergeAttributes = (cmClassSource, cmClassRecipient) =>
            {
                if (cmClassSource.PermissibleAttributes.Count == 0)
                {
                    return;
                }

                foreach (CMAttribute cmClassSourceAttribute in cmClassSource.PermissibleAttributes.Values)
                {
                    if (!cmClassRecipient.PermissibleAttributes.ContainsKey(cmClassSourceAttribute.Id))
                    {
                        CMAttribute newAttribute = new CMAttribute(cmClassSourceAttribute);
                        if (cmClassSourceAttribute.CameFrom != null)
                        {
                            newAttribute.CameFrom = cmClassSourceAttribute.CameFrom;
                        }
                        else
                        {
                            newAttribute.CameFrom = cmClassSource;
                        }
                        cmClassRecipient.PermissibleAttributes.Add(newAttribute.Id, newAttribute);
                        CMAttribute attribute = GetAttributeById(newAttribute.Id);
                        if (attribute != null)
                        {
                            attribute.AddApplicableClass(cmClassRecipient);
                        }
                        if (newAttribute != null)
                        {
                            newAttribute.AddApplicableClass(cmClassRecipient);
                        }
                    }
                }
            };

            // merge permissible attributes of roots functional and physical classes

            foreach (CMClass physicalClass in classes["physicals"].Values)
            {
                if (physicalClass.Parent == null)
                {
                    foreach (CMClass functionalClass in classes["functionals"].Values)
                    {
                        if (functionalClass.Parent == null)
                        {
                            MergeAttributes(functionalClass, physicalClass);
                            break;
                        }
                    }
                    break;
                }
            }

            // merge permissible attributes of functional and physical classes (classes merged by name)

            foreach (CMClass physicalClass in classes["physicals"].Values)
            {
                foreach (CMClass functionalClass in classes["functionals"].Values)
                {
                    if (functionalClass.Name.Equals(physicalClass.Name))
                    {
                        if (functionalClass.PermissibleAttributes.Count > 0)
                        {
                            MergeAttributes(functionalClass, physicalClass);

                            if (physicalClass.Children.Count > 0)
                            {
                                foreach(CMClass physicalClassChild in physicalClass.Children.Values)
                                {
                                    MergeAttributes(functionalClass, physicalClassChild);
                                }
                            }
                        }

                        CMClass functionalClassParent = functionalClass.Parent;
                        while (functionalClassParent != null)
                        {
                            MergeAttributes(functionalClassParent, physicalClass);

                            MergeAttributes(functionalClass, physicalClass);

                            if (physicalClass.Children.Count > 0)
                            {
                                foreach (CMClass physicalClassChild in physicalClass.Children.Values)
                                {
                                    MergeAttributes(functionalClassParent, physicalClassChild);
                                }
                            }

                            functionalClassParent = functionalClassParent.Parent;
                        }
                    }
                }
            }

            CalculateMaxDepth();
        }
        public CMAttribute GetAttributeByName(string name)
        {
            foreach (string group in attributes.Keys)
            {
                foreach (CMAttribute attribute in attributes[group].Values)
                {
                    if (attribute.Name.Equals(name))
                    {
                        return attribute;
                    }
                }
            }
            return null;
        }
        public CMAttribute GetAttribute(string id)
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
        public CMAttribute GetAttribute(int number)
        {
            int col = 0;
            foreach (string group in attributes.Keys)
            {
                foreach (CMAttribute attribute in attributes[group].Values)
                {
                    if (col == number)
                    {
                        return attribute;
                    }
                    col++;
                }
            }
            return null;
        }
        public CMClass GetClass(string id)
        {
            foreach(var map in classes.Values)
            {
                if (map.ContainsKey(id))
                {
                    return map[id];
                }
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
            if (classes.ContainsKey("merged"))
            {
                classes.Remove("merged");
            }
            classes.Add("merged", new Dictionary<string, CMClass>());
            Dictionary<string, CMClass> merged = classes["merged"];
            foreach (CMClass cmClass in classes["functionals"].Values)
            {
                if (!cmClass.Extends.Equals(""))
                {
                    // continue;
                }

                if (!merged.Values.Contains(cmClass))
                {
                    merged.Add(cmClass.Id, cmClass);
                }

                foreach (CMAttribute attribute in cmClass.PermissibleAttributes.Values)
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

        private void MergeByName()
        {
            if (!classes.ContainsKey("merged"))
            {
                classes.Add("merged", new Dictionary<string, CMClass>());
            }

            bool hasPhysicals = classes.ContainsKey("physicals");
            
            foreach (CMClass functionalClass in classes["functionals"].Values)
            {
                // IClass mergedClass = new IClass(functionalClass);

                if (hasPhysicals)
                {
                    foreach (CMClass physicalClass in classes["physicals"].Values)
                    {
                        if (physicalClass.Name.Equals(functionalClass.Name))
                        {
                            if (physicalClass.Children.Count > 0)
                            {
                                foreach (CMClass physicalChild in physicalClass.Children.Values)
                                {
                                    // mergedClass.Children.Add(child.Id, child);
                                    CMClass functionalChild = functionalClass.GetChildByName(physicalChild);
                                    if (functionalChild == null)
                                    {
                                        physicalChild.Extends = functionalClass.Id;
                                        functionalClass.Children.Add(physicalChild.Id, physicalChild);
                                        // functionalChild.Id = physicalChild.Id;
                                    }
                                    
                                }
                            }
                        }
                    }
                }

                // classes["merged"].Add(mergedClass.Id, mergedClass);
                classes["merged"].Add(functionalClass.Id, functionalClass);
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
        private void WriteClassAttributes(IXLWorksheet worksheet, KeyValuePair<int, string[]> rowPair, int classesCount)
        {
            int row = rowPair.Key;
            string[] values = rowPair.Value;
            CellStyle style = CellStyle.Class;
            if (row % 2 == 0)
            {
                // style = CellStyle.ClassId;
            }

            for (int col = 0; col < values.Length; col++)
            {
                SetCell(worksheet.Cell(CellName(row, col)), style, values[col]);
            }

            int progress = (row * 100) / classesCount;
            this.ExportProgress?.Invoke(this, progress);
        }
        private void WriteClass(IXLWorksheet worksheet, KeyValuePair<int, CMClass> classRow, Queue<string> mergedRanges, int classesCount)
        {
            int row = classRow.Key;
            CMClass cmClass = classRow.Value;
            int classDepth = cmClass.Depth;
            int count = MaxDepth + AttributesCount + 2;

            SetCell(worksheet.Cell(CellName(row, MaxDepth + 2)), CellStyle.ClassId, cmClass.Id); // set class id cell
            SetCell(worksheet.Cell(CellName(row, classDepth)), CellStyle.Class, cmClass.Name); // set class name cell
            SetCell(worksheet.Cell(CellName(row, MaxDepth + 1)), CellStyle.Discipline, ""); // set class discipline cell

            if (MaxDepth != classDepth) // merge subclass cells
            {
                mergedRanges.Enqueue($"{CellName(row, classDepth)}:{CellName(row, MaxDepth)}");
            }

            for (int col = MaxDepth + 3; col < count; col++) // set class attributes presence cells
            {
                string presence = cmClass.PermissibleAttributePresence(GetAttribute(col - MaxDepth - 2).Id);
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

            int progress = (row * 100) / classesCount;
            this.ExportProgress?.Invoke(this, progress);
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
        private int AddClassAttributes(CMClass cmClass, List<KeyValuePair<int, string[]>> rows, int rowIndex)
        {
            int row = rowIndex;
            Dictionary<string, CMAttribute> attributes = cmClass.PermissibleAttributes;
            if (attributes.Count > 0)
            {
                foreach (CMAttribute attribute in attributes.Values)
                {
                    rows.Add(new KeyValuePair<int, string[]>(row, new string[] { cmClass.Id, cmClass.Name, attribute.Id, attribute.Name }));
                    row++;
                }
            }
            return row;
        }
        public void ExportClassAttributes()
        {
            int row = 1;
            List<KeyValuePair<int, string[]>> rows = new List<KeyValuePair<int, string[]>>();

            Dictionary<string, CMClass> map = null;
            if (Physicals != null)
            {
                map = Physicals;
            }
            else
            {
                if (Functionals != null)
                {
                    map = Functionals;
                }
            }

            if (map == null)
            {
                return;
            }

            foreach (CMClass cmClass in map.Values)
            {
                row = AddClassAttributes(cmClass, rows, row);
            }

            using (XLWorkbook workbook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workbook.Worksheets.Add($"MergedClassAttributes");

                SetCell(worksheet.Cell(CellName(0, 0)), CellStyle.Header, $"Class ID");
                SetCell(worksheet.Cell(CellName(0, 1)), CellStyle.Header, $"Class Name");
                SetCell(worksheet.Cell(CellName(0, 2)), CellStyle.Header, $"Attribute ID");
                SetCell(worksheet.Cell(CellName(0, 3)), CellStyle.Header, $"Attribute Name");

                foreach (var classRow in rows)
                {
                    WriteClassAttributes(worksheet, classRow, rows.Count);
                }

                for (int col = 1; col < 5; col++)
                {
                    worksheet.Column(col).AdjustToContents();
                }

                string filename = FullPathXml;
                filename = filename.Remove(filename.LastIndexOf("."), filename.Length - filename.LastIndexOf("."));
                filename += "_ClassAttributes.xlsx";
                try
                {
                    workbook.SaveAs(filename);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private int AddClassChildren(CMClass cmClass, ConcurrentDictionary<int, CMClass> classRows, int rowIndex)
        {
            int row = rowIndex;
            if (cmClass.Children.Count > 0)
            {
                foreach(CMClass cmClassChild in cmClass.Children.Values)
                {
                    if (classRows.TryAdd(row, cmClassChild))
                    {
                        row++;
                    }
                    row = AddClassChildren(cmClassChild, classRows, row);
                }
            }
            return row;
        }
        public void ExportPermissibleGrid()
        {
            using (XLWorkbook workbook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workbook.Worksheets.Add($"Permissible Grid");

                int count = MaxDepth + AttributesCount + 2;

                // define classes attributes rows

                Dictionary<string, CMClass> map = null;
                if (Physicals != null)
                {
                    map = Physicals;
                }
                else
                {
                    if (Functionals != null)
                    {
                        map = Functionals;
                    }
                }

                if (map == null)
                {
                    return;
                }

                int row = 3;

                ConcurrentDictionary<int, CMClass> classRows = new ConcurrentDictionary<int, CMClass>(); // dictionary of pairs "row number - conceptual model class"

                foreach (CMClass cmClass in map.Values) // fill dictionary of pairs <row number, conceptual model class>
                {
                    if (cmClass.Parent == null)
                    {
                        if (classRows.TryAdd(row, cmClass))
                        {
                            row++;
                        }
                        row = AddClassChildren(cmClass, classRows, row);
                    }
                }

                // write header

                SetCell(worksheet.Cell(CellName(2, 0)), CellStyle.Header, $"Classes ({map.Count})"); // set classes count header cell
                SetCell(worksheet.Cell(CellName(2, MaxDepth + 1)), CellStyle.Header, $"Discipline"); // set class discipline header cell
                SetCell(worksheet.Cell(CellName(2, MaxDepth + 2)), CellStyle.Header, $"Class ID"); // set class id header cell

                Queue<string> mergedRanges = new Queue<string>();
                mergedRanges.Enqueue($"{CellName(0, 0)}:{CellName(1, MaxDepth + 2)}");
                mergedRanges.Enqueue($"{CellName(2, 0)}:{CellName(2, MaxDepth)}");

                string mergedCell = "";
                int col = MaxDepth + 3;
                foreach (string group in attributes.Keys)
                {
                    mergedCell = $"{CellName(0, col)}";
                    IXLCell cell = worksheet.Cell($"{CellName(0, col)}");
                    SetCell(cell, CellStyle.AttributesGroup, group);
                    foreach (CMAttribute attribute in attributes[group].Values)
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
                        WriteClass(worksheet, classRow, mergedRanges, classRows.Count);
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

                string filename = FullPathXml;
                filename = filename.Remove(filename.LastIndexOf("."), filename.Length - filename.LastIndexOf("."));
                filename += ".xlsx";

                workbook.SaveAs(filename);
            }
        }
        public List<CMClass> GetClassesWithId(List<string> filter)
        {
            List<CMClass> classList = new List<CMClass>();

            foreach (Dictionary<string, CMClass> map in classes.Values)
            {
                foreach (string classId in map.Keys)
                {
                    if (filter.Contains(classId))
                    {
                        classList.Add(map[classId]);
                    }
                }
            }

            return classList;
        }
        public List<CMClass> GetClassesWithName(List<string> filter)
        {
            List<CMClass> classList = new List<CMClass>();

            foreach (Dictionary<string, CMClass> map in classes.Values)
            {
                foreach (CMClass cmClass in map.Values)
                {
                    if (filter.Contains(cmClass.Name))
                    {
                        classList.Add(cmClass);
                    }
                }
            }

            return classList;
        }
        public List<CMClass> GetClassesWithAttributeId(List<string> filter)
        {
            List<CMClass> classList = new List<CMClass>();
            
            foreach (Dictionary<string, CMAttribute> group in attributes.Values)
            {
                foreach (CMAttribute attribute in group.Values)
                {
                    if (filter.Contains(attribute.Id))
                    {
                        if (attribute.ApplicableClasses != null)
                        {
                            foreach (CMClass cmClass in attribute.ApplicableClasses.Values)
                            {
                                classList.Add(cmClass);
                            }
                        }
                    }
                }
            }

            return classList;
        }
        public List<CMClass> GetClassesWithAttributeName(List<string> filter)
        {
            List<CMClass> classList = new List<CMClass>();

            foreach (Dictionary<string, CMAttribute> group in attributes.Values)
            {
                foreach (CMAttribute attribute in group.Values)
                {
                    if (filter.Contains(attribute.Name))
                    {
                        if (attribute.ApplicableClasses != null)
                        {
                            foreach (CMClass cmClass in attribute.ApplicableClasses.Values)
                            {
                                classList.Add(cmClass);
                            }
                        }
                    }
                }
            }

            return classList;
        }
    }
}
