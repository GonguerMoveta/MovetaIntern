using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fecher.Common
{
    /// <summary>
    /// Represents an element in a <see cref="T:MsSqlViewCollection" />
    /// </summary>
    public class MsSqlView
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="T:MsSqlView" /> class.
        /// </summary>
        /// <param name="name">>A <see cref="T:String" /> object representing the name of the table. This value is assigned to the <see cref="P:Name" /> property.</param>
        public MsSqlView(string name)
        {
            this.Name = name.ToUpper();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the name of the view
        /// </summary>
        /// <value>A <see cref="T:String" /> object representing the name of the view.</value>
        public string Name { get; private set; }
        #endregion
    }
}
