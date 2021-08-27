using ClassLibraryTreeView.Classes;
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
