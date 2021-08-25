using System.Collections.Generic;
using System.Xml.Linq;

namespace ClassLibraryTreeView
{
    public class CMClass // : ICMElement
    {
        public Dictionary<string, string> Attribute
        {
            get
            {
                if (attribute.Count > 0)
                {
                    return attribute;
                }
                return null;
            }
        }
        public Dictionary<string, CMClass> Descendant
        {
            get
            {
                if (descendant.Count > 0)
                {
                    return descendant;
                }
                return null;
            }
        }
        public Dictionary<string, CMAttribute> PermissibleAttribute
        {
            get
            {
                if (permissibleAttribute.Count > 0)
                {
                    return permissibleAttribute;
                }
                return null;
            }
        }

        private Dictionary<string, string> attribute;
        private Dictionary<string, CMClass> descendant;
        private Dictionary<string, CMAttribute> permissibleAttribute;
        public string ParentId
        {
            get
            {
                string parentId = null;
                if (Attribute.ContainsKey("extends"))
                {
                    parentId = Attribute["extends"];
                    if (parentId.Equals("not found"))
                    {
                        parentId = null;
                    }
                }
                return parentId;
            }
        }
        public string Id
        {
            get
            {
                string id = null;
                if (Attribute.ContainsKey("id"))
                {
                    id = Attribute["id"];
                }
                return id;
            }
        }
        public CMClass()
        {
            Init();
        }
        public CMClass(XElement element)
        {
            Init();
            CMAttribute.CopyAttributeX(this.Attribute, element.Attributes());
            foreach (XElement child in element.Elements())
            {
                string childName = child.Name.LocalName;
                if (childName.Equals("Attributes"))
                {
                    foreach (XElement permissibleAttribute in child.Elements())
                    {
                        this.PermissibleAttribute.Add(new CMAttribute(permissibleAttribute));
                    }
                }

                if (childName.Equals("Elements"))
                {
                    foreach (XElement descendant in child.Elements())
                    {
                        this.Descendant.Add(descendant.Attribute("id").Value, new CMClass(descendant));
                    }
                }
            }
        }
        private void Init()
        {
            attribute = new Dictionary<string, string>();
            descendant = new Dictionary<string, CMClass>();
            permissibleAttribute = new Dictionary<string, CMAttribute>();
        }
        public void Clone(CMClass cmClass)
        {
            this.attribute = cmClass.attribute;
            this.descendant = cmClass.descendant;
            this.permissibleAttribute = cmClass.permissibleAttribute;
        }
        public void Clear()
        {
            this.Attribute.Clear();
            this.Descendant.Clear();
            this.PermissibleAttribute.Clear();
        }
        public void AddPermissibleAttribute(CMAttribute cmAttribute)
        {
            permissibleAttribute.Add(cmAttribute.Attribute["id"], cmAttribute);
        }
        public void AddAttribute(string id, string value)
        {
            attribute.Add(id, value);
        }
        public void AddDescendant(CMClass cmClass)
        {
            descendant.Add(cmClass.Attribute["id"], cmClass);
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
            Dictionary<string, CMClass> newMap = new Dictionary<string, CMClass>(map);
            foreach (CMClass cmClass in map.Values)
            {
                string parentId = cmClass.ParentId;
                if (parentId != null)
                {
                    CMClass parent = CMClass.FindClass(parentId, newMap);
                    if (parent != null)
                    {
                        parent.AddDescendant(cmClass);
                        newMap.Remove(cmClass.Id);
                    }
                }
            }
            return newMap;
        }
        public static CMClass FindClass(string classId, Dictionary<string, CMClass> classMap)
        {
            CMClass cmClass;
            if (classMap.TryGetValue(classId, out cmClass))
            {
                return cmClass;
            }
            else
            {
                
                return null;
            }
        }
    }
}
