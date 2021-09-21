using ClassLibraryTreeView;
using ClassLibraryTreeView.Classes;
using ClassLibraryTreeView.Interfaces;
using ClosedXML.Excel;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        private ConceptualModel model = new ConceptualModel();
        string conceptualModelFileName = "";
        public MainForm()
        {
            InitializeComponent();
        }
        private void ShowClasses()
        {
            ConceptualModelTreeView treeView = new ConceptualModelTreeView(model, 0);
            treeView.NodeClicked += new TreeNodeMouseClickEventHandler(this.ViewClassProperties);

            TabPage tabPage = new TabPage("Classes");
            tabPage.Controls.Add(treeView);
            tabControlTrees.TabPages.Add(tabPage);
        }
        private void ShowAttributes()
        {
            ConceptualModelTreeView treeView = new ConceptualModelTreeView(model, 1);
            treeView.NodeClicked += new TreeNodeMouseClickEventHandler(this.ViewAttributeProperties);

            TabPage tabPage = new TabPage("Attributes");
            tabPage.Controls.Add(treeView);
            tabControlTrees.TabPages.Add(tabPage);
        }
        private void ShowEnumerations()
        {
            ConceptualModelTreeView treeView = new ConceptualModelTreeView(model, 2);
            treeView.NodeClicked += new TreeNodeMouseClickEventHandler(this.ViewEnumerationProperties);

            TabPage tabPage = new TabPage("Enumerations");
            tabPage.Controls.Add(treeView);
            tabControlTrees.TabPages.Add(tabPage);
        }
        private void ShowMeasureClasses()
        {
            ConceptualModelTreeView treeView = new ConceptualModelTreeView(model, 3);
            treeView.NodeClicked += new TreeNodeMouseClickEventHandler(this.ViewMeasureProperties);

            TabPage tabPage = new TabPage("Measures");
            tabPage.Controls.Add(treeView);
            tabControlTrees.TabPages.Add(tabPage);
        }
        private void ShowTaxonomies()
        {
            ConceptualModelTreeView treeView = new ConceptualModelTreeView(model, 4);
            treeView.NodeClicked += new TreeNodeMouseClickEventHandler(this.ViewTaxonomyProperties);

            TabPage tabPage = new TabPage("Taxonomies");
            tabPage.Controls.Add(treeView);
            tabControlTrees.TabPages.Add(tabPage);
        }
        private void OpenFile()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = ".";
                openFileDialog.Filter = "XML files (*.xml)|*.xml";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filename = openFileDialog.FileName;
                    conceptualModelFileName = filename;
                    model.ImportXml(filename);
                    filename = filename.Remove(filename.LastIndexOf("."), filename.Length - filename.LastIndexOf("."));
                    filename = filename.Substring(filename.LastIndexOf("\\") + 1, filename.Length - filename.LastIndexOf("\\") - 1);
                    labelModelName.Text = $"{filename}";

                    tabControlTrees.TabPages.Clear();
                    ShowClasses();
                    ShowAttributes();
                    ShowTaxonomies();
                    ShowEnumerations();
                    ShowMeasureClasses();
                }
            }
        }
        private void AddPropertiesTab(IIdentifiable element, TabControl tabControl)
        {
            TabPage tabPage = new TabPage("Properties");
            tabPage.Controls.Add(new IIdentifiablePropertiesListView(element));
            tabControl.TabPages.Add(tabPage);
        }
        private void ViewTaxonomyProperties(object sender, TreeNodeMouseClickEventArgs e)
        {
            tabControlProperties.TabPages.Clear();
            if (e.Node.Tag != null)
            {
                if (e.Node.Parent != null)
                {
                    if (e.Node.Parent.Text.ToLower().Contains("taxonomies"))
                    {
                        Taxonomy taxonomy = (Taxonomy)e.Node.Tag;
                        labelInfo.Text = $"Taxonomy : {taxonomy.Name}";
                        AddPropertiesTab(taxonomy, tabControlProperties);
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
                        listViewItems.Font = tabControlProperties.Font;

                        TabPage pageNodes = new TabPage("Nodes");
                        pageNodes.Controls.Add(listViewItems);
                        tabControlProperties.TabPages.Add(pageNodes);
                    }
                    else
                    {
                        TaxonomyNode taxonomyNode = (TaxonomyNode)e.Node.Tag;
                        Taxonomy taxonomy = (Taxonomy)e.Node.Parent.Tag;
                        foreach (TaxonomyNode node in taxonomy.Nodes)
                        {
                            
                            if (node.Id.Equals(taxonomyNode.Id))
                            {
                                AddPropertiesTab(node, tabControlProperties);
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
                                listViewItems.Font = tabControlProperties.Font;

                                TabPage pageClasses = new TabPage("Classes");
                                pageClasses.Controls.Add(listViewItems);
                                tabControlProperties.TabPages.Add(pageClasses);
                                break;
                            }
                        }
                    }
                }
            }
        }
        private void ViewMeasureProperties(object sender, TreeNodeMouseClickEventArgs e)
        {
            tabControlProperties.TabPages.Clear();
            if (e.Node.Tag != null)
            {
                if (e.Node.Parent != null)
                {
                    if (e.Node.Parent.Text.ToLower().Contains("units"))
                    {
                        MeasureUnit measureUnit = (MeasureUnit)e.Node.Tag;
                        labelInfo.Text = $"Measure Unit : {measureUnit.Name}";
                        AddPropertiesTab(measureUnit, tabControlProperties);
                    }
                    if (e.Node.Parent.Text.ToLower().Contains("classes"))
                    {
                        MeasureClass measureClass = (MeasureClass)e.Node.Tag;
                        labelInfo.Text = $"Measure Class : {measureClass.Name}";
                        AddPropertiesTab(measureClass, tabControlProperties);
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
                        listViewItems.Font = tabControlProperties.Font;

                        TabPage pageItems = new TabPage("Units");
                        pageItems.Controls.Add(listViewItems);
                        tabControlProperties.TabPages.Add(pageItems);
                    }
                }
            }
        }
        private void ViewEnumerationProperties(object sender, TreeNodeMouseClickEventArgs e)
        {
            tabControlProperties.TabPages.Clear();
            if (e.Node.Tag != null)
            {
                EnumerationList enumerationList = (EnumerationList)e.Node.Tag;
                labelInfo.Text = $"Enumeration List : {enumerationList.Name}";
                AddPropertiesTab(enumerationList, tabControlProperties);

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
                listViewItems.Font = tabControlProperties.Font;

                TabPage pageItems = new TabPage("Items");
                pageItems.Controls.Add(listViewItems);
                tabControlProperties.TabPages.Add(pageItems);
            }
        }
        private void ViewAttributeProperties(object sender, TreeNodeMouseClickEventArgs eventArgs)
        {
            if (eventArgs.Node.Tag == null)
            {
                return;
            }

            tabControlProperties.TabPages.Clear();
            IAttribute attribute = (IAttribute)eventArgs.Node.Tag;
            labelInfo.Text = $"Attribute : {attribute.Name}";
            AddPropertiesTab(attribute, tabControlProperties);
        }
        private void ViewClassProperties(object sender, TreeNodeMouseClickEventArgs eventArgs)
        {
            if (eventArgs.Button == MouseButtons.Right)
            {
                return;
            }

            tabControlProperties.TabPages.Clear();

            IClass cmClass = (IClass)eventArgs.Node.Tag;

            if (cmClass != null)
            {
                labelInfo.Text = $"Class : {cmClass.Name}";
                AddPropertiesTab(cmClass, tabControlProperties);

                tabControlProperties.TabPages.Add(new TabPage("Permissible Attributes"));
                tabControlProperties.TabPages.Add(new TabPage("Permissible Grid"));

                // Add permissible attributes

                Dictionary<string, IAttribute> permissibleAttributes = cmClass.PermissibleAttributesMap;

                ListView listView = new ListView();
                listView.View = View.Details;
                listView.Dock = DockStyle.Fill;
                listView.LabelEdit = true;
                listView.GridLines = true;
                listView.Font = tabControlProperties.Font;
                listView.MouseDoubleClick += new MouseEventHandler(EditPermissibleAttribute);

                tabControlProperties.TabPages[1].Controls.Add(listView);

                if (permissibleAttributes.Count == 0)
                {
                    return;
                }

                KeyValuePair<string, string>[] attributes = cmClass.PermissibleAttributesMap.First().Value.Attributes();
                foreach (KeyValuePair<string, string> property in attributes)
                {
                    listView.Columns.Add($"{property.Key}", 150, HorizontalAlignment.Left);
                }
                listView.HeaderStyle = ColumnHeaderStyle.Clickable;
                listView.FullRowSelect = true;

                foreach (IAttribute attribute in permissibleAttributes.Values)
                {
                    attributes = attribute.Attributes();
                    List<string> items = new List<string>();
                    foreach (KeyValuePair<string, string> property in attributes)
                    {
                        items.Add(property.Value);
                    }

                    ListViewItem item = new ListViewItem(items.ToArray());
                    item.Tag = attribute;
                    listView.Items.Add(item);

                    if (attribute.ValidationType.ToLower().Equals("association"))
                    {
                        string[] rules = ConceptualModel.SplitValidationRules(attribute.ValidationRule);
                        string concept = rules[1];

                        for (int index = 2; index < rules.Length; index++)
                        {
                            items.Clear();
                            foreach (KeyValuePair<string, string> property in attributes)
                            {
                                string value = "";
                                if (property.Key.ToLower().Equals("validationrule"))
                                {
                                    value = rules[index];
                                }
                                items.Add(value);
                            }
                            listView.Items.Add(new ListViewItem(items.ToArray()));
                        }
                    }
                }

                // add permissible grid

                // tabControlProperties.TabPages[1].Controls.Add(listView);

            }
        }
        private void EditPermissibleAttribute(object sender, MouseEventArgs eventArgs)
        {
            // контектсное меню
            /*
            if (eventArgs.Button == MouseButtons.Right)
            {
                if (this.tabControlProperties.SelectedTab.Text.ToLower().Equals("permissible attributes"))
                {
                    ListView listView = (ListView)tabControlProperties.SelectedTab.Controls[0];
                    ListViewItem item = listView.SelectedItems[0];
                }

                System.Windows.Forms.ContextMenuStrip menu = new System.Windows.Forms.ContextMenuStrip();
                ToolStripItem itemEdit = menu.Items.Add("Edit");
                itemEdit.Image = global::ClassLibraryTreeView.Properties.Resources.edit;
                itemEdit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
                itemEdit.Click += new EventHandler(this.EditItem);

                ToolStripItem itemAdd = menu.Items.Add("Add");
                itemAdd.Image = global::ClassLibraryTreeView.Properties.Resources.add;
                itemAdd.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
                itemAdd.Click += new EventHandler(this.AddItem);

                ToolStripItem itemRemove = menu.Items.Add("Remove");
                itemRemove.Image = global::ClassLibraryTreeView.Properties.Resources.remove;
                itemRemove.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
                itemRemove.Click += new EventHandler(this.RemoveItem);

                menu.Show((System.Windows.Forms.Control)sender, new Point(eventArgs.X, eventArgs.Y));
            }
            */

            if (eventArgs.Button == MouseButtons.Left)
            {
                ListView listView = (ListView)sender;
                ListViewHitTestInfo info = listView.HitTest(eventArgs.X, eventArgs.Y);
                ListViewItem item = info.Item;
                IAttribute attribute = (IAttribute)item.Tag;
            }
        }
        private void ButtonOpenFile_Click(object sender, EventArgs e)
        {
            OpenFile();
        }
        private async void ExportPermissibleGrid(object sender, EventArgs e)
        {
            this.menuBar.Enabled = false;

            string newFileName = conceptualModelFileName;
            newFileName = newFileName.Remove(newFileName.LastIndexOf("."), newFileName.Length - newFileName.LastIndexOf("."));
            newFileName += ".xlsx";

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            await Task.Run(() => model.ExportPermissibleGrid(newFileName));

            stopWatch.Stop();
            TimeSpan timeSpan = stopWatch.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds,
                timeSpan.Milliseconds / 10);

            /*
            dataGridView.AutoGenerateColumns = false;
            dataGridView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView.DataSource = dataTable;
            */
            this.menuBar.Enabled = true;

            MessageBox.Show($"Export done for {elapsedTime}");
        }
    }
}
