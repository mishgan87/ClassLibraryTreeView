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
        public SearchingForm(ConceptualModel modelReference) : base()
        {
            InitializeComponent();

            listViewResult.Columns.Add("ID", 300, HorizontalAlignment.Left);
            listViewResult.Columns.Add("Name", 300, HorizontalAlignment.Left);

            model = modelReference;
        }
        private void Find()
        {
            listViewResult.Items.Clear();
            string text = searchString.Text;
            FindClasses(text);
            FindAttributes(text);
        }
        private void FindClasses(string text)
        {
            if (!searchClasses.Checked)
            {
                return;
            }

            List<IClass> classesList = new List<IClass>();

            foreach (Dictionary<string, IClass> map in model.classes.Values)
            {
                foreach (IClass cmClass in map.Values)
                {
                    if ((cmClass.Id.Contains(text) && searchId.Checked)
                        || (cmClass.Name.Contains(text) && searchName.Checked))
                    {
                        classesList.Add(cmClass);
                    }
                }
            }

            if (classesList.Count > 0)
            {
                foreach (IClass cmClass in classesList)
                {
                    ListViewItem listViewItem = new ListViewItem(new string[] { $"{cmClass.Id}", $"{cmClass.Name}" });
                    listViewItem.Tag = cmClass;
                    listViewItem.BackColor = Color.Yellow;
                    listViewResult.Items.Add(listViewItem);
                }
            }
        }
        private void FindAttributes(string text)
        {
            if (!searchAttributes.Checked)
            {
                return;
            }

            List<IAttribute> attributesList = new List<IAttribute>();

            foreach (var map in model.attributes.Values)
            {
                foreach (var attribute in map.Values)
                {
                    if ((attribute.Id.Contains(text) && searchId.Checked)
                        || (attribute.Name.Contains(text) && searchName.Checked))
                    {
                        attributesList.Add(attribute);
                    }
                }
            }

            if (attributesList.Count > 0)
            {
                foreach (IAttribute attribute in attributesList)
                {
                    ListViewItem listViewItem = new ListViewItem(new string[] { $"{attribute.Id}", $"{attribute.Name}" });
                    listViewItem.Tag = attribute;
                    listViewItem.BackColor = Color.Green;
                    listViewResult.Items.Add(listViewItem);
                }
            }
        }
        ConceptualModel model = null;

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            Find();
        }
    }
}
