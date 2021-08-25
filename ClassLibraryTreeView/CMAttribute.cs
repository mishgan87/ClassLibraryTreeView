using System.Collections.Generic;
using System.Xml.Linq;

namespace ClassLibraryTreeView
{
    public class CMAttribute : ICMElement
    {
        public CMAttribute()
        {
            Attribute = new Dictionary<string, string>();
        }
        public CMAttribute(XElement source)
        {
            Attribute = new Dictionary<string, string>();
            foreach (XAttribute attribute in source.Attributes())
            {
                Attribute.Add($"{attribute.Name.LocalName}", $"{attribute.Value}");
            }
        }
        public Dictionary<string, string> Attribute { get; set; }
        public static void CopyAttributeX(Dictionary<string, string> recipient, IEnumerable<XAttribute> source)
        {
            foreach (XAttribute attribute in source)
            {
                recipient.Add($"{attribute.Name.LocalName}", $"{attribute.Value}");
            }
        }
    }
}
