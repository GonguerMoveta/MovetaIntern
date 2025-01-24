using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace fecher.Common
{
    /// <summary>
    /// Represents the collection that maintains the tables contained in a <see cref="T:MsSqlView"/> object
    /// </summary>
    public class MsSqlViewCollection : CollectionBase
    {
        #region Indexer
        /// <summary>
        /// Provides indexed access to the elements in the collection.
        /// </summary>
		/// <param name="index">An integer value specifying the zero-based index of the required <see cref="T:MsSqlView" /> object.</param>
		/// <value>A <see cref="T:MsSqlView" /> object representing the item in the collection.</value>
        public MsSqlView this[int index]
        {
            get
            {
                return (MsSqlView)base.List[index];
            }
        }

        /// <summary>
        /// Gets the <see cref="T:MsSqlView" /> specified by the view name.
        /// </summary>
        /// <param name="name">A string value specifying the view name.</param>
        /// <returns></returns>
        public MsSqlView this[string name]
        {
            get
            {
                name = name.ToUpper().RemoveBrackets();
                return base.List.OfType<MsSqlView>().Where(s => s.Name == name).FirstOrDefault();
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Adds a <see cref="T:MsSqlView" /> object to the list.
        /// </summary>
        /// <param name="view">A <see cref="T:MsSqlView" /> object representing the view.</param>
        /// <returns>Index of the view in collection</returns>
        public int Add(MsSqlView view)
        {
            int index = base.List.IndexOf(view);
            if(index == -1)
            {
                index = base.List.Add(view);
            }
            return index;
        }
        #endregion
    }
}
