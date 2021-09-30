using ClassLibraryTreeView.Forms;
using ClassLibraryTreeView.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassLibraryTreeView.Classes
{
    class AttributesGrid : DataGridView
    {
        int filterMode = -1;
        ConceptualModel model = null;
        List<string>[] itemsLists = null;
        public AttributesGrid(ConceptualModel modelReference) : base()
        {
            this.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.Dock = DockStyle.Fill;
            this.ColumnAdded += new DataGridViewColumnEventHandler(this.ColumnAddedAdvance);
            this.CellMouseClick += new DataGridViewCellMouseEventHandler(this.OnHeaderMouseClick);

            model = modelReference;

            itemsLists = new List<string>[4];
            for (int index = 0; index < 4; index++)
            {
                itemsLists[index] = new List<string>();
            }

            FillGridViewByDefault(true);
        }
        private DataTable NewDataTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add($"Class ID");
            table.Columns.Add($"Class Name");
            table.Columns.Add($"Attribute ID");
            table.Columns.Add($"Attribute Name");
            return table;
        }
        private void OnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs eventArgs)
        {
            if (eventArgs.Button == MouseButtons.Left && eventArgs.RowIndex == -1)
            {
                int columnIndex = eventArgs.ColumnIndex;
                filterMode = columnIndex;
                if (columnIndex >= 0)
                {
                    FilterForm filterForm = new FilterForm(this.Columns[columnIndex].HeaderText, itemsLists[columnIndex]);
                    filterForm.ItemsChecked += new EventHandler<List<string>>(this.GetFilter);
                    filterForm.Show();
                }
                else
                {
                    FillGridViewByDefault(true);
                }
            }
        }
        private void GetFilter(object sender, List<string> filter)
        {
            if (filterMode == 0)
            {
                Print(model.GetClassesWithId(filter));
            }

            if (filterMode == 1)
            {
                Print(model.GetClassesWithName(filter));
            }

            if (filterMode == 2)
            {
                PrintSingleAttributeId(model.GetClassesWithAttributeId(filter), filter);
            }

            if (filterMode == 3)
            {
                PrintSingleAttributeName(model.GetClassesWithAttributeName(filter), filter);
            }
        }
        private void FillGridViewByDefault(bool isInit)
        {
            filterMode = -1;
            DataTable table = NewDataTable();
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
            this.DataSource = table;
        }
        private void Print(List<IClass> classList)
        {
            DataTable table = NewDataTable();
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

            this.DataSource = table;
        }
        private void PrintSingleAttributeId(List<IClass> classList, List<string> filter)
        {
            DataTable table = NewDataTable();
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

            this.DataSource = table;
        }
        private void PrintSingleAttributeName(List<IClass> classList, List<string> filter)
        {
            DataTable table = NewDataTable();
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

            this.DataSource = table;
        }
        private void ColumnAddedAdvance(object sender, DataGridViewColumnEventArgs e)
        {
            // e.Column.SortMode = DataGridViewColumnSortMode.NotSortable;
            e.Column.SortMode = DataGridViewColumnSortMode.Programmatic;
        }
    }
}
