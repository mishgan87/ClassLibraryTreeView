using ClassLibraryTreeView.Interfaces;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace ClassLibraryTreeView.Classes
{
    public class EnumerationListItem
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

        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class EnumerationList : IIdentifiable
    {
        // Interface members
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsObsolete { get; set; }
        public string SortOrder { get; set; }
        public List<string> Aspect { get; set; }
        // Class members
        public List<EnumerationListItem> Items { get; set; }
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
        public EnumerationList(XElement source)
        {
            Clone(source);
        }
        public void Clone(IIdentifiable source)
        {
            Id = source.Id;
            Name = source.Name;
            Description = source.Description;
            IsObsolete = source.IsObsolete;
            SortOrder = source.SortOrder;
            Aspect = new List<string>(source.Aspect);

            EnumerationList enumerationList = (EnumerationList)source;
            Items = new List<EnumerationListItem>(enumerationList.Items);
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
            
            foreach(XElement child in source.Elements())
            {
                string name = child.Name.LocalName.ToLower();
                if (name.Equals("items"))
                {
                    foreach (XElement item in child.Elements())
                    {
                        Items.Add(new EnumerationListItem(item));
                    }
                }
            }
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

            EnumerationList enumerationList = (EnumerationList)source;
            if (!Items.Equals(enumerationList.Items))
            {
                return false;
            }

            return true;
        }

        public void Init()
        {
            Id = "";
            Name = "";
            Description = "";
            IsObsolete = false;
            SortOrder = "";
            Aspect = new List<string>();

            Items = new List<EnumerationListItem>();
        }
    }
}
