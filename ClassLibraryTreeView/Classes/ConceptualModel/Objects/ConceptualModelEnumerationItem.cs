using ClassLibraryTreeView.Interfaces;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace ClassLibraryTreeView.Classes
{
    public class ConceptualModelEnumerationItem : ConceptualModelObject
    {
        public ConceptualModelEnumerationItem()
        {
            Init();
        }
        public ConceptualModelEnumerationItem(ConceptualModelEnumerationItem other)
        {
            Init();
            Clone(other);
        }
        public ConceptualModelEnumerationItem(XElement xElement)
        {
            Init();
            Clone(xElement);
        }
        public override KeyValuePair<string, string>[] Attributes()
        {
            return base.Attributes();
        }

        public override void Clone(IIdentifiable other)
        {
            base.Clone(other);
        }

        public override void Clone(XElement xElement)
        {
            base.Clone(xElement);
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
            if (!(otherObject is ConceptualModelEnumerationItem) || otherObject == null)
            {
                return false;
            }

            ConceptualModelEnumerationItem other = (ConceptualModelEnumerationItem)otherObject;

            bool baseEquals = (this.Id.Equals(other.Id)
                && this.Name.Equals(other.Name)
                && this.Description.Equals(other.Description)
                && this.IsObsolete.Equals(other.IsObsolete)
                && this.SortOrder.Equals(other.SortOrder)
                && this.Aspect.Equals(other.Aspect));

            return baseEquals;
        }

        public override void Init()
        {
            base.Init();
        }
    }
}
