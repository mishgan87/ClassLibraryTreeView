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
        }
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            listViewResult.Items.Clear();
            string text = searchString.Text;
            int filter = 0;

            List<KeyValuePair<string, object>> results = CMSearcher.SearchText(text, model, filter);

            if (results.Count > 0)
            {
                foreach (KeyValuePair<string, object> result in results)
                {
                    string type = result.Key;
                    ListViewItem listViewItem = null;

                    if (type.Equals("class"))
                    {
                        IClass cmClass = (IClass)result.Value;
                        listViewItem = new ListViewItem(new string[] { $"{cmClass.Id}", $"{cmClass.Name}" });
                        listViewItem.Tag = cmClass;
                        listViewItem.BackColor = Color.Yellow;
                    }

                    if (type.Equals("attribute"))
                    {
                        IAttribute attribute = (IAttribute)result.Value;
                        listViewItem = new ListViewItem(new string[] { $"{attribute.Id}", $"{attribute.Name}" });
                        listViewItem.Tag = attribute;
                        listViewItem.BackColor = Color.Green;
                    }

                    listViewResult.Items.Add(listViewItem);
                }
            }
        }
    }
}
