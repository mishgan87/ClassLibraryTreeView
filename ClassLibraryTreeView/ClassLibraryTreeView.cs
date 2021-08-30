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
        private TreeNode NewNode(IAttribute attribute)
        {
            TreeNode treeNode = new TreeNode();
            treeNode.Text = $"{attribute.Name}";
            treeNode.Tag = $"{attribute.Id}";
            return treeNode;
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
        public void AddAttributes(Dictionary<string, IAttribute> attributes)
        {
            TreeNode nodeUnset = new TreeNode("Unset");
            TreeNode nodeCommon = new TreeNode("Common");
            TreeNode nodeDocument = new TreeNode("Document");
            TreeNode nodeHazardous = new TreeNode("Hazardous");
            TreeNode nodeSpecific = new TreeNode("Specific");

            foreach (IAttribute attribute in attributes.Values)
            {
                TreeNode newNode = NewNode(attribute);
                string groupId = attribute.Group.ToLower();
                if (groupId.Contains("common"))
                {
                    nodeCommon.Nodes.Add(newNode);
                    
                }
                else
                {
                    if (groupId.Contains("document"))
                    {
                        nodeDocument.Nodes.Add(newNode);
                    }
                    else
                    {
                        if (groupId.Contains("hazardous"))
                        {
                            nodeHazardous.Nodes.Add(newNode);
                        }
                        else
                        {
                            if (groupId.Contains("specific"))
                            {
                                nodeSpecific.Nodes.Add(newNode);
                            }
                            else
                            {
                                nodeUnset.Nodes.Add(newNode);
                            }
                        }
                    }
                }
            }
            Nodes.Add(nodeUnset);
            Nodes.Add(nodeCommon);
            Nodes.Add(nodeDocument);
            Nodes.Add(nodeHazardous);
            Nodes.Add(nodeSpecific);
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
    }
}
