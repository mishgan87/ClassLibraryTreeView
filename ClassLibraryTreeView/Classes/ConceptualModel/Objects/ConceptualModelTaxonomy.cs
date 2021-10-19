using ClassLibraryTreeView.Interfaces;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;

namespace ClassLibraryTreeView.Classes
{
    public class ConceptualModelTaxonomy : ConceptualModelObject
    {
        public ConceptualModelTaxonomy()
        {
            Init();
        }

        public ConceptualModelTaxonomy(ConceptualModelObject other)
        {
            Init();
            Clone(other);
        }

        public ConceptualModelTaxonomy(XElement xElement)
        {
            Init();
            Clone(xElement);
        }

        public string Concept { get; set; }
        public List<ConceptualModelTaxonomyNode> Nodes { get; set; }
        public override Dictionary<string, string[]> PropertiesArrays()
        {
            Dictionary<string, string[]> propertiesArrays = base.PropertiesArrays();

            List<string> idList = new List<string>();
            if (Nodes.Count > 0)
            {
                idList.AddRange(from ConceptualModelTaxonomyNode node in Nodes select node.Id);
            }
            propertiesArrays.Add($"Nodes ({idList.Count})", idList.ToArray());

            return propertiesArrays;
        }

        public override void Clone(IConceptualModelObject otherObject)
        {
            base.Clone(otherObject);
            ConceptualModelTaxonomy other = (ConceptualModelTaxonomy)otherObject;
            Concept = other.Concept;
            Nodes = new List<ConceptualModelTaxonomyNode>(other.Nodes);
        }

        public override void Clone(XElement xElement)
        {
            base.Clone(xElement);
            foreach (XElement child in xElement.Elements())
            {
                string name = child.Name.LocalName.ToLower();
                if (name.Equals("nodes"))
                {
                    foreach (XElement node in child.Elements())
                    {
                        Nodes.Add(new ConceptualModelTaxonomyNode(node));
                    }
                }
            }
        }

        public override bool Equals(object otherObject)
        {
            if (!(otherObject is ConceptualModelTaxonomy) || otherObject == null)
            {
                return false;
            }

            ConceptualModelClass other = (ConceptualModelClass)otherObject;

            bool baseEquals = (this.Id.Equals(other.Id)
                                && this.Name.Equals(other.Name)
                                && this.Description.Equals(other.Description)
                                && this.IsObsolete.Equals(other.IsObsolete)
                                && this.SortOrder.Equals(other.SortOrder)
                                && this.Aspect.Equals(other.Aspect));

            bool thisEquals = this.Concept.Equals(other.Concept);

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
            Concept = "";
            Nodes = new List<ConceptualModelTaxonomyNode>();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
