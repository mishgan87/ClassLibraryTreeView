using System.Xml.Linq;

namespace ClassLibraryTreeView.Classes
{
    // Функциональный класс
    class FunctionalClass : ConceptualModelClass
    {
        public FunctionalClass() : base()
        {
            Discipline = "";
        }

        public FunctionalClass(ConceptualModelClass source) : base(source)
        {
        }

        public FunctionalClass(XElement source) : base(source)
        {
        }

        public string Discipline { get; set; }
    }
}
