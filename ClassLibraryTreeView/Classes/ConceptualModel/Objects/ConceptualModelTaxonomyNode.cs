﻿using ClassLibraryTreeView.Interfaces;
using System.Collections.Generic;
using System.Xml.Linq;

namespace ClassLibraryTreeView.Classes
{
    public class ConceptualModelTaxonomyNode : ConceptualModelObject
    {
        public ConceptualModelTaxonomyNode()
        {
            Init();
        }

        public ConceptualModelTaxonomyNode(ConceptualModelObject other)
        {
            Init();
            Clone(other);
        }

        public ConceptualModelTaxonomyNode(XElement xElement)
        {
            Init();
            Clone(xElement);
        }

        public override KeyValuePair<string, string>[] Attributes()
        {
            return base.Attributes();
        }

        public override void Clone(IIdentifiable otherObject)
        {
            base.Clone(otherObject);

            ConceptualModelTaxonomyNode node = (ConceptualModelTaxonomyNode)otherObject;

            this.Classes = new List<string>(node.Classes);
        }

        public override void Clone(XElement xElement)
        {
            base.Clone(xElement);
            foreach (XElement child in xElement.Elements())
            {
                string name = child.Name.LocalName.ToLower();
                if (name.Equals("classes"))
                {
                    foreach (XElement node in child.Elements())
                    {
                        Classes.Add(node.Attribute("id").Value);
                    }
                }
            }
        }

        public override bool Equals(object otherObject)
        {
            return base.Equals(otherObject);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override void Init()
        {
            base.Init();
            Classes = new List<string>();
        }

        public override string ToString()
        {
            return base.ToString();
        }
        public List<string> Classes { get; set; }
    }
}
