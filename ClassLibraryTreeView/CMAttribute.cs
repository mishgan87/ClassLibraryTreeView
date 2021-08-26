using System.Collections.Generic;
using System.Xml.Linq;

namespace ClassLibraryTreeView
{
    public class CMAttribute
    {
        private Dictionary<string, string> attributes;
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
        public string Id => attributes["id"];
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
        }
        public void Clear()
        {
            attributes.Clear();
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
