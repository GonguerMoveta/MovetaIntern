using System;
using System.Collections;
using System.Collections.Specialized;

namespace fecher.Common
{
	/// <summary>
	/// Class that represents the nodes of the syntax tree
	/// </summary>
	public class TreeNodeCollection : IEnumerable
	{
		private ArrayList myNodes = new ArrayList();

		public TreeNodeCollection()
		{
		}

		public TreeNodeCollection(TreeNodeCollection nodeCollection)
		{
            foreach (TreeNode node in nodeCollection)
            {
                myNodes.Add(node);
            }
		}

        //@RDU new constructor
        public TreeNodeCollection(TreeNodeCollection nodeCollection, string useless)
        {
            foreach (TreeNode node in nodeCollection)
            {
                myNodes.Add(new TreeNode(node, useless));
            }
        }
        //

        public TreeNode this[int index]
		{
			get	{return (TreeNode)myNodes[index]; }
			set {myNodes[index] = value; }
		}

		public int Count
		{
			get {return myNodes.Count; }
		}

		public void Add(TreeNode treeNode)
		{
			myNodes.Add(treeNode);
		}

		public void Remove(TreeNode treeNode)
		{
			myNodes.Remove(treeNode);
		}

		public void RemoveAt(int index)
		{
			myNodes.RemoveAt(index);
		}

		public int IndexOf(object value)
		{
			return myNodes.IndexOf(value);
		}

        public bool Contains(TreeNode treeNode)
        {
            return myNodes.Contains(treeNode);
        }

        public void Replace(TreeNode node1, TreeNode node2)
        {
            myNodes[myNodes.IndexOf(node1)] = node2;
        }

        public void Insert(TreeNode node, int beforeindex)
        {
            TreeNode newNode = new TreeNode(Tokens.Null);
            this.Add(newNode);
            for (int i = this.Count - 1; i > beforeindex; i--)
            {
                this[i] = this[i - 1];
            }
            this[beforeindex] = node;
        }

        public TreeNode Find(string nodeInfo)
        {
            foreach (TreeNode node in myNodes)
            {
                if (node.NodeInfo == nodeInfo)
                {
                    return node;
                }
            }
            return null;
        }

        public void Clear()
        {
            myNodes.Clear();
        }

        #region IEnumerable Members
        public IEnumerator GetEnumerator()
		{
			return (IEnumerator)new TreeNodeEnumerator(this);
		}

		private class TreeNodeEnumerator : IEnumerator
		{
			TreeNodeCollection theNodes;
			int myIndex = -1;

			public TreeNodeEnumerator(TreeNodeCollection theNodes)
			{
				this.theNodes = theNodes;
				myIndex = -1;
			}

			#region IEnumerator Members
			public void Reset()
			{
				myIndex = -1;
			}

			public object Current
			{
				get {return theNodes.myNodes[myIndex]; }
			}

			public bool MoveNext()
			{
				myIndex++;
                return myIndex < theNodes.myNodes.Count;
			}
			#endregion
		}
		#endregion
	}
}
