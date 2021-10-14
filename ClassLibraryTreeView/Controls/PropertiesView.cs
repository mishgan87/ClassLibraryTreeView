using ClassLibraryTreeView.Classes;
using ClassLibraryTreeView.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassLibraryTreeView.Forms
{
    class PropertiesView : SplitContainer
    {
        Label label = new Label();
        TabControl tabControl = new TabControl();
        public PropertiesView()
        {
            Init();
        }
        public PropertiesView(object cmObject)
        {
            Init();

            IConceptualModelObject obj = (IConceptualModelObject)cmObject;
            KeyValuePair<string, string>[] properties = obj.Properties();
            var arraysProperties = obj.ArraysProperties();

            label.Text = $"{cmObject.GetType().Name}";
            tabControl.TabPages.Clear();
            if (properties.Length > 0)
            {
                TabPage tabPage = new TabPage("Properties");
                tabPage.Controls.Add(new PropertiesListView(properties));
                tabControl.TabPages.Add(tabPage);
            }

        }
        public PropertiesView(ConceptualModelAttribute attribute)
        {
            Init();
            ViewAttributeProperties(attribute);
        }
        public PropertiesView(ConceptualModelClass cmClass)
        {
            Init();
            ViewClassProperties(cmClass);
        }
        private void Init()
        {
            this.Dock = DockStyle.Fill;
            label.Dock = DockStyle.Fill;
            tabControl.Dock = DockStyle.Fill;

            label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.SplitterDistance = 50;
            this.Panel1.Controls.Add(label);
            this.Panel2.Controls.Add(tabControl);

            this.IsSplitterFixed = true;
            this.FixedPanel = FixedPanel.Panel1;
            this.Orientation = Orientation.Horizontal;
        }
        public void ViewAttributeProperties(ConceptualModelAttribute attribute)
        {
            label.Text = $"Attribute : {attribute.Name}";
            tabControl.TabPages.Clear();
            tabControl.TabPages.Add(new TabPage("Properties"));
            tabControl.TabPages[0].Controls.Add(new PropertiesListView(attribute));

            if (attribute.ApplicableClasses == null)
            {
                tabControl.TabPages.Add(new TabPage($"Applicable classes (0)"));
            }
            else
            {
                tabControl.TabPages.Add(new TabPage($"Applicable classes ({attribute.ApplicableClasses.Values.Count})"));
            }
            tabControl.TabPages[1].Controls.Add(new PropertiesListView(attribute.ApplicableClasses));
        }
        public void ViewClassProperties(ConceptualModelClass cmClass)
        {
            label.Text = $"Class : {cmClass.Name}";
            tabControl.TabPages.Clear();
            // Add properties

            tabControl.TabPages.Add(new TabPage("Properties"));
            tabControl.TabPages[0].Controls.Add(new PropertiesListView(cmClass));

            // Add permissible attributes

            Dictionary<string, ConceptualModelAttribute> permissibleAttributes = cmClass.PermissibleAttributes;
            tabControl.TabPages.Add(new TabPage($"Permissible Attributes ({permissibleAttributes.Values.Count})"));
            tabControl.TabPages[1].Controls.Add(new PropertiesListView(permissibleAttributes));

            // tabControl.TabPages[2].Controls.Add(listView); // add permissible grid
        }
        public void ViewEnumerationProperties(object tag)
        {
            Type type = tag.GetType();
            if (tag is ConceptualModelEnumerationItem)
            {
                ConceptualModelEnumerationItem enumerationListItem = (ConceptualModelEnumerationItem)tag;
                label.Text = $"Enumeration List Item : {enumerationListItem.Name}";
                tabControl.TabPages.Add(new TabPage("Properties"));
                tabControl.TabPages[0].Controls.Add(new PropertiesListView(enumerationListItem));
                return;
            }
            if (!(tag is ConceptualModelEnumeration))
            {
                return;
            }
            ConceptualModelEnumeration enumerationList = (ConceptualModelEnumeration)tag;
            label.Text = $"Enumeration List : {enumerationList.Name}";
            tabControl.TabPages.Add(new TabPage("Properties"));
            tabControl.TabPages.Add(new TabPage($"Items ({enumerationList.Items.Count})"));
            tabControl.TabPages[0].Controls.Add(new PropertiesListView(enumerationList));

            ListView listView = new ListView();
            listView.Font = tabControl.Font;
            listView.Dock = DockStyle.Fill;
            listView.FullRowSelect = true;
            listView.LabelEdit = true;
            listView.GridLines = true;
            listView.View = View.Details;
            listView.Columns.Clear();
            listView.Columns.Add("Id", 300, HorizontalAlignment.Left);
            listView.Columns.Add("Name", 300, HorizontalAlignment.Left);
            listView.Columns.Add("Description", 300, HorizontalAlignment.Left);

            foreach (ConceptualModelEnumerationItem item in enumerationList.Items)
            {
                string[] items = { $"{item.Id}", $"{item.Name}", $"{item.Description}" };
                listView.Items.Add(new ListViewItem(items));
            }

            tabControl.TabPages[1].Controls.Add(listView);
        }
        public void ViewConceptualModelTaxonomyProperties(object tag)
        {
            if (tag is ConceptualModelTaxonomy)
            {
                ConceptualModelTaxonomy taxonomy = (ConceptualModelTaxonomy)tag;
                label.Text = $"ConceptualModelTaxonomy : {taxonomy.Name}";
                tabControl.TabPages.Add(new TabPage("Properties"));
                tabControl.TabPages[0].Controls.Add(new PropertiesListView(taxonomy));
            }

            if (tag is ConceptualModelTaxonomyNode)
            {
                ConceptualModelTaxonomyNode taxonomyNode = (ConceptualModelTaxonomyNode)tag;
                label.Text = $"ConceptualModelTaxonomy Node : {taxonomyNode.Name}";
                tabControl.TabPages.Add(new TabPage("Properties"));
                tabControl.TabPages.Add(new TabPage("Classes"));
                tabControl.TabPages[0].Controls.Add(new PropertiesListView(taxonomyNode));

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

                tabControl.TabPages[1].Controls.Add(listView);
            }

        }
        public void ViewMeasureProperties(TreeNode selectedNode)
        {
            tabControl.TabPages.Clear();
            if (selectedNode.Tag != null)
            {
                if (selectedNode.Parent != null)
                {
                    if (selectedNode.Parent.Text.ToLower().Contains("units"))
                    {
                        ConceptualModelMeasureUnit measureUnit = (ConceptualModelMeasureUnit)selectedNode.Tag;
                        label.Text = $"Measure Unit : {measureUnit.Name}";
                        tabControl.TabPages.Add(new TabPage("Properties"));
                        tabControl.TabPages[0].Controls.Add(new PropertiesListView(measureUnit));
                    }
                    if (selectedNode.Parent.Text.ToLower().Contains("classes"))
                    {
                        ConceptualModelMeasureClass measureClass = (ConceptualModelMeasureClass)selectedNode.Tag;
                        label.Text = $"Measure Class : {measureClass.Name}";
                        tabControl.TabPages.Add(new TabPage("Properties"));
                        tabControl.TabPages[0].Controls.Add(new PropertiesListView(measureClass));
                        ListView listViewItems = new ListView();
                        listViewItems.View = View.Details;
                        listViewItems.Columns.Clear();
                        listViewItems.Items.Clear();
                        listViewItems.GridLines = true;
                        listViewItems.Columns.Add("Id", 300, HorizontalAlignment.Left);
                        listViewItems.Columns.Add("Name", 300, HorizontalAlignment.Left);
                        listViewItems.Columns.Add("Description", 300, HorizontalAlignment.Left);

                        foreach (ConceptualModelMeasureUnit unit in measureClass.Units)
                        {
                            string[] items = { $"{unit.Id}",
                                                $"{unit.Name}",
                                                $"{unit.Description}" };
                            listViewItems.Items.Add(new ListViewItem(items));
                        }

                        listViewItems.Dock = DockStyle.Fill;
                        listViewItems.Font = tabControl.Font;

                        TabPage pageItems = new TabPage($"Units ({measureClass.Units.Count})");
                        pageItems.Controls.Add(listViewItems);
                        tabControl.TabPages.Add(pageItems);
                    }
                }
            }
        }
    }
}
