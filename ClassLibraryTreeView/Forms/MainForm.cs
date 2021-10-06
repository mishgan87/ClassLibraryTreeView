using ClassLibraryTreeView.Classes;
using ClassLibraryTreeView.Controls;
using ClassLibraryTreeView.Forms;
using ClassLibraryTreeView.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
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
            tabControl.MouseClick += new MouseEventHandler(this.OnTabControlMouseClick);
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
        private void OnTabControlMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                for (var i = 0; i < this.tabControl.TabPages.Count; i++)
                {
                    var tabRect = this.tabControl.GetTabRect(i);
                    tabRect.Inflate(-2, -2);
                    var closeImage = Properties.Resources.close;
                    var imageRect = new Rectangle(
                        (tabRect.Right - closeImage.Width),
                        tabRect.Top + (tabRect.Height - closeImage.Height) / 2,
                        closeImage.Width,
                        closeImage.Height);
                    if (imageRect.Contains(e.Location))
                    {
                        this.tabControl.TabPages.RemoveAt(i);
                        break;
                    }
                }
            }
        }
        // private void ViewProperties(object sender, TreeNodeMouseClickEventArgs eventArgs)
        private void ViewProperties(object sender, TreeNode selectedNode)
        {
            this.btnAdd.Enabled = true;
            this.btnDelete.Enabled = true;

            if (selectedNode.Tag == null)
            {
                return;
            }

            SelectedTreeNode = selectedNode;

            PropertiesView propertiesView = new PropertiesView();
            TabPage tabPage = new TabPage(SelectedTreeNode.Text);
            tabPage.Controls.Add(propertiesView);
            tabControl.TabPages.Add(tabPage);
            tabControl.SelectedTab = tabPage;

            if (this.treeTabs.SelectedTab.Text.ToLower().Equals("attributes"))
            {
                propertiesView.ViewAttributeProperties((CMAttribute)selectedNode.Tag);
            }

            if (this.treeTabs.SelectedTab.Text.ToLower().Equals("classes"))
            {
                propertiesView.ViewClassProperties((CMClass)selectedNode.Tag);
            }

            if (this.treeTabs.SelectedTab.Text.ToLower().Equals("enumerations"))
            {
                propertiesView.ViewEnumerationProperties(selectedNode.Tag);
            }

            if (this.treeTabs.SelectedTab.Text.ToLower().Equals("taxonomies"))
            {
                propertiesView.ViewTaxonomyProperties(selectedNode.Tag);
            }

            if (this.treeTabs.SelectedTab.Text.ToLower().Equals("measures"))
            {
                propertiesView.ViewMeasureProperties(selectedNode);
            }
        }
        private async void ExportPermissibleGrid(object sender, EventArgs e)
        {
            layoutWorkplace.Panel1.Enabled = false;
            progressBar.Value = 0;
            progressBar.Visible = true;

            // Stopwatch stopWatch = new Stopwatch();
            // stopWatch.Start();
            await Task.Run(() => model.ExportPermissibleGrid());
            // stopWatch.Stop();
            // TimeSpan timeSpan = stopWatch.Elapsed;
            // string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds / 10);
            // MessageBox.Show($"Export done for {elapsedTime}");

            layoutWorkplace.Panel1.Enabled = true;
            progressBar.Visible = false;
        }

        private void BtnOpenFile_Click(object sender, EventArgs e)
        {
            if (model.OpenFile())
            {
                treeTabs.TabPages.Clear();
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
                    treeView.NodeClicked += new EventHandler<TreeNode>(this.ViewProperties);
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
            layoutWorkplace.Panel1.Enabled = false;
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
            layoutWorkplace.Panel1.Enabled = true;
            progressBar.Visible = false;
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            TabPage page = new TabPage($"Search");
            SearchPanel searchPanel = new SearchPanel(model);
            page.Controls.Add(searchPanel);
            tabControl.TabPages.Add(page);
            tabControl.SelectedTab = page;
        }

        private void TabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            var tabPage = this.tabControl.TabPages[e.Index];
            var tabRect = this.tabControl.GetTabRect(e.Index);
            tabRect.Inflate(-2, -2);

            var closeImage = Properties.Resources.close;
            e.Graphics.DrawImage(closeImage, (tabRect.Right - closeImage.Width),
                tabRect.Top + (tabRect.Height - closeImage.Height) / 2);

            TextRenderer.DrawText(e.Graphics, tabPage.Text, tabPage.Font,
                tabRect, tabPage.ForeColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
        }

        private void TreeTabs_DrawItem(object sender, DrawItemEventArgs e)
        {
            var tabPage = this.treeTabs.TabPages[e.Index];
            var tabRect = this.treeTabs.GetTabRect(e.Index);
            tabRect.Inflate(-2, -2);
            /*
            var closeImage = Properties.Resources.close;
            e.Graphics.DrawImage(closeImage, (tabRect.Right - closeImage.Width),
                tabRect.Top + (tabRect.Height - closeImage.Height) / 2);
            */
            TextRenderer.DrawText(e.Graphics, tabPage.Text, tabPage.Font,
                tabRect, tabPage.ForeColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
        }
    }
}
