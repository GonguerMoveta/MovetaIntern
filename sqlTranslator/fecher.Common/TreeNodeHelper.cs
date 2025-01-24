using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PPJ.Runtime.Sql;

namespace fecher.Common
{
    public static class TreeNodeHelper
    {
        /// <summary>
        /// Returns the part from NodeValue after the latest .
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static string GetUnqualifiedValue(this TreeNode node)
        {
            if (node == null)
            {
                return null;
            }

            return node.NodeValue.RemoveBrackets().GetUnqualifiedValue();
        }

        public static IEnumerable<Column> GetTableColumns(this TreeNode node, DatabaseSettings settings)
        {
            return GetTableColumns(node, settings, false);
        }

        public static IEnumerable<Column> GetTableColumns(this TreeNode node, DatabaseSettings settings, bool skipRowID)
        {
            if (node == null ||
                node.NodeType != Tokens.Table ||
                settings == null ||
                settings.DbStructure == null)
            {
                return null;
            }

            string tableName = node.NodeValue.ToUpper().RemoveBrackets();
            var columns = settings.DbStructure.Where(c => c.Table == tableName && c.TbCreator.ToUpper() == GetSchema(node, settings));
            if (skipRowID)
            {
                columns = columns.Where(c => c.Name.ToUpperInvariant() != "ROWID");
            }
            return columns;
        }

        public static string GetSchema(this TreeNode node, DatabaseSettings settings)
        {
            string schema = Sql.User.ToUpper();
            if (node != null &&
                (node.NodeType == Tokens.Table || node.NodeType == Tokens.View) &&
                settings.ReadStructure)
            {
                var table = node.NodeValue;
                int index = node.NodeValue.IndexOf(".");
                if (index > -1)
                {
                    schema = node.NodeValue.Substring(0, index);
                }
                if (!MsSqlSchema.IsBuildIn(schema) &&
                    !settings.Schemas.HasTableOrView(schema, node.GetUnqualifiedValue()))
                {
                    schema = "dbo";
                }
            }

            return schema.ToUpper();
        }

        /// <summary>
        /// Qualify table name with current schema if the current schema is not SYSADM
        /// </summary>
        /// <param name="node"></param>
        public static void QualifyTableName(this TreeNode node)
        {
            if (Sql.User.ToUpper() != "SYSADM")
            {
                QualifyTableName(node, Sql.User);
            }
        }

        /// <summary>
        /// Qualify table with current schema, if schema contains table.
        /// Otherwise, use the default one eg. dbo.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="settings"></param>
        public static void QualifyTableName(this TreeNode node, DatabaseSettings settings)
        {
            if (node != null &&
                (node.NodeType == Tokens.Table || node.NodeType == Tokens.View) &&
                settings.ReadStructure)
            {
                string schema = GetSchema(node, settings);

                QualifyTableName(node, schema);
            }
        }

        /// <summary>
        /// Qualify table with provided schema
        /// </summary>
        /// <param name="node"></param>
        /// <param name="schema"></param>
        public static void QualifyTableName(this TreeNode node, string schema)
        {
            if (node != null &&
                (node.NodeType == Tokens.Table || node.NodeType == Tokens.View) &&
                !String.IsNullOrEmpty(schema))
            {
                int index = node.NodeValue.IndexOf(".");
                if (index == -1)
                {
                    node.NodeValue = String.Format("{0}.{1}", schema, node.NodeValue);
                }
                else
                {
                    node.NodeValue = String.Format("{0}.{1}", schema, node.NodeValue.Substring(index + 1));
                }
            }
        }
    }
}
