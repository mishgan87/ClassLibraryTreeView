using ClassLibraryTreeView.Interfaces;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;

namespace ClassLibraryTreeView.Classes
{
    public class ConceptualModelEnumeration : ConceptualModelObject
    {
        public List<ConceptualModelEnumerationItem> Items { get; set; }
        public ConceptualModelEnumeration(XElement xElement)
        {
            Clone(xElement);
        }
        public override void Clone(IConceptualModelObject source)
        {
            Id = source.Id;
            Name = source.Name;
            Description = source.Description;
            IsObsolete = source.IsObsolete;
            SortOrder = source.SortOrder;
            Aspect = new List<string>(source.Aspect);

            ConceptualModelEnumeration enumerationList = (ConceptualModelEnumeration)source;
            Items = new List<ConceptualModelEnumerationItem>(enumerationList.Items);
        }
        public override Dictionary<string, object[]> PropertiesArrays()
        {
            Dictionary<string, object[]> propertiesArrays = base.PropertiesArrays();
            propertiesArrays.Add($"Items ({Items.Count})", Items.ToArray());
            return propertiesArrays;
        }
        public override void Clone(XElement xElement)
        {
            Init();
            base.Clone(xElement);
            
            foreach(XElement child in xElement.Elements())
            {
                string name = child.Name.LocalName.ToLower();
                if (name.Equals("items"))
                {
                    foreach (XElement item in child.Elements())
                    {
                        Items.Add(new ConceptualModelEnumerationItem(item));
                    }
                }
            }
        }
        public override void Init()
        {
            Id = "";
            Name = "";
            Description = "";
            IsObsolete = false;
            SortOrder = "";
            Aspect = new List<string>();

            Items = new List<ConceptualModelEnumerationItem>();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object otherObject)
        {
            if (!(otherObject is ConceptualModelEnumeration) || otherObject == null)
            {
                return false;
            }

            ConceptualModelEnumeration other = (ConceptualModelEnumeration)otherObject;

            bool baseEquals = (this.Id.Equals(other.Id)
                && this.Name.Equals(other.Name)
                && this.Description.Equals(other.Description)
                && this.IsObsolete.Equals(other.IsObsolete)
                && this.SortOrder.Equals(other.SortOrder)
                && this.Aspect.Equals(other.Aspect));

            ConceptualModelEnumeration enumerationList = (ConceptualModelEnumeration)otherObject;
            if (!Items.Equals(enumerationList.Items) || !baseEquals)
            {
                return false;
            }

            return true;
        }
    }
}
