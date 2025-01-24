using System;
using System.Linq;

namespace fecher.Common
{
	/// <summary>
	/// Base class for the syntax tree
	/// </summary>
	public class Tree
	{
		public TreeNode root;
		
		public Tree(TreeNode node)
		{
			this.root = node;
		}

		//Traverse the tree in preorder and construct the target string
		public static string Traverse(TreeNode node)
		{
			string sql = "";

            if (node.NodeValue != "" && node.NodeInfo != "NOT USED")
            {
                if (node.NodeValue == "END" || node.NodeType == Tokens.Variable || node.NodeValue == "AS")
                {
                    sql += "\r\n";
                }
                sql += node.NodeValue + " ";
                if (node.NodeValue == "AS" || node.NodeValue == "BEGIN")
                {
                    sql += "\r\n";
                }
            }
            for (int i = 0; i < node.Children.Count; i++)
            {
                if (node.NodeInfo != "NOT USED")
                {
                    sql += Traverse((TreeNode)node.Children[i]);
                }
            }

			return sql;
		}

		public void RemoveNode(TreeNode node)
		{
			FindParent(this.root, node).RemoveChild(node);
		}

		//Returns the parent of the 'child' node
		public TreeNode FindParent(TreeNode parent, TreeNode child)
		{
            if (parent == null) return null;
            if (parent.IsLeaf())
            {
                return null;
            }
            else
            {
                int num = parent.Children.Count;
                for (int i = 0; i < num; i++)
                    if (parent.Children[i] == child)
                    {
                        return parent;
                    }

                int j = 0;
                TreeNode found = null;
                while ((found == null) && (j < num))
                {
                    found = FindParent((TreeNode)parent.Children[j], child);
                    j++;
                }
                return found;
            }
		}

        public TreeNode FindParent(TreeNode child, Tokens nodeType)
        {
            if (child == null) return null;
            
            TreeNode parent = child.Parent;
            while (parent != null)
            {
                if (parent.NodeType == nodeType) break;
                parent = parent.Parent;
            }

            return parent;
        }

        public TreeNode FindParent(TreeNode child, string nodeValue)
        {
            if (child == null) return null;

            TreeNode parent = child.Parent;
            while (parent != null)
            {
                if (parent.NodeValue == nodeValue) break;
                parent = parent.Parent;
            }

            return parent;
        }

        public bool FindNode(TreeNode start, TreeNode node)
        {
            if (start == null) return false;
            if (start == node) return true;
            else
            {
                if (start.IsLeaf()) return false;
                else
                {
                    int num = start.Children.Count;
                    int i = 0;
                    bool found = false;
                    while ((!found) && (i < num))
                    {
                        found = FindNode((TreeNode)start.Children[i], node);
                        i++;
                    }
                    return found;
                }
            }
        }

		public TreeNode FindNode(TreeNode start, string nodeValue)
		{
            if (start == null) return null;
            if ( start.NodeValue.ToUpper() == nodeValue.ToUpper()) return start;
			else
			{
				if (start.IsLeaf()) return null;
				else
				{
					int num = start.Children.Count;
					int i = 0;
					TreeNode found = null;
					while ((found == null) && (i < num))
					{
						found = FindNode((TreeNode)start.Children[i], nodeValue);
						i++;
					}
					return found;
				}
			}
		}

		public TreeNode FindNode(TreeNode start, Tokens nodeType)
		{
            return FindNode(start, new Tokens[] { nodeType });
        }

        public TreeNode FindNode(TreeNode start, Tokens[] nodeTypes)
        {
            if (start == null) return null;
            if (nodeTypes.Contains(start.NodeType)) return start;
            else
            {
                if (start.IsLeaf()) return null;
                else
                {
                    int num = start.Children.Count;
                    int i = 0;
                    TreeNode found = null;
                    while ((found == null) && (i < num))
                    {
                        found = FindNode((TreeNode)start.Children[i], nodeTypes);
                        i++;
                    }
                    return found;
                }
            }
        }

        public bool IsNodeFromSearch(TreeNode start, TreeNode node)
        {
            TreeNode interm = node;
            while (interm != null && interm.NodeType != Tokens.Root)
            {
                if (interm.NodeType == Tokens.SearchCond)
                {
                    return true;
                }
                interm = interm.Parent;
            }
            return false;
        }

        public bool IsNodeFromCase(TreeNode start, TreeNode node)
        {
            TreeNode interm = node;
            while (interm != null && interm.NodeType != Tokens.Root)
            {
                if (interm.NodeType == Tokens.Function && (interm.NodeValue == "CASE" || interm.NodeValue == "CAST(CASE"))
                {
                    return true;
                }
                interm = interm.Parent;
            }

            return false;
        }

        public TreeNode FindNode(TreeNode start, Tokens nodeType, string nodeValue)
        {
            return FindNode(start, nodeType, new string[] { nodeValue });
        }

        public TreeNode FindNode(TreeNode start, Tokens nodeType, string[] values)
        {
            if (start == null) return null;
            if (start.NodeType == nodeType && values.Contains(start.NodeValue)) return start;
            else
            {
                if (start.IsLeaf()) return null;
                else
                {
                    int num = start.Children.Count;
                    int i = 0;
                    TreeNode found = null;
                    while ((found == null) && (i < num))
                    {
                        found = FindNode((TreeNode)start.Children[i], nodeType, values);
                        i++;
                    }
                    return found;
                }
            }
        }

        public TreeNode FindNodeReverse(TreeNode start, Tokens nodeType)
        {
            if (start == null) return null;
            if (start.NodeType == nodeType) return start;
            else
            {
                if (start.IsLeaf()) return null;
                else
                {
                    int num = start.Children.Count;
                    int i = num - 1;
                    TreeNode found = null;
                    while ((found == null) && (i >= 0))
                    {
                        found = FindNode((TreeNode)start.Children[i], nodeType);
                        i--;
                    }
                    return found;
                }
            }
        }

		public TreeNode FindNode(TreeNode start, string nodeInfo, byte useless)
		{
            if (start == null) return null;
            if ( start.NodeInfo.ToUpper() == nodeInfo.ToUpper()) return start;
			else
			{
				if (start.IsLeaf()) return null;
				else
				{
					int num = start.Children.Count;
					int i = 0;
					TreeNode found = null;
					while ((found == null) && (i < num))
					{
						found = FindNode((TreeNode)start.Children[i], nodeInfo, 0);
						i++;
					}
					return found;
				}
			}
		}

        public int FindAll(TreeNode start, string nodeValue)
        {
            int count = 0;
            if (start == null) return 0;
            if (start.NodeValue.ToUpper() == nodeValue.ToUpper()) return 1;
            else
            {
                if (start.IsLeaf()) return 0;
                else
                {
                    int num = start.Children.Count;
                    int i = 0;
                    while (i < num)
                    {
                        count += FindAll((TreeNode)start.Children[i], nodeValue);
                        i++;
                    }
                    return count;
                }
            }
        }

        public int FindAll(TreeNode start, Tokens nodeType)
        {
            int count = 0;
            if (start == null) return 0;
            if (start.NodeType == nodeType) return 1;
            else
            {
                if (start.IsLeaf()) return 0;
                else
                {
                    int num = start.Children.Count;
                    int i = 0;
                    while (i < num)
                    {
                        count += FindAll((TreeNode)start.Children[i], nodeType);
                        i++;
                    }
                    return count;
                }
            }
        }

        public int FindAllUsed(TreeNode start, Tokens nodeType)
        {
            int count = 0;
            if (start == null) return 0;
            if (start.NodeType == nodeType && start.NodeInfo!="NOT USED") return 1;
            else
            {
                if (start.IsLeaf()) return 0;
                else
                {
                    int num = start.Children.Count;
                    int i = 0;
                    while (i < num)
                    {
                        count += FindAllUsed((TreeNode)start.Children[i], nodeType);
                        i++;
                    }
                    return count;
                }
            }
        }

        public int FindAllUsed(TreeNode start, Tokens nodeType, ref TreeNodeCollection nodes)
        {
            int count = 0;
            if (start == null) return 0;
            if (start.NodeType == nodeType && start.NodeInfo != "NOT USED")
            {
                nodes.Add(start);
                return 1;
            }
            else
            {
                if (start.IsLeaf()) return 0;
                else
                {
                    int num = start.Children.Count;
                    int i = 0;
                    while (i < num)
                    {
                        count += FindAllUsed((TreeNode)start.Children[i], nodeType, ref nodes);
                        i++;
                    }
                    return count;
                }
            }
        }

        public int FindAll(TreeNode start, Tokens nodeType, ref TreeNodeCollection nodes)
        {
            int count = 0;
            if (start == null) return 0;
            if (start.NodeType == nodeType)
            {
                nodes.Add(start);
                return 1;
            }
            else
            {
                if (start.IsLeaf()) return 0;
                else
                {
                    int num = start.Children.Count;
                    int i = 0;
                    while (i < num)
                    {
                        count += FindAll((TreeNode)start.Children[i], nodeType, ref nodes);
                        i++;
                    }
                    return count;
                }
            }
        }

        public int FindAll(TreeNode start, string nodeValue, ref TreeNodeCollection nodes)
        {
            int count = 0;
            if (start == null) return 0;
            if (start.NodeValue == nodeValue)
            {
                nodes.Add(start);
                return 1;
            }
            else
            {
                if (start.IsLeaf()) return 0;
                else
                {
                    int num = start.Children.Count;
                    int i = 0;
                    while (i < num)
                    {
                        count += FindAll((TreeNode)start.Children[i], nodeValue, ref nodes);
                        i++;
                    }
                    return count;
                }
            }
        }

        public int FindAllNested(TreeNode start, Tokens nodeType)
        {
            int count = 0;
            if (start == null) return 0;
            if (start.NodeType == nodeType)
            {
                count++;
            }
            if (start.IsLeaf()) return count;
            else
            {
                int num = start.Children.Count;
                int i = 0;
                while (i < num)
                {
                    count += FindAllNested((TreeNode)start.Children[i], nodeType);
                    i++;
                }
                return count;
            }
        }

        public TreeNode FindTopmostExpression(TreeNode start)
        {
            if (start == null) return null;

            TreeNode parent = start.Parent;
            while (parent != null && parent.Parent != null && parent.Parent.Parent != null && parent.Parent.Parent.NodeType != Tokens.Select && parent.Parent.Parent.NodeType != Tokens.Update)
            {
                parent = parent.Parent;
            }
            if (root.Children.Contains(parent))
            {
                return null;
            }
            return parent;
        }

        public TreeNode PreviousNode(TreeNode node, Tokens nodeType)
        {
            TreeNode parent = node.Parent;
            int index = parent.Children.IndexOf(node);
            while (--index >= 0)
            {
                if (parent.Children[index].NodeType == nodeType)
                {
                    return parent.Children[index];
                }
            }
            return null;
        }

        public TreeNode NextNode(TreeNode node, Tokens nodeType)
        {
            TreeNode parent = node.Parent;
            int index = parent.Children.IndexOf(node);
            while (++index < parent.Children.Count)
            {
                if (parent.Children[index].NodeType == nodeType)
                {
                    return parent.Children[index];
                }
            }
            return null;
        }

        public bool IsLastNode(TreeNode parent, TreeNode node)
        {
            if (parent.IsLastChild(node))
            {
                return true;
            }
            int index = parent.Children.IndexOf(node);
            for (int i = index + 1; i < parent.Children.Count; i++)
            {
                if (parent.Children[i].NodeInfo != "NOT USED")
                {
                    return false;
                }
            }
            return true;
        }
    }
}
