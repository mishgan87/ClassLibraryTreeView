namespace ClassLibraryTreeView.Classes
{
    // Физический класс
    class PhysicalClass : CMClass
    {
        public PhysicalClass() : base()
        {
            Discipline = "";
        }
        public string Discipline { get; set; }
    }
}
