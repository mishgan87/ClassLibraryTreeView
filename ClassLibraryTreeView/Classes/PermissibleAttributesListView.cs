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
        private ComboBox comboBoxValidationType = new ComboBox();
        private TextBox textBoxProperty = new TextBox();
        private ListViewItem editedItem = new ListViewItem();
        Dictionary<string, IAttribute> attributes = null;
        public PermissibleAttributesListView(Dictionary<string, IAttribute> permissibleAttributes) : base()
        {
            attributes = permissibleAttributes;

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
        }

    }
}
