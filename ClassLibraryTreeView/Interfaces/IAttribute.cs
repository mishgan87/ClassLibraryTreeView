using System.Collections.Generic;
using System.Xml.Linq;

namespace ClassLibraryTreeView.Interfaces
{
    // Базовый класс для атрибутов
    public class IAttribute : IIdentifiable
    {
        // Interface members
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsObsolete { get; set; }
        public string SortOrder { get; set; }
        public List<string> Aspect { get; set; }
        // Attribute members
        // MeasureClass ClassOfMeasure { get; set; }
        string ClassOfMeasure { get; set; }
        public string DataType { get; set; }
        public string Group { get; set; }
        public string Presence { get; set; }
        public string ValidationType { get; set; }
        string ValidationRule { get; set; }
        string MinOccurs { get; set; }
        string MaxOccurs { get; set; }
        // List<MaturityLevel> MaturityLevels { get; set; }
        public List<string> MaturityLevels { get; set; }
        public string Concept { get; set; }
        string Discipline { get; set; }
        public bool IsUoMRequired { get; set; }
        public IAttribute()
        {
            Init();
        }
        public IAttribute(IAttribute source)
        {
            Clone(source);
        }
        public IAttribute(XElement source)
        {
            Clone(source);
        }
        public bool Equals(IIdentifiable source)
        {
            if (!Id.Equals(source.Id)
                || !Name.Equals(source.Name)
                || !Description.Equals(source.Description)
                || !IsObsolete.Equals(source.IsObsolete)
                || !SortOrder.Equals(source.SortOrder)
                || !Aspect.Equals(source.Aspect) )
            {
                return false;
            }

            IAttribute someAttribute = (IAttribute)source;

            if (!ClassOfMeasure.Equals(someAttribute.ClassOfMeasure)
                || !DataType.Equals(someAttribute.DataType)
                || !Group.Equals(someAttribute.Group)
                || !Presence.Equals(someAttribute.Presence)
                || !ValidationType.Equals(someAttribute.ValidationType)
                || !ValidationRule.Equals(someAttribute.ValidationRule)
                || !MinOccurs.Equals(someAttribute.MinOccurs)
                || !MaxOccurs.Equals(someAttribute.MaxOccurs)
                || !MaturityLevels.Equals(someAttribute.MaturityLevels)
                || !Concept.Equals(someAttribute.Concept)
                || !Discipline.Equals(someAttribute.Discipline)
                || !IsUoMRequired.Equals(someAttribute.IsUoMRequired) )
            {
                return false;
            }
            return true;
        }
        public void Clone(IIdentifiable source)
        {
            Id = source.Id;
            Name = source.Name;
            Description = source.Description;
            IsObsolete = source.IsObsolete;
            SortOrder = source.SortOrder;
            Aspect = new List<string>(source.Aspect);

            IAttribute someAttribute = (IAttribute)source;

            ClassOfMeasure = someAttribute.ClassOfMeasure;
            DataType = someAttribute.DataType;
            Group = someAttribute.Group;
            Presence = someAttribute.Presence;
            ValidationType = someAttribute.ValidationType;
            ValidationRule = someAttribute.ValidationRule;
            MinOccurs = someAttribute.MinOccurs;
            MaxOccurs = someAttribute.MaxOccurs;
            MaturityLevels = new List<string>(someAttribute.MaturityLevels);
            Concept = someAttribute.Concept;
            Discipline = someAttribute.Discipline;
            IsUoMRequired = someAttribute.IsUoMRequired;
        }

        public void Clone(XElement source)
        {
            Init();
            foreach (XAttribute attribute in source.Attributes())
            {
                string name = attribute.Name.LocalName.ToLower();
                switch (name)
                {
                    case "name":
                        Name = $"{attribute.Value}";
                        break;
                    case "id":
                        Id = $"{attribute.Value}";
                        break;
                    case "description":
                        Description = $"{attribute.Value}";
                        break;
                    case "isobsolete":
                        IsObsolete = false;
                        if (attribute.Value.Equals("true"))
                        {
                            IsObsolete = true;
                        }
                        break;
                    case "sortorder":
                        SortOrder = $"{attribute.Value}";
                        break;
                    case "aspect":
                        Aspect.Add($"{attribute.Value}");
                        break;

                    case "classofmeasure":
                        ClassOfMeasure = $"{attribute.Value}";
                        break;
                    case "datatype":
                        DataType = $"{attribute.Value}";
                        break;
                    case "groupid":
                        Group = $"{attribute.Value}";
                        break;
                    case "presence":
                        Presence = $"{attribute.Value}";
                        break;
                    case "validationtype":
                        ValidationType = $"{attribute.Value}";
                        break;
                    case "validationrule":
                        ValidationRule = $"{attribute.Value}";
                        break;
                    case "minoccurs":
                        MinOccurs = $"{attribute.Value}";
                        break;
                    case "maxoccurs":
                        MaxOccurs = $"{attribute.Value}";
                        break;
                    case "maturitylevel":
                        MaturityLevels.Add($"{attribute.Value}");
                        break;
                    case "concept":
                        Concept = $"{attribute.Value}";
                        break;
                    case "discipline":
                        Discipline = $"{attribute.Value}";
                        break;
                    case "isuomrequired":
                        IsUoMRequired = false;
                        if (attribute.Value.Equals("true"))
                        {
                            IsUoMRequired = true;
                        }
                        break;

                    default:
                        break;
                }
            }
        }

        public void Init()
        {
            Id = "";
            Name = "";
            Description = "";
            IsObsolete = false;
            SortOrder = "";
            Aspect = new List<string>();

            ClassOfMeasure = null;
            DataType = "";
            Group = "";
            Presence = "";
            ValidationType = "";
            ValidationRule = "";
            MinOccurs = "";
            MaxOccurs = "";
            MaturityLevels = new List<string>();
            Concept = "";
            Discipline = "";
            IsUoMRequired = false;
        }
    }
}
