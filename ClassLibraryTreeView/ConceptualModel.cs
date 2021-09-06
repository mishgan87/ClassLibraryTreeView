using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using ClassLibraryTreeView.Interfaces;
using ClassLibraryTreeView.Classes;
using System;

namespace ClassLibraryTreeView
{
    public class GridCell
    {
        public GridCell()
        {
            Text = "";
            StyleIndex = 0;
        }
        public GridCell(string text, uint style)
        {
            Text = text;
            StyleIndex = style;
        }
        public string Text { get; set; }
        public uint StyleIndex { get; set; }
    }
    public class ConceptualModel
    {
        // public Dictionary<string, IAttribute> attributes;
        public Dictionary<string, IClass> documents = new Dictionary<string, IClass>();
        public Dictionary<string, IClass> functionals = new Dictionary<string, IClass>();
        public Dictionary<string, IClass> physicals = new Dictionary<string, IClass>();

        public Dictionary<string, MeasureClass> measureClasses = new Dictionary<string, MeasureClass>();
        public Dictionary<string, MeasureUnit> measureUnits = new Dictionary<string, MeasureUnit>();
        public Dictionary<string, EnumerationList> enumerations = new Dictionary<string, EnumerationList>();

        public Dictionary<string, Dictionary<string, IAttribute>> attributes = new Dictionary<string, Dictionary<string, IAttribute>>();
        public List<IClass> merged = new List<IClass>();

        public int AttributesCount = 0;

        public int MaxDepth
        {
            get
            {
                int maxDepth = MapMaxDepth(functionals);
                int maxDepthaAuxiliary = MapMaxDepth(physicals);
                if (maxDepth < maxDepthaAuxiliary)
                {
                    maxDepth = maxDepthaAuxiliary;
                }
                return maxDepth;
            }
        }

        public ConceptualModel()
        {
            Init();
        }
        public void Init()
        {
            attributes = new Dictionary<string, Dictionary<string, IAttribute>>();
            documents = new Dictionary<string, IClass>();
            functionals = new Dictionary<string, IClass>();
            physicals = new Dictionary<string, IClass>();

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

            enumerations.Clear();
            measureUnits.Clear();
            measureClasses.Clear();

            merged.Clear();

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
                    result.Add(attribute);
                }
                parent = map[parent].Extends;
            }

            return result;
        }
        public string Presence(IClass cmClass, IAttribute attribute)
        {
            List<IAttribute> permissibleAttributes = PermissibleAttributes(cmClass);
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
                        }
                        
                        attributes[group].Add(newAttribute.Id, newAttribute);

                        AttributesCount++;
                    }
                }
            }
            SetInheritance(documents);
            SetInheritance(functionals);
            SetInheritance(physicals);
            MergeAndClean(physicals, functionals);
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
        private void MergeAndClean(Dictionary<string, IClass> source, Dictionary<string, IClass> recipient)
        {
            List<IClass> classes = new List<IClass>();
            foreach (IClass cmClass in recipient.Values)
            {
                classes.Add(cmClass);
                foreach (IClass sourceClass in source.Values)
                {
                    if (sourceClass.Name.Equals(cmClass.Name))
                    {
                        for (int index = 0; index < sourceClass.Children.Count; index++)
                        {
                            if (!cmClass.ContainsChildName(sourceClass.Children[index]))
                            {
                                classes.Add(sourceClass.Children[index]);
                                cmClass.Children.Add(sourceClass.Children[index]);
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
                    merged.Add(cmClass);
                    AddClassChildren(cmClass, merged);
                }
            }
        }
        public void AddClassChildren(IClass cmClass, List<IClass> classes)
        {
            foreach(IClass child in cmClass.Children)
            {
                classes.Add(child);
                AddClassChildren(child, classes);
            }
        }
        private void AddChildrenPresence(IClass cmClass, int maxDepth, IAttribute[] attributes, List<GridCell[]> grid)
        {
            if (cmClass.Children.Count > 0)
            {
                foreach (IClass child in cmClass.Children)
                {
                    AddPresence(child, maxDepth, attributes, grid);
                }
            }
        }
        private void AddPresence(IClass cmClass, int maxDepth, IAttribute[] attributes, List<GridCell[]> grid)
        {
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

            AddChildrenPresence(cmClass, maxDepth, attributes, grid);
        }
    }
}
