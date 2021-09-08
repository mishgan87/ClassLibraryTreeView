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
                    // continue;
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

            splitContainer.Panel2.Controls.Clear();
            splitContainer.Panel2.Controls.Add(treeView);
            treeView.Font = splitContainer.Panel2.Font;

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

            splitContainer.Panel2.Controls.Clear();
            splitContainer.Panel2.Controls.Add(treeView);
            treeView.Font = splitContainer.Panel2.Font;

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

            splitContainer.Panel2.Controls.Clear();
            splitContainer.Panel2.Controls.Add(treeView);
            treeView.Font = splitContainer.Panel2.Font;

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
                    ShowClasses();
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

            if (cmClass != null)
            {
                AddPropertiesTab(cmClass, tabControlProperties);

                // Add permissible attributes

                TreeView treeView = new TreeView();
                List<IAttribute> permissibleAttributes = model.PermissibleAttributes(cmClass);

                foreach (IAttribute attribute in permissibleAttributes)
                {
                    TreeNode rootNode = new TreeNode();
                    rootNode.Text = $"{attribute.Id}";
                    rootNode.Tag = attribute;
                    treeView.Nodes.Add(rootNode);
                }

                treeView.Dock = DockStyle.Fill;
                treeView.Font = tabControlProperties.Font;
                treeView.NodeMouseClick += new TreeNodeMouseClickEventHandler(this.ShowPermissibleAttribute);

                TabPage pagePermissibleAttributes = new TabPage("Permissible Attributes");
                pagePermissibleAttributes.Controls.Add(treeView);
                tabControlProperties.TabPages.Add(pagePermissibleAttributes);

                /*
                ListView listView = new ListView();
                listView.View = View.Details;
                listView.Columns.Clear();
                listView.Columns.Add("Id", 300, HorizontalAlignment.Left);
                listView.Columns.Add("Presence", 300, HorizontalAlignment.Left);
                listView.Items.Clear();

                List<IAttribute> permissibleAttributes = model.PermissibleAttributes(cmClass);
                foreach (IAttribute attribute in permissibleAttributes)
                {
                    string[] items = { $"{attribute.Id}", $"{attribute.Presence}" };
                    listView.Items.Add(new ListViewItem(items));
                }

                listView.Dock = DockStyle.Fill;
                listView.Font = tabControlProperties.Font;

                TabPage pagePermissibleAttributes = new TabPage("Permissible Attributes");
                pagePermissibleAttributes.Controls.Add(listView);
                tabControlProperties.TabPages.Add(pagePermissibleAttributes);

                listView.MouseClick += new MouseEventHandler(ShowPermissibleAttribute);
                */
            }
        }
        private void ShowPermissibleAttribute(object sender, TreeNodeMouseClickEventArgs eventArgs)
        // private void (object sender, MouseEventArgs eventArgs)
        {
            // вариант обработчика нажатия на ListView
            // ListView listView = (ListView)sender;
            // ListViewHitTestInfo info = listView.HitTest(eventArgs.X, eventArgs.Y);
            // ListViewItem item = info.Item;
            // ---

            if (eventArgs.Node.Tag == null)
            {
                return;
            }

            IAttribute attribute = (IAttribute)eventArgs.Node.Tag;
            KeyValuePair<string, string>[] attributes = attribute.Attributes();

            Form form = new Form();
            form.Text = "Attribute Properties";
            form.Icon = this.Icon;
            
            ListView listView = new ListView();
            listView.View = View.Details;
            listView.Columns.Clear();
            listView.Items.Clear();
            listView.Columns.Add("", 300, HorizontalAlignment.Left);
            listView.Columns.Add("", 300, HorizontalAlignment.Left);
            listView.HeaderStyle = ColumnHeaderStyle.None;
            listView.FullRowSelect = true;

            foreach (KeyValuePair<string, string> pair in attributes)
            {
                string[] items = { $"{pair.Key}", $"{pair.Value}" };
                listView.Items.Add(new ListViewItem(items));
            }

            listView.Dock = DockStyle.Fill;
            listView.Font = tabControlProperties.Font;

            form.Controls.Add(listView);
            form.Width = 650;
            form.Height = listView.Items.Count * 30;
            form.MaximumSize = new Size(form.Width, form.Height);
            form.MinimumSize = new Size(form.Width, form.Height);
            form.ShowDialog();
        }
        private async void ExportPermissibleGrid()
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

        private void ButtonOpenFile_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void ToolStripButtonExportPermissibleGrid_Click(object sender, EventArgs e)
        {
            ExportPermissibleGrid();
        }
    }
}
