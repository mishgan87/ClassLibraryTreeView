using ClassLibraryTreeView.Classes;
using ClassLibraryTreeView.Interfaces;
using System;
using System.Collections.Generic;
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
        private void ViewTaxonomyProperties(object tag)
        {
            Type type = tag.GetType();
            if (type.Name.ToLower().Equals("taxonomy"))
            {
                Taxonomy taxonomy = (Taxonomy)tag;
                info.Text = $"Taxonomy : {taxonomy.Name}";
                propertiesTabs.TabPages.Add(new TabPage("Properties"));
                propertiesTabs.TabPages[0].Controls.Add(new PropertiesListView(taxonomy));
            }

            if (type.Name.ToLower().Equals("taxonomynode"))
            {
                TaxonomyNode taxonomyNode = (TaxonomyNode)tag;
                info.Text = $"Taxonomy Node : {taxonomyNode.Name}";
                propertiesTabs.TabPages.Add(new TabPage("Properties"));
                propertiesTabs.TabPages.Add(new TabPage("Classes"));
                propertiesTabs.TabPages[0].Controls.Add(new PropertiesListView(taxonomyNode));

                if (taxonomyNode.Classes.Count == 0)
                {
                    return;
                }

                ListView listView = new ListView();
                listView.LabelEdit = true;
                listView.GridLines = true;
                listView.HeaderStyle = ColumnHeaderStyle.None;
                listView.Dock = DockStyle.Fill;
                listView.View = View.Details;
                listView.FullRowSelect = true;

                listView.Columns.Add("ID", 300, HorizontalAlignment.Left);

                foreach (string id in taxonomyNode.Classes)
                {
                    string[] items = { $"{id}" };
                    ListViewItem item = new ListViewItem(items);
                    listView.Items.Add(item);
                }

                propertiesTabs.TabPages[1].Controls.Add(listView);
            }

        }
        private void ViewMeasureProperties(object sender, TreeNodeMouseClickEventArgs e)
        {
            propertiesTabs.TabPages.Clear();
            if (e.Node.Tag != null)
            {
                if (e.Node.Parent != null)
                {
                    if (e.Node.Parent.Text.ToLower().Contains("units"))
                    {
                        MeasureUnit measureUnit = (MeasureUnit)e.Node.Tag;
                        info.Text = $"Measure Unit : {measureUnit.Name}";
                        propertiesTabs.TabPages.Add(new TabPage("Properties"));
                        propertiesTabs.TabPages[0].Controls.Add(new PropertiesListView(measureUnit));
                    }
                    if (e.Node.Parent.Text.ToLower().Contains("classes"))
                    {
                        MeasureClass measureClass = (MeasureClass)e.Node.Tag;
                        info.Text = $"Measure Class : {measureClass.Name}";
                        propertiesTabs.TabPages.Add(new TabPage("Properties"));
                        propertiesTabs.TabPages[0].Controls.Add(new PropertiesListView(measureClass));
                        ListView listViewItems = new ListView();
                        listViewItems.View = View.Details;
                        listViewItems.Columns.Clear();
                        listViewItems.Items.Clear();
                        listViewItems.Columns.Add("Id", 300, HorizontalAlignment.Left);
                        listViewItems.Columns.Add("Name", 300, HorizontalAlignment.Left);
                        listViewItems.Columns.Add("Description", 300, HorizontalAlignment.Left);

                        foreach (string unitId in measureClass.Units)
                        {
                            string[] items = { $"{model.measureUnits[unitId].Id}",
                                                $"{model.measureUnits[unitId].Name}",
                                                $"{model.measureUnits[unitId].Description}" };
                            listViewItems.Items.Add(new ListViewItem(items));
                        }

                        listViewItems.Dock = DockStyle.Fill;
                        listViewItems.Font = propertiesTabs.Font;

                        TabPage pageItems = new TabPage("Units");
                        pageItems.Controls.Add(listViewItems);
                        propertiesTabs.TabPages.Add(pageItems);
                    }
                }
            }
        }
        private void ViewEnumerationProperties(object tag)
        {
            Type type = tag.GetType();
            if (type.Name.ToLower().Equals("enumerationlistitem"))
            {
                EnumerationListItem enumerationListItem = (EnumerationListItem)tag;
                info.Text = $"Enumeration List Item : {enumerationListItem.Name}";
                propertiesTabs.TabPages.Add(new TabPage("Properties"));
                propertiesTabs.TabPages[0].Controls.Add(new PropertiesListView(enumerationListItem));
                return;
            }
            EnumerationList enumerationList = (EnumerationList)tag;
            info.Text = $"Enumeration List : {enumerationList.Name}";
            propertiesTabs.TabPages.Add(new TabPage("Properties"));
            propertiesTabs.TabPages.Add(new TabPage("Items"));
            propertiesTabs.TabPages[0].Controls.Add(new PropertiesListView(enumerationList));

            ListView listView = new ListView();
            listView.Font = propertiesTabs.Font;
            listView.Dock = DockStyle.Fill;
            listView.FullRowSelect = true;
            listView.LabelEdit = true;
            listView.GridLines = true;
            listView.View = View.Details;
            listView.Columns.Clear();
            listView.Columns.Add("Id", 300, HorizontalAlignment.Left);
            listView.Columns.Add("Name", 300, HorizontalAlignment.Left);
            listView.Columns.Add("Description", 300, HorizontalAlignment.Left);

            foreach (EnumerationListItem item in enumerationList.Items)
            {
                string[] items = { $"{item.Id}", $"{item.Name}", $"{item.Description}" };
                listView.Items.Add(new ListViewItem(items));
            }

            propertiesTabs.TabPages[1].Controls.Add(listView);
        }
        private void DisableButtons(object sender, EventArgs e)
        {
            this.btnAdd.Enabled = false;
            this.btnDelete.Enabled = false;
        }
        private void ViewProperties(object sender, TreeNodeMouseClickEventArgs eventArgs)
        {
            propertiesTabs.TabPages.Clear();
            info.Text = "";

            this.btnAdd.Enabled = true;
            this.btnDelete.Enabled = true;

            if (eventArgs.Node.Tag == null)
            {
                return;
            }

            SelectedTreeNode = eventArgs.Node;

            if (this.treeTabs.SelectedTab.Text.ToLower().Equals("attributes"))
            {
                IAttribute attribute = (IAttribute)eventArgs.Node.Tag;
                info.Text = $"Attribute : {attribute.Name}";
                propertiesTabs.TabPages.Add(new TabPage("Properties"));
                propertiesTabs.TabPages[0].Controls.Add(new PropertiesListView(attribute));
            }

            if (this.treeTabs.SelectedTab.Text.ToLower().Equals("classes"))
            {
                IClass cmClass = (IClass)eventArgs.Node.Tag;
                info.Text = $"Class : {cmClass.Name}";

                propertiesTabs.TabPages.Add(new TabPage("Properties"));
                
                
                propertiesTabs.TabPages[0].Controls.Add(new PropertiesListView(cmClass)); // Add properties
                // propertiesTabs.TabPages[1].Controls.Add(new PropertiesListView(cmClass.PermissibleAttributesMap)); // Add permissible attributes

                List<IAttribute> pattributes = model.ClassPermissibleAttributes(cmClass);


                propertiesTabs.TabPages.Add(new TabPage($"Permissible Attributes ({pattributes.Count})"));
                propertiesTabs.TabPages[1].Controls.Add(new PropertiesListView(pattributes)); // Add permissible attributes


                // propertiesTabs.TabPages[2].Controls.Add(listView); // add permissible grid
            }

            if (this.treeTabs.SelectedTab.Text.ToLower().Equals("enumerations"))
            {
                ViewEnumerationProperties(eventArgs.Node.Tag);
            }

            if (this.treeTabs.SelectedTab.Text.ToLower().Equals("taxonomies"))
            {
                ViewTaxonomyProperties(eventArgs.Node.Tag);
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
                info.Text = "";
                treeTabs.TabPages.Clear();
                propertiesTabs.TabPages.Clear();
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

        private async void BtnReport_Click(object sender, EventArgs e)
        {
            layoutMain.Panel1.Enabled = false;
            progressBar.Value = 0;
            progressBar.Visible = true;

            try
            {
                await Task.Run(() => model.ExportClassAttributes());
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            layoutMain.Panel1.Enabled = true;
            progressBar.Visible = false;
        }
    }
}
