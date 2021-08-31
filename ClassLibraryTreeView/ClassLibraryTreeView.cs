using ClassLibraryTreeView.Classes;
using ClassLibraryTreeView.Interfaces;
using System;
using System.Collections.Generic;
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
        public void AddClass(Dictionary<string, IClass> map, string xtype)
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
