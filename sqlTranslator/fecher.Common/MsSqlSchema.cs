using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fecher.Common
{
    /// <summary>
    /// Represents an element in a <see cref="T:MsSqlSchemaCollection" />
    /// </summary>
    public class MsSqlSchema
    {
        #region Constants
        private static List<string> BUILTINSCHEMAS = new List<string>() { "SYS" };
        #endregion

        #region Members
        private MsSqlTableCollection tables = null;
        private MsSqlViewCollection views = null;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="T:MsSqlSchema" /> class.
        /// </summary>
        /// <param name="name">>A <see cref="T:String" /> object representing the name of the schema. This value is assigned to the <see cref="P:Name" /> property.</param>
        public MsSqlSchema(string name)
        {
            this.Name = name.ToUpper();
        }
        #endregion

        #region Methods
        public static bool IsBuildIn(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                return false;
            }

            return BUILTINSCHEMAS.Contains(name.ToUpper());
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the name of the schema
        /// </summary>
        /// <value>A <see cref="T:String" /> object representing the name of the schema.</value>
        public string Name { get; set; }

        /// <summary>
        /// Provides access to the collection of tables available for schema.
        /// </summary>
        public MsSqlTableCollection Tables
        {
            get
            {
                if (tables == null)
                {
                    tables = new MsSqlTableCollection();
                }

                return tables;
            }
        }

        /// <summary>
        /// Provides access to the collection of views available for schema.
        /// </summary>
        public MsSqlViewCollection Views
        {
            get
            {
                if (views == null)
                {
                    views = new MsSqlViewCollection();
                }

                return views;
            }
        }
        #endregion
    }
}
