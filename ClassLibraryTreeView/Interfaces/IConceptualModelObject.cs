using System.Collections.Generic;
using System.Xml.Linq;

namespace ClassLibraryTreeView.Interfaces
{
    public interface IConceptualModelObject
    {
        string Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        bool IsObsolete { get; set; }
        string SortOrder { get; set; }
        List<string> Aspect { get; set; }
        KeyValuePair<string, string>[] Properties();
        Dictionary<string, string[]> PropertiesArrays();
        void Clone(IConceptualModelObject other);
        void Clone(XElement xElement);
        void Init();
    }
}
