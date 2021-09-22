using ClassLibraryTreeView.Classes;
using ClassLibraryTreeView.Interfaces;
using System;
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
            progressBar.Visible = false;
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
        private void ViewTaxonomyProperties(object sender, TreeNodeMouseClickEventArgs e)
        {
            propertiesTabs.TabPages.Clear();
            if (e.Node.Tag != null)
            {
                if (e.Node.Parent != null)
                {
                    if (e.Node.Parent.Text.ToLower().Contains("taxonomies"))
                    {
                        Taxonomy taxonomy = (Taxonomy)e.Node.Tag;
                        info.Text = $"Taxonomy : {taxonomy.Name}";
                        propertiesTabs.TabPages.Add(new TabPage("Properties"));
                        propertiesTabs.TabPages[0].Controls.Add(new PropertiesListView(taxonomy));
                        ListView listViewItems = new ListView();
                        listViewItems.View = View.Details;
                        listViewItems.Columns.Clear();
                        listViewItems.Items.Clear();
                        listViewItems.Columns.Add("Id", 300, HorizontalAlignment.Left);
                        listViewItems.Columns.Add("Name", 300, HorizontalAlignment.Left);
                        listViewItems.Columns.Add("Description", 300, HorizontalAlignment.Left);

                        foreach (TaxonomyNode node in taxonomy.Nodes)
                        {
                            string[] items = { $"{node.Id}",
                                                $"{node.Name}",
                                                $"{node.Description}" };
                            listViewItems.Items.Add(new ListViewItem(items));
                        }

                        listViewItems.Dock = DockStyle.Fill;
                        listViewItems.Font = propertiesTabs.Font;

                        TabPage pageNodes = new TabPage("Nodes");
                        pageNodes.Controls.Add(listViewItems);
                        propertiesTabs.TabPages.Add(pageNodes);
                    }
                    else
                    {
                        TaxonomyNode taxonomyNode = (TaxonomyNode)e.Node.Tag;
                        Taxonomy taxonomy = (Taxonomy)e.Node.Parent.Tag;
                        foreach (TaxonomyNode node in taxonomy.Nodes)
                        {
                            
                            if (node.Id.Equals(taxonomyNode.Id))
                            {
                                propertiesTabs.TabPages.Add(new TabPage("Properties"));
                                propertiesTabs.TabPages[0].Controls.Add(new PropertiesListView(node));
                                ListView listViewItems = new ListView();
                                listViewItems.View = View.Details;
                                listViewItems.Columns.Clear();
                                listViewItems.Items.Clear();
                                listViewItems.Columns.Add("Id", 300, HorizontalAlignment.Left);
                                listViewItems.Columns.Add("Name", 300, HorizontalAlignment.Left);
                                listViewItems.Columns.Add("Description", 300, HorizontalAlignment.Left);

                                foreach (string classId in node.Classes)
                                {
                                    string[] items = { $"{classId}",
                                                $"",
                                                $"" };
                                    listViewItems.Items.Add(new ListViewItem(items));
                                }

                                listViewItems.Dock = DockStyle.Fill;
                                listViewItems.Font = propertiesTabs.Font;

                                TabPage pageClasses = new TabPage("Classes");
                                pageClasses.Controls.Add(listViewItems);
                                propertiesTabs.TabPages.Add(pageClasses);
                                break;
                            }
                        }
                    }
                }
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
            EnumerationList enumerationList = (EnumerationList)tag;
            info.Text = $"Enumeration List : {enumerationList.Name}";
            propertiesTabs.TabPages.Add(new TabPage("Properties"));
            propertiesTabs.TabPages.Add(new TabPage("Items"));
            propertiesTabs.TabPages[0].Controls.Add(new PropertiesListView(enumerationList));

            ListView listViewItems = new ListView();
            listViewItems.View = View.Details;
            listViewItems.Columns.Clear();
            listViewItems.Items.Clear();
            listViewItems.Columns.Add("Id", 300, HorizontalAlignment.Left);
            listViewItems.Columns.Add("Name", 300, HorizontalAlignment.Left);
            listViewItems.Columns.Add("Description", 300, HorizontalAlignment.Left);

            foreach (EnumerationListItem item in enumerationList.Items)
            {
                string[] items = { $"{item.Id}", $"{item.Name}", $"{item.Description}" };
                listViewItems.Items.Add(new ListViewItem(items));
            }

            listViewItems.Dock = DockStyle.Fill;
            listViewItems.Font = propertiesTabs.Font;

            propertiesTabs.TabPages[1].Controls.Add(listViewItems);
        }
        private void ViewProperties(object sender, TreeNodeMouseClickEventArgs eventArgs)
        {
            if (eventArgs.Node.Tag == null)
            {
                return;
            }

            SelectedTreeNode = eventArgs.Node;

            propertiesTabs.TabPages.Clear();

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
                propertiesTabs.TabPages.Add(new TabPage("Permissible Attributes"));
                propertiesTabs.TabPages.Add(new TabPage("Permissible Grid"));

                propertiesTabs.TabPages[0].Controls.Add(new PropertiesListView(cmClass)); // Add properties
                propertiesTabs.TabPages[1].Controls.Add(new PropertiesListView(cmClass.PermissibleAttributesMap)); // Add permissible attributes
                // propertiesTabs.TabPages[2].Controls.Add(listView); // add permissible grid
            }

            if (this.treeTabs.SelectedTab.Text.ToLower().Equals("enumerations"))
            {
                ViewEnumerationProperties(eventArgs.Node.Tag);
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
    }
}
