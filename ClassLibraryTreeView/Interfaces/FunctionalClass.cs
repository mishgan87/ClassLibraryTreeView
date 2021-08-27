namespace ClassLibraryTreeView.Interfaces
{
    // Функциональный класс
    class FunctionalClass : IClass
    {
        public FunctionalClass() : base()
        {
            Discipline = "";
        }
        public string Discipline { get; set; }
    }
}
