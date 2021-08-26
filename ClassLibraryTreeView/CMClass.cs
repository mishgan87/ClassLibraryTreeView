using System.Collections.Generic;
using System.Xml.Linq;

namespace ClassLibraryTreeView
{
    public class CMClass // : ICMElement
    {
        private Dictionary<string, string> attributes;
        private List<CMClass> descendants;
        private List<CMAttribute> permissibleAttributes;
        public int Depth
        {
            get
            {
                int depth = 1;
                CMClass parent = Parent;
                while (parent != null)
                {
                    depth++;
                    parent = parent.Parent;
                }
                return depth;
            }
        }
        public string Presence(string attributeId, string defaultPresence)
        {
            string presence = defaultPresence;
            for(int index = 0; index < permissibleAttributes.Count; index++)
            {
                CMAttribute attribute = permissibleAttributes[index];
                if (attribute.Id.Equals(attributeId))
                {
                    presence = "X";
                    if (attribute.Attributes.ContainsKey("presence"))
                    {
                        if (!attribute.Attributes["presence"].Equals(""))
                        {
                            presence = attribute.Attributes["presence"].Substring(0, 1);
                        }
                    }
                }
            }
            if (presence.Equals("") || presence.Equals("X"))
            {
                CMClass parent = Parent;
                if (parent != null)
                {
                    presence = parent.Presence(attributeId, presence);
                }
            }
            return presence;
        }
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
        public List<CMClass> Descendants => descendants;
        public List<CMAttribute> PermissibleAttributes
        {
            get
            {
                List<CMAttribute> list = new List<CMAttribute>();

                for (int index = 0; index < permissibleAttributes.Count; index++)
                {
                    list.Add(permissibleAttributes[index]);
                }

                CMClass parent = Parent;

                while (parent != null)
                {
                    for (int index = 0; index < parent.PermissibleAttributes.Count; index++)
                    {
                        list.Add(Parent.PermissibleAttributes[index]);
                    }
                    parent = parent.Parent;
                }

                return list;
            }
        }
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
            this.descendants = new List<CMClass>(cmClass.descendants);
            this.permissibleAttributes = new List<CMAttribute>(cmClass.permissibleAttributes);
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
            }
        }
        public bool Equals(CMClass cmClass)
        {
            return (this.attributes.Equals(cmClass.attributes)
                    && this.descendants.Equals(cmClass.descendants)
                    && this.permissibleAttributes.Equals(cmClass.permissibleAttributes)
                    && this.Parent.Equals(cmClass.Parent));
        }
        private void Init()
        {
            Parent = null;
            attributes = new Dictionary<string, string>();
            descendants = new List<CMClass>();
            permissibleAttributes = new List<CMAttribute>();
        }
        public void Clone(CMClass cmClass)
        {
            Parent = cmClass.Parent;
            this.attributes = cmClass.attributes;
            this.descendants = cmClass.descendants;
            this.permissibleAttributes = cmClass.permissibleAttributes;
        }
        public void Clear()
        {
            Parent = null;
            this.Attributes.Clear();
            this.Descendants.Clear();
            this.PermissibleAttributes.Clear();
        }
        public void AddPermissibleAttribute(CMAttribute cmAttribute)
        {
            permissibleAttributes.Add(cmAttribute);
        }
        public void AddAttribute(string id, string value)
        {
            attributes.Add(id, value);
        }
        public void AddDescendant(CMClass cmClass)
        {
            descendants.Add(cmClass);
        }
        public bool HasParent(Dictionary<string, CMClass> map)
        {
            string parentId = this.ParentId;

            if (parentId == null)
            {
                return false;
            }

            if (!map.ContainsKey(parentId))
            {
                return false;
            }

            return true;
        }
    }
}
