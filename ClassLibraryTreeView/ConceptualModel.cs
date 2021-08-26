using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ClassLibraryTreeView
{
    public class ConceptualModel
    {
        public List<CMClass> classes;
        public List<string> classesTypes;
        public List<CMAttribute> attributes;

        public Dictionary<string, CMClass> func;
        public Dictionary<string, CMClass> phys;
        public Dictionary<string, CMClass> docs;

        public int MaxDepth
        {
            get
            {
                int maxDepth = 1;
                if (func.Count > 0)
                {
                    foreach (CMClass cmClass in func.Values)
                    {
                        int depth = cmClass.Depth;
                        if (depth > maxDepth)
                        {
                            maxDepth = depth;
                        }
                    }
                }
                if (phys.Count > 0)
                {
                    foreach (CMClass cmClass in phys.Values)
                    {
                        int depth = cmClass.Depth;
                        if (depth > maxDepth)
                        {
                            maxDepth = depth;
                        }
                    }
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
            func = new Dictionary<string, CMClass>();
            phys = new Dictionary<string, CMClass>();
            docs = new Dictionary<string, CMClass>();

            classes = new List<CMClass>();
            classesTypes = new List<string>();
            attributes = new List<CMAttribute>();
        }
        public void Clear()
        {
            func.Clear();
            phys.Clear();
            docs.Clear();

            classes.Clear();
            attributes.Clear();
            classesTypes.Clear();

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
                    AddClass(element, func);
                }
                if (name.ToLower().Equals("physicals"))
                {
                    AddClass(element, phys);
                }
                if (name.ToLower().Equals("documents"))
                {
                    AddClass(element, docs);
                }
                if (name.ToLower().Equals("attributes"))
                {
                    CMAttribute.AddAttributes(attributes, element);
                }
            }
            SetInheritance(func);
            SetInheritance(docs);
            SetInheritance(phys);
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
        public CMClass GetClass(string id, string xtype)
        {
            CMClass cmClass = null;
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
            return cmClass;
        }
    }
}
