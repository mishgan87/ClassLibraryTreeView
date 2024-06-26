﻿using ClassLibraryTreeView.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryTreeView.Classes
{
    class ConceptualModelSearcher
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
            bool searchConceptualModelTaxonomy = GetBit(filter, 5);
            bool searchEnumeration = GetBit(filter, 6);
            bool searchMeasureUnit = GetBit(filter, 7);
            bool searchMeasureClass = GetBit(filter, 8);

            if (searchClass)
            {
                foreach (Dictionary<string, ConceptualModelClass> map in model.classes.Values)
                {
                    foreach (ConceptualModelClass ConceptualModelClass in map.Values)
                    {
                        if ((ConceptualModelClass.Id.Contains(text) && searchId)
                            || (ConceptualModelClass.Name.Contains(text) && searchName))
                        {
                            objects.Add(new KeyValuePair<string, object>($"class", ConceptualModelClass));
                        }
                    }
                }
            }

            if (searchAttribute)
            {
                foreach (Dictionary<string, ConceptualModelAttribute> map in model.attributes.Values)
                {
                    foreach (ConceptualModelAttribute attribute in map.Values)
                    {
                        if (attribute.ContainsText(text, searchId, searchName))
                        {
                            objects.Add(new KeyValuePair<string, object>($"attribute", attribute));
                        }
                    }
                }
            }

            if (searchConceptualModelTaxonomy)
            {
                //IEnumerable<ConceptualModelTaxonomy> selectedTaxonomies = (IEnumerable<ConceptualModelTaxonomy>)model.Taxonomies.SelectMany(taxonomy => taxonomy.ContainsText(text, searchId, searchName));
                /*
                IEnumerable <ConceptualModelTaxonomy> selectedTaxonomies = model.Taxonomies.Select(taxonomy =>
                {
                    if (taxonomy.ContainsText(text, searchId, searchName))
                    {
                        return taxonomy;
                    }
                    return null;
                });
                */
                foreach (ConceptualModelTaxonomy taxonomy in model.Taxonomies.Values)
                {
                    if (taxonomy.ContainsText(text, searchId, searchName))
                    {
                        objects.Add(new KeyValuePair<string, object>($"taxonomy", taxonomy));
                    }
                    foreach (ConceptualModelTaxonomyNode node in taxonomy.Nodes)
                    {
                        if (node.ContainsText(text, searchId, searchName))
                        {
                            objects.Add(new KeyValuePair<string, object>($"taxonomynode", node));
                        }
                    }
                }
            }

            if (searchEnumeration)
            {
                foreach (ConceptualModelEnumeration enumeration in model.Enumerations.Values)
                {
                    if (enumeration.ContainsText(text, searchId, searchName))
                    {
                        objects.Add(new KeyValuePair<string, object>($"enumeration", enumeration));
                    }
                    foreach (ConceptualModelEnumerationItem item in enumeration.Items)
                    {
                        if (item.ContainsText(text, searchId, searchName))
                        {
                            objects.Add(new KeyValuePair<string, object>($"enumerationitem", item));
                        }
                    }
                }
            }

            if (searchMeasureUnit)
            {
                foreach (ConceptualModelMeasureUnit measureUnit in model.measureUnits.Values)
                {
                    if (measureUnit.ContainsText(text, searchId, searchName))
                    {
                        objects.Add(new KeyValuePair<string, object>($"measureunit", measureUnit));
                    }
                }
            }

            if (searchMeasureClass)
            {
                foreach (ConceptualModelMeasureClass measureClass in model.MeasureClasses.Values)
                {
                    if (measureClass.ContainsText(text, searchId, searchName))
                    {
                        objects.Add(new KeyValuePair<string, object>($"measureclass", measureClass));
                    }
                }
            }

            return objects;
        }
    }
}
