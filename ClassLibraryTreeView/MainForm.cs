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
        private void ViewEnumerationProperties(object sender, TreeNodeMouseClickEventArgs e)
        {
            propertiesTabs.TabPages.Clear();
            if (e.Node.Tag != null)
            {
                EnumerationList enumerationList = (EnumerationList)e.Node.Tag;
                info.Text = $"Enumeration List : {enumerationList.Name}";
                propertiesTabs.TabPages.Add(new TabPage("Properties"));
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

                TabPage pageItems = new TabPage("Items");
                pageItems.Controls.Add(listViewItems);
                propertiesTabs.TabPages.Add(pageItems);
            }
        }
        private void ViewAttributeProperties(object sender, TreeNodeMouseClickEventArgs eventArgs)
        {
            if (eventArgs.Node.Tag == null)
            {
                return;
            }

            propertiesTabs.TabPages.Clear();
            IAttribute attribute = (IAttribute)eventArgs.Node.Tag;
            info.Text = $"Attribute : {attribute.Name}";
            propertiesTabs.TabPages.Add(new TabPage("Properties"));
            propertiesTabs.TabPages[0].Controls.Add(new PropertiesListView(attribute));
        }
        private void ViewClassProperties(object sender, TreeNodeMouseClickEventArgs eventArgs)
        {
            if (eventArgs.Button == MouseButtons.Right)
            {
                return;
            }

            propertiesTabs.TabPages.Clear();

            IClass cmClass = (IClass)eventArgs.Node.Tag;

            if (cmClass != null)
            {
                info.Text = $"Class : {cmClass.Name}";

                propertiesTabs.TabPages.Add(new TabPage("Properties"));
                propertiesTabs.TabPages.Add(new TabPage("Permissible Attributes"));
                propertiesTabs.TabPages.Add(new TabPage("Permissible Grid"));

                propertiesTabs.TabPages[0].Controls.Add(new PropertiesListView(cmClass));

                // Add permissible attributes

                propertiesTabs.TabPages[1].Controls.Add(new PropertiesListView(cmClass.PermissibleAttributesMap));

                // add permissible grid

                // tabControlProperties.TabPages[1].Controls.Add(listView);

            }
        }
        private async void ExportPermissibleGrid(object sender, EventArgs e)
        {
            string newFileName = model.FullPathXml;
            newFileName = newFileName.Remove(newFileName.LastIndexOf("."), newFileName.Length - newFileName.LastIndexOf("."));
            newFileName += ".xlsx";

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            await Task.Run(() => model.ExportPermissibleGrid(newFileName));

            stopWatch.Stop();
            TimeSpan timeSpan = stopWatch.Elapsed;
            string elapsedTime = String.Format("{1:00}:{2:00}.{3:00}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds / 10);
            
            MessageBox.Show($"Export done for {elapsedTime}");
        }

        private void BtnOpenFile_Click(object sender, EventArgs e)
        {
            if (model.OpenFile())
            {
                modelName.Text = $"{model.ModelName}";

                tabControlTrees.TabPages.Clear();
                propertiesTabs.TabPages.Clear();
                info.Text = "";

                ShowClasses();
                ShowAttributes();
                ShowTaxonomies();
                ShowEnumerations();
                ShowMeasureClasses();
            }
        }

        private void BtnExportPermissibleGrid_Click(object sender, EventArgs e)
        {
            ExportPermissibleGrid(sender, e);
        }
    }
}
