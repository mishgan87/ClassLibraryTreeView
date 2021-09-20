using ClassLibraryTreeView.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassLibraryTreeView.Classes
{
    class ConceptualModelTreeView : TreeView
    {
        private ConceptualModel model = null;
        private int mode = -1;
        public ConceptualModelTreeView() : base()
        {
        }
        /// <summary>
        /// construct custom tree view with conceptual model classes or attributes
        /// </summary>
        /// <param name="model"></param>
        /// <param name="mode">0 - classes, 1 - attributes, 2 - enumerations, 3 - measure classes and units, 4 - taxonomies</param>
        public ConceptualModelTreeView(ConceptualModel conceptualModel, int viewMode) : base()
        {
            mode = viewMode;
            model = conceptualModel;

            NodeMouseClick += new TreeNodeMouseClickEventHandler(this.ShowContextMenu);

            this.LabelEdit = true;
            this.Dock = DockStyle.Fill;
            this.Nodes.Clear();

            if (mode == 0)
            {
                AddClasses(model);
            }

            if (mode == 1)
            {
                AddAttributes(model);
            }

            if (mode == 2)
            {
                AddEnumerations(model);
            }

            if (mode == 3)
            {
                AddMeasures(model);
            }
            
            if (mode == 4)
            {
                AddTaxomies(model);
            }
        }
        private void ShowContextMenu(object sender, TreeNodeMouseClickEventArgs eventArgs)
        {
            if (eventArgs.Button == MouseButtons.Right)
            {
                this.SelectedNode = eventArgs.Node;

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
        }
        private void EditItem(object sender, EventArgs e)
        {
            if (!this.SelectedNode.IsEditing)
            {
                this.SelectedNode.BeginEdit();
            }
        }
        private void AddItem(object sender, EventArgs e)
        {
            TreeNode node = new TreeNode(" ");
            this.SelectedNode.Nodes.Add(node);
            this.SelectedNode = node;
            if (!node.IsEditing)
            {
                node.BeginEdit();
            }
        }
        private void RemoveItem(object sender, EventArgs e)
        {
            this.Nodes.Remove(this.SelectedNode);
        }
        private TreeNode NewClassNode(IClass cmClass)
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
        private void AddClassChildren(IClass cmClass, TreeNode treeNode)
        {
            if (cmClass.Children.Count > 0)
            {
                foreach (IClass child in cmClass.Children.Values)
                {
                    TreeNode childNode = NewClassNode(child);
                    AddClassChildren(child, childNode);
                    treeNode.Nodes.Add(childNode);
                }
            }
        }
        public void AddAttributes(ConceptualModel model)
        {
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
                this.Nodes.Add(groupNode);
            }
        }
        public void AddClasses(ConceptualModel model)
        {
            foreach (var classKey in model.classes.Keys)
            {
                this.AddClassMap(model.classes[classKey], classKey);
            }
        }
        private void AddClassMap(Dictionary<string, IClass> map, string xtype)
        {
            if (map.Count > 0)
            {
                TreeNode rootNode = new TreeNode(xtype);
                foreach (IClass cmClass in map.Values)
                {
                    if (cmClass.Extends.Equals(""))
                    {
                        TreeNode newNode = NewClassNode(cmClass);
                        AddClassChildren(cmClass, newNode);
                        rootNode.Nodes.Add(newNode);
                    }
                }
                this.Nodes.Add(rootNode);
            }
        }
        public void AddEnumerations(ConceptualModel model)
        {
            TreeNode rootNode = new TreeNode($"Enumerations");
            Dictionary<string, EnumerationList> map = model.enumerations;
            foreach (string key in map.Keys)
            {
                TreeNode treeNode = new TreeNode(key);
                treeNode.Text = map[key].Name;
                treeNode.Tag = map[key];
                rootNode.Nodes.Add(treeNode);
            }
            this.Nodes.Add(rootNode);
        }
        public void AddMeasures(ConceptualModel model)
        {
            TreeNode rootUnitsNode = new TreeNode($"Measure Units");
            Dictionary<string, MeasureUnit> map = model.measureUnits;
            foreach (string key in map.Keys)
            {
                TreeNode treeNode = new TreeNode(key);
                treeNode.Text = map[key].Name;
                treeNode.Tag = map[key];
                rootUnitsNode.Nodes.Add(treeNode);
            }
            this.Nodes.Add(rootUnitsNode);

            TreeNode rootClassesNode = new TreeNode($"Measure Classes");
            Dictionary<string, MeasureClass> mapClasses = model.measureClasses;
            foreach (string key in mapClasses.Keys)
            {
                TreeNode treeNode = new TreeNode(key);
                treeNode.Text = mapClasses[key].Name;
                treeNode.Tag = mapClasses[key];
                rootClassesNode.Nodes.Add(treeNode);
            }
            this.Nodes.Add(rootClassesNode);
        }

        public void AddTaxomies(ConceptualModel model)
        {
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
            this.Nodes.Add(rootNode);
        }
    }
}
