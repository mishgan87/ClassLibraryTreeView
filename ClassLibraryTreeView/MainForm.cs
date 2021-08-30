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
        public bool OpenFile()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
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
                }
            }
            return true;
        }
        private void ButtonOpenFile_Click(object sender, EventArgs e)
        {
            if (OpenFile())
            {
                tabControl.TabPages.Clear();
                
                // Add classes

                ClassLibraryTreeView classes = new ClassLibraryTreeView();
                classes.Nodes.Clear();
                classes.AddClass(model.documents, "Documents");
                classes.AddClass(model.functionals, "Functionals");
                classes.AddClass(model.physicals, "Physicals");
                classes.Dock = DockStyle.Fill;
                classes.NodeMouseDoubleClick += new TreeNodeMouseClickEventHandler(this.ViewClassProperties);
                classes.Font = tabControl.Font;

                TabPage pageClasses = new TabPage("Classes");
                pageClasses.Controls.Add(classes);
                tabControl.TabPages.Add(pageClasses);

                // Add attributes

                ClassLibraryTreeView attributes = new ClassLibraryTreeView();
                attributes.Nodes.Clear();
                attributes.AddAttributes(model.attributes);
                attributes.Dock = DockStyle.Fill;
                attributes.NodeMouseDoubleClick += new TreeNodeMouseClickEventHandler(this.ViewAttributeProperties);
                attributes.Font = tabControl.Font;

                TabPage pageAttributes = new TabPage("Attributes");
                pageAttributes.Controls.Add(attributes);
                tabControl.TabPages.Add(pageAttributes);
            }
        }
        private void ViewAttributeProperties(object sender, TreeNodeMouseClickEventArgs e)
        {
            tabControlProperties.TabPages.Clear();

            if (e.Node.Tag == null)
            {
                return;
            }

            IAttribute attribute = model.attributes[e.Node.Tag.ToString()];

            ListView listView = new ListView();
            listView.View = View.Details;
            listView.Columns.Clear();
            listView.Items.Clear();
            listView.Columns.Add("Attribute", 150, HorizontalAlignment.Left);
            listView.Columns.Add("Value", 150, HorizontalAlignment.Left);

            KeyValuePair<string, string>[] attributes = attribute.Attributes;

            foreach (KeyValuePair<string, string> pair in attributes)
            {
                string[] items = { $"{pair.Key}", $"{pair.Value}" };
                listView.Items.Add(new ListViewItem(items));
            }

            foreach (ColumnHeader col in listView.Columns)
            {
                col.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
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

            // Insert properties

            ListView listView = new ListView();
            listView.View = View.Details;
            listView.Columns.Clear();
            listView.Items.Clear();
            listView.Columns.Add("Attribute", 150, HorizontalAlignment.Left);
            listView.Columns.Add("Value", 150, HorizontalAlignment.Left);

            KeyValuePair<string, string>[] attributes = cmClass.Attributes;

            foreach (KeyValuePair<string, string> attribute in attributes)
            {
                string[] items = { $"{attribute.Key}", $"{attribute.Value}" };
                listView.Items.Add(new ListViewItem(items));
            }

            foreach (ColumnHeader col in listView.Columns)
            {
                col.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            }

            listView.Dock = DockStyle.Fill;
            listView.Font = tabControlProperties.Font;

            TabPage pageProperties = new TabPage("Properties");
            pageProperties.Controls.Add(listView);
            tabControlProperties.TabPages.Add(pageProperties);

            // Insert children

            // Insert permissible attributes

            TreeView treeView = new TreeView();
            string[] permissibleAttributes = model.PermissibleAttributes(cmClass);
            for (int index = 0; index < permissibleAttributes.Length; index++)
            {
                treeView.Nodes.Add(new TreeNode($"{permissibleAttributes[index]}"));
            }

            treeView.Dock = DockStyle.Fill;
            treeView.Font = tabControlProperties.Font;

            TabPage pagePermissibleAttributes = new TabPage("Permissible Attributes");
            pagePermissibleAttributes.Controls.Add(treeView);
            tabControlProperties.TabPages.Add(pagePermissibleAttributes);
        }
        private void ToolStripButtonExportPermissibleGrid_Click(object sender, EventArgs e)
        {
            string newFileName = fileName;
            newFileName = newFileName.Remove(newFileName.LastIndexOf("."), newFileName.Length - newFileName.LastIndexOf("."));
            newFileName += ".xlsx";
            ExcelExporter exporter = new ExcelExporter();
            // exporter.ExportDone += new EventHandler(this.GetProgress);
            exporter.GetProgress += (s, ea) =>
            {
                progressBar.Value = ea.Progress;
                progressBar.ToolTipText = ea.Progress.ToString();
            };

            int result = exporter.ExportPermissibleGrid(newFileName, model);
            if (result == 0)
            {
                progressBar.ToolTipText = $"Export done";
            }
        }
    }
}
