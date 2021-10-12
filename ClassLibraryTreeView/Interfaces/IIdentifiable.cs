using System.Collections.Generic;
using System.Xml.Linq;

namespace ClassLibraryTreeView.Interfaces
{
    public interface IIdentifiable
    {
        string Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        bool IsObsolete { get; set; }
        string SortOrder { get; set; }
        List<string> Aspect { get; set; }
        KeyValuePair<string, string>[] Attributes();
        void Clone(IIdentifiable other);
        void Clone(XElement xElement);
        void Init();
    }
}
