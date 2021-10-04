using ClassLibraryTreeView.Forms;
using ClassLibraryTreeView.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassLibraryTreeView.Classes
{
    class AttributesGrid : DataGridView
    {
        int columnIndex = -1;
        List<string>[] items = new List<string>[4];
        public AttributesGrid(ConceptualModel model) : base()
        {
            this.Dock = DockStyle.Fill;
            this.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.ColumnAdded += new DataGridViewColumnEventHandler(this.ColumnAddedAdvance);
            this.CellMouseClick += new DataGridViewCellMouseEventHandler(this.OnHeaderMouseClick);

            for (int index = 0; index < 4; index++)
            {
                items[index] = new List<string>();
            }

            foreach (string classMapKey in model.classes.Keys)
            {
                var map = model.classes[classMapKey];
                foreach (CMClass cmClass in map.Values)
                {
                    foreach (CMAttribute attribute in cmClass.PermissibleAttributes.Values)
                    {
                        // items.Add(new string[] { cmClass.Id , cmClass.Name, attribute.Id, attribute.Name });
                        items[0].Add(cmClass.Id);
                        items[1].Add(cmClass.Name);
                        items[2].Add(attribute.Id);
                        items[3].Add(attribute.Name);
                    }
                }
            }

            ApplyFilter(this, new List<string>());
        }
        private void OnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs eventArgs)
        {
            if (eventArgs.Button == MouseButtons.Left && eventArgs.RowIndex == -1)
            {
                columnIndex = eventArgs.ColumnIndex;
                if (columnIndex >= 0)
                {
                    // List<string> list = items[columnIndex].Distinct().ToList();
                    List<string> list = new HashSet<string>(items[columnIndex]).ToList();
                    FilterForm filterForm = new FilterForm(this.Columns[columnIndex].HeaderText, list);
                    filterForm.ItemsChecked += new EventHandler<List<string>>(this.ApplyFilter);
                    filterForm.Show();
                }
                else
                {
                    ApplyFilter(this, new List<string>());
                }
            }
        }
        private void ApplyFilter(object sender, List<string> filter)
        {
            DataTable table = new DataTable();
            table.Columns.Add($"Class ID");
            table.Columns.Add($"Class Name");
            table.Columns.Add($"Attribute ID");
            table.Columns.Add($"Attribute Name");
            for (int row = 0; row < items[0].Count; row++)
            {
                if (filter.Count == 0 || filter.Contains(items[columnIndex][row]))
                {
                    table.Rows.Add(new string[] { items[0][row], items[1][row], items[2][row], items[3][row] });
                }
            }
            this.DataSource = table;
        }
        private void ColumnAddedAdvance(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.SortMode = DataGridViewColumnSortMode.Programmatic;
        }
    }
}
