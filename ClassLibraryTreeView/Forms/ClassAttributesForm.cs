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
        public ClassAttributesForm(ConceptualModel modelReference) : base()
        {
            InitializeComponent();

            model = modelReference;

            DataTable table = new DataTable();

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

        private void ComboBoxClassId_SelectedIndexChanged(object sender, EventArgs e)
        {
            string text = comboBoxClassId.Text;

            DataTable table = new DataTable();

            table.Columns.Add($"Class ID");
            table.Columns.Add($"Class Name");
            table.Columns.Add($"Attribute ID");
            table.Columns.Add($"Attribute Name");
            foreach (var (classId, className, attributeId, attributeName) in from Dictionary<string, IClass> map in model.classes.Values
                                                                             where map.ContainsKey(text)
                                                                             let cmClass = map[text]
                                                                             let classId = cmClass.Id
                                                                             let className = cmClass.Name
                                                                             from IAttribute attribute in cmClass.PermissibleAttributes.Values
                                                                             let attributeId = attribute.Id
                                                                             let attributeName = attribute.Name
                                                                             select (classId, className, attributeId, attributeName))
            {
                table.Rows.Add(new string[] { classId, className, attributeId, attributeName });
            }

            dataGridView.DataSource = table;
        }
    }
}
