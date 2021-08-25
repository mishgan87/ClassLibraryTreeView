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
            if(OpenFile())
            {
                ShowModelTreeView();
            }
        }
        private void ShowModelTreeView()
        {
            treeView.Nodes.Clear();
            /*
            foreach(CMElement element in model.Elements.Values)
            {
                TreeNode treeNode = new TreeNode();
                treeNode.Text = element.Name;
                treeNode.Tag = element.Id;
                AddSubElements(element, treeNode);
                treeView.Nodes.Add(treeNode);
            }
            */
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
    }
}
