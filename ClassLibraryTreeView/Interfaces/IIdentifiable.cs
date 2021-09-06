using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ClassLibraryTreeView.Interfaces
{
    public enum IIdentifiablePresence : ushort
    {
        Unselect = 0,
        NotApplicable = 1,
        Optional = 2,
        Preferred = 3,
        Required = 4
    }
    public enum IIdentifiableConcept
    {
        Unselect = 0,
        Functional = 1,
        Physical = 2,
        General = 4,
        Document = 4
    }
    public enum IIdentifiableDataValidationType : ushort
    {
        Unselect = 0,
        Enumeration = 1,
        ValueRangeInclusive = 2,
        RegularExpression = 3,
        Association = 4
    }
    // Интерфейс с базовым набором параметров
    public interface IIdentifiable
    {
        string Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        bool IsObsolete { get; set; }
        string SortOrder { get; set; }
        List<string> Aspect { get; set; }
        KeyValuePair<string, string>[] Attributes();
        bool Equals(IIdentifiable source);
        void Clone(IIdentifiable source);
        void Clone(XElement source);
        void Init();
    }
}
