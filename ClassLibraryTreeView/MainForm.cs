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

                    ClassLibraryTreeView classes = new ClassLibraryTreeView();
                    classes.Nodes.Clear();
                    classes.AddClass(model.documents, "Documents");
                    classes.AddClass(model.functionals, "Functionals");
                    classes.AddClass(model.physicals, "Physicals");
                    classes.AddList(model, "Merged");
                    classes.Dock = DockStyle.Fill;
                    classes.NodeMouseClick += new TreeNodeMouseClickEventHandler(this.ViewClassProperties);
                    classes.Font = tabControl.Font;

                    TabPage pageClasses = new TabPage("Classes");
                    pageClasses.Controls.Add(classes);
                    tabControl.TabPages.Add(pageClasses);

                    // Add attributes

                    ClassLibraryTreeView attributes = new ClassLibraryTreeView();
                    attributes.Nodes.Clear();
                    attributes.AddAttributes(model);
                    attributes.Dock = DockStyle.Fill;
                    attributes.NodeMouseClick += new TreeNodeMouseClickEventHandler(this.ViewAttributeProperties);
                    attributes.Font = tabControl.Font;

                    TabPage pageAttributes = new TabPage("Attributes");
                    pageAttributes.Controls.Add(attributes);
                    tabControl.TabPages.Add(pageAttributes);
                }
            }
        }
        private void ViewAttributeProperties(object sender, TreeNodeMouseClickEventArgs e)
        {
            tabControlProperties.TabPages.Clear();

            if (e.Node.Tag == null)
            {
                return;
            }

            string group = e.Node.Name;
            string id = e.Node.Tag.ToString();

            IAttribute attribute = model.attributes[group][id];

            ListView listView = new ListView();
            listView.View = View.Details;
            listView.Columns.Clear();
            listView.Items.Clear();
            listView.Columns.Add("Attribute", 300, HorizontalAlignment.Left);
            listView.Columns.Add("Value", 300, HorizontalAlignment.Left);

            KeyValuePair<string, string>[] attributes = attribute.Attributes;

            foreach (KeyValuePair<string, string> pair in attributes)
            {
                string[] items = { $"{pair.Key}", $"{pair.Value}" };
                listView.Items.Add(new ListViewItem(items));
            }

            listView.Dock = DockStyle.Fill;
            listView.Font = tabControlProperties.Font;

            TabPage pageProperties = new TabPage("Properties");
            pageProperties.Controls.Add(listView);
            tabControlProperties.TabPages.Add(pageProperties);
        }
        private void ViewClassProperties(object sender, TreeNodeMouseClickEventArgs e)
        {
            tabControlProperties.TabPages.Clear();

            if (e.Node.Tag == null)
            {
                return;
            }

            IClass cmClass = model.GetClass(e.Node.Tag.ToString(), e.Node.Name.ToLower());

            if (cmClass == null)
            {
                return;
            }

            // Add properties

            ListView listView = new ListView();
            listView.View = View.Details;
            listView.Columns.Clear();
            listView.Items.Clear();
            listView.Columns.Add("Attribute", 300, HorizontalAlignment.Left);
            listView.Columns.Add("Value", 300, HorizontalAlignment.Left);

            KeyValuePair<string, string>[] attributes = cmClass.Attributes;

            foreach (KeyValuePair<string, string> attribute in attributes)
            {
                string[] items = { $"{attribute.Key}", $"{attribute.Value}" };
                listView.Items.Add(new ListViewItem(items));
            }

            listView.Dock = DockStyle.Fill;
            listView.Font = tabControlProperties.Font;

            TabPage pageProperties = new TabPage("Properties");
            pageProperties.Controls.Add(listView);
            tabControlProperties.TabPages.Add(pageProperties);

            // Add children

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
        // private async void ToolStripButtonExportPermissibleGrid_Click(object sender, EventArgs e)
        private void ExportPermissibleGrid(object sender, EventArgs e)
        {
            string newFileName = fileName;
            newFileName = newFileName.Remove(newFileName.LastIndexOf("."), newFileName.Length - newFileName.LastIndexOf("."));
            newFileName += ".xlsx";
            ExcelExporter exporter = new ExcelExporter(newFileName, model);
            exporter.ExportPermissibleGrid();
            /*
            exporter.GetProgress += (s, ea) =>
            {
                progressBar.Value = ea.Progress;
                progressBar.ToolTipText = ea.Progress.ToString();
            };
            var exportGrid = Task.Factory.StartNew(async () =>
            {
                await exporter.ExportPermissibleGrid();
            });
            */
            MessageBox.Show($"Export done");
        }
    }
}
