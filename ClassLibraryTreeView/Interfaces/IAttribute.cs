using System.Collections.Generic;
using System.Xml.Linq;

namespace ClassLibraryTreeView.Interfaces
{
    // Базовый класс для атрибутов
    public class IAttribute : IIdentifiable
    {
        public IAttribute()
        {
            Id = "unset";
            Name = "unset";
            Description = "unset";
            IsObsolete = false;
            SortOrder = "unset";
            Aspect = null;
            ClassOfMeasure = null;
            DataType = "Unset";
            Group = "Unset";
            Presence = "Unselect";
            ValidationType = "Unselect";
            ValidationRule = "unset";
            MinOccurs = 0;
            MaxOccurs = 0;
            MaturityLevels = null;
            Concept = "Unselect";
            Discipline = "unset";
            IsUoMRequired = false;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsObsolete { get; set; }
        public string SortOrder { get; set; }
        public List<string> Aspect { get; set; }
        // MeasureClass ClassOfMeasure { get; set; }
        string ClassOfMeasure { get; set; }
        public string DataType { get; set; }
        public string Group { get; set; }
        public string Presence { get; set; }
        public string ValidationType { get; set; }
        string ValidationRule { get; set; }
        int MinOccurs { get; set; }
        int MaxOccurs { get; set; }
        // List<MaturityLevel> MaturityLevels { get; set; }
        List<string> MaturityLevels { get; set; }
        public string Concept { get; set; }
        string Discipline { get; set; }
        bool IsUoMRequired { get; set; }

        public void Clone(IIdentifiable source)
        {
            throw new System.NotImplementedException();
        }

        public void Clone(XElement source)
        {
            throw new System.NotImplementedException();
        }

        public void Init()
        {
            throw new System.NotImplementedException();
        }
    }
}
