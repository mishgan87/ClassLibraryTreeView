using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using ClassLibraryTreeView.Interfaces;

namespace ClassLibraryTreeView
{
    public class ConceptualModel
    {
        /*
        public List<CMAttribute> attributes;
        public Dictionary<string, CMClass> func;
        public Dictionary<string, CMClass> phys;
        public Dictionary<string, CMClass> docs;
        */
        public Dictionary<string, IAttribute> attributes;
        public Dictionary<string, IClass> documents = new Dictionary<string, IClass>();
        public Dictionary<string, IClass> functionals = new Dictionary<string, IClass>();
        public Dictionary<string, IClass> physicals = new Dictionary<string, IClass>();

        public int MaxDepth
        {
            get
            {
                int maxDepth = ConceptualModel.MapMaxDepth(functionals);
                int maxDepthaAuxiliary = ConceptualModel.MapMaxDepth(physicals);
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
            attributes = new Dictionary<string, IAttribute>();
            documents = new Dictionary<string, IClass>();
            functionals = new Dictionary<string, IClass>();
            physicals = new Dictionary<string, IClass>();
        }
        public void Clear()
        {
            attributes.Clear();
            documents.Clear();
            functionals.Clear();
            physicals.Clear();
        }
        public static string AttributePresence(string id, string defaultPresence, IClass cmClass, Dictionary<string, IClass> map)
        {
            string presence = defaultPresence;
            for (int index = 0; index < cmClass.PermissibleAttributes.Count; index++)
            {
                IAttribute attribute = cmClass.PermissibleAttributes[index];
                if (attribute.Id.Equals(id))
                {
                    presence = "X";
                    if (!attribute.Presence.Equals(""))
                    {
                        presence = attribute.Presence;
                    }
                }
            }
            if ( (presence.Equals("") || presence.Equals("X")) && (cmClass.Extends != null) )
            {
                IClass parent = map[cmClass.Extends];
                if (parent != null)
                {
                    presence = ConceptualModel.AttributePresence(id, presence, parent, map);
                }
            }
            if (presence.Length > 1)
            {
                presence = presence.Substring(0, 1);
            }
            return presence;
        }
        public static int ClassDepth(IClass cmClass, Dictionary<string, IClass> map)
        {
            if (map.Count == 0)
            {
                return 0;
            }
            int depth = 1;
            string parent = cmClass.Extends;
            while (parent != null)
            {
                depth++;
                parent = map[parent].Extends;
            }
            return depth;
        }
        public static int MapMaxDepth(Dictionary<string, IClass> map)
        {
            if (map.Count == 0)
            {
                return 0;
            }
            int maxDepth = 1;
            foreach (IClass cmClass in map.Values)
            {
                int depth = ConceptualModel.ClassDepth(cmClass, map);
                if (depth > maxDepth)
                {
                    maxDepth = depth;
                }
            }
            return maxDepth;
        }
        public void ImportXml(string fileName)
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

                    // AddClass(element, func);
                }
                if (name.ToLower().Equals("physicals"))
                {
                    foreach (XElement child in element.Elements())
                    {
                        IClass newClass = new IClass(child);
                        physicals.Add(newClass.Id, newClass);
                    }
                    // AddClass(element, phys);
                }
                if (name.ToLower().Equals("documents"))
                {
                    foreach (XElement child in element.Elements())
                    {
                        IClass newClass = new IClass(child);
                        documents.Add(newClass.Id, newClass);
                    }
                    // AddClass(element, docs);
                }
                if (name.ToLower().Equals("attributes"))
                {
                    foreach (XElement child in element.Elements())
                    {
                        IAttribute newAttribute = new IAttribute(child);
                        attributes.Add(newAttribute.Id, newAttribute);
                    }
                    // CMAttribute.AddAttributes(attributes, element);
                }
            }
            SetInheritance(documents);
            SetInheritance(functionals);
            SetInheritance(physicals);
            MergeClasses(physicals, functionals);
            /*
            SetInheritance(func);
            SetInheritance(docs);
            SetInheritance(phys);
            MergeClasses(phys, func);
            */
        }
        private void SetInheritance(Dictionary<string, IClass> map)
        {
            if(map.Count > 0)
            {
                foreach (IClass cmClass in map.Values)
                {
                    if (cmClass.Extends != null)
                    {
                        map[cmClass.Extends].Children.Add(cmClass);
                    }
                }
            }
        }
        private void AddClass(XElement element, Dictionary<string, CMClass> map)
        {
            foreach (XElement child in element.Elements())
            {
                if (!child.Name.LocalName.ToLower().Equals("extension"))
                {
                    string id = null;
                    CMClass cmClass = new CMClass(child);

                    if (cmClass.Attributes.ContainsKey("id"))
                    {
                        id = cmClass.Id;
                    }
                    else
                    {
                        if (cmClass.Attributes.ContainsKey("name"))
                        {
                            id = cmClass.Name;
                        }
                    }

                    if (id != null)
                    {
                        map.Add(id, new CMClass(cmClass));
                    }
                }
            }
        }
        private void SetInheritance(Dictionary<string, CMClass> map)
        {
            foreach (CMClass cmClass in map.Values)
            {
                if (!cmClass.HasParent(map))
                {
                    continue;
                }

                string parentId = cmClass.ParentId;
                CMClass parent = map[parentId];
                cmClass.Parent = parent;
                parent.AddDescendant(cmClass);
            }
        }
        public IClass GetIClass(string id, string xtype)
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
        public CMClass GetClass(string id, string xtype)
        {
            CMClass cmClass = null;
            /*
            if (xtype.ToLower().Equals("functionals"))
            {
                cmClass = func[id];
            }
            if (xtype.ToLower().Equals("physicals"))
            {
                cmClass = phys[id];
            }
            if (xtype.ToLower().Equals("documents"))
            {
                cmClass = docs[id];
            }
            */
            return cmClass;
        }
        private void MergeClasses(Dictionary<string, IClass> source, Dictionary<string, IClass> recipient)
        {
            if (source.Count > 0)
            {
                foreach (IClass sourceClass in source.Values)
                {
                    foreach (IClass cmClass in recipient.Values)
                    {
                        if(sourceClass.Name.Equals(cmClass.Name))
                        {
                            for(int index = 0; index < sourceClass.Children.Count; index++)
                            {
                                if (!cmClass.ContainsChild(sourceClass.Children[index]))
                                {
                                    cmClass.Children.Add(sourceClass.Children[index]);
                                }
                            }
                        }
                    }
                }
            }
        }
        private void MergeClasses(Dictionary<string, CMClass> source, Dictionary<string, CMClass> recipient)
        {
            foreach (CMClass cmClass in recipient.Values)
            {
                CMClass sameClass = null;
                foreach (CMClass cmSourceClass in source.Values)
                {
                    if (cmClass.Name.Equals(cmSourceClass.Name))
                    {
                        sameClass = cmSourceClass;
                        break;
                    }
                }
                if (sameClass != null)
                {
                    for(int index = 0; index < sameClass.Descendants.Count; index++)
                    {
                        cmClass.AddDescendant(sameClass.Descendants[index]);
                    }
                }
            }
        }
    }
}
