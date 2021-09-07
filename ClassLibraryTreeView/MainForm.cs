using ClassLibraryTreeView;
using ClassLibraryTreeView.Classes;
using ClassLibraryTreeView.Interfaces;
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
        private Dictionary<TabPage, System.Drawing.Color> TabColors = new Dictionary<TabPage, System.Drawing.Color>();
        private ConceptualModel model = new ConceptualModel();
        string fileName;
        public MainForm()
        {
            InitializeComponent();
            model = new ConceptualModel();
            fileName = "";
        }
        private TreeNode NewNode(IClass cmClass)
        {
            TreeNode treeNode = new TreeNode();
            treeNode.Text = cmClass.Name;
            treeNode.Name = $"{cmClass.Xtype}";
            treeNode.Tag = $"{cmClass.Id}";
            if (cmClass.Xtype.ToLower().Equals("functionals"))
            {
                treeNode.ForeColor = System.Drawing.Color.Green;
            }
            if (cmClass.Xtype.ToLower().Equals("physicals"))
            {
                treeNode.ForeColor = System.Drawing.Color.DarkBlue;
            }
            return treeNode;
        }
        private void AddChildren(IClass cmClass, TreeNode treeNode)
        {
            if (cmClass.Children.Count > 0)
            {
                foreach (IClass child in cmClass.Children)
                {
                    TreeNode childNode = NewNode(child);
                    AddChildren(child, childNode);
                    treeNode.Nodes.Add(childNode);
                }
            }
        }
        public void AddClass(Dictionary<string, IClass> map, string xtype, TreeView treeView)
        {
            if (map.Count > 0)
            {
                TreeNode rootNode = new TreeNode(xtype);
                foreach (IClass cmClass in map.Values)
                {
                    if (cmClass.Extends.Equals(""))
                    {
                        TreeNode newNode = NewNode(cmClass);
                        AddChildren(cmClass, newNode);
                        rootNode.Nodes.Add(newNode);
                    }
                }
                treeView.Nodes.Add(rootNode);
            }
        }
        public void AddList(ConceptualModel model, string text, TreeView treeView)
        {
            TreeNode rootNode = new TreeNode(text);
            foreach (IClass cmClass in model.merged)
            {
                if (cmClass.Extends.Equals(""))
                {
                    TreeNode newNode = NewNode(cmClass);
                    AddChildren(cmClass, newNode);
                    rootNode.Nodes.Add(newNode);
                }
            }
            treeView.Nodes.Add(rootNode);
        }
        private void ShowClasses()
        {
            TreeView treeView = new TreeView();
            treeView.Dock = DockStyle.Fill;
            treeView.Nodes.Clear();

            splitContainer.Panel2.Controls.Clear();
            splitContainer.Panel2.Controls.Add(treeView);
            treeView.Font = splitContainer.Panel2.Font;

            AddClass(model.documents, "Documents", treeView);
            AddClass(model.functionals, "Functionals", treeView);
            AddClass(model.physicals, "Physicals", treeView);
            AddList(model, "Merged", treeView);

            treeView.NodeMouseClick += new TreeNodeMouseClickEventHandler(this.ViewClassProperties);
        }
        private void ShowAttributes()
        {
            TreeView treeView = new TreeView();
            treeView.Dock = DockStyle.Fill;
            treeView.Nodes.Clear();

            splitContainer.Panel2.Controls.Clear();
            splitContainer.Panel2.Controls.Add(treeView);
            treeView.Font = splitContainer.Panel2.Font;

            Dictionary<string, Dictionary<string, IAttribute>> attributes = model.attributes;
            foreach (string group in attributes.Keys)
            {
                TreeNode groupNode = new TreeNode(group);
                foreach (IAttribute attribute in attributes[group].Values)
                {
                    TreeNode treeNode = new TreeNode();
                    treeNode.Name = attribute.Group;
                    treeNode.Text = attribute.Name;
                    treeNode.Tag = attribute.Id;
                    groupNode.Nodes.Add(treeNode);
                }
                treeView.Nodes.Add(groupNode);
            }

            treeView.NodeMouseClick += new TreeNodeMouseClickEventHandler(this.ViewAttributeProperties);
        }
        private void ShowEnumerations()
        {
            TreeView treeView = new TreeView();
            treeView.Dock = DockStyle.Fill;
            treeView.Nodes.Clear();

            splitContainer.Panel2.Controls.Clear();
            splitContainer.Panel2.Controls.Add(treeView);
            treeView.Font = splitContainer.Panel2.Font;

            TreeNode rootNode = new TreeNode($"Enumerations");
            Dictionary<string, EnumerationList> map = model.enumerations;
            foreach (string key in map.Keys)
            {
                TreeNode treeNode = new TreeNode(key);
                treeNode.Name = map[key].Description;
                treeNode.Text = map[key].Name;
                treeNode.Tag = map[key].Id;
                rootNode.Nodes.Add(treeNode);
            }
            treeView.Nodes.Add(rootNode);

            treeView.NodeMouseClick += new TreeNodeMouseClickEventHandler(this.ViewEnumerationProperties);
        }
        private void ShowMeasureClasses()
        {
            TreeView treeView = new TreeView();
            treeView.Dock = DockStyle.Fill;
            treeView.Nodes.Clear();

            splitContainer.Panel2.Controls.Clear();
            splitContainer.Panel2.Controls.Add(treeView);
            treeView.Font = splitContainer.Panel2.Font;

            TreeNode rootUnitsNode = new TreeNode($"Measure Units");
            Dictionary<string, MeasureUnit> map = model.measureUnits;
            foreach (string key in map.Keys)
            {
                TreeNode treeNode = new TreeNode(key);
                treeNode.Name = map[key].Description;
                treeNode.Text = map[key].Name;
                treeNode.Tag = map[key].Id;
                rootUnitsNode.Nodes.Add(treeNode);
            }
            treeView.Nodes.Add(rootUnitsNode);

            TreeNode rootClassesNode = new TreeNode($"Measure Classes");
            Dictionary<string, MeasureClass> mapClasses = model.measureClasses;
            foreach (string key in mapClasses.Keys)
            {
                TreeNode treeNode = new TreeNode(key);
                treeNode.Name = mapClasses[key].Description;
                treeNode.Text = mapClasses[key].Name;
                treeNode.Tag = mapClasses[key].Id;
                rootClassesNode.Nodes.Add(treeNode);
            }
            treeView.Nodes.Add(rootClassesNode);

            treeView.NodeMouseClick += new TreeNodeMouseClickEventHandler(this.ViewMeasureProperties);
        }
        private void ShowTaxonomies()
        {
            TreeView treeView = new TreeView();
            treeView.Dock = DockStyle.Fill;
            treeView.Nodes.Clear();

            splitContainer.Panel2.Controls.Clear();
            splitContainer.Panel2.Controls.Add(treeView);
            treeView.Font = splitContainer.Panel2.Font;

            TreeNode rootNode = new TreeNode($"Taxonomies");
            Dictionary<string, Taxonomy> map = model.taxonomies;
            foreach (string key in map.Keys)
            {
                TreeNode treeNode = new TreeNode(key);
                treeNode.Name = map[key].Description;
                treeNode.Text = map[key].Name;
                treeNode.Tag = map[key].Id;
                if (map[key].Nodes.Count > 0)
                {
                    foreach (TaxonomyNode node in map[key].Nodes)
                    {
                        TreeNode treeSubNode = new TreeNode();
                        treeSubNode.Name = node.Description;
                        treeSubNode.Text = node.Name;
                        treeSubNode.Tag = node.Id;
                        treeNode.Nodes.Add(treeSubNode);
                    }
                }
                rootNode.Nodes.Add(treeNode);
            }
            treeView.Nodes.Add(rootNode);

            treeView.NodeMouseClick += new TreeNodeMouseClickEventHandler(this.ViewTaxonomyProperties);
        }
        private void OpenFile(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = ".";
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
                    ShowClasses();
                }
            }
        }
        private void AddPropertiesTab(IIdentifiable element, TabControl tabControl)
        {
            KeyValuePair<string, string>[] attributes = element.Attributes();

            ListView listView = new ListView();
            listView.View = View.Details;
            listView.Columns.Clear();
            listView.Items.Clear();
            listView.Columns.Add("Property", 300, HorizontalAlignment.Left);
            listView.Columns.Add("Value", 300, HorizontalAlignment.Left);
            listView.HeaderStyle = ColumnHeaderStyle.Clickable;
            listView.FullRowSelect = true;

            foreach (KeyValuePair<string, string> pair in attributes)
            {
                string[] items = { $"{pair.Key}", $"{pair.Value}" };
                listView.Items.Add(new ListViewItem(items));
            }

            listView.Dock = DockStyle.Fill;
            listView.Font = tabControlProperties.Font;

            TabPage tabPage = new TabPage("Properties");
            tabPage.Controls.Add(listView);
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
                        string id = e.Node.Tag.ToString();
                        AddPropertiesTab(model.taxonomies[id], tabControlProperties);
                        ListView listViewItems = new ListView();
                        listViewItems.View = View.Details;
                        listViewItems.Columns.Clear();
                        listViewItems.Items.Clear();
                        listViewItems.Columns.Add("Id", 300, HorizontalAlignment.Left);
                        listViewItems.Columns.Add("Name", 300, HorizontalAlignment.Left);
                        listViewItems.Columns.Add("Description", 300, HorizontalAlignment.Left);

                        foreach (TaxonomyNode node in model.taxonomies[id].Nodes)
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
                        string id = e.Node.Parent.Tag.ToString();
                        foreach (TaxonomyNode node in model.taxonomies[id].Nodes)
                        {
                            if (node.Id.Equals(e.Node.Tag.ToString()))
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
                    string id = e.Node.Tag.ToString();
                    if (e.Node.Parent.Text.ToLower().Contains("units"))
                    {
                        AddPropertiesTab(model.measureUnits[id], tabControlProperties);
                    }
                    if (e.Node.Parent.Text.ToLower().Contains("classes"))
                    {
                        AddPropertiesTab(model.measureClasses[id], tabControlProperties);
                        ListView listViewItems = new ListView();
                        listViewItems.View = View.Details;
                        listViewItems.Columns.Clear();
                        listViewItems.Items.Clear();
                        listViewItems.Columns.Add("Id", 300, HorizontalAlignment.Left);
                        listViewItems.Columns.Add("Name", 300, HorizontalAlignment.Left);
                        listViewItems.Columns.Add("Description", 300, HorizontalAlignment.Left);

                        foreach (string unitId in model.measureClasses[id].Units)
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
                string id = e.Node.Tag.ToString();

                AddPropertiesTab(model.enumerations[id], tabControlProperties);

                ListView listViewItems = new ListView();
                listViewItems.View = View.Details;
                listViewItems.Columns.Clear();
                listViewItems.Items.Clear();
                listViewItems.Columns.Add("Id", 300, HorizontalAlignment.Left);
                listViewItems.Columns.Add("Name", 300, HorizontalAlignment.Left);
                listViewItems.Columns.Add("Description", 300, HorizontalAlignment.Left);

                foreach (EnumerationListItem item in model.enumerations[id].Items)
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
            tabControlProperties.TabPages.Clear();
            if (eventArgs.Node.Tag != null)
            {
                string group = eventArgs.Node.Name;
                string id = eventArgs.Node.Tag.ToString();

                AddPropertiesTab(model.attributes[group][id], tabControlProperties);
            }
        }
        private void ViewClassProperties(object sender, TreeNodeMouseClickEventArgs eventArgs)
        {
            tabControlProperties.TabPages.Clear();
            if (eventArgs.Node.Tag == null)
            {
                return;
            }

            string xtype = eventArgs.Node.Name.ToLower();
            string id = eventArgs.Node.Tag.ToString();
            IClass cmClass = model.GetClass(id, xtype);

            if (cmClass != null)
            {
                AddPropertiesTab(cmClass, tabControlProperties);

                // Add permissible attributes

                ListView listViewAttributes = new ListView();
                listViewAttributes.View = View.Details;
                listViewAttributes.Columns.Clear();
                listViewAttributes.Items.Clear();
                listViewAttributes.Columns.Add("Id", 300, HorizontalAlignment.Left);
                listViewAttributes.Columns.Add("Name", 300, HorizontalAlignment.Left);
                listViewAttributes.Columns.Add("Presence", 300, HorizontalAlignment.Left);

                List<IAttribute> permissibleAttributes = model.PermissibleAttributes(cmClass);

                foreach (IAttribute attribute in permissibleAttributes)
                {
                    string[] items = { $"{attribute.Id}", $"{attribute.Name}", $"{attribute.Presence}" };
                    listViewAttributes.Items.Add(new ListViewItem(items));
                }

                listViewAttributes.Dock = DockStyle.Fill;
                listViewAttributes.Font = tabControlProperties.Font;

                TabPage pagePermissibleAttributes = new TabPage("Permissible Attributes");
                pagePermissibleAttributes.Controls.Add(listViewAttributes);
                tabControlProperties.TabPages.Add(pagePermissibleAttributes);
            }
        }
        private async void ExportPermissibleGrid(object sender, EventArgs e)
        {
            string newFileName = fileName;
            newFileName = newFileName.Remove(newFileName.LastIndexOf("."), newFileName.Length - newFileName.LastIndexOf("."));
            newFileName += ".xlsx";
            ExcelExporter exporter = new ExcelExporter(newFileName, model);

            this.buttonOpenFile.Enabled = false;
            this.toolStripButtonExportPermissibleGrid.Enabled = false;

            await Task.Run(() => exporter.ExportPermissibleGrid());

            this.buttonOpenFile.Enabled = true;
            this.toolStripButtonExportPermissibleGrid.Enabled = true;

            MessageBox.Show($"Export done");
        }
        private void ButtonClasses_Click(object sender, EventArgs e)
        {
            ShowClasses();
        }

        private void ButtonAttributes_Click(object sender, EventArgs e)
        {
            ShowAttributes();
        }

        private void ButtonEnumerations_Click(object sender, EventArgs e)
        {
            ShowEnumerations();
        }

        private void ButtonMeasure_Click(object sender, EventArgs e)
        {
            ShowMeasureClasses();
        }

        private void ButtonTaxonomies_Click(object sender, EventArgs e)
        {
            ShowTaxonomies();
        }
    }
}
