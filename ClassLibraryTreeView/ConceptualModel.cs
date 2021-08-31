using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using ClassLibraryTreeView.Interfaces;
using ClassLibraryTreeView.Classes;
using System;

namespace ClassLibraryTreeView
{
    public class ConceptualModel
    {
        // public Dictionary<string, IAttribute> attributes;
        public Dictionary<string, IClass> documents = new Dictionary<string, IClass>();
        public Dictionary<string, IClass> functionals = new Dictionary<string, IClass>();
        public Dictionary<string, IClass> physicals = new Dictionary<string, IClass>();

        public Dictionary<string, Dictionary<string, IAttribute>> attributes = new Dictionary<string, Dictionary<string, IAttribute>>();
        public List<IClass> merged = new List<IClass>();

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

            merged = new List<IClass>();
        }
        public void Clear()
        {
            attributes.Clear();
            documents.Clear();
            functionals.Clear();
            physicals.Clear();

            merged.Clear();
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
                result.AddRange(map[parent].PermissibleAttributes);
                parent = map[parent].Extends;
            }

            return result;
        }
        public string Presence(IClass cmClass, IAttribute attribute)
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
            string presence = "";
            foreach(IAttribute permissibleAttribute in cmClass.PermissibleAttributes)
            {
                if (permissibleAttribute.Id.Equals(attribute.Id))
                {
                    presence = "X";
                    if (!permissibleAttribute.Presence.Equals(""))
                    {
                        presence = permissibleAttribute.Presence;
                    }

                    if (presence.Equals("") && !cmClass.Extends.Equals(""))
                    {
                        IClass parent = map[cmClass.Extends];
                        if (!parent.Equals(""))
                        {
                            presence = Presence(parent, attribute);
                        }
                    }

                    break;
                }
            }

            if (presence.Length > 1)
            {
                presence = presence.Substring(0, 1);
            }
            return presence;
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
            int depth = 1;
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
            int maxDepth = 1;
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
        public async void ImportXml(string fileName)
        {
            Clear();
            XDocument doc = XDocument.Load(fileName);

            foreach (XElement element in doc.Elements().First().Elements())
            {
                string name = element.Name.LocalName;
                if (name.ToLower().Equals("functionals"))
                {
                    foreach (XElement child in element.Elements())
                    {
                        IClass newClass = new IClass(child);
                        functionals.Add(newClass.Id, newClass);
                    }
                }
                if (name.ToLower().Equals("physicals"))
                {
                    foreach (XElement child in element.Elements())
                    {
                        IClass newClass = new IClass(child);
                        physicals.Add(newClass.Id, newClass);
                    }
                }
                if (name.ToLower().Equals("documents"))
                {
                    foreach (XElement child in element.Elements())
                    {
                        IClass newClass = new IClass(child);
                        documents.Add(newClass.Id, newClass);
                    }
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
                    }
                }
            }
            SetInheritance(documents);
            SetInheritance(functionals);
            SetInheritance(physicals);
            MergeAndClean(physicals, functionals);
            // MergeClasses(physicals, functionals);
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
            merged.Clear();
            foreach (IClass cmClass in recipient.Values)
            {
                merged.Add(cmClass);
                foreach (IClass sourceClass in source.Values)
                {
                    if (sourceClass.Name.Equals(cmClass.Name))
                    {
                        for (int index = 0; index < sourceClass.Children.Count; index++)
                        {
                            if (!cmClass.ContainsChildName(sourceClass.Children[index]))
                            {
                                merged.Add(sourceClass.Children[index]);
                                cmClass.Children.Add(sourceClass.Children[index]);
                            }
                        }
                    }
                }
            }
        }
    }
}
