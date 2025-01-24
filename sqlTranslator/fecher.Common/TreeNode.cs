using System;
using System.Collections;
using System.Collections.Specialized;

namespace fecher.Common
{
	/// <summary>
	/// Class that represents the nodes of the syntax tree
	/// </summary>
	public class TreeNode
    {
        #region Private Members
        private Tokens nodeType;
		private string nodeValue;
		private string nodeInfo; //string for storing additional information about the node
        private TreeNode parent;
		private TreeNodeCollection children;
        //Store the original node value - used for converting in Oracle the @DATETOCHAR in the @DECODE statement
        //@DATETOCHAR is translated with TO_CHAR. SQLBase does not aply other format to returned value from @DATETOCHAR
        //In the translator we need a method to identify if the node was @DATETOCHAR translated to TO_CHAR
        //or if the node was translated with TO_CHAR from different reasons (date colum cast, etc.)
        private string originalNodeValue = "";
        #endregion

        #region Properties
        public Tokens NodeType
		{
			get { return nodeType; }
			set { nodeType = value; }
		}

        public string NodeValue
        {
            get
            {
                return nodeValue;
            }
            set
            {
                nodeValue = value;
                OriginalNodeValue = NodeValue;
            }
        }

        public string NodeInfo
		{
			get { return nodeInfo; }
			set { nodeInfo = value; }
        }

        public TreeNode Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        public TreeNodeCollection Children
        {
            get { return children; }
        }

        public string OriginalNodeValue
        {
            get
            {
                return originalNodeValue;
            }
            set
            {
                if (originalNodeValue == "")
                {
                    originalNodeValue = value;
                }
            }
        }

        //ASZ:index of TreeNode 
        public int Index
        {
            get
            {
                return (Parent == null) ? -1 : Parent.Children.IndexOf(this);
            }
        }

        #endregion

        #region Constructors
        public TreeNode(TreeNode node)
		{
			this.nodeType = node.nodeType;
			this.nodeValue = node.NodeValue;
            this.originalNodeValue = node.NodeValue;
			this.nodeInfo = node.nodeInfo;			
			this.children = node.children;
		}

		//CO20061010 New constructor added to copy the nodeColletion into another object
		public TreeNode(TreeNode node, string useless)
		{
			this.nodeType = node.nodeType;
			this.nodeValue = node.NodeValue;
            this.originalNodeValue = node.NodeValue;
			this.nodeInfo = node.nodeInfo;			
			this.children = new TreeNodeCollection(node.children, useless);
		}

		public TreeNode(Tokens type)
		{
			CommonContructor(type, string.Empty, string.Empty);
		}

		public TreeNode(Tokens type, string val)
		{
			CommonContructor(type, val, string.Empty);
		}

		public TreeNode(Tokens type, string val, string info)
		{
			CommonContructor(type, val, info);
		}

		private void CommonContructor(Tokens type, string val, string info)
		{
			this.nodeType = type;
			this.nodeValue = val;
            this.originalNodeValue = val;
			this.nodeInfo = info;
			this.children = new TreeNodeCollection();
        }
        #endregion

        #region Public Methods
        public TreeNode AddChild(TreeNode node)
        {
            this.children.Add(node);
            node.Parent = this;
            return this.children[this.children.IndexOf(node)];
        }

        public TreeNode InsertChild(TreeNode node, int beforeIndex)
        {
            this.children.Insert(node, beforeIndex);
            node.Parent = this;
            return this.children[this.children.IndexOf(node)];
        }

		public void RemoveChild(TreeNode node)
		{
			this.children.Remove(node);
		}

        public void RemoveAll()
        {
            while (this.children.Count > 0)
            {
                this.children.RemoveAt(0);
            }
        }

        public void RemoveChild(int index)
		{
			this.children.RemoveAt(index);
		}

		public bool IsLeaf()
		{
            return this.children.Count == 0;
		}

        public bool IsFirstChild(TreeNode node)
        {
            return this.children.IndexOf(node) == 0;
        }

        public bool IsLastChild(TreeNode node)
        {
            return this.children.IndexOf(node) == this.children.Count - 1;
        }
        #endregion

        public override string ToString()
        {
            return Tree.Traverse(this);
        }

        public TreeNode FindChild(string nodeValue)
        {
            int i = this.Children.Count;
            while (--i >= 0 && this.Children[i].NodeValue.ToUpper() != nodeValue.ToUpper()) ;

            if (i >= 0)
            {
                return this.Children[i];
            }
            else
            {
                return null;
            }
        }

        //ASZ:clones a TreeNode structure and sets Parent node far all nodes
        public TreeNode Clone(TreeNode parentNode)
        {
            TreeNode node = new TreeNode(this.NodeType, this.NodeValue, this.NodeInfo);
            node.Parent = parentNode;

            for (int i = 0; i < this.Children.Count; i++)
            {
                node.Children.Add(this.Children[i].Clone(node));
            }

            return node;
        }
    }

}
