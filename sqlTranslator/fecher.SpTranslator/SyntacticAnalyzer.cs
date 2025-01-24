using System;
using fecher.Common;

namespace fecher.SpTranslator
{
    /// <summary>
    /// Class for constructing the syntax tree
    /// </summary>
    public class SyntacticAnalyzer
    {
        private IToken token = new Token();
        private Tree syntaxTree = new Tree(new TreeNode(Tokens.Root));
        private LexicalAnalyzer lex = new LexicalAnalyzer();

        public Tree Parse(DatabaseBrand sourceDB, string sourceSql)
        {
            if (sourceDB <= DatabaseBrand.SqlBase)
            {
                lex.SourceSql = sourceSql;
                Procedure(syntaxTree.root);
            }
            else
                throw new Exception("Uknown database brand for source database");

            return syntaxTree;
        }

        private void Procedure(TreeNode parent)
        {
            //[STORE identifier]
            //PROCEDURE [:] identifier [STATIC|DYNAMIC] 
            //[PARAMETERS]{[RECEIVE] dataType [:] identifier}*
            //[LOCAL VARIABLES] {dataType [:] identifier}*
            //ACTIONS block
            TreeNode node, temp;

            token = lex.GetToken();
            if (token.LiteralValue == "STORE")
            {
                token = lex.GetToken();
            }
            if (token.LexicalCode == Tokens.Identifier)
            {
                token = lex.GetToken();
            }
            temp = new TreeNode(Tokens.Keyword, token.LiteralValue);
            parent.AddChild(temp);

            token = lex.GetToken();
            if (token.LexicalCode == Tokens.Colon)
            {
                token = lex.GetToken();
            }
            node = new TreeNode(Tokens.Identifier, token.LiteralValue);
            temp.AddChild(node);

            token = lex.GetToken();
            if (token.LiteralValue == "STATIC" || token.LiteralValue == "DYNAMIC")
            {
                node = new TreeNode(Tokens.Keyword, token.LiteralValue);
                temp.AddChild(node);
                token = lex.GetToken();
            }

            if (token.LiteralValue == "PARAMETERS")
            {
                temp = new TreeNode(Tokens.Keyword, token.LiteralValue);
                parent.AddChild(temp);

                token = lex.GetToken();

                while (token.LiteralValue != "LOCAL" && token.LiteralValue != "ACTIONS" && token.LexicalCode != Tokens.Null)
                {
                    if (token.LexicalCode == Tokens.Comment)
                    {
                        temp.AddChild(new TreeNode(Tokens.Comment, token.LiteralValue));
                    }
                    else
                    {
                        node = new TreeNode(Tokens.Parameter);
                        temp.AddChild(node);
                        ParamsAndVars(node);
                    }
                    token = lex.GetToken();
                } 
            }

            if (token.LiteralValue == "LOCAL")
            {
                token = lex.GetToken();
                temp = new TreeNode(Tokens.Keyword, "LOCAL VARIABLES");
                parent.AddChild(temp);

                token = lex.GetToken();

                while (token.LiteralValue != "ACTIONS" && token.LexicalCode != Tokens.Null)
                {
                    if (token.LexicalCode == Tokens.Comment)
                    {
                        temp.AddChild(new TreeNode(Tokens.Comment, token.LiteralValue));
                    }
                    else
                    {
                        node = new TreeNode(Tokens.Variable);
                        temp.AddChild(node);
                        ParamsAndVars(node);
                    }
                    token = lex.GetToken();
                } 
            }

            if (token.LiteralValue == "ACTIONS")
            {
                node = new TreeNode(Tokens.Keyword, "ACTIONS");
                parent.AddChild(node);

                token = lex.GetToken();
                Block(node, Common.Statement.None);
            }
        }

        private void ParamsAndVars(TreeNode parent)
        {
            //[RECEIVE] dataType [:] identifier
            TreeNode node;
            string info = "";

            if (token.LiteralValue == "RECEIVE")
            {
                //node = new TreeNode(Tokens.Keyword, token.LiteralValue);
                //parent.AddChild(node);
                info = "RECEIVE";
                token = lex.GetToken();
            }

            if (token.LiteralValue == "SQL") // Sql Handle type
            {
                token = lex.GetToken();
                node = new TreeNode(Tokens.DataType, "SQL HANDLE");
            }
            else if (token.LiteralValue == "LONG")
            {
                if (lex.Peek().LiteralValue == "STRING")
                {
                    token = lex.GetToken();
                }
                node = new TreeNode(Tokens.DataType, "LONG STRING");
            }
            else
            {
                node = new TreeNode(Tokens.DataType, token.LiteralValue);
            }
            parent.AddChild(node);

            token = lex.GetToken();
            if (token.LexicalCode == Tokens.Colon)
            {
                token = lex.GetToken();
            }
            node = new TreeNode(Tokens.Identifier, token.LiteralValue, info);
            parent.AddChild(node);
        }

        private void Block(TreeNode parent, Statement statement)
        {
            //[BEGIN] {statement}* [END]
            TreeNode node;
            int currentIndentLevel = 0;

            node = new TreeNode(Tokens.Block, statement.ToString());
            parent.AddChild(node);
            parent = node;
            
            if (token.LiteralValue == "BEGIN")
            {
                token = lex.GetToken();
            }

            currentIndentLevel = token.IndentLevel;

            while (token.LiteralValue != "END" && token.LexicalCode != Tokens.Null &&
                   token.LiteralValue != "ELSE" && 
                   (token.IndentLevel == currentIndentLevel || token.LexicalCode == Tokens.Comment))
            {
                node = new TreeNode(Tokens.Statement);
                parent.AddChild(node);
                Statement(node);
            }

            //if the indentification level is greater than the current one then create a new block
            if (token.IndentLevel > currentIndentLevel)
            {
                Block(node, Common.Statement.None);
            }

            if (token.LiteralValue == "END")
            {
                token = lex.GetToken();
            }
        }

        private void Statement(TreeNode parent)
        {
            switch (token.LiteralValue)
            {
                case "BREAK":
                    Break(parent);
                    break;
                case "CALL":
                    Call(parent);
                    break;
                case "IF":
                    If(parent);
                    break;
                case "LOOP":
                    Loop(parent);
                    break;
                case "RETURN":
                    Return(parent);
                    break;
                case "SET":
                    Set(parent);
                    break;
                case "TRACE":
                    Trace(parent);
                    break;
                case "WHEN":
                    WhenSqlError(parent);
                    break;
                case "WHILE":
                    While(parent);
                    break;
                case "ON":
                    OnProcedure(parent);
                    break;
                default:
                    Comment(parent);
                    break;
            }
        }

        private void Break(TreeNode parent)
        {
            //BREAK [identifier]
            TreeNode node;
            node = new TreeNode(Tokens.Keyword, "BREAK");
            parent.AddChild(node);

            token = lex.GetToken();
            if (token.LexicalCode == Tokens.Identifier)
            {
                node = new TreeNode(Tokens.Identifier, token.LiteralValue);
                parent.AddChild(node);
                token = lex.GetToken();
            }

        }

        private void Call(TreeNode parent)
        {
            //CALL function
            TreeNode node;
            node = new TreeNode(Tokens.Keyword, "CALL");
            parent.AddChild(node);

            token = lex.GetToken();
            Function(parent);
        }

        private void If(TreeNode parent)
        {
            //IF condition block [ELSE IF condition block]* [ELSE block]
            TreeNode node;
            node = new TreeNode(Tokens.Keyword, "IF");
            parent.AddChild(node);

            token = lex.GetToken();
            Expression(parent);

            Block(parent, Common.Statement.If);

            while (token.LiteralValue == "ELSE")
            {
                token = lex.GetToken();
                if (token.LiteralValue == "IF")
                {
                    node = new TreeNode(Tokens.Keyword, "ELSE IF");
                    parent.AddChild(node);

                    token = lex.GetToken();
                    Expression(parent);

                    Block(parent, Common.Statement.ElseIf);
                }
                else
                {
                    node = new TreeNode(Tokens.Keyword, "ELSE");
                    parent.AddChild(node);

                    Block(parent, Common.Statement.Else);
                }               
            }
        }

        private void Loop(TreeNode parent)
        {
            //LOOP [identifier] block
            TreeNode node;
            node = new TreeNode(Tokens.Keyword, "LOOP");
            parent.AddChild(node);

            token = lex.GetToken();
            if (token.LexicalCode == Tokens.Identifier)
            {
                node = new TreeNode(Tokens.Identifier, token.LiteralValue);
                token = lex.GetToken();
            }
            Block(parent, Common.Statement.Loop);
        }

        private void Return(TreeNode parent)
        {
            //RETURN expression;
            TreeNode node;
            node = new TreeNode(Tokens.Keyword, "RETURN");
            parent.AddChild(node);

            token = lex.GetToken();
            Expression(parent);
        }
       
        private void Set(TreeNode parent)
        {
            //SET identifier = expression
            TreeNode node;
            node = new TreeNode(Tokens.Keyword, "SET");
            parent.AddChild(node);

            token = lex.GetToken();
            node = new TreeNode(Tokens.Identifier, token.LiteralValue);
            parent.AddChild(node);

            token = lex.GetToken();
            node = new TreeNode(Tokens.RelatOperator, "=");
            parent.AddChild(node);

            token = lex.GetToken();
            Expression(parent);
        }

        private void Trace(TreeNode parent)
        {
            //TRACE {identifier [,]}*
            TreeNode node;
            node = new TreeNode(Tokens.Keyword, "TRACE");
            parent.AddChild(node);
            parent = node;

            token = lex.GetToken();
            while (token.LexicalCode == Tokens.Identifier)
            {
                node = new TreeNode(Tokens.Identifier, token.LiteralValue);
                parent.AddChild(node);

                token = lex.GetToken();
                if (token.LexicalCode == Tokens.Comma)
                {
                    node = new TreeNode(Tokens.Comma, ",");
                    parent.AddChild(node);
                    token = lex.GetToken();
                }
            }
        }

        private void WhenSqlError(TreeNode parent)
        {
            //WHEN SQLERROR block
            TreeNode node;
            token = lex.GetToken();
            node = new TreeNode(Tokens.Keyword, "WHEN SQLERROR");
            parent.AddChild(node);

            token = lex.GetToken();
            Block(parent, Common.Statement.WhenSqlError);
        }

        private void While(TreeNode parent)
        {
            //WHILE expression block
            TreeNode node;
            node = new TreeNode(Tokens.Keyword, "WHILE");
            parent.AddChild(node);

            token = lex.GetToken();
            Expression(parent);
            Block(parent, Common.Statement.While);
        }

        private void OnProcedure(TreeNode parent)
        {
            //ON PROCEDURE STARTUP|EXECUTE|FETCH|CLOSE block
            TreeNode node;
            token = lex.GetToken();
            node = new TreeNode(Tokens.Keyword, "ON PROCEDURE");
            parent.AddChild(node);

            token = lex.GetToken();
            node = new TreeNode(Tokens.Keyword, token.LiteralValue);
            parent.AddChild(node);

            token = lex.GetToken();
            Block(parent, Common.Statement.None);
        }

        private void Comment(TreeNode parent)
        {
            //! text
            if (token.LexicalCode == Tokens.Comment)
            {
                parent.AddChild(new TreeNode(token.LexicalCode, token.LiteralValue));
                token = lex.GetToken();
            }
        }

        private void Function(TreeNode parent)
        {
            //identifier ( {expression [,]}* )
            TreeNode node;
            node = new TreeNode(Tokens.Function, token.LiteralValue);
            parent.AddChild(node);

            parent = node;

            token = lex.GetToken(); //skip over paranthesis
            while (token.LexicalCode != Tokens.RightParant)
            {
                token = lex.GetToken();
                Expression(parent);

                if (token.LexicalCode != Tokens.Comma)
                {
                    break;
                }
            }
            token = lex.GetToken(); //skip over paranthesis
        }

        private void Expression(TreeNode parent)
        {
            //{constant|identifier|function|syskeyword|(expression) [operator]}*
            TreeNode node;
            node = new TreeNode(Tokens.Expression);
            parent.AddChild(node);
            parent = node;

            while ((token.LexicalCode != Tokens.Keyword ||
                  (token.LiteralValue == "AND" || token.LiteralValue == "OR" || token.LiteralValue == "NOT")) &&
                  (token.LexicalCode != Tokens.Comma && token.LexicalCode != Tokens.RightParant) &&
                   token.LexicalCode != Tokens.Comment && token.LexicalCode != Tokens.Null)
            {
                if (token.LexicalCode == Tokens.LeftParant)
                {
                    node = new TreeNode(Tokens.LeftParant, "(");
                    parent.AddChild(node);
                    token = lex.GetToken();
                    Expression(parent);
                    if (token.LexicalCode == Tokens.RightParant)
                    {
                        node = new TreeNode(Tokens.RightParant, ")");
                        parent.AddChild(node);
                        token = lex.GetToken();
                    }
                }
                else if (token.LexicalCode == Tokens.Function)
                {
                    Function(parent);
                }

                else
                {
                    node = new TreeNode(token.LexicalCode, token.LiteralValue);
                    parent.AddChild(node);
                    token = lex.GetToken();
                }
            }
        }
    }
}
