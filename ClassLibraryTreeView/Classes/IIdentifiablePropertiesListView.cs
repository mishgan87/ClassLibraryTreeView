using ClassLibraryTreeView.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ClassLibraryTreeView.Classes
{
    class IIdentifiablePropertiesListView : ListView
    {
        private ComboBox comboBoxProperty = new ComboBox();
        private TextBox textBoxProperty = new TextBox();
        private ListViewItem editedItem = new ListViewItem();
        public IIdentifiablePropertiesListView(IIdentifiable element) : base()
        {
            comboBoxProperty.Visible = false;
            comboBoxProperty.Items.Add("Functionals");
            comboBoxProperty.Items.Add("Physicals");
            comboBoxProperty.Items.Add("Documents");
            comboBoxProperty.Items.Add("General");
            comboBoxProperty.SelectedValueChanged += new EventHandler(this.PropertyChanged);

            textBoxProperty.Visible = false;
            textBoxProperty.KeyPress += new KeyPressEventHandler(this.PropertyTextBoxKeyPressed);
            textBoxProperty.LostFocus += new EventHandler(this.OnTextBoxLostFocus);

            this.Controls.Add(comboBoxProperty);
            this.Controls.Add(textBoxProperty);

            this.LabelEdit = true;
            this.GridLines = true;
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
                ListViewItem item = new ListViewItem(items);
                this.Items.Add(item);
            }

            this.MouseDoubleClick += new MouseEventHandler(this.OnMouseDoubleClick);
        }
        private void OnTextBoxLostFocus(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            editedItem.SubItems[1].Text = textBox.Text;
            textBox.Hide();
        }
        private void OnMouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                editedItem = this.GetItemAt(e.X, e.Y);
                ListViewItem.ListViewSubItem editedSubItem = editedItem.SubItems[1];
                Rectangle ClickedItem = new Rectangle(editedSubItem.Bounds.X, editedSubItem.Bounds.Y, editedSubItem.Bounds.Width, editedSubItem.Bounds.Height);

                if (editedSubItem.Text.ToLower().Equals("true"))
                {
                    editedSubItem.Text = "false";
                    return;
                }

                if (editedSubItem.Text.ToLower().Equals("false"))
                {
                    editedSubItem.Text = "true";
                    return;
                }

                if (editedItem.Text.ToLower().Equals("xtype"))
                {
                    comboBoxProperty.Bounds = ClickedItem;
                    comboBoxProperty.Text = editedSubItem.Text;
                    comboBoxProperty.Visible = true;
                    comboBoxProperty.BringToFront();
                    comboBoxProperty.Focus();
                    return;
                }

                textBoxProperty.Bounds = ClickedItem;
                textBoxProperty.Text = editedSubItem.Text;
                textBoxProperty.Visible = true;
                textBoxProperty.BringToFront();
                textBoxProperty.Focus();
            }
        }
        private void PropertyChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            editedItem.SubItems[1].Text = comboBox.Text;
            comboBox.Hide();
        }
        private void PropertyTextBoxKeyPressed(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                TextBox textBox = (TextBox)sender;
                editedItem.SubItems[1].Text = textBox.Text;
                textBox.Hide();
            }
        }
    }
}
