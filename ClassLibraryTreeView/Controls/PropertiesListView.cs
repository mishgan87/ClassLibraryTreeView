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
        private Rectangle ClickedItem = new Rectangle();
        private IIdentifiable parentTag = null;
        public PropertiesListView()
        {
            Init();
        }
        public PropertiesListView(Dictionary<string, ConceptualModelClass> applicableClasses)
        {
            Init();
            ViewApplicableClasses(applicableClasses);
        }
        public PropertiesListView(Dictionary<string, ConceptualModelAttribute> permissibleAttributes)
        {
            Init();
            ViewPermissibleAttributes(permissibleAttributes);
        }
        public PropertiesListView(IIdentifiable element) : base()
        {
            Init();
            ViewElementProperties(element);
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
            this.MouseDoubleClick += new MouseEventHandler(this.OnMouseDoubleClick);

            this.DrawItem += new DrawListViewItemEventHandler(this.OnDrawItem);
        }
        private void OnDrawItem(object sender,  DrawListViewItemEventArgs e)
        {
            e.Item.SubItems[0].BackColor = Color.LightGray;
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

                if (parentTag != null)
                {

                }
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

                ClickedItem = new Rectangle(editedSubItem.Bounds.X, editedSubItem.Bounds.Y, editedSubItem.Bounds.Width, editedSubItem.Bounds.Height);

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

                if (editedItem.Text.ToLower().Equals("xtype") || editedItem.Text.ToLower().Equals("concept"))
                {
                    ShowComboBox(new object[] { "Unselect", "Functional", "Physical", "General", "Document" });
                    return;
                }

                if (this.Columns[editedSubItemIndex].Text.ToLower().Equals("validationtype") || editedItem.Text.ToLower().Equals("validationtype"))
                {
                    ShowComboBox(new object[] { "Unselect", "Enumeration", "ValueRangeInclusive", "RegularExpression", "Association" });
                    return;
                }

                if (this.Columns[editedSubItemIndex].Text.ToLower().Equals("presence") || editedItem.Text.ToLower().Equals("presence"))
                {
                    ShowComboBox(new object[] { "Unselect", "NotApplicable", "Optional", "Preferred", "Required" });
                    return;
                }


                ShowTextBox();
            }
        }
        private void ShowTextBox()
        {
            textBoxProperty.Bounds = ClickedItem;
            textBoxProperty.Text = editedItem.SubItems[editedSubItemIndex].Text;
            textBoxProperty.Visible = true;
            textBoxProperty.BringToFront();
            textBoxProperty.Focus();
        }
        private void ShowComboBox(object[] items)
        {
            comboBoxProperty.Items.Clear();
            comboBoxProperty.Items.AddRange(items);
            comboBoxProperty.Bounds = ClickedItem;
            comboBoxProperty.Text = editedItem.SubItems[editedSubItemIndex].Text;
            comboBoxProperty.Visible = true;
            comboBoxProperty.BringToFront();
            comboBoxProperty.Focus();
        }
        public void ViewApplicableClasses(Dictionary<string, ConceptualModelClass> applicableClasses)
        {
            this.Items.Clear();

            if (applicableClasses == null)
            {
                return;
            }

            KeyValuePair<string, string>[] names = applicableClasses.Values.First().Attributes();
            foreach (KeyValuePair<string, string> name in names)
            {
                this.Columns.Add($"{name.Key}", 150, HorizontalAlignment.Left);
            }

            foreach (ConceptualModelClass cmClass in applicableClasses.Values)
            {
                List<string> items = new List<string>();
                KeyValuePair<string, string>[] properties = cmClass.Attributes();
                foreach (KeyValuePair<string, string> property in properties)
                {
                    items.Add(property.Value);
                }
                ListViewItem item = new ListViewItem(items.ToArray());
                item.Tag = cmClass;
                this.Items.Add(item);
            }
        }
        public void ViewElementProperties(IIdentifiable element)
        {
            this.Items.Clear();

            if (element == null)
            {
                return;
            }

            parentTag = element;

            this.Columns.Add("Property", 300, HorizontalAlignment.Left);
            this.Columns.Add("Value", 300, HorizontalAlignment.Left);

            KeyValuePair<string, string>[] properties = element.Attributes();
            foreach (KeyValuePair<string, string> property in properties)
            {
                string[] items = { $"{property.Key}", $"{property.Value}" };
                ListViewItem item = new ListViewItem(items);
                this.Items.Add(item);
            }
        }
        public void ViewPermissibleAttributes(Dictionary<string, ConceptualModelAttribute> permissibleAttributes)
        {
            this.Items.Clear();

            if (permissibleAttributes.Count == 0)
            {
                return;
            }

            // KeyValuePair<string, string>[] names = permissibleAttributes.Values.First().Attributes();
            List<KeyValuePair<string, string>> names = permissibleAttributes.Values.First().Attributes().ToList();
            names.Insert(0, new KeyValuePair<string, string>("Came From", ""));

            foreach (KeyValuePair<string, string> name in names)
            {
                if (name.Key.Equals("Came From"))
                {
                    this.Columns.Add($"{name.Key}", 300, HorizontalAlignment.Left);
                }
                else
                {
                    this.Columns.Add($"{name.Key}", 150, HorizontalAlignment.Left);
                }
            }

            foreach (ConceptualModelAttribute attribute in permissibleAttributes.Values)
            {
                KeyValuePair<string, string>[] properties = attribute.Attributes();
                List<string> items = new List<string>();
                if (attribute.CameFrom != null)
                {
                    items.Add($"{attribute.CameFrom.Name} : {attribute.CameFrom.Xtype}");
                }
                else
                {
                    items.Add("");
                }
                foreach (KeyValuePair<string, string> property in properties)
                {
                    items.Add(property.Value);
                }

                ListViewItem item = new ListViewItem(items.ToArray());
                item.Tag = attribute;
                if (attribute.CameFrom != null)
                {
                    if (attribute.CameFrom.Xtype.ToLower().Equals("functionals"))
                    {
                        item.BackColor = Color.Aquamarine;
                    }
                    if (attribute.CameFrom.Xtype.ToLower().Equals("physicals"))
                    {
                        item.BackColor = Color.LightBlue;
                    }
                }
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
    }
}
