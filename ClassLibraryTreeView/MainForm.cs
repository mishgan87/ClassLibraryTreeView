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
        private ConceptualModel model;
        string fileName;
        public MainForm()
        {
            InitializeComponent();
            model = new ConceptualModel();
            fileName = "";
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

                    tabControl.TabPages.Clear();

                    // Add classes

                    ClassLibraryTreeView classesTree = new ClassLibraryTreeView();
                    classesTree.Nodes.Clear();
                    classesTree.AddClass(model.documents, "Documents");
                    classesTree.AddClass(model.functionals, "Functionals");
                    classesTree.AddClass(model.physicals, "Physicals");
                    classesTree.AddList(model, "Merged");
                    classesTree.Dock = DockStyle.Fill;
                    classesTree.NodeMouseClick += new TreeNodeMouseClickEventHandler(this.ViewClassProperties);
                    classesTree.Font = tabControl.Font;

                    TabPage pageClasses = new TabPage("Classes");
                    pageClasses.Controls.Add(classesTree);
                    tabControl.TabPages.Add(pageClasses);

                    // Add attributes

                    ClassLibraryTreeView attributesTree = new ClassLibraryTreeView();
                    attributesTree.Nodes.Clear();
                    attributesTree.AddAttributes(model);
                    attributesTree.Dock = DockStyle.Fill;
                    attributesTree.NodeMouseClick += new TreeNodeMouseClickEventHandler(this.ViewAttributeProperties);
                    attributesTree.Font = tabControl.Font;

                    TabPage pageAttributes = new TabPage("Attributes");
                    pageAttributes.Controls.Add(attributesTree);
                    tabControl.TabPages.Add(pageAttributes);

                    // Add enumerations

                    ClassLibraryTreeView enumeartionsTree = new ClassLibraryTreeView();
                    enumeartionsTree.Nodes.Clear();
                    enumeartionsTree.AddEnumerations(model);
                    enumeartionsTree.Dock = DockStyle.Fill;
                    enumeartionsTree.NodeMouseClick += new TreeNodeMouseClickEventHandler(this.ViewEnumerationProperties);
                    enumeartionsTree.Font = tabControl.Font;
                    TabPage pageEnumerations = new TabPage("Enumerations");
                    pageEnumerations.Controls.Add(enumeartionsTree);
                    tabControl.TabPages.Add(pageEnumerations);

                    // Add measure units

                    ClassLibraryTreeView measureTree = new ClassLibraryTreeView();
                    measureTree.Nodes.Clear();
                    measureTree.AddMeasure(model);
                    measureTree.Dock = DockStyle.Fill;
                    measureTree.NodeMouseClick += new TreeNodeMouseClickEventHandler(this.ViewMeasureProperties);
                    measureTree.Font = tabControl.Font;

                    TabPage pageMeasure = new TabPage("Measure");
                    pageMeasure.Controls.Add(measureTree);
                    tabControl.TabPages.Add(pageMeasure);

                    // Add measure classes

                    TabColors[pageClasses] = System.Drawing.Color.Magenta;
                    TabColors[pageAttributes] = System.Drawing.Color.Yellow;
                    TabColors[pageEnumerations] = System.Drawing.Color.Gray;
                    TabColors[pageMeasure] = System.Drawing.Color.Orange;
                    tabControl.Invalidate();
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
        private void ViewMeasureProperties(object sender, TreeNodeMouseClickEventArgs e)
        {
            tabControlProperties.TabPages.Clear();
            if (e.Node.Tag != null)
            {
                string id = e.Node.Tag.ToString();
                if (e.Node.Parent != null)
                {
                    if (e.Node.Parent.Text.ToLower().Contains("units"))
                    {
                        AddPropertiesTab(model.measureUnits[id], tabControlProperties);
                    }
                    if (e.Node.Parent.Text.ToLower().Contains("classes"))
                    {
                        AddPropertiesTab(model.measureClasses[id], tabControlProperties);
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
        private void TabControl_DrawItem(object sender, DrawItemEventArgs eventArgs)
        {
            using (Brush brush = new SolidBrush(TabColors[tabControl.TabPages[eventArgs.Index]]))
            {
                eventArgs.Graphics.FillRectangle(brush, eventArgs.Bounds);
                SizeF sz = eventArgs.Graphics.MeasureString(tabControl.TabPages[eventArgs.Index].Text, eventArgs.Font);
                eventArgs.Graphics.DrawString(tabControl.TabPages[eventArgs.Index].Text, eventArgs.Font, Brushes.Black,
                                                eventArgs.Bounds.Left + (eventArgs.Bounds.Width - sz.Width) / 2,
                                                eventArgs.Bounds.Top + (eventArgs.Bounds.Height - sz.Height) / 2 + 1);

                Rectangle rect = eventArgs.Bounds;
                rect.Offset(0, 1);
                rect.Inflate(0, -1);
                eventArgs.Graphics.DrawRectangle(Pens.DarkGray, rect);
                eventArgs.DrawFocusRectangle();
            }
        }
    }
}
