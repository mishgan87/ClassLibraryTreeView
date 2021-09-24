using ClassLibraryTreeView.Interfaces;
using System.Collections.Generic;
using System.Xml.Linq;

namespace ClassLibraryTreeView.Classes
{
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
                        if (Extends.Equals("not found"))
                        {
                            Extends = "";
                        }
                        break;
                    case "concept":
                        Concept = $"{attribute.Value}";
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
                        IAttribute permissibleAttribute = new IAttribute(attribute);
                        // PermissibleAttributes.Add(permissibleAttribute);
                        PermissibleAttributes.Add(permissibleAttribute.Id, permissibleAttribute);
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
            Extends = "";
            Concept = "";
            LifeCycleType = "";
            NamingTemplates = new List<string>();
            // PermissibleAttributes = new List<IAttribute>();
            PermissibleAttributes = new Dictionary<string, IAttribute>();
            Xtype = "";
            Parent = null;
            Children = new Dictionary<string, IClass>();
        }
        public KeyValuePair<string, string>[] Attributes()
        {
            List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();

            result.Add(new KeyValuePair<string, string>("Id", Id));
            result.Add(new KeyValuePair<string, string>("Name", Name));
            result.Add(new KeyValuePair<string, string>("Description", Description));
            result.Add(new KeyValuePair<string, string>("IsObsolete", IsObsolete.ToString()));
            result.Add(new KeyValuePair<string, string>("SortOrder", SortOrder));

            if (Aspect.Count == 0)
            {
                result.Add(new KeyValuePair<string, string>("Aspect", ""));
            }
            else
            {
                foreach (string aspect in Aspect)
                {
                    result.Add(new KeyValuePair<string, string>("Aspect", aspect));
                }
            }

            result.Add(new KeyValuePair<string, string>("IsAbstract", IsAbstract.ToString()));
            result.Add(new KeyValuePair<string, string>("Extends", Extends));
            result.Add(new KeyValuePair<string, string>("Concept", Concept));
            result.Add(new KeyValuePair<string, string>("LifeCycleType", LifeCycleType));

            if (NamingTemplates.Count == 0)
            {
                result.Add(new KeyValuePair<string, string>("NamingTemplate", ""));
            }
            else
            {
                foreach (string namingTemplate in NamingTemplates)
                {
                    result.Add(new KeyValuePair<string, string>("NamingTemplate", namingTemplate));
                }
            }

            result.Add(new KeyValuePair<string, string>("Xtype", Xtype));

            return result.ToArray();
        }
        public bool Equals(IIdentifiable source)
        {
            if ( (!Id.Equals(source.Id))
                 || (!Name.Equals(source.Name))
                 || (!Description.Equals(source.Description))
                 || (!IsObsolete.Equals(source.IsObsolete))
                 || (!SortOrder.Equals(source.SortOrder))
                 || (!Aspect.Equals(source.Aspect))
                 || (!Id.Equals(source.Id)) )
            {
                return false;
            }

            IClass someClass = (IClass)source;

            if ((!IsAbstract.Equals(someClass.IsAbstract))
                 || (!Extends.Equals(someClass.Extends))
                 || (!Concept.Equals(someClass.Concept))
                 || (!LifeCycleType.Equals(someClass.LifeCycleType))
                 || (!NamingTemplates.Equals(someClass.NamingTemplates))
                 || (!PermissibleAttributes.Equals(someClass.PermissibleAttributes))
                 || (!Xtype.Equals(someClass.Xtype))
                 || (!Children.Equals(someClass.Children)) )
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

            IClass someClass = (IClass)source;

            IsAbstract = someClass.IsAbstract;
            Extends = someClass.Extends;
            Concept = someClass.Concept;
            LifeCycleType = someClass.LifeCycleType;
            NamingTemplates = new List<string>(someClass.NamingTemplates);
            // PermissibleAttributes = new List<IAttribute>(someClass.PermissibleAttributes);
            PermissibleAttributes = new Dictionary<string, IAttribute>(someClass.PermissibleAttributes);
            Xtype = someClass.Xtype;
            Parent = someClass.Parent;
            Children = new Dictionary<string, IClass>(someClass.Children);
        }
        public IClass GetChildById(IClass cmClass)
        {
            if (Children.ContainsKey(cmClass.Id))
            {
                return Children[cmClass.Id];
            }
            return null;
        }
        public IClass GetChildByName(IClass cmClass)
        {
            foreach (IClass child in Children.Values)
            {
                if(child.Name.Equals(cmClass.Name))
                {
                    return child;
                }
            }
            return null;
        }
        public bool ContainsChild(IClass cmClass) => Children.ContainsValue(cmClass);
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
        public int Depth
        {
            get
            {
                int depth = 0;
                IClass parent = this.Parent;
                while (parent != null)
                {
                    depth++;
                    parent = parent.Parent;
                }
                return depth;
            }
        }
        
        // Interface members
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsObsolete { get; set; }
        public string SortOrder { get; set; }
        public List<string> Aspect { get; set; }
        // Class members
        public bool IsAbstract { get; set; }
        public string Extends { get; set; }
        // IIdentifiableConcept Concept { get; set; }
        public string Concept { get; set; }
        public string LifeCycleType { get; set; }
        public List<string> NamingTemplates { get; set; }
        // public List<IAttribute> PermissibleAttributes { get; set; }
        public Dictionary<string, IAttribute> PermissibleAttributes { get; set; }
        public string Xtype { get; set; }
        public IClass Parent { get; set; }
        public Dictionary<string, IClass> Children { get; set; }
    }
}
