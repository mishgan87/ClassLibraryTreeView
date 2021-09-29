using ClassLibraryTreeView.Classes;
using ClassLibraryTreeView.Interfaces;
using System;
using System.Collections.Concurrent;
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
        int filterMode = -1;
        DataTable table = null;
        ConceptualModel model = null;
        List<string>[] itemsLists = null;

        public ClassAttributesForm(ConceptualModel modelReference) : base()
        {
            InitializeComponent();

            model = modelReference;
            table = new DataTable();

            table.Columns.Add($"Class ID");
            table.Columns.Add($"Class Name");
            table.Columns.Add($"Attribute ID");
            table.Columns.Add($"Attribute Name");

            itemsLists = new List<string>[4];
            for (int index = 0; index < 4; index++)
            {
                itemsLists[index] = new List<string>();
            }

            dataGridView.CellMouseClick += new DataGridViewCellMouseEventHandler(this.OnHeaderMouseClick);

            FillGridViewByDefault(true);
        }
        private void OnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs eventArgs)
        {
            if (eventArgs.Button == MouseButtons.Left && eventArgs.RowIndex == -1)
            {
                int columnIndex = eventArgs.ColumnIndex;
                filterMode = columnIndex;
                FilterForm filterForm = new FilterForm(dataGridView.Columns[columnIndex].HeaderText, itemsLists[columnIndex]);
                filterForm.ItemsChecked += new EventHandler<List<string>>(this.GetFilter);
                filterForm.Show();
            }
        }
        private void GetFilter(object sender, List<string> filter)
        {
            table.Rows.Clear();
            List<IClass> classList = null;

            if (filterMode == 0)
            {
                classList = model.GetClassesWithId(filter);
                Print(classList);
            }

            if (filterMode == 1)
            {
                classList = model.GetClassesWithName(filter);
                Print(classList);
            }

            if (filterMode == 2)
            {
                classList = model.GetClassesWithAttributeId(filter);
                PrintSingleAttributeId(classList, filter);
            }

            if (filterMode == 3)
            {
                classList = model.GetClassesWithAttributeName(filter);
                PrintSingleAttributeName(classList, filter);
            }
        }
        private void FillGridViewByDefault(bool isInit)
        {
            dataGridView.Enabled = false;
            filterMode = -1;
            table.Rows.Clear();
            foreach (string classMapKey in model.classes.Keys)
            {
                var map = model.classes[classMapKey];
                foreach (IClass cmClass in map.Values)
                {
                    string classId = cmClass.Id;
                    string className = cmClass.Name;
                    
                    if (isInit)
                    {
                        if (!itemsLists[0].Contains(classId))
                        {
                            itemsLists[0].Add(classId);
                        }
                        if (!itemsLists[1].Contains(className))
                        {
                            itemsLists[1].Add(className);
                        }
                    }

                    foreach (IAttribute attribute in cmClass.PermissibleAttributes.Values)
                    {
                        string attributeId = attribute.Id;
                        string attributeName = attribute.Name;

                        table.Rows.Add(new string[] { classId, className, attributeId, attributeName });

                        if (isInit)
                        {
                            if (!itemsLists[2].Contains(attributeId))
                            {
                                itemsLists[2].Add(attributeId);
                            }
                            if (!itemsLists[3].Contains(attributeName))
                            {
                                itemsLists[3].Add(attributeName);
                            }
                        }
                    }
                }
            }
            dataGridView.DataSource = table;
            dataGridView.Enabled = true;
        }
        private void Print(List<IClass> classList)
        {
            table.Rows.Clear();
            foreach (IClass cmClass in classList)
            {
                string classId = cmClass.Id;
                string className = cmClass.Name;

                foreach (IAttribute attribute in cmClass.PermissibleAttributes.Values)
                {
                    string attributeId = attribute.Id;
                    string attributeName = attribute.Name;
                    table.Rows.Add(new string[] { classId, className, attributeId, attributeName });
                }
            }

            dataGridView.DataSource = table;
        }
        private void PrintSingleAttributeId(List<IClass> classList, List<string> filter)
        {
            table.Rows.Clear();
            foreach (IClass cmClass in classList)
            {
                string classId = cmClass.Id;
                string className = cmClass.Name;
                foreach (IAttribute attribute in cmClass.PermissibleAttributes.Values)
                {
                    if (filter.Contains(attribute.Id))
                    {
                        table.Rows.Add(new string[] { classId, className, attribute.Id, attribute.Name });
                    }
                }
            }

            dataGridView.DataSource = table;
        }
        private void PrintSingleAttributeName(List<IClass> classList, List<string> filter)
        {
            table.Rows.Clear();
            foreach (IClass cmClass in classList)
            {
                string classId = cmClass.Id;
                string className = cmClass.Name;

                foreach (IAttribute attribute in cmClass.PermissibleAttributes.Values)
                {
                    if (filter.Contains(attribute.Name))
                    {
                        table.Rows.Add(new string[] { classId, className, attribute.Id, attribute.Name });
                    }
                }
            }

            dataGridView.DataSource = table;
        }
        private void DataGridView_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            // e.Column.SortMode = DataGridViewColumnSortMode.NotSortable;
            e.Column.SortMode = DataGridViewColumnSortMode.Programmatic;
        }
    }
}
