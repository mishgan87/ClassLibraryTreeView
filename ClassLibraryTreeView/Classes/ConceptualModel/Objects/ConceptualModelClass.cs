using ClassLibraryTreeView.Interfaces;
using System.Collections.Generic;
using System.Xml.Linq;

namespace ClassLibraryTreeView.Classes
{
    public class ConceptualModelClass : ConceptualModelObject
    {
        public ConceptualModelClass() : base()
        {
        }

        public ConceptualModelClass(ConceptualModelObject other) : base(other)
        {
        }

        public ConceptualModelClass(XElement xElement) : base(xElement)
        {
        }
        public override void Init()
        {
            base.Init();
            IsAbstract = false;
            Extends = "";
            Concept = "";
            LifeCycleType = "";
            NamingTemplates = new List<string>();
            PermissibleAttributes = new Dictionary<string, ConceptualModelAttribute>();
            Xtype = "";
            Parent = null;
            Children = new Dictionary<string, ConceptualModelClass>();
        }
        public override void Clone(IConceptualModelObject otherObject)
        {
            base.Clone(otherObject);
            ConceptualModelClass other = (ConceptualModelClass)otherObject;
            IsAbstract = other.IsAbstract;
            Extends = other.Extends;
            Concept = other.Concept;
            LifeCycleType = other.LifeCycleType;
            NamingTemplates = new List<string>(other.NamingTemplates);
            PermissibleAttributes = new Dictionary<string, ConceptualModelAttribute>(other.PermissibleAttributes);
            Xtype = other.Xtype;
            Parent = other.Parent;
            Children = new Dictionary<string, ConceptualModelClass>(other.Children);
        }

        public override void Clone(XElement xElement)
        {
            base.Clone(xElement);
            foreach (XAttribute attribute in xElement.Attributes())
            {
                string attributeName = attribute.Name.LocalName.ToLower();
                switch (attributeName)
                {
                    case "extends":
                        Extends = $"{attribute.Value}";
                        break;
                    case "concept":
                        Concept = $"{attribute.Value}";
                        break;
                    case "lifestyletype":
                        LifeCycleType = $"{attribute.Value}";
                        break;
                    case "isabstract":
                        IsAbstract = false;
                        if (attribute.Value.Equals("true"))
                        {
                            IsAbstract = true;
                        }
                        break;
                    case "namingtemplate":
                        NamingTemplates.Add($"{attribute.Value}");
                        break;
                }
            }

            Xtype = $"{xElement.Parent.Name.LocalName}";

            foreach (XElement child in xElement.Elements())
            {
                string name = child.Name.LocalName;
                if (name.Equals("Attributes"))
                {
                    foreach (XElement attribute in child.Elements())
                    {
                        ConceptualModelAttribute permissibleAttribute = new ConceptualModelAttribute(attribute);
                        PermissibleAttributes.Add(permissibleAttribute.Id, permissibleAttribute);
                    }
                }
            }
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override bool Equals(object otherObject)
        {
            if (!base.Equals(otherObject))
            {
                return false;
            }

            ConceptualModelClass other = (ConceptualModelClass)otherObject;

            return (this.IsAbstract.Equals(other.IsAbstract)
                                && this.Extends.Equals(other.Extends)
                                && this.Concept.Equals(other.Concept)
                                && this.LifeCycleType.Equals(other.LifeCycleType)
                                && this.NamingTemplates.Equals(other.NamingTemplates)
                                && this.PermissibleAttributes.Equals(other.PermissibleAttributes)
                                && this.Xtype.Equals(other.Xtype)
                                && this.Parent.Equals(other.Parent));
        }

        public override string ToString()
        {
            return base.ToString();
        }
        public static int Depth(ConceptualModelClass cmClass)
        {
            int depth = 0;
            ConceptualModelClass parent = cmClass.Parent;
            while (parent != null)
            {
                depth++;
                parent = parent.Parent;
            }
            return depth;
        }
        public bool ContainsChild(ConceptualModelClass cmClass) => Children.ContainsValue(cmClass);
        public string PermissibleAttributePresence(string id)
        {
            if (PermissibleAttributes.ContainsKey(id))
            {
                if (!PermissibleAttributes[id].Presence.Equals(""))
                {
                    return PermissibleAttributes[id].Presence.Substring(0, 1);
                }
                return "X";
            }

            return "";
        }
        public bool IsAbstract { get; set; }
        public string Extends { get; set; }
        public string Concept { get; set; } // Unselect, Functional, Physical, General, Document
        public string LifeCycleType { get; set; }
        public List<string> NamingTemplates { get; set; }
        public Dictionary<string, ConceptualModelAttribute> PermissibleAttributes { get; set; }
        public string Xtype { get; set; }
        public ConceptualModelClass Parent { get; set; }
        public Dictionary<string, ConceptualModelClass> Children { get; set; }
    }
}
