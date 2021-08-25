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
            if (cmClass.HasDecendants)
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
            string id = e.Node.Tag.ToString();
            TreeNode parentNode = e.Node.Parent;
            string parentText = parentNode.Text.ToString();
            while (parentNode != null)
            {
                parentText = parentNode.Text.ToString();
                parentNode = parentNode.Parent;
            }

            if (parentText.Equals("Functionals"))
            {
                CMClass cmClass = CMClass.FindClass(id, model.functionals);
            }
        }
    }
}
