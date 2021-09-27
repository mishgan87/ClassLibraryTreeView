using ClassLibraryTreeView.Classes;
using ClassLibraryTreeView.Interfaces;
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
        ConceptualModel model = null;
        DataTable table = null;
        
        List<string> classIdList = null;
        List<string> classNameList = null;
        List<string> attributeIdList = null;
        List<string> attributeNameList = null;

        public ClassAttributesForm(ConceptualModel modelReference) : base()
        {
            InitializeComponent();

            listViewFIlter.Columns.Add($"ID", 300, HorizontalAlignment.Left);

            model = modelReference;

            table = new DataTable();

            table.Columns.Add($"Class ID");
            table.Columns.Add($"Class Name");
            table.Columns.Add($"Attribute ID");
            table.Columns.Add($"Attribute Name");

            classIdList = new List<string>();
            classNameList = new List<string>();
            attributeIdList = new List<string>();
            attributeNameList = new List<string>();

            foreach (string classMapKey in model.classes.Keys)
            {
                var map = model.classes[classMapKey];
                foreach (IClass cmClass in map.Values)
                {
                    string classId = cmClass.Id;
                    string className = cmClass.Name;

                    classIdList.Add(classId);
                    classNameList.Add(className);

                    foreach (IAttribute attribute in cmClass.PermissibleAttributes.Values)
                    {
                        string attributeId = attribute.Id;
                        string attributeName = attribute.Name;
                        table.Rows.Add(new string[] { classId, className, attributeId, attributeName });

                        attributeIdList.Add(attributeId);
                        attributeNameList.Add(attributeName);
                    }
                }
            }

            dataGridView.DataSource = table;
        }

        private void BtnApplyFilter_Click(object sender, EventArgs e)
        {
            List<IClass> classList = null;

            if (checkBoxFilterByClassId.Checked)
            {
                classList = model.GetClassesWithId(listViewFIlter.SelectedItems[0].Text);
                Print(classList);
            }

            if (checkBoxFilterByClassName.Checked)
            {
                classList = model.GetClassesWithName(listViewFIlter.SelectedItems[0].Text);
                Print(classList);
            }

            if (checkBoxFilterByAttributeId.Checked)
            {
                classList = model.GetClassesWithAttributeId(listViewFIlter.SelectedItems[0].Text);
                PrintSingleAttributeId(classList, listViewFIlter.SelectedItems[0].Text);
            }

            if (checkBoxFilterByAttributeName.Checked)
            {
                classList = model.GetClassesWithAttributeName(listViewFIlter.SelectedItems[0].Text);
                PrintSingleAttributeName(classList, listViewFIlter.SelectedItems[0].Text);
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
        private void FillFilterListView(List<string> values)
        {
            foreach (string value in values)
            {
                string[] items = new string[] { $"{value}" };
                ListViewItem item = new ListViewItem(items);
                listViewFIlter.Items.Add(item);
            }
        }
        private void CheckBoxCheckedChanged(object sender, EventArgs e)
        {
            listViewFIlter.Items.Clear();

            if (checkBoxFilterByClassId.Focused && checkBoxFilterByClassId.Checked)
            {
                checkBoxFilterByClassName.Checked = false;
                checkBoxFilterByAttributeId.Checked = false;
                checkBoxFilterByAttributeName.Checked = false;
                FillFilterListView(classIdList);
                return;
            }

            if (checkBoxFilterByClassName.Focused && checkBoxFilterByClassName.Checked)
            {
                checkBoxFilterByClassId.Checked = false;
                checkBoxFilterByAttributeId.Checked = false;
                checkBoxFilterByAttributeName.Checked = false;
                FillFilterListView(classNameList);
                return;
            }

            if (checkBoxFilterByAttributeId.Focused && checkBoxFilterByAttributeId.Checked)
            {
                checkBoxFilterByClassId.Checked = false;
                checkBoxFilterByClassName.Checked = false;
                checkBoxFilterByAttributeName.Checked = false;
                FillFilterListView(attributeIdList);
                return;
            }

            if (checkBoxFilterByAttributeName.Focused && checkBoxFilterByAttributeName.Checked)
            {
                checkBoxFilterByClassId.Checked = false;
                checkBoxFilterByClassName.Checked = false;
                checkBoxFilterByAttributeId.Checked = false;
                FillFilterListView(attributeNameList);
                return;
            }
        }
    }
}
