using System.Collections.Generic;
using System.Xml.Linq;

namespace ClassLibraryTreeView.Interfaces
{
    public class Taxonomy : IIdentifiable
    {
        public Taxonomy()
        {
            Init();
        }
        public Taxonomy(XElement source)
        {
            Clone(source);
        }

        // Interface members
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsObsolete { get; set; }
        public string SortOrder { get; set; }
        public List<string> Aspect { get; set; }
        // Class members
        public string Concept { get; set; }
        public List<TaxonomyNode> Nodes { get; set; }

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

            result.Add(new KeyValuePair<string, string>("Concept", Concept));

            return result.ToArray();
        }

        public void Clone(IIdentifiable source)
        {
            Id = source.Id;
            Name = source.Name;
            Description = source.Description;
            IsObsolete = source.IsObsolete;
            SortOrder = source.SortOrder;
            Aspect = new List<string>(source.Aspect);

            Taxonomy taxonomy = (Taxonomy)source;

            Concept = taxonomy.Concept;
            Nodes = new List<TaxonomyNode>(taxonomy.Nodes);
        }

        public void Clone(XElement source)
        {
            Init();
            foreach (XAttribute attribute in source.Attributes())
            {
                string name = attribute.Name.LocalName.ToLower();
                switch (name)
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

                    case "concept":
                        Concept = $"{attribute.Value}";
                        break;
                    default:
                        break;
                }
            }

            foreach (XElement child in source.Elements())
            {
                string name = child.Name.LocalName.ToLower();
                if (name.Equals("nodes"))
                {
                    foreach (XElement node in child.Elements())
                    {
                        Nodes.Add(new TaxonomyNode(node));
                    }
                }
            }
        }
        public bool Equals(IIdentifiable source)
        {
            throw new System.NotImplementedException();
        }

        public void Init()
        {
            Id = "";
            Name = "";
            Description = "";
            IsObsolete = false;
            SortOrder = "";
            Aspect = new List<string>();

            Concept = "";
            Nodes = new List<TaxonomyNode>();
        }
    }
}
