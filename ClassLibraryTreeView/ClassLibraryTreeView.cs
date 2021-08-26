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
        private TreeNode NewNode(CMClass cmClass)
        {
            TreeNode treeNode = new TreeNode();
            treeNode.Text = cmClass.Name;
            treeNode.Name = $"{cmClass.Xtype}";
            treeNode.Tag = $"{cmClass.Id}";
            return treeNode;
        }
        private void AddChildren(CMClass cmClass, TreeNode treeNode)
        {
            if (cmClass.Descendants.Count == 0)
            {
                return;
            }

            foreach(CMClass child in cmClass.Descendants)
            {
                TreeNode childNode = NewNode(child);
                AddChildren(child, childNode);
                treeNode.Nodes.Add(childNode);
            }
        }
        public void AddClass(Dictionary<string, CMClass> map, string xtype)
        {
            TreeNode rootNode = new TreeNode(xtype);
            foreach (CMClass cmClass in map.Values)
            {
                if (!cmClass.HasParent(map))
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
