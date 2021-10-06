using System.Xml.Linq;

namespace ClassLibraryTreeView.Classes
{
    // Функциональный класс
    class FunctionalClass : CMClass
    {
        public FunctionalClass() : base()
        {
            Discipline = "";
        }

        public FunctionalClass(CMClass source) : base(source)
        {
        }

        public FunctionalClass(XElement source) : base(source)
        {
        }

        public string Discipline { get; set; }
    }
}
