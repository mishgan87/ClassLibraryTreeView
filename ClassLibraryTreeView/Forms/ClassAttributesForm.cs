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
    public partial class ClassAttributesForm : Form
    {
        public ClassAttributesForm(List<KeyValuePair<int, string[]>> rows) : base()
        {
            InitializeComponent();

            DataTable table = new DataTable();

            table.Columns.Add($"Class ID");
            table.Columns.Add($"Class Name");
            table.Columns.Add($"Attribute ID");
            table.Columns.Add($"Attribute Name");

            foreach (KeyValuePair<int, string[]> classRow in rows)
            {
                table.Rows.Add(classRow.Value);
            }

            dataGridView.DataSource = table;

        }

        private void BtnApplyFilter_Click(object sender, EventArgs e)
        {

        }

        private void ComboBoxColumn_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> values = new List<string>();

            for(int row = 0; row < dataGridView.Rows.Count; row++)
            {
                int col = comboBoxColumn.SelectedIndex;
                string value = (string)dataGridView.Rows[row].Cells[col].Value;
                values.Add(value);
            }

            comboBoxValue.Items.Clear();
            comboBoxValue.Items.AddRange(values.ToArray());
        }
    }
}
