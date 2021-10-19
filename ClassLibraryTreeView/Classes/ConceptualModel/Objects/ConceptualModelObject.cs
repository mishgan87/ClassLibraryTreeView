using ClassLibraryTreeView.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Linq;

namespace ClassLibraryTreeView.Classes
{
    public class ConceptualModelObject : IConceptualModelObject
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsObsolete { get; set; }
        public string SortOrder { get; set; }
        public List<string> Aspect { get; set; }
        public ConceptualModelObject()
        {
            Init();
        }
        public ConceptualModelObject(ConceptualModelObject other)
        {
            Init();
            Clone(other);
        }
        public ConceptualModelObject(XElement xElement)
        {
            Init();
            Clone(xElement);
        }
        public bool ContainsText(string text, bool idSearch, bool nameSearch)
        {
            return ((idSearch && Id.Contains(text)) || (nameSearch && Name.Contains(text)));
        }
        public KeyValuePair<string, string>[] Properties()
        {
            List<KeyValuePair<string, string>> properties = new List<KeyValuePair<string, string>>();

            PropertyInfo[] props = this.GetType().GetProperties();

            foreach (PropertyInfo prop in props)
            {
                if (!prop.PropertyType.FullName.ToLower().Contains("collection"))
                {
                    if (prop.GetIndexParameters().Length == 0)
                    {
                        properties.Add(new KeyValuePair<string, string>(prop.Name, $"{prop.GetValue(this)}"));
                    }
                    else
                    {
                        properties.Add(new KeyValuePair<string, string>(prop.Name, "<Indexed>"));
                    }
                }
            }

            return properties.ToArray();
        }
        public virtual Dictionary<string, object[]> PropertiesArrays()
        {
            Dictionary<string, object[]> propertiesArrays = new Dictionary<string, object[]>();

            propertiesArrays.Add($"Aspect", Aspect.ToArray());

            /*
            Type objectType = Type.GetType(this.GetType().FullName);
            PropertyInfo[] properties = objectType.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                string propertyType = property.PropertyType.FullName.ToLower();
                if (propertyType.Contains("collection"))
                {
                    var obj = Activator.CreateInstance(property.PropertyType);
                    List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
                    propertiesArrays.Add(property.Name, list.ToArray());
                }
            }
            */
            return propertiesArrays;
        }
        public virtual void Clone(IConceptualModelObject other)
        {
            this.Id = other.Id;
            this.Name = other.Name;
            this.Description = other.Description;
            this.IsObsolete = other.IsObsolete;
            this.SortOrder = other.SortOrder;
            this.Aspect = new List<string>(other.Aspect);
        }

        public virtual void Clone(XElement xElement)
        {
            foreach (XAttribute attribute in xElement.Attributes())
            {
                string attributeName = attribute.Name.LocalName.ToLower();
                switch (attributeName)
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
                }
            }
        }
        public override string ToString()
        {
            return this.Id;
        }
        public override int GetHashCode()
        {
            return ShiftAndWrap(Id.GetHashCode(), 2) ^ Name.GetHashCode();
        }
        private int ShiftAndWrap(int value, int positions)
        {
            positions = positions & 0x1F;
            uint number = BitConverter.ToUInt32(BitConverter.GetBytes(value), 0);
            uint wrapped = number >> (32 - positions);
            return BitConverter.ToInt32(BitConverter.GetBytes((number << positions) | wrapped), 0);
        }
        public override bool Equals(object otherObject)
        {
            if (!Object.ReferenceEquals(this.GetType(), otherObject.GetType()) || otherObject == null)
            {
                return false;
            }

            ConceptualModelObject other = (ConceptualModelObject)otherObject;

            return (this.Id.Equals(other.Id)
                && this.Name.Equals(other.Name)
                && this.Description.Equals(other.Description)
                && this.IsObsolete.Equals(other.IsObsolete)
                && this.SortOrder.Equals(other.SortOrder)
                && this.Aspect.Equals(other.Aspect));
        }

        public virtual void Init()
        {
            this.Id = "";
            this.Name = "";
            this.Description = "";
            this.IsObsolete = false;
            this.SortOrder = "";
            this.Aspect = new List<string>();
        }
    }
}
