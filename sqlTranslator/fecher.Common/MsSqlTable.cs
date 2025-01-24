using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fecher.Common
{
    /// <summary>
    /// Represents an element in a <see cref="T:MsSqlTableCollection" />
    /// </summary>
    public class MsSqlTable
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="T:MsSqlTable" /> class.
        /// </summary>
        /// <param name="name">>A <see cref="T:String" /> object representing the name of the table. This value is assigned to the <see cref="P:Name" /> property.</param>
        public MsSqlTable(string name)
        {
            this.Name = name.ToUpper();
        }

        public MsSqlTable(string name, string synonym)
        {
            this.Name = name.ToUpper();
            this.Synonym = synonym.ToUpper();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the name of the table
        /// </summary>
        /// <value>A <see cref="T:String" /> object representing the name of the table.</value>
        public string Name { get; private set; }
        public string Synonym { get; private set; }
        #endregion
    }
}
