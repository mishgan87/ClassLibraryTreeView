using ClassLibraryTreeView.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassLibraryTreeView.Classes
{
    class PropertiesListView : ListView
    {
        private TextBox textBoxProperty = new TextBox();
        private ListViewItem editedItem = new ListViewItem();
        private int editedSubItemIndex = -1;
        private ComboBox comboBoxProperty = new ComboBox();
        private Dictionary<string, IAttribute> attributes = null;
        public PropertiesListView(IIdentifiable element) : base()
        {
            Init();

            if (element == null)
            {
                return;
            }

            KeyValuePair<string, string>[] properties = element.Attributes();
            foreach (KeyValuePair<string, string> property in properties)
            {
                string[] items = { $"{property.Key}", $"{property.Value}" };
                ListViewItem item = new ListViewItem(items);
                this.Items.Add(item);
            }

            
        }
        public PropertiesListView(Dictionary<string, IAttribute> permissibleAttributes) : base()
        {
            Init();

            if (permissibleAttributes.Count == 0)
            {
                return;
            }

            this.attributes = permissibleAttributes;

            KeyValuePair<string, string>[] names = this.attributes.First().Value.Attributes();
            foreach (KeyValuePair<string, string> name in names)
            {
                this.Columns.Add($"{name.Key}", 150, HorizontalAlignment.Left);
            }

            foreach (IAttribute attribute in attributes.Values)
            {
                KeyValuePair<string, string>[] properties = attribute.Attributes();
                List<string> items = new List<string>();
                foreach (KeyValuePair<string, string> property in properties)
                {
                    items.Add(property.Value);
                }

                ListViewItem item = new ListViewItem(items.ToArray());
                item.Tag = attribute;
                this.Items.Add(item);

                if (attribute.ValidationType.ToLower().Equals("association"))
                {
                    string[] rules = ConceptualModel.SplitValidationRules(attribute.ValidationRule);
                    string concept = rules[1];

                    for (int index = 2; index < rules.Length; index++)
                    {
                        items.Clear();
                        foreach (KeyValuePair<string, string> property in properties)
                        {
                            string value = "";
                            if (property.Key.ToLower().Equals("validationrule"))
                            {
                                value = rules[index];
                            }
                            items.Add(value);
                        }
                        this.Items.Add(new ListViewItem(items.ToArray()));
                    }
                }
            }
        }
        private void Init()
        {
            comboBoxProperty.Visible = false;
            comboBoxProperty.SelectedValueChanged += new EventHandler(this.PropertyChanged);
            comboBoxProperty.LostFocus += new EventHandler(this.OnComboBoxLostFocus);

            textBoxProperty.Visible = false;
            textBoxProperty.KeyPress += new KeyPressEventHandler(this.PropertyTextBoxKeyPressed);
            textBoxProperty.LostFocus += new EventHandler(this.OnTextBoxLostFocus);

            this.Controls.Add(comboBoxProperty);
            this.Controls.Add(textBoxProperty);

            this.LabelEdit = true;
            this.GridLines = true;
            this.Dock = DockStyle.Fill;
            this.View = View.Details;
            this.FullRowSelect = true;
            this.Columns.Add("Property", 300, HorizontalAlignment.Left);
            this.Columns.Add("Value", 300, HorizontalAlignment.Left);
            this.MouseDoubleClick += new MouseEventHandler(this.OnMouseDoubleClick);
        }
        private void PropertyChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            editedItem.SubItems[editedSubItemIndex].Text = comboBox.Text;
            comboBox.Hide();
        }
        private void OnTextBoxLostFocus(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            editedItem.SubItems[editedSubItemIndex].Text = textBox.Text;
            textBox.Hide();
        }
        private void OnComboBoxLostFocus(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            editedItem.SubItems[editedSubItemIndex].Text = comboBox.Text;
            comboBox.Hide();
        }
        private void PropertyTextBoxKeyPressed(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                TextBox textBox = (TextBox)sender;
                editedItem.SubItems[editedSubItemIndex].Text = textBox.Text;
                textBox.Hide();
            }
        }
        private void OnMouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                editedItem = this.GetItemAt(e.X, e.Y);
                ListViewHitTestInfo listViewHitTestInfo = this.HitTest(e.X, e.Y);
                editedSubItemIndex = listViewHitTestInfo.Item.SubItems.IndexOf(listViewHitTestInfo.SubItem);

                if (editedSubItemIndex == 0)
                {
                    return;
                }

                ListViewItem.ListViewSubItem editedSubItem = editedItem.SubItems[editedSubItemIndex];

                Rectangle ClickedItem = new Rectangle(editedSubItem.Bounds.X, editedSubItem.Bounds.Y, editedSubItem.Bounds.Width, editedSubItem.Bounds.Height);

                if (editedSubItem.Text.ToLower().Equals("true"))
                {
                    editedSubItem.Text = "False";
                    return;
                }

                if (editedSubItem.Text.ToLower().Equals("false"))
                {
                    editedSubItem.Text = "True";
                    return;
                }

                if (editedItem.Text.ToLower().Equals("xtype"))
                {
                    comboBoxProperty.Items.Clear();
                    comboBoxProperty.Items.Add("Unselect");
                    comboBoxProperty.Items.Add("Functional");
                    comboBoxProperty.Items.Add("Physical");
                    comboBoxProperty.Items.Add("General");
                    comboBoxProperty.Items.Add("Document");

                    comboBoxProperty.Bounds = ClickedItem;
                    comboBoxProperty.Text = editedSubItem.Text;
                    comboBoxProperty.Visible = true;
                    comboBoxProperty.BringToFront();
                    comboBoxProperty.Focus();
                    return;
                }

                if (this.Columns[editedSubItemIndex].Text.ToLower().Equals("validationtype"))
                {
                    comboBoxProperty.Items.Clear();
                    comboBoxProperty.Items.Add("Unselect");
                    comboBoxProperty.Items.Add("Enumeration");
                    comboBoxProperty.Items.Add("ValueRangeInclusive");
                    comboBoxProperty.Items.Add("RegularExpression");
                    comboBoxProperty.Items.Add("Association");

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
    }
}
