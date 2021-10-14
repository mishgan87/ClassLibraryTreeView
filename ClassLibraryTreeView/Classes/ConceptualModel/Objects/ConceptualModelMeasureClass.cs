using ClassLibraryTreeView.Interfaces;
using System.Collections.Generic;
using System.Xml.Linq;

namespace ClassLibraryTreeView.Classes
{
    public class ConceptualModelMeasureClass : ConceptualModelObject
    {
        public string Group { get; set; }
        public List<ConceptualModelMeasureUnit> Units { get; set; }
        public ConceptualModelMeasureClass()
        {
        }

        public ConceptualModelMeasureClass(ConceptualModelObject other) : base(other)
        {
        }

        public ConceptualModelMeasureClass(XElement xElement) : base(xElement)
        {
        }

        public override void Clone(IConceptualModelObject other)
        {
            base.Clone(other);
            ConceptualModelMeasureClass measureClass = (ConceptualModelMeasureClass)other;
            Group = measureClass.Group;
        }

        public override void Clone(XElement xElement)
        {
            base.Clone(xElement);
            foreach (XAttribute attribute in xElement.Attributes())
            {
                string name = attribute.Name.LocalName.ToLower();
                switch (name)
                {
                    case "groupid":
                        Group = $"{attribute.Value}";
                        break;
                    default:
                        break;
                }
            }

            foreach (XElement child in xElement.Elements())
            {
                string name = child.Name.LocalName.ToLower();
                if (name.Equals("units"))
                {
                    foreach (XElement attribute in child.Elements())
                    {
                        Units.Add(new ConceptualModelMeasureUnit(attribute));
                    }
                }
            }
        }

        public override bool Equals(object otherObject)
        {
            if (!base.Equals(otherObject))
            {
                return false;
            }
            ConceptualModelMeasureClass measureClass = (ConceptualModelMeasureClass)otherObject;
            return (Group.Equals(measureClass.Group) && Units.Equals(measureClass.Units));
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override void Init()
        {
            base.Init();
            Group = "";
            Units = new List<ConceptualModelMeasureUnit>();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
