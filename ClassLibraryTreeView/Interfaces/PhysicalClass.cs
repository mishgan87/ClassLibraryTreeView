namespace ClassLibraryTreeView.Interfaces
{
    // Физический класс
    class PhysicalClass : IClass
    {
        public PhysicalClass() : base()
        {
            Discipline = "";
        }
        public string Discipline { get; set; }
    }
}
