using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace fecher.Common
{
    /// <summary>
    /// Represents the collection that maintains the tables contained in a <see cref="T:MsSqlSchema"/> object
    /// </summary>
    public class MsSqlTableCollection : CollectionBase
    {
        #region Indexer
        /// <summary>
        /// Provides indexed access to the elements in the collection.
        /// </summary>
		/// <param name="index">An integer value specifying the zero-based index of the required <see cref="T:MsSqlTable" /> object.</param>
		/// <value>A <see cref="T:MsSqlTable" /> object representing the item in the collection.</value>
        public MsSqlTable this[int index]
        {
            get
            {
                return (MsSqlTable)base.List[index];
            }
        }

        /// <summary>
        /// Gets the <see cref="T:MsSqlTable" /> specified by the table name.
        /// </summary>
        /// <param name="name">A string value specifying the table name.</param>
        /// <returns></returns>
        public MsSqlTable this[string name]
        {
            get
            {
                name = name.ToUpper().RemoveBrackets();
                return base.List.OfType<MsSqlTable>().Where(s => s.Name == name).FirstOrDefault();
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Adds a <see cref="T:MsSqlTable" /> object to the list.
        /// </summary>
        /// <param name="table">A <see cref="T:MsSqlTable" /> object representing the table.</param>
        /// <returns>Index of the table in collection</returns>
        public int Add(MsSqlTable table)
        {
            int index = base.List.IndexOf(table);
            if(index == -1)
            {
                index = base.List.Add(table);
            }
            return index;
        }
        #endregion
    }
}
