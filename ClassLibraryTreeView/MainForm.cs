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
        private Dictionary<TabPage, System.Drawing.Color> TabColors = new Dictionary<TabPage, System.Drawing.Color>();
        private ConceptualModel model = new ConceptualModel();
        private IIdentifiable CurrentItem { get; set; }
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
            treeNode.Tag = cmClass;
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
                if (cmClass == null)
                {
                    continue;
                }

                if (!cmClass.Extends.Equals(""))
                {
                    continue;
                }

                TreeNode newNode = NewNode(cmClass);
                AddChildren(cmClass, newNode);
                rootNode.Nodes.Add(newNode);
            }
            treeView.Nodes.Add(rootNode);
        }
        private void ShowClasses()
        {
            TreeView treeView = new TreeView();
            treeView.Dock = DockStyle.Fill;
            treeView.Nodes.Clear();
            treeView.Font = tabControlTrees.Font;

            TabPage tabPage = new TabPage("Classes");
            tabPage.Controls.Add(treeView);
            tabControlTrees.TabPages.Add(tabPage);

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
            treeView.Font = tabControlTrees.Font;

            TabPage tabPage = new TabPage("Attributes");
            tabPage.Controls.Add(treeView);
            tabControlTrees.TabPages.Add(tabPage);

            Dictionary<string, Dictionary<string, IAttribute>> attributes = model.attributes;
            foreach (string group in attributes.Keys)
            {
                TreeNode groupNode = new TreeNode(group);
                foreach (IAttribute attribute in attributes[group].Values)
                {
                    TreeNode treeNode = new TreeNode();
                    treeNode.Text = attribute.Name;
                    treeNode.Tag = attribute;
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
            treeView.Font = tabControlTrees.Font;

            TabPage tabPage = new TabPage("Enumerations");
            tabPage.Controls.Add(treeView);
            tabControlTrees.TabPages.Add(tabPage);

            TreeNode rootNode = new TreeNode($"Enumerations");
            Dictionary<string, EnumerationList> map = model.enumerations;
            foreach (string key in map.Keys)
            {
                TreeNode treeNode = new TreeNode(key);
                treeNode.Text = map[key].Name;
                treeNode.Tag = map[key];
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
            treeView.Font = tabControlTrees.Font;

            TabPage tabPage = new TabPage("Measures");
            tabPage.Controls.Add(treeView);
            tabControlTrees.TabPages.Add(tabPage);

            TreeNode rootUnitsNode = new TreeNode($"Measure Units");
            Dictionary<string, MeasureUnit> map = model.measureUnits;
            foreach (string key in map.Keys)
            {
                TreeNode treeNode = new TreeNode(key);
                treeNode.Text = map[key].Name;
                treeNode.Tag = map[key];
                rootUnitsNode.Nodes.Add(treeNode);
            }
            treeView.Nodes.Add(rootUnitsNode);

            TreeNode rootClassesNode = new TreeNode($"Measure Classes");
            Dictionary<string, MeasureClass> mapClasses = model.measureClasses;
            foreach (string key in mapClasses.Keys)
            {
                TreeNode treeNode = new TreeNode(key);
                treeNode.Text = mapClasses[key].Name;
                treeNode.Tag = mapClasses[key];
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
            treeView.Font = tabControlTrees.Font;

            TabPage tabPage = new TabPage("Taxonomies");
            tabPage.Controls.Add(treeView);
            tabControlTrees.TabPages.Add(tabPage);

            TreeNode rootNode = new TreeNode($"Taxonomies");
            Dictionary<string, Taxonomy> map = model.taxonomies;
            foreach (string key in map.Keys)
            {
                TreeNode treeNode = new TreeNode(key);
                treeNode.Text = map[key].Name;
                treeNode.Tag = map[key];
                if (map[key].Nodes.Count > 0)
                {
                    foreach (TaxonomyNode node in map[key].Nodes)
                    {
                        TreeNode treeSubNode = new TreeNode();
                        treeSubNode.Text = node.Name;
                        treeSubNode.Tag = node;
                        treeNode.Nodes.Add(treeSubNode);
                    }
                }
                rootNode.Nodes.Add(treeNode);
            }
            treeView.Nodes.Add(rootNode);

            treeView.NodeMouseClick += new TreeNodeMouseClickEventHandler(this.ViewTaxonomyProperties);
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
                    fileName = openFileDialog.FileName;
                    model.ImportXml(openFileDialog.FileName);
                    string text = fileName;
                    text = text.Remove(text.LastIndexOf("."), text.Length - text.LastIndexOf("."));
                    text = text.Substring(text.LastIndexOf("\\") + 1, text.Length - text.LastIndexOf("\\") - 1);
                    labelModelName.Text = $"{text}";

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
            if (element == null)
            {
                return;
            }

            KeyValuePair<string, string>[] attributes = element.Attributes();

            ListView listView = new ListView();
            listView.View = View.Details;
            listView.Columns.Clear();
            listView.Items.Clear();
            listView.Columns.Add("Property", 300, HorizontalAlignment.Left);
            listView.Columns.Add("Value", 300, HorizontalAlignment.Left);
            listView.HeaderStyle = ColumnHeaderStyle.None;
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
                        Taxonomy taxonomy = (Taxonomy)e.Node.Tag;

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
                        AddPropertiesTab(measureUnit, tabControlProperties);
                    }
                    if (e.Node.Parent.Text.ToLower().Contains("classes"))
                    {
                        MeasureClass measureClass = (MeasureClass)e.Node.Tag;
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
            tabControlProperties.TabPages.Clear();
            IAttribute attribute = (IAttribute)eventArgs.Node.Tag;
            AddPropertiesTab(attribute, tabControlProperties);
        }
        private void ViewClassProperties(object sender, TreeNodeMouseClickEventArgs eventArgs)
        {
            tabControlProperties.TabPages.Clear();

            IClass cmClass = (IClass)eventArgs.Node.Tag;

            CurrentItem = cmClass;

            if (cmClass != null)
            {
                AddPropertiesTab(cmClass, tabControlProperties);

                // Add permissible attributes

                List<IAttribute> permissibleAttributes = model.PermissibleAttributes(cmClass);

                ListView listView = new ListView();
                listView.View = View.Details;
                listView.Columns.Clear();
                listView.Items.Clear();

                listView.Dock = DockStyle.Fill;
                listView.Font = tabControlProperties.Font;
                listView.MouseDoubleClick += new MouseEventHandler(EditPermissibleAttribute);

                TabPage pagePermissibleAttributes = new TabPage("Permissible Attributes");
                pagePermissibleAttributes.Controls.Add(listView);
                tabControlProperties.TabPages.Add(pagePermissibleAttributes);

                if (permissibleAttributes.Count == 0)
                {
                    return;
                }

                KeyValuePair<string, string>[] attributes = permissibleAttributes.First().Attributes();
                foreach (KeyValuePair<string, string> property in attributes)
                {
                    listView.Columns.Add($"{property.Key}", 150, HorizontalAlignment.Left);
                }
                listView.HeaderStyle = ColumnHeaderStyle.Clickable;
                listView.FullRowSelect = true;

                foreach (IAttribute attribute in permissibleAttributes)
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
            }
        }
        private void EditItem(object sender, EventArgs e)
        {

        }
        private void AddItem(object sender, EventArgs e)
        {

        }
        private void RemoveItem(object sender, EventArgs e)
        {

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
        private void ExportPermissibleGrid(object sender, EventArgs e)
        {
            this.menuBar.Enabled = false;

            string newFileName = fileName;
            newFileName = newFileName.Remove(newFileName.LastIndexOf("."), newFileName.Length - newFileName.LastIndexOf("."));
            newFileName += ".xlsx";

            ExcelExporter exporter = new ExcelExporter();

            // exporter.ExportPermissibleGrid(newFileName, model);

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            model.GetPermissibleGrid(newFileName);

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
