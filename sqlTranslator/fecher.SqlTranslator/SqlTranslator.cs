using System;
using System.Collections;
using fecher.Common;
using System.Text.RegularExpressions;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace fecher.SqlTranslator
{
    /// <summary>
    /// Class that contains the TranslateSql function, which groups all the other functions and does the actual translation
    /// </summary>
    public class SqlTranslator
    {
        private string owner = String.Empty;  //represents the owner of a table; mostly used in CREATE TABLE command

        // The alreadyTranslated string is added at the beginning of the already translated statement
        // If this is detected, the tag will be removed and the statement will be returned without any other processing
        private string alreadyTranslated = "@NOTRANS@";

        private DatabaseBrand dbBrand; //used to differentiate between SqlBase versions
        private Dictionary<string, ColumnInformation> columnInfo = new Dictionary<string, ColumnInformation>();
        private Dictionary<string, bool> tableInfoExtCols = new Dictionary<string, bool>();
        private Dictionary<string, bool> tableInfoExtBinCols = new Dictionary<string, bool>();
        private NameValueCollection aliases = new NameValueCollection();
        private Dictionary<string, int> aliasCount = new Dictionary<string, int>();
        private DatabaseSettings Settings;
        private List<ExternalColumn> externalCols;
        private StringBuilder rawPositions;
        private StringBuilder longCount;
        private int countLong = 0;
        private int countLongRaw = 0;
        private int countVarchar2 = 0;
        private int countRaw = 0;
        private static Dictionary<string, string> statementCache = new Dictionary<string, string>();
        private static List<string> DATEADDCONSTANTS = new List<string>() { "YEAR", "YEARS", "MONTH", "MONTHS", "DAY", "DAYS", "HOUR", "MINUTE", "MINUTES", "SECOND", "SECONDS", "MICROSECOND", "MICROSECONDS" };

        /// <summary>
        /// Translates a SQL statement from SqlBase to SqlServer
        /// </summary>
        /// <param name="sourceSql"></param>
        /// <returns></returns>
        public string TranslateSql(string sourceSql)
        {
            return TranslateSql(DatabaseBrand.SqlServer, sourceSql, DatabaseSettings.Default);
        }

        /// <summary>
        /// Translates a SQL statement from SqlBase to targetDB
        /// </summary>
        /// <param name="targetDB"></param>
        /// <param name="sourceSql"></param>
        /// <returns></returns>
        public string TranslateSql(DatabaseBrand targetDB, string sourceSql)
        {
            return TranslateSql(targetDB, sourceSql, DatabaseSettings.Default);
        }

        /// <summary>
        /// Translates a SQL statement from SqlBase to targetDB using the specified database settings
        /// </summary>
        /// <param name="targetDB"></param>
        /// <param name="sourceSql"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public string TranslateSql(DatabaseBrand targetDB, string sourceSql, DatabaseSettings settings)
        {
            return TranslateSql(settings.SqlBaseVersion, targetDB, sourceSql, settings);
        }

        /// <summary>
        /// Translates a SQL statement from sourceDB to targetDB using the specified database settings
        /// </summary>
        /// <param name="sourceDB"></param>
        /// <param name="targetDB"></param>
        /// <param name="sourceSql"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public string TranslateSql(DatabaseBrand sourceDB, DatabaseBrand targetDB, string sourceSql, DatabaseSettings settings)
        {
            if (String.IsNullOrEmpty(sourceSql))
            {
                return settings.ReplaceEmptyStatements ? "exec GETNORESULTS" : String.Empty;
            }

            // If string is already translated, remove already translated tag and return the string
            if (sourceSql.ToLower().StartsWith(alreadyTranslated.ToLower()))
            {
                string translated = sourceSql.Remove(0, alreadyTranslated.Length).Trim();

                return translated;
            }

            Settings = settings;

            //check the database brand
            //if the source brand is the same as the target brand simply return the statement
            if (sourceDB == targetDB)
            {
                return sourceSql;
            }
            else if (sourceDB == DatabaseBrand.SqlServer || sourceDB == DatabaseBrand.Oracle)
            {
                throw new NotSupportedException("Only SqlBase and Informix are supported as source databases");
            }

            //Don't translate stored procedures calls
            if (sourceSql.ToLower().StartsWith("exec"))
            {
                return sourceSql;
            }

            ISyntacticAnalyzer parser = null;
            if (sourceDB == DatabaseBrand.SqlBase || sourceDB == DatabaseBrand.SqlBaseOld)
            {
                parser = new SyntacticAnalyzerSqlBase();
            }
            Tree parseTree = null;
            try
            {
                parseTree = parser.Parse(sourceSql);
            }
            catch (Exception ex)
            {
                if (Settings.OnParseError == ParseErrorAction.ReturnSource)
                {
                    throw new Exception(sourceSql);
                    //return sourceSql;
                }
                else if (Settings.OnParseError == ParseErrorAction.ThrowException)
                {
                    throw ex;
                }

            }

            if (Settings != null && Settings.OnParseError != ParseErrorAction.Ignore)
            {
                if (sourceSql.Replace("\r", "").Replace("\n", "").Replace(" ", "").Replace("\t", "").ToLower() !=
                    parseTree.root.ToString().Replace("\r", "").Replace("\n", "").Replace(" ", "").Replace("\t", "").ToLower())
                {
                    //The parser will add commas between into bindVariable if they are missing in the original string
                    //get the differences between the two strings and check if only commas are the differences
                    //RHE: TODO
                    //if (!AllDiffAreCommas(StrDifferences(sourceSql.Replace("\r", "").Replace("\n", "").Replace(" ", "").Replace("\t", "").ToLower(),
                    //    parseTree.root.ToString().Replace("\r", "").Replace("\n", "").Replace(" ", "").Replace("\t", "").ToLower())))
                    //{
                    //    if (Settings.OnParseError == ParseErrorAction.ReturnSource)
                    //    {
                    //        return sourceSql;
                    //    }
                    //    else
                    //    {
                    //        throw new Exception("Parse error for statement:\r\n" + sourceSql);
                    //    }
                    //}
                }
            }

            if (Settings != null && Settings.TableOwner != "")
            {
                owner = Settings.TableOwner;
            }

            string result = "";
            result = __TranslateSql(sourceDB, targetDB, parseTree, sourceSql);
            //return __TranslateSql(sourceDB, targetDB, parseTree, sourceSql);
            return result;
        }

        public string TranslateSql(DatabaseBrand targetDB, string sourceSql, DatabaseSettings settings, out string rawBindPositions)
        {
            externalCols = new List<ExternalColumn>();
            rawPositions = new StringBuilder();
            string result = TranslateSql(targetDB, sourceSql, settings);
            rawBindPositions = rawPositions.ToString();
            return result;
        }

        public string TranslateSql(DatabaseBrand targetDB, string sourceSql, DatabaseSettings settings, out string rawBindPositions, out int countLong,
            out int countLongRaw, out int countVarchar2, out int countRaw, out string countPositions)
        {
            externalCols = new List<ExternalColumn>();
            rawPositions = new StringBuilder();
            longCount = new StringBuilder();
            string result = TranslateSql(targetDB, sourceSql, settings);
            rawBindPositions = rawPositions.ToString();
            if (longCount.Length != 0)
            {
                longCount.Insert(0, DatabaseSettings.Default.Separator);
            }
            longCount.Insert(0, this.countLong + "," + this.countLongRaw + "," + this.countVarchar2 + "," + this.countRaw);
            countPositions = longCount.ToString();
            countLong = this.countLong;
            countLongRaw = this.countLongRaw;
            countVarchar2 = this.countVarchar2;
            countRaw = this.countRaw;
            return result;
        }

        private List<string> StrDifferences(string original, string compare)
        {
            List<string> diff = new List<string>();

            string keyw = "LOCKMODEROW";
            int origLength = original.Length;
            if (origLength - compare.Length >= keyw.Length && original.Substring(origLength - keyw.Length, keyw.Length).ToUpper() == keyw)
            {
                origLength = origLength - keyw.Length;
            }

            for (int count = 0; count < origLength; count++)
            {
                if (count >= compare.Length)
                {
                    diff.Add(compare[count].ToString());
                }
                else if (original[count] != compare[count + diff.Count])
                {
                    diff.Add(compare[count + diff.Count].ToString());
                }
            }
            return diff;
        }

        private bool AllDiffAreCommas(List<string> diff)
        {
            foreach (string str in diff)
            {
                if (str != "," && str != "\"")
                {
                    return false;
                }
            }

            return true;
        }

        private TreeNode GetTable(TreeNode columnNode, TreeNodeCollection tables)
        {
            if (columnNode == null)
            {
                return null;
            }

            if (columnNode.NodeType == Tokens.BindVariable ||
                columnNode.NodeType == Tokens.NumericConst ||
                columnNode.NodeType == Tokens.StringConst ||
                columnNode.NodeType == Tokens.DatetimeConst)
            {
                return null;
            }

            string tableName;
            string columnName = columnNode.NodeValue.ToUpper();
            columnName = columnName.Replace("TO_CHAR", "");
            columnName = columnName.Replace("TO_DATE", "");
            columnName = columnName.Replace("TO_NUMBER", "");
            columnName = columnName.Replace("(", "");
            columnName = columnName.Replace(")", "");
            if (columnName.Contains(","))
            {
                columnName = columnName.Split(',')[0];
            }

            //remove the table owner
            if (columnName.IndexOf('.') != columnName.LastIndexOf('.'))
            {
                columnName = columnName.Remove(0, columnName.IndexOf('.') + 1);
            }

            if (columnName.IndexOf('.') != -1)
            {
                tableName = columnName.Substring(0, columnName.LastIndexOf('.'));
            }
            else if (Settings != null && Settings.DbStructure != null)
            {
                tableName = Settings.DbStructure[columnName.ToLower().RemoveBrackets()];
            }
            else
            {
                throw new NotSupportedException("Joins without specifying the table names are not supported");
            }

            string[] tableList = tableName.Split(',');

            foreach (string table in tableList)
            {
                foreach (TreeNode tableNode in tables)
                {
                    if (table == tableNode.NodeInfo || table.ToUpper() == tableNode.GetUnqualifiedValue().ToUpper().RemoveBrackets() ||
                        (tableNode.Children.Count > 0 && table.ToUpper() == tableNode.Children[0].NodeValue.ToUpper())) //check the alias
                    {
                        return tableNode;
                    }

                    //RHE: check table synonym
                    if (Settings != null && Settings.Schemas.Count > 0)
                    {
                        var tableInfo = Settings.Schemas.FindTable(tableNode.GetSchema(Settings), table);
                        var synonym = tableInfo != null ? tableInfo.Synonym : String.Empty;
                        if (!String.IsNullOrEmpty(synonym))
                        {
                            if (synonym == tableNode.NodeInfo || synonym.ToUpper() == tableNode.GetUnqualifiedValue().ToUpper().RemoveBrackets() ||
                                (tableNode.Children.Count > 0 && synonym.ToUpper() == tableNode.Children[0].NodeValue.ToUpper()))
                            {
                                return tableNode;
                            }
                        }

                    }
                    
                }
            }

            return null;
        }

        private string GetTableName(string table)
        {
            string[] tableName = table.Split('.');
            if (tableName.Length > 0)
            {
                table = tableName[tableName.Length - 1];
            }
            return table;
        }

        private string GetTableNameFromColumn(string table)
        {
            string[] tableName = table.Split('.');
            if (tableName.Length > 0)
            {
                return tableName[0];
            }
            return "";
        }

        private string GetColumnName(string column)
        {
            string[] columnName = column.Split('.');
            if (columnName.Length > 0)
            {
                column = columnName[columnName.Length - 1];
            }
            return column;
        }

        private string __TranslateSql(DatabaseBrand sourceDB, DatabaseBrand targetDB, Tree sourceTree, string sourceSql)
        {
            //PPJ:FINAL:PJ: Performance - Add StatementCache
            string stmt = string.Empty;
            statementCache.TryGetValue(sourceSql, out stmt);

            if (!string.IsNullOrEmpty(stmt))
            {
                return stmt;
            }


            //Copy structure of sourceTree into the targetTree
            Tree targetTree = sourceTree;
            dbBrand = sourceDB;

            try
            {
                //Make the transformations
                if (sourceDB <= DatabaseBrand.SqlBase && targetDB == DatabaseBrand.SqlServer)
                {
                    TransformTreeSqlBase_MsSql(ref targetTree, targetTree.root, targetTree.root);
                }
                else if (sourceDB <= DatabaseBrand.SqlBase && targetDB == DatabaseBrand.Oracle)
                {
                    InitCurrentColsInfo(targetTree.root, targetTree);
                    TransformTreeSqlBase_Oracle(ref targetTree, targetTree.root, targetTree.root);
                    if (!String.IsNullOrEmpty(Settings.BindVar))
                    {
                        TreeNode mainRootNode = targetTree.root;
                        TreeNode intoNode = new TreeNode(Tokens.BindVariable, ":" + Settings.BindVar, "SKIP");

                        if (!mainRootNode.ToString().StartsWith("SELECT COUNT ( * ) INTO " + intoNode))
                        {
                            foreach (TreeNode selectStatement in targetTree.root.Children)
                            {
                                TreeNode intosNode = targetTree.FindNode(selectStatement, "INTO");

                                if (intosNode != null)
                                {
                                    intosNode.NodeInfo = "NOT USED";

                                    foreach (TreeNode n in intosNode.Children)
                                    {
                                        n.NodeInfo = "NOT USED";
                                    }
                                }
                            }

                            mainRootNode.Children.Insert(new TreeNode(Tokens.Select, "SELECT", "SKIP"), 0);
                            mainRootNode.Children.Insert(new TreeNode(Tokens.Function, "COUNT", "SKIP"), 1);
                            mainRootNode.Children.Insert(new TreeNode(Tokens.LeftParant, "(", "SKIP"), 2);
                            mainRootNode.Children.Insert(new TreeNode(Tokens.Asterisk, "*", "SKIP"), 3);
                            mainRootNode.Children.Insert(new TreeNode(Tokens.RightParant, ")", "SKIP"), 4);
                            mainRootNode.Children.Insert(new TreeNode(Tokens.Keyword, "INTO", "SKIP"), 5);
                            mainRootNode.Children.Insert(intoNode, 6);
                            mainRootNode.Children.Insert(new TreeNode(Tokens.Keyword, "FROM", "SKIP"), 7);
                            mainRootNode.Children.Insert(new TreeNode(Tokens.LeftParant, "(", "SKIP"), 8);
                            mainRootNode.Children.Add(new TreeNode(Tokens.RightParant, ")", "SKIP"));
                        }
                    }
                }
                else if (sourceDB == DatabaseBrand.Informix && targetDB == DatabaseBrand.SqlServer)
                {
                    int dummy = 0;
                    TransformTreeInformix_MsSql(ref targetTree, targetTree.root, targetTree.root, ref dummy);
                    if (targetTree.root.NodeInfo == "CREATE+SEQUENCE")
                    {
                        string tableName = targetTree.root.Children[0].Children[0].Children[0].NodeValue;
                        TreeNode node = targetTree.root.InsertChild(new TreeNode(Tokens.Create), 0);
                        node.AddChild(new TreeNode(Tokens.Keyword, "IF OBJECT_ID('Sequence_" + tableName + "', 'SO') IS NULL CREATE SEQUENCE"));
                        node.AddChild(new TreeNode(Tokens.StringConst, "Sequence_" + tableName + " AS[int] START WITH 1 INCREMENT BY 1;"));
                    }
                }
            }
            catch (Exception ex)
            {
                if (Settings.OnParseError == ParseErrorAction.ReturnSource)
                {
                    return sourceSql;
                }
                else if (Settings.OnParseError == ParseErrorAction.ThrowException)
                {
                    throw ex;
                }
            }

            //PPJ:FINAL:PJ: Performance - Cache
            stmt = Tree.Traverse(targetTree.root);

            if (statementCache.Count > 50)
            {
                statementCache.Clear();
            }
            statementCache.Add(sourceSql, stmt);
            //Construct the target string
            return stmt;
        }


        private void TransformTreeSqlBase_MsSql(ref Tree targetTree, TreeNode root, TreeNode node)
        {
            if (node.NodeType == Tokens.Select)
                root = node;

            if (node.NodeInfo != "SKIP")
            {

                //Transformations for current node
                switch (node.NodeType)
                {
                    case Tokens.Insert:
                        if (node.Children.Count > 0 
                            && node.Children[0].Children.Count > 0)
                        {
                            var tableNode = node.Children[0].Children[node.Children[0].Children.Count - 1];
                            if (tableNode.NodeType == Tokens.Table &&
                                Settings != null && 
                                Settings.DbStructure != null)
                            {
                                var columns = tableNode.GetTableColumns(Settings, true);
                                if (columns != null && columns.Count() > 0)
                                {
                                    var expression = new TreeNode(Tokens.Expression);
                                    expression.AddChild(new TreeNode(Tokens.LeftParant, "("));

                                    bool firstColumn = true;
                                    foreach (var column in columns)
                                    {
                                        if (!firstColumn)
                                        {
                                            expression.AddChild(new TreeNode(Tokens.Comma, ","));
                                        }
                                        else
                                        {
                                            firstColumn = false;
                                        }

                                        expression.AddChild(new TreeNode(Tokens.Column, column.Name));
                                    }

                                    expression.AddChild(new TreeNode(Tokens.RightParant, ")"));

                                    tableNode.Parent.InsertChild(expression, tableNode.Index + 1);
                                }
                            }
                        }
                        break;

                    case Tokens.Lock:
                        TreeNode parent = node.Parent;
                        parent.RemoveChild(node);
                        parent.AddChild(new TreeNode(Tokens.Select, "SELECT"));
                        parent.Children[0].AddChild(new TreeNode(Tokens.Asterisk, "* FROM"));
                        parent.Children[0].AddChild(new TreeNode(Tokens.Table, node.Children[0].NodeValue));
                        parent.Children[0].AddChild(new TreeNode(Tokens.Expression, "WITH (TABLOCKX)"));
                        break;
                    case Tokens.Index:
                        {
                            if (node.NodeValue.Contains("."))
                            {
                                node.NodeValue = node.NodeValue.Substring(node.NodeValue.IndexOf(".") + 1);
                            }
                            break;
                        }
                    //FIX 2006.07.14
                    case Tokens.View:
                    case Tokens.Table: //Replace table owner (if exists)
                        {
                            //if it`s not a CREATE or DROP TABLE statement, qualify table name
                            if (!node.Parent.ToString().StartsWith("CREATE TABLE") && 
                                !node.Parent.ToString().StartsWith("DROP TABLE") &&
                                !node.Parent.ToString().StartsWith("CREATE VIEW") &&
                                !node.Parent.ToString().StartsWith("DROP VIEW"))
                            {
                                node.QualifyTableName(Settings);
                            }

                            //add the alias to the collection
                            //this is needed for subselects that use the alias from the main statement (UPDATE, DELETE, SELECT)
                            if (node.Children.Count > 0)
                            {
                                aliases.Add(node.Children[0].NodeValue, node.NodeValue);
                            }

                            //replace SYSCOLUMNS with SYSCOLUMNSVIEW
                            if (node.NodeValue.ToUpper().EndsWith("SYSCOLUMNS"))
                            {
                                node.NodeValue = node.NodeValue.ToUpper().Replace("SYSCOLUMNS", "SYSCOLUMNSVIEW");
                            }
                            else if (node.NodeValue.ToUpper().EndsWith("SYSINDEXES"))
                            {
                                node.NodeValue = node.NodeValue.ToUpper().Replace("SYSINDEXES", "SYSINDEXES");
                            }

                            node.NodeValue = GetNameWithCase(node.NodeValue);

                            //surround the table name with [] if it's a reserved word
                            //PPJ:TODO:PJ: if (!node.NodeValue.Contains("[")) -> Collation nicht berücksichtigen SQL_Latin1_General
                            //PPJ:TODO:PJ:DEL if (!node.NodeValue.Contains("[") && !node.NodeValue.ToLower().Contains("collate") && !node.NodeValue.ToLower().Contains("sql_latin1_general"))
                            if (!node.NodeValue.Contains("[") && !node.NodeValue.ToLower().Contains("collate") && !node.NodeValue.ToLower().Contains("sql_latin1_general"))
                            //if (!node.NodeValue.Contains("[")) 
                            {
                                node.NodeValue = Regex.Replace(node.NodeValue, @"^(?<owner>(.*\.))?(?<table>(.*))$", "${owner}[${table}]");
                            }
                            break;
                        }

                    case Tokens.Function: //Replace function name with the corresponding function from the functions table
                        {
                            #region Functions
                            switch (node.NodeValue.ToUpper())
                            {
                                //Functions @CHOOSE and @DECODE uses a variant number of parameters so 
                                //the best thing to do is to implement them here using the CASE function

                                case "@MEDIAN":
                                    {
                                        throw new NotSupportedException("Function @MEDIAN is not supported by SqlTranslator");
                                    }

                                //@CHOOSE (selector number, value 0, value 1, ..., value n)
                                case "@CHOOSE":
                                    {
                                        //Replace function name with "CASE"
                                        node.NodeValue = "CASE";

                                        node.RemoveChild(0); //Remove left paranthesis
                                        TreeNode selectorNode = node.Children[0];
                                        node.RemoveChild(0);
                                        int j = 0;
                                        int selectorIndex = 0;
                                        string relatOp = String.Empty;
                                        int valuesCount = node.Children.Count / 2;

                                        TreeNode currentNode = node.Children[j];
                                        while (currentNode.NodeType == Tokens.Comma)
                                        {
                                            //Remove comma
                                            node.RemoveChild(currentNode);

                                            //Extract value0, value1,...
                                            currentNode = node.Children[j]; //expression

                                            //Create the WHEN argument
                                            currentNode.InsertChild(new TreeNode(Tokens.Keyword, "WHEN"), 0);
                                            currentNode.InsertChild(selectorNode, 1);
                                            if (selectorIndex == 0)
                                            {
                                                relatOp = "<=";
                                            }
                                            else if (selectorIndex == valuesCount - 1)
                                            {
                                                relatOp = ">=";
                                            }
                                            else
                                            {
                                                relatOp = "=";
                                            }
                                            currentNode.InsertChild(new TreeNode(Tokens.RelatOperator, relatOp), 2);
                                            currentNode.InsertChild(new TreeNode(Tokens.NumericConst, selectorIndex.ToString()), 3);
                                            currentNode.InsertChild(new TreeNode(Tokens.Keyword, "THEN"), 4);

                                            j++;
                                            selectorIndex++;
                                            currentNode = node.Children[j];
                                        }

                                        node.RemoveChild(j); //Remove right paranthesis

                                        //Add "END" keyword
                                        node.AddChild(new TreeNode(Tokens.Keyword, "END"));
                                        break;
                                    }

                                //@DECODE(expr, search1, return1, search2, return2, ..., [default])
                                case "@DECODE":
                                    {
                                        //@MU
                                        TreeNode conditionNode = node.Children[1];
                                        bool reverseWHEN = false;
                                        if (node.ToString().Contains("NULL"))
                                        {
                                            reverseWHEN = true;
                                        }
                                        //END @MU

                                        //Replace function name with "CASE"
                                        node.NodeValue = "CASE";

                                        node.RemoveChild(0); //Remove left paranthesis
                                        TreeNode valueNode;
                                        TreeNode returnNode;
                                        TreeNode exprNode;

                                        TreeNode currentNode = node.Children[1];
                                        while (currentNode.NodeType == Tokens.Comma)
                                        {
                                            //Remove comma
                                            node.RemoveChild(currentNode);

                                            //Extract search expression
                                            valueNode = node.Children[1]; //expression
                                            node.RemoveChild(valueNode);


                                            //Extract return expression
                                            if (((TreeNode)node.Children[1]).NodeType == Tokens.Comma)
                                            {
                                                node.RemoveChild(node.Children[1]); //Remove comma
                                                returnNode = node.Children[1];
                                                node.RemoveChild(returnNode);

                                                //Create the WHEN expression
                                                exprNode = new TreeNode(Tokens.Expression);
                                                exprNode.AddChild(new TreeNode(Tokens.Keyword, "WHEN"));
                                                //@MU 
                                                if (reverseWHEN)
                                                {
                                                    exprNode.AddChild(conditionNode);
                                                    if (valueNode.ToString().Contains("NULL"))
                                                    {
                                                        exprNode.AddChild(new TreeNode(Tokens.Keyword, "IS"));
                                                    }
                                                    else
                                                    {
                                                        exprNode.AddChild(new TreeNode(Tokens.Keyword, "="));
                                                    }
                                                }
                                                //@MU END 
                                                exprNode.AddChild(valueNode);
                                                exprNode.AddChild(new TreeNode(Tokens.Keyword, "THEN"));
                                                exprNode.AddChild(returnNode);
                                            }

                                            //Create default
                                            else
                                            {
                                                node.RemoveChild(node.Children[1]); //Remove right paranthesis
                                                exprNode = new TreeNode(Tokens.Expression);
                                                exprNode.AddChild(new TreeNode(Tokens.Keyword, "ELSE"));
                                                exprNode.AddChild(valueNode);
                                            }

                                            //Add expression to the function node
                                            node.AddChild(exprNode);

                                            currentNode = node.Children[1];
                                        }


                                        if (currentNode.NodeType == Tokens.RightParant)
                                            node.RemoveChild(1); //Remove right paranthesis

                                        //Add "END" keyword
                                        node.AddChild(new TreeNode(Tokens.Keyword, "END"));
                                        //@MU 
                                        if (reverseWHEN)
                                        {
                                            node.RemoveChild(0);
                                        }
                                        //END @MU
                                        break;
                                    }


                                //@IF(number, value1, value2)
                                case "@IF":
                                    {
                                        //CASE number WHEN 0 THEN value2  ELSE value1 END
                                        //OR
                                        //CASE WHEN number THEN value1 ELSE value2 END  (when number returns a boolean value, in case of @ISNA)
                                        //Replace function name with "CASE"
                                        node.NodeValue = "CASE";
                                        node.NodeType = Tokens.Expression;
                                        node.RemoveChild(0); //Remove left paranthesis

                                        TreeNode conditionNode = node.Children[0];
                                        TreeNode valueNode1 = node.Children[2];
                                        TreeNode valueNode2 = node.Children[4];

                                        //Remove unnecessary nodes
                                        for (int i = 1; i <= 5; i++)
                                            node.RemoveChild(1);

                                        //#113: This code is only needed if the condition contains only @ISNA
                                        if (conditionNode.Children.Count == 1 && conditionNode.ToString().Contains("@ISNA"))
                                        {
                                            conditionNode.Children[0].NodeInfo = "CASE WHEN";
                                            node.RemoveChild(0);

                                            //Create WHEN number THEN value1
                                            TreeNode whenNode = new TreeNode(Tokens.Keyword, "WHEN");
                                            whenNode.AddChild(conditionNode);
                                            whenNode.AddChild(new TreeNode(Tokens.Keyword, "THEN"));
                                            whenNode.AddChild(valueNode1);

                                            node.AddChild(whenNode);

                                            //Create ELSE value2
                                            TreeNode elseNode = new TreeNode(Tokens.Keyword, "ELSE");
                                            elseNode.AddChild(valueNode2);

                                            node.AddChild(elseNode);
                                        }
                                        else
                                        {

                                            //Create WHEN 0 THEN value2
                                            TreeNode whenNode = new TreeNode(Tokens.Keyword, "WHEN");
                                            //@MU
                                            node.RemoveChild(0);
                                            whenNode.AddChild(conditionNode);
                                            //whenNode.AddChild(new TreeNode(Tokens.Keyword, "IS NULL"));
                                            //whenNode.AddChild(new TreeNode(Tokens.Keyword, "= 0"));
                                            //whenNode.AddChild(new TreeNode(Tokens.Keyword, "OR"));
                                            //whenNode.AddChild(conditionNode);
                                            whenNode.AddChild(new TreeNode(Tokens.Keyword, "IS NULL"));
                                            int numValueIndex = targetTree.FindAll(valueNode1, Tokens.NumericConst);
                                            numValueIndex += targetTree.FindAll(valueNode2, Tokens.NumericConst);
                                            if (valueNode1.Children[0].NodeType == Tokens.NumericConst
                                                || valueNode2.Children[0].NodeType == Tokens.NumericConst || numValueIndex > 0)
                                            {
                                                whenNode.AddChild(new TreeNode(Tokens.Keyword, "OR"));
                                                whenNode.AddChild(conditionNode);
                                                whenNode.AddChild(new TreeNode(Tokens.Keyword, "="));
                                                whenNode.AddChild(new TreeNode(Tokens.NumericConst, "0"));
                                            }

                                            if (Settings != null && Settings.DbStructure != null)
                                            {
                                                TreeNode columnNode = targetTree.FindNode(conditionNode, Tokens.Column);
                                                TreeNodeCollection tables = new TreeNodeCollection();
                                                targetTree.FindAll(root, Tokens.Table, ref tables);
                                                TreeNode table = GetTable(columnNode, tables);
                                                if (table != null)
                                                {
                                                    //string datatype = Settings.DbStructure[columnNode.GetUnqualifiedValue(), table.NodeValue];                                                   
                                                    //FC:RHE: Check if the datatype is numeric
                                                    string datatype = Settings.DbStructure[columnNode.GetUnqualifiedValue(), Settings.Schemas.FindTable(table.GetSchema(Settings), table.OriginalNodeValue)?.Name];
                                                    if (datatype == "int" || datatype == "bigint" || datatype == "smallint" || datatype == "decimal" || datatype == "float" || datatype == "money")
                                                    {
                                                        whenNode.AddChild(new TreeNode(Tokens.Keyword, "OR"));
                                                        whenNode.AddChild(conditionNode);
                                                        whenNode.AddChild(new TreeNode(Tokens.Keyword, "="));
                                                        whenNode.AddChild(new TreeNode(Tokens.NumericConst, "0"));
                                                    }
                                                    else
                                                    {
                                                        whenNode.AddChild(new TreeNode(Tokens.Keyword, "OR"));
                                                        whenNode.AddChild(conditionNode);
                                                        whenNode.AddChild(new TreeNode(Tokens.Keyword, "="));
                                                        whenNode.AddChild(new TreeNode(Tokens.StringConst, "''"));
                                                    }
                                                }
                                                else
                                                {
                                                    whenNode.AddChild(new TreeNode(Tokens.Keyword, "OR"));
                                                    whenNode.AddChild(conditionNode);
                                                    whenNode.AddChild(new TreeNode(Tokens.Keyword, "="));
                                                    whenNode.AddChild(new TreeNode(Tokens.NumericConst, "0"));
                                                }
                                            }
                                            whenNode.AddChild(new TreeNode(Tokens.Keyword, "THEN"));
                                            whenNode.AddChild(valueNode2);

                                            node.AddChild(whenNode);

                                            //Create ELSE value1
                                            TreeNode elseNode = new TreeNode(Tokens.Keyword, "ELSE");
                                            elseNode.AddChild(valueNode1);

                                            node.AddChild(elseNode);
                                        }

                                        node.AddChild(new TreeNode(Tokens.Keyword, "END"));
                                        break;
                                    }


                                //@ISNA(argument)
                                case "@ISNA":
                                    {
                                        //Find parent of function (expression)
                                        TreeNode parentNode = targetTree.FindParent(root, node);
                                        TreeNode exprNode = targetTree.FindNode(node, Tokens.Expression);

                                        TreeNode selectNode = targetTree.FindNode(root, "SELECT");
                                        //if (targetTree.FindNode(selectNode, node))
                                        if (node.NodeInfo != "CASE WHEN")
                                        {
                                            //if @ISNA is in the select list then we have to translate it using CASE
                                            //CASE WHEN argument IS NULL OR agument = '' THEN 1 ELSE 0 END
                                            TreeNode newNode = new TreeNode(Tokens.Expression);
                                            //newNode.NodeValue = String.Format("CASE WHEN {0} IS NULL OR {0} = '' THEN 1 ELSE 0 END", exprNode.ToString());

                                            //Create new expression
                                            newNode.AddChild(new TreeNode(Tokens.Keyword, "CASE"));
                                            newNode.AddChild(new TreeNode(Tokens.Keyword, "WHEN"));
                                            newNode.AddChild(exprNode);
                                            newNode.AddChild(new TreeNode(Tokens.Keyword, "IS"));
                                            newNode.AddChild(new TreeNode(Tokens.SysKeyword, "NULL"));
                                            newNode.AddChild(new TreeNode(Tokens.Keyword, "OR"));
                                            newNode.AddChild(exprNode);
                                            newNode.AddChild(new TreeNode(Tokens.RelatOperator, "="));
                                            newNode.AddChild(new TreeNode(Tokens.StringConst, "''"));
                                            newNode.AddChild(new TreeNode(Tokens.Keyword, "THEN"));
                                            newNode.AddChild(new TreeNode(Tokens.NumericConst, "1"));
                                            newNode.AddChild(new TreeNode(Tokens.Keyword, "ELSE"));
                                            newNode.AddChild(new TreeNode(Tokens.NumericConst, "0"));
                                            newNode.AddChild(new TreeNode(Tokens.Keyword, "END"));

                                            parentNode.Children[parentNode.Children.IndexOf(node)] = newNode;
                                        }
                                        else
                                        {
                                            //argument IS NULL OR argument = ''                                       
                                            //Remove function from parent
                                            parentNode.RemoveChild(node);
                                            //Create new expression
                                            parentNode.AddChild(exprNode);
                                            parentNode.AddChild(new TreeNode(Tokens.Keyword, "IS"));
                                            parentNode.AddChild(new TreeNode(Tokens.SysKeyword, "NULL"));
                                            parentNode.AddChild(new TreeNode(Tokens.Keyword, "OR"));
                                            parentNode.AddChild(exprNode);
                                            parentNode.AddChild(new TreeNode(Tokens.RelatOperator, "="));
                                            parentNode.AddChild(new TreeNode(Tokens.StringConst, "''"));
                                        }
                                        break;

                                    }


                                //These functions use the index of the string
                                //In SqlBase the index starts from 0; in MSSql the index starts from 1
                                case "@FIND":
                                    {
                                        node.NodeValue = FunctionTranslator.TranslateFunction(DatabaseBrand.SqlServer, node.NodeValue);
                                        //Find the index argument
                                        if (node.Children.Count > 5)
                                        {
                                            TreeNode indexNode = node.Children[5]; //this is the 2nd parameter
                                            indexNode.AddChild(new TreeNode(Tokens.Expression, "+ 1"));
                                        }
                                        //The return value is 1 based, so we have to substract 1
                                        node.AddChild(new TreeNode(Tokens.Expression, "- 1"));
                                        break;
                                    }

                                case "@MID":
                                    node.NodeValue = FunctionTranslator.TranslateFunction(DatabaseBrand.SqlServer, node.NodeValue);
                                    //Find the index argument
                                    if (node.Children.Count > 3)
                                    {
                                        node.Children[3] = ApplyCast(node.Children[3], "INT");
                                        node.Children[3].AddChild(new TreeNode(Tokens.Expression, "+ 1"));

                                    }

                                    if (node.Children.Count > 5)
                                    {
                                        node.Children[5] = ApplyCast(node.Children[5], "INT");
                                    }
                                    break;
                                case "@REPLACE":
                                    {
                                        node.NodeValue = FunctionTranslator.TranslateFunction(DatabaseBrand.SqlServer, node.NodeValue);
                                        //Find the index argument
                                        if (node.Children.Count > 3)
                                        {
                                            TreeNode indexNode = node.Children[3]; //this is the 2nd parameter
                                            indexNode.AddChild(new TreeNode(Tokens.Expression, "+ 1"));
                                        }
                                        break;
                                    }

                                case "@SUBSTRING":
                                    {
                                        node.NodeValue = FunctionTranslator.TranslateFunction(DatabaseBrand.SqlServer, node.NodeValue);
                                        //Find the index argument
                                        if (node.Children.Count > 3)
                                        {
                                            TreeNode indexNode = node.Children[3]; //this is the 2nd parameter
                                            indexNode.AddChild(new TreeNode(Tokens.Expression, "+ 1"));
                                        }
                                        if (node.Children.Count == 5)
                                        {
                                            //The length parameter is not mandatory for @SUBSTRING but it is for SqlServer's SUBSTRING function
                                            //The 3rd parameter will be the length of the 1st parameter
                                            node.Children.Insert(new TreeNode(Tokens.Comma, ","), 4);
                                            node.Children.Insert(new TreeNode(Tokens.Expression, "LEN(" + node.Children[1].ToString() + ")"), 5);
                                        }
                                        break;
                                    }

                                //@SCAN(string, pattern) -> CHARINDEX(pattern, string)
                                case "@SCAN":
                                    {
                                        node.NodeValue = FunctionTranslator.TranslateFunction(DatabaseBrand.SqlServer, node.NodeValue);
                                        node.NodeInfo = "SKIP";
                                        //exchange the arguments
                                        if (node.Children.Count >= 4)
                                        {
                                            TreeNode tempNode = new TreeNode(node.Children[1], "dummy");
                                            node.Children[1] = new TreeNode(node.Children[3], "dummy");
                                            node.Children[3] = tempNode;
                                        }

                                        //substract 1 from the result
                                        node.AddChild(new TreeNode(Tokens.MathOperator, "-"));
                                        node.AddChild(new TreeNode(Tokens.NumericConst, "1"));
                                        break;
                                    }
                                //@RDU Added @NOW 
                                case "@NOW":
                                    {
                                        node.AddChild(new TreeNode(Tokens.LeftParant, "("));
                                        node.AddChild(new TreeNode(Tokens.RightParant, ")"));
                                        node.NodeValue = FunctionTranslator.TranslateFunction(DatabaseBrand.SqlServer, node.NodeValue);
                                        node.NodeInfo = "SKIP";
                                        break;
                                    }
                                case "@NULLVALUE":
                                    {
                                        //@NULLVALUE(x,y) -> CASE WHEN x IS NULL THEN y ELSE x END
                                        //Find parent of function (expression)
                                        TreeNode parentNode = node.Parent;
                                        TreeNode x = targetTree.FindNode(node, Tokens.Expression);
                                        TreeNode y = targetTree.FindNodeReverse(node, Tokens.Expression);

                                        TreeNode selectNode = targetTree.FindNode(root, "SELECT");
                                        TreeNode newNode = new TreeNode(Tokens.Expression);

                                        //Create new expression
                                        newNode.AddChild(new TreeNode(Tokens.Keyword, "CASE"));
                                        newNode.AddChild(new TreeNode(Tokens.Keyword, "WHEN"));
                                        newNode.AddChild(x);
                                        newNode.AddChild(new TreeNode(Tokens.Keyword, "IS"));
                                        newNode.AddChild(new TreeNode(Tokens.SysKeyword, "NULL"));
                                        newNode.AddChild(new TreeNode(Tokens.Keyword, "THEN"));
                                        newNode.AddChild(y);
                                        newNode.AddChild(new TreeNode(Tokens.Keyword, "ELSE"));
                                        newNode.AddChild(x);
                                        newNode.AddChild(new TreeNode(Tokens.Keyword, "END"));

                                        parentNode.Children[parentNode.Children.IndexOf(node)] = newNode;
                                        break;
                                    }
                                case "@ROUND":
                                    {
                                        // for the case where the @round expression contains a date diff
                                        // ex: @round(@now - @now, 0)
                                        // this will be translated to DATEDIFF(dd, GETDATE(), GETDATE())
                                        TreeNode nowNode = targetTree.FindNode(node, "@NOW");
                                        TreeNode mathOperatorNode = targetTree.FindNode(node, Tokens.MathOperator);
                                        if (nowNode != null && mathOperatorNode != null && nowNode.Parent == mathOperatorNode.Parent)
                                        {
                                            if (nowNode.Parent.Children.Count == 3 && node.Children.Count == 5 && node.Children[3].Children[0].NodeValue == "0")
                                            {
                                                node.NodeValue = "DATEDIFF";
                                                node.NodeInfo = "SKIP";

                                                // remove the comma
                                                node.RemoveChild(2);
                                                // remove the decimal places (0 decimal places)
                                                node.RemoveChild(2);

                                                // insert the date part specifier dd and comma
                                                node.InsertChild(new TreeNode(Tokens.Identifier, "dd"), 1);
                                                node.InsertChild(new TreeNode(Tokens.Comma, ","), 2);

                                                // now the expression will look like: DATEDIFF ( dd, @NOW-@NOW )

                                                // change the math operator to comma
                                                mathOperatorNode.NodeValue = ",";
                                                mathOperatorNode.NodeType = Tokens.Comma;

                                                // the final expression will look like: DATEDIFF(dd, GETDATE(), GETDATE())
                                            }
                                        }
                                        else
                                        {
                                            node.NodeValue = FunctionTranslator.TranslateFunction(DatabaseBrand.SqlServer, node.NodeValue);
                                            node.NodeInfo = "SKIP";
                                        }
                                        break;
                                    }
                                //
                                //For the rest of the functions we can write user-defined functions in MSSQL
                                default:
                                    {
                                        node.NodeValue = FunctionTranslator.TranslateFunction(DatabaseBrand.SqlServer, node.NodeValue);
                                        node.NodeInfo = "SKIP";
                                        break;
                                    }
                            }
                            break;
                            #endregion Functions
                        }

                    case Tokens.Concatenate: //Replace the "||" operator with "+"
                        {
                            node.NodeValue = "+";

                            //Check if the operands are columns and if yes convert them to nvarchar
                            //Otherwise we might get errors if the concatening is made for different data types
                            if (Settings.ConvertConcatOperandsToString)
                            {
                                TreeNode parentNode = targetTree.FindParent(root, node);
                                if (parentNode != null)
                                {
                                    int index = parentNode.Children.IndexOf(node);
                                    if (index > 0 && parentNode.Children.Count >= index + 1)
                                    {
                                        TreeNode leftNode = parentNode.Children[index - 1];
                                        //RHE:
                                        if (leftNode.NodeType == Tokens.RightParant && parentNode.Children.Count >= index - 2)
                                        {
                                            leftNode = parentNode.Children[index - 2];
                                        }
                                        TreeNode rightNode = parentNode.Children[index + 1];

                                        bool isRightNode = false;

                                        TreeNode nodeToConvert = null;

                                        if (ConvertNodeToVarchar(leftNode))
                                        {
                                            nodeToConvert = leftNode;
                                        }
                                        else if (ConvertNodeToVarchar(rightNode))
                                        {
                                            nodeToConvert = rightNode;
                                            isRightNode = true;
                                        }
                                        else if (leftNode.NodeType == Tokens.StringConst)
                                        {
                                            nodeToConvert = rightNode;
                                            isRightNode = true;
                                        }
                                        else if (rightNode.NodeType == Tokens.StringConst)
                                        {
                                            nodeToConvert = leftNode;
                                        }

                                        if (nodeToConvert != null)
                                        {
                                            if (!nodeToConvert.NodeValue.StartsWith("CONVERT(nvarchar"))
                                            {
                                                var colInfo = GetColumnInfo(nodeToConvert, targetTree, root);
                                                if (colInfo == null //not a column
                                                    || (colInfo.Type != "varchar" && colInfo.Type != "nvarchar"))
                                                {
                                                    if (isRightNode)
                                                    {
                                                        TransformTreeSqlBase_MsSql(ref targetTree, root, nodeToConvert);
                                                        nodeToConvert = parentNode.Children[index + 1];
                                                    }

                                                    nodeToConvert.NodeValue = "CONVERT(nvarchar(MAX)," + nodeToConvert.NodeValue;
                                                    nodeToConvert.AddChild(new TreeNode(Tokens.RightParant, ")"));
                                                    nodeToConvert.NodeInfo = "changed";
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            break;
                        }


                    case Tokens.JoinOperator: //Replace old join syntax with the new one
                        {
                            TranslateOuterJoins(targetTree, root, node);
                            break;
                        }

                    case Tokens.Row:
                        {
                            // for the first row the bind variables are replaced with the value list
                            if (node.NodeInfo == "1")
                            {
                                TreeNode nodeParent = targetTree.FindParent(root, node);

                                int i;
                                for (i = 1; i < nodeParent.Children.IndexOf(node); i++)
                                {
                                    nodeParent.Children[i].NodeInfo = "NOT USED";
                                }

                                nodeParent.InsertChild(new TreeNode(Tokens.RightParant, ")"), i + 1);
                                node.NodeInfo = "SKIP";
                            }

                            // if there are more rows, separate INSERT statements are created for each row
                            else if (node.NodeInfo != "SKIP")
                            {
                                TreeNode nodeInsertInto = targetTree.FindNode(root, "INSERT INTO");
                                TreeNode nodeInsert = new TreeNode(Tokens.Insert);

                                root.AddChild(new TreeNode(Tokens.Semicolon, ";"));
                                root.AddChild(nodeInsert);

                                nodeInsert.AddChild(nodeInsertInto);
                                TreeNode nodeValues = new TreeNode(Tokens.Keyword, "VALUES");
                                nodeInsert.AddChild(nodeValues);

                                nodeValues.AddChild(new TreeNode(Tokens.LeftParant, "("));
                                for (int i = 0; i < node.Children.Count; i++)
                                {
                                    nodeValues.AddChild((TreeNode)node.Children[i]);
                                }
                                nodeValues.AddChild(new TreeNode(Tokens.RightParant, ")"));

                                //remove row's children from tree
                                while (node.Children.Count > 0)
                                {
                                    node.RemoveChild(0);
                                }
                            }
                            break;
                        }


                    case Tokens.SysKeyword:
                        {
                            //These syskeywords can appear in expressions like: expr +|- const syskeyword
                            //They are translated using the DATEADD function: DATEADD(datepart, +|-const, expr)
                            if (DATEADDCONSTANTS.Contains(node.NodeValue))
                            {
                                //Find parent of the node
                                TreeNode exprNode = targetTree.FindParent(root, node);
                                TreeNode functionExprNode = new TreeNode(Tokens.Expression);
                                TreeNode parentNode = functionExprNode;
                                TreeNode currentNode = targetTree.FindNode(exprNode, Tokens.SysKeyword, DATEADDCONSTANTS.ToArray());
                                TreeNode argNode = null;
                                TreeNode aliasNode = null;
                                if (Settings.AddAlias)
                                {
                                    aliasNode = targetTree.FindNode(exprNode, Tokens.Correlation);
                                    exprNode.RemoveChild(aliasNode);
                                    exprNode.RemoveChild(targetTree.FindNode(exprNode, "AS"));
                                }

                                do
                                {
                                    TreeNode dateaddNode = new TreeNode(Tokens.Function, "DATEADD");
                                    parentNode.AddChild(dateaddNode);

                                    argNode = new TreeNode(Tokens.Expression);
                                    dateaddNode.AddChild(argNode);
                                    argNode.AddChild(new TreeNode(Tokens.LeftParant, "("));

                                    //Create datepart argument of the DATEADD function
                                    if (currentNode.NodeValue == "YEAR" || currentNode.NodeValue == "YEARS")
                                        argNode.AddChild(new TreeNode(Tokens.Identifier, "yy"));
                                    else if (currentNode.NodeValue == "MONTH" || currentNode.NodeValue == "MONTHS")
                                        argNode.AddChild(new TreeNode(Tokens.Identifier, "mm"));
                                    else if (currentNode.NodeValue == "DAY" || currentNode.NodeValue == "DAYS")
                                        argNode.AddChild(new TreeNode(Tokens.Identifier, "dd"));
                                    else if (currentNode.NodeValue == "HOUR" || currentNode.NodeValue == "HOURS")
                                        argNode.AddChild(new TreeNode(Tokens.Identifier, "hh"));
                                    else if (currentNode.NodeValue == "MINUTE" || currentNode.NodeValue == "MINUTES")
                                        argNode.AddChild(new TreeNode(Tokens.Identifier, "mi"));
                                    else if (currentNode.NodeValue == "SECOND" || currentNode.NodeValue == "SECONDS")
                                        argNode.AddChild(new TreeNode(Tokens.Identifier, "ss"));
                                    else
                                        argNode.AddChild(new TreeNode(Tokens.Identifier, "ms"));

                                    argNode.AddChild(new TreeNode(Tokens.Comma, ","));

                                    exprNode.RemoveChild(currentNode);

                                    currentNode = (TreeNode)exprNode.Children[exprNode.Children.Count - 2]; // +|-
                                    argNode.AddChild(currentNode);

                                    exprNode.RemoveChild(currentNode);

                                    currentNode = exprNode.Children[exprNode.Children.Count - 1]; //numeric constant
                                    argNode.AddChild(currentNode);
                                    argNode.AddChild(new TreeNode(Tokens.Comma, ","));

                                    exprNode.RemoveChild(currentNode);

                                    TreeNode subExprNode = new TreeNode(Tokens.Expression);
                                    argNode.AddChild(subExprNode);

                                    currentNode = exprNode.Clone(currentNode);

                                    parentNode = subExprNode;
                                    argNode.AddChild(new TreeNode(Tokens.RightParant, ")"));

                                } while (currentNode.NodeType == Tokens.Keyword && DATEADDCONSTANTS.Contains(currentNode.NodeValue));

                                parentNode.AddChild(currentNode);

                                exprNode.RemoveAll();

                                exprNode.AddChild(functionExprNode);

                                if (Settings.AddAlias && aliasNode != null)
                                {
                                    exprNode.AddChild(new TreeNode(Tokens.Keyword, "AS"));
                                    exprNode.AddChild(aliasNode);
                                }
                            }
                            else
                            {
                                switch (node.NodeValue)
                                {
                                    case "SYSDATETIME":
                                    case "SYSDATE":
                                    case "SYSTIME":
                                        {
                                            node.NodeValue = "CURRENT_TIMESTAMP";
                                            break;
                                        }

                                    case "USER":
                                        {
                                            node.NodeValue = "CURRENT_USER";
                                            break;
                                        }

                                    case "SYSDBSEQUENCE.CURRVAL":
                                        {
                                            node.NodeValue = "dbo.CURRVAL()";
                                            break;
                                        }

                                    case "SYSDBSEQUENCE.NEXTVAL":
                                        {
                                            //This is translated using a procedure and a temp variable
                                            root.InsertChild(new TreeNode(Tokens.Statement,
                                                @"DECLARE @nextVal int
                                      EXEC dbo.NEXTVAL @nextVal OUTPUT"), 0);

                                            node.NodeValue = "@nextVal";
                                            break;
                                        }

                                    case "SYSDBTRANSID":
                                    case "SYSTIMEZONE":
                                        {
                                            throw new NotSupportedException("System Keyword " + node.NodeValue + " is not supported by SqlTranslator");
                                        }
                                }
                            }
                            break;
                        }


                    case Tokens.DataTypeKeyword:
                        {
                            switch (node.NodeValue)
                            {
                                case "INTEGER":
                                    {
                                        node.NodeValue = "INT";
                                        break;
                                    }

                                case "CHAR":
                                    {
                                        node.NodeValue = Settings.Unicode ? "NCHAR" : "CHAR";
                                        break;
                                    }

                                case "VARCHAR":
                                    {
                                        node.NodeValue = Settings.Unicode ? "NVARCHAR" : "VARCHAR";
                                        break;
                                    }

                                case "LONG":
                                case "LONG VARCHAR":
                                    {
                                        node.NodeValue = Settings.Unicode ? "NVARCHAR(MAX)" : "VARCHAR(MAX)";
                                        break;
                                    }

                                case "TIMESTAMP":
                                case "DATE":
                                case "TIME":
                                    {
                                        node.NodeValue = "DATETIME";
                                        break;
                                    }

                                case "NUMBER":
                                    {
                                        node.NodeValue = "FLOAT";
                                        break;
                                    }

                                case "DOUBLE PRECISION":
                                    {
                                        node.NodeValue = "FLOAT(53)";
                                        break;
                                    }
                            }
                            break;
                        }


                    case Tokens.Keyword:
                        {
                            switch (node.NodeValue)
                            {
                                case "NATURAL":
                                case "USING":
                                //case "FOR":
                                //case "CHECK EXISTS":
                                case "ADJUSTING":
                                //case "PCTFREE":
                                //case "RESTRICT":
                                //case "SET NULL":
                                //case "SIZE":
                                case "TRANSACTION":
                                case "FORCE":
                                case "DISTINCTCOUNT":
                                    {
                                        throw new NotSupportedException("Keyword " + node.NodeValue + " is not supported by SqlTranslator");
                                    }

                                case "PCTFREE":
                                case "SIZE":
                                    {
                                        node.NodeInfo = "NOT USED";
                                        break;
                                    }
                                //CHECK EXISTS will raise an error if the number of affected rows is 0
                                case "CHECK EXISTS":
                                    {
                                        node.NodeValue = @"IF @@ROWCOUNT = 0 RAISERROR('Error: 00380 EXE CEF CHECK EXISTS failure', 16, 1)";
                                        break;
                                    }

                                //SqlServer doens't support GROUP BY integer constant
                                //have to replace constants with column names
                                case "GROUP BY":
                                    {
                                        TreeNode selectNode = new TreeNode(targetTree.FindNode(root, "SELECT"), "");
                                        int[] ExpressionIndex = new int[selectNode.Children.Count];
                                        int order = 0;

                                        for (int i = 0; i < selectNode.Children.Count; i++)
                                        {
                                            if (selectNode.Children[i].NodeType == Tokens.Expression)
                                            {
                                                ExpressionIndex[order++] = i;
                                            }
                                        }
                                        for (int i = 0; i < node.Children.Count; i++)
                                        {
                                            if (node.Children[i].NodeType == Tokens.NumericConst)
                                            {
                                                TreeNode currentNode = new TreeNode(selectNode.Children[ExpressionIndex[Convert.ToInt32(node.Children[i].NodeValue) - 1]], "");

                                                //The GROUP BY expression has to contain a column
                                                if (targetTree.FindNode(currentNode, Tokens.Column) != null)
                                                {

                                                    for (int k = 0; k < currentNode.Children.Count; k++)
                                                    {
                                                        if (currentNode.Children[k].NodeValue == "AS")
                                                        {
                                                            currentNode.RemoveChild(k);
                                                            currentNode.RemoveChild(k);
                                                        }
                                                    }
                                                    node.Children[i] = currentNode;
                                                }
                                                else
                                                {
                                                    //Remove the number from the GROUP BY list
                                                    node.Children[i].NodeInfo = "NOT USED";

                                                    if (i == node.Children.Count - 1)
                                                    {
                                                        if (i - 1 > 0 && node.Children[i - 1].NodeInfo != "NOT USED")
                                                        {
                                                            node.Children[i - 1].NodeInfo = "NOT USED";
                                                        }
                                                        else
                                                        {
                                                            for (int j = node.Children.Count - 1; j >= 0; j--)
                                                            {
                                                                if (node.Children[j].NodeType == Tokens.Comma &&
                                                                    node.Children[j].NodeInfo != "NOT USED")
                                                                {
                                                                    node.Children[j].NodeInfo = "NOT USED";
                                                                    break;
                                                                }
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        node.Children[i + 1].NodeInfo = "NOT USED";
                                                    }

                                                    //If GROUP BY node does not contain any used nodes remove the GROUP BY node
                                                    bool hasUsedChildren = false;
                                                    foreach (TreeNode childNode in node.Children)
                                                    {
                                                        if (childNode.NodeInfo != "NOT USED")
                                                        {
                                                            hasUsedChildren = true;
                                                        }
                                                    }

                                                    if (!hasUsedChildren)
                                                    {
                                                        node.NodeInfo = "NOT USED";
                                                    }
                                                }
                                            }
                                        }
                                        break;
                                    }

                                case "CLUSTERED HASHED":
                                    {
                                        node.NodeValue = "CLUSTERED";
                                        break;
                                    }

                                //this clause is ignored by SqlBase so we can delete it without throwing an exception
                                case "IN DATABASE":
                                    {
                                        if (node.NodeInfo == "CREATE")
                                        {
                                            targetTree.RemoveNode(node);
                                        }
                                        break;
                                    }
                                case "IN":
                                    {
                                        if (node.NodeInfo == "CREATE")
                                        {
                                            targetTree.RemoveNode(node);
                                        }
                                        else
                                        {
                                            //if the IN predicate contains only one expression we have to surround it with paranthesis
                                            TreeNode nodeParent = targetTree.FindParent(root, node);
                                            if (nodeParent != null && nodeParent.NodeType == Tokens.Predicate && nodeParent.Children.Count == 3)
                                            {
                                                nodeParent.InsertChild(new TreeNode(Tokens.LeftParant, "("), 2);
                                                nodeParent.AddChild(new TreeNode(Tokens.RightParant, ")"));
                                            }
                                        }
                                        break;
                                    }

                                case "NOT NULL WITH DEFAULT":
                                    {
                                        //If AlterColumn stored procedure has to be executed, 
                                        //then replacing NOT NULL WITH DEFAULT with correct value will be done in
                                        //AlterColumn where NOT NULL WITH DEFAULT is expected as last parameter
                                        //TreeNode nodeDataType = targetTree.FindNode(nodeParent, Tokens.DataType);
                                        TreeNode temp = targetTree.FindNode(root, Tokens.Statement);
                                        if (temp == null || !targetTree.FindNode(root, Tokens.Statement).NodeValue.ToUpper().StartsWith("EXEC ALTERCOLUMN"))
                                        {
                                            TreeNode nodeParent = targetTree.FindParent(root, node);

                                            TreeNode nodeDataType = nodeParent.Children[nodeParent.Children.IndexOf(node) - 1];
                                            string colDataType = nodeDataType.Children[0].NodeValue;

                                            node.NodeType = Tokens.Expression;
                                            node.NodeValue = "";

                                            node.AddChild(new TreeNode(Tokens.Keyword, "NOT NULL", "SKIP"));

                                            node.AddChild(new TreeNode(Tokens.Keyword, "DEFAULT", "SKIP"));
                                            switch (colDataType)
                                            {
                                                case "NCHAR":
                                                case "CHAR":
                                                case "NVARCHAR":
                                                case "VARCHAR":
                                                    {
                                                        node.AddChild(new TreeNode(Tokens.StringConst, @"' '", "SKIP"));
                                                        break;
                                                    }

                                                case "DECIMAL":
                                                case "DEC":
                                                case "INT":
                                                case "SMALLINT":
                                                case "FLOAT":
                                                    {
                                                        node.AddChild(new TreeNode(Tokens.NumericConst, "0", "SKIP"));
                                                        break;
                                                    }

                                                case "DATETIME":
                                                case "TIMESTAMP":
                                                    {
                                                        node.AddChild(new TreeNode(Tokens.SysKeyword, "CURRENT_TIMESTAMP", "SKIP"));
                                                        break;
                                                    }
                                            }
                                        }

                                        break;
                                    }

                                case "AUTO_INCREMENT":
                                    {
                                        node.NodeValue = "IDENTITY";
                                        break;
                                    }

                                case "SAVEPOINT":
                                    {
                                        node.NodeValue = "SAVE TRANSACTION";
                                        break;
                                    }

                                case "ORDER BY":
                                    {
                                        //ORDER BY items must appear in the select list if SELECT DISTINCT is specified
                                        TreeNode select = targetTree.FindNode(root, "SELECT");
                                        TreeNode distinct = targetTree.FindNode(select, "DISTINCT");
                                        if (distinct != null)
                                        {
                                            TreeNodeCollection orderbyColumns = new TreeNodeCollection();
                                            TreeNodeCollection notFoundOrderbyColumns = new TreeNodeCollection();
                                            targetTree.FindAll(node, Tokens.Column, ref orderbyColumns);
                                            bool found = false;
                                            foreach (TreeNode column in orderbyColumns)
                                            {
                                                found = false;
                                                foreach (TreeNode selectColumn in select.Children)
                                                {
                                                    if (selectColumn.NodeType == Tokens.Expression)
                                                    {
                                                        //remove the [] - it has already been added for the select columns
                                                        string columnName = selectColumn.ToString().RemoveBrackets().ToUpper().Trim();
                                                        //also remove the surrounding paranthesis
                                                        if (columnName.StartsWith("(") && columnName.EndsWith(")"))
                                                        {
                                                            columnName = columnName.Substring(1, columnName.Length - 2).Trim();
                                                        }

                                                        //check the column names without the table name
                                                        if (column.NodeValue.Substring(column.NodeValue.IndexOf(".") + 1).ToUpper() ==
                                                            columnName.Substring(columnName.IndexOf(".") + 1).ToUpper())
                                                        {
                                                            found = true;
                                                            break;
                                                        }
                                                    }
                                                }
                                                if (!found)
                                                {
                                                    notFoundOrderbyColumns.Add(column);
                                                }
                                            }
                                            if (notFoundOrderbyColumns.Count > 0)
                                            {
                                                //The statement has to be translated like this:
                                                //*****Original******
                                                //SELECT DISTINCT col1, col2, ..., coln
                                                //FROM TABLENAME
                                                //ORDER BY x
                                                //****Translated******
                                                //SELECT col1, col2, ..., coln
                                                //FROM TABLENAME
                                                //GROUP BY col1, col2, ..., coln
                                                //ORDER BY MIN(x)
                                                distinct.NodeInfo = "NOT USED";

                                                TreeNode groupBy = targetTree.FindNode(select, "GROUP BY");
                                                if (groupBy == null)
                                                {
                                                    groupBy = new TreeNode(Tokens.Keyword, "GROUP BY");
                                                    root.InsertChild(groupBy, root.Children.IndexOf(node));
                                                }

                                                string groupByString = groupBy.ToString();
                                                foreach (TreeNode selectColumn in select.Children)
                                                {
                                                    if (!groupByString.Contains(selectColumn.ToString()) && targetTree.FindNode(selectColumn, Tokens.Column) != null)
                                                    {
                                                        if (groupBy.Children.Count > 0)
                                                        {
                                                            groupBy.Children.Add(new TreeNode(Tokens.Comma, ","));
                                                        }
                                                        TreeNode columnToAdd = new TreeNode(selectColumn, "");
                                                        TreeNode asNode = targetTree.FindNode(columnToAdd, "AS");
                                                        if (asNode != null)
                                                        {
                                                            columnToAdd.Children.Remove(asNode);
                                                            columnToAdd.Children.RemoveAt(columnToAdd.Children.Count - 1);
                                                        }
                                                        groupBy.Children.Add(new TreeNode(columnToAdd));
                                                    }
                                                }

                                                foreach (TreeNode column in notFoundOrderbyColumns)
                                                {
                                                    TreeNode expr = new TreeNode(Tokens.Expression);
                                                    expr.AddChild(new TreeNode(Tokens.Function, "MIN"));
                                                    expr.AddChild(new TreeNode(Tokens.LeftParant, "("));
                                                    expr.AddChild(column);
                                                    expr.AddChild(new TreeNode(Tokens.RightParant, ")"));

                                                    node.Children[node.Children.IndexOf(column)] = expr;
                                                }
                                            }
                                            node.NodeInfo = "SKIP";
                                        }
                                        //Remove ORDER BY for statements like SELECT COUNT(*) FROM TABLE1 ORDER BY COLUMN 
                                        //where we only have an aggregate function in the select list: COUNT, MAX, MIN, AVG, SUM, @MEDIAN and @SDV 
                                        if (select.Children.Count == 1)
                                        {
                                            string selectList = select.Children[0].ToString();
                                            if (selectList.StartsWith("COUNT") || selectList.StartsWith("SUM") || selectList.StartsWith("AVG") ||
                                               selectList.StartsWith("MAX") || selectList.StartsWith("MIN") || selectList.StartsWith("@MEDIAN") || selectList.StartsWith("@SDV"))
                                            {
                                                node.NodeInfo = "NOT USED";
                                            }
                                        }
                                        break;
                                    }

                                case "LIKE":
                                    {
                                        //in SqlBase versions older than 9.0 there is a bug in evaluating the NOT LIKE predicates
                                        //column NOT LIKE 'c' returns also null values
                                        //so we have to add a 2nd condition: OR column IS NULL
                                        //also conditions like: column LIKE '' return null values too
                                        //but column NOT LIKE '' don't
                                        TreeNode predicateNode = targetTree.FindParent(root, node);
                                        if (predicateNode != null && predicateNode.NodeType == Tokens.Predicate)
                                        {
                                            if (Settings.SqlBaseVersion == DatabaseBrand.SqlBaseOld)
                                            {

                                                int index = predicateNode.Children.IndexOf(node);
                                                if ((index > 1 && predicateNode.Children[index - 1].NodeValue == "NOT" && predicateNode.Children[index + 1].NodeValue != "''") ||  //NOT LIKE 'c'
                                                    (index == 1 && predicateNode.Children[index + 1].NodeValue == "''"))    //LIKE ''
                                                {
                                                    TreeNode exprNode = targetTree.FindNode(predicateNode, Tokens.Expression); //column node
                                                    if (exprNode != null)
                                                    {
                                                        TreeNode newNode = new TreeNode(Tokens.Expression, "", "SKIP");
                                                        newNode.AddChild(new TreeNode(Tokens.Keyword, "OR"));
                                                        newNode.AddChild(exprNode);
                                                        newNode.AddChild(new TreeNode(Tokens.Keyword, "IS NULL"));

                                                        predicateNode.AddChild(newNode);
                                                    }

                                                    //surround the condition with paranthesis
                                                    predicateNode.InsertChild(new TreeNode(Tokens.LeftParant, "("), 0);
                                                    predicateNode.AddChild(new TreeNode(Tokens.RightParant, ")"));
                                                    node.NodeInfo = "SKIP";
                                                }
                                            }

                                            //Have to specify the escape character in the like pattern
                                            predicateNode.InsertChild(new TreeNode(Tokens.Expression, "ESCAPE '\\'"), predicateNode.Children.IndexOf(node) + 2);
                                        }
                                        break;
                                    }

                                case "STATISTICS":
                                    {
                                        //UPDATE STATISTICS ON DATABASE
                                        if (targetTree.FindNode(root, "DATABASE") != null)
                                        {
                                            targetTree.root = new TreeNode(Tokens.Statement, "EXEC sp_updatestats");
                                            break;
                                        }

                                        //UPDATE STATISTICS ON INDEX ident
                                        if (targetTree.FindNode(root, "INDEX") != null)
                                        {
                                            string indexName = targetTree.FindNode(root, Tokens.Identifier).NodeValue;
                                            targetTree.root = new TreeNode(Tokens.Statement, "EXEC UpdateStatsOnIndex " + indexName);
                                            break;
                                        }

                                        if (targetTree.FindNode(root, "SET") != null)
                                        {
                                            throw new NotSupportedException("UPDATE STATISTICS with SET clause is not supported.");
                                        }

                                        //UPDATE STATISTICS ON TABLE ident
                                        TreeNode tempNode = targetTree.FindNode(root, "ON");
                                        if (tempNode != null)
                                        {
                                            tempNode.NodeInfo = "NOT USED";
                                        }

                                        tempNode = targetTree.FindNode(root, "TABLE");
                                        if (tempNode != null)
                                        {
                                            tempNode.NodeInfo = "NOT USED";
                                        }
                                        break;
                                    }

                                case "CURRENT DATETIME":
                                case "CURRENT TIMESTAMP":
                                    {
                                        node.NodeValue = "CURRENT_TIMESTAMP";
                                        break;
                                    }

                                case "CURRENT DATE":
                                    {
                                        node.NodeValue = "dbo.CURRENTDATE()";
                                        break;
                                    }

                                case "CURRENT TIME":
                                    {
                                        node.NodeValue = "dbo.CURRENTTIME()";
                                        break;
                                    }

                                //Keywords used in CREATE SYNONYM
                                case "PUBLIC":
                                case "EXTERNAL FUNCTION":
                                case "FUNCTION":
                                case "PROCEDURE":
                                    {
                                        node.NodeInfo = "NOT USED";
                                        break;
                                    }

                                case "TABLE":
                                    {
                                        if (root.ToString().Contains("SYNONYM"))
                                        {
                                            node.NodeInfo = "NOT USED";
                                        }
                                        break;
                                    }

                                case "DROP":
                                    {
                                        if (root.Children[0].NodeType == Tokens.Alter &&
                                            !root.ToString().Contains(" PRIMARY ") &&
                                            !root.ToString().Contains(" FOREIGN "))
                                        {
                                            node.NodeValue = "DROP COLUMN";

                                            string tableName = String.Empty, columnName = String.Empty;
                                            TreeNode temp = targetTree.FindNode(root, Tokens.Table);
                                            if (temp != null)
                                            {
                                                tableName = GetNameWithCase(temp.NodeValue);
                                            }
                                            temp = targetTree.FindNode(root, Tokens.Column);
                                            if (temp != null)
                                            {
                                                columnName = GetNameWithCase(temp.NodeValue);
                                            }

                                            targetTree.root.Children[0] = new TreeNode(Tokens.Statement,
                                                String.Format("EXEC DropColumn '{0}', '{1}'",
                                                tableName.RemoveBrackets(),
                                                columnName));
                                        }
                                        break;
                                    }

                                case "RENAME":
                                    {
                                        //Get the table name
                                        TreeNode tableNode = targetTree.FindNode(root, Tokens.Table);
                                        string tableName = String.Empty;
                                        if (tableNode != null)
                                        {
                                            tableName = tableNode.NodeValue;
                                        }
                                        TreeNode alterNode = root.Children[0];
                                        int index = alterNode.Children.IndexOf(node);

                                        if (!alterNode.ToString().Contains("RENAME TABLE"))
                                        {
                                            string oldColumnName = "";
                                            string newColumnName = "";

                                            while (index < alterNode.Children.Count)
                                            {
                                                oldColumnName = GetNameWithCase(alterNode.Children[++index].NodeValue);
                                                newColumnName = GetNameWithCase(alterNode.Children[++index].NodeValue);

                                                root.InsertChild(new TreeNode(Tokens.Statement,
                                                    String.Format("EXEC sp_rename '{0}.{1}', '{2}'", tableName,
                                                        oldColumnName,
                                                        newColumnName)), 0);
                                                index++;
                                            }
                                        }
                                        else
                                        {
                                            string newTableName = GetNameWithCase(alterNode.Children[index + 2].NodeValue);
                                            root.InsertChild(new TreeNode(Tokens.Statement,
                                                    String.Format("EXEC sp_rename '{0}', '{1}'", tableName,
                                                        newTableName)), 0);
                                        }

                                        //Remove the original statement
                                        root.RemoveChild(root.Children.Count - 1);

                                        break;
                                    }

                                case "MODIFY":
                                    {
                                        if (targetTree.FindNode(root, Tokens.DataTypeKeyword) != null)
                                        {
                                            node.NodeValue = "ALTER COLUMN";
                                            break;
                                        }
                                        else
                                        {
                                            string tableName = String.Empty, columnName = String.Empty, val = String.Empty;
                                            TreeNode temp = targetTree.FindNode(root, Tokens.Table);
                                            if (temp != null)
                                            {
                                                tableName = GetNameWithCase(temp.NodeValue);
                                            }
                                            temp = targetTree.FindNode(root, Tokens.Column);
                                            if (temp != null)
                                            {
                                                columnName = GetNameWithCase(temp.NodeValue);
                                            }
                                            temp = root.Children[0].Children[root.Children[0].Children.Count - 1];
                                            val = temp.NodeValue;

                                            targetTree.root.Children[0] = new TreeNode(Tokens.Statement,
                                                String.Format("EXEC AlterColumn '{0}', '{1}', '{2}'",
                                                tableName.RemoveBrackets(),
                                                columnName,
                                                val));
                                        }
                                        break;
                                    }

                                case "PRIMARY KEY":
                                    {
                                        if (targetTree.FindNode(root, Tokens.Alter) != null)
                                        {
                                            if (!root.ToString().Contains(" DROP "))
                                            {
                                                node.NodeValue = "ADD PRIMARY KEY";
                                            }
                                            else
                                            {
                                                //DROP PRIMARY KEY is translated using a stored procedure
                                                //EXEC [dbo].[DropPrimaryKey] tablename
                                                string tableName = GetTableName(targetTree.FindNode(root, Tokens.Table).NodeValue);
                                                targetTree.root = new TreeNode(Tokens.Statement, "EXEC [dbo].[DropPrimaryKey] " + tableName);

                                            }
                                        }
                                        break;
                                    }

                                case "FOREIGN KEY":
                                    {
                                        TreeNode alterNode = targetTree.FindNode(root, Tokens.Alter);
                                        if (alterNode != null)
                                        {
                                            int foreignKeyIndex = alterNode.Children.IndexOf(node);
                                            if (!alterNode.ToString().Contains(" DROP "))
                                            {
                                                //ADD CONSTRAINT keyname FOREIGN KEY (colname) REFERENCES tablename
                                                TreeNode keyNameNode = targetTree.FindNode(alterNode, Tokens.KeyName);
                                                alterNode.InsertChild(new TreeNode(Tokens.Keyword, "ADD CONSTRAINT"), foreignKeyIndex);
                                                alterNode.InsertChild(new TreeNode(keyNameNode), foreignKeyIndex + 1);
                                                keyNameNode.NodeInfo = "NOT USED";
                                                node.NodeInfo = "SKIP";

                                            }
                                            else
                                            {
                                                //DROP CONSTRAINT keyname
                                                node.NodeValue = "CONSTRAINT";
                                            }
                                        }
                                        else
                                        {
                                            //For CREATE TABLE with FOREIGN KEY we have to remove the key name
                                            TreeNode keyNameNode = targetTree.FindNode(root, Tokens.KeyName);
                                            if (keyNameNode != null)
                                            {
                                                keyNameNode.NodeInfo = "NOT USED";
                                            }
                                        }
                                        break;
                                    }

                                case "SET":
                                    {
                                        if (Settings.Rowid == RowidType.UniqueIdentifier &&
                                            !targetTree.ToString().Contains("UPDATE STATISTICS"))
                                        {
                                            node.AddChild(new TreeNode(Tokens.Comma, ","));
                                            node.AddChild(new TreeNode(Tokens.Column, "ROWID"));
                                            node.AddChild(new TreeNode(Tokens.RelatOperator, "="));
                                            node.AddChild(new TreeNode(Tokens.Expression, "newid()"));
                                        }
                                        break;
                                    }

                                case "DROP VIEW":
                                case "DROP TABLE":
                                    TreeNodeCollection nodes = new TreeNodeCollection();

                                    targetTree.FindAll(root, Tokens.Table, ref nodes);
                                    targetTree.FindAll(root, Tokens.View, ref nodes);
                                    foreach (TreeNode table in nodes)
                                    {
                                        table.QualifyTableName(Settings);
                                    }
                                    break;

                                case "CREATE VIEW":
                                case "CREATE TABLE":
                                    {
                                        nodes = new TreeNodeCollection();

                                        targetTree.FindAll(node, Tokens.Table, ref nodes);
                                        targetTree.FindAll(node, Tokens.View, ref nodes);
                                        foreach (TreeNode table in nodes)
                                        {
                                            table.QualifyTableName();
                                        }

                                        break;
                                    }

                                //Reverted adding ROWID at Sage's request
                                //case "CREATE VIEW":
                                //    {
                                //        //add ROWID to the list of columns, but only if there is one table in the select statement
                                //        TreeNodeCollection selects = new TreeNodeCollection();
                                //        targetTree.FindAll(root, Tokens.Select, ref selects);

                                //        bool addRowid = true;

                                //        foreach (TreeNode select in selects)
                                //        {
                                //            if (targetTree.FindAll(select, Tokens.Table) > 1)
                                //            {
                                //                addRowid = false;
                                //                break;
                                //            }
                                //        }

                                //        if (addRowid)
                                //        {
                                //            TreeNode rightParant = targetTree.FindNode(node, Tokens.RightParant);
                                //            int index;
                                //            if (rightParant != null && (index = node.Children.IndexOf(rightParant)) < node.Children.IndexOf(targetTree.FindNode(node, "AS")))
                                //            {
                                //                node.InsertChild(new TreeNode(Tokens.Comma, ","), index);
                                //                node.InsertChild(new TreeNode(Tokens.Column, "ROWID"), index + 1);
                                //            }

                                //            foreach (TreeNode select in selects)
                                //            {
                                //                TreeNode selectNode = select.Children[0];
                                //                selectNode.AddChild(new TreeNode(Tokens.Comma, ","));
                                //                selectNode.AddChild(new TreeNode(Tokens.Column, "ROWID"));

                                //                TreeNode groupByNode = targetTree.FindNode(select, "GROUP BY");
                                //                if (groupByNode != null)
                                //                {
                                //                    //if there is a GROUP BY clause then we have to add  ROWID here as well
                                //                    groupByNode.AddChild(new TreeNode(Tokens.Comma, ","));
                                //                    groupByNode.AddChild(new TreeNode(Tokens.Column, "ROWID"));
                                //                }

                                //            }
                                //        }
                                //    }
                                //    break;

                                case "DROP INDEX":
                                    {
                                        targetTree.root = new TreeNode(Tokens.Statement, "EXEC DropIndex " + root.Children[0].Children[1].NodeValue);
                                        break;
                                    }

                                case "DROP SYNONYM":
                                case "DROP PUBLIC SYNONYM":
                                    {
                                        targetTree.root = new TreeNode(Tokens.Statement, "DROP SYNONYM " + targetTree.root.Children[0].Children[1]);
                                        break;
                                    }

                                case "SELECT":
                                    {
                                        if (Settings.AddAlias)
                                        {
                                            foreach (TreeNode childNode in node.Children)
                                            {
                                                if (childNode.NodeType != Tokens.Comma &&
                                                    childNode.NodeType != Tokens.Asterisk &&
                                                    childNode.NodeValue != "ALL" &&
                                                    childNode.NodeValue != "DISTINCT" &&
                                                    targetTree.FindNode(childNode, "AS") == null
                                                    )
                                                {
                                                    string expr = childNode.ToString().Trim();
                                                    if (expr.Length <= 128 && childNode.Children.Count > 1)
                                                    {
                                                        childNode.AddChild(new TreeNode(Tokens.Keyword, "AS"));
                                                        childNode.AddChild(new TreeNode(Tokens.Correlation, GenerateAlias(expr)));
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    break;

                                case "RESTRICT":
                                    {
                                        node.NodeValue = "NO ACTION";
                                        break;
                                    }
                            }

                            break;
                        }

                    case Tokens.Column:
                        {
                            //RHE:replace SYSCOLUMNS with SYSCOLUMNSVIEW
                            if (node.NodeValue.ToUpper().StartsWith("SYSCOLUMNS."))
                            {
                                node.NodeValue = node.NodeValue.ToUpper().Replace("SYSCOLUMNS.", "SYSCOLUMNSVIEW.");
                            }
                            //surround the column with [] because the column name might be a reserved word in SqlServer
                            //table.column -> table.[column]    (the table name can be missing)
                            //PPJ:TODO:PJ:DEL -> Collation nicht berücksichtigen SQL_Latin1_General                            
                            if (node.NodeInfo != "changed" && !node.NodeValue.Contains("*") && !node.NodeValue.ToLower().Contains("collate") && !node.NodeValue.ToLower().Contains("sql_latin1_general"))
                            //if (node.NodeInfo != "changed" && !node.NodeValue.Contains("*"))
                            {
                                node.NodeValue = Regex.Replace(node.NodeValue, @"^(?<table>(.*\.))?(?<column>(.*))$", "${table}[${column}]");
                                node.NodeInfo = "changed";
                            }

                            //replace table owner
                            if (!String.IsNullOrEmpty(owner) && node.NodeValue.ToLower().StartsWith("sysadm."))
                            {
                                node.NodeValue = node.NodeValue.Remove(0, "sysadm.".Length);
                                node.NodeValue = owner + "." + node.NodeValue;
                            }

                            //for subqueries inside DELETE or UPDATE the alias has to be replaced with the table name
                            if ((targetTree.root.Children[0].NodeType == Tokens.Delete ||
                                targetTree.root.Children[0].NodeType == Tokens.Update) &&
                                node.NodeValue.Contains("."))
                            {
                                string alias = node.NodeValue.Substring(0, node.NodeValue.LastIndexOf(".")).ToLower();
                                if (alias.Contains("."))
                                {
                                    //remove the table owner
                                    alias = alias.Substring(alias.IndexOf(".") + 1);
                                }
                                TreeNode rootNode = targetTree.FindParent(root, Tokens.Select);
                                if (rootNode == null)
                                {
                                    rootNode = root;
                                }
                                TreeNodeCollection nodeAliases = new TreeNodeCollection();
                                targetTree.FindAll(rootNode, Tokens.Correlation, ref nodeAliases);
                                if (nodeAliases.Count > 0)
                                {
                                    bool foundAlias = false;
                                    foreach (TreeNode nodeAlias in nodeAliases)
                                    {
                                        if (nodeAlias.NodeInfo != "NOT USED" && nodeAlias.NodeValue.ToLower() == alias)
                                        {
                                            foundAlias = true;
                                            break;
                                        }
                                    }
                                    if (!foundAlias)
                                    {
                                        int index = node.NodeValue.IndexOf(".");
                                        if (index > -1 && alias.ToLower() == node.NodeValue.Substring(0, index).ToLower())
                                        {
                                            node.NodeValue = aliases[alias] + "." + node.NodeValue.Substring(index + 1, node.NodeValue.Length - index - 1);
                                        }
                                    }
                                }
                            }

                            node.NodeValue = GetNameWithCase(node.NodeValue);
                            break;
                        }

                    case Tokens.RelatOperator:
                        if (node.NodeValue == "=")
                        {
                            if (Settings != null && !String.IsNullOrEmpty(Settings.Collation))
                            {
                                parent = node.Parent;
                                if (parent != null && parent.NodeType == Tokens.Predicate)
                                {
                                    int index = parent.Children.IndexOf(node);
                                    if (index > 0)
                                    {
                                        TreeNode rightNode = null;
                                        TreeNode leftNode = parent.Children[index - 1];

                                        if (leftNode.NodeType == Tokens.Expression && leftNode.Children.Count == 1)
                                        {
                                            leftNode = leftNode.Children[0];
                                        }

                                        if (leftNode.NodeType == Tokens.Column && parent.Children.Count > index)
                                        {
                                            rightNode = parent.Children[index + 1];

                                            if (rightNode.NodeType == Tokens.Expression && rightNode.Children.Count == 1)
                                            {
                                                rightNode = rightNode.Children[0];
                                            }

                                            if (rightNode.NodeType == Tokens.Column)
                                            {
                                                TransformTreeSqlBase_MsSql(ref targetTree, root, rightNode);
                                                Column leftColInfo = GetColumnInfo(leftNode, targetTree, root);
                                                Column rightColInfo = GetColumnInfo(rightNode, targetTree, root);
                                                if (leftColInfo != null && rightColInfo != null && leftColInfo.Collation != rightColInfo.Collation)
                                                {
                                                    if (leftColInfo.Collation != String.Empty && leftColInfo.Collation != Settings.Collation)
                                                    {
                                                        leftNode.NodeValue += " COLLATE " + Settings.Collation;
                                                    }

                                                    if (rightColInfo.Collation != String.Empty && rightColInfo.Collation != Settings.Collation)
                                                    {
                                                        rightNode.NodeValue += " COLLATE " + Settings.Collation;
                                                    }
                                                }

                                            }
                                        }
                                    }
                                }
                            }
                        }
                        break;

                    case Tokens.CheckDatabase:
                        {
                            if (node.ToString().ToUpper().Contains("SYSTEM"))
                            {
                                throw new NotSupportedException("The SYSTEM ONLY clause for CHECK DATABASE is not supported by the SqlTranslator");
                            }
                            else
                            {
                                node.Children[0].NodeValue = "DBCC CHECKDB WITH NO_INFOMSGS";
                            }
                            break;
                        }

                    case Tokens.Correlation:
                        {
                            //Aliases are not allowed in UPDATE and DELETE statements
                            if (targetTree.FindNode(root, Tokens.Delete) != null ||
                                targetTree.FindNode(root, Tokens.Update) != null)
                            {
                                node.NodeInfo = "NOT USED";
                            }

                            node.NodeValue = GetNameWithCase(node.NodeValue);
                            break;
                        }

                    case Tokens.StringConst:
                    case Tokens.DatetimeConst:
                        {
                            //In SqlServer setting a column to '' will result in the default value instead of NULL
                            //if (node.NodeValue == "''")
                            //{
                            //    node.NodeValue = "NULL";
                            //    break;
                            //}

                            CustomDateTime datetime;

                            if (IsValidDateTime(node.NodeValue.Substring(1, node.NodeValue.Length - 2), out datetime))
                            {
                                //find the corresponding column
                                TreeNode columnNode = null, tableNode, parentNode = null;
                                int index = 0;
                                if (root.Children[0].NodeType == Tokens.Insert)
                                {
                                    if (Settings != null && Settings.DbStructure != null)
                                    {
                                        index = Convert.ToInt32(node.NodeInfo);
                                        TreeNodeCollection columnNodes = new TreeNodeCollection();
                                        targetTree.FindAll(root, Tokens.Column, ref columnNodes);
                                        tableNode = targetTree.FindNode(root, Tokens.Table);
                                        if (columnNodes.Count > 0)
                                        {
                                            columnNode = columnNodes[index];
                                            // string datatype = Settings.DbStructure[columnNode.GetUnqualifiedValue(), Settings.Schemas.FindTable(table.GetSchema(Settings), table.OriginalNodeValue).Name];
                                            //if (Settings.DbStructure[columnNode.NodeValue, tableNode.NodeValue].In("datetime", "datetime2", "date", "time"))
                                            if (Settings.DbStructure[columnNode.NodeValue, Settings.Schemas.FindTable(tableNode.GetSchema(Settings), tableNode.NodeValue)?.Name].In("datetime", "datetime2", "date", "time"))
                                            {
                                                IsValidDateTime(node.NodeValue, out datetime);
                                                node.NodeValue = "'" + datetime.ToString() + "'";
                                            }
                                        }
                                        else
                                        {
                                            if (Settings.DbStructure[index, Settings.Schemas.FindTable(tableNode.GetSchema(Settings), tableNode.NodeValue)?.Name].In("datetime", "datetime2", "date", "time"))
                                            {
                                                IsValidDateTime(node.NodeValue, out datetime);
                                                node.NodeValue = "'" + datetime.ToString() + "'";
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    parentNode = targetTree.FindParent(node, Tokens.Predicate);
                                    if (parentNode != null)
                                    {
                                        columnNode = targetTree.FindNode(parentNode, Tokens.Column);
                                    }
                                    else
                                    {
                                        parentNode = targetTree.FindParent(node, "SET");
                                        if (parentNode != null) //UPDATE
                                        {
                                            for (int i = 0; i < parentNode.Children.Count; i++)
                                            {
                                                if (targetTree.FindNode(parentNode.Children[i], node))
                                                {
                                                    index = i;
                                                    break;
                                                }
                                            }
                                            if (index > 0)
                                            {
                                                columnNode = parentNode.Children[index - 2];
                                            }
                                        }
                                    }
                                    if (columnNode != null && !parentNode.ToString().Contains("DATETOCHAR"))
                                    {
                                        //only translate to datetime when the column is a date type and it's not converted with DATETOCHAR
                                        if (node.NodeType == Tokens.DatetimeConst)
                                        {
                                            IsValidDateTime(node.NodeValue, out datetime);
                                            node.NodeValue = "'" + datetime.ToString() + "'";
                                        }
                                        else if (Settings != null && Settings.DbStructure != null)
                                        {
                                            TreeNodeCollection tables = new TreeNodeCollection();
                                            targetTree.FindAll(root, Tokens.Table, ref tables);
                                            tableNode = GetTable(columnNode, tables);
                                            if (tableNode != null)
                                            {
                                                string column = columnNode.NodeValue;
                                                if (column.Contains("."))
                                                {
                                                    column = column.Substring(column.LastIndexOf(".") + 1);
                                                }
                                                if (Settings.DbStructure[column, Settings.Schemas.FindTable(tableNode.GetSchema(Settings), tableNode.NodeValue)?.Name].In("datetime", "datetime2", "date", "time"))
                                                {
                                                    node.NodeValue = "'" + datetime.ToString() + "'";
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (Settings != null && Settings.Unicode)
                                {
                                    node.NodeValue = "N" + node.NodeValue;
                                }
                            }
                            //PPJ:FINAL:PJ:#i68 Check if Parent is null
                            if (node.NodeValue == "''" && ((node.Parent == null ||
                                node.Parent.NodeType == Tokens.Keyword && node.Parent.NodeValue == "VALUES") ||
                                targetTree.FindParent(node, "SET") != null))
                            {
                                //RHE: to check
                                if (node.Parent != null)
                                    node.NodeValue = "NULL";
                            }
                            break;
                        }

                    //Used in DROP statements
                    case Tokens.Name:
                        if (!String.IsNullOrEmpty(owner) && node.NodeValue.ToLower().StartsWith("sysadm."))
                        {
                            node.NodeValue = node.NodeValue.Remove(0, "sysadm.".Length);
                            node.NodeValue = owner + "." + node.NodeValue;
                        }
                        break;

                    case Tokens.Identifier:
                        node.NodeValue = GetNameWithCase(node.NodeValue);
                        break;

                    case Tokens.DataType:
                        if (Settings.Collation != String.Empty)
                        {
                            string dataType = node.Children[0].NodeValue;
                            if (dataType.In("CHAR", "VARCHAR", "TEXT", "NCHAR", "NVARCHAR", "NTEXT"))
                            {
                                TreeNode temp = new TreeNode(Tokens.Keyword, "COLLATE " + Settings.Collation, "SKIP");
                                node.Children.Add(temp);
                            }
                        }
                        break;

                    case Tokens.BindVariable:
                        if (Settings.ReadStructure && (Settings.ConvertDateValues || root.Children[0].NodeType == Tokens.Insert))
                        {
                            TreeNode table = targetTree.FindNode(root, Tokens.Table);
                            string columnName = "";
                            if (targetTree.FindNode(root, Tokens.Insert) != null)
                            {
                                TreeNodeCollection columns = new TreeNodeCollection();
                                targetTree.FindAll(root, Tokens.Column, ref columns);
                                int index = 0;
                                if (int.TryParse(node.NodeInfo, out index))
                                {
                                    if (columns.Count > index && index >= 0)
                                    {
                                        columnName = columns[index].NodeValue;
                                    }
                                }
                            }
                            else
                            {
                                TreeNode predicate = node.Parent.Parent;
                                //e.g. DATE = :dt1
                                if (predicate.NodeType == Tokens.Predicate)
                                {
                                    TreeNode temp = targetTree.FindNode(predicate, Tokens.Column);
                                    if (temp != null)
                                    {
                                        columnName = temp.NodeValue;
                                    }
                                }
                            }
                            if (!String.IsNullOrEmpty(columnName))
                            {
                                //string dataType = Settings.DbStructure[GetColumnName(columnName.RemoveBrackets()), table.NodeInfo];
                                string dataType = Settings.DbStructure[GetColumnName(columnName.RemoveBrackets()), Settings.Schemas.FindTable(table.GetSchema(Settings), table.NodeInfo)?.Name];
                                switch (dataType)
                                {
                                    case "date":
                                        if (Settings.ConvertDateValues)
                                        {
                                            node.NodeValue = "convert(datetime, " + node.NodeValue + ", 120)";
                                        }
                                        break;

                                    case "varchar":
                                    case "nvarchar":
                                        bool replaceBindVar = false;
                                        if (root.Children[0].NodeType == Tokens.Insert && node.Parent.NodeType == Tokens.Keyword && node.Parent.NodeValue.StartsWith("VALUES"))
                                        {
                                            replaceBindVar = true;
                                        }

                                        if (replaceBindVar)
                                        {
                                            node.NodeValue = String.Format("CASE WHEN {0} LIKE '' THEN null ELSE {0} END", node.NodeValue);
                                        }
                                        break;

                                    //PPJ:FINAL:BBE:#26:#88 - Add missing dataType case for time, if value is '' then null should be written to db. Previously, if the string was empty '' 00:00:00 was written instead of null.
                                    case "time":
                                        bool replaceBindVarTime = false;
                                        if (root.Children[0].NodeType == Tokens.Insert && node.Parent.NodeType == Tokens.Keyword && node.Parent.NodeValue.StartsWith("VALUES"))
                                        {
                                            replaceBindVarTime = true;
                                        }

                                        if (replaceBindVarTime)
                                        {
                                            node.NodeValue = String.Format("CASE WHEN {0} LIKE '' THEN null ELSE {0} END", node.NodeValue);
                                        }
                                        break;

                                }

                            }
                        }
                        break;

                    case Tokens.MathOperator:
                        //When dividing 2 integers, in SqlServer the result will be also an integer
                        //Therefore one of the operands has to be casted to float
                        //e.g 30/1440 -> cast(30 as float)/1440
                        switch (node.NodeValue)
                        {
                            case "/":
                                TreeNode expr = targetTree.FindParent(root, node);
                                if (expr.Children.Count == 3 &&
                                    expr.Children[0].NodeType == Tokens.NumericConst &&
                                    expr.Children[2].NodeType == Tokens.NumericConst)
                                {
                                    expr.Children[0].NodeValue = String.Format("cast({0} as float)", expr.Children[0].NodeValue);
                                }
                                break;

                            //RHE: handling "+" operator for date arithmetic 
                            case "+":
                            case "-":
                                if (Settings.DbStructure != null)
                                {
                                    parent = node.Parent;
                                    //RHE: break if parent is null
                                    if (parent != null)
                                    {
                                        var leftNode = parent.Children[0];
                                        if (leftNode.NodeValue == "-" || leftNode.NodeValue == "+")
                                        {
                                            break;
                                        }
                                        var rightNode = parent.Children[2];
                                        //TransformTreeSqlBase_MsSql(ref targetTree, root, rightNode);
                                        if (IsDateNode(leftNode, targetTree, root) || IsDateNode(rightNode, targetTree, root))
                                        {
                                            //FC:RHE: decide between DATEADD and DATEDIFF
                                            if (IsDateNode(rightNode, targetTree, root))
                                            {
                                                //BCU - we need to encapsulate the DATEDIFF node in a expression or the Group By implementation
                                                //wont work correctly when replacing numeric constants in the group by clause
                                                TreeNode dateDiff = new TreeNode(Tokens.Expression);
                                                //RHE: use DATEDIFF
                                                dateDiff.AddChild(new TreeNode(Tokens.Function));
                                                dateDiff.AddChild(new TreeNode(Tokens.Identifier, "DATEDIFF"));
                                                dateDiff.AddChild(new TreeNode(Tokens.LeftParant, "("));
                                                dateDiff.AddChild(new TreeNode(Tokens.Identifier, "DAY"));
                                                dateDiff.AddChild(new TreeNode(Tokens.RightParant, ","));
                                                if (node.NodeValue == "-")
                                                    dateDiff.AddChild(rightNode);
                                                else
                                                    dateDiff.AddChild(leftNode);
                                                dateDiff.AddChild(new TreeNode(Tokens.Comma, ","));
                                                if (node.NodeValue == "-")
                                                    dateDiff.AddChild(leftNode);
                                                else
                                                    dateDiff.AddChild(rightNode);
                                                dateDiff.AddChild(new TreeNode(Tokens.RightParant, ")"));
                                                int index = parent.Parent.Children.IndexOf(parent);
                                                parent = parent.Parent;
                                                parent.RemoveChild(index);
                                                parent.InsertChild(dateDiff, index);
                                            }
                                            else
                                            {
                                                //RHE: use DATEADD
                                                // check if expresiion contains keyword like DAYS, MINUTES etc.
                                                TreeNode keywordNode = targetTree.FindNode(parent, Tokens.SysKeyword);
                                                if (keywordNode != null)
                                                {
                                                    break;
                                                }
                                                else
                                                {
                                                    // add default DAYS if nothing is specified
                                                    parent.AddChild(new TreeNode(Tokens.SysKeyword, "DAYS"));
                                                }
                                            }
                                        }
                                    }
                                }
                                break;
                        }
                        break;

                    case Tokens.StoreCommand:
                        {
                            //Stored commands are translated as stored procedures:
                            //
                            //CREATE PROCEDURE <Procedure_Name> 
                            //    <@Param1> <Datatype> = <Default_Value_For_Param1>, 
                            //    <@Param2> <Datatype> = <Default_Value_For_Param2>
                            //AS
                            //BEGIN
                            //    statement [bind variables]
                            //END
                            //
                            //For each bind variable we have to create a parameter and determine its type from the statement
                            if (Settings.DbStructure != null)
                            {
                                node.NodeValue = "CREATE PROCEDURE";
                                string commandName = node.Children[0].NodeValue.ToUpper();
                                if (!String.IsNullOrEmpty(owner) && commandName.StartsWith("SYSADM."))
                                {
                                    node.Children[0].NodeValue = commandName.Replace("SYSADM.", owner + ".");
                                }
                                node.Children.Insert(new TreeNode(Tokens.KeyName, "AS"), 1);
                                node.Children.Insert(new TreeNode(Tokens.Keyword, "BEGIN"), 2);
                                node.AddChild(new TreeNode(Tokens.Keyword, "END"));

                                StringNumberComparer comparer = new StringNumberComparer();
                                SortedDictionary<string, string> parameters = new SortedDictionary<string, string>(comparer);
                                TreeNodeCollection bindVariables = new TreeNodeCollection();
                                targetTree.FindAll(node, Tokens.BindVariable, ref bindVariables);


                                if (node.Children[3].NodeType == Tokens.Select ||
                                    node.Children[3].NodeType == Tokens.Update ||
                                    node.Children[3].NodeType == Tokens.Delete)
                                {
                                    TreeNodeCollection tables = new TreeNodeCollection();
                                    targetTree.FindAll(node, Tokens.Table, ref tables);

                                    foreach (TreeNode bindVariable in bindVariables)
                                    {
                                        bindVariable.NodeValue = bindVariable.NodeValue.Replace(":", "@param");

                                        TreeNode predicate = targetTree.FindParent(bindVariable, Tokens.Predicate);
                                        //Determine the datatype of the expression on the left
                                        string dataType = String.Empty;
                                        TreeNodeCollection columns = new TreeNodeCollection();
                                        TreeNode function = targetTree.FindNode(predicate, Tokens.Function);
                                        if (function != null)
                                        {
                                            switch (function.NodeValue)
                                            {
                                                case "@DATE": dataType = "date"; break;
                                                case "@TIME": dataType = "datetime2"; break;
                                                case "@DATEVALUE": dataType = "datetime2"; break;
                                            }
                                        }
                                        if (dataType == String.Empty)
                                        {
                                            targetTree.FindAll(predicate, Tokens.Column, ref columns);

                                            List<string> dataTypes = new List<string>();
                                            foreach (TreeNode column in columns)
                                            {
                                                //dataTypes.Add(Settings.DbStructure[GetColumnName(column.NodeValue), GetTable(column, tables).NodeInfo]);
                                                var table1 = GetTable(column, tables);
                                                dataTypes.Add(Settings.DbStructure[GetColumnName(column.NodeValue), Settings.Schemas.FindTable(table1.GetSchema(Settings), table1.NodeInfo)?.Name]);
                                            }
                                            if (dataTypes.Count == 1)
                                            {
                                                dataType = dataTypes[0];
                                            }
                                            else if (dataTypes.Count == 2)
                                            {
                                                if (dataTypes[0].In("date", "datetime", "datetime2") && dataTypes[1].In("date", "datetime", "datetime2"))
                                                {
                                                    dataType = "int";
                                                }
                                                else if (dataTypes[0].In("date", "datetime", "datetime2") && dataTypes[1].In("int"))
                                                {
                                                    dataType = "datetime2";
                                                }
                                            }
                                        }
                                        if (!parameters.ContainsKey(bindVariable.NodeValue))
                                        {
                                            parameters.Add(bindVariable.NodeValue, dataType);
                                        }
                                    }
                                }
                                else if (node.Children[3].NodeType == Tokens.Insert)
                                {
                                    TreeNode table = targetTree.FindNode(node, Tokens.Table);
                                    TreeNodeCollection columnNodes = new TreeNodeCollection();
                                    targetTree.FindAll(node, Tokens.Column, ref columnNodes);

                                    List<Column> columns = null;
                                    if (columnNodes.Count == 0)
                                    {
                                        columns = Settings.DbStructure.GetAllColumns(table.NodeValue);
                                        //Create column list except for ROWID
                                        TreeNode insertNode = node.Children[3];
                                        insertNode.InsertChild(new TreeNode(Tokens.LeftParant, "("), 1);
                                        int i = 2;
                                        foreach (Column col in columns)
                                        {
                                            if (col.Name.ToUpper() != "ROWID")
                                            {
                                                if (i != 2)
                                                {
                                                    insertNode.InsertChild(new TreeNode(Tokens.Comma, ","), i++);
                                                }
                                                insertNode.InsertChild(new TreeNode(Tokens.Column, col.Name), i++);
                                            }
                                        }
                                        insertNode.InsertChild(new TreeNode(Tokens.RightParant, ")"), i);
                                    }
                                    string dataType = String.Empty;
                                    foreach (TreeNode bindVariable in bindVariables)
                                    {
                                        bindVariable.NodeValue = bindVariable.NodeValue.Replace(":", "@param");
                                        int bindIndex = Convert.ToInt32(bindVariable.NodeInfo);
                                        if (columnNodes.Count > 0)
                                        {
                                            //dataType = Settings.DbStructure[GetColumnName(columnNodes[bindIndex].NodeValue), table.NodeInfo];
                                            dataType = Settings.DbStructure[GetColumnName(columnNodes[bindIndex].NodeValue), Settings.Schemas.FindTable(table.GetSchema(Settings), table.NodeInfo)?.Name];
                                        }
                                        else
                                        {
                                            dataType = columns[bindIndex].Type;
                                        }
                                        if (!parameters.ContainsKey(bindVariable.NodeValue))
                                        {
                                            parameters.Add(bindVariable.NodeValue, dataType);
                                        }
                                    }
                                }

                                int index = 1;
                                foreach (string parameter in parameters.Keys)
                                {
                                    if (index > 1)
                                    {
                                        node.Children.Insert(new TreeNode(Tokens.Comma, ","), index++);
                                    }
                                    node.Children.Insert(new TreeNode(Tokens.Variable, parameter), index);
                                    node.Children.Insert(new TreeNode(Tokens.DataType, parameters[parameter].ToString()), index + 1);
                                    index += 2;
                                }
                            }
                            else
                            {
                                throw new NotSupportedException("Can't translate STORE commands without reading the database structure");
                            }
                            break;
                        }
                }
                for (int i = 0; i < node.Children.Count; i++)
                    TransformTreeSqlBase_MsSql(ref targetTree, root, node.Children[i]);

            }
        }

        private Column GetColumnInfo(TreeNode columnNode, Tree targetTree, TreeNode root)
        {
            TreeNodeCollection tables = new TreeNodeCollection();
            targetTree.FindAll(root, Tokens.Table, ref tables);
            TreeNode tableNode = GetTable(columnNode, tables);
            if (tableNode != null)
            {
                string column = columnNode.GetUnqualifiedValue().ToUpper();
                string tableName = tableNode.GetUnqualifiedValue().ToUpper();

                if (Settings != null && Settings.DbStructure != null)
                {
                    return Settings.DbStructure.OfType<Column>().Where(c => c.Table.ToUpper() == tableName && c.Name.ToUpper() == column).FirstOrDefault();
                }
            }

            return null;
        }

        private bool ConvertNodeToVarchar(TreeNode node)
        {
            if (node == null)
            {
                return false;
            }

            return (node.NodeType == Tokens.Column || node.NodeType == Tokens.Function) && !node.NodeValue.Contains("CONVERT");
        }

        private int manyWithOneReplaced = 0;
        private void TransformTreeSqlBase_Oracle(ref Tree targetTree, TreeNode root, TreeNode node)
        {
            if (node.NodeType == Tokens.Select)
                root = node;

            if (node.NodeInfo != "SKIP")
            {

                //Transformations for current node
                switch (node.NodeType)
                {
                    case Tokens.Table:
                        {
                            //for Oracle it's not needed to remove SYSADM
                            //if (node.NodeValue.ToLower().StartsWith("sysadm."))
                            //{
                            //    node.NodeValue = node.NodeValue.Remove(0, "sysadm.".Length);

                            //    //TODO: Surrounding columns with "" is not working
                            //    //If the table name is a reserved word, surround it with ""
                            //    //if (LexicalAnalyzer.IsKeyword(node.NodeValue))
                            //    //{
                            //    //    node.NodeValue = "\"" + node.NodeValue + "\"";
                            //    //}
                            //    if (!String.IsNullOrEmpty(owner))
                            //    {
                            //        node.NodeValue = owner + "." + node.NodeValue;
                            //    }
                            //}

                            // "#" is not a valid character for starting an identifier
                            if (node.NodeValue.StartsWith("#"))
                            {
                                node.NodeValue = "temp_" + node.NodeValue.Substring(1);
                            }

                            //Change the table name to view when there are external long columns
                            if (root.NodeType == Tokens.Select && Globals.TableInformation.IsInitialized)
                            {
                                if (this.HasExternalColumns(GetTableName(node.NodeValue)))
                                {
                                    node.NodeValue += "_V";
                                }
                            }
                            break;
                        }

                    case Tokens.Function: //Replace function name with the corresponding function from the functions table
                        {
                            #region Functions
                            switch (node.NodeValue.ToUpper())
                            {
                                //Functions @CHOOSE uses a variant number of parameters so 
                                //the best thing to do is to implement it here using the CASE function

                                //@CHOOSE (selector number, value 0, value 1, ..., value n)
                                case "@CHOOSE":
                                    {
                                        //Replace function name with "CASE"
                                        node.NodeValue = "CASE";

                                        node.RemoveChild(0); //Remove left paranthesis
                                        TreeNode selectorNode = node.Children[0];
                                        node.RemoveChild(0);
                                        int j = 0;
                                        int selectorIndex = 0;
                                        string relatOp = String.Empty;
                                        int valuesCount = node.Children.Count / 2;

                                        TreeNode currentNode = node.Children[j];
                                        while (currentNode.NodeType == Tokens.Comma)
                                        {
                                            //Remove comma
                                            node.RemoveChild(currentNode);

                                            //Extract value0, value1,...
                                            currentNode = node.Children[j]; //expression

                                            //Create the WHEN argument
                                            currentNode.InsertChild(new TreeNode(Tokens.Keyword, "WHEN"), 0);
                                            currentNode.InsertChild(selectorNode, 1);
                                            if (selectorIndex == 0)
                                            {
                                                relatOp = "<=";
                                            }
                                            else if (selectorIndex == valuesCount - 1)
                                            {
                                                relatOp = ">=";
                                            }
                                            else
                                            {
                                                relatOp = "=";
                                            }
                                            currentNode.InsertChild(new TreeNode(Tokens.RelatOperator, relatOp), 2);
                                            currentNode.InsertChild(new TreeNode(Tokens.NumericConst, selectorIndex.ToString()), 3);
                                            currentNode.InsertChild(new TreeNode(Tokens.Keyword, "THEN"), 4);

                                            j++;
                                            selectorIndex++;
                                            currentNode = node.Children[j];
                                        }

                                        node.RemoveChild(j); //Remove right paranthesis

                                        //Add "END" keyword
                                        node.AddChild(new TreeNode(Tokens.Keyword, "END"));
                                        break;
                                    }

                                //@IF(number, value1, value2)
                                case "@IF":
                                    {
                                        //CASE number WHEN 0 THEN value2  ELSE value1 END
                                        //OR
                                        //CASE WHEN number THEN value1 ELSE value2 END  (when number returns a boolean value, in case of @ISNA)
                                        //Replace function name with "CASE"
                                        bool castWasUsed = false;
                                        node.NodeValue = "CASE";
                                        node.RemoveChild(0); //Remove left paranthesis

                                        TreeNode conditionNode = node.Children[0];
                                        TreeNode valueNode1 = node.Children[2];
                                        TreeNode valueNode2 = node.Children[4];

                                        string dataType = "";

                                        if (Settings != null && Settings.DbStructure != null && Settings.DbStructure.IsInitialized &&
                                                                           (
                                                                           ((valueNode1.Children[0].NodeType == Tokens.BindVariable && valueNode2.Children[0].NodeType == Tokens.Column) ||
                                                                           (valueNode2.Children[0].NodeType == Tokens.BindVariable && valueNode1.Children[0].NodeType == Tokens.Column)) ||
                                                                           ((valueNode1.Children[0].NodeType == Tokens.BindVariable && valueNode2.Children[0].NodeType == Tokens.NumericConst) ||
                                                                           (valueNode2.Children[0].NodeType == Tokens.BindVariable && valueNode1.Children[0].NodeType == Tokens.NumericConst)) ||
                                                                           ((valueNode1.Children[0].NodeType == Tokens.Column && valueNode2.Children[0].NodeType == Tokens.StringConst) ||
                                                                           (valueNode2.Children[0].NodeType == Tokens.Column && valueNode1.Children[0].NodeType == Tokens.StringConst)) ||
                                                                           (((valueNode2.Children[0].NodeValue == "SYSDATE" || valueNode2.Children[0].NodeValue == "SYSTIMESTAMP" || valueNode1.Children[0].NodeValue == "SYSDATETIME" || valueNode2.Children[0].NodeValue == "@NOW")
                                                                           && (valueNode1.Children[0].NodeType == Tokens.StringConst || valueNode1.Children[0].NodeType == Tokens.DatetimeConst)) ||
                                                                           ((valueNode2.Children[0].NodeValue == "SYSDATE" || valueNode2.Children[0].NodeValue == "SYSTIMESTAMP" || valueNode1.Children[0].NodeValue == "SYSDATETIME" || valueNode2.Children[0].NodeValue == "@NOW")
                                                                           && (valueNode1.Children[0].NodeType == Tokens.Column)) ||
                                                                           ((valueNode2.Children[0].NodeValue == "SYSDATE" || valueNode2.Children[0].NodeValue == "SYSTIMESTAMP" || valueNode1.Children[0].NodeValue == "SYSDATETIME" || valueNode2.Children[0].NodeValue == "@NOW")
                                                                           && (valueNode1.Children[0].NodeType == Tokens.BindVariable))) ||
                                                                           (((valueNode1.Children[0].NodeValue == "SYSDATE" || valueNode1.Children[0].NodeValue == "SYSTIMESTAMP" || valueNode1.Children[0].NodeValue == "SYSDATETIME" || valueNode1.Children[0].NodeValue == "@NOW")
                                                                           && (valueNode2.Children[0].NodeType == Tokens.StringConst || valueNode2.Children[0].NodeType == Tokens.DatetimeConst)) ||
                                                                           ((valueNode1.Children[0].NodeValue == "SYSDATE" || valueNode1.Children[0].NodeValue == "SYSTIMESTAMP" || valueNode1.Children[0].NodeValue == "SYSDATETIME" || valueNode1.Children[0].NodeValue == "@NOW")
                                                                           && (valueNode2.Children[0].NodeType == Tokens.Column)) ||
                                                                           ((valueNode1.Children[0].NodeValue == "SYSDATE" || valueNode1.Children[0].NodeValue == "SYSTIMESTAMP" || valueNode1.Children[0].NodeValue == "SYSDATETIME" || valueNode1.Children[0].NodeValue == "@NOW")
                                                                           && (valueNode2.Children[0].NodeType == Tokens.BindVariable))) ||
                                                                           (valueNode1.Children[0].NodeValue == "NULL" && valueNode2.Children[0].NodeValue == "NULL")
                                                                           ))
                                        {
                                            string colName = "";
                                            bool numeric = false;
                                            TreeNode bindNode = new TreeNode(Tokens.BindVariable);

                                            if (valueNode1.Children[0].NodeType == Tokens.Column)
                                            {
                                                colName = valueNode1.Children[0].NodeValue;
                                                bindNode = valueNode2;
                                            }
                                            else if (valueNode2.Children[0].NodeType == Tokens.Column)
                                            {
                                                colName = valueNode2.Children[0].NodeValue;
                                                bindNode = valueNode1;
                                            }
                                            else if (valueNode1.Children[0].NodeType == Tokens.NumericConst)
                                            {
                                                colName = "";
                                                numeric = true;
                                                bindNode = valueNode2;
                                            }
                                            else if (valueNode2.Children[0].NodeType == Tokens.NumericConst)
                                            {
                                                colName = "";
                                                numeric = true;
                                                bindNode = valueNode1;
                                            }

                                            if (string.IsNullOrEmpty(colName) && numeric)
                                            {
                                                bindNode.Children[0].NodeValue = "TO_NUMBER(" + bindNode.Children[0].NodeValue + ")";
                                            }
                                            else if (this.GetColumnDataType(colName, ref dataType))
                                            {

                                                if (!string.IsNullOrEmpty(dataType))
                                                {
                                                    if (dataType.StartsWith("TIMESTAMP"))
                                                    {
                                                        dataType = "TIMESTAMP";
                                                    }
                                                    node.NodeValue = "CAST(" + node.NodeValue;
                                                    castWasUsed = true;
                                                    switch (dataType)
                                                    {
                                                        case "NUMBER":
                                                            {
                                                                bindNode.Children[0].NodeValue = "TO_NUMBER(" + bindNode.Children[0].NodeValue + ")";
                                                                break;
                                                            }
                                                        case "CHAR":
                                                            {
                                                                bindNode.Children[0].NodeValue = "TO_CHAR(" + bindNode.Children[0].NodeValue + ")";
                                                                break;
                                                            }
                                                        case "DATE":
                                                            {
                                                                bool isDate = false;
                                                                bool isTime = false;
                                                                bool isTimestamp = false;
                                                                int milliseconds = 0;
                                                                this.GetColumnDateTimeProperties(colName, out isDate, out isTime, out isTimestamp, out milliseconds);
                                                                if (isTimestamp && milliseconds != 0)
                                                                {
                                                                    if (isTimestamp)
                                                                    {
                                                                        string format = "'YYYY-MM-DD HH24:MI:SS";
                                                                        if (milliseconds > 0)
                                                                        {
                                                                            format += ":FF" + milliseconds;
                                                                        }
                                                                        format += "'";
                                                                        if (bindNode.Children[0].NodeValue == "SISDATETIME"
                                                                            || bindNode.Children[0].NodeValue == "SYSDATETIME"
                                                                            || bindNode.Children[0].NodeValue == "SYSTIME"
                                                                            || bindNode.Children[0].NodeValue == "@NOW")
                                                                        {
                                                                            bindNode.Children[0].NodeValue = "SYSTIMESTAMP";
                                                                        }
                                                                        else
                                                                        {
                                                                            bindNode.Children[0].NodeValue = String.Format("TO_TIMESTAMP(" + bindNode.Children[0].NodeValue + ", {0})", format);
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        if (bindNode.Children[0].NodeValue == "SISDATETIME"
                                                                            || bindNode.Children[0].NodeValue == "SYSDATETIME"
                                                                            || bindNode.Children[0].NodeValue == "SYSTIME"
                                                                            || bindNode.Children[0].NodeValue == "@NOW")
                                                                        {
                                                                            bindNode.Children[0].NodeValue = "SYSDATE";
                                                                        }
                                                                        bindNode.Children[0].NodeValue = String.Format("TO_DATE(TO_CHAR(TO_TIMESTAMP(" + bindNode.Children[0].NodeValue + ", 'YYYY-MM-DD HH24:MI:SS.FF'), {0}), {0})", Settings.DateFormat != null ? Settings.DateFormat : "'DD.MM.YYYY'");
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    if (bindNode.ToString().ToUpper().Trim() != "@NOW" &&
                                                                        bindNode.ToString().ToUpper().Trim() != "SYSDATE" &&
                                                                        bindNode.ToString().ToUpper().Trim() != "SYSTIMESTAMP" &&
                                                                        bindNode.ToString().ToUpper().Trim() != "SYSDATETIME")
                                                                    {
                                                                        if (bindNode.Children.Count > 0)
                                                                        {
                                                                            bindNode.Children.Insert(new TreeNode(Tokens.Function, "TO_DATE"), 0);
                                                                            bindNode.Children.Insert(new TreeNode(Tokens.LeftParant, "("), 1);
                                                                            bindNode.Children.Add(new TreeNode(Tokens.RightParant, ")"));
                                                                        }
                                                                        else
                                                                        {
                                                                            bindNode.NodeValue = "TO_DATE(" + bindNode.Children[0].NodeValue + ")";
                                                                        }
                                                                    }
                                                                }
                                                                break;
                                                            }
                                                        case "TIMESTAMP":
                                                            {
                                                                bool isDate = false;
                                                                bool isTime = false;
                                                                bool isTimestamp = false;
                                                                int milliseconds = 0;
                                                                this.GetColumnDateTimeProperties(colName, out isDate, out isTime, out isTimestamp, out milliseconds);
                                                                if (isTimestamp)
                                                                {
                                                                    string format = "'YYYY-MM-DD HH24:MI:SS";
                                                                    if (milliseconds > 0)
                                                                    {
                                                                        format += ":FF" + milliseconds;
                                                                    }
                                                                    format += "'";

                                                                    if (bindNode.Children[0].NodeValue == "SISDATETIME"
                                                                        || bindNode.Children[0].NodeValue == "SYSDATETIME"
                                                                        || bindNode.Children[0].NodeValue == "SYSTIME"
                                                                        || bindNode.Children[0].NodeValue == "@NOW")
                                                                    {
                                                                        bindNode.Children[0].NodeValue = "SYSTIMESTAMP";
                                                                        if (root.NodeType == Tokens.Select || root.Children[0].NodeType == Tokens.Select)
                                                                        {
                                                                            if (bindNode.Children.Count > 1)
                                                                            {
                                                                                bindNode.Children.Insert(new TreeNode(Tokens.Function, "TO_CHAR"), 0);
                                                                                bindNode.Children.Insert(new TreeNode(Tokens.LeftParant, "("), 1);
                                                                                bindNode.Children.Insert(new TreeNode(Tokens.Function, "TO_TIMESTAMP"), 2);
                                                                                bindNode.Children.Insert(new TreeNode(Tokens.LeftParant, "("), 3);
                                                                                bindNode.Children.Add(new TreeNode(Tokens.RightParant, ")"));
                                                                                bindNode.Children.Add(new TreeNode(Tokens.Comma, ","));
                                                                                bindNode.Children.Add(new TreeNode(Tokens.StringConst, format));
                                                                                bindNode.Children.Add(new TreeNode(Tokens.RightParant, ")"));
                                                                            }
                                                                            else
                                                                            {
                                                                                bindNode.Children[0].NodeValue = "TO_CHAR(" + bindNode.Children[0].NodeValue + "," + format + ")";
                                                                            }
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        if (root.NodeType == Tokens.Select || root.Children[0].NodeType == Tokens.Select)
                                                                        {
                                                                            //if (bindNode.Children[0].NodeType == Tokens.StringConst || bindNode.Children[0].NodeType == Tokens.DatetimeConst)
                                                                            //{
                                                                            //    CustomDateTime bindNodeDateTime = new CustomDateTime();
                                                                            //    if (this.IsValidDateTime(bindNode.Children[0].NodeValue.Replace("'", ""), out bindNodeDateTime))
                                                                            //    {
                                                                            //        bindNode.Children[0].NodeValue = String.Format("TO_TIMESTAMP(" + bindNode.Children[0].NodeValue + ", {0})", format);
                                                                            //    }
                                                                            //}

                                                                            bindNode.Children[0].NodeValue = String.Format("TO_CHAR(TO_TIMESTAMP(" + bindNode.Children[0].NodeValue + ", {0}), {0})", format);
                                                                        }
                                                                        else
                                                                        {
                                                                            bindNode.Children[0].NodeValue = String.Format("TO_TIMESTAMP(" + bindNode.Children[0].NodeValue + ", {0})", format);
                                                                        }
                                                                    }

                                                                    node.NodeValue = "CASE";
                                                                    castWasUsed = false;
                                                                }
                                                                break;
                                                            }
                                                    }
                                                }
                                            }

                                            //@IF( @ISNA( PIPERSONAL.PS_AUSNGENVER2),
                                            //@IF( @ISNA( PIPERSONAL.PS_AUSNGENVERL1),
                                            //@IF( @ISNA( PIPERSONAL.PS_AUSNAHMEGEN), NULL, NULL), -> in such cases we have to convert to the datatype
                                            //PIPERSONAL.PS_AUSNAHMEGEN),                          <- of the following column
                                            //PIPERSONAL.PS_AUSNGENVERL1 )
                                            if (valueNode1.Children[0].NodeValue == "NULL" && valueNode2.Children[0].NodeValue == "NULL")
                                            {
                                                if (node.Parent != null && node.Parent.Parent != null && node.Parent.Parent.Parent != null)
                                                {
                                                    TreeNode parentNode = node.Parent.Parent.Parent;
                                                    if (parentNode.NodeValue == "CASE" && parentNode.Children.Count > 1)
                                                    {
                                                        TreeNode columnNode = targetTree.FindNode(parentNode.Children[1], Tokens.Column);
                                                        this.GetColumnDataType(GetColumnName(columnNode.NodeValue), ref dataType);
                                                        if (!string.IsNullOrEmpty(dataType))
                                                        {
                                                            switch (dataType)
                                                            {
                                                                case "NUMBER":
                                                                    {
                                                                        valueNode1.Children[0].NodeValue = "TO_NUMBER(NULL)";
                                                                        valueNode2.Children[0].NodeValue = "TO_NUMBER(NULL)";
                                                                        break;
                                                                    }

                                                                case "DATE":
                                                                    {
                                                                        valueNode1.Children[0].NodeValue = "TO_DATE(NULL)";
                                                                        valueNode2.Children[0].NodeValue = "TO_DATE(NULL)";
                                                                        break;
                                                                    }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }

                                        //Remove unnecessary nodes
                                        for (int i = 1; i <= 5; i++)
                                            node.RemoveChild(1);

                                        bool toCharWasAdded = false;
                                        string castFormat = "";
                                        if (node.Children.Count - 3 > 0 &&
                                            node.Children[node.Children.Count - 2].NodeType == Tokens.StringConst &&
                                            node.Children[node.Children.Count - 2].NodeInfo == "SKIP" &&
                                            node.Children[node.Children.Count - 3].NodeType == Tokens.Comma &&
                                            node.Children[node.Children.Count - 1].NodeType == Tokens.RightParant)
                                        {
                                            toCharWasAdded = true;
                                            castFormat = node.Children[node.Children.Count - 2].NodeValue;
                                            node.RemoveChild(node.Children.Count - 3);
                                            node.RemoveChild(node.Children.Count - 2);
                                            node.RemoveChild(node.Children.Count - 1);
                                        }

                                        bool containsISNA = false;
                                        foreach (TreeNode n in conditionNode.Children)
                                        {
                                            if (n.ToString().StartsWith("@ISNA"))
                                            {
                                                containsISNA = true;
                                                break;
                                            }
                                        }

                                        //if (conditionNode.ToString().Contains("@ISNA"))
                                        if (containsISNA)
                                        {
                                            node.RemoveChild(0);

                                            //Create WHEN number THEN value1
                                            TreeNode whenNode = new TreeNode(Tokens.Keyword, "WHEN");
                                            whenNode.AddChild(conditionNode);
                                            whenNode.AddChild(new TreeNode(Tokens.Keyword, "THEN"));
                                            whenNode.AddChild(valueNode1);

                                            node.AddChild(whenNode);

                                            //Create ELSE value2
                                            TreeNode elseNode = new TreeNode(Tokens.Keyword, "ELSE");
                                            elseNode.AddChild(valueNode2);

                                            node.AddChild(elseNode);
                                        }
                                        else
                                        {
                                            bool isDate = false;
                                            bool isTime = false;
                                            bool isTimestamp = false;

                                            //Create WHEN 0 THEN value2
                                            TreeNode whenNode = new TreeNode(Tokens.Keyword, "WHEN");
                                            //@MU
                                            node.RemoveChild(0);
                                            whenNode.AddChild(conditionNode);
                                            whenNode.AddChild(new TreeNode(Tokens.Keyword, "IS NULL"));
                                            TreeNode columnNodeFromCondition = targetTree.FindNode(conditionNode, Tokens.Column);

                                            if (columnNodeFromCondition != null)
                                            {
                                                int milliseconds = 0;

                                                GetColumnDateTimeProperties(GetColumnName(columnNodeFromCondition.NodeValue), out isDate, out isTime, out isTimestamp, out milliseconds);
                                            }
                                            if (!isDate && !isTime && !isTimestamp)
                                            {
                                                whenNode.AddChild(new TreeNode(Tokens.Keyword, "OR"));
                                                whenNode.AddChild(conditionNode);
                                                whenNode.AddChild(new TreeNode(Tokens.Keyword, "="));
                                                //@MU END
                                                whenNode.AddChild(new TreeNode(Tokens.NumericConst, "0"));
                                            }
                                            whenNode.AddChild(new TreeNode(Tokens.Keyword, "THEN"));
                                            whenNode.AddChild(valueNode2);

                                            node.AddChild(whenNode);

                                            //Create ELSE value1
                                            TreeNode elseNode = new TreeNode(Tokens.Keyword, "ELSE");
                                            elseNode.AddChild(valueNode1);

                                            node.AddChild(elseNode);
                                        }

                                        node.AddChild(new TreeNode(Tokens.Keyword, "END"));
                                        if (toCharWasAdded)
                                        {
                                            node.Children.Add(new TreeNode(Tokens.Comma, ",", "SKIP"));
                                            node.Children.Add(new TreeNode(Tokens.StringConst, castFormat, "SKIP"));
                                            node.Children.Add(new TreeNode(Tokens.RightParant, ")", "SKIP"));
                                        }

                                        if (castWasUsed)
                                        {
                                            switch (dataType)
                                            {
                                                case "NUMBER":
                                                    {
                                                        node.Children.Add(new TreeNode(Tokens.Expression, " AS NUMBER)", "SKIP"));
                                                        break;
                                                    }
                                                case "CHAR":
                                                case "VARCHAR2":
                                                    {
                                                        node.Children.Add(new TreeNode(Tokens.Expression, " AS VARCHAR2(254))", "SKIP"));
                                                        break;
                                                    }
                                                case "DATE":
                                                    {
                                                        node.Children.Add(new TreeNode(Tokens.Expression, "AS DATE)", "SKIP"));
                                                        break;
                                                    }
                                                case "TIMESTAMP":
                                                    {
                                                        node.Children.Add(new TreeNode(Tokens.Expression, "AS TIMESTAMP)", "SKIP"));
                                                        break;
                                                    }
                                            }
                                        }
                                        break;
                                    }


                                //@ISNA(argument)
                                case "@ISNA":
                                    {
                                        //argument IS NULL
                                        //@ISNA(arg) = 1 -> arg is NULL
                                        //@ISNA(arg) = 0 -> arg is NOT NULL

                                        //Find parent of function (expression)
                                        TreeNode parentNode = targetTree.FindParent(root, node);
                                        TreeNode predicateNode = targetTree.FindParent(root, parentNode);

                                        TreeNode exprNode = targetTree.FindNode(node, Tokens.Expression);

                                        bool negate = false;
                                        TreeNode equalNode = targetTree.FindNode(predicateNode, "=");
                                        if (equalNode != null)
                                        {
                                            int index = predicateNode.Children.IndexOf(equalNode);
                                            predicateNode.RemoveChild(equalNode);
                                            negate = (predicateNode.Children[index].ToString().Contains("0"));
                                            predicateNode.RemoveChild(index);
                                        }

                                        TreeNode sumNode = targetTree.FindNode(predicateNode, "SUM");
                                        if (sumNode != null)
                                        {
                                            TreeNode valueNode = node.Children[1];

                                            node.NodeValue = "CASE";

                                            TreeNode whenNode = new TreeNode(Tokens.Keyword, "WHEN");

                                            node.Children.Clear();
                                            whenNode.AddChild(valueNode);
                                            whenNode.AddChild(new TreeNode(Tokens.Keyword, "IS NULL"));
                                            //whenNode.AddChild(new TreeNode(Tokens.Keyword, "OR"));
                                            //whenNode.AddChild(valueNode);
                                            //whenNode.AddChild(new TreeNode(Tokens.Keyword, "="));
                                            //whenNode.AddChild(new TreeNode(Tokens.NumericConst, "0"));
                                            whenNode.AddChild(new TreeNode(Tokens.Keyword, "THEN"));
                                            whenNode.AddChild(new TreeNode(Tokens.NumericConst, "1"));

                                            node.AddChild(whenNode);

                                            //Create ELSE value1
                                            node.AddChild(new TreeNode(Tokens.Keyword, "ELSE"));
                                            node.AddChild(new TreeNode(Tokens.NumericConst, "0"));

                                            node.AddChild(new TreeNode(Tokens.Keyword, "END"));
                                        }
                                        else
                                        {
                                            if (predicateNode.ToString().StartsWith("SELECT"))
                                            {
                                                //Remove function from parent
                                                parentNode.RemoveChild(node);
                                                parentNode.AddChild(new TreeNode(Tokens.Keyword, "CASE"));
                                                parentNode.AddChild(new TreeNode(Tokens.Keyword, "WHEN"));
                                                parentNode.AddChild(exprNode);
                                                parentNode.AddChild(new TreeNode(Tokens.Keyword, "IS"));
                                                parentNode.AddChild(new TreeNode(Tokens.SysKeyword, "NULL"));
                                                parentNode.AddChild(new TreeNode(Tokens.Keyword, "THEN"));
                                                parentNode.AddChild(new TreeNode(Tokens.NumericConst, "1"));
                                                parentNode.AddChild(new TreeNode(Tokens.Keyword, "ELSE"));
                                                parentNode.AddChild(new TreeNode(Tokens.NumericConst, "0"));
                                                parentNode.AddChild(new TreeNode(Tokens.Keyword, "END"));
                                            }
                                            else
                                            {
                                                //Remove function from parent
                                                parentNode.RemoveChild(node);

                                                //Create new expression
                                                parentNode.AddChild(exprNode);
                                                parentNode.AddChild(new TreeNode(Tokens.Keyword, "IS"));
                                                parentNode.AddChild(new TreeNode(Tokens.SysKeyword, negate ? "NOT NULL" : "NULL"));
                                            }
                                        }
                                        break;

                                    }


                                //These functions use the index of the string
                                //In SqlBase the index starts from 0; in Oracle the index starts from 1
                                case "@MID":
                                case "@SUBSTRING":
                                    {
                                        node.NodeValue = FunctionTranslator.TranslateFunction(DatabaseBrand.Oracle, node.NodeValue);
                                        //Find the index argument
                                        if (node.Children.Count > 3)
                                        {
                                            TreeNode indexNode = node.Children[3]; //this is the 2nd parameter
                                            indexNode.AddChild(new TreeNode(Tokens.Expression, "+ 1"));
                                        }
                                        break;
                                    }

                                case "@HEX":
                                case "@LICS":
                                case "@REPEAT":
                                    //case "@REPLACE":
                                    //case "@DATETOCHAR":
                                    //case "@LEFT":
                                    //case "@RIGHT":
                                    //case "@STRING":
                                    {
                                        //the entire expression that contains these functions has to be cast to varchar2(254)
                                        //otherwise in Gupta they can't be read into a string bind variable; only in a long string
                                        //this is because the return type of these functions is varchar2 and if no length is specified
                                        //it will default to the max length
                                        node.NodeValue = FunctionTranslator.TranslateFunction(DatabaseBrand.Oracle, node.NodeValue);
                                        node.NodeInfo = "SKIP";

                                        TreeNodeCollection expressions = new TreeNodeCollection();
                                        targetTree.FindAll(root, Tokens.Expression, ref expressions);

                                        foreach (TreeNode expr in expressions)
                                        {
                                            if (targetTree.FindNode(expr, node))
                                            {
                                                if (expr.NodeInfo != "SKIP" && !expr.ToString().Contains("TO_NUMBER") && expr.NodeValue != "CAST(")
                                                {
                                                    expr.NodeValue = "CAST(";
                                                    if (expr.Children.Count >= 2 && expr.Children[expr.Children.Count - 2].NodeValue == "AS")
                                                    {
                                                        expr.InsertChild(new TreeNode(Tokens.Expression, " as varchar2(254))"), expr.Children.Count - 2);
                                                    }
                                                    else
                                                    {
                                                        expr.AddChild(new TreeNode(Tokens.Expression, " as varchar2(254))"));
                                                    }
                                                    expr.NodeInfo = "SKIP";
                                                }
                                                break;
                                            }
                                        }


                                        break;
                                    }
                                //@SCAN -> INSTR - the first position in the string is 1
                                case "@SCAN":
                                    {
                                        //node.NodeValue = FunctionTranslator.TranslateFunction(DatabaseBrand.Oracle, node.NodeValue);
                                        //node.NodeInfo = "SKIP";

                                        ////substract 1 from the result
                                        //node.AddChild(new TreeNode(Tokens.MathOperator, "-"));
                                        //node.AddChild(new TreeNode(Tokens.NumericConst, "1"));
                                        TreeNode columnNode = targetTree.FindNode(node, Tokens.Column);
                                        bool isLong = false;
                                        bool isBinary = false;
                                        bool isExternal = false;
                                        GetColumnProperties(GetColumnName(columnNode.NodeValue), out isBinary, out isLong, out isExternal);

                                        //If SCAN contains Long column translate to:
                                        //SCANLong(tablename.rowid, pattern, creator.tablename, longcolumn)
                                        if (isLong)
                                        {
                                            string tableName = "";
                                            string schemaName = "";
                                            var colInfo = this.columnInfo[GetColumnName(columnNode.NodeValue).ToUpper()];
                                            if (colInfo != null)
                                            {
                                                tableName = colInfo.Table;
                                                schemaName = colInfo.Owner;
                                                node.NodeValue = "SYSADM.SCANLong";
                                                TreeNode patternNode = node.Children[3];

                                                node.Children.Clear();

                                                if (patternNode.NodeType == Tokens.StringConst || patternNode.Children[0].NodeType == Tokens.StringConst)
                                                {
                                                    node.Children.Add(new TreeNode(Tokens.LeftParant, "("));
                                                    node.Children.Add(new TreeNode(Tokens.Column, tableName + ".ROWID", "SKIP"));
                                                    node.Children.Add(new TreeNode(Tokens.Comma, ","));
                                                    node.Children.Add(patternNode);
                                                    node.Children.Add(new TreeNode(Tokens.Comma, ","));
                                                    node.Children.Add(new TreeNode(Tokens.StringConst, "'" + schemaName + "." + tableName + "'"));
                                                    node.Children.Add(new TreeNode(Tokens.Comma, ","));
                                                    node.Children.Add(new TreeNode(Tokens.StringConst, "'" + columnNode.ToString() + "'"));
                                                    node.Children.Add(new TreeNode(Tokens.RightParant, ")"));
                                                }
                                            }
                                        }
                                        else
                                        {
                                            node.NodeValue = "SCAN";
                                        }
                                        break;
                                    }

                                //case "@REPEAT":
                                //    {
                                //        //@REPEAT(str, nr)
                                //        //RPAD(str, LENGTH(str) * nr, str);

                                //        node.NodeValue = "RPAD";
                                //        string str = node.Children[1].ToString();
                                //        node.Children[3].NodeValue = "LENGTH(" + str + ") * " + node.Children[3].NodeValue;
                                //        node.Children.Insert(new TreeNode(Tokens.Identifier, str), 4);
                                //        node.Children.Insert(new TreeNode(Tokens.Comma, ","), 4);
                                //        break;
                                //    }
                                case "@LENGTH":
                                    {
                                        TreeNode columnNode = targetTree.FindNode(node, Tokens.Column);
                                        if (columnNode != null)
                                        {
                                            bool isLong = false;
                                            bool isBinary = false;
                                            bool isExternal = false;
                                            GetColumnProperties(GetColumnName(columnNode.NodeValue), out isBinary, out isLong, out isExternal);

                                            //If SCAN contains Long column translate to:
                                            //SCANLong(tablename.rowid, pattern, creator.tablename, longcolumn)
                                            //if (isLong && isBinary)
                                            if (isLong)
                                            {
                                                string tableName = "";
                                                string schemaName = "";
                                                var colInfo = this.columnInfo[GetColumnName(columnNode.NodeValue).ToUpper()];
                                                if (colInfo != null)
                                                {
                                                    tableName = colInfo.Table;
                                                    schemaName = colInfo.Owner;
                                                    node.NodeValue = "SYSADM.LENGTHLongRaw";

                                                    node.Children.Clear();

                                                    node.Children.Add(new TreeNode(Tokens.LeftParant, "("));
                                                    node.Children.Add(new TreeNode(Tokens.Column, tableName + (isExternal ? "_V" : "") + ".ROWID", "SKIP"));
                                                    node.Children.Add(new TreeNode(Tokens.Comma, ","));
                                                    node.Children.Add(new TreeNode(Tokens.StringConst, "'" + schemaName + "." + tableName + "'"));
                                                    node.Children.Add(new TreeNode(Tokens.Comma, ","));
                                                    node.Children.Add(new TreeNode(Tokens.StringConst, "'" + columnNode.ToString() + "'"));
                                                    node.Children.Add(new TreeNode(Tokens.Comma, ","));
                                                    if (isExternal)
                                                    {
                                                        node.Children.Add(new TreeNode(Tokens.StringConst, isBinary ? "'CPLONGBIN'" : "'CPLONGTEXT'"));
                                                    }
                                                    else
                                                    {
                                                        node.Children.Add(new TreeNode(Tokens.StringConst, "''"));
                                                    }
                                                    node.Children.Add(new TreeNode(Tokens.RightParant, ")"));
                                                }
                                            }
                                            else
                                            {
                                                node.NodeValue = "LEN";
                                            }
                                        }
                                        else
                                        {
                                            node.NodeValue = "LEN";
                                        }
                                        break;
                                    }
                                case "@REPLACE":
                                    {
                                        //@REPLACE(string1, pos, length, string2)
                                        //SUBSTR(string1, 1, pos-1) || string2 || SUBSTR(string1, pos + length)
                                        if (node.Children.Count >= 8)
                                        {
                                            node.NodeValue = "";
                                            node.NodeInfo = "SKIP";
                                            TreeNode str1Node = new TreeNode(node.Children[1]);
                                            TreeNode posNode = new TreeNode(node.Children[3]);
                                            TreeNode lengthNode = new TreeNode(node.Children[5]);
                                            TreeNode str2Node = new TreeNode(node.Children[7]);

                                            node.Children.Clear();
                                            node.Children.Add(new TreeNode(Tokens.Function, "SUBSTR"));
                                            node.Children.Add(new TreeNode(Tokens.LeftParant, "("));
                                            node.Children.Add(str1Node);
                                            node.Children.Add(new TreeNode(Tokens.Comma, ","));
                                            node.Children.Add(new TreeNode(Tokens.NumericConst, "1"));
                                            node.Children.Add(new TreeNode(Tokens.Comma, ","));
                                            node.Children.Add(posNode);
                                            //node.Children.Add(new TreeNode(Tokens.MathOperator, "-"));
                                            //node.Children.Add(new TreeNode(Tokens.NumericConst, "1"));
                                            node.Children.Add(new TreeNode(Tokens.RightParant, ")"));
                                            node.Children.Add(new TreeNode(Tokens.Concatenate, "||"));
                                            node.Children.Add(str2Node);
                                            node.Children.Add(new TreeNode(Tokens.Concatenate, "||"));
                                            node.Children.Add(new TreeNode(Tokens.Function, "SUBSTR"));
                                            node.Children.Add(new TreeNode(Tokens.LeftParant, "("));
                                            node.Children.Add(str1Node);
                                            node.Children.Add(new TreeNode(Tokens.Comma, ","));
                                            node.Children.Add(posNode);
                                            node.Children.Add(new TreeNode(Tokens.MathOperator, "+"));
                                            node.Children.Add(lengthNode);
                                            node.Children.Add(new TreeNode(Tokens.MathOperator, "+"));
                                            node.Children.Add(new TreeNode(Tokens.NumericConst, "1"));
                                            node.Children.Add(new TreeNode(Tokens.RightParant, ")"));
                                        }
                                        break;
                                    }
                                case "@DECODE":
                                    {
                                        node.NodeValue = "DECODE";
                                        bool castNodeToDate = false;
                                        //If the DECODE node is preceded or followed by a mathematical operator cast the whole node TO_DATE
                                        if (node.Parent.Children.Count > 1)
                                        {
                                            int nodeIndex = node.Parent.Children.IndexOf(node);
                                            if (node.Parent.Children.Count > nodeIndex + 1)
                                            {
                                                if (node.Parent.Children[nodeIndex + 1].NodeType == Tokens.MathOperator)
                                                {
                                                    castNodeToDate = true;
                                                }
                                            }
                                            else if (nodeIndex > 0)
                                            {
                                                if (node.Parent.Children[nodeIndex - 1].NodeType == Tokens.MathOperator)
                                                {
                                                    castNodeToDate = true;
                                                }
                                            }
                                        }

                                        bool isDate = false;
                                        bool isTime = false;
                                        bool isTimestamp = false;
                                        int milliseconds = 0;

                                        //If the node is from a search condition and the comparing column is not a date column do not cast it TO_DATE
                                        //But also, the node may not be involved in a mathematical opeartion, so we still need to check if the node is compared
                                        //to a date column
                                        if (targetTree.IsNodeFromSearch(root, node))
                                        {
                                            TreeNode predicateNode = targetTree.FindParent(node, Tokens.Predicate);
                                            if (predicateNode != null)
                                            {
                                                if (predicateNode.Children.Count == 3)
                                                {
                                                    if (targetTree.FindNode(predicateNode.Children[2], node))
                                                    {
                                                        TreeNode comparedNode = predicateNode.Children[0];
                                                        if (comparedNode.Children.Count == 1 && comparedNode.Children[0].NodeType == Tokens.Column)
                                                        {
                                                            comparedNode = comparedNode.Children[0];

                                                            isDate = false;
                                                            isTime = false;
                                                            isTimestamp = false;
                                                            milliseconds = 0;
                                                            string colName = comparedNode.NodeValue;
                                                            string tableName = GetTableNameFromColumn(comparedNode.NodeValue);

                                                            this.GetColumnDateTimeProperties(GetColumnName(colName), out isDate, out isTime, out isTimestamp, out milliseconds);

                                                            if (!isDate && !isTimestamp)
                                                            {
                                                                castNodeToDate = false;
                                                            }
                                                            else
                                                            {
                                                                castNodeToDate = true;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            //If the node is not from a search node, find the column from the decode function
                                            //If the column node is not a date/time column, then the cast To_Date must not be applied
                                            isDate = false;
                                            isTime = false;
                                            isTimestamp = false;
                                            milliseconds = 0;

                                            foreach (TreeNode childrenNode in node.Children)
                                            {
                                                TreeNode columnNode = null;
                                                if (childrenNode.Children.Count == 1 && childrenNode.Children[0].NodeType == Tokens.Column)
                                                {
                                                    columnNode = childrenNode.Children[0];
                                                }
                                                if (columnNode != null)
                                                {
                                                    this.GetColumnDateTimeProperties(GetColumnName(columnNode.NodeValue), out isDate, out isTime, out isTimestamp, out milliseconds);

                                                    if (!isDate && !isTimestamp)
                                                    {
                                                        castNodeToDate = false;
                                                    }
                                                    else
                                                    {
                                                        castNodeToDate = true;
                                                    }
                                                }
                                            }
                                        }

                                        if (castNodeToDate)
                                        {

                                            node.NodeValue = "TO_DATE ( TO_CHAR ( " + node.NodeValue;
                                            node.Children.Add(new TreeNode(Tokens.Comma, ",", "SKIP"));
                                            node.Children.Add(new TreeNode(Tokens.StringConst, "'YYYY-MM-DD'", "SKIP"));
                                            node.Children.Add(new TreeNode(Tokens.RightParant, ")", "SKIP"));
                                            node.Children.Add(new TreeNode(Tokens.Comma, ",", "SKIP"));
                                            node.Children.Add(new TreeNode(Tokens.StringConst, "'YYYY-MM-DD'", "SKIP"));
                                            node.Children.Add(new TreeNode(Tokens.RightParant, ")", "SKIP"));
                                        }

                                        TreeNode topMostNode = node;
                                        TreeNode datetocharNode = targetTree.FindTopmostExpression(node);
                                        bool isDateToChar = false;
                                        string dateToCharFormat = "";
                                        if (datetocharNode != null &&
                                            datetocharNode.OriginalNodeValue == "@DATETOCHAR" ||
                                            (datetocharNode.Children.Count == 1 && datetocharNode.Children[0].OriginalNodeValue == "@DATETOCHAR"))
                                        {
                                            isDateToChar = true;
                                            string decodeValue = datetocharNode.ToString();
                                            string[] decodeValueSplits = decodeValue.Split(',');
                                            dateToCharFormat = decodeValueSplits[decodeValueSplits.Length - 1];
                                            dateToCharFormat = dateToCharFormat.Replace("(", "");
                                            dateToCharFormat = dateToCharFormat.Replace(")", "");
                                            dateToCharFormat = dateToCharFormat.Trim();

                                        }

                                        if (topMostNode.Children.Count == 1)
                                        {
                                            topMostNode = topMostNode.Children[0];
                                        }

                                        TreeNodeCollection decodeNodes = new TreeNodeCollection();
                                        foreach (TreeNode n in topMostNode.Children)
                                        {
                                            if (n.NodeType != Tokens.Comma && (n.Children.Count > 0 && n.Children[0].NodeType != Tokens.Comma)
                                                || n.NodeType != Tokens.LeftParant && (n.Children.Count > 0 && n.Children[0].NodeType != Tokens.LeftParant)
                                                || n.NodeType != Tokens.RightParant && (n.Children.Count > 0 && n.Children[0].NodeType != Tokens.RightParant))
                                            {
                                                decodeNodes.Add(n);
                                            }
                                        }
                                        TreeNode expressionNode = null;
                                        List<TreeNode> comparisonNodes = new List<TreeNode>();
                                        List<TreeNode> returnNodes = new List<TreeNode>();
                                        if (decodeNodes.Count == 3)
                                        {
                                            expressionNode = decodeNodes[0];
                                            comparisonNodes.Add(decodeNodes[1]);
                                            returnNodes.Add(decodeNodes[2]);
                                        }
                                        else if (decodeNodes.Count == 4)
                                        {
                                            expressionNode = decodeNodes[0];
                                            comparisonNodes.Add(decodeNodes[1]);
                                            returnNodes.Add(decodeNodes[2]);
                                            returnNodes.Add(decodeNodes[3]);
                                        }
                                        else
                                        {
                                            for (int count = 0; count < decodeNodes.Count - 1; count++)
                                            {
                                                if (count == 0)
                                                {
                                                    expressionNode = decodeNodes[count];
                                                }
                                                else if (count % 2 != 0)
                                                {
                                                    comparisonNodes.Add(decodeNodes[count]);
                                                }
                                                else
                                                {
                                                    returnNodes.Add(decodeNodes[count]);
                                                }
                                            }
                                            returnNodes.Add(decodeNodes[decodeNodes.Count - 1]);
                                        }

                                        foreach (TreeNode returnNode in returnNodes)
                                        {
                                            if (targetTree.FindNode(returnNode, Tokens.MathOperator) != null)
                                            {
                                                isDateToChar = true;
                                            }
                                        }

                                        string expressionCast = "";
                                        string expressionFormat = "";
                                        bool expresionIsString = false;
                                        bool dateColumnFound = false;
                                        foreach (TreeNode n in node.Children)
                                        {
                                            bool isFromComparison = false;
                                            bool isFromReturn = false;
                                            foreach (TreeNode cn in comparisonNodes)
                                            {
                                                if (targetTree.FindNode(cn, n))
                                                {
                                                    isFromComparison = true;
                                                    break;
                                                }
                                            }

                                            foreach (TreeNode rn in returnNodes)
                                            {
                                                if (targetTree.FindNode(rn, n))
                                                {
                                                    isFromReturn = true;
                                                    break;
                                                }
                                            }

                                            TreeNode decodeInternalNode = n;
                                            if (n.Children.Count == 1)
                                            {
                                                decodeInternalNode = n.Children[0];
                                            }

                                            TreeNode decodeInternalNodeTopMost = targetTree.FindTopmostExpression(decodeInternalNode);
                                            if (decodeInternalNode.NodeType == Tokens.StringConst
                                                || decodeInternalNode.NodeType == Tokens.DatetimeConst)
                                            {
                                                CustomDateTime date = null;
                                                if (IsValidDateTime(decodeInternalNode.ToString().Replace("'", "").Trim(), out date))
                                                {
                                                    if (!decodeInternalNode.OriginalNodeValue.Contains("'"))
                                                    {
                                                        decodeInternalNode.NodeValue = "'" + decodeInternalNode.OriginalNodeValue + "'";
                                                    }
                                                    string dateFormat = "";
                                                    string decodeInternalValue = "";
                                                    if (decodeInternalNode.NodeType == Tokens.StringConst)
                                                    {
                                                        decodeInternalValue = decodeInternalNode.ToString().Replace("'", "").Trim();
                                                    }
                                                    else
                                                    {
                                                        decodeInternalValue = decodeInternalNode.OriginalNodeValue.ToString().Trim();
                                                    }
                                                    if (decodeInternalValue.Contains("-"))
                                                    {
                                                        if (decodeInternalValue.Split('-')[0].Length == 2 && decodeInternalValue.Split('-')[2].Length == 4)
                                                        {
                                                            dateFormat = "'DD-MM-YYYY'";
                                                        }
                                                        else if (decodeInternalValue.Split('-')[0].Length == 4 && decodeInternalValue.Split('-')[2].Length == 2)
                                                        {
                                                            dateFormat = "'YYYY-MM-DD'";
                                                        }
                                                    }
                                                    else if (decodeInternalValue.Contains("."))
                                                    {
                                                        if (decodeInternalValue.Split('.')[0].Length == 2 && decodeInternalValue.Split('.')[2].Length == 4)
                                                        {
                                                            dateFormat = "'DD.MM.YYYY'";
                                                        }
                                                        else if (decodeInternalValue.Split('.')[0].Length == 4 && decodeInternalValue.Split('.')[2].Length == 2)
                                                        {
                                                            dateFormat = "'YYYY.MM.DD'";
                                                        }
                                                    }
                                                    if (isFromComparison && !string.IsNullOrEmpty(expressionCast) && !string.IsNullOrEmpty(expressionFormat))
                                                    {
                                                        if (expressionCast != "TO_DATE")
                                                        {
                                                            decodeInternalNode.NodeValue = expressionCast + " ( TO_DATE ( " + decodeInternalNode.NodeValue + " , " + expressionFormat + " ) " + " , " + expressionFormat + " )";
                                                        }
                                                        else
                                                        {
                                                            decodeInternalNode.NodeValue = expressionCast + " ( " + decodeInternalNode.NodeValue + " , " + expressionFormat + " )";
                                                        }
                                                    }
                                                    else if (isFromComparison && !expresionIsString)
                                                    {
                                                        decodeInternalNode.NodeValue = "TO_DATE ( " + decodeInternalNode.NodeValue + " , " + dateFormat + " )";
                                                    }
                                                    else if (isDateToChar && isFromReturn)
                                                    {
                                                        decodeInternalNode.NodeValue = "TO_DATE ( " + decodeInternalNode.NodeValue + " , " + dateFormat + " )";
                                                    }
                                                    else
                                                    {
                                                        if (isFromReturn && decodeInternalNode.NodeType == Tokens.DatetimeConst)
                                                        {
                                                            decodeInternalNode.NodeValue = "TO_CHAR ( TO_DATE ( " + decodeInternalNode.NodeValue + " , " + dateFormat + " ) " + " , " + dateFormat + " )";
                                                        }
                                                    }
                                                    decodeInternalNode.NodeInfo = "SKIP";
                                                }
                                                else if (decodeInternalNode.NodeType == Tokens.StringConst &&
                                                    string.IsNullOrEmpty(decodeInternalNode.NodeValue.Replace("'", "").Trim()) && isDateToChar && !string.IsNullOrEmpty(dateToCharFormat))
                                                {
                                                    decodeInternalNode.NodeValue = "TO_DATE ( " + decodeInternalNode.NodeValue + " , " + dateToCharFormat + " )";
                                                }

                                                decodeInternalNode.NodeInfo = "SKIP";
                                            }
                                            else if (decodeInternalNode.NodeType == Tokens.Column)
                                            {
                                                isDate = false;
                                                isTime = false;
                                                isTimestamp = false;
                                                milliseconds = 0;
                                                string colName = decodeInternalNode.NodeValue;
                                                string tableName = GetTableNameFromColumn(decodeInternalNode.NodeValue);

                                                this.GetColumnDateTimeProperties(GetColumnName(colName), out isDate, out isTime, out isTimestamp, out milliseconds);
                                                if (isDate && !dateColumnFound)
                                                {
                                                    dateColumnFound = true;
                                                }
                                                if (!isDate && !isTimestamp)
                                                {
                                                    TranslateColumn(decodeInternalNode, root, targetTree);
                                                    if (!isTime)
                                                    {
                                                        expresionIsString = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (isDate)
                                                    {
                                                        if (isFromReturn)
                                                        {
                                                            if (!isDateToChar)
                                                            {
                                                                decodeInternalNode.NodeValue = "TO_CHAR ( " + decodeInternalNode.NodeValue + " , 'YYYY-MM-DD' )";
                                                                decodeInternalNode.NodeInfo = "SKIP";
                                                            }
                                                            else
                                                            {
                                                                decodeInternalNode.NodeValue = "TO_DATE ( TO_CHAR ( " + decodeInternalNode.NodeValue + " , 'YYYY-MM-DD' ) " + " , 'YYYY-MM-DD' )";
                                                                decodeInternalNode.NodeInfo = "SKIP";
                                                            }
                                                        }
                                                    }
                                                    else if (isTimestamp && isFromReturn)
                                                    {
                                                        string format = "'YYYY-MM-DD HH24:MI:SS";
                                                        if (milliseconds != 0)
                                                        {
                                                            format += ".FF" + milliseconds;
                                                        }
                                                        format += "'";

                                                        if (isDateToChar)
                                                        {
                                                            if (milliseconds > 0)
                                                            {
                                                                decodeInternalNode.NodeValue = "TO_TIMESTAMP ( " + decodeInternalNode.NodeValue + " , " + format + " )";
                                                            }
                                                            else
                                                            {
                                                                decodeInternalNode.NodeValue = "TO_DATE ( " + decodeInternalNode.NodeValue + " , " + format + " )";
                                                            }
                                                            decodeInternalNode.NodeInfo = "SKIP";
                                                        }
                                                        else
                                                        {
                                                            decodeInternalNode.NodeValue = "TO_CHAR ( " + decodeInternalNode.NodeValue + " , " + format + " )";
                                                            decodeInternalNode.NodeInfo = "SKIP";
                                                        }
                                                    }
                                                    if (decodeInternalNode != expressionNode && decodeInternalNode.Parent != expressionNode)
                                                    {
                                                        if (comparisonNodes != null && comparisonNodes.Count > 0 && (comparisonNodes.Contains(decodeInternalNode) || comparisonNodes.Contains(decodeInternalNode.Parent) || isFromComparison))
                                                        {
                                                            if (!string.IsNullOrEmpty(expressionCast) && !string.IsNullOrEmpty(expressionFormat))
                                                            {
                                                                if (expressionFormat.Contains(".FF") && !isTimestamp && expressionCast != "TO_TIMESTAMP")
                                                                {
                                                                    decodeInternalNode.NodeValue = expressionCast + " ( TO_TIMESTAMP ( " + decodeInternalNode.OriginalNodeValue + " , " + expressionFormat + " )" + " , " + expressionFormat + " )";
                                                                }
                                                                else
                                                                {
                                                                    decodeInternalNode.NodeValue = expressionCast + " ( " + decodeInternalNode.OriginalNodeValue + " , " + expressionFormat + " )";
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                decodeInternalNode.NodeInfo = "SKIP";
                                            }
                                            else if (decodeInternalNode.NodeType == Tokens.SysKeyword)
                                            {
                                                if (decodeInternalNode.NodeValue == "NULL")
                                                {
                                                    if (isFromReturn && isDateToChar)
                                                    {
                                                        decodeInternalNode.NodeValue = "TO_DATE ( " + decodeInternalNode.NodeValue + " )";
                                                    }
                                                }
                                                else
                                                {
                                                    string sysDateFormat = "";
                                                    string sysDateCast = "";
                                                    if (decodeInternalNode.NodeValue == "SYSDATETIME")
                                                    {
                                                        decodeInternalNode.NodeValue = "SYSTIMESTAMP";
                                                        sysDateFormat = "'YYYY-MM-DD HH24:MI:SS.FF6'";
                                                    }
                                                    else if (decodeInternalNode.NodeValue == "SYSDATE")
                                                    {
                                                        decodeInternalNode.NodeValue = "TRUNC(SYSDATE)";
                                                        sysDateFormat = "'YYY-MM-DD'";
                                                    }
                                                    else if (decodeInternalNode.NodeValue == "SYSTIME")
                                                    {
                                                        decodeInternalNode.NodeValue = "SYSDATE";
                                                        sysDateFormat = "'YYYY-MM-DD'";
                                                    }
                                                    else if (decodeInternalNode.NodeValue == "@NOW")
                                                    {
                                                        decodeInternalNode.NodeValue = "SYSDATE";
                                                        sysDateFormat = "'YYYY-MM-DD HH24:MI:SS'";
                                                    }


                                                    if (isFromReturn)
                                                    {
                                                        if (returnNodes[0] != decodeInternalNode)
                                                        {
                                                            foreach (TreeNode returnNode in returnNodes)
                                                            {
                                                                if (returnNode == decodeInternalNode)
                                                                {
                                                                    break;
                                                                }
                                                                else
                                                                {
                                                                    if (returnNode.ToString().StartsWith("TO_"))
                                                                    {
                                                                        string returnedFormat = "";
                                                                        string returnedCast = "";
                                                                        GetCastAndFormatFromNode(returnNode, ref returnedCast, ref returnedFormat);

                                                                        if (!string.IsNullOrEmpty(sysDateFormat))
                                                                        {
                                                                            sysDateFormat = returnedFormat;
                                                                        }

                                                                        break;
                                                                    }
                                                                }
                                                            }

                                                            if (!isDateToChar)
                                                            {
                                                                decodeInternalNode.NodeValue = "TO_CHAR ( " + decodeInternalNode.NodeValue + " , " + sysDateFormat + " )";
                                                            }
                                                            else
                                                            {
                                                                decodeInternalNode.NodeValue = "TO_DATE ( " + decodeInternalNode.NodeValue + " , " + sysDateFormat + " )";
                                                            }
                                                        }
                                                    }
                                                    else if (isFromComparison)
                                                    {
                                                        decodeInternalNode.NodeValue = expressionCast + " ( " + decodeInternalNode.NodeValue + " , " + expressionFormat + " )";
                                                    }
                                                    decodeInternalNode.NodeInfo = "SKIP";
                                                }
                                            }
                                            else if (decodeInternalNode.NodeType == Tokens.Function && isFromReturn
                                                && decodeInternalNode.NodeValue != "@DECODE" && decodeInternalNode.NodeValue != "DECODE")
                                            {
                                                if (isFromReturn)
                                                {
                                                    TreeNode parentNode = decodeInternalNode.Parent;
                                                    int nodeIndex = parentNode.Children.IndexOf(decodeInternalNode);
                                                    if (dateColumnFound)
                                                    {
                                                        if (!isDateToChar)
                                                        {
                                                            parentNode.Children.Insert(new TreeNode(Tokens.Function, "TO_CHAR", "SKIP"), nodeIndex);
                                                            parentNode.Children.Insert(new TreeNode(Tokens.LeftParant, "(", "SKIP"), nodeIndex + 1);
                                                            decodeInternalNode.Children.Add(new TreeNode(Tokens.Comma, ",", "SKIP"));
                                                            decodeInternalNode.Children.Add(new TreeNode(Tokens.StringConst, "'YYYY-MM-DD'", "SKIP"));
                                                            decodeInternalNode.Children.Add(new TreeNode(Tokens.RightParant, ")", "SKIP"));
                                                        }
                                                        else
                                                        {
                                                            parentNode.Children.Insert(new TreeNode(Tokens.Function, "TO_DATE", "SKIP"), nodeIndex);
                                                            parentNode.Children.Insert(new TreeNode(Tokens.LeftParant, "(", "SKIP"), nodeIndex + 1);
                                                            parentNode.Children.Insert(new TreeNode(Tokens.Function, "TO_CHAR", "SKIP"), nodeIndex + 2);
                                                            parentNode.Children.Insert(new TreeNode(Tokens.LeftParant, "(", "SKIP"), nodeIndex + 3);
                                                            decodeInternalNode.Children.Add(new TreeNode(Tokens.Comma, ",", "SKIP"));
                                                            decodeInternalNode.Children.Add(new TreeNode(Tokens.StringConst, "'YYYY-MM-DD'", "SKIP"));
                                                            decodeInternalNode.Children.Add(new TreeNode(Tokens.RightParant, ")", "SKIP"));
                                                            decodeInternalNode.Children.Add(new TreeNode(Tokens.Comma, ",", "SKIP"));
                                                            decodeInternalNode.Children.Add(new TreeNode(Tokens.StringConst, "'YYYY-MM-DD'", "SKIP"));
                                                            decodeInternalNode.Children.Add(new TreeNode(Tokens.RightParant, ")", "SKIP"));
                                                        }
                                                    }
                                                }
                                            }

                                            if (decodeInternalNode == expressionNode ||
                                                    decodeInternalNode.Parent == expressionNode)
                                            {
                                                GetCastAndFormatFromNode(expressionNode, ref expressionCast, ref expressionFormat);
                                            }
                                        }

                                        if (returnNodes.Count > 1 && !isDateToChar)
                                        {
                                            string finalFormat = "";
                                            foreach (TreeNode returnNode in returnNodes)
                                            {
                                                string returnCast = "";
                                                string returnFormat = "";
                                                GetCastAndFormatFromNode(returnNode, ref returnCast, ref returnFormat);
                                                if (finalFormat.Length < returnFormat.Length)
                                                {
                                                    finalFormat = returnFormat;
                                                }
                                                if (returnNode.OriginalNodeValue == "@DATETOCHAR" || (returnNode.Children.Count == 1 && returnNode.Children[0].OriginalNodeValue == "@DATETOCHAR"))
                                                {
                                                    finalFormat = returnFormat;
                                                    break;
                                                }
                                            }

                                            foreach (TreeNode returnNode in returnNodes)
                                            {
                                                string returnCast = "";
                                                string returnFormat = "";
                                                GetCastAndFormatFromNode(returnNode, ref returnCast, ref returnFormat);

                                                if (!string.IsNullOrEmpty(returnFormat) && returnFormat != finalFormat)
                                                {
                                                    if (returnNode.Children.Count == 1)
                                                    {
                                                        if (finalFormat.Contains(".FF") && !returnCast.Contains("TO_TIMESTAMP"))
                                                        {
                                                            returnNode.Children[0].NodeValue = returnNode.Children[0].NodeValue.Replace(returnCast, returnCast + " ( TO_TIMESTAMP");
                                                            returnNode.Children[0].NodeValue = returnNode.Children[0].NodeValue.Replace(returnFormat, finalFormat + " ) , " + finalFormat);
                                                        }
                                                        else
                                                        {
                                                            returnNode.Children[0].NodeValue = ReplaceNodeFormat(returnNode.Children[0].NodeValue, returnFormat, finalFormat);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (finalFormat.Contains(".FF") && !returnCast.Contains("TO_TIMESTAMP"))
                                                        {
                                                            returnNode.NodeValue = returnNode.NodeValue.Replace(returnCast, returnCast + " ( TO_TIMESTAMP");
                                                            returnNode.NodeValue = returnNode.NodeValue.Replace(returnFormat, finalFormat + " ) , " + finalFormat);
                                                        }
                                                        else
                                                        {
                                                            returnNode.NodeValue = ReplaceNodeFormat(returnNode.NodeValue, returnFormat, finalFormat);
                                                        }
                                                    }
                                                }
                                            }
                                        }

                                        break;
                                    }
                                case "@DATETOCHAR":
                                    {
                                        //@DATETOCHAR(date, picture)
                                        //TO_CHAR(date, picture);
                                        bool isTimestampFormat = false;
                                        TreeNode pictureNode = targetTree.FindNode(node.Children[3], Tokens.StringConst);
                                        if (Regex.IsMatch(pictureNode.NodeValue, "\\d")
                                            && !pictureNode.NodeValue.ToUpper().Contains("Y") && !pictureNode.NodeValue.ToUpper().Contains("M") && !pictureNode.NodeValue.ToUpper().Contains("D") && !pictureNode.NodeValue.ToUpper().Contains("S"))
                                        {
                                            node.Children.Clear();
                                            node.NodeValue = pictureNode.NodeValue;
                                        }
                                        else
                                        {
                                            pictureNode.NodeValue = pictureNode.NodeValue.Replace("hh", "hh24");
                                            if (pictureNode.NodeValue.Replace("'", "").EndsWith("9"))
                                            {
                                                string[] tempMillisec = pictureNode.NodeValue.Split('.');
                                                if (tempMillisec.Length > 0)
                                                {
                                                    string milliseconds = tempMillisec[tempMillisec.Length - 1].Replace("'", "");
                                                    string temp = "";
                                                    for (int count = 0; count < milliseconds.Length; count++)
                                                    {
                                                        temp += "9";
                                                    }
                                                    if (milliseconds == temp)
                                                    {
                                                        string newMillisecondsFormat = "FF" + milliseconds.Length;
                                                        pictureNode.NodeValue = pictureNode.NodeValue.Replace("." + milliseconds, "." + newMillisecondsFormat);
                                                        isTimestampFormat = true;
                                                    }
                                                }
                                            }
                                            //The following expression will crash under Oracle becase it can't convert the empty strings using @DATETOCHAR
                                            //@DATETOCHAR( @DECODE( PS_GEPLRUECKKEHR , 1400-01-01, '' ,1700-01-01 , '', PS_GEPLRUECKKEHR ), 'dd.MM.yyyy')
                                            //Such expressions have to be changed like this:
                                            //( @DECODE( PS_GEPLRUECKKEHR , 1400-01-01, '' ,1700-01-01 , '', @DATETOCHAR(PS_GEPLRUECKKEHR , 'dd.MM.yyyy'))
                                            TreeNode decodeNode = targetTree.FindNode(node, "@DECODE");
                                            if (decodeNode == null)
                                            {
                                                decodeNode = targetTree.FindNode(node, "DECODE");
                                            }
                                            if (decodeNode != null && targetTree.FindNode(decodeNode, Tokens.StringConst) != null)
                                            {
                                                node.NodeValue = "TO_CHAR";
                                            }
                                            else
                                            {
                                                if (targetTree.IsNodeFromSearch(root, node) && node.Children[1].Children[0].NodeValue == "@NOW")
                                                {
                                                    TreeNode toChar = targetTree.FindNode(node, "@DATETOCHAR");
                                                    TreeNode strConstant = targetTree.FindNode(toChar, Tokens.StringConst);

                                                    node.Children.Clear();

                                                    if (toChar != null && strConstant != null)
                                                    {
                                                        node.NodeValue = "";

                                                        node.AddChild(new TreeNode(Tokens.Function, "TO_CHAR"));
                                                        node.AddChild(new TreeNode(Tokens.LeftParant, "("));
                                                        node.AddChild(new TreeNode(Tokens.Function, FunctionTranslator.TranslateFunction(DatabaseBrand.Oracle, "@NOW")));
                                                        node.AddChild(new TreeNode(Tokens.Comma, ","));
                                                        node.AddChild(pictureNode);
                                                        node.AddChild(new TreeNode(Tokens.RightParant, ")"));
                                                    }
                                                    else
                                                    {
                                                        node.NodeValue = FunctionTranslator.TranslateFunction(DatabaseBrand.Oracle, "@NOW");
                                                        node.NodeInfo = "SKIP";
                                                    }
                                                }
                                                else
                                                {
                                                    if (isTimestampFormat)
                                                    {
                                                        if (node.Children[1].Children[0].NodeValue == "@NOW" ||
                                                            node.Children[1].Children[0].NodeValue == "SYSDATE" ||
                                                            node.Children[1].Children[0].NodeValue == "SYSTIMESTAMP" ||
                                                            node.Children[1].Children[0].NodeValue == "SYSDATETIME")
                                                        {
                                                            node.NodeValue = "TO_CHAR";
                                                        }
                                                        else
                                                        {
                                                            node.NodeValue = "TO_CHAR(TO_TIMESTAMP";
                                                            node.Children.Insert(new TreeNode(Tokens.LeftParant, ")"), node.Children.IndexOf(pictureNode.Parent) - 1);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        node.NodeValue = "TO_CHAR";
                                                        if (node.Children.Count == 5 &&
                                                            (node.Children[3] == pictureNode || (node.Children[3].Children.Count == 1 && node.Children[3].Children[0] == pictureNode)) &&
                                                            (node.Children[1].NodeType == Tokens.Column || (node.Children[1].Children.Count == 1 && node.Children[1].Children[0].NodeType == Tokens.Column)))
                                                        {
                                                            node.Children[1].NodeInfo = "SKIP";
                                                            node.Children[3].NodeInfo = "SKIP";
                                                            if (node.Children[3].Children.Count == 1)
                                                            {
                                                                node.Children[3].Children[0].NodeInfo = "SKIP";
                                                            }
                                                            if (node.Children[1].Children.Count == 1)
                                                            {
                                                                node.Children[1].Children[0].NodeInfo = "SKIP";
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        break;
                                    }

                                case "@LEFT":
                                    {
                                        //@LEFT(str, len)
                                        //SUBSTR(str, 1, len);
                                        //node.NodeValue = "SUBSTR";
                                        //node.Children.Insert(new TreeNode(Tokens.Comma, ","), 3);
                                        //node.Children.Insert(new TreeNode(Tokens.NumericConst, "1"), 3);

                                        node.NodeValue = "STRLEFT";
                                        if (!node.Parent.ToString().StartsWith("CAST"))
                                        {
                                            TreeNode parentNode = node.Parent;
                                            TreeNode strLeft = new TreeNode(Tokens.Function, "CAST");
                                            strLeft.AddChild(new TreeNode(Tokens.RightParant, "("));
                                            strLeft.AddChild(node);
                                            strLeft.AddChild(new TreeNode(Tokens.Keyword, "AS"));
                                            strLeft.AddChild(new TreeNode(Tokens.NumericConst, "VARCHAR2(254)"));
                                            strLeft.AddChild(new TreeNode(Tokens.LeftParant, ")"));
                                            int nodeIndex = parentNode.Children.IndexOf(node);
                                            parentNode.RemoveChild(node);
                                            strLeft.Parent = parentNode;
                                            parentNode.Children.Insert(strLeft, nodeIndex);
                                        }
                                        break;
                                    }

                                case "@RIGHT":
                                    {
                                        //@RIGHT(str, len)
                                        //SUBSTR(str, -len, len);

                                        //node.NodeValue = "SUBSTR";
                                        //node.Children.Insert(new TreeNode(Tokens.Comma, ","), 3);
                                        //node.Children[4].Children.Insert(new TreeNode(Tokens.LeftParant, "(", "SKIP"), 0);
                                        //node.Children[4].Children.Add(new TreeNode(Tokens.RightParant, ")", "SKIP"));
                                        //node.Children.Insert(new TreeNode(node.Children[4]), 3);
                                        //node.Children.Insert(new TreeNode(Tokens.MathOperator, "-"), 3);
                                        node.NodeValue = "STRRIGHT";
                                        if (!node.Parent.ToString().StartsWith("CAST"))
                                        {
                                            TreeNode parentNode = node.Parent;
                                            TreeNode strRight = new TreeNode(Tokens.Function, "CAST");
                                            strRight.AddChild(new TreeNode(Tokens.RightParant, "("));
                                            strRight.AddChild(node);
                                            strRight.AddChild(new TreeNode(Tokens.Keyword, "AS"));
                                            strRight.AddChild(new TreeNode(Tokens.NumericConst, "VARCHAR2(254)"));
                                            strRight.AddChild(new TreeNode(Tokens.LeftParant, ")"));
                                            int nodeIndex = parentNode.Children.IndexOf(node);
                                            parentNode.RemoveChild(node);
                                            strRight.Parent = parentNode;
                                            parentNode.Children.Insert(strRight, nodeIndex);
                                        }
                                        break;
                                    }

                                case "@STRING":
                                    {
                                        //@STRING(nr, scale)
                                        //TO_CHAR(ROUND(nr, scale))

                                        node.NodeValue = "TO_CHAR";
                                        int scale = Convert.ToInt32(node.Children[node.Children.Count - 2].Children[0].NodeValue);
                                        node.Children.Insert(new TreeNode(Tokens.Function, "ROUND"), 1);
                                        node.Children.Insert(new TreeNode(Tokens.LeftParant, "("), 2);

                                        string format = "'FM999999999999999999999999999999999990";

                                        if (!String.IsNullOrEmpty(Settings.NumberFormat))
                                        {
                                            format = Settings.NumberFormat;
                                        }

                                        string scaleFormat = "";
                                        for (int scaleCount = 0; scaleCount < scale; scaleCount++)
                                        {
                                            scaleFormat += "0";
                                        }
                                        if (!string.IsNullOrEmpty(scaleFormat))
                                        {
                                            format += ".";

                                            format += scaleFormat;
                                        }
                                        format += "'";
                                        node.Children.Add(new TreeNode(Tokens.Comma, ","));
                                        node.Children.Add(new TreeNode(Tokens.Expression, format));
                                        node.Children.Add(new TreeNode(Tokens.RightParant, ")"));
                                        break;
                                    }
                                case "@DATEVALUE":
                                    {
                                        node.NodeValue = FunctionTranslator.TranslateFunction(DatabaseBrand.Oracle, node.NodeValue);
                                        node.NodeInfo = "SKIP";
                                        bool dateOperation = false;
                                        bool isTimestampValue = false;

                                        TreeNode selectNode = targetTree.FindNode(root, Tokens.Select);
                                        TreeNode insertNode = targetTree.FindNode(root, Tokens.Insert);
                                        TreeNode updateNode = targetTree.FindNode(root, Tokens.Update);

                                        TreeNode dateOperationNode = targetTree.PreviousNode(node, Tokens.MathOperator);
                                        if (dateOperationNode != null)
                                        {
                                            dateOperation = true;
                                        }
                                        dateOperationNode = targetTree.NextNode(node, Tokens.MathOperator);
                                        if (dateOperationNode != null)
                                        {
                                            dateOperation = true;
                                        }
                                        dateOperationNode = targetTree.NextNode(node, Tokens.RelatOperator);
                                        if (dateOperationNode != null)
                                        {
                                            dateOperation = true;
                                        }
                                        dateOperationNode = targetTree.PreviousNode(node, Tokens.RelatOperator);
                                        if (dateOperationNode != null)
                                        {
                                            dateOperation = true;
                                        }

                                        TreeNode dateValue = targetTree.FindNode(node, Tokens.StringConst);
                                        if (dateValue == null)
                                        {
                                            dateValue = targetTree.FindNode(node, Tokens.DatetimeConst);
                                        }

                                        if (dateValue != null)
                                        {
                                            Match match = Regex.Match(dateValue.NodeValue, @"(:|\.)?(\d{0,10})?( |:|\.)?$");
                                            if (match.Success)
                                            {
                                                isTimestampValue = true;
                                            }
                                        }

                                        if (targetTree.FindNode(selectNode, node) || targetTree.FindNode(insertNode, node) || targetTree.FindNode(updateNode, node))
                                        {
                                            if (dateOperation)
                                            {
                                                if (isTimestampValue)
                                                {
                                                    node.Children.RemoveAt(node.Children.Count - 1);
                                                    node.Children.Insert(new TreeNode(Tokens.LeftParant, "(", "SKIP"), 0);
                                                    node.Children.Insert(new TreeNode(Tokens.Function, "TO_CHAR", "SKIP"), 1);
                                                    node.Children.Insert(new TreeNode(Tokens.LeftParant, "(", "SKIP"), 2);
                                                    node.Children.Insert(new TreeNode(Tokens.Function, "TO_TIMESTAMP", "SKIP"), 3);
                                                    node.Children.Add(new TreeNode(Tokens.Comma, ",", "SKIP"));
                                                    node.Children.Add(new TreeNode(Tokens.StringConst, "'YYYY-MM-DD HH24:MI:SS.FF'", "SKIP"));
                                                    node.Children.Add(new TreeNode(Tokens.RightParant, ")", "SKIP"));
                                                    node.Children.Add(new TreeNode(Tokens.Comma, ",", "SKIP"));
                                                    node.Children.Add(new TreeNode(Tokens.StringConst, "'yyyy-MM-dd'", "SKIP"));
                                                    node.Children.Add(new TreeNode(Tokens.RightParant, ")", "SKIP"));
                                                    node.Children.Add(new TreeNode(Tokens.Comma, ",", "SKIP"));
                                                    node.Children.Add(new TreeNode(Tokens.StringConst, "'yyyy-MM-dd'", "SKIP"));
                                                    node.Children.Add(new TreeNode(Tokens.RightParant, ")", "SKIP"));
                                                }
                                                else
                                                {
                                                    //Add date format
                                                    node.Children.RemoveAt(node.Children.Count - 1);
                                                    node.Children.Add(new TreeNode(Tokens.Comma, ",", "SKIP"));
                                                    node.Children.Add(new TreeNode(Tokens.StringConst, "'yyyy-MM-dd'", "SKIP"));
                                                    node.Children.Add(new TreeNode(Tokens.RightParant, ")", "SKIP"));
                                                    //Add string date format
                                                    node.Parent.Children.Insert(new TreeNode(Tokens.Function, "TO_CHAR", "SKIP"), 0);
                                                    node.Parent.Children.Insert(new TreeNode(Tokens.LeftParant, "(", "SKIP"), 1);
                                                    node.Parent.Children.Add(new TreeNode(Tokens.Comma, ",", "SKIP"));
                                                    node.Parent.Children.Add(new TreeNode(Tokens.StringConst, "'yyyy-MM-dd'", "SKIP"));
                                                    node.Parent.Children.Add(new TreeNode(Tokens.RightParant, ")", "SKIP"));
                                                }
                                            }
                                            else
                                            {
                                                node.NodeValue = "TO_CHAR(" + node.NodeValue;
                                                node.Children.Add(new TreeNode(Tokens.Comma, ",", "SKIP"));
                                                node.Children.Add(new TreeNode(Tokens.StringConst, "'yyyy-MM-dd'", "SKIP"));
                                                node.Children.Add(new TreeNode(Tokens.RightParant, ")", "SKIP"));
                                            }
                                        }

                                        break;
                                    }
                                case "@MONTHBEG":
                                    {
                                        TreeNode timestampNode = targetTree.FindNode(node, "SYSTIMESTAMP");
                                        if (timestampNode == null)
                                        {
                                            timestampNode = targetTree.FindNode(node, "SYSDATETIME");
                                        }
                                        if (timestampNode != null && (timestampNode.NodeValue == "SYSTIMESTAMP" || timestampNode.NodeValue == "SYSDATETIME"))
                                        {
                                            timestampNode.NodeValue = "SYSTIMESTAMP";
                                            timestampNode.NodeInfo = "SKIP";
                                            node.Children.Insert(new TreeNode(Tokens.Function, "TO_CHAR"), 1);
                                            node.Children.Insert(new TreeNode(Tokens.LeftParant, "("), 2);
                                            node.Children.Insert(new TreeNode(Tokens.Comma, ","), node.Children.Count - 1);
                                            node.Children.Insert(new TreeNode(Tokens.StringConst, "'YYYY-MM-DD'"), node.Children.Count - 1);
                                            node.Children.Add(new TreeNode(Tokens.RightParant, ")"));

                                        }
                                        node.NodeValue = FunctionTranslator.TranslateFunction(DatabaseBrand.Oracle, node.NodeValue);
                                        break;
                                    }
                                //For the rest of the functions we can write user-defined functions in MSSQL
                                default:
                                    {
                                        node.NodeValue = FunctionTranslator.TranslateFunction(DatabaseBrand.Oracle, node.NodeValue);
                                        node.NodeInfo = "SKIP";
                                        if ((node.NodeValue == "GETMONTH" || node.NodeValue == "GETYEAR" || node.NodeValue == "GETDAY")
                                            && targetTree.FindNode(node, "@NOW") == null && targetTree.FindNode(node, "SYSDATE") == null)
                                        {
                                            TreeNodeCollection expressionNodes = new TreeNodeCollection();
                                            TreeNodeCollection functionNodes = new TreeNodeCollection();
                                            targetTree.FindAll(node, Tokens.Expression, ref expressionNodes);
                                            foreach (TreeNode expressionNode in expressionNodes)
                                            {
                                                targetTree.FindAll(expressionNode, Tokens.Function, ref functionNodes);
                                            }
                                            bool bAddRelativeDate = true;
                                            foreach (TreeNode functionNode in functionNodes)
                                            {
                                                if (functionNode.NodeValue != "@NULLVALUE" && functionNode.NodeValue != "NVL")
                                                {
                                                    bAddRelativeDate = false;
                                                    break;
                                                }
                                            }

                                            if (bAddRelativeDate && targetTree.FindNode(node, Tokens.MathOperator) != null &&
                                                targetTree.FindNode(node, "MONTHS") == null && targetTree.FindNode(node, "MONTH") == null &&
                                                targetTree.FindNode(node, "YEAR") == null && targetTree.FindNode(node, "YEARS") == null &&
                                                targetTree.FindNode(node, Tokens.NumericConst) == null)
                                            {
                                                //TO_DATE('1899-12-30', 'YYYY-MM-DD')                           +
                                                node.Children.Insert(new TreeNode(Tokens.LeftParant, "(", "SKIP"), 0);
                                                node.Children.Insert(new TreeNode(Tokens.Function, "TO_DATE", "SKIP"), 1);
                                                node.Children.Insert(new TreeNode(Tokens.LeftParant, "(", "SKIP"), 2);
                                                node.Children.Insert(new TreeNode(Tokens.DatetimeConst, "'1899-12-30'", "SKIP"), 3);
                                                node.Children.Insert(new TreeNode(Tokens.Comma, ",", "SKIP"), 4);
                                                node.Children.Insert(new TreeNode(Tokens.StringConst, "'YYYY-MM-DD'", "SKIP"), 5);
                                                node.Children.Insert(new TreeNode(Tokens.RightParant, ")", "SKIP"), 6);
                                                node.Children.Insert(new TreeNode(Tokens.MathOperator, "+", "SKIP"), 7);
                                                node.Children.Add(new TreeNode(Tokens.RightParant, ")", "SKIP"));
                                            }
                                        }
                                        if (node.NodeValue == "TRIM")
                                        {
                                            int insertIndex = 0;
                                            TreeNode leftParanNode = targetTree.FindNode(node, Tokens.LeftParant);
                                            if (leftParanNode != null)
                                            {
                                                insertIndex = node.Children.IndexOf(leftParanNode);
                                            }

                                            node.Children.Insert(new TreeNode(Tokens.Function, "REPLACE", "SKIP"), insertIndex + 1);
                                            node.Children.Insert(new TreeNode(Tokens.LeftParant, "(", "SKIP"), insertIndex + 2);
                                            node.Children.Insert(new TreeNode(Tokens.Function, "REPLACE", "SKIP"), insertIndex + 3);
                                            node.Children.Insert(new TreeNode(Tokens.LeftParant, "(", "SKIP"), insertIndex + 4);

                                            TreeNode rightParanNode = targetTree.FindNode(node, Tokens.RightParant);
                                            if (rightParanNode != null)
                                            {
                                                insertIndex = node.Children.IndexOf(rightParanNode);
                                            }

                                            if (insertIndex < 0 || rightParanNode == null)
                                            {
                                                insertIndex = node.Children.Count - 1;
                                            }

                                            node.Children.Insert(new TreeNode(Tokens.Comma, ",", "SKIP"), insertIndex);
                                            node.Children.Insert(new TreeNode(Tokens.Expression, "CHR(10)", "SKIP"), insertIndex + 1);
                                            node.Children.Insert(new TreeNode(Tokens.Comma, ",", "SKIP"), insertIndex + 2);
                                            node.Children.Insert(new TreeNode(Tokens.StringConst, "''", "SKIP"), insertIndex + 3);
                                            node.Children.Insert(new TreeNode(Tokens.RightParant, ")", "SKIP"), insertIndex + 4);
                                            node.Children.Insert(new TreeNode(Tokens.Comma, ",", "SKIP"), insertIndex + 5);
                                            node.Children.Insert(new TreeNode(Tokens.Expression, "CHR(13)", "SKIP"), insertIndex + 6);
                                            node.Children.Insert(new TreeNode(Tokens.Comma, ",", "SKIP"), insertIndex + 7);
                                            node.Children.Insert(new TreeNode(Tokens.StringConst, "''", "SKIP"), insertIndex + 8);
                                            node.Children.Insert(new TreeNode(Tokens.RightParant, ")", "SKIP"), insertIndex + 9);
                                        }
                                        break;
                                    }
                            }
                            break;
                            #endregion Functions
                        }

                    case Tokens.JoinOperator: //Replace old join syntax with the new one
                        {
                            if (node.NodeInfo.Contains("LEFT") || node.NodeInfo.Contains("RIGHT"))
                            {
                                bool bOk = true;
                                TreeNode nodeWhere = targetTree.FindNode(targetTree.root, "WHERE");
                                TreeNode subselect = targetTree.FindNode(nodeWhere, "SELECT");
                                if (subselect != null)
                                {
                                    nodeWhere = targetTree.FindNode(subselect, "WHERE");
                                    if (nodeWhere == null)
                                    {
                                        nodeWhere = targetTree.FindNode(subselect.Parent, "WHERE");
                                    }
                                    if (nodeWhere != null)
                                    {
                                        if (targetTree.FindNode(nodeWhere, node) && targetTree.FindNode(nodeWhere, node.Parent))
                                        {
                                            bOk = false;
                                        }
                                    }
                                }

                                if (bOk)
                                {
                                    TreeNodeCollection joinNodes = new TreeNodeCollection();
                                    TreeNodeCollection joinNodesTbl = new TreeNodeCollection();
                                    Dictionary<string, Dictionary<string, TreeNodeCollection>> joinNodesWithTables = new Dictionary<string, Dictionary<string, TreeNodeCollection>>();
                                    targetTree.FindAll(root, Tokens.JoinOperator, ref joinNodes);
                                    if (joinNodes.Count > 1)
                                    {
                                        foreach (TreeNode joinOpNode in joinNodes)
                                        {
                                            TreeNode parentNode = joinOpNode.Parent;
                                            TreeNode joinOp = targetTree.FindNode(parentNode, Tokens.JoinOperator);
                                            int joinOperator = parentNode.Children.IndexOf(joinOp);
                                            TreeNode joinNode;
                                            TreeNode joinedNode;
                                            if (joinOperator > 0)
                                            {
                                                joinNode = parentNode.Children[joinOperator - 1];
                                                if (joinOperator == parentNode.Children.Count - 1)
                                                {
                                                    joinedNode = parentNode.Children[0];
                                                }
                                                else
                                                {
                                                    joinedNode = parentNode.Children[parentNode.Children.Count - 1];
                                                }
                                            }
                                            else
                                            {
                                                joinNode = parentNode.Children[joinOperator + 1];
                                                if (joinOperator == parentNode.Children.Count - 1)
                                                {
                                                    joinedNode = parentNode.Children[0];
                                                }
                                                else
                                                {
                                                    joinedNode = parentNode.Children[parentNode.Children.Count - 1];
                                                }
                                            }
                                            if (joinedNode.Children[0].NodeType == Tokens.Column)
                                            {
                                                TreeNodeCollection tables = new TreeNodeCollection();
                                                targetTree.FindAll(targetTree.root.Children[0], Tokens.Table, ref tables);
                                                string tableNode = GetTable(joinNode.Children[0], tables).NodeValue;
                                                string joinedTable = GetTable(joinedNode.Children[0], tables).NodeValue;

                                                if (!joinNodesWithTables.ContainsKey(tableNode))
                                                {
                                                    joinNodesWithTables.Add(tableNode, new Dictionary<string, TreeNodeCollection>());
                                                    joinNodesWithTables[tableNode].Add(joinedTable, new TreeNodeCollection());
                                                    joinNodesTbl = joinNodesWithTables[tableNode][joinedTable];
                                                    joinNodesTbl.Add(parentNode);
                                                }
                                                else
                                                {
                                                    if (!joinNodesWithTables[tableNode].ContainsKey(joinedTable))
                                                    {
                                                        joinNodesWithTables[tableNode].Add(joinedTable, new TreeNodeCollection());
                                                        joinNodesTbl = joinNodesWithTables[tableNode][joinedTable];
                                                        joinNodesTbl.Add(parentNode);
                                                    }
                                                    else
                                                    {
                                                        joinNodesTbl = joinNodesWithTables[tableNode][joinedTable];

                                                        if (!joinNodesTbl.Contains(parentNode))
                                                        {
                                                            joinNodesTbl.Add(parentNode);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        foreach (KeyValuePair<string, Dictionary<string, TreeNodeCollection>> joinTblValue in joinNodesWithTables)
                                        {
                                            if (joinTblValue.Value.Count > 1)
                                            {
                                                int index = 0;
                                                foreach (KeyValuePair<string, TreeNodeCollection> joinValue in joinTblValue.Value)
                                                {
                                                    if (index > 0)
                                                    {
                                                        foreach (TreeNode join in joinValue.Value)
                                                        {
                                                            TreeNode joinOp = targetTree.FindNode(join, Tokens.JoinOperator);
                                                            TreeNode relatOp = targetTree.FindNode(join, Tokens.RelatOperator);
                                                            TreeNode joinNode = new TreeNode(Tokens.Predicate);

                                                            TreeNodeCollection tables = new TreeNodeCollection();
                                                            targetTree.FindAll(join, Tokens.Expression, ref tables);

                                                            TreeNode table1 = new TreeNode(Tokens.Table, tables[0].Children[0].NodeValue.Split('.')[0]);
                                                            TreeNode table2 = new TreeNode(Tokens.Table, tables[1].Children[0].NodeValue.Split('.')[0]);

                                                            int joinOperator = join.Children.IndexOf(joinOp);
                                                            int relatOperator = join.Children.IndexOf(relatOp);
                                                            TreeNode selectNode;
                                                            TreeNode joinedNode;
                                                            if (joinOperator > relatOperator)
                                                            {
                                                                selectNode = join.Children[relatOperator - 1];
                                                                joinedNode = join.Children[relatOperator + 1];
                                                            }
                                                            else
                                                            {
                                                                selectNode = join.Children[relatOperator + 1];
                                                                joinedNode = join.Children[0];
                                                            }
                                                            joinNode.Children.Clear();
                                                            joinNode.Children.Add(selectNode);
                                                            joinNode.Children.Add(new TreeNode(Tokens.Keyword, "IN (SELECT"));
                                                            joinNode.Children.Add(selectNode);
                                                            joinNode.Children.Add(new TreeNode(Tokens.Keyword, "FROM"));
                                                            joinNode.Children.Add(new TreeNode(Tokens.Table, joinValue.Key));
                                                            joinNode.Children.Add(new TreeNode(Tokens.Comma, ","));
                                                            joinNode.Children.Add(new TreeNode(Tokens.Table, joinTblValue.Key));
                                                            joinNode.Children.Add(new TreeNode(Tokens.Keyword, "WHERE"));
                                                            joinNode.Children.Add(join);
                                                            joinNode.Children.Add(new TreeNode(Tokens.RightParant, ")"));
                                                            joinNode.NodeInfo = "SKIP";

                                                            if (join.Parent.Children.Contains(join))
                                                            {
                                                                join.Parent.Children.Replace(join, joinNode);
                                                            }

                                                            nodeWhere = targetTree.FindNode(targetTree.root, "WHERE");
                                                            subselect = targetTree.FindNode(nodeWhere, "SELECT");
                                                            if (subselect != null)
                                                            {
                                                                nodeWhere = targetTree.FindNode(subselect, "WHERE");
                                                                if (nodeWhere == null)
                                                                {
                                                                    nodeWhere = targetTree.FindNode(subselect.Parent, "WHERE");
                                                                }
                                                                if (nodeWhere != null)
                                                                {
                                                                    if (targetTree.FindNode(nodeWhere, node) && targetTree.FindNode(nodeWhere, node.Parent))
                                                                    {
                                                                        selectNode = nodeWhere.Parent;
                                                                        nodeWhere = targetTree.FindNode(selectNode, "WHERE");
                                                                        if (targetTree.FindAll(nodeWhere, Tokens.Function) == 0)
                                                                        {
                                                                            TreeNode fromNode = targetTree.FindNode(selectNode, "FROM");
                                                                            //selectNode = targetTree.FindNode(selectNode, "SELECT");
                                                                            TreeNodeCollection cols = new TreeNodeCollection();
                                                                            targetTree.FindAll(selectNode, Tokens.Column, ref cols);
                                                                            tables = new TreeNodeCollection();
                                                                            targetTree.FindAll(targetTree.root, Tokens.Table, ref tables);
                                                                            foreach (TreeNode col in cols)
                                                                            {
                                                                                TreeNode missingTable = GetTable(col, tables);
                                                                                if (missingTable != null && !fromNode.Children.Contains(missingTable))
                                                                                {
                                                                                    fromNode.AddChild(new TreeNode(Tokens.Comma, ","));
                                                                                    fromNode.AddChild(missingTable);
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                    index++;
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    TreeNode nvlNode = targetTree.FindNode(root, "NVL");
                                    TreeNode subSelect = subselect.Parent;
                                    if (nvlNode != null && !subSelect.ToString().StartsWith("NVL"))
                                    {
                                        TreeNode nvlValue = nvlNode.Children[nvlNode.Children.Count - 2];
                                        subSelect.InsertChild(new TreeNode(Tokens.Function, "NVL", "SKIP"), 0);
                                        subSelect.InsertChild(new TreeNode(Tokens.LeftParant, "(", "SKIP"), 1);
                                        subSelect.InsertChild(new TreeNode(Tokens.LeftParant, "(", "SKIP"), 2);
                                        subSelect.AddChild(new TreeNode(Tokens.RightParant, ")", "SKIP"));
                                        subSelect.AddChild(new TreeNode(Tokens.Comma, ",", "SKIP"));
                                        subSelect.AddChild(nvlValue);
                                        subSelect.AddChild(new TreeNode(Tokens.RightParant, ")", "SKIP"));
                                    }
                                }
                            }
                            //TranslateOuterJoins(targetTree, root, node);
                            break;
                        }

                    case Tokens.SysKeyword:
                        {
                            #region SysKeywords
                            //These syskeywords can appear in expressions like: expr +|- const syskeyword
                            switch (node.NodeValue)
                            {
                                //Use ADD_MONTHS(date, integer)
                                case "YEAR":
                                case "YEARS":
                                case "MONTH":
                                case "MONTHS":
                                    {
                                        //Find parent of the node
                                        TreeNode exprNode = targetTree.FindParent(root, node);
                                        TreeNode functionExprNode = new TreeNode(Tokens.Expression);
                                        TreeNode parentNode = functionExprNode;
                                        TreeNode currentNode = node;
                                        TreeNode argNode = null;
                                        bool isYear = false;
                                        int index = 0;

                                        do
                                        {
                                            TreeNode addMonths = new TreeNode(Tokens.Function, "NON_ANSI_ADD_MONTHS");
                                            parentNode.AddChild(addMonths);

                                            argNode = new TreeNode(Tokens.Expression);
                                            addMonths.AddChild(argNode);
                                            argNode.AddChild(new TreeNode(Tokens.LeftParant, "("));
                                            argNode.AddChild(new TreeNode(Tokens.Comma, ","));
                                            if (node.Parent.ToString().StartsWith("+") || node.Parent.ToString().StartsWith("-"))
                                            {
                                                int indexOfNode = node.Parent.Parent.Parent.Children.IndexOf(node.Parent.Parent);
                                                int counter = indexOfNode - 1;
                                                TreeNode prev = node.Parent.Parent.Parent.Children[counter];
                                                while (prev.NodeType != Tokens.Predicate && counter >= 0)
                                                {
                                                    counter--;
                                                    prev = node.Parent.Parent.Parent.Children[counter];
                                                }
                                                if (prev.Children[prev.Children.Count - 1].Children.Count > 0
                                                    && (prev.Children[prev.Children.Count - 1].Children[0].NodeType == Tokens.StringConst ||
                                                    prev.Children[prev.Children.Count - 1].Children[0].NodeType == Tokens.DatetimeConst)
                                                    && prev.Children[prev.Children.Count - 2].NodeType == Tokens.LeftParant)
                                                {
                                                    CustomDateTime date = new CustomDateTime();
                                                    if (IsValidDateTime(prev.Children[prev.Children.Count - 1].ToString().Replace("'", "").Trim(), out date) ||
                                                        prev.Children[prev.Children.Count - 1].ToString().StartsWith("TO_DATE"))
                                                    {
                                                        node.Parent.Children.Insert(prev.Children[prev.Children.Count - 1], 0);
                                                        prev.Children.RemoveAt(prev.Children.Count - 1);
                                                        //node.Parent.Children.Insert(prev.Children[prev.Children.Count - 1], 0);
                                                        if (prev.Children[prev.Children.Count - 1].NodeType == Tokens.LeftParant)
                                                        {
                                                            prev.Children.RemoveAt(prev.Children.Count - 1);
                                                            if ((prev.Parent.Children.IndexOf(node.Parent.Parent) - prev.Parent.Children.IndexOf(prev)) == 2)
                                                            {
                                                                int indexBetween = prev.Parent.Children.IndexOf(prev) + 1;
                                                                if (prev.Parent.Children[indexBetween].NodeType == Tokens.RightParant)
                                                                {
                                                                    prev.Parent.Children.RemoveAt(indexBetween);
                                                                }
                                                            }
                                                        }
                                                        TreeNode mathOperator = targetTree.FindNode(node.Parent, Tokens.MathOperator);
                                                        if (mathOperator == null)
                                                        {
                                                            TreeNode numericConstant = targetTree.FindNode(node.Parent, Tokens.NumericConst);
                                                            if (numericConstant.ToString().StartsWith("+") || numericConstant.ToString().StartsWith("-"))
                                                            {
                                                                mathOperator = new TreeNode(Tokens.MathOperator, numericConstant.ToString().Substring(0, 1));
                                                                numericConstant.NodeValue = numericConstant.NodeValue.Remove(0, 1);
                                                                node.Parent.InsertChild(mathOperator, node.Parent.Children.IndexOf(numericConstant));
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            index = exprNode.Children.IndexOf(node);
                                            isYear = currentNode.NodeValue.StartsWith("YEAR");
                                            exprNode.RemoveChild(currentNode);

                                            index -= 2;
                                            if (index >= 0)
                                            {
                                                currentNode = exprNode.Children[index]; // +|-
                                                argNode.AddChild(currentNode);

                                                exprNode.RemoveChild(currentNode);

                                                currentNode = exprNode.Children[index]; //numeric constant
                                                argNode.AddChild(currentNode);
                                                if (isYear)
                                                {
                                                    argNode.AddChild(new TreeNode(Tokens.Expression, "* 12"));
                                                }

                                                exprNode.RemoveChild(currentNode);

                                                if (index > exprNode.Children.Count)
                                                {
                                                    while (exprNode.Children[index].NodeType == Tokens.MathOperator)
                                                    {
                                                        currentNode = exprNode.Children[index]; // +|-
                                                        argNode.AddChild(currentNode);

                                                        exprNode.RemoveChild(currentNode);

                                                        currentNode = exprNode.Children[index]; // numeric constant
                                                        argNode.AddChild(currentNode);

                                                        exprNode.RemoveChild(currentNode);
                                                    }
                                                }

                                                TreeNode subExprNode = new TreeNode(Tokens.Expression);
                                                argNode.InsertChild(subExprNode, 1);

                                                string nodeExpressionValue = "";
                                                foreach (TreeNode nodeExpression in exprNode.Children)
                                                {
                                                    nodeExpressionValue += nodeExpression.NodeValue;
                                                }
                                                if (string.Compare(nodeExpressionValue, "TRUNC(SYSDATE)") != 0 && nodeExpressionValue.Contains("TRUNC"))
                                                {
                                                    index--;
                                                }
                                                else if (nodeExpressionValue.Contains("@NOW"))
                                                {
                                                    index--;
                                                }
                                                else if (targetTree.FindNode(node.Parent, Tokens.DatetimeConst) != null
                                                    && targetTree.FindNode(node.Parent, Tokens.DatetimeConst).Children.Count == 0)
                                                {
                                                    index--;
                                                }
                                                else if (nodeExpressionValue.Contains("DAY"))
                                                {
                                                    index--;
                                                }

                                                if (index == exprNode.Children.Count)
                                                {
                                                    index--;
                                                }
                                                if (node.Parent.NodeValue == "CAST" || node.Parent.NodeValue == "CAST(")
                                                {
                                                    index--;
                                                }

                                                if (exprNode.Children.Count == 3)
                                                {
                                                    if (exprNode.Children[1].NodeType == Tokens.MathOperator && exprNode.Children[2].NodeType == Tokens.NumericConst && index == 1)
                                                    {
                                                        index--;
                                                    }
                                                }

                                                currentNode = exprNode.Children[index];
                                                parentNode = subExprNode;
                                            }

                                            argNode.AddChild(new TreeNode(Tokens.RightParant, ")"));
                                            if (index < 0)
                                            {
                                                break;
                                            }

                                        } while (currentNode.NodeValue == "YEAR" || currentNode.NodeValue == "YEARS" ||
                                                 currentNode.NodeValue == "MONTH" || currentNode.NodeValue == "MONTHS");

                                        int index2 = 0;
                                        while (index2 <= index)
                                        {
                                            parentNode.AddChild(exprNode.Children[0]);
                                            exprNode.RemoveChild(exprNode.Children[0]);
                                            index2++;
                                        }

                                        exprNode.Children.Insert(functionExprNode, 0);
                                        manyWithOneReplaced = index2;
                                        break;
                                    }

                                //Use INTERVAL
                                //e.g. col + 1 HOUR -> col + INTERVAL '1' HOUR
                                case "HOUR":
                                case "HOURS":
                                case "MINUTE":
                                case "MINUTES":
                                case "SECOND":
                                case "SECONDS":
                                    {
                                        if (node.NodeValue.EndsWith("S"))
                                        {
                                            node.NodeValue = node.NodeValue.Remove(node.NodeValue.Length - 1);
                                        }
                                        node.NodeInfo = "SKIP";
                                        TreeNode exprNode = targetTree.FindParent(root, node);
                                        int index = exprNode.Children.IndexOf(node);
                                        exprNode.Children[index - 1].NodeValue = "'" + exprNode.Children[index - 1].NodeValue + "'";
                                        exprNode.Children.Insert(new TreeNode(Tokens.KeyName, "INTERVAL"), index - 1);
                                        break;
                                    }

                                //Oracle supports expressions like:  date_expr + constant, where constant is interpreted in days
                                case "DAY":
                                case "DAYS":
                                    {
                                        node.NodeInfo = "NOT USED";
                                        break;
                                    }

                                //Can't translate microseconds because the date datatype doesn't have microseconds
                                case "MICROSECOND":
                                case "MICROSECONDS":
                                    {
                                        throw new NotSupportedException("System Keyword " + node.NodeValue + " is not supported by SqlTranslator");

                                    }

                                case "SYSDATETIME":
                                    {
                                        node.NodeValue = "SYSTIMESTAMP";
                                        TreeNode parentNode = targetTree.FindTopmostExpression(node);
                                        TreeNode selectNode = targetTree.FindNode(root, "SELECT");
                                        if (parentNode != null
                                            && parentNode.Parent != null
                                            && parentNode.Parent.Parent != null
                                            && parentNode.Parent.Parent.NodeType == Tokens.Select
                                            && selectNode != null
                                            && targetTree.FindNode(selectNode, node)
                                            && !targetTree.IsNodeFromCase(root, node)
                                            && !node.Parent.Parent.ToString().StartsWith("TO_CHAR"))
                                        {
                                            node.NodeValue = "TO_CHAR(" + node.NodeValue + ", 'YYYY-MM-DD HH24:MI:SS.FF6')";
                                            if (parentNode.Children[0].NodeValue == "NVL")
                                            {
                                                TreeNode nvlNode = parentNode.Children[0];
                                                if (nvlNode.Children[1].NodeType == Tokens.Expression)
                                                {
                                                    string nodeValue = nvlNode.Children[1].ToString();
                                                    if (nodeValue.StartsWith("TO_"))
                                                    {
                                                        string cast = "";
                                                        string format = "";
                                                        cast = nodeValue.Split('(')[0];
                                                        format = nodeValue.Split(',')[1].Replace(")", "").Trim();
                                                        if (cast != "TO_CHAR")
                                                        {
                                                            node.NodeValue = cast + "(" + node.NodeValue;
                                                            if (!string.IsNullOrEmpty(format))
                                                            {
                                                                node.NodeValue += ", " + format + ")";
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }

                                        ConvertWithFormat(targetTree, root, node);
                                        break;
                                    }
                                case "SYSTIME":
                                    {
                                        node.NodeValue = "SYSDATE";
                                        ConvertWithFormat(targetTree, root, node);
                                        break;
                                    }

                                case "SYSDATE":
                                    {
                                        node.NodeValue = "TRUNC(SYSDATE)";
                                        ConvertWithFormat(targetTree, root, node);
                                        break;
                                    }

                                case "SYSDBTRANSID":
                                case "SYSTIMEZONE":
                                    {
                                        throw new NotSupportedException("System Keyword " + node.NodeValue + " is not supported by SqlTranslator");
                                    }

                                case "ROWID":
                                    {
                                        //ROWID is a system column in Oracle but it can't be used like SqlBase's ROWID
                                        node.NodeValue = "ROWVERSION";
                                    }
                                    break;
                                case "NULL":
                                    {
                                        bool date = false;
                                        bool isDate = false;
                                        bool isTime = false;
                                        bool isTimestamp = false;
                                        int milliseconds = 0;
                                        TreeNode decodeNode = targetTree.FindParent(node, "DECODE");
                                        if (decodeNode == null)
                                        {
                                            decodeNode = targetTree.FindNode(root, "@DECODE");
                                        }
                                        if (decodeNode != null)
                                        {
                                            TreeNodeCollection decodeCols = new TreeNodeCollection();
                                            targetTree.FindAll(decodeNode, Tokens.Column, ref decodeCols);
                                            foreach (TreeNode col in decodeCols)
                                            {
                                                this.GetColumnDateTimeProperties(GetColumnName(col.NodeInfo), out isDate, out isTime, out isTimestamp, out milliseconds);
                                                TreeNode strNode = targetTree.FindNode(decodeNode, "CAST");
                                                if (strNode != null)
                                                {
                                                    if (strNode.Children[strNode.Children.Count - 3].NodeValue == "AS" && strNode.Children[strNode.Children.Count - 2].NodeValue == "VARCHAR2(254)")
                                                    {
                                                        if (targetTree.FindNode(strNode, col))
                                                        {
                                                            if (isDate)
                                                            {
                                                                isDate = false;
                                                            }
                                                        }
                                                    }
                                                }

                                                if (isDate)
                                                {
                                                    strNode = targetTree.FindNode(decodeNode, "STRLEFT");
                                                    if (targetTree.FindNode(strNode, col))
                                                    {
                                                        isDate = false;
                                                    }
                                                }

                                                if (isDate)
                                                {
                                                    strNode = targetTree.FindNode(decodeNode, "STRRIGHT");
                                                    if (targetTree.FindNode(strNode, col))
                                                    {
                                                        isDate = false;
                                                    }
                                                }

                                                if (!isDate)
                                                {
                                                    date = false;
                                                    break;
                                                }
                                                else
                                                {
                                                    date = true;
                                                }
                                            }
                                            //if (date)
                                            //{
                                            //    node.NodeValue = "TO_DATE(" + node.NodeValue + ")";
                                            //}
                                        }
                                        break;
                                    }
                            }
                            break;
                            #endregion
                        }

                    case Tokens.Keyword:
                        {
                            switch (node.NodeValue)
                            {
                                case "CURRENT":
                                //case "FOR":
                                //case "CHECK EXISTS":
                                case "ADJUSTING":
                                case "RESTRICT":
                                case "SET NULL":
                                case "TRANSACTION":
                                case "FORCE":
                                case "DISTINCTCOUNT":
                                    {
                                        throw new NotSupportedException("Keyword " + node.NodeValue + " is not supported by SqlTranslator");
                                    }

                                case "PCTFREE":
                                case "SIZE":
                                case "CLUSTERED HASHED":
                                    {
                                        node.NodeInfo = "NOT USED";
                                        break;
                                    }

                                //this clause is ignored by SqlBase so we can delete it without throwing an exception
                                case "IN DATABASE":
                                case "IN":
                                    {
                                        if (node.NodeInfo == "CREATE")
                                        {
                                            targetTree.RemoveNode(node);
                                        }
                                        break;
                                    }

                                case "NOT NULL WITH DEFAULT":
                                    {
                                        //If AlterColumn stored procedure has to be executed, 
                                        //then replacing NOT NULL WITH DEFAULT with correct value will be done in
                                        //AlterColumn where NOT NULL WITH DEFAULT is expected as last parameter
                                        //TreeNode nodeDataType = targetTree.FindNode(nodeParent, Tokens.DataType);
                                        TreeNode temp = targetTree.FindNode(root, Tokens.Statement);
                                        if (temp == null || !targetTree.FindNode(root, Tokens.Statement).NodeValue.ToUpper().StartsWith("EXEC ALTERCOLUMN"))
                                        {
                                            TreeNode nodeParent = targetTree.FindParent(root, node);

                                            TreeNode nodeDataType = nodeParent.Children[nodeParent.Children.IndexOf(node) - 1];
                                            string colDataType = nodeDataType.Children[0].NodeValue;

                                            node.NodeType = Tokens.Expression;
                                            node.NodeValue = "";

                                            node.AddChild(new TreeNode(Tokens.Keyword, "DEFAULT", "SKIP"));
                                            switch (colDataType)
                                            {
                                                case "NCHAR":
                                                case "CHAR":
                                                case "NVARCHAR2":
                                                case "VARCHAR2":
                                                    {
                                                        node.AddChild(new TreeNode(Tokens.StringConst, @"' '", "SKIP"));
                                                        break;
                                                    }

                                                case "NUMBER":
                                                case "INT":
                                                case "SMALLINT":
                                                case "FLOAT":
                                                    {
                                                        node.AddChild(new TreeNode(Tokens.NumericConst, "0", "SKIP"));
                                                        break;
                                                    }

                                                case "DATE":
                                                case "TIMESTAMP":
                                                    {
                                                        node.AddChild(new TreeNode(Tokens.SysKeyword, "CURRENT_TIMESTAMP", "SKIP"));
                                                        break;
                                                    }
                                            }

                                            node.AddChild(new TreeNode(Tokens.Keyword, "NOT NULL", "SKIP"));
                                        }

                                        break;
                                    }


                                case "ORDER BY":
                                    {
                                        //ORDER BY items must appear in the select list if SELECT DISTINCT is specified
                                        TreeNode select = targetTree.FindNode(root, "SELECT");
                                        TreeNode distinct = targetTree.FindNode(select, "DISTINCT");
                                        if (distinct != null)
                                        {
                                            TreeNodeCollection orderbyColumns = new TreeNodeCollection();
                                            TreeNodeCollection notFoundOrderbyColumns = new TreeNodeCollection();
                                            targetTree.FindAll(node, Tokens.Column, ref orderbyColumns);
                                            bool found = false;
                                            foreach (TreeNode column in orderbyColumns)
                                            {
                                                found = false;
                                                foreach (TreeNode selectColumn in select.Children)
                                                {
                                                    if (selectColumn.NodeType == Tokens.Expression)
                                                    {
                                                        string columnName = selectColumn.ToString().ToUpper().Trim();
                                                        //also remove the surrounding paranthesis
                                                        if (columnName.StartsWith("(") && columnName.EndsWith(")"))
                                                        {
                                                            columnName = columnName.Substring(1, columnName.Length - 2).Trim();
                                                        }

                                                        //check the column names without the table name
                                                        if (column.NodeValue.Substring(column.NodeValue.IndexOf(".") + 1).ToUpper() ==
                                                            columnName.Substring(columnName.IndexOf(".") + 1).ToUpper())
                                                        {
                                                            found = true;
                                                            break;
                                                        }
                                                    }
                                                }
                                                if (!found)
                                                {
                                                    notFoundOrderbyColumns.Add(column);
                                                }
                                            }
                                            if (notFoundOrderbyColumns.Count > 0)
                                            {
                                                //The statement has to be translated like this:
                                                //*****Original******
                                                //SELECT DISTINCT col1, col2, ..., coln
                                                //FROM TABLENAME
                                                //ORDER BY x
                                                //****Translated******
                                                //SELECT col1, col2, ..., coln
                                                //FROM TABLENAME
                                                //GROUP BY col1, col2, ..., coln
                                                //ORDER BY MIN(x)
                                                distinct.NodeInfo = "NOT USED";

                                                TreeNode groupBy = targetTree.FindNode(select, "GROUP BY");
                                                if (groupBy == null)
                                                {
                                                    groupBy = new TreeNode(Tokens.Keyword, "GROUP BY");
                                                    root.InsertChild(groupBy, root.Children.IndexOf(node));
                                                }

                                                string groupByString = groupBy.ToString();
                                                foreach (TreeNode selectColumn in select.Children)
                                                {
                                                    if (!groupByString.Contains(selectColumn.ToString()) && targetTree.FindNode(selectColumn, Tokens.Column) != null)
                                                    {
                                                        if (groupBy.Children.Count > 0)
                                                        {
                                                            groupBy.Children.Add(new TreeNode(Tokens.Comma, ","));
                                                        }
                                                        TreeNode columnToAdd = new TreeNode(selectColumn, "");
                                                        TreeNode asNode = targetTree.FindNode(columnToAdd, "AS");
                                                        if (asNode != null)
                                                        {
                                                            columnToAdd.Children.Remove(asNode);
                                                            if (columnToAdd.Children.Count > 1)
                                                            {
                                                                columnToAdd.Children.RemoveAt(columnToAdd.Children.Count - 1);
                                                            }
                                                        }
                                                        groupBy.Children.Add(new TreeNode(columnToAdd));
                                                    }
                                                }

                                                foreach (TreeNode column in notFoundOrderbyColumns)
                                                {
                                                    TreeNode expr = new TreeNode(Tokens.Expression);
                                                    expr.AddChild(new TreeNode(Tokens.Function, "MIN"));
                                                    expr.AddChild(new TreeNode(Tokens.LeftParant, "("));
                                                    expr.AddChild(column);
                                                    expr.AddChild(new TreeNode(Tokens.RightParant, ")"));

                                                    node.Children[node.Children.IndexOf(column)] = expr;
                                                }
                                            }
                                            node.NodeInfo = "SKIP";
                                        }

                                        //In SqlBase the null values are the first ones
                                        for (int i = 0; i <= node.Children.Count; i++)
                                        {
                                            if (i == node.Children.Count || node.Children[i].NodeType == Tokens.Comma)
                                            {
                                                if (node.Children[i - 1].NodeValue != "NULLS LAST" && node.Children[i - 1].NodeValue != "NULLS FIRST")
                                                {
                                                    if (node.Children[i - 1].NodeValue == "DESC")
                                                    {
                                                        node.InsertChild(new TreeNode(Tokens.SysKeyword, "NULLS LAST"), i++);
                                                    }
                                                    else
                                                    {
                                                        node.InsertChild(new TreeNode(Tokens.SysKeyword, "NULLS FIRST"), i++);
                                                    }
                                                }
                                            }
                                        }
                                        break;
                                    }

                                //Oracle doens't support GROUP BY integer constant
                                //have to replace constants with column names
                                case "GROUP BY":
                                    {
                                        //in SqlBase GROUP BY is also ordering the resultset but in Oracle it doesn't.
                                        //In case there is no ORDER BY in the statement we have to add one with the list from GROUP BY
                                        //Only if ORDER BY is not in a subquery
                                        if (targetTree.FindNode(root, "ORDER BY") == null)
                                        {
                                            TreeNode groupByParentNode = node.Parent;
                                            if (groupByParentNode.Parent == null || groupByParentNode.Parent.NodeType == Tokens.Root)
                                            {
                                                TreeNode orderByNode = new TreeNode(Tokens.Keyword, "ORDER BY");
                                                foreach (TreeNode child in node.Children)
                                                {
                                                    orderByNode.AddChild(new TreeNode(child));
                                                }
                                                root.AddChild(orderByNode);
                                            }
                                        }

                                        TreeNode selectNode = new TreeNode(targetTree.FindNode(root, "SELECT"), "");
                                        int[] ExpressionIndex = new int[selectNode.Children.Count];
                                        int order = 0;

                                        for (int i = 0; i < selectNode.Children.Count; i++)
                                        {
                                            if (selectNode.Children[i].NodeType == Tokens.Expression)
                                            {
                                                ExpressionIndex[order++] = i;
                                            }
                                        }
                                        for (int i = 0; i < node.Children.Count; i++)
                                        {
                                            if (node.Children[i].NodeType == Tokens.NumericConst)
                                            {
                                                TreeNode currentNode = null;
                                                if (selectNode.Children[ExpressionIndex[Convert.ToInt32(node.Children[i].NodeValue) - 1]].NodeInfo != "NOT USED")
                                                {
                                                    currentNode = new TreeNode(selectNode.Children[ExpressionIndex[Convert.ToInt32(node.Children[i].NodeValue) - 1]], "");
                                                }
                                                else
                                                {
                                                    currentNode = new TreeNode(selectNode.Children[ExpressionIndex[Convert.ToInt32(node.Children[i].NodeValue) - 1]].Children[0], "");
                                                }

                                                //The GROUP BY expression has to contain a column
                                                if (targetTree.FindNode(currentNode, Tokens.Column) != null)
                                                {

                                                    for (int k = 0; k < currentNode.Children.Count; k++)
                                                    {
                                                        if (currentNode.Children[k].NodeValue == "AS")
                                                        {
                                                            currentNode.RemoveChild(k);
                                                            currentNode.RemoveChild(k);
                                                        }
                                                    }
                                                    node.Children[i] = currentNode;
                                                }
                                                //else
                                                //{
                                                //    //Remove the number from the GROUP BY list
                                                //    node.children[i].NodeInfo = "NOT USED";

                                                //    if (i == node.children.Count - 1 && i > 0)
                                                //    {
                                                //        if (node.children[i - 1].NodeInfo != "NOT USED")
                                                //        {
                                                //            node.children[i - 1].NodeInfo = "NOT USED";
                                                //        }
                                                //        else
                                                //        {
                                                //            for (int j = node.children.Count - 1; j >= 0; j--)
                                                //            {
                                                //                if (node.children[j].NodeType == Tokens.Comma &&
                                                //                    node.children[j].NodeInfo != "NOT USED")
                                                //                {
                                                //                    node.children[j].NodeInfo = "NOT USED";
                                                //                    break;
                                                //                }
                                                //            }
                                                //        }
                                                //    }
                                                //    else if(i < node.children.Count - 1)
                                                //    {
                                                //        node.children[i + 1].NodeInfo = "NOT USED";
                                                //    }
                                                //}
                                            }

                                        }
                                        break;
                                    }

                                case "LIKE":
                                    {
                                        //in SqlBase versions older than 9.0 there is a bug in evaluating the NOT LIKE predicates
                                        //column NOT LIKE 'c' returns also null values
                                        //so we have to add a 2nd condition: OR column IS NULL
                                        //also conditions like: column LIKE '' return null values too
                                        //but column NOT LIKE '' don't
                                        if (Settings.SqlBaseVersion == DatabaseBrand.SqlBaseOld)
                                        {
                                            TreeNode predicateNode = targetTree.FindParent(root, node);
                                            if (predicateNode != null && predicateNode.NodeType == Tokens.Predicate)
                                            {
                                                int index = predicateNode.Children.IndexOf(node);
                                                if ((index > 1 && predicateNode.Children[index - 1].NodeValue == "NOT" && predicateNode.Children[index + 1].NodeValue != "''") ||  //NOT LIKE 'c'
                                                    (index == 1 && predicateNode.Children[index + 1].NodeValue == "''"))    //LIKE ''
                                                {
                                                    TreeNode exprNode = targetTree.FindNode(predicateNode, Tokens.Expression); //column node
                                                    if (exprNode != null)
                                                    {
                                                        TreeNode newNode = new TreeNode(Tokens.Expression, "", "SKIP");
                                                        newNode.AddChild(new TreeNode(Tokens.Keyword, "OR"));
                                                        newNode.AddChild(exprNode);
                                                        newNode.AddChild(new TreeNode(Tokens.Keyword, "IS NULL"));

                                                        predicateNode.AddChild(newNode);
                                                    }

                                                    //surround the condition with paranthesis
                                                    predicateNode.InsertChild(new TreeNode(Tokens.LeftParant, "("), 0);
                                                    predicateNode.AddChild(new TreeNode(Tokens.RightParant, ")"));
                                                    node.NodeInfo = "SKIP";
                                                }
                                            }
                                        }
                                        break;
                                    }

                                case "STATISTICS":
                                    {
                                        if (targetTree.FindNode(root, "DATABASE") != null ||
                                            targetTree.FindNode(root, "SET") != null)
                                        {
                                            throw new NotSupportedException("UPDATE STATISTICS is only supported with the TABLE clause");
                                        }

                                        TreeNode tempNode = targetTree.FindNode(root, "ON");
                                        if (tempNode != null)
                                        {
                                            tempNode.NodeInfo = "NOT USED";
                                        }

                                        //@MU
                                        tempNode = targetTree.FindNode(root, "TABLE");
                                        if (tempNode != null)
                                        {
                                            tempNode.NodeInfo = "NOT USED";
                                        }
                                        //@MU END
                                        break;
                                    }

                                case "CURRENT DATETIME":
                                case "CURRENT TIMESTAMP":
                                    {
                                        node.NodeValue = "CURRENT_TIMESTAMP";
                                        break;
                                    }

                                case "CURRENT DATE":
                                    {
                                        node.NodeValue = "dbo.CURRENTDATE()";
                                        break;
                                    }

                                case "CURRENT TIME":
                                    {
                                        node.NodeValue = "dbo.CURRENTTIME()";
                                        break;
                                    }

                                case "CREATE TABLE":
                                    {
                                        bool addComma = false;
                                        if (node.Children[node.Children.Count - 1].NodeType != Tokens.Comma)
                                        {
                                            node.AddChild(new TreeNode(Tokens.Comma, ","));
                                        }
                                        else
                                        {
                                            addComma = true;
                                        }
                                        node.AddChild(new TreeNode(Tokens.Column, "ROWVERSION"));
                                        node.AddChild(new TreeNode(Tokens.DataType, "VARCHAR2(20 BYTE)"));

                                        if (addComma)
                                        {
                                            node.AddChild(new TreeNode(Tokens.Comma, ","));
                                        }
                                        break;
                                    }

                                case "SELECT":
                                    {
                                        foreach (TreeNode childNode in node.Children)
                                        {
                                            //Surround the aliases with dot in their names with ""
                                            for (int i = 0; i < childNode.Children.Count; i++)
                                            {
                                                if (childNode.Children[i].NodeValue == "AS")
                                                {
                                                    i++;
                                                    if (childNode.Children[i].NodeValue.Contains("."))
                                                    {
                                                        childNode.Children[i].NodeValue = "\"" + childNode.Children[i].NodeValue + "\"";
                                                    }
                                                    else if (LexicalAnalyzerSqlBase.IsKeyword(childNode.Children[i].NodeValue))
                                                    {
                                                        childNode.Children[i].NodeValue = "\"" + childNode.Children[i].NodeValue + "\"";
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    break;
                            }
                            break;
                        }

                    case Tokens.Column:
                        {
                            TranslateColumn(node, root, targetTree);
                            break;
                        }

                    case Tokens.DatetimeConst:
                    case Tokens.StringConst:
                        {
                            if (node.NodeType == Tokens.DatetimeConst && !node.NodeValue.StartsWith("TO_"))
                            {
                                //surround the constant with '' 
                                node.NodeValue = "'" + node.NodeValue + "'";
                            }

                            CustomDateTime datetime;

                            if (node.NodeValue.Length > 2 && IsValidDateTime(node.NodeValue.Substring(1, node.NodeValue.Length - 2), out datetime))
                            {
                                //find the corresponding column
                                TreeNode columnNode = null, tableNode, parentNode = null;
                                int index = 0;
                                if (root.Children[0].NodeType == Tokens.Insert)
                                {
                                    if (Settings != null && Settings.DbStructure != null)
                                    {
                                        index = Convert.ToInt32(node.NodeInfo);
                                        TreeNodeCollection columnNodes = new TreeNodeCollection();
                                        targetTree.FindAll(root, Tokens.Column, ref columnNodes);
                                        tableNode = targetTree.FindNode(root, Tokens.Table);
                                        string tempTable = tableNode.NodeValue.RemoveBrackets();
                                        if (columnNodes.Count > 0)
                                        {
                                            columnNode = columnNodes[index];
                                            //Settings.DbStructure[columnNode.NodeValue.RemoveBrackets(), Settings.Schemas.FindTable(tableNode.GetSchema(Settings), tempTable).Name]
                                            if (Settings.DbStructure[columnNode.NodeValue.RemoveBrackets(), Settings.Schemas.FindTable(tableNode.GetSchema(Settings), tempTable)?.Name].In("datetime", "date", "time"))
                                            {
                                                node.NodeValue = datetime.ToOracleString();
                                            }
                                        }
                                        else
                                        {
                                            if (Settings.DbStructure[index, Settings.Schemas.FindTable(tableNode.GetSchema(Settings), tempTable)?.Name].In("datetime", "date", "time"))
                                            {
                                                node.NodeValue = datetime.ToOracleString();
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (node.NodeType == Tokens.DatetimeConst)
                                    {
                                        node.NodeValue = datetime.ToOracleString();
                                        bool toString = false;
                                        TreeNode decodeNode = targetTree.FindParent(node, "DECODE");
                                        if (decodeNode == null)
                                        {
                                            decodeNode = targetTree.FindNode(root, "@DECODE");
                                        }
                                        bool nodeIsDecodeNode = false;
                                        if (decodeNode != null)
                                        {
                                            foreach (TreeNode childNode in decodeNode.Children)
                                            {
                                                if (childNode == node || (childNode.Children.Count > 0 && childNode.Children.Contains(node)))
                                                {
                                                    nodeIsDecodeNode = true;
                                                    break;
                                                }
                                            }
                                            if (nodeIsDecodeNode)
                                            {
                                                foreach (TreeNode childNode in decodeNode.Children)
                                                {
                                                    if (childNode.ToString().StartsWith("TO_CHAR") || childNode.ToString().StartsWith("@DATETOCHAR"))
                                                    {
                                                        toString = true;
                                                    }
                                                }
                                                parentNode = node.Parent;
                                                int nodeIndex = parentNode.Children.IndexOf(node);
                                                TreeNode mathOperator = targetTree.PreviousNode(node, Tokens.MathOperator);
                                                if (mathOperator != null)
                                                {
                                                    if ((nodeIndex > 0 && nodeIndex - 1 == parentNode.Children.IndexOf(mathOperator))
                                                        || (nodeIndex == 0 && nodeIndex + 1 == parentNode.Children.IndexOf(mathOperator)))
                                                    {
                                                        toString = false;
                                                    }
                                                }

                                                if (toString)
                                                {
                                                    node.NodeValue = "TO_CHAR(" + node.NodeValue + ")";
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        parentNode = targetTree.FindParent(node, Tokens.Predicate);
                                        if (parentNode == null)
                                        {
                                            parentNode = targetTree.FindParent(node, Tokens.Function);
                                        }
                                        if (parentNode != null)
                                        {
                                            columnNode = targetTree.FindNode(parentNode, Tokens.Column);
                                        }
                                        else
                                        {
                                            parentNode = targetTree.FindParent(node, "SET");
                                            if (parentNode != null) //UPDATE
                                            {
                                                for (int i = 0; i < parentNode.Children.Count; i++)
                                                {
                                                    if (targetTree.FindNode(parentNode.Children[i], node))
                                                    {
                                                        index = i;
                                                        break;
                                                    }
                                                }
                                                if (index > 0)
                                                {
                                                    columnNode = parentNode.Children[index - 2];
                                                }
                                            }
                                        }
                                        if (columnNode != null && parentNode != null && !parentNode.ToString().Contains("DATETOCHAR"))
                                        {
                                            //only translate to datetime when the column is a date type and it's not converted with DATETOCHAR
                                            if (Settings != null && Settings.DbStructure != null)
                                            {
                                                TreeNodeCollection tables = new TreeNodeCollection();
                                                targetTree.FindAll(root, Tokens.Table, ref tables);
                                                if (tables != null)
                                                {
                                                    //Use the NodeInfo to get the column
                                                    //string originalNodeValue = columnNode.NodeValue;
                                                    //string column = columnNode.NodeValue;
                                                    //if (columnNode.NodeValue.Contains("("))
                                                    //{
                                                    //    column = columnNode.NodeValue.Split('(')[1].Replace(")", "");
                                                    //    columnNode.NodeValue = column;
                                                    //}
                                                    //if (column.Contains("."))
                                                    //{
                                                    //    column = column.Substring(column.LastIndexOf(".") + 1);
                                                    //}
                                                    string column = columnNode.NodeInfo;

                                                    tableNode = GetTable(columnNode, tables);

                                                    //columnNode.NodeValue = originalNodeValue;

                                                    bool isDate = false, isTime = false, isTimestamp = false;
                                                    int milliseconds = 0;
                                                    GetColumnDateTimeProperties(column.ToUpper(), out isDate, out isTime, out isTimestamp, out milliseconds);

                                                    //if ((tableNode != null && Settings.DbStructure[column, GetTableName(tableNode.NodeValue.RemoveBrackets())].In("datetime", "date", "time")
                                                    //    || targetTree.FindNode(parentNode, Tokens.Function, "TODATE") != null) && targetTree.FindNode(parentNode, Tokens.Function, "EXACT") == null)
                                                    if ((tableNode != null && (isDate || isTime || isTimestamp)
                                                        || targetTree.FindNode(parentNode, Tokens.Function, "TODATE") != null) && targetTree.FindNode(parentNode, Tokens.Function, "EXACT") == null
                                                        && node.NodeValue.Replace("'", "").Length >= 4 && milliseconds == 0)
                                                    {
                                                        node.NodeValue = datetime.ToOracleString();
                                                    }
                                                    else
                                                    {
                                                        TreeNode toCharNode = targetTree.FindNode(parentNode, "TO_CHAR");
                                                        if (targetTree.FindNode(parentNode, toCharNode))
                                                        {
                                                            if (targetTree.FindNode(toCharNode, node))
                                                            {
                                                                node.NodeValue = datetime.ToOracleString();
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if ((tableNode != null && (isDate || isTime || isTimestamp)
                                                                || targetTree.FindNode(parentNode, Tokens.Function, "TODATE") != null) && targetTree.FindNode(parentNode, Tokens.Function, "EXACT") == null
                                                                && node.NodeValue.Replace("'", "").Length >= 4 && (parentNode.NodeValue == "NVL" || parentNode.NodeValue == "@NULLVALUE"))
                                                            {
                                                                foreach (TreeNode n in parentNode.Children)
                                                                {
                                                                    if (n.ToString().StartsWith("TO_CHAR"))
                                                                    {
                                                                        toCharNode = n;
                                                                        break;
                                                                    }
                                                                }
                                                                if (toCharNode != null)
                                                                {
                                                                    node.NodeValue = datetime.ToOracleString();
                                                                }
                                                            }
                                                        }
                                                    }

                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (node.Parent.Parent.ToString().StartsWith("TO_DATE"))
                                            {
                                                TreeNode dateNode = new TreeNode(Tokens.DatetimeConst);
                                                dateNode.NodeValue = datetime.ToOracleString();
                                                dateNode.NodeValue = dateNode.NodeValue.Replace("TO_DATE", "");
                                                dateNode.NodeValue = dateNode.NodeValue.Replace("(", "");
                                                dateNode.NodeValue = dateNode.NodeValue.Replace(")", "");
                                                string[] dateNodeChildren = dateNode.NodeValue.Split(',');
                                                if (dateNodeChildren.Length == 2)
                                                {
                                                    node.NodeValue = dateNodeChildren[0];
                                                    node.Parent.Parent.Children[node.Parent.Parent.Children.Count - 2].NodeValue = dateNodeChildren[1];
                                                }
                                                else
                                                {
                                                    node.NodeValue = datetime.ToOracleString();
                                                }
                                            }
                                            else
                                            {
                                                node.NodeValue = datetime.ToOracleString();
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                bool date = false;
                                bool toString = false;
                                bool isDate = false;
                                bool isTime = false;
                                bool isTimestamp = false;
                                bool thenDate = false;
                                bool elseDate = false;
                                int milliseconds = 0;
                                TreeNode caseNode = targetTree.FindParent(node, "CASE");
                                TreeNode thenNode = targetTree.FindNode(caseNode, "THEN");
                                TreeNode elseNode = targetTree.FindNode(caseNode, "ELSE");

                                TreeNodeCollection thenCols = new TreeNodeCollection();
                                TreeNodeCollection elseCols = new TreeNodeCollection();

                                targetTree.FindAll(thenNode, Tokens.Column, ref thenCols);
                                targetTree.FindAll(elseNode, Tokens.Column, ref elseCols);

                                if (thenCols.Count == 0)
                                {
                                    thenDate = false;
                                }
                                else
                                {
                                    foreach (TreeNode col in thenCols)
                                    {
                                        this.GetColumnDateTimeProperties(GetColumnName(col.NodeValue), out isDate, out isTime, out isTimestamp, out milliseconds);
                                        if (!isDate)
                                        {
                                            thenDate = false;
                                            break;
                                        }
                                        else
                                        {
                                            if (col.Parent.NodeType != Tokens.Function && col.Parent.Parent.NodeType != Tokens.Function)
                                            {
                                                thenDate = true;
                                            }
                                            else
                                            {
                                                thenDate = false;
                                                break;
                                            }
                                        }
                                    }
                                }


                                if (elseCols.Count == 0)
                                {
                                    elseDate = false;
                                }
                                else
                                {
                                    foreach (TreeNode col in elseCols)
                                    {
                                        this.GetColumnDateTimeProperties(GetColumnName(col.NodeValue), out isDate, out isTime, out isTimestamp, out milliseconds);
                                        if (!isDate)
                                        {
                                            elseDate = false;
                                            break;
                                        }
                                        else
                                        {
                                            if (col.Parent.NodeType != Tokens.Function && (col.Parent.Parent.NodeType != Tokens.Function || caseNode.Parent.Parent.ToString().StartsWith("TO_DATE") || caseNode.Parent.Parent.ToString().StartsWith("TO_CHAR")))
                                            {
                                                elseDate = true;
                                            }
                                            else
                                            {
                                                elseDate = false;
                                                break;
                                            }
                                        }
                                    }
                                }

                                if ((thenDate || elseDate) && caseNode != null && targetTree.FindNode(caseNode, node) && node.NodeValue.Replace("'", "").Trim() == "")
                                {
                                    TreeNodeCollection caseCols = new TreeNodeCollection();
                                    targetTree.FindAll(caseNode, Tokens.Column, ref caseCols);
                                    foreach (TreeNode col in caseCols)
                                    {
                                        this.GetColumnDateTimeProperties(GetColumnName(col.NodeValue), out isDate, out isTime, out isTimestamp, out milliseconds);
                                        if (!isDate)
                                        {
                                            date = false;
                                            break;
                                        }
                                        else
                                        {
                                            if (col.Parent.NodeType != Tokens.Function && (col.Parent.Parent.NodeType != Tokens.Function || caseNode.Parent.Parent.ToString().StartsWith("TO_DATE") || caseNode.Parent.Parent.ToString().StartsWith("TO_CHAR")))
                                            {
                                                date = true;
                                            }
                                            else
                                            {
                                                date = false;
                                                break;
                                            }
                                        }
                                    }
                                    if (date)
                                    {
                                        node.NodeValue = "TO_DATE(" + node.NodeValue + ")";
                                    }
                                }
                                else if (!thenDate && !elseDate)
                                {
                                    if (caseNode == null && thenNode == null && elseNode == null && node.NodeValue.Replace("'", "").Trim() != "")
                                    {
                                        TreeNode decodeNode = targetTree.FindParent(node, "DECODE");
                                        if (decodeNode == null)
                                        {
                                            decodeNode = targetTree.FindNode(root, "@DECODE");
                                        }
                                        if (decodeNode != null)
                                        {
                                            if (targetTree.FindNode(decodeNode, node))
                                            {
                                                TreeNodeCollection decodeCols = new TreeNodeCollection();
                                                targetTree.FindAll(decodeNode, Tokens.Column, ref decodeCols);
                                                foreach (TreeNode col in decodeCols)
                                                {
                                                    this.GetColumnDateTimeProperties(GetColumnName(col.NodeInfo), out isDate, out isTime, out isTimestamp, out milliseconds);
                                                    TreeNode strNode = targetTree.FindNode(decodeNode, "CAST");
                                                    if (strNode != null)
                                                    {
                                                        if (strNode.Children[strNode.Children.Count - 3].NodeValue == "AS" && strNode.Children[strNode.Children.Count - 2].NodeValue == "VARCHAR2(254)")
                                                        {
                                                            if (targetTree.FindNode(strNode, col))
                                                            {
                                                                if (isDate)
                                                                {
                                                                    isDate = false;
                                                                }
                                                            }
                                                        }
                                                    }

                                                    if (isDate)
                                                    {
                                                        strNode = targetTree.FindNode(decodeNode, "STRLEFT");
                                                        if (targetTree.FindNode(strNode, col))
                                                        {
                                                            isDate = false;
                                                        }
                                                    }

                                                    if (isDate)
                                                    {
                                                        strNode = targetTree.FindNode(decodeNode, "STRRIGHT");
                                                        if (targetTree.FindNode(strNode, col))
                                                        {
                                                            isDate = false;
                                                        }
                                                    }

                                                    if (!isDate)
                                                    {
                                                        date = false;
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        date = true;
                                                    }
                                                }
                                                foreach (TreeNode childNode in decodeNode.Children)
                                                {
                                                    if (childNode.ToString().StartsWith("TO_CHAR") || childNode.ToString().StartsWith("@DATETOCHAR"))
                                                    {
                                                        toString = true;
                                                    }
                                                }
                                                if (date)
                                                {
                                                    node.NodeValue = "TO_DATE(" + node.NodeValue + ")";
                                                }
                                                if (toString)
                                                {
                                                    node.NodeValue = "TO_CHAR(" + node.NodeValue + ")";
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            ConvertWithFormat(targetTree, root, node);
                            break;
                        }

                    case Tokens.CheckDatabase:
                        {
                            //There is no equivalent in Oracle? Can use DBVERIFY instead (external command-line utility)
                            throw new NotSupportedException("CHECK DATABASE is not supported by the SqlTranslator");
                            break;
                        }

                    //Oracle doesn't support name = expression in the SELECT clause
                    case Tokens.Name:
                        {
                            if (node.NodeInfo == "NAME=")
                            {
                                TreeNode parent = targetTree.FindParent(root, node);
                                node.NodeType = Tokens.Identifier;
                                parent.AddChild(new TreeNode(node));
                                node.NodeInfo = "NOT USED";
                                TreeNode equal = targetTree.FindNode(parent, "=");
                                equal.NodeInfo = "NOT USED";
                            }
                            break;
                        }

                    case Tokens.DataTypeKeyword:
                        {
                            switch (node.NodeValue)
                            {
                                case "CHAR":
                                    {
                                        node.NodeValue = Settings.Unicode ? "NCHAR" : "CHAR";
                                        break;
                                    }

                                case "VARCHAR":
                                    {
                                        node.NodeValue = Settings.Unicode ? "NVARCHAR2" : "VARCHAR2";
                                        break;
                                    }

                                case "LONG VARCHAR":
                                    {
                                        node.NodeValue = "CLOB";
                                        break;
                                    }

                                case "TIME":
                                case "DATETIME":
                                    {
                                        node.NodeValue = "TIMESTAMP";
                                        break;
                                    }

                                case "DEC":
                                case "DECIMAL":
                                    {
                                        node.NodeValue = "NUMBER";
                                        break;
                                    }

                                case "DOUBLE PRECISION":
                                    {
                                        node.NodeValue = "BINARY_DOUBLE";
                                        break;
                                    }

                                case "REAL":
                                    {
                                        node.NodeValue = "BINARY_FLOAT";
                                        break;
                                    }
                            }
                            break;
                        }

                    //Only needed for SP translation
                    //case Tokens.BindVariable:
                    //    node.NodeValue = node.NodeValue.Substring(1);
                    //    break;

                    case Tokens.Expression:
                        //When 2 ore more strings are concatenated and the resulting max length is > 254, the result can't be fetched in Gupta into a string variable
                        //E.g col1 || col2 -> if col1 is varchar2(100) and col2 is varchar2(200) the max length is 300 even if the actual content is smaller
                        if (node.Parent != null && node.Parent.NodeValue == "SELECT" && node.ToString().Contains("||")
                            && node.NodeValue != "CAST(" && node.NodeValue != "CAST")
                        {
                            node.NodeValue = "CAST(";
                            TreeNode aliasNode = targetTree.FindNode(node, "AS");
                            TreeNode castNode = new TreeNode(Tokens.Expression, " as varchar2(254))");
                            if (aliasNode != null)
                            {
                                node.InsertChild(castNode, node.Children.IndexOf(aliasNode));
                            }
                            else
                            {
                                node.AddChild(castNode);
                            }
                        }
                        break;

                    case Tokens.Predicate:
                        if (Globals.TableInformation.IsInitialized)
                        {
                            if (node.Children.Count == 3)
                            {
                                TreeNode leftExpr = node.Children[0];
                                TreeNode rightExpr = node.Children[2];

                                TreeNode columnNode = targetTree.FindNode(leftExpr, Tokens.Column);
                                if (columnNode != null)
                                {
                                    bool isDate = false;
                                    string colName = columnNode.NodeValue;
                                    if (colName.Contains("."))
                                    {
                                        string tableName = colName.Substring(0, colName.LastIndexOf("."));
                                        colName = colName.Substring(colName.LastIndexOf(".") + 1);
                                        isDate = this.IsDateColumn(colName);
                                    }
                                    else
                                    {
                                        isDate = this.IsDateColumn(colName);
                                    }
                                    if (isDate)
                                    {
                                        if (rightExpr.NodeValue != "NULL" && rightExpr.ToString().Trim() != "''")
                                        {
                                            bool addTrunc = true;
                                            TreeNode whereNode = targetTree.FindNode(root, "WHERE");
                                            if (whereNode != null)
                                            {
                                                if ((rightExpr.Children[0].NodeValue == "@NOW" && targetTree.FindNode(whereNode, rightExpr)) || (targetTree.FindNode(rightExpr, "YEAR") != null || targetTree.FindNode(rightExpr, "MONTH") != null)
                                                    || (rightExpr.Children[0].NodeValue == "@DATETOCHAR" && targetTree.FindNode(rightExpr, "@NOW") != null) || (rightExpr.Children[0].NodeValue == "@DECODE"))
                                                {
                                                    addTrunc = false;
                                                }
                                            }
                                            if (addTrunc)
                                            {
                                                bool toDateInserted = false;
                                                rightExpr.Children.Insert(new TreeNode(Tokens.Function, "TRUNC"), 0);
                                                rightExpr.Children.Insert(new TreeNode(Tokens.LeftParant, "("), 1);
                                                if (rightExpr.Children[2].NodeType == Tokens.BindVariable || rightExpr.Children[2].NodeType == Tokens.StringConst)
                                                {
                                                    rightExpr.Children.Insert(new TreeNode(Tokens.Function, "TO_DATE"), 0);
                                                    rightExpr.Children.Insert(new TreeNode(Tokens.LeftParant, "("), 1);

                                                    rightExpr.Children.Insert(new TreeNode(Tokens.Function, "TO_TIMESTAMP"), 4);
                                                    rightExpr.Children.Insert(new TreeNode(Tokens.LeftParant, "("), 5);
                                                    rightExpr.Children.Add(new TreeNode(Tokens.RightParant, ", 'YYYY-MM-DD HH24:MI:SS.FF6')"));

                                                    toDateInserted = true;
                                                }
                                                rightExpr.Children.Add(new TreeNode(Tokens.RightParant, ")"));
                                                if (toDateInserted)
                                                {
                                                    rightExpr.Children.Add(new TreeNode(Tokens.RightParant, ", 'YYYY-MM-DD HH24:MI:SS')"));
                                                }

                                                TreeNode mathOperator = targetTree.FindNode(rightExpr, Tokens.MathOperator);
                                                if (mathOperator != null)
                                                {
                                                    TreeNode numericConstant = targetTree.FindNode(rightExpr, Tokens.NumericConst);
                                                    if (numericConstant != null)
                                                    {
                                                        int count = rightExpr.Children.IndexOf(mathOperator);
                                                        TreeNode lastRightParant = rightExpr.Children[rightExpr.Children.Count - 1];
                                                        if (lastRightParant.NodeType == Tokens.RightParant
                                                            && count > 0 && count < rightExpr.Children.Count)
                                                        {
                                                            TreeNode temp = rightExpr.Children[count];
                                                            while (temp != lastRightParant)
                                                            {
                                                                rightExpr.Children.Remove(temp);
                                                                rightExpr.Children.Add(temp);
                                                                temp = rightExpr.Children[count];
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        break;

                    //There can be only one LONG column in an Oracle table so we have to keep the LONG values in separate tables
                    //and modify the INSERT and UPDATE statements accordingly
                    case Tokens.Insert:
                        if (Globals.TableInformation.IsInitialized)
                        {
                            TreeNodeCollection rawBindsNodes = new TreeNodeCollection();
                            int adjustment = 2;

                            TreeNode valuesNode = targetTree.FindNode(root, "SELECT");

                            string table = GetTableName(targetTree.FindNode(node, Tokens.Table).NodeValue);
                            if (this.HasExternalLongOrBinaryColumns(table))
                            {
                                int columnCount = targetTree.FindAll(node.Children[0], Tokens.Column);
                                int index = 0;
                                int childrenCount = 0;
                                if (valuesNode != null)
                                {
                                    childrenCount = valuesNode.Children.Count;
                                }
                                else
                                {
                                    childrenCount = node.Children[0].Children.Count;
                                }
                                for (int i = 0; i < childrenCount; i++)
                                {
                                    TreeNode childNode = node.Children[0].Children[i];
                                    if (childNode.NodeType == Tokens.Column)
                                    {
                                        int insertColIndex = root.Children[0].Children[0].Children.IndexOf(childNode) - adjustment;

                                        index++;
                                        bool isBinary, isLong, isExternal;
                                        string columnName = GetColumnName(childNode.NodeValue);
                                        if (this.GetColumnProperties(columnName, out isBinary, out isLong, out isExternal))
                                        {
                                            if (isExternal)
                                            {
                                                ExternalColumn extColumn = new ExternalColumn();
                                                extColumn.Column = columnName;
                                                extColumn.Index = index;
                                                extColumn.GUID = "'" + Guid.NewGuid().ToString() + "'";
                                                extColumn.IsBinary = isBinary;
                                                extColumn.Value = node.Children[1].Children[i - 1].NodeValue;
                                                node.Children[1].Children[i - 1].NodeValue = extColumn.GUID;
                                                node.Children[1].Children[i - 1].NodeType = Tokens.StringConst;
                                                externalCols.Add(extColumn);
                                            }
                                            else
                                            {
                                                //We have to return the position of the raw bind variables so that the calling application
                                                //can set the binds using SqlSetLongBindDatatype
                                                if (isBinary)
                                                {
                                                    if (valuesNode != null)
                                                    {
                                                        if (!rawBindsNodes.Contains(valuesNode.Children[i - 1]))
                                                        {
                                                            if (valuesNode.Children[i - 1].NodeType == Tokens.BindVariable)
                                                            {
                                                                rawBindsNodes.Add(valuesNode.Children[i - 1]);
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (!rawBindsNodes.Contains(node.Children[1].Children[i - 1]))
                                                        {
                                                            rawBindsNodes.Add(node.Children[1].Children[i - 1]);
                                                        }
                                                    }
                                                }

                                                //The LONG/LONG RAW columns have to be the last in the statement when the value is larger than 4000 bytes
                                                if (isLong && index < columnCount)
                                                {
                                                    node.Children[0].RemoveChild(childNode);
                                                    node.Children[0].RemoveChild(i); //remove the comma
                                                    int childCount = node.Children[0].Children.Count;
                                                    node.Children[0].InsertChild(new TreeNode(Tokens.Comma, ","), childCount - 1); //add the comma before the closing paranthesis
                                                    node.Children[0].InsertChild(childNode, childCount);
                                                    //do the same for the corresponding bind variable
                                                    TreeNode bindNode = null;
                                                    if (valuesNode != null)
                                                    {
                                                        bindNode = valuesNode.Children[i - adjustment];
                                                        valuesNode.RemoveChild(bindNode);
                                                    }
                                                    else
                                                    {
                                                        bindNode = node.Children[1].Children[i - 1];
                                                        node.Children[1].RemoveChild(bindNode);
                                                    }

                                                    if (i > 0)
                                                    {
                                                        if (valuesNode != null)
                                                        {
                                                            valuesNode.RemoveChild(i - adjustment);
                                                        }
                                                        else
                                                        {
                                                            node.Children[1].RemoveChild(i - 1);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (valuesNode != null)
                                                        {
                                                            valuesNode.RemoveChild(0);
                                                        }
                                                        else
                                                        {
                                                            node.RemoveChild(0);
                                                        }
                                                    }
                                                    if (valuesNode != null)
                                                    {
                                                        valuesNode.InsertChild(new TreeNode(Tokens.Comma, ","), valuesNode.Children.Count);
                                                        valuesNode.InsertChild(bindNode, valuesNode.Children.Count);
                                                    }
                                                    else
                                                    {
                                                        childCount = node.Children[1].Children.Count;
                                                        node.Children[1].InsertChild(new TreeNode(Tokens.Comma, ","), childCount - 1);
                                                        node.Children[1].InsertChild(bindNode, childCount);
                                                    }

                                                    i--;
                                                }
                                            }
                                        }
                                    }
                                }
                                if (rawPositions != null)
                                {
                                    TreeNodeCollection cols = new TreeNodeCollection();
                                    targetTree.FindAll(node.Children[1], Tokens.BindVariable, ref cols);
                                    foreach (TreeNode rawBindNode in rawBindsNodes)
                                    {
                                        int colIndex = cols.IndexOf(rawBindNode) + 1;
                                        if (rawPositions.Length > 0)
                                        {
                                            rawPositions.Append(",");
                                        }
                                        rawPositions.Append(colIndex.ToString());
                                    }
                                    if (rawPositions.Length == 0)
                                    {
                                        rawPositions.Append(" ");
                                    }
                                }
                                //Generate the additional INSERTs
                                if (externalCols != null)
                                {
                                    foreach (ExternalColumn extCol in externalCols)
                                    {
                                        string insert = String.Format("{0} INSERT INTO {1} (LongGUID, Tablename, Columnname, LongValue) VALUES ({2}, '{4}', '{5}', {3})",
                                            DatabaseSettings.Default.Separator, extCol.IsBinary ? "CPLONGBIN" : "CPLONGTEXT", extCol.GUID, extCol.Value, table, extCol.Column.ToUpper());
                                        root.AddChild(new TreeNode(Tokens.Statement, insert, "SKIP"));

                                        bool isBinary = false;
                                        bool isLong = false;
                                        bool isExternal = false;
                                        int countLongTmp = 0;
                                        int countLongRawTmp = 0;
                                        int countVarchar2Tmp = 0;
                                        int countRawTmp = 0;
                                        GetColumnProperties(extCol.Column.ToUpper(), out isBinary, out isLong, out isExternal);

                                        if (isLong && !isBinary)
                                        {
                                            this.countLong--;
                                            countLongTmp++;
                                        }
                                        if (isLong && isBinary)
                                        {
                                            this.countLongRaw--;
                                            countLongRawTmp++;
                                        }
                                        if (!isLong && !isBinary)
                                        {
                                            this.countVarchar2--;
                                            countVarchar2Tmp++;
                                        }
                                        if (!isLong && isBinary)
                                        {
                                            this.countRaw--;
                                            countRawTmp++;
                                        }
                                        if (longCount.Length > 0)
                                        {
                                            longCount.Append(DatabaseSettings.Default.Separator);
                                        }

                                        longCount.Append(countLongTmp + "," + countLongRawTmp + "," + countVarchar2Tmp + "," + countRawTmp);

                                        rawPositions.Append(DatabaseSettings.Default.Separator);

                                        if (extCol.IsBinary)
                                        {
                                            rawPositions.Append("1");
                                        }
                                        else
                                        {
                                            rawPositions.Append(" ");
                                        }
                                    }
                                }
                            }
                        }
                        break;

                    case Tokens.Update:
                        if (Globals.TableInformation.IsInitialized)
                        {
                            if (!node.ToString().StartsWith("UPDATE STATISTICS"))
                            {
                                TreeNodeCollection rawBindsNodes = new TreeNodeCollection();
                                int notUsedNodes = 0;
                                string table = GetTableName(targetTree.FindNode(node, Tokens.Table).NodeValue);
                                string tableNodeWithOwner = targetTree.FindNode(node, Tokens.Table).NodeValue;
                                if (this.HasExternalLongOrBinaryColumns(table))
                                {
                                    //Have to translate first the statement
                                    TransformTreeSqlBase_Oracle(ref targetTree, root, node.Children[2]);

                                    int columnCount = targetTree.FindAll(node.Children[1], Tokens.Column);
                                    int index = 0;
                                    for (int i = 0; i < node.Children[1].Children.Count; i++)
                                    {
                                        TreeNode childNodeTemp = node.Children[1].Children[i];
                                        TreeNodeCollection columnCollection = new TreeNodeCollection();
                                        if (childNodeTemp.NodeType != Tokens.Column)
                                        {
                                            targetTree.FindAll(childNodeTemp, Tokens.Column, ref columnCollection);
                                        }
                                        else
                                        {
                                            columnCollection.Add(childNodeTemp);
                                        }
                                        foreach (TreeNode childNode in columnCollection)
                                        {
                                            if (childNode.NodeType == Tokens.Column)
                                            {
                                                index++;
                                                bool isBinary, isLong, isExternal;
                                                string columnName = GetColumnName(childNode.NodeValue);
                                                if (this.GetColumnProperties(columnName, out isBinary, out isLong, out isExternal))
                                                {
                                                    if (isExternal && externalCols != null)
                                                    {
                                                        ExternalColumn extColumn = new ExternalColumn();
                                                        extColumn.Column = columnName;
                                                        extColumn.Index = index;
                                                        extColumn.IsBinary = isBinary;
                                                        extColumn.Value = childNode.Parent.Children[2].ToString();
                                                        externalCols.Add(extColumn);

                                                        //Remove the setting of the column from the main update
                                                        childNode.NodeInfo = "NOT USED";
                                                        foreach (TreeNode notUsedNode in childNode.Parent.Children)
                                                        {
                                                            notUsedNode.NodeInfo = "NOT USED";
                                                            notUsedNodes++;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (isBinary)
                                                        {
                                                            if (childNode.Parent.Children.Count == 4 && !rawBindsNodes.Contains(childNode.Parent.Children[2]))
                                                            {
                                                                if (childNode.Parent.Children[0].NodeType == Tokens.Comma)
                                                                {
                                                                    rawBindsNodes.Add(childNode.Parent.Children[3].Children[0]);
                                                                }
                                                                else
                                                                {
                                                                    rawBindsNodes.Add(childNode.Parent.Children[2].Children[0]);
                                                                }
                                                            }
                                                            else if (!rawBindsNodes.Contains(node.Children[1].Children[i + 2].Children[0]))
                                                            {
                                                                rawBindsNodes.Add(node.Children[1].Children[i + 2].Children[0]);
                                                            }
                                                        }

                                                        if (isLong && index < columnCount)
                                                        {

                                                            node.Children[1].RemoveChild(childNode.Parent);
                                                            node.Children[1].AddChild(childNode.Parent);
                                                            TreeNode commaNode = childNode.Parent.Children[childNode.Parent.Children.Count - 1];
                                                            if (commaNode.NodeType == Tokens.Comma)
                                                            {
                                                                childNode.Parent.RemoveChild(commaNode);
                                                                childNode.Parent.InsertChild(commaNode, 0);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    if (externalCols != null)
                                    {
                                        //Generate the additional MERGE statements
                                        foreach (ExternalColumn extCol in externalCols)
                                        {
                                            //                                        string merge = String.Format(@"MERGE INTO {0} EL
                                            //USING (Select {1} FROM {6} {3}) D 
                                            //ON (EL.LongGUID = D.{1})
                                            //WHEN MATCHED 
                                            //    THEN UPDATE SET EL.LongValue = {4} 
                                            //WHEN NOT MATCHED 
                                            //    THEN INSERT (LongGUID, Tablename, Columnname, LongValue)  
                                            //         VALUES (D.{1}, '{2}', '{1}', {4}) 
                                            //{5}",
                                            //                                            extCol.IsBinary ? "SYSADM.CPLONGBIN" : "SYSADM.CPLONGTEXT", extCol.Column.ToUpper(), table.ToUpper(), node.Children[2].ToString(), extCol.Value, DatabaseSettings.Default.Separator, tableNodeWithOwner);

                                            if (node.Children[1].Children.Count == notUsedNodes)
                                            {
                                                root.RemoveChild(node);
                                            }
                                            string merge = String.Format(@"UPDATE {0} SET LongValue = {1} WHERE LONGGUID IN (SELECT {2} FROM {5} {3}) 
{4}",
                                                extCol.IsBinary ? "SYSADM.CPLONGBIN" : "SYSADM.CPLONGTEXT", extCol.Value, extCol.Column.ToUpper(), node.Children[2].ToString(), DatabaseSettings.Default.Separator, tableNodeWithOwner);

                                            root.InsertChild(new TreeNode(Tokens.Statement, merge, "SKIP"), 0);

                                            bool isBinary = false;
                                            bool isLong = false;
                                            bool isExternal = false;
                                            int countLongTmp = 0;
                                            int countLongRawTmp = 0;
                                            int countVarchar2Tmp = 0;
                                            int countRawTmp = 0;
                                            GetColumnProperties(extCol.Column.ToUpper(), out isBinary, out isLong, out isExternal);

                                            if (isLong && !isBinary)
                                            {
                                                this.countLong--;
                                                countLongTmp++;
                                            }
                                            if (isLong && isBinary)
                                            {
                                                this.countLongRaw--;
                                                countLongRawTmp++;
                                            }
                                            if (!isLong && !isBinary)
                                            {
                                                this.countVarchar2--;
                                                countVarchar2Tmp++;
                                            }
                                            if (!isLong && isBinary)
                                            {
                                                this.countRaw--;
                                                countRawTmp++;
                                            }
                                            if (longCount.Length > 0)
                                            {
                                                longCount.Append(DatabaseSettings.Default.Separator);
                                            }

                                            longCount.Append(countLongTmp + "," + countLongRawTmp + "," + countVarchar2Tmp + "," + countRawTmp);


                                            rawPositions.Insert(0, DatabaseSettings.Default.Separator);

                                            if (extCol.IsBinary)
                                            {
                                                rawPositions.Insert(0, "1");
                                            }
                                            else
                                            {
                                                rawPositions.Insert(0, " ");
                                            }
                                        }
                                    }
                                    index = 0;
                                    if (rawPositions != null && node.Children[1].Children.Count != notUsedNodes)
                                    {
                                        TreeNodeCollection cols = new TreeNodeCollection();
                                        targetTree.FindAllUsed(node.Children[1], Tokens.BindVariable, ref cols);
                                        foreach (TreeNode rawBindNode in rawBindsNodes)
                                        {
                                            int colIndex = cols.IndexOf(rawBindNode) + 1;
                                            if (index > 0)
                                            {
                                                rawPositions.Append(",");
                                            }
                                            rawPositions.Append(colIndex.ToString());
                                            index++;
                                        }
                                    }

                                    node.NodeInfo = "SKIP";
                                }
                            }
                            else
                            {
                                if (node.ToString().Contains("DATABASE"))
                                {
                                    node.Children.Clear();
                                    node.AddChild(new TreeNode(Tokens.Function, "EXECUTE", "SKIP"));
                                    node.AddChild(new TreeNode(Tokens.Expression, "DBMS_STATS.GATHER_DATABASE_STATS", "SKIP"));
                                }
                                else
                                {
                                    TreeNode tableNode = targetTree.FindNode(node, Tokens.Identifier);
                                    if (tableNode != null)
                                    {
                                        node.Children.Clear();
                                        string[] tableInfo = tableNode.NodeValue.Split('.');
                                        string tableName = GetTableName(tableNode.NodeValue);
                                        string ownerName = "SYSADM";
                                        if (tableInfo.Length > 1)
                                        {
                                            ownerName = tableInfo[0];
                                        }

                                        node.AddChild(new TreeNode(Tokens.Function, "CALL", "SKIP"));
                                        TreeNode expressionNode = new TreeNode(Tokens.Expression, "DBMS_STATS.GATHER_TABLE_STATS", "SKIP");
                                        expressionNode.AddChild(new TreeNode(Tokens.LeftParant, "(", "SKIP"));
                                        expressionNode.AddChild(new TreeNode(Tokens.Identifier, "'" + ownerName + "'", "SKIP"));
                                        expressionNode.AddChild(new TreeNode(Tokens.Comma, ",", "SKIP"));
                                        expressionNode.AddChild(new TreeNode(Tokens.Identifier, "'" + tableName + "'", "SKIP"));
                                        expressionNode.AddChild(new TreeNode(Tokens.RightParant, ")", "SKIP"));
                                        node.AddChild(expressionNode);
                                    }
                                }
                            }
                        }
                        break;

                    case Tokens.Select:
                        if (Globals.TableInformation.IsInitialized)
                        {
                            TreeNodeCollection tables = new TreeNodeCollection();
                            if (targetTree.FindAll(node, Tokens.Table, ref tables) > 0)
                            {
                                foreach (TreeNode tn in tables)
                                {
                                    string table = GetTableName(tn.NodeValue);
                                    if (this.HasExternalLongOrBinaryColumns(table))
                                    {
                                        int index = 0;
                                        {
                                            if (targetTree.FindNode(node, "INTO") != null)
                                            {
                                                for (int i = 0; i < node.Children[0].Children.Count; i++)
                                                {
                                                    TreeNode childNode = node.Children[0].Children[i];
                                                    if (childNode.NodeType == Tokens.Expression)
                                                    {
                                                        index++;
                                                        TreeNode column = targetTree.FindNode(childNode, Tokens.Column);
                                                        bool isBinary, isLong, isExternal;
                                                        if (column != null)
                                                        {
                                                            if (this.GetColumnProperties(GetColumnName(column.NodeValue), out isBinary, out isLong, out isExternal))
                                                            {
                                                                if (isBinary)
                                                                {
                                                                    if (rawPositions != null)
                                                                    {
                                                                        if (rawPositions.Length > 0)
                                                                        {
                                                                            rawPositions.Append(",");
                                                                        }
                                                                        rawPositions.Append(index.ToString());
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        break;
                }

                for (int i = 0; i < node.Children.Count; i++)
                {
                    TransformTreeSqlBase_Oracle(ref targetTree, root, node.Children[i]);
                    if (manyWithOneReplaced > 0)
                    {
                        i -= (1 + manyWithOneReplaced);
                        manyWithOneReplaced = 0;
                    }
                }
            }
        }

        private void TransformMultiformPredicate(Tree tree, TreeNode newPredicate, TreeNode oldPredicate, TreeNode updTable, TreeNode fromTables, TreeNodeCollection columns, TreeNode where, TreeNode updFrom)
        {
            for (int i = 0; i < oldPredicate.Children.Count; i++)
            {
                TreeNode node = oldPredicate.Children[i];
                switch (node.NodeType)
                {
                    case Tokens.Expression:
                        {
                            TransformMultiformPredicate(tree, newPredicate, node, updTable, fromTables, columns, where, updFrom);
                            break;
                        }
                    case Tokens.Select:
                        {
                            TreeNode whereNode = null;
                            TreeNode fromNode = null;
                            for (int j = node.Children.Count - 1; j >= 0; j--)
                            {
                                if (node.Children[j].NodeValue == "WHERE")
                                {
                                    whereNode = node.Children[j].Children[0]; //where.searchcondition node
                                }
                                else if (node.Children[j].NodeValue == "FROM")
                                {
                                    fromNode = node.Children[j];
                                }
                            }
                            TreeNodeCollection tables = new TreeNodeCollection();
                            tree.FindAll(fromNode, Tokens.Table, ref tables);
                            for (int j = 0; j < tables.Count; j++)
                            {
                                if (updFrom.Children.Count > 0)
                                {
                                    updFrom.AddChild(new TreeNode(Tokens.Comma, ","));
                                }
                                updFrom.AddChild(tables[j].Clone(updFrom));
                            }

                            if (whereNode != null)
                            {
                                tables.Add(updTable);
                                TreeNodeCollection whereColumns = new TreeNodeCollection();
                                tree.FindAll(whereNode, Tokens.Column, ref whereColumns);

                                bool structEnabled = (Settings != null && Settings.DbStructure != null);
                                for (int j = 0; j < whereColumns.Count; j++)
                                {
                                    if (whereColumns[j].NodeValue.LastIndexOf(".") == -1)
                                    {
                                        string tableName = "";
                                        if (structEnabled)
                                        {
                                            TreeNode table = GetTable(whereColumns[j], tables);
                                            if (table != null)
                                            {
                                                tableName = (table.Children.Count > 0) ? (table.Children[0].NodeValue + ".") : (table.NodeValue + ".");
                                            }
                                        }
                                        whereColumns[j].NodeValue = tableName + whereColumns[j].NodeValue;
                                    }
                                }

                                if (where.Children.Count > 0)
                                {
                                    where.AddChild(new TreeNode(Tokens.Keyword, "AND"));
                                }
                                where.AddChild(new TreeNode(Tokens.LeftParant, "(", "GROUP START"));
                                where.AddChild(whereNode.Clone(where));
                                where.AddChild(new TreeNode(Tokens.RightParant, ")", "GROUP END"));
                            }

                            node = node.Children[0];  //select node
                            for (int j = 0; j < node.Children.Count; j++)
                            {
                                TransformMultiformPredicate(tree, newPredicate, node.Children[j], updTable, fromNode, columns, where, updFrom);
                            }
                            break;
                        }
                    case Tokens.Column:
                    case Tokens.StringConst:
                    case Tokens.NumericConst:
                    case Tokens.BindVariable:
                        {
                            TreeNode column = null;
                            for (int j = 0; j < columns.Count; j++)
                            {
                                if (columns[j].NodeInfo != "SKIP")
                                {
                                    column = columns[j];
                                    break;
                                }
                            }
                            if (column != null)
                            {
                                column.NodeInfo = "SKIP";

                                if (newPredicate.Children.Count > 0)
                                {
                                    newPredicate.AddChild(new TreeNode(Tokens.Comma, ","));
                                }
                                newPredicate.AddChild(new TreeNode(column));
                                newPredicate.AddChild(new TreeNode(Tokens.RelatOperator, "="));
                                if (fromTables == null)
                                {
                                    newPredicate.AddChild(new TreeNode(Tokens.Column, node.NodeValue));
                                }
                                else  //enter here from a select statement
                                {
                                    string tableName = "";
                                    TreeNodeCollection tables = new TreeNodeCollection();

                                    if (node.NodeType == Tokens.Column && node.NodeValue.LastIndexOf(".") == -1 && Settings != null && Settings.DbStructure != null)
                                    {
                                        for (int n = 0; n < fromTables.Children.Count; n++)
                                        {
                                            if (fromTables.Children[n].NodeType == Tokens.Table)
                                            {
                                                tables.Add(fromTables.Children[n]);
                                            }
                                        }

                                        TreeNode table = GetTable(node, tables);
                                        if (table != null)
                                        {
                                            if (table.Children.Count > 0)
                                            {
                                                tableName = table.Children[0].NodeValue + ".";
                                            }
                                            else
                                            {
                                                tableName = table.NodeValue + ".";
                                            }
                                        }
                                    }

                                    if (tableName == "" && node.NodeValue.LastIndexOf(".") == -1)
                                    {
                                        if (fromTables.Children[0].Children.Count > 0)
                                        {
                                            tableName = fromTables.Children[0].Children[0].NodeValue + ".";
                                        }
                                        else
                                        {
                                            tableName = fromTables.Children[0].NodeValue + ".";
                                        }
                                    }
                                    newPredicate.AddChild(new TreeNode(Tokens.Column, tableName + node.NodeValue));
                                }
                            }

                            break;
                        }
                }
            }
        }

        private void TransformTreeInformix_MsSql(ref Tree targetTree, TreeNode root, TreeNode node, ref int idxNextNode) //ASZ: while transforming current node if a function deletes nodes it must decrement index to process the correct next node
        {
            if (node.NodeType == Tokens.Select)
                root = node;

            if (node.NodeInfo != "SKIP")
            {
                //Transformations for current node
                switch (node.NodeType)
                {
                    case Tokens.Function: //Replace function name with the corresponding function from the functions table
                        {
                            if (node.NodeValue == "HEX")
                            {
                                node.NodeValue = "sys.fn_varbintohexstr";

                                TreeNode expr = node.Children[1];
                                node.Children[1] = new TreeNode(Tokens.Function, "CONVERT");
                                node.Children[1].AddChild(new TreeNode(Tokens.LeftParant, "("));

                                TreeNode tmp = node.Children[1].AddChild(new TreeNode(Tokens.DataType));
                                tmp.AddChild(new TreeNode(Tokens.DataTypeKeyword, "VARBINARY"));
                                tmp.AddChild(new TreeNode(Tokens.LeftParant, "("));
                                tmp.AddChild(new TreeNode(Tokens.NumericConst, "4"));
                                tmp.AddChild(new TreeNode(Tokens.RightParant, ")"));

                                node.Children[1].AddChild(new TreeNode(Tokens.Comma, ","));
                                node.Children[1].AddChild(expr);
                                node.Children[1].AddChild(new TreeNode(Tokens.RightParant, ")"));
                            }
                            else if (node.NodeValue == "DECODE")
                            {
                                //DECODE(expr, search1, return1, search2, return2, ..., [default])
                                TreeNode conditionNode = node.Children[1];
                                bool reverseWHEN = false;
                                if (node.ToString().Contains("NULL"))
                                {
                                    reverseWHEN = true;
                                }

                                //Replace function name with "CASE"
                                node.NodeValue = "CASE";

                                node.RemoveChild(0); //Remove left paranthesis
                                TreeNode valueNode;
                                TreeNode returnNode;
                                TreeNode exprNode;

                                TreeNode currentNode = node.Children[1];
                                while (currentNode.NodeType == Tokens.Comma)
                                {
                                    //Remove comma
                                    node.RemoveChild(currentNode);

                                    //Extract search expression
                                    valueNode = node.Children[1]; //expression
                                    node.RemoveChild(valueNode);


                                    //Extract return expression
                                    if (((TreeNode)node.Children[1]).NodeType == Tokens.Comma)
                                    {
                                        node.RemoveChild(node.Children[1]); //Remove comma
                                        returnNode = node.Children[1];
                                        node.RemoveChild(returnNode);

                                        //Create the WHEN expression
                                        exprNode = new TreeNode(Tokens.Expression);
                                        exprNode.AddChild(new TreeNode(Tokens.Keyword, "WHEN"));

                                        if (reverseWHEN)
                                        {
                                            exprNode.AddChild(conditionNode);
                                            if (valueNode.ToString().Contains("NULL"))
                                            {
                                                exprNode.AddChild(new TreeNode(Tokens.Keyword, "IS"));
                                            }
                                            else
                                            {
                                                exprNode.AddChild(new TreeNode(Tokens.Keyword, "="));
                                            }
                                        }

                                        exprNode.AddChild(valueNode);
                                        exprNode.AddChild(new TreeNode(Tokens.Keyword, "THEN"));
                                        exprNode.AddChild(returnNode);
                                    }

                                    //Create default
                                    else
                                    {
                                        node.RemoveChild(node.Children[1]); //Remove right paranthesis
                                        exprNode = new TreeNode(Tokens.Expression);
                                        exprNode.AddChild(new TreeNode(Tokens.Keyword, "ELSE"));
                                        exprNode.AddChild(valueNode);
                                    }

                                    //Add expression to the function node
                                    node.AddChild(exprNode);

                                    currentNode = node.Children[1];
                                }


                                if (currentNode.NodeType == Tokens.RightParant)
                                    node.RemoveChild(1); //Remove right paranthesis

                                //Add "END" keyword
                                node.AddChild(new TreeNode(Tokens.Keyword, "END"));

                                if (reverseWHEN)
                                {
                                    node.RemoveChild(0);
                                }
                            }
                            else
                            {
                                node.NodeValue = FunctionTranslator.TranslateFunction(DatabaseBrand.Informix, DatabaseBrand.SqlServer, node.NodeValue);
                                node.NodeInfo = "SKIP";
                            }
                            break;
                        }

                    case Tokens.Keyword:
                        {
                            switch (node.NodeValue)
                            {
                                case "INSERT INTO":
                                    {   //insert into at_hhkto_pass17 select * from at_hhkto_pass16                                     
                                        if (node.Index == 0 && node.Children.Count == 1 && node.Children[0].NodeType == Tokens.Table && node.Parent.Children.Count > 1 && node.Parent.Children[1].NodeType == Tokens.Select)
                                        {
                                            TreeNode nodeSelect = node.Parent.Children[1].Children[0];
                                            TreeNode nodeFrom = (node.Parent.Children[1].Children[1].NodeValue == "FROM") ? node.Parent.Children[1].Children[1] : null;

                                            if (nodeSelect.Children[0].NodeType == Tokens.Asterisk && nodeFrom != null && nodeFrom.Children.Count > 0 && nodeFrom.Children[0].NodeType == Tokens.Table && Settings != null && Settings.DbStructure != null)
                                            {
                                                List<Column> columns = Settings.DbStructure.GetAllColumns(nodeFrom.Children[0].NodeValue);
                                                if (columns.Count > 0)
                                                {
                                                    nodeSelect.Children.Clear();

                                                    node.InsertChild(new TreeNode(Tokens.RightParant, ")"), 1);
                                                    //nodeSelect.InsertChild(new TreeNode(Tokens.RightParant, ")"), 0);

                                                    for (int i = columns.Count - 1; i >= 0; i--)
                                                    {
                                                        if (columns[i].Name.ToUpper() != "ROWID")
                                                        {
                                                            node.InsertChild(new TreeNode(Tokens.Column, columns[i].Name), 1);
                                                            nodeSelect.InsertChild(new TreeNode(Tokens.Column, columns[i].Name), 0);
                                                            if (i > 0)
                                                            {
                                                                node.InsertChild(new TreeNode(Tokens.Comma, ","), 1);
                                                                nodeSelect.InsertChild(new TreeNode(Tokens.Comma, ","), 0);
                                                            }
                                                        }
                                                    }
                                                    node.InsertChild(new TreeNode(Tokens.LeftParant, "("), 1);
                                                    //nodeSelect.InsertChild(new TreeNode(Tokens.LeftParant, "("), 0);
                                                }
                                            }
                                        }
                                        break;
                                    }
                                case "CURRENT":
                                    {
                                        node.NodeValue = "GETDATE()";
                                        node.Children.Clear();
                                        break;
                                    }
                                case "SET":
                                    {
                                        if (node.NodeInfo == "MULTI FORMAT")
                                        {
                                            TreeNode predLeft = node.Children[0];
                                            TreeNode oper = node.Children[1];
                                            TreeNode predRight = node.Children[2];
                                            if (predLeft.Children[0].NodeType != Tokens.LeftParant || predRight.Children[0].NodeType != Tokens.LeftParant || oper.NodeValue != "=")
                                            {
                                                throw new NotImplementedException("paranthese or relation missing: update table set(...) = (...)");
                                            }
                                            else if (predRight.Children[1].NodeType == Tokens.Expression && predRight.Children[1].Children[0].NodeType == Tokens.LeftParant && predRight.Children[1].Children[1].NodeType == Tokens.Select)
                                            {   //ASZ:update at_vsab_Pass16 set(agbez,agkomp,agse,agzaehler,agwasser,agkanal) = ((select ag_text, ag_komp, ag_se, ag_zaehler, ag_wasser, ag_kanal from at_ag_Pass16 where ag_nummer = argruppe)) where vsid = :fpnVsid
                                                TreeNodeCollection columnNodes = new TreeNodeCollection();
                                                targetTree.FindAll(node.Children[0], Tokens.Column, ref columnNodes);

                                                TreeNode whereNode = null;
                                                for (int i = node.Parent.Children.Count - 1; i >= 0; i--)
                                                {
                                                    if (node.Parent.Children[i].NodeValue == "WHERE")
                                                    {
                                                        whereNode = node.Parent.Children[i];
                                                        break;
                                                    }
                                                }

                                                TreeNode tableNode = node.Parent.Children[node.Index - 1].Children[0];
                                                TreeNode newPredicate = new TreeNode(Tokens.Predicate);
                                                TreeNode updFrom = new TreeNode(Tokens.Keyword, "FROM");
                                                if (whereNode != null)
                                                {
                                                    whereNode.Children[0].InsertChild(new TreeNode(Tokens.LeftParant, "(", "GROUP START"), 0);
                                                    whereNode.Children[0].AddChild(new TreeNode(Tokens.RightParant, ")", "GROUP END"));
                                                    TransformMultiformPredicate(targetTree, newPredicate, node.Children[2], tableNode, null, columnNodes, whereNode.Children[0], updFrom);
                                                }
                                                else
                                                {
                                                    whereNode = new TreeNode(Tokens.Keyword, "WHERE");
                                                    whereNode.AddChild(new TreeNode(Tokens.SearchCond));
                                                    TransformMultiformPredicate(targetTree, newPredicate, node.Children[2], tableNode, null, columnNodes, whereNode.Children[0], updFrom);
                                                    if (whereNode.Children[0].Children.Count > 0)
                                                    {
                                                        node.Parent.AddChild(whereNode);
                                                    }
                                                }
                                                node.Children.Clear();
                                                if (updFrom.Children.Count > 0)
                                                {
                                                    node.Parent.InsertChild(updFrom, node.Index + 1);
                                                }
                                                node.Children.Add(newPredicate);
                                            }
                                            else
                                            {   //ASZ:UPDATE kb_kbuch_pass16 SET (upd_user_kt, upd_stamp_kt, upd_user_ub) = (@p0, current year to fraction(5), @p1) WHERE kb_id = @p2                                            
                                                TreeNodeCollection columnNodes = new TreeNodeCollection();
                                                targetTree.FindAll(predLeft, Tokens.Column, ref columnNodes);
                                                TreeNode newPredicate = new TreeNode(Tokens.Predicate);
                                                for (int i = 0; i < columnNodes.Count; i++)
                                                {
                                                    if (i > 0)
                                                    {
                                                        newPredicate.AddChild(new TreeNode(Tokens.Comma, ","));
                                                    }
                                                    newPredicate.AddChild(new TreeNode(columnNodes[i]));
                                                    newPredicate.AddChild(new TreeNode(Tokens.RelatOperator, "="));
                                                    newPredicate.AddChild(new TreeNode(predRight.Children[1 + i * 2]));
                                                }
                                                node.Children.Clear();
                                                node.Children.Add(newPredicate);
                                            }
                                        }
                                        break;
                                    }
                                case "STATISTICS":
                                    {
                                        targetTree.root = new TreeNode(Tokens.Statement, "EXEC sp_updatestats");
                                    }
                                    break;
                                //SqlServer doens't support GROUP BY integer constant
                                //have to replace constants with column names
                                case "GROUP BY":
                                    {
                                        TreeNode selectNode = new TreeNode(targetTree.FindNode(root, "SELECT"), "");
                                        int[] ExpressionIndex = new int[selectNode.Children.Count];
                                        int order = 0;

                                        for (int i = 0; i < selectNode.Children.Count; i++)
                                        {
                                            if (selectNode.Children[i].NodeType == Tokens.Expression)
                                            {
                                                ExpressionIndex[order++] = i;
                                            }
                                        }
                                        for (int i = 0; i < node.Children.Count; i++)
                                        {
                                            if (node.Children[i].NodeType == Tokens.NumericConst)
                                            {
                                                TreeNode currentNode = new TreeNode(selectNode.Children[ExpressionIndex[Convert.ToInt32(node.Children[i].NodeValue) - 1]], "");

                                                //The GROUP BY expression has to contain a column
                                                if (targetTree.FindNode(currentNode, Tokens.Column) != null)
                                                {

                                                    for (int k = 0; k < currentNode.Children.Count; k++)
                                                    {
                                                        if (currentNode.Children[k].NodeValue == "AS")
                                                        {
                                                            currentNode.RemoveChild(k);
                                                            currentNode.RemoveChild(k);
                                                        }
                                                    }
                                                    node.Children[i] = currentNode;
                                                }
                                                else
                                                {
                                                    //Remove the number from the GROUP BY list
                                                    node.Children[i].NodeInfo = "NOT USED";

                                                    if (i == node.Children.Count - 1)
                                                    {
                                                        if (i - 1 > 0 && node.Children[i - 1].NodeInfo != "NOT USED")
                                                        {
                                                            node.Children[i - 1].NodeInfo = "NOT USED";
                                                        }
                                                        else
                                                        {
                                                            for (int j = node.Children.Count - 1; j >= 0; j--)
                                                            {
                                                                if (node.Children[j].NodeType == Tokens.Comma &&
                                                                    node.Children[j].NodeInfo != "NOT USED")
                                                                {
                                                                    node.Children[j].NodeInfo = "NOT USED";
                                                                    break;
                                                                }
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        node.Children[i + 1].NodeInfo = "NOT USED";
                                                    }

                                                    //If GROUP BY node does not contain any used nodes remove the GROUP BY node
                                                    bool hasUsedChildren = false;
                                                    foreach (TreeNode childNode in node.Children)
                                                    {
                                                        if (childNode.NodeInfo != "NOT USED")
                                                        {
                                                            hasUsedChildren = true;
                                                        }
                                                    }

                                                    if (!hasUsedChildren)
                                                    {
                                                        node.NodeInfo = "NOT USED";
                                                    }
                                                }
                                            }
                                        }
                                        break;
                                    }

                                case "OUTER":
                                    {
                                        //asz: don't translate LEFT/RIGHT OUTER JOIN
                                        int idx = node.Index;
                                        if (idx >= 0 && idx < node.Parent.Children.Count && node.Parent.Children[idx + 1].NodeValue != "JOIN")
                                        {
                                            //TranslateOuterJoinsInformix_old(targetTree, root, node);
                                            TranslateOuterJoinsInformix(targetTree, node, ref idxNextNode);
                                        }
                                        break;
                                    }

                                case "CREATE TEMP TABLE":
                                    {
                                        //In SqlServer the temp tables are prefixes with #
                                        //ASZ:delete temp table if exist before create, else error allready exists                                        
                                        node.NodeValue = @"IF OBJECT_ID('tempdb..#" + node.Children[0].NodeValue + @"') IS NOT NULL DROP TABLE #" + node.Children[0].NodeValue + " CREATE TABLE";
                                        node.Children[0].NodeValue = "#" + node.Children[0].NodeValue;
                                        break;
                                    }
                                case "LIKE":  //ASZ: if col is "nchar" type: where col like '...' -> where RTRIM(col) like '...'
                                    {
                                        if (Settings == null || Settings.DbStructure == null)
                                        {
                                            break;
                                        }
                                        if (node.Parent == null || node.Parent.Parent == null || node.Parent.Parent.Parent == null || node.Parent.Parent.Parent.Parent == null)
                                        {
                                            break;
                                        }
                                        if (node.Parent.NodeType != Tokens.Predicate || node.Parent.Parent.NodeType != Tokens.SearchCond || node.Parent.Parent.Parent.NodeValue != "WHERE" || node.Parent.Parent.Parent.Parent.NodeType != Tokens.Select)
                                        {
                                            break;
                                        }
                                        TreeNode nodeFrom = null;
                                        if (node.Parent.Parent.Parent.Index > 0)
                                        {
                                            nodeFrom = node.Parent.Parent.Parent.Parent.Children[node.Parent.Parent.Parent.Index - 1];
                                        }
                                        if (nodeFrom == null)
                                        {
                                            break;
                                        }

                                        TreeNode likeLeft = node.Parent.Children[node.Index - 1];
                                        //TreeNode likeRight = node.Parent.Children[node.Index + 1];

                                        TreeNodeCollection tables = new TreeNodeCollection();
                                        targetTree.FindAll(nodeFrom, Tokens.Table, ref tables);

                                        TreeNodeCollection columns = new TreeNodeCollection();
                                        targetTree.FindAll(likeLeft, Tokens.Column, ref columns);

                                        bool needTrim = false;
                                        foreach (TreeNode col in columns)
                                        {
                                            string colname = col.NodeValue;
                                            int i = colname.IndexOf('.');
                                            if (i >= 0)
                                            {
                                                colname = colname.Substring(i + 1);
                                            }
                                            TreeNode table = GetTable(col, tables);
                                            if (table != null)
                                            {
                                                //string datatype = Settings.DbStructure[colname, table.NodeValue];
                                                string datatype = Settings.DbStructure[colname, Settings.Schemas.FindTable(table.GetSchema(Settings), table.NodeValue)?.Name];
                                                if (datatype == "nchar")
                                                {
                                                    TreeNode nd = (col.Parent != null && col.Parent.NodeType == Tokens.Expression) ? col.Parent.Parent : col.Parent;
                                                    if (nd == null || nd.NodeType != Tokens.Function || nd.NodeValue != "RTRIM")
                                                    {
                                                        needTrim = true;
                                                    }
                                                }
                                            }
                                        }
                                        if (needTrim)
                                        {
                                            TreeNode nd = new TreeNode(Tokens.Function, "RTRIM");
                                            nd.Parent = likeLeft.Parent;
                                            nd.AddChild(new TreeNode(Tokens.LeftParant, "("));
                                            nd.AddChild(likeLeft);
                                            nd.AddChild(new TreeNode(Tokens.RightParant, ")"));
                                            node.Parent.Children[node.Index - 1] = nd;
                                        }
                                        break;
                                    }
                            }
                            break;
                        }
                    case Tokens.Concatenate:
                        {
                            //ASZ:concatenate left and right nodes
                            node.NodeValue = "+";

                            int idx = node.Index;
                            if (idx == 0 || idx == node.Parent.Children.Count - 1)
                            {
                                break;
                            }

                            TransformConcatenate(targetTree, node.Parent.Children[idx - 1]);

                            TransformTreeInformix_MsSql(ref targetTree, root, node.Parent.Children[idx + 1], ref idxNextNode);

                            TransformConcatenate(targetTree, node.Parent.Children[idx + 1]);

                            break;
                        }

                    case Tokens.View: //Replace view owner
                        {
                            if (!String.IsNullOrEmpty(owner))
                            {
                                node.NodeValue = node.NodeValue.Remove(0, node.NodeValue.IndexOf(".") + 1);

                                //If the table name is a reserved word, surround it with ""
                                if (LexicalAnalyzerSqlBase.IsKeyword(node.NodeValue))
                                {
                                    node.NodeValue = "\"" + node.NodeValue + "\"";
                                }

                                node.NodeValue = owner + "." + node.NodeValue;
                            }

                            node.NodeValue = GetNameWithCase(node.NodeValue);
                            break;
                        }

                    case Tokens.StringConst:
                        {
                            //Replace double quotes with single quotes
                            node.NodeValue = Regex.Replace(node.NodeValue, "^\"(.*)\"$", "'$1'");

                            //ASZ: transform: column like 'identifier' into column = 'identifier%'
                            if (node.Parent != null && node.Parent.NodeType == Tokens.Expression && node.Parent.Parent != null)
                            {
                                int idx = node.Parent.Parent.Children.IndexOf(node.Parent);
                                if (idx > 0 && node.Parent.Parent.Children[idx - 1].NodeType == Tokens.Keyword && node.Parent.Parent.Children[idx - 1].NodeValue == "LIKE")
                                {

                                    if (node.NodeValue.IndexOf('%') == -1)
                                    {
                                        node.Parent.Parent.Children[idx - 1].NodeValue = "=";
                                    }
                                }
                            }
                            break;
                        }
                    case Tokens.StringIndexer:
                        {
                            //Replace column[1,1] with SUBSTRING(column, 1, 1)
                            //ASZ:Replace column[n,k] with SUBSTRING(column, n, k-n+1)
                            if (node.Children.Count == 5)
                            {
                                node.NodeType = Tokens.Function;
                                node.NodeValue = "SUBSTRING";
                                node.Children[0].NodeType = Tokens.Comma;
                                node.Children[0].NodeValue = ",";
                                node.Children[3].NodeValue = (Convert.ToInt32(node.Children[3].NodeValue) - Convert.ToInt32(node.Children[1].NodeValue) + 1).ToString();
                                node.Children[4].NodeType = Tokens.RightParant;
                                node.Children[4].NodeValue = ")";
                                node.InsertChild(node.Parent.Children[0], 0);
                                node.InsertChild(new TreeNode(Tokens.LeftParant, "("), 0);
                                node.Parent.Children.RemoveAt(0);
                                //node.Parent.Children[0].NodeValue = "SUBSTRING(" + node.Parent.Children[0].NodeValue;
                            }//ASZ:Replace column[n] with SUBSTRING(column, n, 1)
                            else if (node.Children.Count == 3)
                            {
                                node.NodeType = Tokens.Function;
                                node.NodeValue = "SUBSTRING";
                                node.Children[0].NodeType = Tokens.Comma;
                                node.Children[0].NodeValue = ",";
                                node.Children[2].NodeType = Tokens.Comma;
                                node.Children[2].NodeValue = ",";
                                node.Children.Add(new TreeNode(Tokens.NumericConst, "1"));
                                node.Children.Add(new TreeNode(Tokens.RightParant, ")"));
                                node.InsertChild(node.Parent.Children[0], 0);
                                node.InsertChild(new TreeNode(Tokens.LeftParant, "("), 0);
                                node.Parent.Children.RemoveAt(0);
                                //node.Parent.Children[0].NodeValue = "SUBSTRING(" + node.Parent.Children[0].NodeValue;
                            }
                            break;
                        }
                    case Tokens.Table:
                        {
                            //asz:replace SYSCOLUMNS with SYSCOLUMNSVIEW
                            if (node.NodeValue.ToUpper().EndsWith("SYSCOLUMNS"))
                            {
                                node.NodeValue = node.NodeValue.ToUpper().Replace("SYSCOLUMNS", "SYSCOLUMNSVIEW");
                            }
                            else if (node.NodeValue.ToUpper().EndsWith("SYSINDEXES"))
                            {
                                node.NodeValue = node.NodeValue.ToUpper().Replace("SYSINDEXES", "SYSINDEXES");
                            }
                            break;
                        }
                    case Tokens.DataTypeKeyword:
                        {
                            //asz:substitute "DATETIME YEAR TO FRACTION(5)" with "DATETIME2(7)"
                            if (node.NodeValue == "DATETIME YEAR TO FRACTION")
                            {
                                int idx = node.Parent.Children.IndexOf(node);
                                if (idx >= 0 && idx + 3 < node.Parent.Children.Count && node.Parent.Children[idx + 1].NodeType == Tokens.LeftParant && node.Parent.Children[idx + 3].NodeType == Tokens.RightParant)
                                {
                                    node.Parent.Children[idx + 2].NodeValue = "7";
                                    node.NodeValue = "DATETIME2";
                                }
                            }
                            else if (node.NodeValue.ToUpper() == "SERIAL" && root.NodeInfo == "CREATE" && root.Children[0].Children[0].NodeValue == "CREATE TABLE")
                            {
                                root.NodeInfo += "+SEQUENCE";
                                node.Parent.Children.Clear();
                                node.Parent.AddChild(new TreeNode(Tokens.DataTypeKeyword, "int"));
                                node.Parent.AddChild(new TreeNode(Tokens.Keyword, "not"));
                                node.Parent.AddChild(new TreeNode(Tokens.Keyword, "null"));
                                node.Parent.AddChild(new TreeNode(Tokens.Keyword, "constraint"));
                                node.Parent.AddChild(new TreeNode(Tokens.StringConst, "DF_" + root.Children[0].Children[0].Children[0].NodeValue + "_" + node.Parent.Parent.Children[node.Parent.Index - 1].NodeValue));
                                node.Parent.AddChild(new TreeNode(Tokens.Keyword, "DEFAULT"));
                                node.Parent.AddChild(new TreeNode(Tokens.Keyword, "NEXT"));
                                node.Parent.AddChild(new TreeNode(Tokens.Keyword, "VALUE"));
                                node.Parent.AddChild(new TreeNode(Tokens.Keyword, "FOR"));
                                node.Parent.AddChild(new TreeNode(Tokens.StringConst, "Sequence_" + root.Children[0].Children[0].Children[0].NodeValue));
                            }
                            break;
                        }
                    case Tokens.NumericConst:
                        {
                            //asz:varchar(-1) = varchar(MAX)
                            if (node.NodeValue == "-1")
                            {
                                int idx = node.Parent.Children.IndexOf(node) - 1;//left neighbor is VARCHAR( or NCHASR(
                                if (idx >= 1 && node.Parent.Children[idx].NodeType == Tokens.LeftParant && node.Parent.Children[idx - 1].NodeType == Tokens.DataTypeKeyword)
                                {
                                    node.NodeValue = "MAX";
                                }
                            }
                            break;
                        }
                }
            }
            for (int i = 0; i < node.Children.Count; i++)
            {
                TransformTreeInformix_MsSql(ref targetTree, root, node.Children[i], ref i);
            }

            if (node.Index > 0 && node.Parent.Children[node.Index - 1].NodeType == Tokens.Column && node.Parent.Children[node.Index - 1].NodeValue.ToUpper() == "ROWID" && node.NodeType == Tokens.DataType && node.Children[0].NodeValue.ToUpper() == "INTEGER")
            {
                node.AddChild(new TreeNode(Tokens.StringConst, "IDENTITY(1, 1)"));
            }
        }

        //ASZ:transforms a TreeNode to string type for concatenation
        //returns the index of last element from the node's neighborhood to continue from on recursive processing
        private int TransformConcatenate(Tree targetTree, TreeNode convertNode)
        {
            int idxRet = convertNode.Parent.Children.IndexOf(convertNode);
            if (convertNode.NodeValue.ToUpper().StartsWith("CONVERT(NVARCHAR")) return idxRet;

            switch (convertNode.NodeType)
            {
                case Tokens.Column: //ASZ:transform column to string if not string type
                    {
                        TreeNode fromNode = convertNode;
                        while (fromNode != null && targetTree.FindNode(fromNode, "FROM") == null)
                        {
                            fromNode = fromNode.Parent;
                        }
                        if (fromNode != null)
                        {
                            var colInfo = GetColumnInfo(convertNode, targetTree, fromNode);
                            //if not a column or not (n)varchar
                            if (colInfo == null || (colInfo.Type != "varchar" && colInfo.Type != "nvarchar"))
                            {
                                convertNode.NodeValue = "CONVERT(nvarchar(MAX)," + convertNode.NodeValue + ")";
                                //convertNode.NodeInfo = "changed";
                            }
                        }
                        break;
                    }
                case Tokens.NumericConst:  //ASZ:transform number to string
                    {
                        convertNode.NodeValue = "'" + convertNode.NodeValue + "'";
                        break;
                    }
                case Tokens.Function: //ASZ:transform function parameters recursively
                    {
                        switch (convertNode.NodeValue.ToUpper())
                        {
                            case "ISNULL":
                                {
                                    //ISNULL's check_expression and replacement_value parameters must convert to string on concatenation
                                    TreeNode x = targetTree.FindNode(convertNode, Tokens.Expression);
                                    TreeNode y = targetTree.FindNodeReverse(convertNode, Tokens.Expression);

                                    TransformConcatenate(targetTree, x);
                                    TransformConcatenate(targetTree, y);

                                    break;
                                }
                        }
                        break;
                    }
                case Tokens.Expression: //ASZ:transform expression elements recursively                
                    {
                        for (int i = 0; i < convertNode.Children.Count; i++)
                        {
                            TransformConcatenate(targetTree, convertNode.Children[i]);
                        }
                        break;
                    }
                //ASZ: transform keyword elements/parameters
                case Tokens.Keyword:
                    {
                        if (convertNode.NodeValue == "CASE")
                        {
                            idxRet++;
                            while (convertNode.Parent.Children[idxRet].NodeValue != "END")
                            {
                                if (convertNode.Parent.Children[idxRet].NodeValue == "WHEN")
                                {
                                    do
                                    {
                                        idxRet++;
                                    } while (convertNode.Parent.Children[idxRet].NodeValue != "THEN");

                                }

                                if (convertNode.Parent.Children[idxRet].NodeValue == "THEN" || convertNode.Parent.Children[idxRet].NodeValue == "ELSE")
                                {
                                    idxRet++;
                                    //here suppose to be expression or column or string constant
                                    idxRet = TransformConcatenate(targetTree, convertNode.Parent.Children[idxRet]);
                                }
                                idxRet++;
                            }
                        }
                        break;
                    }
            }
            return idxRet;
        }

        /// <summary>
        /// Creates node that will apply CAST for target node
        /// </summary>
        /// <param name="targetNode"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private TreeNode ApplyCast(TreeNode targetNode, string type)
        {
            var castNode = new TreeNode(Tokens.Function, "CAST");
            castNode.AddChild(new TreeNode(Tokens.LeftParant, "("));
            castNode.AddChild(targetNode);
            castNode.AddChild(new TreeNode(Tokens.Expression, String.Format(" AS {0} ", type)));
            castNode.AddChild(new TreeNode(Tokens.RightParant, ")"));
            return castNode;
        }

        private void TranslateColumn(TreeNode node, TreeNode root, Tree targetTree)
        {
            //TODO: Surrounding columns with "" is not working
            //surround the column with "" because the column name might be a reserved word in Oracle
            //table.column -> table."column"    (the table name can be missing)
            //if (node.NodeInfo != "changed")
            //{
            //    node.NodeValue = Regex.Replace(node.NodeValue, @"^(?<table>(.*\.))?(?<column>(.*))$", "${table}\"${column}\"");
            //    node.NodeInfo = "changed";
            //}

            //replace table owner
            if (!String.IsNullOrEmpty(owner) && node.NodeValue.ToLower().StartsWith("sysadm."))
            {
                node.NodeValue = node.NodeValue.Remove(0, "sysadm.".Length);
                node.NodeValue = owner + "." + node.NodeValue;
            }
            //Change the table name to view when there are external long columns
            if (root.NodeType == Tokens.Select && Globals.TableInformation.IsInitialized && node.NodeValue.Contains("."))
            {
                string table = GetTableNameFromColumn(node.NodeValue);
                if (!String.IsNullOrEmpty(table) && this.HasExternalColumns(table))
                {
                    node.NodeValue = node.NodeValue.Replace(table, table + "_V");
                }
            }
            if (node.NodeValue.ToUpper() == "ROWID" || node.NodeValue.ToUpper().Contains(".ROWID"))
            {
                node.NodeValue = node.NodeValue.ToUpper().Replace("ROWID", "ROWVERSION");
            }

            if (Globals.TableInformation.IsInitialized)
            {
                bool isDate = false;
                bool isTime = false;
                bool isTimestamp = false;
                bool isNumeric = false;
                int milliseconds = 0;
                object dataPrecision = null;
                object dataScale = null;
                string colName = node.NodeValue;
                string tableName = GetTableNameFromColumn(node.NodeValue);

                this.GetColumnDateTimeProperties(GetColumnName(colName), out isDate, out isTime, out isTimestamp, out milliseconds);
                string dataType = "";
                this.GetColumnDataType(GetColumnName(colName), ref dataType);
                if (!string.IsNullOrEmpty(dataType) && dataType == "NUMBER")
                {
                    isNumeric = true;
                    this.GetNumericPrecision(colName, ref dataPrecision, ref dataScale);
                }
                TreeNode parentExpression = targetTree.FindTopmostExpression(node);
                if (parentExpression == null)
                {
                    parentExpression = new TreeNode(Tokens.Expression);
                    parentExpression.Children.Add(new TreeNode(Tokens.Null));
                }
                TreeNode topmostExpression = targetTree.FindTopmostExpression(node);
                if (topmostExpression == null)
                {
                    topmostExpression = new TreeNode(Tokens.Null);
                }
                if (isDate)
                {
                    if (node.Parent != null && node.Parent.Parent != null &&
                        ((node.Parent.NodeValue != "TO_CHAR" && node.Parent.Children.Count != 5) &&
                        node.Parent.Parent.NodeValue != "TO_CHAR" &&
                        parentExpression.Children[0].NodeValue != "TO_CHAR" &&
                        !targetTree.IsNodeFromCase(root, node)
                        && node.Parent.Parent != null
                        && (targetTree.PreviousNode(node, Tokens.MathOperator) == null && targetTree.NextNode(node, Tokens.MathOperator) == null
                        && (targetTree.PreviousNode(node, Tokens.Function) == null ||
                        (targetTree.PreviousNode(node, Tokens.Function) != null && targetTree.PreviousNode(node, Tokens.Function).NodeValue != "TO_CHAR")))
                        && node.Parent.Parent.NodeType != Tokens.Insert && node.Parent.Parent.NodeType != Tokens.Update
                        && targetTree.FindParent(node, Tokens.Update) == null
                        && node.Parent.Parent.NodeType != Tokens.SearchCond
                        && topmostExpression.NodeType != Tokens.SearchCond
                        && node.Parent.Parent.NodeValue != "GROUP BY"
                        && node.Parent.Parent.NodeValue != "ORDER BY"))
                    {
                        string cast = "";

                        bool isFromSearch = false;
                        if (targetTree.IsNodeFromSearch(root, node))
                        {
                            cast = "TO_DATE";
                            isFromSearch = true;
                        }
                        else if (node.Parent.Parent.NodeType == Tokens.Function ||
                            node.Parent.Parent.NodeType == Tokens.UserDefinedFunction)
                        {
                            TreeNode mathOperatorNode = null;
                            if (topmostExpression != null)
                            {
                                mathOperatorNode = targetTree.FindNode(topmostExpression, Tokens.MathOperator);
                            }

                            if (mathOperatorNode == null)
                            {
                                cast = "TO_CHAR";
                            }
                            else
                            {
                                cast = "TO_DATE";
                                isFromSearch = true;
                            }
                        }
                        else
                        {
                            TreeNodeCollection caseNodes = new TreeNodeCollection();
                            targetTree.FindAll(root, "CASE", ref caseNodes);
                            //TreeNode caseNode = targetTree.FindNode(root, "CASE");
                            if (caseNodes != null && caseNodes.Count == 0)
                            {
                                targetTree.FindAll(root, "CAST(CASE", ref caseNodes);
                                //caseNode = targetTree.FindNode(root, "CAST(CASE");
                            }
                            if (caseNodes != null)
                            {
                                TreeNode caseNode = null;
                                foreach (TreeNode caseNodeTemp in caseNodes)
                                {
                                    if (targetTree.FindNode(caseNodeTemp, node))
                                    {
                                        caseNode = caseNodeTemp;
                                        break;
                                    }
                                }
                                if (caseNode != null)
                                {
                                    TreeNodeCollection cols = new TreeNodeCollection();
                                    targetTree.FindAll(root, Tokens.Column, ref cols);
                                    bool containsDate = false;
                                    if (caseNode.ToString().Contains("TO_DATE") || caseNode.ToString().Contains("@DATE") || caseNode.ToString().Contains("AS DATE"))
                                    {
                                        containsDate = true;
                                    }
                                    if (!containsDate)
                                    {
                                        foreach (TreeNode colNode in cols)
                                        {
                                            this.GetColumnDateTimeProperties(GetColumnName(colNode.NodeValue), out isDate, out isTime, out isTimestamp, out milliseconds);
                                            if (isDate == true)
                                            {
                                                containsDate = true;
                                                break;
                                            }
                                        }
                                    }
                                    if (containsDate)
                                    {
                                        cast = "TO_DATE";
                                    }
                                    else
                                    {
                                        cast = "TO_CHAR";
                                    }
                                }
                                else
                                {
                                    cast = "TO_CHAR";
                                }
                            }
                            else
                            {
                                cast = "TO_CHAR";
                            }
                        }
                        if (!String.IsNullOrEmpty(Settings.DateFormat))
                        {
                            TreeNode prevNode = targetTree.PreviousNode(node, Tokens.Keyword);
                            TreeNode nextNode = targetTree.NextNode(node, Tokens.MathOperator);
                            if (prevNode == null && nextNode == null)
                            {
                                if (cast == "TO_DATE")
                                {
                                    node.NodeValue = cast + "(TO_CHAR(" + node.NodeValue + (isFromSearch == true ? ")" : ", " + Settings.DateFormat + ")") + (isFromSearch == true ? ")" : ", " + Settings.DateFormat + ")");
                                }
                                else
                                {
                                    node.NodeValue = cast + "(" + node.NodeValue + (isFromSearch == true ? ")" : ", " + Settings.DateFormat + ")");
                                }
                            }
                            else if (prevNode != null && prevNode.NodeValue != "AS")
                            {
                                if (cast == "TO_DATE")
                                {
                                    node.NodeValue = cast + "(TO_CHAR(" + node.NodeValue + (isFromSearch == true ? ")" : ", " + Settings.DateFormat + ")") + (isFromSearch == true ? ")" : ", " + Settings.DateFormat + ")");
                                }
                                else
                                {
                                    node.NodeValue = cast + "(" + node.NodeValue + (isFromSearch == true ? ")" : ", " + Settings.DateFormat + ")");
                                }
                            }
                            else if (nextNode != null && nextNode.NodeType != Tokens.MathOperator)
                            {
                                if (cast == "TO_DATE")
                                {
                                    node.NodeValue = cast + "(TO_CHAR(" + node.NodeValue;
                                    TreeNode expressionLastNode = node.Parent.Children[node.Parent.Children.Count - 1];
                                    expressionLastNode.NodeValue = expressionLastNode.NodeValue + (isFromSearch == true ? ")" : ", " + Settings.DateFormat + ")") + (isFromSearch == true ? ")" : ", " + Settings.DateFormat + ")");
                                }
                                else
                                {
                                    node.NodeValue = cast + "(" + node.NodeValue;
                                    TreeNode expressionLastNode = node.Parent.Children[node.Parent.Children.Count - 1];
                                    expressionLastNode.NodeValue = expressionLastNode.NodeValue + (isFromSearch == true ? ")" : ", " + Settings.DateFormat + ")");
                                }
                            }
                        }
                        else
                        {
                            TreeNode prevNode = targetTree.PreviousNode(node, Tokens.Keyword);
                            TreeNode nextNode = targetTree.NextNode(node, Tokens.MathOperator);
                            if (prevNode == null && nextNode == null)
                            {
                                if (cast == "TO_DATE")
                                {
                                    node.NodeValue = cast + "(TO_CHAR(" + node.NodeValue + (isFromSearch == true ? ")" : ", 'DD.MM.YYYY')") + (isFromSearch == true ? ")" : ", 'DD.MM.YYYY')");
                                }
                                else
                                {
                                    node.NodeValue = cast + "(" + node.NodeValue + (isFromSearch == true ? ")" : ", 'DD.MM.YYYY')");
                                }
                            }
                            else if (prevNode != null && prevNode.NodeValue != "AS")
                            {
                                if (cast == "TO_DATE")
                                {
                                    node.NodeValue = cast + "(TO_CHAR(" + node.NodeValue + (isFromSearch == true ? ")" : ", 'DD.MM.YYYY')") + (isFromSearch == true ? ")" : ", 'DD.MM.YYYY')");
                                }
                                else
                                {
                                    node.NodeValue = cast + "(" + node.NodeValue + (isFromSearch == true ? ")" : ", 'DD.MM.YYYY')");
                                }
                            }
                            else if (nextNode != null)
                            {
                                node.NodeValue = cast + "(" + node.NodeValue;
                                TreeNode expressionLastNode = node.Parent.Children[node.Parent.Children.Count - 1];
                                expressionLastNode.NodeValue = expressionLastNode.NodeValue + (isFromSearch == true ? ")" : ", 'DD.MM.YYYY')");
                            }
                        }

                        TreeNodeCollection predicateNodes = new TreeNodeCollection();
                        targetTree.FindAll(root, Tokens.Predicate, ref predicateNodes);
                        TreeNode predicate = null;
                        foreach (TreeNode n in predicateNodes)
                        {
                            if (targetTree.FindNode(n, node))
                            {
                                predicate = n;
                                break;
                            }
                        }

                        if (predicate != null && predicate.Children.Count == 3)
                        {
                            TreeNode leftOperand = predicate.Children[0];
                            TreeNode relatOperand = predicate.Children[1];
                            TreeNode rightOperand = predicate.Children[2];

                            TreeNode operandWithNode = null;
                            TreeNode operandWithoutNode = null;
                            if (targetTree.FindNode(leftOperand, node))
                            {
                                operandWithNode = leftOperand;
                                operandWithoutNode = rightOperand;
                            }
                            else
                            {
                                operandWithoutNode = leftOperand;
                                operandWithNode = rightOperand;
                            }
                            TreeNode dateToCharNode = targetTree.FindNode(operandWithoutNode, "@DATETOCHAR");
                            if (dateToCharNode != null && operandWithNode.NodeValue != "TO_CHAR" && operandWithNode.Children[0].NodeValue != "TO_CHAR")
                            {
                                TreeNode formatNode = targetTree.FindNode(dateToCharNode, Tokens.StringConst);
                                operandWithNode.Children.Insert(new TreeNode(Tokens.Function, "TO_CHAR", "SKIP"), 0);
                                operandWithNode.Children.Insert(new TreeNode(Tokens.LeftParant, "(", "SKIP"), 1);
                                operandWithNode.Children.Add(new TreeNode(Tokens.Comma, ",", "SKIP"));
                                operandWithNode.Children.Add(formatNode);
                                operandWithNode.Children.Add(new TreeNode(Tokens.RightParant, ")", "SKIP"));

                                if (operandWithoutNode.Children[0].NodeValue == "TRUNC")
                                {
                                    operandWithoutNode.Children[0].NodeValue = "";
                                }
                            }
                        }
                    }
                    else if (node.Parent != null && node.Parent.Parent != null &&
                        ((node.Parent.NodeValue != "TO_CHAR" && node.Parent.Children.Count != 5) &&
                        node.Parent.Parent.NodeValue != "TO_CHAR" &&
                        parentExpression.Children[0].NodeValue != "TO_CHAR" &&
                        !targetTree.IsNodeFromCase(root, node)
                        && node.Parent.Parent != null
                        && (targetTree.PreviousNode(node, Tokens.MathOperator) == null && targetTree.NextNode(node, Tokens.MathOperator) == null)
                        && (node.Parent.Parent.NodeType == Tokens.Insert || node.Parent.Parent.NodeType == Tokens.Update)
                        && node.Parent.Parent.NodeType != Tokens.SearchCond))
                    {
                        CastToTimestamp(targetTree, root, node, Settings.DateFormat, milliseconds);
                    }
                }
                else if (isTime)
                {
                    TreeNode prevNode = targetTree.PreviousNode(node, Tokens.Keyword);
                    if (node.Parent.Parent != null && node.Parent.Parent.NodeType != Tokens.Function
                        && node.Parent.Parent.NodeType != Tokens.Insert && node.Parent.Parent.NodeType != Tokens.Update
                        && (root.Children[0].NodeType != Tokens.Update && root.Children[0].NodeType != Tokens.Insert))
                    {
                        if (prevNode == null || (prevNode != null && prevNode.NodeValue != "AS"))
                        {
                            node.NodeValue = "TO_CHAR(" + node.NodeValue + ", 'HH24:MI:SS')";
                        }
                    }
                    else if (node.Parent.Parent != null && node.Parent.Parent.NodeType != Tokens.Function
                        && ((node.Parent.Parent.NodeType == Tokens.Insert || node.Parent.Parent.NodeType == Tokens.Update)
                        || (root.Children[0].NodeType == Tokens.Update || root.Children[0].NodeType == Tokens.Insert)))
                    {
                        string format = "'HH24:MI:SS'";
                        if (root.Children[0].NodeType == Tokens.Insert)
                        {
                            //The adjustmen accounts for the table name node and the left parathesis node
                            int adjustment = 1;
                            TreeNodeCollection cols = new TreeNodeCollection();

                            TreeNode valuesNode = targetTree.FindNode(root, "VALUES");
                            TreeNodeCollection valuesNodeCollection = new TreeNodeCollection();

                            if (valuesNode == null)
                            {
                                adjustment = 2;
                                valuesNode = targetTree.FindNode(root, "SELECT");
                            }

                            int colIndex = root.Children[0].Children[0].Children.IndexOf(node) - adjustment;

                            TreeNode intos = valuesNode.Children[colIndex];
                            if (intos.NodeValue != "SYSDATETIME" && intos.NodeValue != "SYSDATE" &&
                                !(intos.ToString().StartsWith("TO_CHAR") || intos.ToString().StartsWith("TO_DATE") || intos.ToString().StartsWith("@DATE")))
                            {
                                if (intos.NodeType == Tokens.Expression)
                                {
                                    intos.Children.Insert(new TreeNode(Tokens.Function, "TO_CHAR", "SKIP"), 0);
                                    intos.Children.Insert(new TreeNode(Tokens.LeftParant, "(", "SKIP"), 1);
                                    intos.Children.Add(new TreeNode(Tokens.Comma, ","));
                                    intos.Children.Add(new TreeNode(Tokens.StringConst, format));
                                    intos.Children.Add(new TreeNode(Tokens.RightParant, ")", "SKIP"));
                                }
                                else
                                {
                                    intos.NodeValue = "TO_CHAR(" + intos.NodeValue + " , " + format + ")";
                                }
                            }
                        }
                        else if (root.Children[0].NodeType == Tokens.Update)
                        {
                            int colIndex = root.Children[0].Children[1].Children.IndexOf(node);
                            TreeNode valueNode = null;
                            TreeNode binvarNode = null;
                            if (colIndex != -1)
                            {
                                valueNode = root.Children[0].Children[1].Children[colIndex + 2];
                                binvarNode = targetTree.FindNode(valueNode, Tokens.BindVariable);
                            }
                            else
                            {
                                valueNode = targetTree.FindParent(node, Tokens.Predicate);
                                colIndex = valueNode.Children.IndexOf(node);
                                valueNode = valueNode.Children[colIndex + 2];
                                binvarNode = targetTree.FindNode(valueNode, Tokens.BindVariable);
                            }
                            if (binvarNode != null)
                            {
                                valueNode.Children.Insert(new TreeNode(Tokens.Function, "TO_CHAR", "SKIP"), 0);
                                valueNode.Children.Insert(new TreeNode(Tokens.LeftParant, "(", "SKIP"), 1);
                                valueNode.Children.Add(new TreeNode(Tokens.Comma, ","));
                                valueNode.Children.Add(new TreeNode(Tokens.StringConst, format));
                                valueNode.Children.Add(new TreeNode(Tokens.RightParant, ")", "SKIP"));
                            }
                        }
                    }
                }
                else if (isTimestamp)
                {
                    if (milliseconds == 0)
                    {
                        TreeNode prevNode = targetTree.PreviousNode(node, Tokens.Keyword);
                        TreeNode topmostNode = targetTree.FindTopmostExpression(node);
                        if ((node.Parent.NodeValue != "TO_CHAR" && node.Parent.Children.Count != 5) &&
                            node.Parent.Parent.NodeValue != "TO_CHAR" &&
                            !targetTree.IsNodeFromCase(root, node)
                            && node.Parent.Parent != null
                            && (targetTree.PreviousNode(node, Tokens.MathOperator) == null && targetTree.NextNode(node, Tokens.MathOperator) == null)
                            && node.Parent.Parent.NodeType != Tokens.Insert && node.Parent.Parent.NodeType != Tokens.Update
                            && node.Parent.Parent.NodeType != Tokens.SearchCond && node.Parent.Parent.Parent.NodeType != Tokens.SearchCond
                            && topmostExpression.NodeType != Tokens.SearchCond && targetTree.FindParent(node, Tokens.Update) == null)
                        {
                            if (prevNode == null || (prevNode != null && prevNode.NodeValue != "AS"))
                            {
                                node.NodeValue = "TO_CHAR(" + node.NodeValue + ", 'YYYY-MM-DD HH24:MI:SS')";
                            }
                        }
                        else if ((node.Parent.NodeValue != "TO_CHAR" && node.Parent.Children.Count != 5) &&
                            node.Parent.Parent.NodeValue != "TO_CHAR" &&
                            !targetTree.IsNodeFromCase(root, node)
                            && node.Parent.Parent != null
                            && (targetTree.PreviousNode(node, Tokens.MathOperator) == null && targetTree.NextNode(node, Tokens.MathOperator) == null)
                            && (node.Parent.Parent.NodeType == Tokens.Insert || node.Parent.Parent.NodeType == Tokens.Update)
                            && node.Parent.Parent.NodeType != Tokens.SearchCond && node.Parent.Parent.Parent.NodeType != Tokens.SearchCond)
                        {
                            CastToTimestamp(targetTree, root, node, "'YYYY-MM-DD HH24:MI:SS'", milliseconds);
                        }
                    }
                    else
                    {
                        TreeNode prevNode = targetTree.PreviousNode(node, Tokens.Keyword);
                        if ((node.Parent.NodeValue != "TO_CHAR" && node.Parent.Children.Count != 5) &&
                            node.Parent.Parent.NodeValue != "TO_CHAR" &&
                            !targetTree.IsNodeFromCase(root, node)
                            && node.Parent.Parent != null
                            && (targetTree.PreviousNode(node, Tokens.MathOperator) == null && targetTree.NextNode(node, Tokens.MathOperator) == null)
                            && node.Parent.Parent.NodeType != Tokens.Insert && node.Parent.Parent.NodeType != Tokens.Update
                            && node.Parent.Parent.NodeType != Tokens.SearchCond && node.Parent.Parent.Parent.NodeType != Tokens.SearchCond)
                        {
                            if (prevNode == null || (prevNode != null && prevNode.NodeValue != "AS"))
                            {
                                if (milliseconds == 0)
                                {
                                    node.NodeValue = "TO_CHAR(" + node.NodeValue + ", 'YYYY-MM-DD HH24:MI:SS')";
                                }
                                else
                                {
                                    node.NodeValue = "TO_CHAR(" + node.NodeValue + ", 'YYYY-MM-DD HH24:MI:SS.FF" + milliseconds + "')";
                                }
                            }
                        }
                        else if ((node.Parent.NodeValue != "TO_CHAR" && node.Parent.Children.Count != 5) &&
                            node.Parent.Parent.NodeValue != "TO_CHAR" &&
                            !targetTree.IsNodeFromCase(root, node)
                            && node.Parent.Parent != null
                            && (targetTree.PreviousNode(node, Tokens.MathOperator) == null && targetTree.NextNode(node, Tokens.MathOperator) == null)
                            && (node.Parent.Parent.NodeType == Tokens.Insert || node.Parent.Parent.NodeType == Tokens.Update)
                            && node.Parent.Parent.NodeType != Tokens.SearchCond && node.Parent.Parent.Parent.NodeType != Tokens.SearchCond)
                        {
                            CastToTimestamp(targetTree, root, node, "'YYYY-MM-DD HH24:MI:SS'", milliseconds);
                        }
                        else if (milliseconds != 0 && (node.Parent.NodeValue != "TO_CHAR" && node.Parent.Children.Count != 5) &&
                            node.Parent.Parent.NodeValue != "TO_CHAR" &&
                            !targetTree.IsNodeFromCase(root, node) && node.Parent.Parent.Parent.NodeType == Tokens.SearchCond)
                        {
                            if (node.Parent.Parent.Parent.NodeType != Tokens.SearchCond)
                            {
                                node.NodeValue = "TO_TIMESTAMP(" + node.NodeValue + ", " + "'YYYY-MM-DD HH24:MI:SS.FF" + milliseconds + "')";
                            }
                            else
                            {
                                TreeNode comparisonNode = null;
                                foreach (TreeNode tmpNode in node.Parent.Parent.Children)
                                {
                                    if (tmpNode.NodeType != Tokens.MathOperator && tmpNode.NodeType != Tokens.RelatOperator && tmpNode != node.Parent)
                                    {
                                        comparisonNode = tmpNode;
                                    }
                                }
                                if (comparisonNode != null)
                                {
                                    string format = "'YYYY-MM-DD HH24:MI:SS";

                                    if (milliseconds != 0)
                                    {
                                        format += ".FF" + milliseconds;
                                    }

                                    format += "'";

                                    if (comparisonNode.ToString().ToUpper().Trim() == "SYSDATETIME")
                                    {
                                        if (isTimestamp)
                                        {
                                            if (comparisonNode.Children.Count == 1)
                                            {
                                                comparisonNode.Children[0].NodeValue = "TO_TIMESTAMP(TO_CHAR(SYSTIMESTAMP, " + format + "), 'YYYY-MM-DD HH24:MI:SS.FF6')";
                                                comparisonNode.Children[0].NodeInfo = "SKIP";
                                            }
                                            else if (comparisonNode.Children.Count == 0)
                                            {
                                                comparisonNode.NodeValue = "TO_TIMESTAMP(TO_CHAR(SYSTIMESTAMP, " + format + "), 'YYYY-MM-DD HH24:MI:SS.FF6')";
                                                comparisonNode.NodeInfo = "SKIP";
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (comparisonNode.Children.Count == 1)
                                        {
                                            comparisonNode.Children[0].NodeValue = "TO_TIMESTAMP(TO_CHAR(TO_TIMESTAMP(" + comparisonNode.Children[0].NodeValue + ", 'YYYY-MM-DD HH24:MI:SS.FF6'), " + format + "), 'YYYY-MM-DD HH24:MI:SS.FF6')";
                                        }
                                        else if (comparisonNode.Children.Count > 1)
                                        {
                                            format = "'YYYY-MM-DD HH24:MI:SS.FF" + milliseconds + "'";
                                            comparisonNode.Children.Insert(new TreeNode(Tokens.Function, "TO_TIMESTAMP", "SKIP"), 0);
                                            comparisonNode.Children.Insert(new TreeNode(Tokens.LeftParant, "(", "SKIP"), 1);
                                            comparisonNode.Children.Add(new TreeNode(Tokens.Comma, ","));
                                            comparisonNode.Children.Add(new TreeNode(Tokens.StringConst, format));
                                            comparisonNode.Children.Add(new TreeNode(Tokens.RightParant, ")", "SKIP"));
                                        }
                                    }
                                }
                            }
                        }
                        else if (milliseconds == 0 && isTimestamp && (node.Parent.NodeValue != "TO_CHAR" && node.Parent.Children.Count != 5) &&
                           node.Parent.Parent.NodeValue != "TO_CHAR" &&
                           !targetTree.IsNodeFromCase(root, node) && node.Parent.Parent.Parent.NodeType == Tokens.SearchCond &&
                           (targetTree.PreviousNode(node, Tokens.RelatOperator) != null || targetTree.NextNode(node, Tokens.RelatOperator) != null ||
                            targetTree.PreviousNode(node.Parent, Tokens.RelatOperator) != null || targetTree.NextNode(node.Parent, Tokens.RelatOperator) != null))
                        {
                            TreeNode comparisonNode = null;
                            foreach (TreeNode tmpNode in node.Parent.Parent.Children)
                            {
                                if (tmpNode.NodeType != Tokens.MathOperator && tmpNode.NodeType != Tokens.RelatOperator && tmpNode != node.Parent)
                                {
                                    comparisonNode = tmpNode;
                                }
                            }
                            CustomDateTime comparisonNodeDateTime = new CustomDateTime();

                            if (comparisonNode != null && !IsValidDateTime(comparisonNode.ToString(), out comparisonNodeDateTime))
                            {
                                string format = "'YYYY-MM-DD HH24:MI:SS";

                                if (milliseconds != 0)
                                {
                                    format += ".FF" + milliseconds;
                                }

                                format += "'";

                                if (comparisonNode.ToString().ToUpper().Trim() == "SYSDATETIME")
                                {
                                    if (isTimestamp)
                                    {
                                        if (comparisonNode.Children.Count == 1)
                                        {
                                            comparisonNode.Children[0].NodeValue = "TO_TIMESTAMP(TO_CHAR(SYSTIMESTAMP, " + format + "), 'YYYY-MM-DD HH24:MI:SS.FF6')";
                                            comparisonNode.Children[0].NodeInfo = "SKIP";
                                        }
                                        else if (comparisonNode.Children.Count == 0)
                                        {
                                            comparisonNode.NodeValue = "TO_TIMESTAMP(TO_CHAR(SYSTIMESTAMP, " + format + "), 'YYYY-MM-DD HH24:MI:SS.FF6')";
                                            comparisonNode.NodeInfo = "SKIP";
                                        }
                                    }
                                }
                                else
                                {
                                    if (comparisonNode.Children.Count == 1)
                                    {
                                        if (comparisonNode.Children[0].NodeType == Tokens.StringConst || comparisonNode.Children[0].NodeType == Tokens.DatetimeConst)
                                        {
                                            comparisonNode.Children[0].NodeValue = "TO_TIMESTAMP(TO_CHAR(TO_TIMESTAMP(" + comparisonNode.Children[0].NodeValue + ", 'YYYY-MM-DD HH24:MI:SS.FF6'), " + format + "), 'YYYY-MM-DD HH24:MI:SS.FF6')";
                                        }
                                        else
                                        {
                                            if (comparisonNode.Children[0].NodeType != Tokens.Function)
                                            {
                                                comparisonNode.Children[0].NodeValue = "TO_TIMESTAMP(" + comparisonNode.Children[0].NodeValue + ", " + format + ")";
                                            }
                                            else
                                            {
                                                comparisonNode.Children.Insert(new TreeNode(Tokens.Function, "TO_TIMESTAMP", "SKIP"), 0);
                                                comparisonNode.Children.Insert(new TreeNode(Tokens.LeftParant, "(", "SKIP"), 1);
                                                comparisonNode.Children.Add(new TreeNode(Tokens.Comma, ","));
                                                comparisonNode.Children.Add(new TreeNode(Tokens.StringConst, format));
                                                comparisonNode.Children.Add(new TreeNode(Tokens.RightParant, ")", "SKIP"));
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (milliseconds == 0)
                                        {
                                            format = "'YYYY-MM-DD HH24:MI:SS'";
                                        }
                                        else
                                        {
                                            format = "'YYYY-MM-DD HH24:MI:SS.FF" + milliseconds + "'";
                                        }
                                        if (comparisonNode.Children.Count != 0 && comparisonNode.NodeType != Tokens.Select && comparisonNode.NodeType != Tokens.Insert && comparisonNode.NodeType != Tokens.Update && comparisonNode.NodeType != Tokens.Delete)
                                        {
                                            comparisonNode.Children.Insert(new TreeNode(Tokens.Function, "TO_TIMESTAMP", "SKIP"), 0);
                                            comparisonNode.Children.Insert(new TreeNode(Tokens.LeftParant, "(", "SKIP"), 1);
                                            comparisonNode.Children.Add(new TreeNode(Tokens.Comma, ","));
                                            comparisonNode.Children.Add(new TreeNode(Tokens.StringConst, format));
                                            comparisonNode.Children.Add(new TreeNode(Tokens.RightParant, ")", "SKIP"));
                                        }
                                    }
                                }
                            }
                            else if (IsValidDateTime(comparisonNode.NodeValue, out comparisonNodeDateTime))
                            {
                                node.NodeValue = comparisonNodeDateTime.ToOracleString();
                            }
                        }
                        else if (isTimestamp && (node.Parent.NodeValue != "TO_CHAR" && node.Parent.Children.Count != 5) &&
                           node.Parent.Parent.NodeValue != "TO_CHAR" &&
                           targetTree.IsNodeFromCase(root, node))
                        {
                            if (node.NodeType == Tokens.Column)
                            {
                                string format = "'YYYY-MM-DD HH24:MI:SS";
                                if (milliseconds > 0)
                                {
                                    format += ":FF" + milliseconds;
                                }
                                format += "'";
                                if (root.NodeType == Tokens.Select || root.Children[0].NodeType == Tokens.Select)
                                {
                                    node.NodeValue = String.Format("TO_CHAR(TO_TIMESTAMP(" + node.NodeValue + ", {0}), {0})", format);
                                }
                                else
                                {
                                    node.NodeValue = String.Format("TO_TIMESTAMP(" + node.NodeValue + ", {0})", format);
                                }
                            }
                        }
                        else if (!targetTree.IsNodeFromCase(root, node))
                        {
                            if (node.Parent.Parent.NodeValue == "TO_CHAR")
                            {
                                string format = "'YYYY-MM-DD HH24:MI:SS";
                                if (milliseconds > 0)
                                {
                                    format += ":FF" + milliseconds;
                                }
                                format += "'";

                                node.NodeValue = String.Format("TO_TIMESTAMP(" + node.NodeValue + ", {0})", format);
                            }
                        }
                    }
                }
                else if (isNumeric)
                {
                    //create the format
                    string format = "";
                    if (dataPrecision != System.DBNull.Value && dataScale != System.DBNull.Value)
                    {
                        format += "'";
                        int integers = Convert.ToInt32(dataPrecision) - Convert.ToInt32(dataScale);
                        int decimalPlaces = 0;
                        if (integers < 0)
                        {
                            format = "9";
                        }
                        else
                        {
                            for (int i = 0; i < integers; i++)
                            {
                                format += "9";
                            }
                        }

                        //calculate decimal places
                        decimalPlaces = Convert.ToInt32(dataScale);

                        if (decimalPlaces > 0)
                        {
                            format += ".";
                        }

                        for (int i = 0; i < decimalPlaces; i++)
                        {
                            format += "9";
                        }
                        format += "'";
                    }
                    if (format == "''")
                    {
                        format = "";
                    }
                    //node.Children[0].NodeValue = "TO_NUMBER(" + node.Children[0].NodeValue + ")";
                    if (root.Children[0].NodeType == Tokens.Insert)
                    {
                        TreeNodeCollection cols = new TreeNodeCollection();
                        targetTree.FindAll(root.Children[0].Children[0], Tokens.Column, ref cols);

                        int colIndex = cols.IndexOf(node);
                        TreeNode valuesNode = targetTree.FindNode(root, "VALUES");
                        TreeNodeCollection valuesNodeCollection = new TreeNodeCollection();
                        int adjustment = 0;
                        if (valuesNode == null)
                        {
                            valuesNode = targetTree.FindNode(root, "SELECT");
                            if (valuesNode.Children[0].NodeValue == "DISTINCT")
                            {
                                adjustment = 1;
                            }
                        }

                        targetTree.FindAll(valuesNode, Tokens.Expression, ref valuesNodeCollection);
                        int index = 0, counter = 0;
                        if (valuesNodeCollection.Count > 0)
                        {
                            foreach (TreeNode n in valuesNode.Children)
                            {
                                if (n.NodeType != Tokens.Comma
                                    && n.NodeType != Tokens.RightParant && n.NodeType != Tokens.LeftParant)
                                {
                                    counter++;
                                    if (n.NodeType != Tokens.Expression)
                                    {
                                        index++;
                                    }
                                }

                                if (counter == colIndex)
                                {
                                    break;
                                }
                            }
                            index -= adjustment;

                            TreeNodeCollection valuesNodes = new TreeNodeCollection();
                            foreach (TreeNode n in valuesNode.Children)
                            {
                                if (n.NodeType != Tokens.Comma
                                    && n.NodeType != Tokens.RightParant && n.NodeType != Tokens.LeftParant && n.NodeType != Tokens.Keyword)
                                {
                                    valuesNodes.Add(n);
                                }
                            }

                            TreeNode intos = valuesNodes[colIndex];
                            if (intos.Children.Count == 0)
                            {
                                intos.NodeValue = "TO_NUMBER(TO_CHAR(" + intos.NodeValue;
                                if (!string.IsNullOrEmpty(format))
                                {
                                    intos.NodeValue += "," + format;
                                }
                                intos.NodeValue += ")";
                                if (!string.IsNullOrEmpty(format))
                                {
                                    intos.NodeValue += "," + format;
                                }
                                intos.NodeValue += ")";
                            }
                            else
                            {
                                intos.Children.Insert(new TreeNode(Tokens.Function, "TO_NUMBER", "SKIP"), 0);
                                intos.Children.Insert(new TreeNode(Tokens.LeftParant, "(", "SKIP"), 1);
                                intos.Children.Insert(new TreeNode(Tokens.Function, "TO_CHAR", "SKIP"), 2);
                                intos.Children.Insert(new TreeNode(Tokens.LeftParant, "(", "SKIP"), 3);
                                if (!string.IsNullOrEmpty(format))
                                {
                                    intos.Children.Add(new TreeNode(Tokens.Comma, ","));
                                    intos.Children.Add(new TreeNode(Tokens.StringConst, format));
                                }
                                intos.Children.Add(new TreeNode(Tokens.RightParant, ")", "SKIP"));
                                if (!string.IsNullOrEmpty(format))
                                {
                                    intos.Children.Add(new TreeNode(Tokens.Comma, ","));
                                    intos.Children.Add(new TreeNode(Tokens.StringConst, format));
                                }
                                intos.Children.Add(new TreeNode(Tokens.RightParant, ")", "SKIP"));
                            }
                        }
                        else
                        {
                            targetTree.FindAll(valuesNode, Tokens.BindVariable, ref valuesNodeCollection);
                            int valuesCoutner = 0;
                            foreach (TreeNode n in valuesNode.Children)
                            {
                                if (n.NodeType != Tokens.Comma
                                    && n.NodeType != Tokens.RightParant && n.NodeType != Tokens.LeftParant)
                                {
                                    if (counter == colIndex)
                                    {
                                        break;
                                    }
                                    counter++;
                                }

                                valuesCoutner++;
                            }
                            TreeNode intos = valuesNode.Children[valuesCoutner];
                            intos.NodeValue = "TO_NUMBER(TO_CHAR(" + intos.NodeValue + (!string.IsNullOrEmpty(format) ? ", " + format + ")" : ")")
                                + (!string.IsNullOrEmpty(format) ? ", " + format + ")" : ")");
                        }

                    }
                    else if (root.Children[0].NodeType == Tokens.Update)
                    {
                        TreeNode valueNode = null;
                        int colIndex = node.Parent.Children.IndexOf(node);
                        if (colIndex + 2 > node.Parent.Children.Count && colIndex + 2 < node.Parent.Parent.Children.Count)
                        {
                            valueNode = node.Parent.Parent.Children[colIndex + 2];
                        }
                        else if (colIndex + 2 < node.Parent.Children.Count)
                        {
                            valueNode = node.Parent.Children[colIndex + 2];
                        }
                        if (valueNode != null)
                        {
                            TreeNode binvarNode = targetTree.FindNode(valueNode, Tokens.BindVariable);
                            if (binvarNode != null)
                            {
                                valueNode.Children.Insert(new TreeNode(Tokens.Function, "TO_NUMBER", "SKIP"), 0);
                                valueNode.Children.Insert(new TreeNode(Tokens.LeftParant, "(", "SKIP"), 1);
                                valueNode.Children.Insert(new TreeNode(Tokens.Function, "TO_CHAR", "SKIP"), 2);
                                valueNode.Children.Insert(new TreeNode(Tokens.LeftParant, "(", "SKIP"), 3);
                                if (!string.IsNullOrEmpty(format))
                                {
                                    valueNode.Children.Add(new TreeNode(Tokens.Comma, ","));
                                    valueNode.Children.Add(new TreeNode(Tokens.StringConst, format));
                                }
                                valueNode.Children.Add(new TreeNode(Tokens.RightParant, ")", "SKIP"));
                                if (!string.IsNullOrEmpty(format))
                                {
                                    valueNode.Children.Add(new TreeNode(Tokens.Comma, ","));
                                    valueNode.Children.Add(new TreeNode(Tokens.StringConst, format));
                                }
                                valueNode.Children.Add(new TreeNode(Tokens.RightParant, ")", "SKIP"));
                            }
                        }
                    }
                }

                ConvertWithFormat(targetTree, root, node);
            }
        }
        //BAN - Used for NVL translation
        //NVL - both expressions should be retuned with same cast and format.
        // The first expression dictates the cast and format
        private void ConvertWithFormat(Tree targetTree, TreeNode root, TreeNode node)
        {
            TreeNode topmostNode = targetTree.FindTopmostExpression(node);
            if (topmostNode != null && (topmostNode.NodeValue == "NVL" || topmostNode.Children[0].NodeValue == "NVL" || topmostNode.ToString().StartsWith("NVL")))
            {
                TreeNode firstNode = targetTree.FindNode(topmostNode, Tokens.LeftParant, "(");
                if (firstNode != null)
                {
                    firstNode = firstNode.Parent;
                    firstNode = firstNode.Children[1];
                    if (node != firstNode && firstNode.Children[0] != node)
                    {
                        string nodeCast = "";
                        string nodeFormat = "";
                        string firstNodeCast = "";
                        string firstNodeFormat = "";
                        string nodeValue = node.ToString();
                        string firstNodeValue = firstNode.ToString();
                        if (nodeValue.Split('(').Length > 1 && nodeValue.Split(',').Length > 1)
                        {
                            nodeCast = nodeValue.Split('(')[0];
                            nodeFormat = nodeValue.Split(',')[1].Replace(")", "").Trim();
                        }
                        if (firstNodeValue.Split('(').Length > 1 && firstNodeValue.Split(',').Length > 1)
                        {
                            firstNodeCast = firstNodeValue.Split('(')[0];
                            firstNodeFormat = firstNodeValue.Split(',')[1].Replace(")", "").Trim();
                        }
                        if ((nodeCast != firstNodeCast || nodeFormat != firstNodeFormat) && !string.IsNullOrEmpty(firstNodeCast) && !string.IsNullOrEmpty(firstNodeFormat))
                        {
                            if (nodeCast == firstNodeCast)
                            {
                                if (node.Children.Count == 0)
                                {
                                    if (firstNodeFormat.Contains(".FF"))
                                    {
                                        node.NodeValue = node.NodeValue.Replace(nodeCast, firstNodeCast + "(TO_TIMESTAMP");
                                        node.NodeValue = node.NodeValue.Replace(nodeFormat, firstNodeFormat + ")" + ", " + firstNodeFormat);
                                    }
                                    else
                                    {
                                        node.NodeValue = node.NodeValue.Replace(nodeFormat, firstNodeFormat);
                                    }
                                }
                                else
                                {
                                    TreeNode formatNode = targetTree.FindNode(node, nodeFormat);
                                    if (formatNode != null)
                                    {
                                        if (formatNode.Children.Count == 0)
                                        {
                                            formatNode.NodeValue = firstNodeFormat;
                                        }
                                        else if (formatNode.Children[0].Children.Count == 0)
                                        {
                                            formatNode.Children[0].NodeValue = firstNodeFormat;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (node.Children.Count == 0)
                                {
                                    if (firstNodeFormat.Contains(".FF"))
                                    {
                                        node.NodeValue = firstNodeCast + "(TO_TIMESTAMP(" + node.NodeValue + ", " + firstNodeFormat + ")" + ", " + firstNodeFormat + ")";
                                    }
                                    else
                                    {
                                        node.NodeValue = firstNodeCast + "(" + node.NodeValue + ", " + firstNodeFormat + ")";
                                    }
                                }
                                else
                                {
                                    node.Children.Insert(new TreeNode(Tokens.Function, firstNodeCast, "SKIP"), 0);
                                    node.Children.Insert(new TreeNode(Tokens.LeftParant, "(", "SKIP"), 1);
                                    if (firstNodeFormat.Contains(".FF"))
                                    {
                                        node.Children.Insert(new TreeNode(Tokens.Function, "TO_TIMESTAMP", "SKIP"), 0);
                                        node.Children.Insert(new TreeNode(Tokens.LeftParant, "(", "SKIP"), 1);
                                    }
                                    node.Children.Add(new TreeNode(Tokens.Comma, ",", "SKIP"));
                                    node.Children.Add(new TreeNode(Tokens.StringConst, firstNodeFormat, "SKIP"));
                                    node.Children.Add(new TreeNode(Tokens.RightParant, ")", "SKIP"));
                                    if (firstNodeFormat.Contains(".FF"))
                                    {
                                        node.Children.Add(new TreeNode(Tokens.StringConst, firstNodeFormat, "SKIP"));
                                        node.Children.Add(new TreeNode(Tokens.RightParant, ")", "SKIP"));
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void CastNodeWithFormat(TreeNode node, List<TreeNode> nodesList)
        {
            TreeNode firstNodeWithoutDateToChar = null;
            foreach (TreeNode n in nodesList)
            {
                if (n.OriginalNodeValue.ToUpper() != "@DATETOCHAR" || (n.Children.Count > 0 && n.Children[0].OriginalNodeValue != "@DATETOCHAR"))
                {
                    if (n.ToString().StartsWith("TO_CHAR"))
                    {
                        firstNodeWithoutDateToChar = n;
                        break;
                    }
                }
            }
            if (firstNodeWithoutDateToChar != null && (firstNodeWithoutDateToChar == node || firstNodeWithoutDateToChar.Children[0] == node))
            {
                int nodeIndex = nodesList.IndexOf(node);
                if (nodeIndex == -1)
                {
                    if (node.Children.Count > 0)
                    {
                        nodeIndex = nodesList.IndexOf(node.Children[0]);
                    }
                    if (nodeIndex == -1)
                    {
                        nodeIndex = nodesList.IndexOf(node.Parent);
                    }
                }
                for (int count = 0; count < nodeIndex; count++)
                {
                    TreeNode previusNode = nodesList[count];
                    string expressionCast = "";
                    string expressionFormat = "";
                    string nodeCast = "";
                    string nodeFormat = "";
                    string nodeValue = previusNode.ToString();
                    string expressionValue = node.ToString();
                    if (nodeValue.Split('(').Length > 1 && nodeValue.Split(',').Length > 1)
                    {
                        nodeCast = nodeValue.Split('(')[0];
                        nodeFormat = nodeValue.Split(',')[1].Replace(")", "").Trim();
                    }
                    if (expressionValue.Split('(').Length > 1 && expressionValue.Split(',').Length > 1)
                    {
                        expressionCast = expressionValue.Split('(')[0];
                        expressionFormat = expressionValue.Split(',')[1].Replace(")", "").Trim();
                    }
                    if (nodeCast != expressionCast || nodeFormat != expressionFormat)
                    {
                        if (previusNode.Children.Count == 1)
                        {
                            previusNode = previusNode.Children[0];
                        }
                        if (previusNode.Children.Count == 0)
                        {
                            previusNode.NodeValue = expressionCast + "(" + previusNode.NodeValue + ", " + expressionFormat + ")";
                        }
                        else
                        {
                            previusNode.Children.Insert(new TreeNode(Tokens.Function, expressionCast, "SKIP"), 0);
                            previusNode.Children.Insert(new TreeNode(Tokens.LeftParant, "(", "SKIP"), 1);
                            previusNode.Children.Add(new TreeNode(Tokens.Comma, ",", "SKIP"));
                            previusNode.Children.Add(new TreeNode(Tokens.StringConst, expressionFormat, "SKIP"));
                            previusNode.Children.Add(new TreeNode(Tokens.RightParant, ")", "SKIP"));
                        }
                    }
                }
            }
            else if (firstNodeWithoutDateToChar != null && (firstNodeWithoutDateToChar != node && firstNodeWithoutDateToChar.Children[0] != node))
            {
                string nodeCast = "";
                string nodeFormat = "";
                string nodeValue = firstNodeWithoutDateToChar.ToString();
                if (nodeValue.Split('(').Length > 1 && nodeValue.Split(',').Length > 1)
                {
                    nodeCast = nodeValue.Split('(')[0];
                    nodeFormat = nodeValue.Split(',')[1].Replace(")", "").Trim();
                }
                if (node.Children.Count == 0)
                {
                    node.NodeValue = nodeCast + "(" + node.NodeValue + ", " + nodeFormat + ")";
                }
                else
                {
                    node.Children.Insert(new TreeNode(Tokens.Function, nodeCast, "SKIP"), 0);
                    node.Children.Insert(new TreeNode(Tokens.LeftParant, "(", "SKIP"), 1);
                    node.Children.Add(new TreeNode(Tokens.Comma, ",", "SKIP"));
                    node.Children.Add(new TreeNode(Tokens.StringConst, nodeFormat, "SKIP"));
                    node.Children.Add(new TreeNode(Tokens.RightParant, ")", "SKIP"));
                }
            }
        }

        private void GetCastAndFormatFromNode(TreeNode node, ref string cast, ref string format)
        {
            string nodeCast = "";
            string nodeFormat = "";
            string nodeValue = node.ToString();
            if (nodeValue.StartsWith("TO_"))
            {
                if (nodeValue.Split('(').Length > 1 && nodeValue.Split(',').Length > 1)
                {
                    nodeCast = nodeValue.Split('(')[0];
                    string[] splitNodeValue = nodeValue.Split(',');
                    nodeFormat = splitNodeValue[splitNodeValue.Length - 1].Replace(")", "").Trim();
                }
            }
            else if (nodeValue.StartsWith("@DATETOCHAR"))
            {
                if (nodeValue.Split('(').Length > 1 && nodeValue.Split(',').Length > 1)
                {
                    nodeCast = "TO_CHAR";
                    string[] splitNodeValue = nodeValue.Split(',');
                    nodeFormat = splitNodeValue[splitNodeValue.Length - 1].Replace(")", "").Trim();
                }
            }
            cast = nodeCast.Trim();
            format = nodeFormat.Trim();
        }

        private string ReplaceNodeFormat(string nodeValue, string oldFormat, string newFormat)
        {
            string[] formats = nodeValue.Split(',');
            formats[formats.Length - 1] = formats[formats.Length - 1].Replace(oldFormat, newFormat);
            string retNodeValue = "";
            foreach (string str in formats)
            {
                retNodeValue += str + ",";
            }
            retNodeValue = retNodeValue.Remove(retNodeValue.Length - 1);

            return retNodeValue;
        }

        private void CastToTimestamp(Tree targetTree, TreeNode root, TreeNode node, string fmt, int millisec)
        {
            string format = "";
            //create the format
            if (String.IsNullOrEmpty(fmt))
            {
                format = "'YYYY-MM-DD HH24:MI:SS'"; ;
            }
            else
            {
                format = fmt;
            }

            string timestampFormat = "'YYYY-MM-DD HH24:MI:SS.FF6'";

            //node.Children[0].NodeValue = "TO_NUMBER(" + node.Children[0].NodeValue + ")";
            if (root.Children[0].NodeType == Tokens.Insert)
            {
                //The adjustmen accounts for the table name node and the left parathesis node
                int adjustment = 1;
                TreeNodeCollection cols = new TreeNodeCollection();

                TreeNode valuesNode = targetTree.FindNode(root, "VALUES");
                TreeNodeCollection valuesNodeCollection = new TreeNodeCollection();

                if (valuesNode == null)
                {
                    adjustment = 2;
                    valuesNode = targetTree.FindNode(root, "SELECT");
                    if (valuesNode.Children[0].NodeValue == "DISTINCT")
                    {
                        adjustment--;
                    }
                }

                int colIndex = root.Children[0].Children[0].Children.IndexOf(node) - adjustment;
                bool isDate = false;
                bool isTime = false;
                bool isTimestamp = false;
                int milliseconds = 0;

                TreeNode intos = valuesNode.Children[colIndex];
                if (intos.NodeType == Tokens.Expression && intos.Children.Count > 0 && intos.Children[0].NodeType == Tokens.Column)
                {
                    this.GetColumnDateTimeProperties(GetColumnName(intos.Children[0].NodeValue), out isDate, out isTime, out isTimestamp, out milliseconds);
                }

                if (!isDate && intos.NodeValue != "SYSDATETIME" &&
                    !(intos.ToString().StartsWith("TO_CHAR") || intos.ToString().StartsWith("TO_DATE") || intos.ToString().StartsWith("@DATE")))
                {
                    if (intos.NodeType == Tokens.Expression)
                    {
                        if (millisec == 0)
                        {
                            intos.Children.Insert(new TreeNode(Tokens.Function, "TO_CHAR", "SKIP"), 0);
                            intos.Children.Insert(new TreeNode(Tokens.LeftParant, "(", "SKIP"), 1);
                            intos.Children.Insert(new TreeNode(Tokens.Function, "TO_TIMESTAMP", "SKIP"), 2);
                            intos.Children.Insert(new TreeNode(Tokens.LeftParant, "(", "SKIP"), 3);
                            intos.Children.Add(new TreeNode(Tokens.Comma, ","));
                            intos.Children.Add(new TreeNode(Tokens.StringConst, timestampFormat));
                            intos.Children.Add(new TreeNode(Tokens.RightParant, ")", "SKIP"));
                            if (!string.IsNullOrEmpty(format))
                            {
                                intos.Children.Add(new TreeNode(Tokens.Comma, ","));
                                intos.Children.Add(new TreeNode(Tokens.StringConst, format));
                            }
                            intos.Children.Add(new TreeNode(Tokens.RightParant, ")", "SKIP"));
                        }
                        else
                        {
                            format = "'YYYY-MM-DD HH24:MI:SS.FF" + millisec + "'";
                            intos.Children.Insert(new TreeNode(Tokens.Function, "TO_TIMESTAMP", "SKIP"), 0);
                            intos.Children.Insert(new TreeNode(Tokens.LeftParant, "(", "SKIP"), 1);
                            intos.Children.Add(new TreeNode(Tokens.Comma, ","));
                            intos.Children.Add(new TreeNode(Tokens.StringConst, format));
                            intos.Children.Add(new TreeNode(Tokens.RightParant, ")", "SKIP"));
                        }
                    }
                    else
                    {
                        this.GetColumnDateTimeProperties(GetColumnName(node.NodeValue), out isDate, out isTime, out isTimestamp, out milliseconds);
                        if (intos.NodeType == Tokens.DatetimeConst)
                        {
                            intos.NodeValue = "'" + intos.NodeValue + "'";
                            intos.NodeInfo = "SKIP";
                        }
                        if (!isTimestamp)
                        {
                            intos.NodeValue = "TO_CHAR(TO_TIMESTAMP(" + intos.NodeValue + ", " + timestampFormat + ")" + " , " + format + ")";
                        }
                        else if (isTimestamp)
                        {
                            if (millisec == 0)
                            {
                                format = "'YYYY-MM-DD HH24:MI:SS'";
                            }
                            else
                            {
                                format = "'YYYY-MM-DD HH24:MI:SS.FF" + millisec + "'";
                            }
                            intos.NodeValue = "TO_TIMESTAMP(" + intos.NodeValue + ", " + timestampFormat + ")";
                        }
                    }
                }
                else if (intos.NodeValue == "SYSDATETIME")
                {
                    this.GetColumnDateTimeProperties(GetColumnName(node.NodeValue), out isDate, out isTime, out isTimestamp, out milliseconds);
                    format = "'YYYY-MM-DD HH24:MI:SS";

                    if (millisec != 0)
                    {
                        format += ".FF" + millisec;
                    }

                    format += "'";
                    if (isTimestamp)
                    {
                        intos.NodeValue = "TO_TIMESTAMP(TO_CHAR(SYSTIMESTAMP, " + format + "), " + format + ")";
                        intos.NodeInfo = "SKIP";
                    }
                }
            }
            else if (root.Children[0].NodeType == Tokens.Update)
            {
                bool isDate = false;
                bool isTime = false;
                bool isTimestamp = false;
                int milliseconds = 0;
                int colIndex = root.Children[0].Children[1].Children.IndexOf(node);
                CustomDateTime dateTime = new CustomDateTime();
                TreeNode valueNode = root.Children[0].Children[1].Children[colIndex + 2];
                TreeNode binvarNode = targetTree.FindNode(valueNode, Tokens.BindVariable);
                this.GetColumnDateTimeProperties(GetColumnName(node.NodeValue), out isDate, out isTime, out isTimestamp, out milliseconds);
                if (binvarNode != null)
                {
                    if (!isTimestamp)
                    {
                        if (isDate)
                        {
                            valueNode.Children.Insert(new TreeNode(Tokens.Function, "TO_DATE", "SKIP"), 0);
                        }
                        else
                        {
                            valueNode.Children.Insert(new TreeNode(Tokens.Function, "TO_CHAR", "SKIP"), 0);
                        }
                        valueNode.Children.Insert(new TreeNode(Tokens.LeftParant, "(", "SKIP"), 1);
                        if (isDate)
                        {
                            valueNode.Children.Insert(new TreeNode(Tokens.Function, "TO_CHAR", "SKIP"), 2);
                            valueNode.Children.Insert(new TreeNode(Tokens.LeftParant, "(", "SKIP"), 3);
                            valueNode.Children.Insert(new TreeNode(Tokens.Function, "TO_TIMESTAMP", "SKIP"), 4);
                            valueNode.Children.Insert(new TreeNode(Tokens.LeftParant, "(", "SKIP"), 5);
                        }
                        else
                        {
                            valueNode.Children.Insert(new TreeNode(Tokens.Function, "TO_TIMESTAMP", "SKIP"), 2);
                            valueNode.Children.Insert(new TreeNode(Tokens.LeftParant, "(", "SKIP"), 3);
                        }
                        valueNode.Children.Add(new TreeNode(Tokens.Comma, ","));
                        valueNode.Children.Add(new TreeNode(Tokens.StringConst, timestampFormat));
                        valueNode.Children.Add(new TreeNode(Tokens.RightParant, ")", "SKIP"));
                        if (isDate)
                        {
                            valueNode.Children.Add(new TreeNode(Tokens.Comma, ","));
                            valueNode.Children.Add(new TreeNode(Tokens.StringConst, format));
                            valueNode.Children.Add(new TreeNode(Tokens.RightParant, ")", "SKIP"));
                        }
                        if (!string.IsNullOrEmpty(format))
                        {
                            valueNode.Children.Add(new TreeNode(Tokens.Comma, ","));
                            valueNode.Children.Add(new TreeNode(Tokens.StringConst, format));
                        }
                        valueNode.Children.Add(new TreeNode(Tokens.RightParant, ")", "SKIP"));
                    }
                    else if (isTimestamp)
                    {
                        if (millisec != 0)
                        {
                            format = "'YYYY-MM-DD HH24:MI:SS.FF" + millisec + "'";
                        }
                        else
                        {
                            format = "'YYYY-MM-DD HH24:MI:SS'";
                        }

                        if (millisec == 0)
                        {
                            valueNode.Children.Insert(new TreeNode(Tokens.Function, "TO_CHAR", "SKIP"), 0);
                            valueNode.Children.Insert(new TreeNode(Tokens.LeftParant, "(", "SKIP"), 1);
                            valueNode.Children.Insert(new TreeNode(Tokens.Function, "TO_TIMESTAMP", "SKIP"), 2);
                            valueNode.Children.Insert(new TreeNode(Tokens.LeftParant, "(", "SKIP"), 3);
                        }
                        else
                        {
                            valueNode.Children.Insert(new TreeNode(Tokens.Function, "TO_TIMESTAMP", "SKIP"), 0);
                            valueNode.Children.Insert(new TreeNode(Tokens.LeftParant, "(", "SKIP"), 1);
                        }
                        if (millisec == 0)
                        {
                            valueNode.Children.Add(new TreeNode(Tokens.Comma, ","));
                            valueNode.Children.Add(new TreeNode(Tokens.StringConst, timestampFormat));
                            valueNode.Children.Add(new TreeNode(Tokens.RightParant, ")", "SKIP"));
                        }
                        valueNode.Children.Add(new TreeNode(Tokens.Comma, ","));
                        valueNode.Children.Add(new TreeNode(Tokens.StringConst, format));
                        valueNode.Children.Add(new TreeNode(Tokens.RightParant, ")", "SKIP"));
                    }
                }
                else if (valueNode.Children.Count == 1 && (valueNode.Children[0].NodeType == Tokens.StringConst || valueNode.Children[0].NodeType == Tokens.DatetimeConst))
                {
                    format = "'YYYY-MM-DD HH24:MI:SS";

                    if (millisec != 0)
                    {
                        format += ".FF" + millisec;
                    }

                    format += "'";
                    CustomDateTime comparisonNodeDateTime = new CustomDateTime();
                    if (IsValidDateTime(valueNode.Children[0].NodeValue, out comparisonNodeDateTime))
                    {
                        valueNode.Children[0].NodeValue = comparisonNodeDateTime.ToOracleString();
                    }
                    valueNode.Children[0].NodeValue = "TO_TIMESTAMP(TO_CHAR(TO_TIMESTAMP(" + valueNode.Children[0].NodeValue + ", 'YYYY-MM-DD HH24:MI:SS.FF6'), " + format + "), 'YYYY-MM-DD HH24:MI:SS.FF6')";
                }
                else if (valueNode.Children.Count == 1 && valueNode.Children[0].NodeType == Tokens.SysKeyword && valueNode.Children[0].NodeValue == "SYSDATETIME")
                {
                    format = "'YYYY-MM-DD HH24:MI:SS";

                    if (millisec != 0)
                    {
                        format += ".FF" + millisec;
                    }

                    format += "'";

                    valueNode.Children[0].NodeValue = "TO_TIMESTAMP(TO_CHAR(SYSTIMESTAMP, " + format + "), 'YYYY-MM-DD HH24:MI:SS.FF6')";
                    valueNode.Children[0].NodeInfo = "SKIP";
                }
            }
        }

        private bool IsDateNode(TreeNode node, Tree targetTree, TreeNode root)
        {
            if (node == null)
            {
                return false;
            }

            switch (node.NodeType)
            {
                case Tokens.Expression:
                    if (node.Children.Count == 1)
                    {
                        return IsDateNode(node.Children[0], targetTree, root);
                    }
                    break;

                case Tokens.SysKeyword:
                    if (node.NodeValue == "CURRENT_TIMESTAMP")
                    {
                        return true;
                    }
                    break;

                case Tokens.Function:
                    if (FunctionTranslator.IsDateFunction(DatabaseBrand.SqlServer, node))
                    {
                        return true;
                    }

                    if (FunctionTranslator.IsScalarFunction(DatabaseBrand.SqlServer, node))
                    {
                        return IsDateNode(node.Children[1], targetTree, root);
                    }

                    return false;

                case Tokens.Column:
                    if (Settings.DbStructure != null)
                    {
                        string columnName = node.OriginalNodeValue;
                        int index = node.OriginalNodeValue.IndexOf(".");
                        if (index > 0)
                        {
                            columnName = node.OriginalNodeValue.Substring(index + 1, node.OriginalNodeValue.Length - index - 1);
                        }
                        TreeNodeCollection tables = new TreeNodeCollection();
                        targetTree.FindAll(root, Tokens.Table, ref tables);
                        TreeNode table = GetTable(node, tables);
                        if (table != null)
                        {
                            //FC:RHE:
                            //string datatype = Settings.DbStructure[columnName, table.NodeValue];
                            //var tableInfo = Settings.Schemas.FindTable(table.GetSchema(Settings), table.OriginalNodeValue);
                            //string datatype = Settings.DbStructure[columnName, Settings.Schemas.FindTable(table.GetSchema(Settings), table.OriginalNodeValue).Name].In("date", "datetime", "datetime2");
                            if (Settings.DbStructure[columnName, Settings.Schemas.FindTable(table.GetSchema(Settings), table.OriginalNodeValue)?.Name].In("date", "datetime", "datetime2"))
                            {
                                return true;
                            }
                        }

                        return false;
                    }
                    break;
            }

            return false;
        }

        private void TranslateOuterJoins(Tree targetTree, TreeNode root, TreeNode node)
        {
            //if there are only inner joins in the select then don't do anything
            if (node.NodeInfo == "INNER" &&
                targetTree.FindNode(root, "LEFT", 0) == null &&
                targetTree.FindNode(root, "RIGHT", 0) == null)
            {
                return;
            }

            TreeNodeCollection tables = new TreeNodeCollection();
            TreeNodeCollection predicates = new TreeNodeCollection();
            TreeNodeCollection joins;
            TreeNode exprNode, table1, table2, tempNode;
            JoinsCollection joinsList = new JoinsCollection();
            Dictionary<TreeNode, TreeNodeCollection> conditions = new Dictionary<TreeNode, TreeNodeCollection>();

            TreeNode fromNode = targetTree.FindNode(root, "FROM");
            targetTree.FindAll(fromNode, Tokens.Table, ref tables);

            TreeNode whereNode = targetTree.FindNode(root, "WHERE");
            targetTree.FindAll(whereNode, Tokens.Predicate, ref predicates);

            TreeNode searchCond = whereNode.Children[0];

            //search for all the join predicates
            foreach (TreeNode predicate in predicates)
            {
                exprNode = targetTree.FindNode(predicate, Tokens.Expression);
                exprNode = targetTree.FindNode(exprNode, Tokens.Column);
                table1 = GetTable(exprNode, tables);

                exprNode = targetTree.FindNodeReverse(predicate, Tokens.Expression);
                exprNode = targetTree.FindNode(exprNode, Tokens.Column);
                table2 = GetTable(exprNode, tables);

                if (predicate.NodeInfo.EndsWith("JOIN"))
                {
                    targetTree.FindNode(predicate, Tokens.JoinOperator).NodeInfo = "SKIP";
                    targetTree.FindNode(predicate, Tokens.JoinOperator).NodeValue = "";

                    if (predicate.NodeInfo == "INNER JOIN" &&
                       (((tempNode = targetTree.PreviousNode(predicate, Tokens.Keyword)) != null && tempNode.NodeValue == "OR") ||
                        ((tempNode = targetTree.NextNode(predicate, Tokens.Keyword)) != null && tempNode.NodeValue == "OR")))
                    {
                        //leave inner joins that are part of an OR condition in the where clause
                    }
                    else if (table1 != table2) // if table1 = table2 then it's not a join, it's a simple condition
                    {
                        joins = joinsList[table1, table2];

                        if (joinsList.IsReversed(table1, table2))
                        {
                            if (predicate.NodeInfo == "RIGHT OUTER JOIN")
                            {
                                predicate.NodeInfo = "LEFT OUTER JOIN";
                            }
                            else if (predicate.NodeInfo == "LEFT OUTER JOIN")
                            {
                                predicate.NodeInfo = "RIGHT OUTER JOIN";
                            }
                        }

                        if (joins.Count == 0)
                        {
                            joins.Add(predicate);
                        }
                        else
                        {
                            if (joins.Find("INNER JOIN") != null && predicate.NodeInfo.Contains("OUTER"))
                            {
                                joins.Clear();
                            }
                            if ((joins.Find("LEFT OUTER JOIN") != null || joins.Find("RIGHT OUTER JOIN") != null) && predicate.NodeInfo == "INNER JOIN")
                            {
                                //don't add the inner joins if there is already an outer join in the list
                            }
                            else
                            {
                                joins.Add(predicate);
                            }
                        }
                    }
                }
                else if (table1 != null)
                {
                    //it's a simple condition
                    if (!conditions.ContainsKey(table1))
                    {
                        conditions.Add(table1, new TreeNodeCollection());
                    }
                    //find the entire group of conditions
                    int groupStart = -1, groupEnd = -1;
                    int index = searchCond.Children.IndexOf(predicate);
                    for (int i = index - 1; i >= 0; i--)
                    {
                        if (searchCond.Children[i].NodeInfo == "GROUP START")
                        {
                            groupStart = i;
                            break;
                        }
                    }
                    for (int i = index + 1; i < searchCond.Children.Count; i++)
                    {
                        if (searchCond.Children[i].NodeInfo == "GROUP END")
                        {
                            groupEnd = i;
                            break;
                        }
                    }
                    if (groupStart != -1 && groupEnd != -1)
                    {
                        for (int i = groupStart; i <= groupEnd; i++)
                        {
                            if (!conditions[table1].Contains(searchCond.Children[i]))
                            {
                                conditions[table1].Add(searchCond.Children[i]);
                            }
                        }
                    }
                    else
                    {
                        conditions[table1].Add(predicate);
                    }
                }
            }
            JoinsCollection origJoins = joinsList.Clone();
            joinsList.Sort();

            //reconstruct the FROM clause
            TreeNode newFromNode = new TreeNode(Tokens.Keyword, "FROM");
            TreeNodeCollection tableNodes = new TreeNodeCollection();

            foreach (Joins joinsItem in joinsList)
            {
                int tableCount = tableNodes.Count;
                if (joinsItem.Table1 != null && !tableNodes.Contains(joinsItem.Table1))
                {
                    tableNodes.Add(joinsItem.Table1);
                }
                if (joinsItem.Table2 != null && !tableNodes.Contains(joinsItem.Table2))
                {
                    tableNodes.Add(joinsItem.Table2);
                }

                //for outer joins we have to add all the conditions on the (+) table to the join condition
                //Example
                //select * from A, B where ACOL1 = BCOL1(+) AND BCOL2 IS NULL
                //has to be translated to:
                //select * from A LEFT OUTER JOIN B ON ACOL1 = BCOL1 AND BCOL2 IS NULL
                TreeNode outerJoin = joinsItem.GetOuterJoin();
                if (outerJoin != null)
                {
                    TreeNode outerTable;
                    if (outerJoin.NodeInfo.Contains("LEFT"))
                    {
                        outerTable = joinsItem.Table2;
                    }
                    else
                    {
                        outerTable = joinsItem.Table1;
                    }
                    if (conditions.ContainsKey(outerTable))
                    {
                        for (int i = 0; i < conditions[outerTable].Count; i++)
                        {
                            if (conditions[outerTable][i].NodeInfo == "GROUP START")
                            {
                                int start = i;
                                bool foundNULL = false;
                                do
                                {
                                    i++;
                                    if (targetTree.FindNode(conditions[outerTable][i], "NULL") != null)
                                    {
                                        foundNULL = true;
                                    }
                                } while (conditions[outerTable][i].NodeInfo != "GROUP END");
                                if (foundNULL)
                                {
                                    bool isJoinItem = true;
                                    //found NULL
                                    for (int j = start; j <= i; j++)
                                    {
                                        if (conditions[outerTable][j].NodeType == Tokens.Predicate)
                                        {
                                            TreeNodeCollection cols = new TreeNodeCollection();
                                            targetTree.FindAll(conditions[outerTable][j], Tokens.Column, ref cols);
                                            if (cols.Count > 1)
                                            {
                                                isJoinItem = false;
                                                break;
                                            }
                                        }

                                    }
                                    if (isJoinItem)
                                    {
                                        for (int j = start; j <= i; j++)
                                        {

                                            joinsItem.Items.Add(conditions[outerTable][j]);
                                        }
                                    }
                                }
                            }
                            else if (targetTree.FindNode(conditions[outerTable][i], "NULL") != null)
                            {
                                joinsItem.Items.Add(conditions[outerTable][i]);
                            }

                        }
                    }
                }

                //if there are any new tables, add them to the FORM clause
                if (tableNodes.Count > tableCount)
                {
                    if (tableNodes.Count == tableCount + 2)
                    {
                        newFromNode.AddChild(tableNodes[tableCount + 1]);
                    }

                    for (int i = 0; i < joinsItem.Items.Count; i++)
                    {
                        TreeNode join = joinsItem.Items[i];
                        if (i == 0)
                        {
                            //check if we don't have to reverse the type of the join
                            if (tableNodes[tableCount] != joinsItem.Table2)
                            {
                                if (join.NodeInfo == "RIGHT OUTER JOIN")
                                {
                                    join.NodeInfo = "LEFT OUTER JOIN";
                                }
                                else if (join.NodeInfo == "LEFT OUTER JOIN")
                                {
                                    join.NodeInfo = "RIGHT OUTER JOIN";
                                }
                            }

                            newFromNode.AddChild(new TreeNode(Tokens.JoinSpecif, join.NodeInfo));
                            newFromNode.AddChild(tableNodes[tableCount]);
                            newFromNode.AddChild(new TreeNode(Tokens.Keyword, "ON"));
                            newFromNode.AddChild(new TreeNode(join));
                        }
                        else
                        {
                            if ((newFromNode.Children[newFromNode.Children.Count - 1].NodeType == Tokens.Predicate &&
                                (join.NodeType == Tokens.Predicate || join.NodeInfo == "GROUP START")) ||
                                (newFromNode.Children[newFromNode.Children.Count - 1].NodeType == Tokens.RightParant &&
                                join.NodeType == Tokens.LeftParant))
                            {
                                newFromNode.AddChild(new TreeNode(Tokens.Keyword, "AND"));
                            }
                            newFromNode.AddChild(new TreeNode(join));
                        }
                    }
                }
                else
                {
                    int index = Math.Max(newFromNode.Children.IndexOf(joinsItem.Table1),
                        newFromNode.Children.IndexOf(joinsItem.Table2));

                    //add the condition after the first predicate found after the table
                    if (newFromNode.Children[index + 2].NodeType == Tokens.Predicate)
                    {
                        index += 3;
                    }
                    else
                    {
                        index += 5;
                    }
                    int leftPar = 0;
                    for (int i = 0; i < joinsItem.Items.Count; i++)
                    {
                        TreeNode join = joinsItem.Items[i];
                        if (join.NodeValue == "(")
                        {
                            leftPar++;
                        }

                        if (leftPar == 0)
                        {
                            newFromNode.InsertChild(new TreeNode(Tokens.Keyword, "AND"), index);
                            newFromNode.InsertChild(new TreeNode(join), index + 1);
                        }
                        if (join.NodeValue == ")")
                        {
                            leftPar--;
                        }
                    }
                }
            }

            //add any other tables that don't belong to a join
            if (tableNodes.Count < tables.Count)
            {
                foreach (TreeNode table in tables)
                {
                    if (!tableNodes.Contains(table))
                    {
                        newFromNode.AddChild(new TreeNode(Tokens.Comma, ","));
                        newFromNode.AddChild(table);
                    }
                }
            }
            root.Children[root.Children.IndexOf(fromNode)] = newFromNode;

            TreeNode newWhereNode = new TreeNode(whereNode);
            //remove the join conditions from the where clause
            foreach (TreeNode join in origJoins.GetAll())
            {
                int index = whereNode.Children[0].Children.IndexOf(join);
                //Remove surrounding paranthesis
                if (index > 0 && index < searchCond.Children.Count)
                {
                    if (searchCond.Children[index - 1].NodeValue == "(" &&
                        searchCond.Children[index + 1].NodeValue == ")")
                    {
                        searchCond.Children[index - 1].NodeInfo = "NOT USED";
                        searchCond.Children[index + 1].NodeInfo = "NOT USED";
                    }
                }
                if (searchCond.Children.Count > 1)
                {
                    if (targetTree.IsLastNode(searchCond, join))
                    {
                        if (index > 0 && searchCond.Children[index - 1].NodeInfo != "NOT USED")
                        {
                            searchCond.Children[index - 1].NodeInfo = "NOT USED"; //remove logical operator AND/OR
                        }
                        else
                        {
                            //locate the last logical operator before the join that doesn't have the "NOT USED" info set
                            for (int i = searchCond.Children.Count - 1; i >= 0; i--)
                            {
                                if (searchCond.Children[i].NodeType == Tokens.Keyword &&
                                    searchCond.Children[i].NodeInfo != "NOT USED")
                                {
                                    searchCond.Children[i].NodeInfo = "NOT USED";
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        TreeNode lastNode = null;
                        for (int i = index - 1; i >= 0; i--)
                        {
                            if (searchCond.Children[i].NodeInfo != "NOT USED")
                            {
                                lastNode = searchCond.Children[i];
                                break;
                            }
                        }

                        //locate the first logical operator after the join that doesn't have the "NOT USED" info set
                        bool found = false;
                        for (int i = index + 1; i < searchCond.Children.Count; i++)
                        {
                            if (searchCond.Children[i].NodeType == Tokens.Predicate)
                            {
                                break;
                            }
                            if ((join.NodeInfo.Contains("JOIN") || lastNode == null || lastNode.NodeType == Tokens.Keyword)
                                && searchCond.Children[i].NodeType == Tokens.Keyword &&
                                searchCond.Children[i].NodeInfo != "NOT USED")
                            {
                                searchCond.Children[i].NodeInfo = "NOT USED";
                                found = true;
                                break;
                            }
                        }

                        if (!found && index > 0)
                        {
                            searchCond.Children[index - 1].NodeInfo = "NOT USED";
                        }
                    }
                }
                searchCond.Children[index].NodeInfo = "NOT USED";
            }

            //check again if there isn't an extra keyword at the end
            int k = searchCond.Children.Count - 1;
            while (k >= 0 && (searchCond.Children[k].NodeInfo == "NOT USED" || searchCond.Children[k].NodeValue == ")"))
            {
                k--;
            }
            if (k >= 0 && searchCond.Children[k].NodeType == Tokens.Keyword)
            {
                searchCond.Children[k].NodeInfo = "NOT USED";
            }

            if (targetTree.FindAllUsed(newWhereNode, Tokens.Predicate) == 0)
            {
                //if there are no more predicates remove the where node
                newWhereNode = new TreeNode(Tokens.Null);
            }

            root.Children[root.Children.IndexOf(whereNode)] = newWhereNode;
        }

        private void TranslateOuterJoinsInformix(Tree tree, TreeNode outerNode, ref int idxNextNode)
        {
            TreeNode nodeFrom = outerNode.Parent;
            if (nodeFrom == null || nodeFrom.NodeValue != "FROM") return;

            TreeNode nodeWhere = null;
            for (int i = nodeFrom.Index + 1; i < nodeFrom.Parent.Children.Count; i++)
            {
                if (nodeFrom.Parent.Children[i].NodeValue == "WHERE")
                {
                    nodeWhere = nodeFrom.Parent.Children[i];
                    break;
                }
            }
            if (nodeWhere == null) return;

            TreeNode outerTable = null;
            if (nodeFrom.Children[outerNode.Index + 1].NodeType == Tokens.Expression)
            {
                //TranslateOuterJoinsInformix(tree, nodeFrom.Children[node.Index + 1].Children[0]);                
                return;
            }
            else
            {
                outerTable = nodeFrom.Children[outerNode.Index + 1];
            }
            if (outerTable == null) return;

            //remove comma before outer keyword and decrement index of next transformation node for the main for loop
            if (outerNode.Index > 0 && nodeFrom.Children[outerNode.Index - 1].NodeType == Tokens.Comma)
            {
                nodeFrom.RemoveChild(outerNode.Index - 1);
                idxNextNode--;
            }

            TreeNode dominantTable = null;
            for (int i = 0; i < nodeFrom.Children.Count; i++)
            {
                if (nodeFrom.Children[i].NodeType == Tokens.Expression && nodeFrom.Children[i].NodeInfo == "TABLE")
                {
                    dominantTable = nodeFrom.Children[i];
                    break;
                }
            }
            if (dominantTable == null)
            {
                for (int i = 0; i < nodeFrom.Children.Count; i++)
                {
                    if (nodeFrom.Children[i].NodeType == Tokens.Table && (i == 0 || nodeFrom.Children[i - 1].NodeType == Tokens.Comma))
                    {
                        if (i > 0) //comma
                        {
                            i--;
                            nodeFrom.RemoveChild(i);
                        }

                        if (dominantTable == null)
                        {
                            dominantTable = new TreeNode(Tokens.Expression, "", "TABLE");
                            dominantTable.Parent = nodeFrom;
                            dominantTable.AddChild(nodeFrom.Children[i]);
                            nodeFrom.Children[i] = dominantTable;
                        }
                        else
                        {
                            dominantTable.AddChild(new TreeNode(Tokens.Keyword, "CROSS JOIN"));
                            dominantTable.AddChild(nodeFrom.Children[i]);
                            nodeFrom.RemoveChild(i);
                            i--;
                        }
                    }
                }
            }
            if (dominantTable == null) return;

            int ii = outerTable.Index;
            if (dominantTable.Index > ii)
            {
                if (ii + 2 < nodeFrom.Children.Count && nodeFrom.Children[ii + 1].NodeType == Tokens.Comma && nodeFrom.Children[ii + 2].NodeValue == "OUTER")
                {
                    nodeFrom.Children[ii + 1].NodeType = Tokens.LeftParant;
                    nodeFrom.Children[ii + 1].NodeValue = "(";
                    outerNode.NodeInfo = "(";
                }
                outerNode.NodeValue = "RIGHT OUTER JOIN";
                ii = outerNode.Index;
                nodeFrom.Children[outerTable.Index] = outerNode;
                nodeFrom.Children[ii] = outerTable;
                TransformOuterWhere(nodeWhere, outerTable, dominantTable, outerNode.NodeInfo == "(");
            }
            else
            {
                outerNode.NodeValue = "LEFT OUTER JOIN";
                TransformOuterWhere(nodeWhere, outerTable, outerTable, outerNode.NodeInfo == "(");
            }
        }

        private void TransformOuterWhere(TreeNode nodeWhere, TreeNode outerTable, TreeNode onTable, bool closeParant)
        {
            TreeNode nodeON = onTable.Parent.InsertChild(new TreeNode(Tokens.Keyword, "ON"), onTable.Index + 1);
            if (closeParant)
            {
                nodeON.Parent.InsertChild(new TreeNode(Tokens.RightParant, ")"), nodeON.Index);
            }

            TreeNode where = nodeWhere.Children[0]; //jump to search condition
            int parant = -1;
            int n = where.Children.Count - 1;
            while (n >= 0)
            {
                switch (where.Children[n].NodeType)
                {
                    case Tokens.Predicate:
                        {
                            if (parant == -1 && IsOuterCondition(where, n, n, outerTable))
                            {
                                #region delete condition from Where clause and add to ON clause
                                parant = n;
                                if (n > 0 && where.Children[n - 1].NodeType == Tokens.Keyword) //AND, OR
                                {
                                    n--;
                                }
                                for (int j = parant; j >= n; j--)
                                {
                                    outerTable.Parent.InsertChild(where.Children[j].Clone(outerTable.Parent), nodeON.Index + 1);
                                    where.RemoveChild(j);
                                }
                                parant = -1;
                                #endregion
                            }
                            break;
                        }
                    case Tokens.RightParant:
                        {
                            parant = n;
                            break;
                        }
                    case Tokens.LeftParant:
                        {
                            if (parant != -1 && IsOuterCondition(where, n, parant, outerTable))
                            {
                                #region delete condition from Where clause and add to ON clause
                                if (n > 0 && where.Children[n - 1].NodeType == Tokens.Keyword) //AND, OR
                                {
                                    n--;
                                }
                                for (int j = parant; j >= n; j--)
                                {
                                    outerTable.Parent.InsertChild(where.Children[j].Clone(outerTable.Parent), nodeON.Index + 1);
                                    where.RemoveChild(j);
                                }
                                #endregion
                            }
                            parant = -1;
                            break;
                        }
                }
                n--;
            }
            if (nodeON.Index + 1 < nodeON.Parent.Children.Count && nodeON.Parent.Children[nodeON.Index + 1].NodeType == Tokens.Keyword)
            {
                nodeON.Parent.RemoveChild(nodeON.Index + 1);
            }
            if (where.Children.Count > 0 && where.Children[0].NodeType == Tokens.Keyword)
            {
                where.RemoveChild(0);
            }
            if (where.Children.Count == 0)
            {
                nodeWhere.Parent.RemoveChild(nodeWhere.Index);
            }
        }

        private bool IsColumnInTable(TreeNode node, TreeNode table)
        {
            int idx;
            if (node.NodeType == Tokens.Column)
            {
                idx = node.NodeValue.LastIndexOf(".");
                if (idx == -1)
                {
                    if (Settings != null && Settings.DbStructure != null && Settings.DbStructure[node.NodeValue.ToLower().RemoveBrackets()].ToUpper().Split(',').Contains(table.NodeValue.ToUpper()))
                    {
                        return true;
                    }
                }
                else
                {
                    if (table.Children.Count > 0 && table.Children[0].NodeValue.ToUpper() == node.NodeValue.Substring(0, idx).ToUpper())
                    {
                        return true;
                    }
                }
            }
            else
            {
                for (idx = 0; idx < node.Children.Count; idx++)
                {
                    if (IsColumnInTable(node.Children[idx], table))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private bool IsOuterCondition(TreeNode node, int iStart, int iEnd, TreeNode outerTable)
        {
            TreeNodeCollection operators = new TreeNodeCollection();

            for (int i = iStart; i <= iEnd; i++)
            {
                if (node.Children[i].NodeType == Tokens.Predicate)
                {
                    TreeNode pred = node.Children[i];
                    int j;
                    for (j = 0; j < pred.Children.Count; j++)
                    {
                        if (pred.Children[j].NodeType == Tokens.RelatOperator || (pred.Children[j].NodeType == Tokens.Keyword && (pred.Children[j].NodeValue == "LIKE" || pred.Children[j].NodeValue == "IN")))
                        {
                            break;
                        }
                    }
                    if (j < pred.Children.Count)
                    {
                        //if expression before relatoperator contains a column of outer table
                        for (int k = 0; k < j; k++)
                        {
                            if (IsColumnInTable(pred.Children[k], outerTable))
                            {
                                return true;
                            }
                        }
                        //if expression after relatoperator contains a column of outer table
                        for (int k = j + 1; k < pred.Children.Count; k++)
                        {
                            if (IsColumnInTable(pred.Children[k], outerTable))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        private void TranslateOuterJoinsInformix_old(Tree targetTree, TreeNode root, TreeNode node)
        {
            TreeNodeCollection tables = new TreeNodeCollection();
            TreeNodeCollection predicates = new TreeNodeCollection();
            TreeNodeCollection joins;
            TreeNode exprNode, table1, table2, tempNode;
            JoinsCollection joinsList = new JoinsCollection();
            Dictionary<TreeNode, TreeNodeCollection> conditions = new Dictionary<TreeNode, TreeNodeCollection>();

            //if a tree contains subSelect searching from root could result in wrong result
            //we need to search keywords from the current select statement
            TreeNode fromNode = root.FindChild("FROM");
            targetTree.FindAll(fromNode, Tokens.Table, ref tables);

            TreeNode whereNode = root.FindChild("WHERE");
            targetTree.FindAll(whereNode, Tokens.Predicate, ref predicates);

            TreeNode searchCond = whereNode.Children[0];

            //search for all the join predicates
            foreach (TreeNode predicate in predicates)
            {
                exprNode = targetTree.FindNode(predicate, Tokens.Expression);
                exprNode = targetTree.FindNode(exprNode, Tokens.Column);
                table1 = GetTable(exprNode, tables);

                exprNode = targetTree.FindNodeReverse(predicate, Tokens.Expression);
                exprNode = targetTree.FindNode(exprNode, Tokens.Column);
                table2 = GetTable(exprNode, tables);

                if (predicate.NodeInfo.EndsWith("JOIN"))
                {
                    targetTree.FindNode(predicate, Tokens.JoinOperator).NodeInfo = "SKIP";
                    targetTree.FindNode(predicate, Tokens.JoinOperator).NodeValue = "";

                    if (predicate.NodeInfo == "JOIN" &&
                       (((tempNode = targetTree.PreviousNode(predicate, Tokens.Keyword)) != null && tempNode.NodeValue == "OR") ||
                        ((tempNode = targetTree.NextNode(predicate, Tokens.Keyword)) != null && tempNode.NodeValue == "OR")))
                    {
                        //leave inner joins that are part of an OR condition in the where clause
                    }
                    else if (table1 != table2) // if table1 = table2 then it's not a join, it's a simple condition
                    {
                        joins = joinsList[table1, table2];

                        if (joinsList.IsReversed(table1, table2))
                        {
                            if (predicate.NodeInfo == "RIGHT OUTER JOIN")
                            {
                                predicate.NodeInfo = "LEFT OUTER JOIN";
                            }
                            else if (predicate.NodeInfo == "LEFT OUTER JOIN")
                            {
                                predicate.NodeInfo = "RIGHT OUTER JOIN";
                            }
                        }
                        int index = table1.Parent.Children.IndexOf(table1);
                        if (index > 0 && table1.Parent.Children[index - 1].NodeValue == "OUTER")
                        {
                            predicate.NodeInfo = "LEFT OUTER JOIN";
                        }
                        index = table2.Parent.Children.IndexOf(table2);
                        if (index > 0 && table2.Parent.Children[index - 1].NodeValue == "OUTER")
                        {
                            predicate.NodeInfo = "LEFT OUTER JOIN";
                        }
                        if (predicate.NodeInfo == "JOIN")
                        {
                            predicate.NodeInfo = "INNER JOIN";
                        }

                        if (joins.Count == 0)
                        {
                            joins.Add(predicate);
                        }
                        else
                        {
                            if (joins.Find("INNER JOIN") != null && predicate.NodeInfo.Contains("OUTER"))
                            {
                                joins.Clear();
                            }
                            if ((joins.Find("LEFT OUTER JOIN") != null || joins.Find("RIGHT OUTER JOIN") != null) && predicate.NodeInfo == "INNER JOIN")
                            {
                                //don't add the inner joins if there is already an outer join in the list
                            }
                            else
                            {
                                joins.Add(predicate);
                            }
                        }
                    }
                }
                else if (table1 != null)
                {
                    //it's a simple condition
                    if (!conditions.ContainsKey(table1))
                    {
                        conditions.Add(table1, new TreeNodeCollection());
                    }
                    //find the entire group of conditions
                    int groupStart = -1, groupEnd = -1;
                    int index = searchCond.Children.IndexOf(predicate);
                    for (int i = index - 1; i >= 0; i--)
                    {
                        if (searchCond.Children[i].NodeInfo == "GROUP START")
                        {
                            groupStart = i;
                            break;
                        }
                    }
                    for (int i = index + 1; i < searchCond.Children.Count; i++)
                    {
                        if (searchCond.Children[i].NodeInfo == "GROUP END")
                        {
                            groupEnd = i;
                            break;
                        }
                    }
                    if (groupStart != -1 && groupEnd != -1)
                    {
                        for (int i = groupStart; i <= groupEnd; i++)
                        {
                            if (!conditions[table1].Contains(searchCond.Children[i]))
                            {
                                conditions[table1].Add(searchCond.Children[i]);
                            }
                        }
                    }
                    else
                    {
                        conditions[table1].Add(predicate);
                    }
                }
            }
            JoinsCollection origJoins = joinsList.Clone();
            joinsList.Sort();

            //reconstruct the FROM clause
            TreeNode newFromNode = new TreeNode(Tokens.Keyword, "FROM");
            TreeNodeCollection tableNodes = new TreeNodeCollection();

            foreach (Joins joinsItem in joinsList)
            {
                int tableCount = tableNodes.Count;
                if (joinsItem.Table1 != null && !tableNodes.Contains(joinsItem.Table1))
                {
                    tableNodes.Add(joinsItem.Table1);
                }
                if (joinsItem.Table2 != null && !tableNodes.Contains(joinsItem.Table2))
                {
                    tableNodes.Add(joinsItem.Table2);
                }

                //for outer joins we have to add all the conditions on the (+) table to the join condition
                //Example
                //select * from A, B where ACOL1 = BCOL1(+) AND BCOL2 IS NULL
                //has to be translated to:
                //select * from A LEFT OUTER JOIN B ON ACOL1 = BCOL1 AND BCOL2 IS NULL
                TreeNode outerJoin = joinsItem.GetOuterJoin();
                if (outerJoin != null)
                {
                    TreeNode outerTable;
                    if (outerJoin.NodeInfo.Contains("LEFT"))
                    {
                        outerTable = joinsItem.Table2;
                    }
                    else
                    {
                        outerTable = joinsItem.Table1;
                    }
                    if (conditions.ContainsKey(outerTable))
                    {
                        for (int i = 0; i < conditions[outerTable].Count; i++)
                        {
                            if (conditions[outerTable][i].NodeInfo == "GROUP START")
                            {
                                int start = i;
                                bool foundNULL = false;
                                do
                                {
                                    i++;
                                    if (targetTree.FindNode(conditions[outerTable][i], "NULL") != null)
                                    {
                                        foundNULL = true;
                                    }
                                } while (conditions[outerTable][i].NodeInfo != "GROUP END");
                                if (foundNULL)
                                {
                                    bool isJoinItem = true;
                                    //found NULL
                                    for (int j = start; j <= i; j++)
                                    {
                                        if (conditions[outerTable][j].NodeType == Tokens.Predicate)
                                        {
                                            TreeNodeCollection cols = new TreeNodeCollection();
                                            targetTree.FindAll(conditions[outerTable][j], Tokens.Column, ref cols);
                                            if (cols.Count > 1)
                                            {
                                                isJoinItem = false;
                                                break;
                                            }
                                        }

                                    }
                                    if (isJoinItem)
                                    {
                                        for (int j = start; j <= i; j++)
                                        {

                                            joinsItem.Items.Add(conditions[outerTable][j]);
                                        }
                                    }
                                }
                            }
                            else if (targetTree.FindNode(conditions[outerTable][i], "NULL") != null)
                            {
                                joinsItem.Items.Add(conditions[outerTable][i]);
                            }

                        }
                    }
                }

                //if there are any new tables, add them to the FORM clause
                if (tableNodes.Count > tableCount)
                {
                    if (tableNodes.Count == tableCount + 2)
                    {
                        newFromNode.AddChild(tableNodes[tableCount + 1]);
                    }

                    for (int i = 0; i < joinsItem.Items.Count; i++)
                    {
                        TreeNode join = joinsItem.Items[i];
                        if (i == 0)
                        {
                            //check if we don't have to reverse the type of the join
                            //if (tableNodes[tableCount] != joinsItem.Table2)
                            //{
                            //    if (join.NodeInfo == "RIGHT OUTER JOIN")
                            //    {
                            //        join.NodeInfo = "LEFT OUTER JOIN";
                            //    }
                            //    else if (join.NodeInfo == "LEFT OUTER JOIN")
                            //    {
                            //        join.NodeInfo = "RIGHT OUTER JOIN";
                            //    }
                            //}

                            newFromNode.AddChild(new TreeNode(Tokens.JoinSpecif, join.NodeInfo));
                            newFromNode.AddChild(tableNodes[tableCount]);
                            newFromNode.AddChild(new TreeNode(Tokens.Keyword, "ON"));
                            newFromNode.AddChild(new TreeNode(join));
                        }
                        else
                        {
                            if ((newFromNode.Children[newFromNode.Children.Count - 1].NodeType == Tokens.Predicate &&
                                (join.NodeType == Tokens.Predicate || join.NodeInfo == "GROUP START")) ||
                                (newFromNode.Children[newFromNode.Children.Count - 1].NodeType == Tokens.RightParant &&
                                join.NodeType == Tokens.LeftParant))
                            {
                                newFromNode.AddChild(new TreeNode(Tokens.Keyword, "AND"));
                            }
                            newFromNode.AddChild(new TreeNode(join));
                        }
                    }
                }
                else
                {
                    int index = Math.Max(newFromNode.Children.IndexOf(joinsItem.Table1),
                        newFromNode.Children.IndexOf(joinsItem.Table2));

                    //add the condition after the first predicate found after the table
                    if (newFromNode.Children[index + 2].NodeType == Tokens.Predicate)
                    {
                        index += 3;
                    }
                    else
                    {
                        index += 5;
                    }
                    int leftPar = 0;
                    for (int i = 0; i < joinsItem.Items.Count; i++)
                    {
                        TreeNode join = joinsItem.Items[i];
                        if (join.NodeValue == "(")
                        {
                            leftPar++;
                        }

                        if (leftPar == 0)
                        {
                            newFromNode.InsertChild(new TreeNode(Tokens.Keyword, "AND"), index);
                            newFromNode.InsertChild(new TreeNode(join), index + 1);
                        }
                        if (join.NodeValue == ")")
                        {
                            leftPar--;
                        }
                    }
                }
            }

            //add any other tables that don't belong to a join
            if (tableNodes.Count < tables.Count)
            {
                foreach (TreeNode table in tables)
                {
                    if (!tableNodes.Contains(table))
                    {
                        //ASZ:don't add coma if there is no tables yet
                        if (newFromNode.Children.Count > 0)
                        {
                            newFromNode.AddChild(new TreeNode(Tokens.Comma, ","));
                        }
                        newFromNode.AddChild(table);
                    }
                }
            }
            root.Children[root.Children.IndexOf(fromNode)] = newFromNode;

            TreeNode newWhereNode = new TreeNode(whereNode);
            //remove the join conditions from the where clause
            foreach (TreeNode join in origJoins.GetAll())
            {
                int index = whereNode.Children[0].Children.IndexOf(join);
                //Remove surrounding paranthesis
                if (index > 0 && index < searchCond.Children.Count)
                {
                    if (searchCond.Children[index - 1].NodeValue == "(" &&
                        searchCond.Children[index + 1].NodeValue == ")")
                    {
                        searchCond.Children[index - 1].NodeInfo = "NOT USED";
                        searchCond.Children[index + 1].NodeInfo = "NOT USED";
                    }
                }
                if (searchCond.Children.Count > 1)
                {
                    if (targetTree.IsLastNode(searchCond, join))
                    {
                        if (index > 0 && searchCond.Children[index - 1].NodeInfo != "NOT USED")
                        {
                            searchCond.Children[index - 1].NodeInfo = "NOT USED"; //remove logical operator AND/OR
                        }
                        else
                        {
                            //locate the last logical operator before the join that doesn't have the "NOT USED" info set
                            for (int i = searchCond.Children.Count - 1; i >= 0; i--)
                            {
                                if (searchCond.Children[i].NodeType == Tokens.Keyword &&
                                    searchCond.Children[i].NodeInfo != "NOT USED")
                                {
                                    searchCond.Children[i].NodeInfo = "NOT USED";
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        TreeNode lastNode = null;
                        for (int i = index - 1; i >= 0; i--)
                        {
                            if (searchCond.Children[i].NodeInfo != "NOT USED")
                            {
                                lastNode = searchCond.Children[i];
                                break;
                            }
                        }

                        //locate the first logical operator after the join that doesn't have the "NOT USED" info set
                        bool found = false;
                        for (int i = index + 1; i < searchCond.Children.Count; i++)
                        {
                            if (searchCond.Children[i].NodeType == Tokens.Predicate)
                            {
                                break;
                            }
                            if ((join.NodeInfo.Contains("JOIN") || lastNode == null || lastNode.NodeType == Tokens.Keyword)
                                && searchCond.Children[i].NodeType == Tokens.Keyword &&
                                searchCond.Children[i].NodeInfo != "NOT USED")
                            {
                                searchCond.Children[i].NodeInfo = "NOT USED";
                                found = true;
                                break;
                            }
                        }

                        if (!found && index > 0)
                        {
                            searchCond.Children[index - 1].NodeInfo = "NOT USED";
                        }
                    }
                }
                searchCond.Children[index].NodeInfo = "NOT USED";
            }

            //check again if there isn't an extra keyword at the end
            int k = searchCond.Children.Count - 1;
            while (k >= 0 && (searchCond.Children[k].NodeInfo == "NOT USED" || searchCond.Children[k].NodeValue == ")"))
            {
                k--;
            }
            if (k >= 0 && searchCond.Children[k].NodeType == Tokens.Keyword)
            {
                searchCond.Children[k].NodeInfo = "NOT USED";
            }

            if (targetTree.FindAllUsed(newWhereNode, Tokens.Predicate) == 0)
            {
                //if there are no more predicates remove the where node
                newWhereNode = new TreeNode(Tokens.Null);
            }

            root.Children[root.Children.IndexOf(whereNode)] = newWhereNode;
        }

        private string GenerateAlias(string expr)
        {
            if (!aliasCount.ContainsKey(expr))
            {
                aliasCount.Add(expr, 1);
                return "\"" + expr + "\"";
            }
            else
            {
                aliasCount[expr]++;
                return "\"" + expr + aliasCount[expr] + "\"";
            }
        }

        private string GetNameWithCase(string name)
        {
            switch (Settings.Casing)
            {
                case Casing.Proper:
                    break;
                case Casing.Lower:
                    name = name.ToLower();
                    break;
                case Casing.Upper:
                    name = name.ToUpper();
                    break;
            }

            return name;
        }

        private bool IsValidDateTime(string nodeValue, out CustomDateTime datetime)
        {
            //Check if it's a datetime constant and change the format
            //In SqlBase valid formats are:
            //--------------------------------------------------
            //dd.mm.yyyy              00:00:00                AM
            //dd-mon-yy               hh                      PM
            //dd-mon-yyyy             hh:mi
            //dd/mon/yy               hh:mi:ss
            //dd/mon/yyyy             hh:mi:ss:999999
            //mm-dd-yy
            //mm-dd-yyyy
            //mm/dd/yy
            //mm/dd/yyyy
            //yyyy-mm-dd
            //--------------------------------------------------
            //In SqlServer there are only 2 safe formats:
            //--------------------------------------------------
            //YYMMDD
            //YYYY-MM-DDThh:mm:ss
            //--------------------------------------------------

            datetime = new CustomDateTime();
            bool valideDateTimeFound = false;
            Regex reg = new Regex(@"^(?<date1>\d{1,4})(?<sep>-|/|\.)(?<date2>\d{1,2}|\w{3})\k<sep>(?<date3>\d{1,4})");
            string stringToMatch = nodeValue;
            Match match = reg.Match(stringToMatch);
            if (match.Success)
            {
                valideDateTimeFound = true;
                GroupCollection groups = match.Groups;
                switch (groups["sep"].Value)
                {
                    case "/":
                        if (groups["date2"].Value.Length == 3)
                        {
                            //dd/mon/yyyy
                            datetime.Day = Convert.ToInt32(groups["date1"].Value);
                            datetime.Month = Array.IndexOf(LexicalAnalyzer.months, groups["date2"].Value.ToUpper()) + 1;
                            datetime.Year = Convert.ToInt32(groups["date3"].Value);
                        }
                        else
                        {
                            //mm/dd/yyyy
                            datetime.Month = Convert.ToInt32(groups["date1"].Value);
                            datetime.Day = Convert.ToInt32(groups["date2"].Value);
                            datetime.Year = Convert.ToInt32(groups["date3"].Value);
                        }
                        break;
                    case "-":
                        if (groups["date2"].Value.Length == 3)
                        {
                            //dd-mon-yyyy
                            datetime.Day = Convert.ToInt32(groups["date1"].Value);
                            datetime.Month = Array.IndexOf(LexicalAnalyzer.months, groups["date2"].Value.ToUpper()) + 1;
                            datetime.Year = Convert.ToInt32(groups["date3"].Value);
                        }
                        else if (groups["date1"].Value.Length == 4)
                        {
                            //yyyy-mm-dd
                            datetime.Year = Convert.ToInt32(groups["date1"].Value);
                            datetime.Month = Convert.ToInt32(groups["date2"].Value);
                            datetime.Day = Convert.ToInt32(groups["date3"].Value);
                        }
                        else
                        {
                            //mm-dd-yyyy
                            datetime.Month = Convert.ToInt32(groups["date1"].Value);
                            datetime.Day = Convert.ToInt32(groups["date2"].Value);
                            datetime.Year = Convert.ToInt32(groups["date3"].Value);
                        }
                        break;
                    case ".":
                        if (groups["date3"].Value.Length == 4)
                        {
                            //dd.mm.yyyy
                            datetime.Day = Convert.ToInt32(groups["date1"].Value);
                            datetime.Month = Convert.ToInt32(groups["date2"].Value);
                            datetime.Year = Convert.ToInt32(groups["date3"].Value);
                        }
                        else
                        {
                            //it's a time; will be matched by the time regex
                            valideDateTimeFound = false;
                        }
                        break;
                }
            }
            if (valideDateTimeFound)
            {
                stringToMatch = stringToMatch.Remove(0, match.Value.Length).Trim();
                stringToMatch = stringToMatch.TrimStart('-');
                if (stringToMatch.Length > 0)
                {
                    valideDateTimeFound = false;
                }
            }


            reg = new Regex(@"^(?<hour>\d{1,2})(?<sep>:|\.)(?<min>\d{0,2})\k<sep>?(?<sec>\d{0,2})\k<sep>?(?<milli>\d{0,10})( |:|\.)?(?<ampm>AM|PM)?$");
            match = reg.Match(stringToMatch);
            if (match.Success)
            {
                valideDateTimeFound = true;
                GroupCollection groups = match.Groups;
                datetime.Hour = Convert.ToInt32(groups["hour"].Value);
                if (groups["min"].Value != "")
                {
                    datetime.Minute = Convert.ToInt32(groups["min"].Value);
                }
                if (groups["sec"].Value != "")
                {
                    datetime.Second = Convert.ToInt32(groups["sec"].Value);
                }
                if (groups["milli"].Value != "")
                {
                    datetime.Millisecond = Convert.ToInt32(groups["milli"].Value.Length > 3 ?
                        groups["milli"].Value.Substring(0, 3) : groups["milli"].Value);
                }
                if (groups["ampm"].Value.ToUpper() == "PM")
                {
                    datetime.Hour = (datetime.Hour + 12) % 24;
                }
            }

            //PPJ:FINAL:PJ:#214 Fix DateTime-Conversion; Use default TryParse
            if (!valideDateTimeFound)
            {
                DateTime dt = DateTime.MinValue;
                if (DateTime.TryParse(nodeValue.Replace("'", ""), out dt))
                {
                    valideDateTimeFound = true;

                    datetime.Day = dt.Day;
                    datetime.Month = dt.Month;
                    datetime.Year = dt.Year;
                    datetime.Hour = dt.Hour;
                    datetime.Minute = dt.Minute;
                    datetime.Second = dt.Second;
                }
            }

            return valideDateTimeFound;
        }

        public bool IsDateColumn(string column)
        {
            bool isDate = false;
            if (this.columnInfo.ContainsKey(column.ToUpper()))
            {
                var colInfo = this.columnInfo[column.ToUpper()];

                if (colInfo != null)
                {
                    isDate = colInfo.IsDate;
                }
            }

            return isDate;
        }

        private bool GetColumnProperties(string columnName, out bool isBinary, out bool isLong, out bool isExternal)
        {
            if (this.columnInfo.ContainsKey(columnName.ToUpper()))
            {
                var colInfo = this.columnInfo[columnName.ToUpper()];

                if (colInfo != null)
                {
                    isBinary = colInfo.IsBinary;
                    isLong = colInfo.IsLong;
                    isExternal = colInfo.IsExternal;
                    return true;
                }
                else
                {
                    isBinary = false;
                    isLong = false;
                    isExternal = false;
                    return false;
                }
            }
            else
            {
                isBinary = false;
                isLong = false;
                isExternal = false;
                return false;
            }
        }

        public bool GetColumnDateTimeProperties(string columnName, out bool isDate, out bool isTime, out bool isTimestamp, out int milliseconds)
        {
            if (this.columnInfo.ContainsKey(columnName.ToUpper()))
            {
                var colInfo = this.columnInfo[columnName.ToUpper()];

                if (colInfo != null)
                {
                    isDate = colInfo.IsDate;
                    isTime = colInfo.IsTime;
                    isTimestamp = colInfo.IsTimestamp;
                    milliseconds = colInfo.Milliseconds;
                    return true;
                }
                else
                {
                    isDate = false;
                    isTime = false;
                    isTimestamp = false;
                    milliseconds = 0;
                    return false;
                }
            }
            else
            {
                isDate = false;
                isTime = false;
                isTimestamp = false;
                milliseconds = 0;
                return false;
            }
        }

        public bool GetNumericPrecision(string columnName, ref object dataPrecision, ref object dataScale)
        {
            if (this.columnInfo.ContainsKey(columnName.ToUpper()))
            {
                var col = this.columnInfo[columnName.ToUpper()];

                if (col != null)
                {
                    dataPrecision = col.DataPrecision;
                    dataScale = col.DataScale;
                    return true;
                }
                else
                {
                    dataPrecision = 0;
                    dataScale = 0;
                    return false;
                }
            }
            else
            {
                dataPrecision = 0;
                dataScale = 0;
                return false;
            }
        }

        public bool GetColumnDataType(string columnName, ref string dataType)
        {
            if (this.columnInfo.ContainsKey(columnName.ToUpper()))
            {
                var colType = this.columnInfo[columnName.ToUpper()];

                if (colType != null)
                {
                    dataType = colType.DataType;
                    return true;
                }
                else
                {
                    dataType = "";
                    return false;
                }
            }
            else
            {
                dataType = "";
                return false;
            }
        }

        public bool HasExternalColumns(string table)
        {
            bool hasExternals = false;
            if (this.tableInfoExtBinCols.ContainsKey(table.ToUpper()))
            {
                var tblInfo = this.tableInfoExtCols[table.ToUpper()];
                if (tblInfo != null)
                {
                    hasExternals = tblInfo;
                }
            }

            return hasExternals;
        }

        public bool HasExternalLongOrBinaryColumns(string table)
        {
            bool hasExternals = false;
            if (this.tableInfoExtBinCols.ContainsKey(table.ToUpper()))
            {
                var tblInfo = this.tableInfoExtBinCols[table.ToUpper()];
                if (tblInfo != null)
                {
                    hasExternals = tblInfo;
                }
            }

            return hasExternals;
        }

        private void InitCurrentColsInfo(TreeNode root, Tree targetTree)
        {
            if (Globals.TableInformation.IsInitialized && Settings.DbStructure != null)
            {
                columnInfo.Clear();

                List<string> colList = new List<string>();
                List<string> tableList = new List<string>();

                TreeNodeCollection cols = new TreeNodeCollection();
                TreeNodeCollection tabls = new TreeNodeCollection();
                targetTree.FindAll(root, Tokens.Column, ref cols);
                targetTree.FindAll(root, Tokens.Table, ref tabls);

                foreach (TreeNode col in cols)
                {
                    string colName = String.IsNullOrEmpty(col.NodeValue) ? col.Children[0].NodeValue : col.NodeValue;
                    colList.Add(GetColumnName(colName).ToUpper());
                    tableList.Add(GetTableName(colName).ToUpper());
                }

                string[] columns = colList.ToArray();
                string[] tables = tableList.ToArray();

                var colPropsTblInf = Globals.TableInformation.GetColProps(columns);
                var colPropsDb = Settings.DbStructure.GetColProps(columns);
                ColumnInformation colInfo;

                foreach (var tempColPropsTblInf in colPropsTblInf)
                {
                    colInfo = new ColumnInformation();

                    //var tempColPropsTblInf = colPropsTblInf.GetEnumerator().Current;
                    if (tempColPropsTblInf != null && !this.columnInfo.ContainsKey(tempColPropsTblInf.Column))
                    {
                        TreeNode col = null;
                        foreach (TreeNode node in cols)
                        {
                            if (GetColumnName(node.NodeValue).ToUpper() == tempColPropsTblInf.Column.ToUpper())
                            {
                                col = node;
                            }
                        }
                        if (col != null)
                        {
                            TreeNode table = GetTable(col, tabls);
                            if (table != null && tempColPropsTblInf.Table.ToUpper() == GetTableName(table.NodeValue).ToUpper())
                            {
                                colInfo.Column = tempColPropsTblInf.Column;
                                colInfo.Table = tempColPropsTblInf.Table;
                                colInfo.Owner = tempColPropsTblInf.Schema;
                                colInfo.IsBinary = tempColPropsTblInf.IsBinary;
                                colInfo.IsDate = tempColPropsTblInf.IsDate;
                                colInfo.IsExternal = tempColPropsTblInf.IsExternal;
                                colInfo.IsLong = tempColPropsTblInf.IsLong;
                                colInfo.IsTime = tempColPropsTblInf.IsTime;
                                colInfo.Milliseconds = tempColPropsTblInf.Milliseconds;

                                if (tempColPropsTblInf.DataType == "TIMESTMP")
                                {
                                    if (!tempColPropsTblInf.IsDate && !tempColPropsTblInf.IsTime)
                                    {
                                        colInfo.IsTimestamp = true;
                                    }
                                    else
                                    {
                                        colInfo.IsTimestamp = false;
                                    }
                                }
                                else
                                {
                                    colInfo.IsTimestamp = false;
                                }

                                if (tempColPropsTblInf.DataType == "LONGVAR")
                                {
                                    if (tempColPropsTblInf.IsLong && !tempColPropsTblInf.IsBinary)
                                    {
                                        this.countLong++;
                                    }
                                    if (tempColPropsTblInf.IsLong && tempColPropsTblInf.IsBinary)
                                    {
                                        this.countLongRaw++;
                                    }
                                    if (!tempColPropsTblInf.IsLong && !tempColPropsTblInf.IsBinary)
                                    {
                                        this.countVarchar2++;
                                    }
                                    if (!tempColPropsTblInf.IsLong && tempColPropsTblInf.IsBinary)
                                    {
                                        this.countRaw++;
                                    }
                                }

                                this.columnInfo.Add(colInfo.Column, colInfo);

                                if (!this.tableInfoExtCols.ContainsKey(colInfo.Table))
                                {
                                    this.tableInfoExtCols.Add(colInfo.Table, colInfo.IsExternal);
                                }
                                else
                                {
                                    if (colInfo.IsExternal == true)
                                    {
                                        this.tableInfoExtCols[colInfo.Table] = colInfo.IsExternal;
                                    }
                                }

                                if (!this.tableInfoExtBinCols.ContainsKey(colInfo.Table))
                                {
                                    this.tableInfoExtBinCols.Add(colInfo.Table, (colInfo.IsExternal || colInfo.IsBinary || colInfo.IsLong));
                                }
                                else
                                {
                                    if ((colInfo.IsExternal || colInfo.IsBinary || colInfo.IsLong) == true)
                                    {
                                        this.tableInfoExtBinCols[colInfo.Table] = (colInfo.IsExternal || colInfo.IsBinary || colInfo.IsLong);
                                    }
                                }
                            }
                        }
                    }
                }
                foreach (var tempColPropsDb in colPropsDb)
                {
                    colInfo = new ColumnInformation();

                    if (tempColPropsDb != null)
                    {
                        TreeNode col = null;
                        foreach (TreeNode node in cols)
                        {
                            if (GetColumnName(node.NodeValue).ToUpper() == tempColPropsDb.Name.ToUpper())
                            {
                                col = node;
                            }
                        }
                        if (col != null)
                        {
                            TreeNode table = GetTable(col, tabls);
                            if (table != null && tempColPropsDb.Table.ToUpper() == GetTableName(table.NodeValue).ToUpper())
                            {
                                if (!this.columnInfo.ContainsKey(tempColPropsDb.Name))
                                {
                                    colInfo.Column = tempColPropsDb.Name;
                                    colInfo.Owner = tempColPropsDb.TbCreator;
                                    colInfo.Table = tempColPropsDb.Table;
                                    this.columnInfo.Add(tempColPropsDb.Name, colInfo);
                                }
                                this.columnInfo[tempColPropsDb.Name].DataType = tempColPropsDb.Type;
                                this.columnInfo[tempColPropsDb.Name].DataScale = tempColPropsDb.DataScale;
                                this.columnInfo[tempColPropsDb.Name].DataPrecision = tempColPropsDb.DataPrecision;
                            }
                        }
                    }
                }
            }
        }
    }

    public class ColumnInformation
    {
        public string Column { get; set; }
        public string Table { get; set; }
        public string Owner { get; set; }
        public string DataType { get; set; }
        public int Milliseconds { get; set; }
        public bool IsBinary { get; set; }
        public bool IsLong { get; set; }
        public bool IsExternal { get; set; }
        public bool IsDate { get; set; }
        public bool IsTime { get; set; }
        public bool IsTimestamp { get; set; }
        public object DataScale { get; set; }
        public object DataPrecision { get; set; }
    }
}
