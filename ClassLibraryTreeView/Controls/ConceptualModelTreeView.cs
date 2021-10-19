﻿using ClassLibraryTreeView.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassLibraryTreeView.Classes
{
    class ConceptualModelTreeView : TreeView
    {
        private ConceptualModel model = null;
        public event EventHandler<TreeNode> NodeClicked;
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
            int mode = viewMode;
            model = conceptualModel;
            // NodeMouseHover

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
                AddTaxomies(model);
            }

            if (mode == 3)
            {
                AddEnumerations(model);
            }
            
            if (mode == 4)
            {
                AddMeasures(model);
            }
        }
        protected override void OnNodeMouseClick(TreeNodeMouseClickEventArgs eventArgs)
        {
            if (eventArgs.Button == MouseButtons.Right)
            {
                this.SelectedNode = eventArgs.Node;

                System.Windows.Forms.ContextMenuStrip menu = new System.Windows.Forms.ContextMenuStrip();

                IConceptualModelObject nodeObject = (IConceptualModelObject)this.SelectedNode.Tag;

                if (nodeObject == null)
                {
                    return;
                }
                
                // ToolStripItem itemName = menu.Items.Add($"{nodeObject.Id} : {nodeObject.Name}");

                // ToolStripItem itemEdit = menu.Items.Add("Edit");
                // itemEdit.Image = global::ClassLibraryTreeView.Properties.Resources.edit;
                // itemEdit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
                // itemEdit.Click += new EventHandler(this.EditItem);

                ToolStripItem itemAdd = menu.Items.Add("Add");
                itemAdd.Image = global::ClassLibraryTreeView.Properties.Resources.add;
                itemAdd.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
                itemAdd.Click += new EventHandler(this.AddItem);

                ToolStripItem itemRemove = menu.Items.Add("Remove");
                itemRemove.Image = global::ClassLibraryTreeView.Properties.Resources.delete;
                itemRemove.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
                itemRemove.Click += new EventHandler(this.RemoveItem);

                // menu.Show((System.Windows.Forms.Control)sender, new Point(eventArgs.X, eventArgs.Y));
                menu.Show((System.Windows.Forms.Control)this, new Point(eventArgs.X, eventArgs.Y));
            }
        }
        protected override void OnAfterExpand(TreeViewEventArgs e)
        {
            base.OnAfterExpand(e);
            this.SelectedNode = e.Node;
        }
        protected override void OnNodeMouseDoubleClick(TreeNodeMouseClickEventArgs eventArgs)
        {
            if (eventArgs.Button == MouseButtons.Left && this.SelectedNode.Tag != null && this.SelectedNode.Tag is ConceptualModelObject)
            {
                this.NodeClicked?.Invoke(this, this.SelectedNode);
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
            node.ForeColor = this.SelectedNode.ForeColor;
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
        private TreeNode NewClassNode(ConceptualModelClass cmClass)
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
        private void AddClassChildren(ConceptualModelClass cmClass, TreeNode treeNode)
        {
            if (cmClass.Children.Count > 0)
            {
                foreach (ConceptualModelClass child in cmClass.Children.Values)
                {
                    TreeNode childNode = NewClassNode(child);
                    AddClassChildren(child, childNode);
                    treeNode.Nodes.Add(childNode);
                }
            }
        }
        public void AddAttributes(ConceptualModel model)
        {
            Dictionary<string, Dictionary<string, ConceptualModelAttribute>> attributes = model.attributes;
            foreach (string group in attributes.Keys)
            {
                TreeNode groupNode = new TreeNode(group);
                foreach (ConceptualModelAttribute attribute in attributes[group].Values)
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
            foreach (string classKey in model.classes.Keys)
            {
                this.AddClassMap(model.classes[classKey], classKey);
            }
        }
        private void AddClassMap(Dictionary<string, ConceptualModelClass> map, string xtype)
        {
            if (map.Count > 0)
            {
                TreeNode rootNode = new TreeNode(xtype);
                foreach (ConceptualModelClass cmClass in map.Values)
                {
                    if (cmClass.Parent == null)
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
            Dictionary<string, ConceptualModelEnumeration> map = model.enumerations;
            foreach (string key in map.Keys)
            {
                TreeNode treeNode = new TreeNode(key);
                treeNode.Text = map[key].Name;
                treeNode.Tag = map[key];
                rootNode.Nodes.Add(treeNode);

                foreach (ConceptualModelEnumerationItem item in map[key].Items)
                {
                    TreeNode treeSubNode = new TreeNode();
                    treeSubNode.Text = item.Id;
                    treeSubNode.Tag = item;
                    treeNode.Nodes.Add(treeSubNode);
                }
            }
            this.Nodes.Add(rootNode);
        }
        public void AddMeasures(ConceptualModel model)
        {
            TreeNode rootUnitsNode = new TreeNode($"Measure Units");
            Dictionary<string, ConceptualModelMeasureUnit> map = model.measureUnits;
            foreach (string key in map.Keys)
            {
                TreeNode treeNode = new TreeNode(key);
                treeNode.Text = map[key].Name;
                treeNode.Tag = map[key];
                rootUnitsNode.Nodes.Add(treeNode);
            }
            this.Nodes.Add(rootUnitsNode);

            TreeNode rootClassesNode = new TreeNode($"Measure Classes");
            Dictionary<string, ConceptualModelMeasureClass> mapClasses = model.measureClasses;
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
            foreach (ConceptualModelTaxonomy taxonomy in model.Taxonomies.Values)
            {
                TreeNode treeNode = new TreeNode(taxonomy.Id);
                treeNode.Text = taxonomy.Name;
                treeNode.Tag = taxonomy;
                for (int nodeIndex = 0; nodeIndex < taxonomy.Nodes.Count; nodeIndex++)
                {
                    ConceptualModelTaxonomyNode node = taxonomy.Nodes[nodeIndex];
                    TreeNode treeSubNode = new TreeNode();
                    treeSubNode.Text = node.Name;
                    treeSubNode.Tag = node;
                    for (int classIndex = 0; classIndex < node.Classes.Count; classIndex++)
                    {
                        treeSubNode.Nodes.Add(new TreeNode(node.Classes[classIndex]));
                    }
                    treeNode.Nodes.Add(treeSubNode);
                }
                rootNode.Nodes.Add(treeNode);
            }
            this.Nodes.Add(rootNode);
        }
    }
}
