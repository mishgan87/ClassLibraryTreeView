using ClassLibraryTreeView.Interfaces;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace ClassLibraryTreeView.Classes
{
    public class EnumerationListItem : IIdentifiable
    {
        public EnumerationListItem()
        {
            Init();
        }
        public EnumerationListItem(string id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }
        public EnumerationListItem(XElement item)
        {
            Init();
            foreach (XAttribute attribute in item.Attributes())
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
                    default:
                        break;
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
        }
        public bool Equals(EnumerationListItem item)
        {
            if (!Id.Equals(item.Id)
               || !Name.Equals(item.Name)
               || !Description.Equals(item.Description))
            {
                return false;
            }
            return true;
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

            return result.ToArray();
        }

        public bool Equals(IIdentifiable source)
        {
            if (!Id.Equals(source.Id)
               || !Name.Equals(source.Name)
               || !Description.Equals(source.Description)
               || !IsObsolete.Equals(source.IsObsolete)
               || !SortOrder.Equals(source.SortOrder)
               || !Aspect.Equals(source.Aspect))
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
                    default:
                        break;
                }
            }
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsObsolete { get; set; }
        public string SortOrder { get; set; }
        public List<string> Aspect { get; set; }
    }
}
