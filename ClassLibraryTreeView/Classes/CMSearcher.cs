using ClassLibraryTreeView.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryTreeView.Classes
{
    class CMSearcher
    {
        /// <summary>
        /// Search text in conceptual model elements names and ids
        /// </summary>
        /// <param name="text">text for searching</param>
        /// <param name="model">conceptual model</param>
        /// <param name="filter">bit filter :
        /// bit 0 - find id
        /// bit 1 - find name
        /// bit 2 - find in attributes
        /// bit 3 - find in classes
        /// bit 4 - find in enumerations
        /// bit 5 - find in taxonomies
        /// bit 6 - find in measures
        /// </param>
        /// <returns>list of objects <type, object> </returns>
        public static List<KeyValuePair<string, object>> SearchText(string text, ConceptualModel model, int filter)
        {
            List<KeyValuePair<string, object>> objects = new List<KeyValuePair<string, object>>();

            bool searchId = true;
            bool searchName = true;
            bool searchClass = true;
            bool searchAttribute = true;
            // bool searchTaxonomy = true;
            // bool searchEnumeration = true;
            // bool searchMeasureUnit = true;
            // bool searchMeasureClass = true;

            if (searchClass)
            {
                foreach (Dictionary<string, IClass> map in model.classes.Values)
                {
                    foreach (IClass cmClass in map.Values)
                    {
                        if ((cmClass.Id.Contains(text) && searchId)
                            || (cmClass.Name.Contains(text) && searchName))
                        {
                            objects.Add(new KeyValuePair<string, object>($"class", cmClass));
                        }
                    }
                }
            }

            if (searchAttribute)
            {
                foreach (Dictionary<string, IAttribute> map in model.attributes.Values)
                {
                    foreach (IAttribute attribute in map.Values)
                    {
                        if ((attribute.Id.Contains(text) && searchId)
                            || (attribute.Name.Contains(text) && searchName))
                        {
                            objects.Add(new KeyValuePair<string, object>($"attribute", attribute));
                        }
                    }
                }
            }

            return objects;
        }
    }
}
