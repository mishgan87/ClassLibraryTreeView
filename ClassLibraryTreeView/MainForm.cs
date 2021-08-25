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
        private void AddChildren(TreeNode treeNodeRoot, CMClass cmClass)
        {
            if (cmClass.Descendants.Count > 0)
            {
                foreach(CMClass descendant in cmClass.Descendants.Values)
                {
                    TreeNode treeNode = new TreeNode();
                    treeNode.Text = descendant.Name;
                    treeNode.Tag = descendant.Id;
                    AddChildren(treeNode, descendant);
                    treeNodeRoot.Nodes.Add(treeNode);
                }
            }
        }
        private void AddClassMap(Dictionary<string, CMClass> map, string text, TreeView treeView)
        {
            TreeNode treeNodeRoot = new TreeNode();
            treeNodeRoot.Text = text;
            foreach (CMClass cmClass in map.Values)
            {
                TreeNode treeNode = new TreeNode();
                treeNode.Text = cmClass.Name;
                treeNode.Tag = cmClass.Id;
                AddChildren(treeNode, cmClass);
                treeNodeRoot.Nodes.Add(treeNode);
            }
            if (treeNodeRoot.Nodes.Count > 0)
            {
                treeView.Nodes.Add(treeNodeRoot);
            }
        }
        private void ShowModelTreeView()
        {
            treeView.Nodes.Clear();
            AddClassMap(model.functionals, "Functionals", treeView);
            AddClassMap(model.physicals, "Physicals", treeView);
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
            TreeNode parentNode = e.Node.Parent;
            string parentText = parentNode.Text.ToString();
            while (parentNode != null)
            {
                parentText = parentNode.Text.ToString();
                parentNode = parentNode.Parent;
            }

            CMClass cmClass = new CMClass();

            if (parentText.Equals("Functionals"))
            {
                cmClass.Clone(CMClass.FindClass(id, model.functionals));
            }

            if (parentText.Equals("Physicals"))
            {
                cmClass.Clone(CMClass.FindClass(id, model.physicals));
            }

            if (parentText.Equals("Documents"))
            {
                cmClass.Clone(CMClass.FindClass(id, model.documents));
            }

            ShowProperties(cmClass);
        }
        private void ShowProperties(CMClass cmClass)
        {
            tabControlProperties.TabPages.Clear();

            if (cmClass.Descendants.Count > 0)
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
            if (model.ExportPermissibleGrid(newFileName) == 0)
            {
                MessageBox.Show($"Export done");
            }
        }
    }
}
