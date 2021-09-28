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
        ConceptualModel model = null;
        DataTable table = null;

        List<string>[] itemsLists = null;

        List<string> classIdList = null;
        List<string> classNameList = null;
        List<string> attributeIdList = null;
        List<string> attributeNameList = null;

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

            classIdList = new List<string>();
            classNameList = new List<string>();
            attributeIdList = new List<string>();
            attributeNameList = new List<string>();

            dataGridView.CellMouseClick += new DataGridViewCellMouseEventHandler(this.OnHeaderMouseClick);

            FillGridViewByDefault(true);
        }
        private void OnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs eventArgs)
        {
            if (eventArgs.Button == MouseButtons.Right && eventArgs.RowIndex == -1)
            {
                int columnIndex = eventArgs.ColumnIndex;

                ComboBox comboBox = new ComboBox();
                comboBox.Items.AddRange(itemsLists[columnIndex].ToArray());
                comboBox.Bounds = new Rectangle(
                    this.dataGridView.Columns[columnIndex].HeaderCell.ContentBounds.X,
                    this.dataGridView.Columns[columnIndex].HeaderCell.ContentBounds.Y,
                    this.dataGridView.Columns[columnIndex].HeaderCell.ContentBounds.Width,
                    this.dataGridView.Columns[columnIndex].HeaderCell.ContentBounds.Height
                    );
                // comboBox.Items.Clear();
                // comboBox.Items.AddRange(items);
                // comboBox.Bounds = ClickedItem;
                // comboBox.Text = editedItem.SubItems[editedSubItemIndex].Text;
                comboBox.Visible = true;
                comboBox.BringToFront();
                comboBox.Focus();
            }
        }
        private void FillGridViewByDefault(bool isInit)
        {
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
                        if (!itemsLists[0].Contains(classId)) // if (!classIdList.Contains(classId))
                        {
                            classIdList.Add(classId);
                            comboBoxClassId.Items.Add(classId);
                            itemsLists[0].Add(classId);
                        }
                        if (!itemsLists[1].Contains(className)) // if (!classNameList.Contains(className))
                        {
                            classNameList.Add(className);
                            comboBoxClassName.Items.Add(className);
                            itemsLists[1].Add(className);
                        }
                    }

                    foreach (IAttribute attribute in cmClass.PermissibleAttributes.Values)
                    {
                        string attributeId = attribute.Id;
                        string attributeName = attribute.Name;
                        /*
                        item.Tag = attribute;
                        if (attribute.CameFrom != null)
                        {
                            item.BackColor = Color.Yellow;
                        }
                        this.Items.Add(item);
                        */
                        table.Rows.Add(new string[] { classId, className, attributeId, attributeName });

                        if (isInit)
                        {
                            if (!itemsLists[2].Contains(attributeId)) // if (!attributeIdList.Contains(attributeId))
                            {
                                attributeIdList.Add(attributeId);
                                comboBoxAttributeId.Items.Add(attributeId);
                                itemsLists[2].Add(attributeId);
                            }
                            if (!itemsLists[3].Contains(attributeName)) // if (!attributeNameList.Contains(attributeName))
                            {
                                attributeNameList.Add(attributeName);
                                comboBoxAttributeName.Items.Add(attributeName);
                                itemsLists[3].Add(attributeName);
                            }
                        }
                    }
                }
            }
            dataGridView.DataSource = table;

            comboBoxClassId.Enabled = false;
            comboBoxClassName.Enabled = false;
            comboBoxAttributeId.Enabled = false;
            comboBoxAttributeName.Enabled = false;
        }
        private void ApplyFilter()
        {
            table.Rows.Clear();
            List<IClass> classList = null;

            if (checkBoxFilterByClassId.Checked)
            {
                string text = comboBoxClassId.Items[comboBoxClassId.SelectedIndex].ToString();
                classList = model.GetClassesWithId(text);
                Print(classList);
            }

            if (checkBoxFilterByClassName.Checked)
            {
                string text = comboBoxClassName.Items[comboBoxClassName.SelectedIndex].ToString();
                classList = model.GetClassesWithName(text);
                Print(classList);
            }

            if (checkBoxFilterByAttributeId.Checked)
            {
                string text = comboBoxAttributeId.Items[comboBoxAttributeId.SelectedIndex].ToString();
                classList = model.GetClassesWithAttributeId(text);
                PrintSingleAttributeId(classList, text);
            }

            if (checkBoxFilterByAttributeName.Checked)
            {
                string text = comboBoxAttributeName.Items[comboBoxAttributeName.SelectedIndex].ToString();
                classList = model.GetClassesWithAttributeName(text);
                PrintSingleAttributeName(classList, text);
            }
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
        private void PrintSingleAttributeId(List<IClass> classList, string attributeId)
        {
            table.Rows.Clear();
            foreach (IClass cmClass in classList)
            {
                string classId = cmClass.Id;
                string className = cmClass.Name;
                foreach (IAttribute attribute in cmClass.PermissibleAttributes.Values)
                {
                    if (attribute.Id.Equals(attributeId))
                    {
                        table.Rows.Add(new string[] { classId, className, attribute.Id, attribute.Name });
                    }
                }
            }

            dataGridView.DataSource = table;
        }
        private void PrintSingleAttributeName(List<IClass> classList, string attributeName)
        {
            table.Rows.Clear();
            foreach (IClass cmClass in classList)
            {
                string classId = cmClass.Id;
                string className = cmClass.Name;

                foreach (IAttribute attribute in cmClass.PermissibleAttributes.Values)
                {
                    if (attribute.Name.Equals(attributeName))
                    {
                        table.Rows.Add(new string[] { classId, className, attribute.Id, attribute.Name });
                    }
                }
            }

            dataGridView.DataSource = table;
        }
        private void CheckBoxCheckedChanged(object sender, EventArgs e)
        {
            comboBoxClassId.Enabled = false;
            comboBoxClassName.Enabled = false;
            comboBoxAttributeId.Enabled = false;
            comboBoxAttributeName.Enabled = false;

            comboBoxClassId.Text = "";
            comboBoxClassName.Text = "";
            comboBoxAttributeId.Text = "";
            comboBoxAttributeName.Text = "";

            if (checkBoxFilterByClassId.Focused && checkBoxFilterByClassId.Checked)
            {
                checkBoxFilterByClassName.Checked = false;
                checkBoxFilterByAttributeId.Checked = false;
                checkBoxFilterByAttributeName.Checked = false;

                comboBoxClassId.Enabled = true;
                return;
            }

            if (checkBoxFilterByClassName.Focused && checkBoxFilterByClassName.Checked)
            {
                checkBoxFilterByClassId.Checked = false;
                checkBoxFilterByAttributeId.Checked = false;
                checkBoxFilterByAttributeName.Checked = false;
                comboBoxClassName.Enabled = true;
                return;
            }

            if (checkBoxFilterByAttributeId.Focused && checkBoxFilterByAttributeId.Checked)
            {
                checkBoxFilterByClassId.Checked = false;
                checkBoxFilterByClassName.Checked = false;
                checkBoxFilterByAttributeName.Checked = false;
                comboBoxAttributeId.Enabled = true;
                return;
            }

            if (checkBoxFilterByAttributeName.Focused && checkBoxFilterByAttributeName.Checked)
            {
                checkBoxFilterByClassId.Checked = false;
                checkBoxFilterByClassName.Checked = false;
                checkBoxFilterByAttributeId.Checked = false;
                comboBoxAttributeName.Enabled = true;
                return;
            }
        }

        private void BtnResetFilter_Click(object sender, EventArgs e)
        {
            table.Rows.Clear();

            checkBoxFilterByClassId.Checked = false;
            checkBoxFilterByClassName.Checked = false;
            checkBoxFilterByAttributeId.Checked = false;
            checkBoxFilterByAttributeName.Checked = false;

            FillGridViewByDefault(false);
        }

        private void СomboBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }
    }
}
