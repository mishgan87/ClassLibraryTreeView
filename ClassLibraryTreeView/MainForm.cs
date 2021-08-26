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
        private void AddChildren(TreeNode treeNodeRoot, CMClass cmClass, List<CMClass> cmClassList)
        {
            foreach (CMClass child in cmClassList)
            {
                if (child.ParentId == null)
                {
                    continue;
                }
                if (child.ParentId.Equals(cmClass.Id))
                {
                    TreeNode treeNode = new TreeNode();
                    treeNode.Text = child.Name;
                    treeNode.Tag = child.Id;
                    AddChildren(treeNode, child, cmClassList);
                    treeNodeRoot.Nodes.Add(treeNode);
                }
            }
        }
        private int AddClass(List<CMClass> cmClassList, string type, TreeView treeView, int beginIndex)
        {
            TreeNode treeNodeRoot = new TreeNode();
            treeNodeRoot.Text = type;
            int index = 0;
            for(index = beginIndex; index < cmClassList.Count; index++)
            {
                if (!cmClassList[index].Xtype.Equals(type))
                {
                    break;
                }
                TreeNode treeNode = new TreeNode();
                treeNode.Text = cmClassList[index].Name;
                treeNode.Tag = cmClassList[index].Id;
                AddChildren(treeNode, cmClassList[index], cmClassList);
                treeNodeRoot.Nodes.Add(treeNode);
            }
            if (treeNodeRoot.Nodes.Count > 0)
            {
                treeView.Nodes.Add(treeNodeRoot);
            }
            return index;
        }
        private void ShowModelTreeView()
        {
            treeView.Nodes.Clear();
            foreach(string type in model.classesTypes)
            {
                TreeNode treeNode = new TreeNode(type);
                treeView.Nodes.Add(treeNode);
            }
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
                ShowModelTreeView();
            }
        }

        private void TreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Tag == null)
            {
                return;
            }
            string id = e.Node.Tag.ToString();
            foreach(CMClass cmClass in model.classes)
            {
                if (cmClass.Id.Equals(id))
                {
                    ShowProperties(cmClass);
                    return;
                }
            }
        }
        private void ShowProperties(CMClass cmClass)
        {
            tabControlProperties.TabPages.Clear();

            if (cmClass.Attributes.Count > 0)
            {
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
            }

            if (cmClass.PermissibleAttributes.Count > 0)
            {
                TreeView treeView = new TreeView();

                foreach (string key in cmClass.PermissibleAttributes.Keys)
                {
                    treeView.Nodes.Add(new TreeNode($"{key}"));
                }

                treeView.Dock = DockStyle.Fill;

                TabPage pagePermissibleAttributes = new TabPage("Permissible Attributes");
                pagePermissibleAttributes.Controls.Add(treeView);
                tabControlProperties.TabPages.Add(pagePermissibleAttributes);
            }
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
