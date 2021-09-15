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

namespace ClassLibraryTreeView
{
    public enum GridCellMergeProperty
    {
        NoMerging = 0,
        MergingStart = 1,
        MergingFinish = 2
    }
    public class GridCell
    {
        public GridCell()
        {
            Text = "";
            StyleIndex = 0;
            Reference = "";
            Merge = 0;
        }
        public GridCell(string text, uint style, string reference, GridCellMergeProperty merge)
        {
            Text = text;
            StyleIndex = style;
            Reference = reference;
            Merge = merge;
        }
        public string Text { get; set; }
        public uint StyleIndex { get; set; }
        public string Reference { get; set; }
        public GridCellMergeProperty Merge { get; set; }
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
        private List<string> attributesGroupList = new List<string>();

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
            if(map == null)
            {
                return null;
            }
            List<IAttribute> result = new List<IAttribute>();
            result.AddRange(cmClass.PermissibleAttributes);

            string parent = cmClass.Extends;
            while (!parent.Equals(""))
            {
                foreach(IAttribute parentAttribute in map[parent].PermissibleAttributes)
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
            switch(classConcept)
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
            if(map.Count > 0)
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
            foreach(IClass child in cmClass.Children)
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
        public List<GridCell> ClassAttributesPermissions(IClass cmClass)
        {
            int colCount = maxDepth + AttributesCount + 2;
            List<GridCell> gridCells = new List<GridCell>();
            for (int col = 0; col < colCount; col++)
            {
                string text = "";
                uint styleIndex = 0;
                GridCellMergeProperty merge = GridCellMergeProperty.NoMerging;

                int classDepth = ClassDepth(cmClass);

                if (col == classDepth) // class name
                {
                    text = $"{cmClass.Name}";
                    styleIndex = 1;
                    merge = GridCellMergeProperty.MergingStart;
                }
                if (col == maxDepth - 1) // class
                {
                    if (classDepth != maxDepth - 1)
                    {
                        merge = GridCellMergeProperty.MergingFinish;
                    }
                }

                if (col == maxDepth) // class discipline
                {
                    // text = $"{cmClass}";
                    styleIndex = 2;
                }

                if (col == maxDepth + 1) // class id
                {
                    text = $"{cmClass.Id}";
                    styleIndex = 2;
                }

                if (col > maxDepth + 2)
                {
                    string presence = Presence(cmClass, attributesList.ElementAt(col - maxDepth - 2));
                    switch (presence)
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
                    text = presence;
                }

                gridCells.Add(new GridCell(text, styleIndex, "", merge));
            }

            return gridCells;
        }
        public List<List<GridCell>> GetPermissibleGrid(MergeCells mergeCells)
        {
            int rowCount = merged.Count + 3;
            int colCount = maxDepth + AttributesCount + 2;

            List<List<GridCell>> grid = new List<List<GridCell>>();

            IEnumerable<List<GridCell>> query = null;
            try
            {
                query = merged
                .AsParallel()
                .AsSequential()
                .Select(ClassAttributesPermissions);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            for (int row = 0; row < 3; row++)
            {
                List<GridCell> list = new List<GridCell>();
                for (int col = 0; col < colCount; col++)
                {
                    uint style = 0;
                    string text = "";
                    GridCellMergeProperty merge = GridCellMergeProperty.NoMerging;

                    if (row == 0 && col == 0)
                    {
                        merge = GridCellMergeProperty.MergingStart;
                    }

                    if (row == 1 && col == maxDepth + 1)
                    {
                        merge = GridCellMergeProperty.MergingFinish;
                    }

                    if (row == 2)
                    {
                        if (col == 0)
                        {
                            style = 9;
                            text = $"Classes ({merged.Count})";
                            merge = GridCellMergeProperty.MergingStart;
                        }

                        if (col == maxDepth - 1)
                        {
                            merge = GridCellMergeProperty.MergingFinish;
                        }

                        if (col == maxDepth)
                        {
                            style = 0;
                            text = $"Discipline";
                        }

                        if (col == maxDepth + 1)
                        {
                            style = 2;
                            text = $"Class ID";
                        }
                    }

                    if (col >= maxDepth + 2)
                    {
                        if (row == 0)
                        {
                            style = 0;
                            text = attributesList.ElementAt(col - maxDepth - 2).Group;
                        }
                        if (row == 1)
                        {
                            style = 3;
                            text = $"{attributesList.ElementAt(col - maxDepth - 2).Id} : {attributesList.ElementAt(col - maxDepth - 2).Name}";
                        }
                    }

                    list.Add(new GridCell(text, style, CellName(row, col), merge));
                }
                grid.Add(list);
            }

            grid.AddRange(query);

            return grid;
        }
    }
}
