using System.Collections.Generic;
using System.Xml.Linq;

namespace ClassLibraryTreeView
{
    public class CMClass // : ICMElement
    {
        private Dictionary<string, string> attributes;
        private Dictionary<string, CMClass> descendants;
        private Dictionary<string, CMAttribute> permissibleAttributes;
        public int Depth{ get;}
        public string Xtype => attributes["xtype"];
        public string Attribute(string id)
        {
            if (attributes.ContainsKey(id))
            {
                return attributes[id];
            }
            return null;
        }
        public Dictionary<string, string> Attributes
        {
            get
            {
                if (attributes.Count > 0)
                {
                    return attributes;
                }
                return null;
            }
        }
        public Dictionary<string, CMClass> Descendants => descendants;
        public Dictionary<string, CMAttribute> PermissibleAttributes => permissibleAttributes;
        public CMClass Parent { get; set; }
        public string ParentId
        {
            get
            {
                string parentId = null;
                if (attributes.ContainsKey("extends"))
                {
                    parentId = attributes["extends"];
                    if (parentId.Equals("not found"))
                    {
                        parentId = null;
                    }
                }
                return parentId;
            }
        }
        public string Name
        {
            get
            {
                string name = null;
                if (attributes.ContainsKey("name"))
                {
                    name = attributes["name"];
                }
                return name;
            }
        }
        public string Id
        {
            get
            {
                string id = null;
                if (attributes.ContainsKey("id"))
                {
                    id = attributes["id"];
                }
                return id;
            }
        }
        public CMClass()
        {
            Init();
        }
        public CMClass(CMClass cmClass)
        {
            this.attributes = new Dictionary<string, string>(cmClass.attributes);
            this.descendants = new Dictionary<string, CMClass>(cmClass.descendants);
            this.permissibleAttributes = new Dictionary<string, CMAttribute>(cmClass.permissibleAttributes);
        }
        public CMClass(XElement element)
        {
            Init();
            foreach (XAttribute attribute in element.Attributes())
            {
                AddAttribute($"{attribute.Name.LocalName}", $"{attribute.Value}");
            }
            AddAttribute($"xtype", $"{element.Parent.Name.LocalName}");
            foreach (XElement child in element.Elements())
            {
                string childName = child.Name.LocalName;
                if (childName.Equals("Attributes"))
                {
                    foreach (XElement permissibleAttribute in child.Elements())
                    {
                        AddPermissibleAttribute(new CMAttribute(permissibleAttribute));
                    }
                }

                if (childName.Equals("Elements"))
                {
                    foreach (XElement descendant in child.Elements())
                    {
                        AddDescendant(new CMClass(descendant));
                    }
                }
            }
        }
        public bool Equals(CMClass cmClass)
        {
            return (this.attributes == cmClass.attributes && this.descendants == cmClass.descendants && this.permissibleAttributes == cmClass.permissibleAttributes);
        }
        private void Init()
        {
            attributes = new Dictionary<string, string>();
            descendants = new Dictionary<string, CMClass>();
            permissibleAttributes = new Dictionary<string, CMAttribute>();
        }
        public void Clone(CMClass cmClass)
        {
            this.attributes = cmClass.attributes;
            this.descendants = cmClass.descendants;
            this.permissibleAttributes = cmClass.permissibleAttributes;
        }
        public void Clear()
        {
            this.Attributes.Clear();
            this.Descendants.Clear();
            this.PermissibleAttributes.Clear();
        }
        public void AddPermissibleAttribute(CMAttribute cmAttribute)
        {
            permissibleAttributes.Add(cmAttribute.Attributes["id"], cmAttribute);
        }
        public void AddAttribute(string id, string value)
        {
            attributes.Add(id, value);
        }
        public void AddDescendant(Dictionary<string, CMClass> classes)
        {
            if (classes == null)
            {
                return;
            }
            if (classes.Count == 0)
            {
                return;
            }
            foreach(CMClass cmClass in classes.Values)
            {
                if (!this.descendants.ContainsKey(cmClass.Id))
                {
                    AddDescendant(cmClass);
                }
                AddDescendant(cmClass.Descendants);
            }
        }
        public void AddDescendant(CMClass cmClass)
        {
            if(descendants.ContainsKey(cmClass.Id))
            {
                CMClass descendant = descendants[cmClass.Id];

            }
            else
            {
                descendants.Add(cmClass.Id, cmClass);
            }
        }
        public static Dictionary<string, CMClass> FillClassMap(List<CMClass> source)
        {
            Dictionary<string, CMClass> map = new Dictionary<string, CMClass>();
            foreach (CMClass cmClass in source)
            {
                if (!map.ContainsKey(cmClass.Id))
                {
                    map.Add(cmClass.Id, cmClass);
                }
            }
            // set classes inheritance
            foreach (CMClass cmClass in map.Values)
            {
                string parentId = cmClass.ParentId;
                if (parentId != null)
                {
                    CMClass parent = CMClass.FindClass(parentId, map);
                    if (parent != null)
                    {
                        if (!parent.Descendants.ContainsKey(cmClass.Id))
                        {
                            parent.AddDescendant(cmClass);
                        }
                    }
                }
            }
            Dictionary<string, CMClass> mapNew = new Dictionary<string, CMClass>(map);
            foreach (CMClass cmClass in map.Values)
            {
                if (cmClass.ParentId != null)
                {
                    mapNew.Remove(cmClass.Id);
                }
            }
            return mapNew;
        }
        public static void FillClassList(XElement source, List<CMClass> list)
        {
            foreach (XElement element in source.Elements())
            {
                if (!element.Name.LocalName.ToLower().Equals("extension"))
                {
                    list.Add(new CMClass(element));
                }
            }
            // set classes inheritance
            /*
            foreach (CMClass cmClass in list)
            {
                string parentId = cmClass.ParentId;
                if (parentId != null)
                {
                    CMClass parent = CMClass.FindClass(parentId, map);
                    if (parent != null)
                    {
                        if (!parent.Descendants.ContainsKey(cmClass.Id))
                        {
                            parent.AddDescendant(cmClass);
                        }
                    }
                }
            }

            foreach (CMClass cmClass in map.Values)
            {
                string parentId = cmClass.ParentId;
                if (parentId != null)
                {
                    CMClass parent = CMClass.FindClass(parentId, map);
                    if (parent != null)
                    {
                        parent.AddDescendant(cmClass);
                    }
                }
            }
            Dictionary<string, CMClass> mapNew = new Dictionary<string, CMClass>(map);
            foreach (CMClass cmClass in map.Values)
            {
                if (cmClass.ParentId != null)
                {
                    mapNew.Remove(cmClass.Id);
                }
            }
            return mapNew;
            return list;
            */
        }
        public static void AddClasses(Dictionary<string, List<CMClass>> map, XElement element)
        {
            foreach (XElement child in element.Elements())
            {
                if (child.Name.LocalName.ToLower().Equals("extension"))
                {
                    continue;
                }

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

                if (id == null)
                {
                    continue;
                }

                if (!map.ContainsKey(id))
                {
                    map.Add(id, new List<CMClass>());
                }
                map[id].Add(new CMClass(cmClass));
            }

            DefineInheritance(map);
        }
        public static void DefineInheritance(Dictionary<string, List<CMClass>> map)
        {
            foreach (string id in map.Keys)
            {
                List<CMClass> cmClassList = map[id];
                foreach (CMClass cmClass in cmClassList)
                {
                    string parentId = cmClass.ParentId;

                    if (parentId == null)
                    {
                        continue;
                    }

                    if (!map.ContainsKey(parentId))
                    {
                        continue;
                    }

                    List<CMClass> parents = map[parentId];

                    for (int index = 0; index < parents.Count; index++)
                    {
                        parents[index].AddDescendant(new CMClass(cmClass));
                    }
                }
            }
        }
        public static void AddClasses(List<CMClass> list, XElement element)
        {
            foreach (XElement child in element.Elements())
            {
                if (!child.Name.LocalName.ToLower().Equals("extension"))
                {
                    list.Add(new CMClass(child));
                }
            }
            /*
            // set classes inheritance
            foreach (CMClass cmClass in map.Values)
            {
                string parentId = cmClass.ParentId;
                if (parentId != null)
                {
                    CMClass parent = CMClass.FindClass(parentId, map);
                    if (parent != null)
                    {
                        parent.AddDescendant(cmClass);
                    }
                }
            }
            Dictionary<string, CMClass> mapNew = new Dictionary<string, CMClass>(map);
            foreach (CMClass cmClass in map.Values)
            {
                if (cmClass.ParentId != null)
                {
                    mapNew.Remove(cmClass.Id);
                }
            }
            return mapNew;
            */
        }
        public static CMClass FindClass(string id, Dictionary<string, CMClass> map)
        {
            if (map == null)
            {
                return null;
            }
            foreach (CMClass value in map.Values)
            {
                if (value.Id.Equals(id))
                {
                    return value;
                }
                if (value.Descendants.Count == 0)
                {
                    continue;
                }
                foreach(CMClass descendant in value.Descendants.Values)
                {
                    if (descendant.Id.Equals(id))
                    {
                        return descendant;
                    }
                    if (descendant.Descendants.Count == 0)
                    {
                        continue;
                    }
                    CMClass cmClass = FindClass(id, descendant.Descendants);
                    if (cmClass != null)
                    {
                        return cmClass;
                    }
                }
            }
            return null;
        }
        private void CopyDescendants(CMClass source, CMClass recipient)
        {
            if (!source.Name.Equals(recipient.Name))
            {
                return;
            }
            foreach(CMClass cmClass in source.Descendants.Values)
            {
                if (!recipient.Descendants.ContainsKey(cmClass.Id))
                {
                    recipient.AddDescendant(cmClass);
                }
            }

            if (source.Descendants.Count == 0)
            {
                return;
            }
            foreach (CMClass cmClass in source.Descendants.Values)
            {
                CopyDescendants(cmClass, recipient);
            }
        }
        public void Merge(CMClass cmClass)
        {
            foreach (CMClass descendantSource in cmClass.Descendants.Values)
            {
                foreach (CMClass descendant in this.descendants.Values)
                {
                    CopyDescendants(descendantSource, descendant);
                }
            }
        }


    }
}
