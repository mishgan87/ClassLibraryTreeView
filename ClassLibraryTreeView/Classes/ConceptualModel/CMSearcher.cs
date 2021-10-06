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
        // private static int GetBit(int data, int position)
        private static bool GetBit(int data, int position)
        {
            var mask = 0b00000001;
            data = data >> position;
            return ((data & mask) == 1);
        }
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

            bool searchId = GetBit(filter, 0);
            bool searchName = GetBit(filter, 1);
            bool matchCase = GetBit(filter, 2);
            bool searchClass = GetBit(filter, 3);
            bool searchAttribute = GetBit(filter, 4);
            bool searchTaxonomy = GetBit(filter, 5);
            bool searchEnumeration = GetBit(filter, 6);
            bool searchMeasureUnit = GetBit(filter, 7);
            bool searchMeasureClass = GetBit(filter, 8);

            if (searchClass)
            {
                foreach (Dictionary<string, CMClass> map in model.classes.Values)
                {
                    foreach (CMClass cmClass in map.Values)
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
                foreach (Dictionary<string, CMAttribute> map in model.attributes.Values)
                {
                    foreach (CMAttribute attribute in map.Values)
                    {
                        if ((attribute.Id.Contains(text) && searchId)
                            || (attribute.Name.Contains(text) && searchName))
                        {
                            objects.Add(new KeyValuePair<string, object>($"attribute", attribute));
                        }
                    }
                }
            }

            if (searchTaxonomy)
            {
                foreach (Taxonomy taxonomy in model.Taxonomies.Values)
                {
                    if ((taxonomy.Id.Contains(text) && searchId)
                        || (taxonomy.Name.Contains(text) && searchName))
                    {
                        objects.Add(new KeyValuePair<string, object>($"taxonomy", taxonomy));
                    }
                    foreach (TaxonomyNode node in taxonomy.Nodes)
                    {
                        if ((node.Id.Contains(text) && searchId)
                        || (node.Name.Contains(text) && searchName))
                        {
                            objects.Add(new KeyValuePair<string, object>($"taxonomynode", node));
                        }
                    }
                }
            }

            if (searchEnumeration)
            {
                foreach (EnumerationList enumeration in model.Enumerations.Values)
                {
                    if ((enumeration.Id.Contains(text) && searchId)
                        || (enumeration.Name.Contains(text) && searchName))
                    {
                        objects.Add(new KeyValuePair<string, object>($"enumeration", enumeration));
                    }
                    foreach (EnumerationListItem item in enumeration.Items)
                    {
                        if ((item.Id.Contains(text) && searchId)
                        || (item.Name.Contains(text) && searchName))
                        {
                            objects.Add(new KeyValuePair<string, object>($"enumerationitem", item));
                        }
                    }
                }
            }

            if (searchMeasureUnit)
            {
                foreach (MeasureUnit measureUnit in model.MeasureUnits.Values)
                {
                    if ((measureUnit.Id.Contains(text) && searchId)
                        || (measureUnit.Name.Contains(text) && searchName))
                    {
                        objects.Add(new KeyValuePair<string, object>($"measureunit", measureUnit));
                    }
                }
            }

            if (searchMeasureClass)
            {
                foreach (MeasureClass measureClass in model.MeasureClasses.Values)
                {
                    if ((measureClass.Id.Contains(text) && searchId)
                        || (measureClass.Name.Contains(text) && searchName))
                    {
                        objects.Add(new KeyValuePair<string, object>($"measureclass", measureClass));
                    }
                }
            }

            return objects;
        }
    }
}
