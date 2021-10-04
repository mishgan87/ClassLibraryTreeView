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
    public partial class FilterForm : Form
    {
        public event EventHandler<List<string>> ItemsChecked;
        public FilterForm()
        {
            InitializeComponent();
        }
        public FilterForm(string text, List<string> items)
        {
            InitializeComponent();
            this.Text = text;
            listView.Items.Clear();
            foreach (string item in items)
            {
                listView.Items.Add(new ListViewItem(new string[] { $"{item}" }));
            }
        }
        private void BtnApply_Click(object sender, EventArgs e)
        {
            List<string> checkedItems = new List<string>();
            checkedItems.AddRange(from object item in listView.CheckedItems
                            let lvitem = (ListViewItem)item
                            select lvitem.Text);
            this.ItemsChecked?.Invoke(this, checkedItems);
            this.Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
