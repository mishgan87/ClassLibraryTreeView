using ClassLibraryTreeView.Classes;
using ClassLibraryTreeView.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassLibraryTreeView
{
    class ClassLibraryTreeView : TreeView
    {
        public ClassLibraryTreeView() : base()
        {
        }
        private TreeNode NewNode(IClass cmClass)
        {
            TreeNode treeNode = new TreeNode();
            treeNode.Text = cmClass.Name;
            treeNode.Name = $"{cmClass.Xtype}";
            treeNode.Tag = $"{cmClass.Id}";
            if (cmClass.Xtype.ToLower().Equals("functionals"))
            {
                treeNode.ForeColor = Color.Green;
            }
            if (cmClass.Xtype.ToLower().Equals("physicals"))
            {
                treeNode.ForeColor = Color.DarkBlue;
            }
            return treeNode;
        }
        private void AddChildren(IClass cmClass, TreeNode treeNode)
        {
            if (cmClass.Children.Count == 0)
            {
                return;
            }

            foreach(IClass child in cmClass.Children)
            {
                TreeNode childNode = NewNode(child);
                AddChildren(child, childNode);
                treeNode.Nodes.Add(childNode);
            }
        }
        public void AddAttributes(ConceptualModel model)
        {
            Dictionary<string, Dictionary<string, IAttribute>> attributes = model.attributes;
            foreach(string group in attributes.Keys)
            {
                TreeNode groupNode = new TreeNode(group);
                foreach(IAttribute attribute in attributes[group].Values)
                {
                    TreeNode treeNode = new TreeNode();
                    treeNode.Name = attribute.Group;
                    treeNode.Text = attribute.Name;
                    treeNode.Tag = attribute.Id;
                    groupNode.Nodes.Add(treeNode);
                }
                Nodes.Add(groupNode);
            }
        }
        public void AddEnumerations(ConceptualModel model)
        {
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
            Nodes.Add(rootNode);
        }
        public void AddMeasure(ConceptualModel model)
        {
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
            Nodes.Add(rootUnitsNode);

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
            Nodes.Add(rootClassesNode);
        }
        public void AddClass(Dictionary<string, IClass> map, string xtype)
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
                Nodes.Add(rootNode);
            }
        }
        public void AddList(ConceptualModel model, string text)
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
            Nodes.Add(rootNode);
        }
    }
}
