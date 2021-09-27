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
        public ClassAttributesForm(ConceptualModel modelReference) : base()
        {
            InitializeComponent();

            model = modelReference;

            table = new DataTable();

            table.Columns.Add($"Class ID");
            table.Columns.Add($"Class Name");
            table.Columns.Add($"Attribute ID");
            table.Columns.Add($"Attribute Name");

            foreach(string classMapKey in model.classes.Keys)
            {
                var map = model.classes[classMapKey];
                foreach (IClass cmClass in map.Values)
                {
                    string classId = cmClass.Id;
                    string className = cmClass.Name;
                    foreach (IAttribute attribute in cmClass.PermissibleAttributes.Values)
                    {
                        string attributeId = attribute.Id;
                        string attributeName = attribute.Name;
                        table.Rows.Add(new string[] { classId, className, attributeId, attributeName });

                        if (!comboBoxClassId.Items.Contains(classId))
                        {
                            comboBoxClassId.Items.Add(classId);
                        }

                        if (!comboBoxClassName.Items.Contains(className))
                        {
                            comboBoxClassName.Items.Add(className);
                        }

                        if (!comboBoxAttributeId.Items.Contains(attributeId))
                        {
                            comboBoxAttributeId.Items.Add(attributeId);
                        }

                        if (!comboBoxAttributeName.Items.Contains(attributeName))
                        {
                            comboBoxAttributeName.Items.Add(attributeName);
                        }
                    }
                }
            }

            dataGridView.DataSource = table;
        }

        private void BtnApplyFilter_Click(object sender, EventArgs e)
        {

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
        private void ComboBoxClassId_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<IClass> classList = model.GetClassesWithId(comboBoxClassId.Text);
            Print(classList);
        }

        private void ComboBoxClassName_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<IClass> classList = model.GetClassesWithName(comboBoxClassName.Text);
            Print(classList);
        }

        private void ComboBoxAttributeId_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<IClass> classList = model.GetClassesWithAttributeId(comboBoxAttributeId.Text);
            PrintSingleAttributeId(classList, comboBoxAttributeId.Text);
        }

        private void ComboBoxAttributeName_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<IClass> classList = model.GetClassesWithAttributeName(comboBoxAttributeName.Text);
            PrintSingleAttributeName(classList, comboBoxAttributeName.Text);
        }
    }
}
