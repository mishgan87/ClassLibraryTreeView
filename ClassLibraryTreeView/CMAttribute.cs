using System.Collections.Generic;
using System.Xml.Linq;

namespace ClassLibraryTreeView
{
    public class CMAttribute
    {
        private Dictionary<string, string> attributes;
        private Dictionary<string, CMClass> permissibleClasses;
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
        public Dictionary<string, CMClass> PermissibleClasses
        {
            get
            {
                if (permissibleClasses.Count > 0)
                {
                    return permissibleClasses;
                }
                return null;
            }
        }
        public CMAttribute()
        {
            Init();
        }
        public CMAttribute(XElement source)
        {
            Init();
            AddAttribute(source);
        }
        public void Init()
        {
            attributes = new Dictionary<string, string>();
            permissibleClasses = new Dictionary<string, CMClass>();
        }
        public void Clear()
        {
            attributes.Clear();
            permissibleClasses.Clear();
        }
        public void AddPermissibleClass(CMClass cmClass)
        {
            permissibleClasses.Add(cmClass.Id, cmClass);
        }
        public void AddAttribute(string id, string value)
        {
            attributes.Add(id, value);
        }
        public void AddAttribute(XElement source)
        {
            foreach (XAttribute attribute in source.Attributes())
            {
                attributes.Add($"{attribute.Name.LocalName}", $"{attribute.Value}");
            }
        }
        // public static void AddAttributes(Dictionary<string, CMAttribute> map, XElement element)
        public static void AddAttributes(List<CMAttribute> list, XElement element)
        {
            foreach(XElement attribute in element.Elements())
            {
                list.Add(new CMAttribute(attribute));
            }
        }
    }
}
