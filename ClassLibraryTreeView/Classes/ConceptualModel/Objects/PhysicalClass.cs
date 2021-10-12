namespace ClassLibraryTreeView.Classes
{
    // Физический класс
    class PhysicalClass : ConceptualModelClass
    {
        public PhysicalClass() : base()
        {
            Discipline = "";
        }
        public string Discipline { get; set; }
    }
}
