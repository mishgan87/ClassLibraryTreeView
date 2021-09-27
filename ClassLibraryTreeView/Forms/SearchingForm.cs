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
            model = modelReference;
        }
        private void Find()
        {
            if (checkBoxClasses.Checked)
            {
                FindInClasses();
            }

            if (checkBoxAttributes.Checked)
            {
                FindInAttributes();
            }
        }
        private void PrintItem(IIdentifiable item)
        {
            KeyValuePair<string, string>[] attributes = item.Attributes();
            List<string> properties = new List<string>();
            foreach (KeyValuePair<string, string> property in attributes)
            {
                properties.Add(property.Value);
            }

            ListViewItem listViewItem = new ListViewItem(properties.ToArray());

            listViewResult.Items.Add(listViewItem);
        }
        private void FindInClasses()
        {
            List<IClass> classesList = new List<IClass>();
            string text = textBox.Text;
            if (checkBoxId.Checked)
            {
                foreach(var map in model.classes.Values)
                {
                    if(map.ContainsKey(text))
                    {
                        classesList.Add(map[text]);
                    }
                }
            }

            if (classesList.Count == 0)
            {
                return;
            }

            KeyValuePair<string, string>[] columnsNames = classesList[0].Attributes();
            foreach (KeyValuePair<string, string> columnName in columnsNames)
            {
                listViewResult.Columns.Add($"{columnName.Key}", 150, HorizontalAlignment.Left);
            }

            listViewResult.Items.Clear();

            foreach (IClass cmClass in classesList)
            {
                PrintItem(cmClass);
            }
        }
        private void FindInAttributes()
        {
            string text = textBox.Text;
            List<IAttribute> attributesList = new List<IAttribute>();

            if (checkBoxId.Checked)
            {
                foreach (var map in model.attributes.Values)
                {
                    if (map.ContainsKey(text))
                    {
                        attributesList.Add(map[text]);
                    }
                }
            }

            if (attributesList.Count == 0)
            {
                return;
            }

            KeyValuePair<string, string>[] columnsNames = attributesList[0].Attributes();
            foreach (KeyValuePair<string, string> columnName in columnsNames)
            {
                listViewResult.Columns.Add($"{columnName.Key}", 150, HorizontalAlignment.Left);
            }

            listViewResult.Items.Clear();

            foreach (IAttribute attribute in attributesList)
            {
                PrintItem(attribute);
            }
        }
        ConceptualModel model = null;

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            Find();
        }
        private void OnMouseDoubleClick(object sender, MouseEventArgs e)
        {

        }
    }
}
