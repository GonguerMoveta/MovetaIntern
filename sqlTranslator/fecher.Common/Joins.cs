using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace fecher.Common
{
    public class JoinsCollection : IEnumerable<Joins>
    {
        private List<Joins> list;

        public TreeNodeCollection this[TreeNode table1, TreeNode table2]
        {
            get
            {
                foreach (Joins joins in list)
                {
                    if ((joins.Table1 == table1 && joins.Table2 == table2) ||
                        (joins.Table2 == table1 && joins.Table1 == table2))
                    {
                        return joins.Items;
                    }
                }
                Joins temp = new Joins(table1, table2);
                list.Add(temp);
                return temp.Items;
            }
        }

        public JoinsCollection()
        {
            list = new List<Joins>();
        }

		public JoinsCollection Clone()
		{
			JoinsCollection clone = new JoinsCollection();
			clone.list.AddRange(this.list);
			return clone;
		}

        public int CountAll()
        {
            int count = 0;
            foreach (Joins joins in list)
            {
                int innerCount = 0;
                foreach (TreeNode treeNode in joins.Items)
                {
                    if (treeNode.NodeType == Tokens.Predicate)
                    {
                        innerCount++;
                    }
                }
                count += innerCount;
            }
            return count;
        }

        public TreeNodeCollection GetAll()
        {
            TreeNodeCollection allJoins = new TreeNodeCollection();
            foreach (Joins joins in list)
            {
                foreach (TreeNode item in joins.Items)
                {
                    allJoins.Add(item);
                }
            }
            return allJoins;
        }

        public void Sort()
        {
            if (list.Count > 0)
            {
                //First sort the joins according to their type: inner joins first then outer joins
                JoinsComparer jc = new JoinsComparer();
                list.Sort(jc);

                //Then order them so that the tables are connected
                List<Joins> tempList = new List<Joins>();
                tempList.Add(list[0]);
                list.RemoveAt(0);
                for (int i = 0; i < list.Count; i++)
                {
                    foreach (Joins joins in tempList)
                    {
                        if (joins.Table1 == list[i].Table1 || joins.Table2 == list[i].Table1 ||
                           joins.Table1 == list[i].Table2 || joins.Table2 == list[i].Table2)
                        {
                            tempList.Add(list[i]);
                            list.RemoveAt(i);
                            i = -1;
                            break;
                        }
                    }
                }
                list = tempList;
            }
        }

        public bool IsReversed(TreeNode table1, TreeNode table2)
        {
            foreach (Joins joins in list)
            {
                if (joins.Table2 == table1 && joins.Table1 == table2)
                {
                    return true;
                }
            }
            return false;
        }

        #region IEnumerable<Joins> Members

        public IEnumerator<Joins> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
    
    public class Joins
    {
        private TreeNode table1;
        private TreeNode table2;
        private TreeNodeCollection items;

        public TreeNode Table1
        {
            get { return table1; }
            set { table1 = value; }
        }

        public TreeNode Table2
        {
            get { return table2; }
            set { table2 = value; }
        }

        public TreeNodeCollection Items
        {
            get { return items; }
            set { items = value; }
        }

        public Joins(TreeNode table1, TreeNode table2)
        {
            this.table1 = table1;
            this.table2 = table2;
            this.items = new TreeNodeCollection();
        }

        public TreeNode GetOuterJoin()
        {
            foreach (TreeNode node in items)
            {
                if (node.NodeInfo.Contains("OUTER"))
                {
                    return node;
                }
            }
            return null;
        }
    }

    internal class JoinsComparer : IComparer<Joins>
    {
        #region IComparer<Joins> Members

        public int Compare(Joins x, Joins y)
        {
            //Inner joins come first and then the outer joins and finally outer joins with bind variables
            if (x.Items.Count > 0 && y.Items.Count > 0)
            {
                if (x.Table1 == null || x.Table2 == null)
                {
                    return 1;
                }
                else if (y.Table1 == null || y.Table2 == null)
                {
                    return -1;
                }
                else if (x.Items[0].NodeInfo == y.Items[0].NodeInfo)
                {
                    return 0;
                }
                else if (x.Items[0].NodeInfo == "INNER JOIN")
                {
                    return -1;
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                return 0;
            }
        }

        #endregion
    }

}
