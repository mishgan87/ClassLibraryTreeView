using ClassLibraryTreeView.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassLibraryTreeView.Classes
{
    class PermissibleAttributesListView : ListView
    {
        private TextBox textBoxProperty = new TextBox();
        private ListViewItem editedItem = new ListViewItem();
        private ComboBox comboBoxValidationType = new ComboBox();
        private Dictionary<string, IAttribute> attributes = null;
        public PermissibleAttributesListView(Dictionary<string, IAttribute> permissibleAttributes) : base()
        {
            comboBoxValidationType.Visible = false;
            comboBoxValidationType.Items.Add("Unselect");
            comboBoxValidationType.Items.Add("Enumeration");
            comboBoxValidationType.Items.Add("ValueRangeInclusive");
            comboBoxValidationType.Items.Add("RegularExpression");
            comboBoxValidationType.Items.Add("Association");

            this.View = View.Details;
            this.Dock = DockStyle.Fill;
            this.LabelEdit = true;
            this.GridLines = true;
            this.FullRowSelect = true;

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

    }
}
