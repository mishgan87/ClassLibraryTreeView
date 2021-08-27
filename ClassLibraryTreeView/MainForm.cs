using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Color = DocumentFormat.OpenXml.Spreadsheet.Color;
using Font = DocumentFormat.OpenXml.Spreadsheet.Font;

namespace ClassLibraryTreeView
{
    public partial class MainForm : Form
    {
        private ConceptualModel model;
        string fileName;
        public MainForm()
        {
            InitializeComponent();
            model = new ConceptualModel();
            fileName = "";
        }
        public bool OpenFile()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "XML files (*.xml)|*.xml";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    fileName = openFileDialog.FileName;
                    model.ImportXml(openFileDialog.FileName);
                    string text = fileName;
                    text = text.Remove(text.LastIndexOf("."), text.Length - text.LastIndexOf("."));
                    text = text.Substring(text.LastIndexOf("\\") + 1, text.Length - text.LastIndexOf("\\") - 1);
                    this.Text = $"{text}";
                }
            }
            return true;
        }
        private void ButtonOpenFile_Click(object sender, EventArgs e)
        {
            if (OpenFile())
            {
                ClassLibraryTreeView treeView = new ClassLibraryTreeView();
                treeView.Nodes.Clear();
                treeView.AddClass(model.docs, "Documents");
                treeView.AddClass(model.func, "Functionals");
                treeView.AddClass(model.phys, "Physicals");
                treeView.Dock = DockStyle.Fill;
                treeView.NodeMouseDoubleClick += new TreeNodeMouseClickEventHandler(this.TreeView_NodeMouseDoubleClick);

                TabPage page = new TabPage("Classes");
                page.Controls.Add(treeView);

                tabControl.TabPages.Add(page);

            }
        }

        private void TreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Tag != null)
            {
                CMClass cmClass = model.GetClass(e.Node.Tag.ToString(), e.Node.Name.ToLower());
                ShowProperties(cmClass);
            }
        }
        private void ShowProperties(CMClass cmClass)
        {
            if (cmClass == null)
            {
                return;
            }

            tabControlProperties.TabPages.Clear();

            ListView listView = new ListView();
            listView.View = View.Details;
            listView.Columns.Clear();
            listView.Items.Clear();
            listView.Columns.Add("Attribute", 150, HorizontalAlignment.Left);
            listView.Columns.Add("Value", 150, HorizontalAlignment.Left);

            foreach (string key in cmClass.Attributes.Keys)
            {
                string[] items = { $"{key}", $"{cmClass.Attributes[key]}" };
                listView.Items.Add(new ListViewItem(items));
            }

            foreach (ColumnHeader col in listView.Columns)
            {
                col.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            }

            listView.Dock = DockStyle.Fill;

            TabPage pageProperties = new TabPage("Properties");
            pageProperties.Controls.Add(listView);
            tabControlProperties.TabPages.Add(pageProperties);

            TreeView treeView = new TreeView();
            List<CMAttribute> permissibleAttributes = cmClass.PermissibleAttributes;
            for(int index = 0; index < permissibleAttributes.Count; index++)
            {
                treeView.Nodes.Add(new TreeNode($"{permissibleAttributes[index].Id}"));
            }

            treeView.Dock = DockStyle.Fill;

            TabPage pagePermissibleAttributes = new TabPage("Permissible Attributes");
            pagePermissibleAttributes.Controls.Add(treeView);
            tabControlProperties.TabPages.Add(pagePermissibleAttributes);
        }

        private void ToolStripButtonExportPermissibleGrid_Click(object sender, EventArgs e)
        {
            string newFileName = fileName;
            newFileName = newFileName.Remove(newFileName.LastIndexOf("."), newFileName.Length - newFileName.LastIndexOf("."));
            newFileName += ".xlsx";
            int result = ExcelExporter.ExportPermissibleGrid(newFileName, model);
            if (result == 0)
            {
                MessageBox.Show($"Export done");
            }
        }
    }
}
