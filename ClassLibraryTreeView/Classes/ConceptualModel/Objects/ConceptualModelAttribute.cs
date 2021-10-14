using ClassLibraryTreeView.Interfaces;
using System.Collections.Generic;
using System.Xml.Linq;

namespace ClassLibraryTreeView.Classes
{
    public class ConceptualModelAttribute : ConceptualModelObject
    {
        public ConceptualModelAttribute()
        {
            Init();
        }

        public ConceptualModelAttribute(ConceptualModelObject other)
        {
            Clone(other);
        }

        public ConceptualModelAttribute(XElement xElement)
        {
            Clone(xElement);
        }
        public override void Clone(IConceptualModelObject other)
        {
            base.Clone(other);
            ConceptualModelAttribute otherAttribute = (ConceptualModelAttribute)other;

            ClassOfMeasure = otherAttribute.ClassOfMeasure;
            DataType = otherAttribute.DataType;
            Group = otherAttribute.Group;
            Presence = otherAttribute.Presence;
            ValidationType = otherAttribute.ValidationType;
            ValidationRule = otherAttribute.ValidationRule;
            MinOccurs = otherAttribute.MinOccurs;
            MaxOccurs = otherAttribute.MaxOccurs;
            MaturityLevels = new List<string>(otherAttribute.MaturityLevels);
            Concept = otherAttribute.Concept;
            Discipline = otherAttribute.Discipline;
            IsUoMRequired = otherAttribute.IsUoMRequired;
        }

        public override void Clone(XElement xElement)
        {
            Init();
            base.Clone(xElement);
            foreach (XAttribute attribute in xElement.Attributes())
            {
                string name = attribute.Name.LocalName.ToLower();
                switch (name)
                {
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

        public override bool Equals(object otherObject)
        {
            if ( !(otherObject is ConceptualModelAttribute) || otherObject == null)
            {
                return false;
            }
            ConceptualModelAttribute other = (ConceptualModelAttribute)otherObject;

            bool baseEquals = (this.Id.Equals(other.Id)
                                && this.Name.Equals(other.Name)
                                && this.Description.Equals(other.Description)
                                && this.IsObsolete.Equals(other.IsObsolete)
                                && this.SortOrder.Equals(other.SortOrder)
                                && this.Aspect.Equals(other.Aspect));


            bool thisEquals = (this.ClassOfMeasure.Equals(other.ClassOfMeasure)
                                && this.DataType.Equals(other.DataType)
                                && this.Group.Equals(other.Group)
                                && this.Presence.Equals(other.Presence)
                                && this.ValidationType.Equals(other.ValidationType)
                                && this.ValidationRule.Equals(other.ValidationRule)
                                && this.MinOccurs.Equals(other.MinOccurs)
                                && this.MaxOccurs.Equals(other.MaxOccurs)
                                && this.MaturityLevels.Equals(other.MaturityLevels)
                                && this.Concept.Equals(other.Concept)
                                && this.Discipline.Equals(other.Discipline)
                                && this.IsUoMRequired.Equals(other.IsUoMRequired));

            if (!baseEquals || !thisEquals)
            {
                return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override void Init()
        {
            base.Init();
            ClassOfMeasure = "";
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

        public override string ToString()
        {
            return base.ToString();
        }
        public void AddApplicableClass(ConceptualModelClass cmClass)
        {
            if (this.ApplicableClasses == null)
            {
                this.ApplicableClasses = new Dictionary<string, ConceptualModelClass>();
            }
            if (!this.ApplicableClasses.ContainsKey(cmClass.Id))
            {
                this.ApplicableClasses.Add(cmClass.Id, cmClass);
            }
        }
        public override KeyValuePair<string, string>[] Properties()
        {
            List<KeyValuePair<string, string>> attributes = new List<KeyValuePair<string, string>>();
            attributes.AddRange(base.Properties());

            attributes.Add(new KeyValuePair<string, string>($"ClassOfMeasure", this.ClassOfMeasure));
            attributes.Add(new KeyValuePair<string, string>($"DataType", this.DataType));
            attributes.Add(new KeyValuePair<string, string>($"Group", this.Group));
            attributes.Add(new KeyValuePair<string, string>($"Presence", this.Presence));
            attributes.Add(new KeyValuePair<string, string>($"ValidationType", this.ValidationType));
            attributes.Add(new KeyValuePair<string, string>($"ValidationRule", this.ValidationRule));
            attributes.Add(new KeyValuePair<string, string>($"MinOccurs", this.MinOccurs));
            attributes.Add(new KeyValuePair<string, string>($"MaxOccurs", this.MaxOccurs));
            for (int index = 0; index < this.MaturityLevels.Count; index++)
            {
                attributes.Add(new KeyValuePair<string, string>($"MaturityLevel", this.MaturityLevels[index]));
            }
            attributes.Add(new KeyValuePair<string, string>($"Concept", this.Concept));
            attributes.Add(new KeyValuePair<string, string>($"Discipline", this.Discipline));
            attributes.Add(new KeyValuePair<string, string>($"IsUomRequired", this.IsUoMRequired.ToString()));

            return base.Properties();
        }
        string ClassOfMeasure { get; set; } // measure class id
        public string DataType { get; set; }
        public string Group { get; set; }
        public string Presence { get; set; } // Unselect, NotApplicable, Optional, Preferred, Required
        public string ValidationType { get; set; } // Unselect, Enumeration, ValueRangeInclusive, RegularExpression, Association
        public string ValidationRule { get; set; }
        public string MinOccurs { get; set; }
        public string MaxOccurs { get; set; }
        public List<string> MaturityLevels { get; set; }
        public string Concept { get; set; }
        public string Discipline { get; set; }
        public bool IsUoMRequired { get; set; }
        public ConceptualModelClass CameFrom { get; set; }
        public Dictionary<string, ConceptualModelClass> ApplicableClasses { get; set; }
    }
}
