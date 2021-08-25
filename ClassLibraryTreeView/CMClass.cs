using System.Collections.Generic;
using System.Xml.Linq;

namespace ClassLibraryTreeView
{
    public class CMClass // : ICMElement
    {
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
        public bool HasAttributes => Attributes != null;
        public bool HasDescendants => Descendants != null;
        public bool HasPermissibleAttributes => PermissibleAttributes != null;
        public Dictionary<string, CMClass> Descendants
        {
            get
            {
                if (descendants.Count > 0)
                {
                    return descendants;
                }
                return null;
            }
        }
        public Dictionary<string, CMAttribute> PermissibleAttributes
        {
            get
            {
                if (permissibleAttributes.Count > 0)
                {
                    return permissibleAttributes;
                }
                return null;
            }
        }

        private Dictionary<string, string> attributes;
        private Dictionary<string, CMClass> descendants;
        private Dictionary<string, CMAttribute> permissibleAttributes;
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
            permissibleAttributes.Add(cmAttribute.Attribute["id"], cmAttribute);
        }
        public void AddAttribute(string id, string value)
        {
            attributes.Add(id, value);
        }
        public void AddDescendant(Dictionary<string, CMClass> classes)
        {
            foreach(CMClass cmClass in classes.Values)
            {
                if (!this.descendants.ContainsKey(cmClass.Id))
                {
                    AddDescendant(cmClass);
                }
            }
        }
        public void AddDescendant(CMClass cmClass)
        {
            descendants.Add(cmClass.Id, cmClass);
        }
        public static Dictionary<string, CMClass> FillClassMap(XElement source)
        {
            Dictionary<string, CMClass> map = new Dictionary<string, CMClass>();
            foreach (XElement child in source.Elements())
            {
                if (!child.Name.LocalName.ToLower().Equals("extension"))
                {
                    map.Add(child.Attribute("id").Value, new CMClass(child));
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
        }
        public static CMClass FindClassByName(string name, Dictionary<string, CMClass> map)
        {
            if (map == null)
            {
                return null;
            }
            foreach (CMClass value in map.Values)
            {
                if (value.Name.Equals(name))
                {
                    return value;
                }
                if (!value.HasDescendants)
                {
                    continue;
                }
                foreach (CMClass descendant in value.Descendants.Values)
                {
                    if (descendant.Id.Equals(name))
                    {
                        return descendant;
                    }
                    if (!descendant.HasDescendants)
                    {
                        continue;
                    }
                    CMClass cmClass = FindClassByName(name, descendant.Descendants);
                    if (cmClass != null)
                    {
                        return cmClass;
                    }
                }
            }
            return null;
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
                if (!value.HasDescendants)
                {
                    continue;
                }
                foreach(CMClass descendant in value.Descendants.Values)
                {
                    if (descendant.Id.Equals(id))
                    {
                        return descendant;
                    }
                    if (!descendant.HasDescendants)
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
        public void Merge(CMClass cmClass)
        {
            if(!this.Id.Equals(cmClass.Id))
            {
                return;
            }
            foreach (CMClass descendant in cmClass.Descendants.Values)
            {
                if (!this.Descendants.ContainsKey(descendant.Id))
                {
                    this.AddDescendant(cmClass.Descendants[descendant.Id]);
                }
            }
        }
    }
}
