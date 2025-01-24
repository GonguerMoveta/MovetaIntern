using System;
using System.Collections;
using System.Linq;

namespace fecher.Common
{
    /// <summary>
    /// Represents the collection of schemas
    /// </summary>
    public class MsSqlSchemaCollection : CollectionBase
    {
        #region Indexer
        /// <summary>
        /// Provides indexed access to the elements in the collection.
        /// </summary>
        /// <param name="index">An integer value specifying the zero-based index of the required <see cref="T:MsSqlSchema" /> object.</param>
        /// <value>A <see cref="T:MsSqlSchema" /> object representing the item in the collection.</value>
        public MsSqlSchema this[int index]
        {
            get
            {
                return (MsSqlSchema)this[index];
            }
        }

        /// <summary>
        /// Gets the <see cref="T:MsSqlSchema" /> specified by the schema name.
        /// </summary>
        /// <param name="name">A string value specifying the schema name.</param>
        /// <returns></returns>
        public MsSqlSchema this[string name]
        {
            get
            {
                name = name.ToUpper().RemoveBrackets();
                return base.List.OfType<MsSqlSchema>().Where(s => s.Name == name).FirstOrDefault();
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Returns the <see cref="T:MsSqlTable" /> contained in <see cref="T:MsSqlSchema" />
        /// </summary>
        /// <param name="name">A string value specifying the name of the <see cref="T:MsSqlSchema" /> object</param>
        /// <param name="table">A string value specifying the name of the <see cref="T:MsSqlTable" /> object</param>
        /// <returns></returns>
        public MsSqlTable FindTable(string name, string table)
        {
            MsSqlTable msSqlTable = null;
            MsSqlSchema msSqlSchema = this[name];
            if (msSqlSchema != null)
            {
                msSqlTable = msSqlSchema.Tables[table];
                if (msSqlTable == null)
                {
                    msSqlTable = msSqlSchema.Tables.OfType<MsSqlTable>().Where(t => t.Synonym.ToUpper() == table.ToUpper().RemoveBrackets()).FirstOrDefault();
                }                
            }
            return msSqlTable;
        }

        /// <summary>
        /// Returns the <see cref="T:MsSqlView" /> contained in <see cref="T:MsSqlSchema" />
        /// </summary>
        /// <param name="name">A string value specifying the name of the <see cref="T:MsSqlSchema" /> object</param>
        /// <param name="view">A string value specifying the name of the <see cref="T:MsSqlView" /> object</param>
        /// <returns></returns>
        public MsSqlView FindView(string name, string view)
        {
            MsSqlView msSqlView = null;
            MsSqlSchema msSqlSchema = this[name];
            if (msSqlSchema != null)
            {
                msSqlView = msSqlSchema.Views[view];
            }
            return msSqlView;
        }

        /// <summary>
        /// Check if the table exists in provided schema
        /// </summary>
        /// <param name="name">A string value specifying the name of the <see cref="T:MsSqlSchema" /> object</param>
        /// <param name="table">A string value specifying the name of the <see cref="T:MsSqlTable" /> or <see cref="T:MsSqlView" /> object</param>
        /// <returns></returns>
        public bool HasTableOrView(string name, string table)
        {
            return FindTable(name, table) != null || FindView(name, table) != null;
        }

        /// <summary>
        /// Adds a <see cref="T:MsSqlSchema" /> object to the list.
        /// </summary>
        /// <param name="schema">A <see cref="T:MsSqlSchema" /> object representing the table.</param>
        /// <returns>Index of the schema in collection</returns>
        public int Add(MsSqlSchema schema)
        {
            int index = base.List.IndexOf(schema);
            if (index == -1)
            {
                index = base.List.Add(schema);
            }
            return index;
        }
        #endregion
    }
}
