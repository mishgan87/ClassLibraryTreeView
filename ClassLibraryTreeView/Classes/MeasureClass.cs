using ClassLibraryTreeView.Interfaces;
using System.Collections.Generic;
using System.Xml.Linq;

namespace ClassLibraryTreeView.Classes
{
    public class MeasureClass : IIdentifiable
    {
        public MeasureClass()
        {
            Init();
        }
        public MeasureClass(XElement source)
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
        public string Group { get; set; }
        public List<string> Units { get; set; }

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

            result.Add(new KeyValuePair<string, string>("Group", Group));

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

            MeasureClass measureClass = (MeasureClass)source;

            Group = measureClass.Group;
            Units = new List<string>(measureClass.Units);
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

                    case "groupid":
                        Group = $"{attribute.Value}";
                        break;
                    default:
                        break;
                }
            }

            foreach (XElement child in source.Elements())
            {
                string name = child.Name.LocalName.ToLower();
                if (name.Equals("units"))
                {
                    foreach (XElement attribute in child.Elements())
                    {
                        Units.Add(attribute.Attribute("id").Value);
                    }
                }
            }
        }
        public bool Equals(IIdentifiable source)
        {
            if ((!Id.Equals(source.Id))
                 || (!Name.Equals(source.Name))
                 || (!Description.Equals(source.Description))
                 || (!IsObsolete.Equals(source.IsObsolete))
                 || (!SortOrder.Equals(source.SortOrder))
                 || (!Aspect.Equals(source.Aspect))
                 || (!Id.Equals(source.Id)))
            {
                return false;
            }

            MeasureClass measureClass = (MeasureClass)source;

            if (!Group.Equals(measureClass.Group)
                 || !Units.Equals(measureClass.Units))
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

            Group = "";
            Units = new List<string>();
        }
    }
}
