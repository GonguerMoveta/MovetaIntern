using System;
using System.Collections;
using fecher.Common;
using System.Text;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace fecher.SpTranslator
{
	/// <summary>
	/// An SpTranslator for translating stored procedures to Oracle syntax
	/// </summary>
	public class SpTranslatorOracle: SpTranslator
    {
        #region Private Members
        private Tree tree;
        private StringBuilder result = new StringBuilder();
        private string sqlInto = "";
        private Queue<string> sqlFetch = new Queue<string>();
        private Queue<string> sqlFetchParam = new Queue<string>();
        private string returnExpr = "";
        private int onProcFetchReturns = 0;
        private StringBuilder whenSqlError = new StringBuilder();
        private bool containsFunction = false;
        private bool isQuery = false;
        private Dictionary<string, Cursor> cursors = new Dictionary<string, Cursor>();
        private Dictionary<string, SqlStatement> sqlStatements = new Dictionary<string, SqlStatement>();
        private StringDictionary preparedStatements = new StringDictionary();
        private string sqlKey = "";
        private List<SpVariable> outputVars = new List<SpVariable>();
        private bool hasTempTable = false;
        #endregion

        protected override string ProcessTree(Tree parseTree)
        {
            tree = parseTree;
            
            bool containsAs = false;

            foreach (TreeNode node in parseTree.root.Children)
            {
                switch (node.NodeValue)
                {
                    case "PROCEDURE":
                        result.Append("CREATE PROCEDURE ");
                        result.AppendLine(node.Children[0].NodeValue);
                        break;

                    case "PARAMETERS":
                        result.Append("(");
                        foreach (TreeNode parentNode in node.Children)
                        {
                            string dataType = "";
                            foreach (TreeNode child in parentNode.Children)
                            {
                                switch (child.NodeType)
                                {
                                    case Tokens.DataType:
                                        dataType = SpDictionaryOracle.GetValue(child.NodeValue);
                                        if (dataType == "not supported")
                                        {
                                            throw new NotSupportedException("The data type: " + child.NodeValue + " is not supported");
                                        }
                                        break;

                                    case Tokens.Identifier:
                                        result.Append(child.NodeValue + " ");
                                        result.Append(dataType);
                                        if (child.NodeInfo == "RECEIVE")
                                        {
                                            result.Append(" IN OUT");
                                            //add the variable to the outputVars list - for later use in temp table
                                            outputVars.Add(new SpVariable(child.NodeValue, dataType));
                                        }
                                        if (!node.IsLastChild(parentNode))
                                        {
                                            result.AppendLine(",");
                                        }
                                        else
                                        {
                                            result.AppendLine();
                                        }
                                        break;

                                    case Tokens.Comment:
                                        result.AppendLine("--" + child.NodeValue);
                                        break;
                                }
                            }
                        }
                        result.AppendLine(")");
                        break;

                    case "LOCAL VARIABLES":
                        result.AppendLine("AS");
                        //TODO: need a more complex checking
                        //string select = CheckForSimpleOnProcedureFetch();
                        //if (!String.IsNullOrEmpty(select))
                        //{
                        //    result.AppendLine(select);
                        //    return result.ToString();
                        //}
                        containsAs = true;
                        foreach (TreeNode parentNode in node.Children)
                        {
                            if (parentNode.NodeType == Tokens.Comment)
                            {
                                result.AppendLine("--" + parentNode.NodeValue);
                            }
                            else
                            {
                                string dataType = "";
                                foreach (TreeNode child in parentNode.Children)
                                {
                                    switch (child.NodeType)
                                    {
                                        case Tokens.DataType:
                                            dataType = SpDictionaryOracle.GetValue(child.NodeValue);
                                            if (dataType == "not supported")
                                            {
                                                throw new NotSupportedException("The data type: " + child.NodeValue + " is not supported");
                                            }
                                            break;

                                        case Tokens.Identifier:
                                            //cursors will be declared as global variables
                                            if (dataType != "CURSOR")
                                            {
                                                result.Append(child.NodeValue + " ");
                                                result.Append(dataType);
                                                result.AppendLine(";");
                                            }
                                            else
                                            {
                                                cursors.Add(child.NodeValue, new Cursor());
                                            }
                                            break;
                                    }
                                }
                            }
                        }
                        break;

                    case "ACTIONS":
                        if (!containsAs)
                        {
                            result.AppendLine("AS");
                        }
                        result.AppendLine("BEGIN");
                        result.AppendLine(TranslateBlock(node.Children[0]));
                        if (whenSqlError.Length > 0)
                        {
                            result.AppendLine(whenSqlError.ToString());
                        }
                        if (hasTempTable)
                        {
                            result.AppendLine("SELECT * FROM @tempTable");
                        }

                        //TODO: deallocate all cursors
                        //foreach (Cursor cursor in cursors)
                        //{
                        //    if (cursor.Status != CursorStatus.Deallocated)
                        //    {
                        //        result.Append("DEALLOCATE ");
                        //    }
                        //}

                        result.AppendLine("END;");
                        break;
                }
            }
            return result.ToString();
        }

        private string TranslateBlock(TreeNode blockNode)
        {
            return TranslateBlock(blockNode, BlockRegion.Unspecified);
        }
        
        private string TranslateBlock(TreeNode blockNode, BlockRegion region)
        {
            StringBuilder block = new StringBuilder();
            string expr = "";
            
            foreach (TreeNode node in blockNode.Children)
            {
                switch (node.NodeType)
                {
                    case Tokens.Block:
                        block.AppendLine("BEGIN");
                        block.AppendLine(TranslateBlock(node, region));
                        block.AppendLine("END;");
                        break;
                    case Tokens.Statement:
                        switch (node.Children[0].NodeValue)
                        {
                            case "BREAK":
                                block.AppendLine("BREAK");
                                break;

                            case "CALL": //it can only be one of the Sql functions
                                block.Append(TranslateFunction(node.Children[1]));
                                break;

                            case "IF":
                                bool addThen = true;
                                foreach (TreeNode child in node.Children)
                                {
                                    switch (child.NodeType)
                                    {
                                        case Tokens.Expression:
                                            containsFunction = false;
                                            string expression = TranslateExpression(child);
                                            if (String.IsNullOrEmpty(expression) && block.ToString().EndsWith("IF "))
                                            {
                                                block.Remove(block.Length - 3, 3);
                                                break;
                                            }
                                            block.Append(expression + " ");
                                            //if we have a function inside an IF then we have to remove the condition
                                            //because we don't have a boolean return value anymore (like the Sql* functions have)
                                            if (containsFunction)
                                            {
                                                if (sqlFetchParam.Count > 0)
                                                {
                                                    string condition = sqlFetchParam.Peek() + " = 0 \r\n";
                                                    //change the order: first the translation of the function and then the condition
                                                    block = new StringBuilder(Regex.Replace(block.ToString(), @"(?<if>IF (NOT )?)(?<function>[\s|\S]*)", "${function}${if}" + condition, RegexOptions.IgnoreCase | RegexOptions.Multiline));
                                                }
                                                else
                                                {
                                                    block = new StringBuilder(Regex.Replace(block.ToString(), @"(?<if>IF (NOT )?)(?<function>[\s|\S]*)", "${function}", RegexOptions.IgnoreCase | RegexOptions.Multiline));
                                                    addThen = false;
                                                }
                                                
                                            }
                                            
                                            break;
                                        case Tokens.Block:
                                            Statement statement = (Statement)Enum.Parse(typeof(Statement), child.NodeValue);
                                            if ((statement == Statement.If && addThen) || statement == Statement.ElseIf)
                                            {
                                                block.AppendLine("THEN");
                                            }
                                            block.AppendLine(TranslateBlock(child, region));
                                            break;
                                        default:
                                            if (child.NodeValue == "ELSE")
                                            {
                                                block.AppendLine("ELSE");
                                            }
                                            else
                                            {
                                                block.Append(child.NodeValue + " ");
                                            }
                                            break;
                                    }
                                }
                                block.AppendLine("END IF;");
                                break;

                            case "LOOP":
                                block.AppendLine("LOOP");
                                block.AppendLine("BEGIN");
                                block.AppendLine(TranslateBlock(node.Children[node.Children.Count -1], region));
                                block.AppendLine("END;");
                                break;

                            case "RETURN":
                                expr = TranslateExpression(node.Children[1]);
                                
                                switch (region)
                                {
                                    case BlockRegion.Unspecified:
                                        block.Append("RETURN ");
                                        block.AppendLine(node.Children.Count > 1 ? expr : "");
                                        returnExpr = "";
                                        break;

                                    case BlockRegion.OnProcedureFetch:
                                        //the return expression must be added as condition to the corresponding WHILE statement
                                        returnExpr = expr;
                                        if (onProcFetchReturns > 1)
                                        {
                                            block.Append("returnValue := ");
                                            block.AppendLine(expr);
                                            returnExpr = "returnValue";
                                        }
                                        break;

                                    case BlockRegion.WhenSqlError:
                                        //no return needed
                                        break;
                                }
                                break;

                            case "SET":
                                string ident = node.Children[1].NodeValue;
                                sqlKey = ident;
                                expr = TranslateExpression(node.Children[3]);

                                block.Append(node.Children[1].NodeValue);
                                block.Append(" := ");
                                block.Append(expr);
                                block.AppendLine(";");
                                break;

                            case "TRACE":
                                block.Append("DBMS_OUTPUT.PUT_LINE ");
                                foreach (TreeNode traceNode in node.Children[0].Children)
                                {
                                    if (traceNode.NodeType == Tokens.Identifier)
                                    {
                                        block.Append(traceNode.NodeValue);
                                    }
                                    else if(traceNode.NodeType == Tokens.Comma)
                                    {
                                        block.Append(",");
                                    }
                                }
                                block.AppendLine();
                                break;

                            case "WHEN SQLERROR":
                                //TODO: implement WHEN SQLERROR for SqlServer 2000 (it doesn't support TRY..CATCH)
                                if (true) 
                                {
                                    //have to surround the whole code with BEGIN TRY... END TRY
                                    //and the WHEN SQLERROR part has to be placed inside BEGIN CATCH...END CATCH
                                    //if there is a RETURN TRUE/FALSE then the execution has to continue
                                    //so no RETURN statements has to be added in this case
                                    int index = result.ToString().IndexOf("AS\r\n");
                                    result.Insert(index + 4, "BEGIN TRY\r\n");
                                    
                                    whenSqlError.AppendLine("END TRY");
                                    whenSqlError.AppendLine("BEGIN CATCH");
                                    whenSqlError.AppendLine(TranslateBlock(node.Children[1], BlockRegion.WhenSqlError));
                                    if (tree.FindNode(node.Children[1], "RETURN") == null)
                                    {
                                        //if there is no RETURN then the execution is returned to the caller
                                        //so it needs to be translated with a RETURN ERROR_NUMBER()
                                        whenSqlError.AppendLine("RETURN ERROR_NUMBER()");
                                    }
                                    whenSqlError.AppendLine("END CATCH");
                                }
                                break;

                            case "WHILE":
                                expr = TranslateExpression(node.Children[1]);
                                if (sqlFetch.Count > 0)
                                {
                                    block.Append(expr);
                                    block.Append("WHILE ");
                                    block.Append(sqlFetchParam.Peek());
                                    block.AppendLine(" = 0");
                                    block.AppendLine("BEGIN");
                                    block.AppendLine(TranslateBlock(node.Children[2], region));
                                    block.AppendLine(sqlFetch.Dequeue());
                                    block.AppendLine(SpDictionaryOracle.GetValue("SQLFETCH", sqlFetchParam.Dequeue(), sqlFetchParam.Dequeue()));
                                    block.AppendLine("END;");
                                }
                                else
                                {
                                    block.Append("WHILE ");
                                    block.Append(expr);
                                    block.AppendLine("BEGIN");
                                    block.AppendLine(TranslateBlock(node.Children[2], region));
                                    block.AppendLine("END;");
                                }
                                break;

                            case "ON PROCEDURE":
                                switch (node.Children[1].NodeValue)
                                {
                                    case "STARTUP":
                                    case "EXECUTE":
                                    case "CLOSE":
                                        //ignore the procedure state and execute the block
                                        block.AppendLine(TranslateBlock(node.Children[2]));
                                        break;

                                    case "FETCH":
                                        int index = 0;
                                        //get the number of RETURN statements inside the ON PROCEDURE FETCH
                                        //if there is more than 1 RETURN (most likely inside an IF ELSE)
                                        //then we have to declare an additional variable and use that in the WHILE condition
                                        onProcFetchReturns = tree.FindAll(node.Children[2], "RETURN");
                                        index = result.ToString().IndexOf("BEGIN");
                                        if (onProcFetchReturns > 1)
                                        {
                                            result.Insert(index, "DECLARE returnValue int\r\n");
                                        }
                                        //we also need to declare a temporary table which will contain the processed resultset
                                        //the columns of the table are the output variables
                                        StringBuilder tempTable = new StringBuilder("DECLARE tempTable TABLE ( ");
                                        hasTempTable = true;
                                        foreach (SpVariable var in outputVars)
                                        {
                                            tempTable.Append(var.Name);
                                            tempTable.Append(" ");
                                            tempTable.Append(var.Type);
                                            tempTable.Append(", ");
                                        }
                                        tempTable.Remove(tempTable.Length - 2, 2);
                                        tempTable.AppendLine(" )");
                                        result.Insert(index, tempTable);

                                        block.AppendLine("returnValue := 0");
                                        block.AppendLine("WHILE {returnExpr}");
                                        block.AppendLine("BEGIN");
                                        
                                        block.AppendLine(TranslateBlock(node.Children[2], BlockRegion.OnProcedureFetch));
                                        block.Replace("{returnExpr}", String.IsNullOrEmpty(returnExpr) ? "" : 
                                           returnExpr + " = 0 ");

                                        //insert the fetched values in the temporary tables
                                        block.Append("INSERT INTO tempTable VALUES (");
                                        block.Append(sqlInto);
                                        block.AppendLine(")");

                                        block.AppendLine("END;");

                                        onProcFetchReturns = 0;
                                        break;
                                }
                                break;

                            default:
                                //comment
                                block.Append("--");
                                block.AppendLine(node.Children[0].NodeValue);
                                break;
                        }
                        break;
                }
            }
            return block.ToString();
        }

        private string TranslateFunction(TreeNode node)
        {
            StringBuilder function = new StringBuilder();
            StringCollection parameters = new StringCollection();
            foreach (TreeNode parameter in node.Children)
            {
                sqlKey = "tempKey";
                string param = TranslateExpression(parameter);
                parameters.Add(param);

                //check if the string parameters are sql statements
                //needed because we can use cursors only for queries, not for DML statements
                if (sqlStatements.ContainsKey(param))
                {
                    sqlKey = param;
                }
                if(sqlStatements.ContainsKey(sqlKey))
                {
                    isQuery = sqlStatements[sqlKey].IsQuery;
                }
            }

            string functionName = node.NodeValue;
            switch (functionName)
            {
                case "SQLCLEARIMMEDIATE":
                case "SQLCONNECT":
                    break;

                case "SQLCOMMIT":
                case "SQLERROR":
                    function.AppendLine(SpDictionaryOracle.GetValue(functionName));
                    break;

                case "SQLCLOSE":
                    cursors[parameters[0]].Status = CursorStatus.Deallocated;
                    function.AppendLine(SpDictionaryOracle.GetValue(functionName, parameters[0]));
                    break;

                case "SQLDISCONNECT":
                    if (cursors[parameters[0]].Status != CursorStatus.None)
                    {
                        cursors[parameters[0]].Status = CursorStatus.Closed;
                        function.AppendLine(SpDictionaryOracle.GetValue(functionName, parameters[0]));
                    }
                    break;

                case "SQLOPEN":
                case "SQLEXECUTE":
                    if (!preparedStatements.ContainsKey(parameters[0]))
                    {
                        if (cursors[parameters[0]].Status != CursorStatus.Declared)
                        {
                            //throw new ArgumentException("The cursor: " + parameters[0] + " is not in a valid state");
                        }
                        else
                        {
                            cursors[parameters[0]].Status = CursorStatus.Opened;
                        }
                        function.AppendLine(SpDictionaryOracle.GetValue(functionName, parameters[0]));
                    }
                    else
                    {
                        //function.AppendLine(String.Format("EXECUTE ( {0} )", preparedStatements[parameters[0]]));
                        function.AppendLine(preparedStatements[parameters[0]]);
                    }
                    break;

                case "SQLDROPSTOREDCMD":
                case "SQLGETMODIFIEDROWS":
                case "SQLGETRESULTSETCOUNT":
                case "SQLSETLOCKTIMEOUT":
                    function.AppendLine(SpDictionaryOracle.GetValue(functionName, parameters[1]));
                    break;

                case "SQLEXISTS":
                    function.AppendLine(SpDictionaryOracle.GetValue(functionName, parameters[0], parameters[1]));
                    break;

                case "SQLFETCHNEXT":
                case "SQLFETCHPREVIOUS":
                    function.Append(SpDictionaryOracle.GetValue(functionName, parameters[0]));
                    function.AppendLine(" " + cursors[parameters[0]].Sql.Into);
                    //the sqlInto variable will be used to insert a row in the temporary table for ON PROCEDURE FETCH
                    if (!String.IsNullOrEmpty(cursors[parameters[0]].Sql.Into))
                    {
                        sqlInto = cursors[parameters[0]].Sql.Into.Substring(4);
                    }
                    else
                    {
                        sqlInto = "";
                    }
                    //store the fetch for later use in WHILE
                    sqlFetch.Enqueue(function.ToString());
                    sqlFetchParam.Enqueue(parameters[1]);
                    sqlFetchParam.Enqueue(parameters[0]);
                    //append the set statement
                    function.AppendLine(SpDictionaryOracle.GetValue("SQLFETCH", parameters[0], parameters[1]));
                    break;

                case "SQLFETCHROW":
                    function.Append(SpDictionaryOracle.GetValue(functionName, parameters[0], parameters[1]));
                    function.AppendLine(" " + cursors[parameters[0]].Sql.Into);
                    //store the fetch for later use in WHILE
                    sqlFetch.Enqueue(function.ToString());
                    sqlFetchParam.Enqueue(parameters[2]);
                    //append the set statement
                    function.AppendLine(SpDictionaryOracle.GetValue("SQLFETCH", parameters[2]));
                    break;

                case "SQLGETERRORTEXT":
                    function.AppendLine(SpDictionaryOracle.GetValue(functionName, parameters[1], parameters[0]));
                    break;

                case "SQLPREPARE":
                    if (isQuery)
                    {
                        if (cursors[parameters[0]].Status == CursorStatus.Declared || cursors[parameters[0]].Status == CursorStatus.Opened)
                        {
                            //first deallocate the cursor
                            function.AppendLine("CLOSE " + parameters[0]);
                        }
                        function.AppendLine(SpDictionaryOracle.GetValue(functionName, parameters[0], parameters[1]));
                        cursors[parameters[0]].Status = CursorStatus.Declared;
                        cursors[parameters[0]].Sql = sqlStatements[sqlKey];
                    }
                    else
                    {
                        preparedStatements[parameters[0]] = parameters[1];
                    }
                    break;

                case "SQLPREPAREANDEXECUTE":
                    if (isQuery)
                    {
                        if (cursors[parameters[0]].Status == CursorStatus.Declared || cursors[parameters[0]].Status == CursorStatus.Opened)
                        {
                            //first deallocate the cursor
                            function.AppendLine("CLOSE " + parameters[0]);
                        }
                        function.AppendLine(SpDictionaryOracle.GetValue(functionName, parameters[0], parameters[1]));
                        cursors[parameters[0]].Status = CursorStatus.Opened;
                        cursors[parameters[0]].Sql = sqlStatements[sqlKey];
                    }
                    else
                    {
                        //function.AppendLine(String.Format("EXECUTE ( {0} )", parameters[1]));
                        function.AppendLine(parameters[1]);
                    }
                    break;

                case "SQLIMMEDIATE":
                    //function.AppendLine(SpDictionaryOracle.GetValue(functionName, parameters[0]));
                    //if the statement is a SELECT with an INTO clause then we have to assign the variables directly in the select list
                    //E.g: SELECT col1 FROM table1 INTO :nVar => SELECT nVar = col1 FROM table1
                    string sqlImmediate = parameters[0];
                    if (!String.IsNullOrEmpty(sqlInto))
                    {
                        Regex reg = new Regex(@"^SELECT(.*?)(FROM.*$)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                        Match match = reg.Match(sqlImmediate);
                        string[] selectColumns = match.Groups[1].Value.Split(',');
                        string[] intoVariables = sqlInto.Substring(4).Split(',');
                        function.Append("SELECT ");
                        //if (selectColumns.Length != intoVariables.Length)
                        //{
                        //    throw new NotSupportedException("Into variables must match the column list");
                        //}
                        for(int i = 0; i < intoVariables.Length; i++)
                        {
                            function.Append(intoVariables[i]);
                            function.Append(" = ");
                            function.Append(selectColumns[i]);
                            if (i < intoVariables.Length - 1)
                            {
                                function.Append(", ");
                            }
                        }
                        function.Append(" ");
                        function.Append(match.Groups[2].Value);
                        
                    }
                    break;

                case "SQLRETRIEVE":
                    preparedStatements[parameters[0]] = "'" + parameters[1].Replace('\'', ' ') + parameters[2].Replace('\'', ' ') + "'";
                    //function.AppendLine(SpDictionaryOracle.GetValue("SQLRETRIEVE", parameters[1], parameters[2]));
                    break;

                case "SQLGETERRORPOSITION":
                case "SQLGETPARAMETERALL":
                case "SQLGETROLLBACKFLAG":
                case "SQLSETPARAMETERALL":
                case "SQLSETRESULTSET":
                    throw new NotSupportedException("The function: " + functionName + " is not supported");

                case "SQLGETPARAMETER":
                    switch (parameters[1])
                    {
                        case "DBP_VERSION":
                        case "4":
                            function.AppendLine(SpDictionaryOracle.GetValue("SQLGETPARAMETER2", parameters[2]));
                            break;

                        case "DBP_LOCKWAITTIMEOUT":
                        case "5":
                            function.AppendLine(SpDictionaryOracle.GetValue("SQLGETPARAMETER1", parameters[2]));
                            break;

                        default:
                            throw new NotSupportedException("The system constant: " + parameters[1] + " is not supported");
                    }
                    break;

                //case "SQLSETPARAMETER":
                //    switch (parameters[1])
                //    {
                //        case "DBP_LOCKWAITTIMEOUT":
                //        case "5":
                //            function.AppendLine(SpDictionaryOracle.GetValue("SQLSETPARAMETER", parameters[2]));
                //            break;

                //        default:
                //            throw new NotSupportedException("The system constant: " + parameters[1] + " is not supported");
                //    }
                //    break;

                case "SQLSETISOLATIONLEVEL":
                    string isoLevel = "";
                    switch (parameters[1])
                    {
                        case "RL":
                        case "CS": isoLevel = "READ COMMITTED"; break;
                        case "RR": isoLevel = "REPEATABLE READ"; break;
                        case "RO": isoLevel = "READ UNCOMMITTED"; break;
                        case "SE": isoLevel = "SERIALIZABLE"; break;
                    }
                    function.AppendLine(SpDictionaryOracle.GetValue("SQLSETISOLATIONLEVEL", isoLevel));
                    break;

                case "SQLSTORE":
                    if (isQuery)
                    {
                        function.AppendLine(SpDictionaryOracle.GetValue("SQLSTORE1", parameters[0], parameters[1]));
                    }
                    else
                    {
                        function.AppendLine(SpDictionaryOracle.GetValue("SQLSTORE2", parameters[1]));
                    }
                    break;
            }
            return function.ToString();
        }

        private string TranslateExpression(TreeNode exprNode)
        {
            StringBuilder expr = new StringBuilder();
            
            foreach (TreeNode node in exprNode.Children)
            {
                switch (node.NodeType)
                {
                    case Tokens.Keyword:
                        if (node.NodeValue == "AND" || node.NodeValue == "OR")
                        {
                            expr.Append(" ");
                            expr.Append(node.NodeValue);
                            expr.Append(" ");
                        }
                        break;
                    
                    case Tokens.SysKeyword:
                        string keyword = SpDictionaryOracle.GetValue(node.NodeValue);
                        if (!String.IsNullOrEmpty(keyword))
                        {
                            expr.Append(keyword);
                        }
                        else
                        {
                            //the DBP_XXX and DBV_XXX constants don't have a translation
                            expr.Append(node.NodeValue);
                        }
                        break;

                    case Tokens.Variable:
                    case Tokens.Identifier:
                        expr.Append(node.NodeValue);
                        break;

                    case Tokens.Concatenate:
                        expr.Append(" + ");
                        break;

                    case Tokens.RelatOperator:
                        if (node.NodeValue.In("=", "!="))
                        {
                             int index = exprNode.Children.IndexOf(node);
                             if (exprNode.Children.Count > index + 1 && exprNode.Children[index + 1].NodeValue.In("NUMBER_NULL", "STRING_NULL", "DATETIME_NULL"))
                             {
                                 expr.Append(node.NodeValue == "=" ? " IS " : " IS NOT ");
                             }
                             else
                             {
                                 expr.Append(node.NodeValue == "=" ? " = " : " <> ");
                             }
                        }
                        else
                        {
                            expr.Append(" ");
                            expr.Append(node.NodeValue);
                            expr.Append(" ");
                        }
                        break;

                    case Tokens.Function:
                        string func = TranslateFunction(node);
                        if (!String.IsNullOrEmpty(func))
                        {
                            expr.AppendLine(func);
                            containsFunction = true;
                        }
                        break;

                    case Tokens.Expression:
                        expr.Append(TranslateExpression(node));
                        break;

                    case Tokens.StringConst:
                        //check if it's an sql statement
                        Regex reg = new Regex("SELECT.*FROM.*|INSERT INTO.*|DELETE FROM.*|UPDATE.*SET.*|COMMIT|ROLLBACK", RegexOptions.Singleline|RegexOptions.IgnoreCase);
                        if (reg.IsMatch(node.NodeValue))
                        {
                            sqlInto = "";
                            SqlTranslator.SqlTranslator sqlTrans = new fecher.SqlTranslator.SqlTranslator();
                            string sqlStatement = sqlTrans.TranslateSql(DatabaseBrand.Oracle, node.NodeValue.Substring(1, node.NodeValue.Length - 2));
                            
                            isQuery = sqlStatement.StartsWith("SELECT");
                            //process the translated statement
                            //2. take out the INTO part (has to be moved to the FETCH statement)
                            if (!sqlStatement.StartsWith("INSERT"))
                            {
                                Regex reg1 = new Regex(".*(INTO.*?)(FROM|WHERE|GROUP|HAVING|ORDER|FOR|UNION|$)", RegexOptions.Singleline|RegexOptions.IgnoreCase);
                                Match match = reg1.Match(sqlStatement);
                                if (match.Success)
                                {
                                    sqlInto = match.Groups[1].Value;
                                    sqlStatement = sqlStatement.Replace(sqlInto, "");
                                }
                            }

                            //expr.Append("'");
                            expr.Append(sqlStatement);
                            //expr.Append("'");
                            expr.AppendLine(";");

                            sqlStatements[sqlKey] = new SqlStatement(isQuery, sqlStatement, sqlInto);
                        }
                        else
                        {
                            expr.Append(node.NodeValue);
                        }
                        break;

                    default:
                        expr.Append(node.NodeValue);
                        expr.Append(" ");
                        break;
                }
            }

            return expr.ToString();
        }

        private string CheckForSimpleOnProcedureFetch()
        {
            string sqlStatement = String.Empty;
            //check if the ON PROCEDURE FETCH only contains one SqlFetchNext call and two return statements
            //because in this case the whole procedure can be translated simply with the sql select
            Regex reg = new Regex(@"ON PROCEDURE FETCH\s*IF (NOT)? SQLFETCHNEXT\s*\(.*\)\s*RETURN \d\s*ELSE\s*RETURN \d", RegexOptions.IgnoreCase);
            if (reg.IsMatch(Source))
            {
                //now search for the select
                Match match = Regex.Match(Source, "'(SELECT .*)?'", RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    SqlTranslator.SqlTranslator sqlTrans = new fecher.SqlTranslator.SqlTranslator();
                    sqlStatement = sqlTrans.TranslateSql(DatabaseBrand.Oracle, match.Groups[1].Value);

                    Regex reg1 = new Regex(".*(INTO.*?)(FROM|WHERE|GROUP|HAVING|ORDER|FOR|UNION|$)", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                    match = reg1.Match(sqlStatement);
                    if (match.Success)
                    {
                        sqlStatement = sqlStatement.Replace(match.Groups[1].Value, "");
                    }
                }
            }
            return sqlStatement;
        }
	}
}
