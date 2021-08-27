using System.Collections.Generic;
using System.Xml.Linq;

namespace ClassLibraryTreeView.Interfaces
{
    // Интерфейс, определяющий базовые параметры всех классов
    public class IClass : IIdentifiable
    {
        public IClass()
        {
            Init();
        }
        public IClass(IClass source)
        {
            Clone(source);
        }
        public IClass(XElement source)
        {
            Clone(source);
        }
        public void Clone(XElement source)
        {
            Init();
            foreach (XAttribute attribute in source.Attributes())
            {
                string name = attribute.Name.LocalName.ToLower();
                switch(name)
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

                    case "isabstract":
                        IsAbstract = false;
                        if (attribute.Value.Equals("true"))
                        {
                            IsAbstract = true;
                        }
                        break;
                    case "extends":
                        Extends = $"{attribute.Value}";
                        if (Extends.Equals("") || Extends.Equals("not found"))
                        {
                            Extends = null;
                        }
                        break;
                    case "concept":
                        Extends = $"{attribute.Value}";
                        break;
                    case "lifecycletype":
                        LifeCycleType = $"{attribute.Value}";
                        break;
                    case "namingtemplate":
                        NamingTemplates.Add($"{attribute.Value}");
                        break;

                    default:
                        break;
                }
            }

            Xtype = $"{source.Parent.Name.LocalName}";

            foreach (XElement child in source.Elements())
            {
                string name = child.Name.LocalName;
                if (name.Equals("Attributes"))
                {
                    foreach (XElement attribute in child.Elements())
                    {
                        PermissibleAttributes.Add(attribute.Attribute("id").Value);
                    }
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

            IsAbstract = false;
            Extends = null;
            Concept = "";
            LifeCycleType = null;
            NamingTemplates = new List<string>();
            PermissibleAttributes = new List<string>();
            Xtype = "";
        }
        public void Clone(IIdentifiable source)
        {
            Id = source.Id;
            Name = source.Name;
            Description = source.Description;
            IsObsolete = source.IsObsolete;
            SortOrder = source.SortOrder;
            Aspect = new List<string>(source.Aspect);

            IClass someClass = (IClass)source;

            IsAbstract = someClass.IsAbstract;
            Extends = someClass.Extends;
            Concept = someClass.Concept;
            LifeCycleType = someClass.LifeCycleType;
            NamingTemplates = new List<string>(someClass.NamingTemplates);
            PermissibleAttributes = new List<string>(someClass.PermissibleAttributes);
            Xtype = someClass.Xtype;
        }

        // Interface members
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsObsolete { get; set; }
        public string SortOrder { get; set; }
        public List<string> Aspect { get; set; }
        // Class members
        bool IsAbstract { get; set; }
        // IClass Extends { get; set; }
        string Extends { get; set; }
        // IIdentifiableConcept Concept { get; set; }
        string Concept { get; set; }
        string LifeCycleType { get; set; }
        List<string> NamingTemplates { get; set; }
        List<string> PermissibleAttributes { get; set; }
        string Xtype { get; set; }

    }
}
