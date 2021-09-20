using ClassLibraryTreeView.Interfaces;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ClassLibraryTreeView.Classes
{
    class IIdentifiablePropertiesListView : ListView
    {
        public IIdentifiablePropertiesListView(IIdentifiable element) : base()
        {
            this.LabelEdit = true;
            this.Dock = DockStyle.Fill;
            this.View = View.Details;
            this.HeaderStyle = ColumnHeaderStyle.None;
            this.FullRowSelect = true;
            this.Columns.Add("Property", 300, HorizontalAlignment.Left);
            this.Columns.Add("Value", 300, HorizontalAlignment.Left);

            if (element == null)
            {
                return;
            }

            KeyValuePair<string, string>[] properties = element.Attributes();
            foreach(KeyValuePair<string, string> property in properties)
            {
                string[] items = { $"{property.Key}", $"{property.Value}" };
                this.Items.Add(new ListViewItem(items));
            }

            this.MouseDoubleClick += new MouseEventHandler(this.OnMouseDoubleClick);
        }
        private void OnMouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ListView listView = (ListView)sender;
                ListViewHitTestInfo info = listView.HitTest(e.X, e.Y);
                ListViewItem item = info.Item;
                item.BeginEdit();
            }
        }
    }
}
