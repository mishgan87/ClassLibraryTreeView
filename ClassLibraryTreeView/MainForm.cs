using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassLibraryTreeView
{
    public partial class MainForm : Form
    {
        private ConceptualModel model;
        public MainForm()
        {
            InitializeComponent();
            model = new ConceptualModel();
        }
        private void AddChildren(TreeNode treeNodeRoot, CMClass cmClass)
        {
            if (cmClass.HasDescendants)
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
        private void ShowModelTreeView()
        {
            treeView.Nodes.Clear();
            TreeNode treeNodeFunctionals = new TreeNode();
            treeNodeFunctionals.Text = "Functionals";
            foreach (CMClass cmClass in model.functionals.Values)
            {
                TreeNode treeNode = new TreeNode();
                treeNode.Text = cmClass.Name;
                treeNode.Tag = cmClass.Id;
                AddChildren(treeNode, cmClass);
                treeNodeFunctionals.Nodes.Add(treeNode);
            }

            TreeNode treeNodePhysicals = new TreeNode();
            treeNodePhysicals.Text = "Physicals";
            foreach (CMClass cmClass in model.physicals.Values)
            {
                TreeNode treeNode = new TreeNode();
                treeNode.Text = cmClass.Name;
                treeNode.Tag = cmClass.Id;
                AddChildren(treeNode, cmClass);
                treeNodePhysicals.Nodes.Add(treeNode);
            }

            if (treeNodeFunctionals.Nodes.Count > 0)
            {
                treeView.Nodes.Add(treeNodeFunctionals);
            }

            if (treeNodePhysicals.Nodes.Count > 0)
            {
                treeView.Nodes.Add(treeNodePhysicals);
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
                    string fileName = openFileDialog.FileName;
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

            if (cmClass.HasAttributes)
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

            if (cmClass.HasPermissibleAttributes)
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
    }
}
