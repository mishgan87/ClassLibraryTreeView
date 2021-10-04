using ClassLibraryTreeView.Classes;
using ClassLibraryTreeView.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassLibraryTreeView.Forms
{
    public partial class SearchingForm : Form
    {
        ConceptualModel model = null;
        public SearchingForm(ConceptualModel modelReference) : base()
        {
            InitializeComponent();
            model = modelReference;
            listViewResult.Columns.Add("ID", 300, HorizontalAlignment.Left);
            listViewResult.Columns.Add("Name", 300, HorizontalAlignment.Left);
            listViewResult.Columns.Add("Type", 300, HorizontalAlignment.Left);
        }
        private static int SetBit(int data, int bitValue, int position)
        {
            var mask = 0b00000001;
            mask = mask << position;

            if (bitValue == 1)
            {
                return data | mask;
            }
            else
            {
                return data & ~mask;
            }
        }
        private int GetFilter()
        {
            int filter = 0;

            if (searchId.Checked)
            {
                filter = SetBit(filter, 1, 0);
            }

            if (searchName.Checked)
            {
                filter = SetBit(filter, 1, 1);
            }

            if (matchCase.Checked)
            {
                filter = SetBit(filter, 1, 2);
            }

            if (searchClasses.Checked)
            {
                filter = SetBit(filter, 1, 3);
            }

            if (searchAttributes.Checked)
            {
                filter = SetBit(filter, 1, 4);
            }

            if (searchTaxonomies.Checked)
            {
                filter = SetBit(filter, 1, 5);
            }

            if (searchEnumerations.Checked)
            {
                filter = SetBit(filter, 1, 6);
            }

            if (searchMeasureUnits.Checked)
            {
                filter = SetBit(filter, 1, 7);
            }

            if (searchMeasureClasses.Checked)
            {
                filter = SetBit(filter, 1, 8);
            }

            return filter;
        }
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            listViewResult.Items.Clear();
            string text = searchString.Text;
            int filter = GetFilter();

            List<KeyValuePair<string, object>> results = CMSearcher.SearchText(text, model, filter);

            Action <ListView, IIdentifiable, string, Color> AddObject = (listView, element, type, color) =>
            {
                ListViewItem listViewItem = new ListViewItem(new string[] { $"{element.Id}", $"{element.Name}", $"{type}" });
                listViewItem.Tag = element;
                listViewItem.BackColor = color;
                listView.Items.Add(listViewItem);
            };

            if (results.Count > 0)
            {
                foreach (KeyValuePair<string, object> result in results)
                {
                    string type = result.Key;

                    if (type.Equals("class"))
                    {
                        CMClass cmClass = (CMClass)result.Value;
                        AddObject(listViewResult, cmClass, $"Class", Color.Yellow);
                    }

                    if (type.Equals("attribute"))
                    {
                        CMAttribute attribute = (CMAttribute)result.Value;
                        AddObject(listViewResult, attribute, $"Attribute", Color.Green);
                    }

                    if (type.Equals("taxonomy"))
                    {
                        Taxonomy taxonomy = (Taxonomy)result.Value;
                        AddObject(listViewResult, taxonomy, $"Taxonomy", Color.Orange);
                    }

                    if (type.Equals("taxonomynode"))
                    {
                        TaxonomyNode taxonomyNode = (TaxonomyNode)result.Value;
                        AddObject(listViewResult, taxonomyNode, $"Taxonomy Node", Color.Aquamarine);
                    }

                    if (type.Equals("enumeration"))
                    {
                        EnumerationList enumeration = (EnumerationList)result.Value;
                        AddObject(listViewResult, enumeration, $"Enumeration", Color.CadetBlue);
                    }

                    if (type.Equals("enumerationitem"))
                    {
                        EnumerationListItem enumerationListItem = (EnumerationListItem)result.Value;
                        AddObject(listViewResult, enumerationListItem, $"Enumeration Item", Color.LightSkyBlue);
                    }

                    if (type.Equals("measureunit"))
                    {
                        MeasureUnit measureUnit = (MeasureUnit)result.Value;
                        AddObject(listViewResult, measureUnit, $"Measure Unit", Color.LightGreen);
                    }

                    if (type.Equals("measureclass"))
                    {
                        MeasureClass measureClass = (MeasureClass)result.Value;
                        AddObject(listViewResult, measureClass, $"Measure Class", Color.SpringGreen);
                    }
                }
            }
        }
    }
}
