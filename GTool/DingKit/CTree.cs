using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.UI.WebControls;
//using System.Web.Security;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using System.Web.UI.WebControls.WebParts;
//using System.Web.UI.HtmlControls;

namespace DingKit
{

    /// <summary>
    /// 树形控件的操作方法
    /// </summary>
    public class CTree
    {
        /// <summary>
        /// 构造
        /// </summary>
        public CTree()
        { }

        /// <summary>
        /// 查找数型控件节点
        /// </summary>
        /// <param name="Tree_left">树</param>
        /// <param name="Url">Url</param>
        /// <returns>Url所在节点</returns>
        public TreeNode FindNode(ref System.Web.UI.WebControls.TreeView Tree_left, string Url)
        {
            TreeNode rNode = new TreeNode();
            rNode = null;
            foreach (TreeNode node in Tree_left.Nodes)
            {
                if (node.NavigateUrl.ToString() == Url)
                {
                    return node;
                }
                else
                {

                    rNode = FindNode(node, Url);
                    if (rNode != null)
                    {
                        return rNode;
                    }

                }
            }
            return rNode;
        }

        /// <summary>
        /// 查找数型控件节点
        /// </summary>
        /// <param name="ParentNode">父节点</param>
        /// <param name="Url">Url</param>
        /// <returns>Url所在节点</returns>
        private TreeNode FindNode(System.Web.UI.WebControls.TreeNode ParentNode, string Url)
        {
            if (ParentNode.ChildNodes.Count == 0)
            {
                return null;
            }
            TreeNode rNode = new TreeNode();
            rNode = null;
            foreach (TreeNode node in ParentNode.ChildNodes)
            {
                if (node.NavigateUrl.ToString() == Url)
                {
                    return node;
                }
                else
                {
                    rNode = FindNode(node, Url);
                    if (rNode != null)
                    {
                        return rNode;
                    }
                }
            }
            return rNode;
        }

        /// <summary>
        /// 节点展开
        /// </summary>
        /// <param name="Tree_left">树</param>
        /// <param name="Url">Url</param>
        public void ExplandNode(ref System.Web.UI.WebControls.TreeView Tree_left, string Url)
        {

            TreeNode node = FindNode(ref Tree_left, Url);
            Tree_left.CollapseAll();
            node = FindBootNode(node);
            if (node != null)
            {
                node.ExpandAll();
            }
        }

        /// <summary>
        /// 返回根到当前节点的路径
        /// </summary>
        /// <param name="Tree_left">树</param>
        /// <param name="Url">Url</param>
        /// <returns></returns>
        public string GetPathText(ref System.Web.UI.WebControls.TreeView Tree_left, string Url)
        {
            TreeNode node = FindNode(ref Tree_left, Url);
            if (node != null)
            {
                return node.Text;
            }
            return "";
        }

        /// <summary>
        /// 查找根节点
        /// </summary>
        /// <param name="Tree_left">树</param>
        /// <param name="Url">Url</param>
        /// <returns></returns>
        public TreeNode FindBootNode(ref System.Web.UI.WebControls.TreeView Tree_left, string Url)
        {
            TreeNode node = FindNode(ref Tree_left, Url);
            while (node.Parent != null)
            {
                node = node.Parent;
            }
            return node;
        }


        /// <summary>
        /// 查找根节点
        /// </summary>
        /// <param name="node">当前节点</param>
        /// <returns></returns>
        public TreeNode FindBootNode(System.Web.UI.WebControls.TreeNode node)
        {
            if (node == null)
            {
                return null;
            }
            while (node.Parent != null)
            {
                node = node.Parent;
            }
            return node;
        }

        /// <summary>
        /// 当前节点展开
        /// </summary>
        /// <param name="Tree_left">树</param>
        /// <param name="Url">节点ID</param>
        public void ExplandCurrentNode(ref System.Web.UI.WebControls.TreeView Tree_left, string Url)
        {
            TreeNode node = FindNode(ref Tree_left, Url);
            Tree_left.CollapseAll();
            ExplandParentNode(node);
            if (node != null)
            {
                node.Expand();
            }
        }

        /// <summary>
        /// 展开祖父节点
        /// </summary>
        /// <param name="node"></param>
        public void ExplandParentNode(System.Web.UI.WebControls.TreeNode node)
        {
            if (node == null)
            {
                return;
            }
            while (node.Parent != null)
            {
                node = node.Parent;
                node.Expand();
            }
        }
    }
}
