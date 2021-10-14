using ClassLibraryTreeView.Interfaces;
using System.Collections.Generic;
using System.Xml.Linq;

namespace ClassLibraryTreeView.Classes
{
    public class ConceptualModelMeasureUnit : ConceptualModelObject
    {
        public string Symbol { get; set; }
        public string Standard { get; set; }
        public string Group { get; set; }
        public string System { get; set; } // Unselect, Metric, US = 2, Imperial = 3
        public ConceptualModelMeasureUnit() : base()
        {
        }

        public ConceptualModelMeasureUnit(ConceptualModelObject other) : base(other)
        {
        }

        public ConceptualModelMeasureUnit(XElement xElement) : base(xElement)
        {
        }

        public override KeyValuePair<string, string>[] Properties()
        {
            List<KeyValuePair<string, string>> attributes = new List<KeyValuePair<string, string>>();
            attributes.AddRange(base.Properties());
            attributes.Add(new KeyValuePair<string, string>($"Symbol", Symbol));
            attributes.Add(new KeyValuePair<string, string>($"Standard", Standard));
            attributes.Add(new KeyValuePair<string, string>($"Group", Group));
            attributes.Add(new KeyValuePair<string, string>($"System", System));
            return attributes.ToArray();
        }

        public override void Clone(IConceptualModelObject other)
        {
            base.Clone(other);
        }

        public override void Clone(XElement xElement)
        {
            base.Clone(xElement);
            foreach (XAttribute attribute in xElement.Attributes())
            {
                string attributeName = attribute.Name.LocalName.ToLower();
                switch (attributeName)
                {
                    case "symbol":
                        Symbol = $"{attribute.Value}";
                        break;
                    case "standard":
                        Standard = $"{attribute.Value}";
                        break;
                    case "group":
                        Group = $"{attribute.Value}";
                        break;
                    case "system":
                        System = $"{attribute.Value}";
                        break;
                }
            }
        }

        public override bool Equals(object otherObject)
        {
            if (!base.Equals(otherObject))
            {
                return false;
            }

            ConceptualModelMeasureUnit other = (ConceptualModelMeasureUnit)otherObject;

            return (this.Symbol.Equals(other.Symbol)
                                && this.Standard.Equals(other.Standard)
                                && this.Group.Equals(other.Group)
                                && this.System.Equals(other.System));
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override void Init()
        {
            base.Init();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
