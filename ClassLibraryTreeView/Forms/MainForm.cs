using ClassLibraryTreeView.Classes;
using ClassLibraryTreeView.Forms;
using ClassLibraryTreeView.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ClassLibraryTreeView
{
    public partial class MainForm : Form
    {
        private ConceptualModel model = null;
        private TreeNode SelectedTreeNode = null;
        public MainForm()
        {
            InitializeComponent();
            model = new ConceptualModel();
            model.ExportProgress += new EventHandler<int>(this.SetExportProgress);

            treeTabs.LostFocus += new EventHandler(this.DisableButtons);
        }
        private void SetExportProgress(object sender, int progressValue)
        {
            if (progressBar.InvokeRequired)
            {
                Action safeWrite = delegate { SetProgress(progressValue); };
                progressBar.Invoke(safeWrite);
            }
            else
            {
                progressBar.Value = progressValue;
            }
        }
        private void SetProgress(int progress)
        {
            progressBar.Value = progress;
        }
        private void DisableButtons(object sender, EventArgs e)
        {
            this.btnAdd.Enabled = false;
            this.btnDelete.Enabled = false;
        }
        private void ViewProperties(object sender, TreeNodeMouseClickEventArgs eventArgs)
        {
            this.btnAdd.Enabled = true;
            this.btnDelete.Enabled = true;

            if (eventArgs.Node.Tag == null)
            {
                return;
            }

            SelectedTreeNode = eventArgs.Node;

            PropertiesView propertiesView = new PropertiesView();
            TabPage tabPage = new TabPage(SelectedTreeNode.Text);
            tabPage.Controls.Add(propertiesView);
            tabControl.TabPages.Add(tabPage);
            tabControl.SelectedTab = tabPage;

            if (this.treeTabs.SelectedTab.Text.ToLower().Equals("attributes"))
            {
                propertiesView.ViewAttributeProperties((IAttribute)eventArgs.Node.Tag);
            }

            if (this.treeTabs.SelectedTab.Text.ToLower().Equals("classes"))
            {
                propertiesView.ViewClassProperties((IClass)eventArgs.Node.Tag);
            }

            if (this.treeTabs.SelectedTab.Text.ToLower().Equals("enumerations"))
            {
                propertiesView.ViewEnumerationProperties(eventArgs.Node.Tag);
            }

            if (this.treeTabs.SelectedTab.Text.ToLower().Equals("taxonomies"))
            {
                propertiesView.ViewTaxonomyProperties(eventArgs.Node.Tag);
            }

            if (this.treeTabs.SelectedTab.Text.ToLower().Equals("measures"))
            {
                propertiesView.ViewMeasureProperties(sender, eventArgs);
            }
        }
        private async void ExportPermissibleGrid(object sender, EventArgs e)
        {
            layoutMain.Panel1.Enabled = false;
            progressBar.Value = 0;
            progressBar.Visible = true;

            // Stopwatch stopWatch = new Stopwatch();
            // stopWatch.Start();
            await Task.Run(() => model.ExportPermissibleGrid());
            // stopWatch.Stop();
            // TimeSpan timeSpan = stopWatch.Elapsed;
            // string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds / 10);
            // MessageBox.Show($"Export done for {elapsedTime}");

            layoutMain.Panel1.Enabled = true;
            progressBar.Visible = false;
        }

        private void BtnOpenFile_Click(object sender, EventArgs e)
        {
            if (model.OpenFile())
            {
                tabControl.TabPages.Clear();

                modelName.Text = $"{model.ModelName}";

                treeTabs.TabPages.Add(new TabPage("Classes"));
                treeTabs.TabPages.Add(new TabPage("Attributes"));
                treeTabs.TabPages.Add(new TabPage("Taxonomies"));
                treeTabs.TabPages.Add(new TabPage("Enumerations"));
                treeTabs.TabPages.Add(new TabPage("Measures"));

                for(int index = 0; index < treeTabs.TabPages.Count; index++)
                {
                    ConceptualModelTreeView treeView = new ConceptualModelTreeView(model, index);
                    treeView.NodeClicked += new TreeNodeMouseClickEventHandler(this.ViewProperties);
                    treeTabs.TabPages[index].Controls.Add(treeView);
                }

                this.btnExportPermissibleGrid.Enabled = true;

                this.btnDelete.Enabled = false;
                this.btnUndo.Enabled = false;
                this.btnAdd.Enabled = false;
                this.btnSave.Enabled = false;
                this.btnReport.Enabled = true;
                this.btnSearch.Enabled = true;
            }
        }

        private void BtnExportPermissibleGrid_Click(object sender, EventArgs e)
        {
            ExportPermissibleGrid(sender, e);
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (SelectedTreeNode == null)
            {
                return;
            }
            TreeView treeView = SelectedTreeNode.TreeView;
            treeView.SelectedNode = SelectedTreeNode;
            treeView.Nodes.Remove(treeView.SelectedNode);
            SelectedTreeNode = null;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (SelectedTreeNode == null)
            {
                return;
            }

            Type tagType = SelectedTreeNode.Tag.GetType();

            TreeNode node = new TreeNode(" ");
            node.ForeColor = SelectedTreeNode.ForeColor;

            TreeView treeView = SelectedTreeNode.TreeView;
            treeView.SelectedNode = SelectedTreeNode;

            treeView.SelectedNode.Nodes.Add(node);
            treeView.SelectedNode = node;
            
            SelectedTreeNode = node;

            if (!node.IsEditing)
            {
                node.BeginEdit();
            }
        }

        // private async void BtnReport_Click(object sender, EventArgs e)
        private void BtnReport_Click(object sender, EventArgs e)
        {
            layoutMain.Panel1.Enabled = false;
            progressBar.Value = 0;
            progressBar.Visible = true;

            // model.ExportClassAttributes();
            TabPage page = new TabPage($"Class Attributes");
            AttributesGrid classAttributesGrid = new AttributesGrid(model);
            page.Controls.Add(classAttributesGrid);
            tabControl.TabPages.Add(page);
            tabControl.SelectedTab = page;

            /*
            try
            {
                await Task.Run(() => model.ExportClassAttributes());
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            */
            layoutMain.Panel1.Enabled = true;
            progressBar.Visible = false;
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            SearchingForm searchForm = new SearchingForm(model);
            searchForm.Show();
        }
    }
}
