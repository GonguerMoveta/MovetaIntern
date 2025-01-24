using System;
using fecher.Common;
using System.Text;
using System.Collections.Generic;

namespace fecher.SqlTranslator
{
    /// <summary>
    /// Class for constructing the syntax tree
    /// </summary>
    public class SyntacticAnalyzerInformix : ISyntacticAnalyzer
    {
        private IToken token = new Token();
        private Tree syntaxTree = new Tree(new TreeNode(Tokens.Root));
        private LexicalAnalyzerInformix lex = new LexicalAnalyzerInformix();
        private Stack<TreeNode> subSelect = new Stack<TreeNode>();

        private enum ExpressionRegion
        {
            Select,
            Other
        }

        public Tree Parse(string sourceSql)
        {         
            lex.SourceSql = sourceSql;
            if (!sourceSql.Contains(DatabaseSettings.Default.Separator))
            {
                token = lex.GetToken();
                switch (token.LiteralValue)
                {
                    case "SELECT": SelectStatement(syntaxTree.root); break;
                    case "INSERT": InsertStatement(syntaxTree.root); break;
                    case "UPDATE": UpdateStatement(syntaxTree.root); break;
                    case "DELETE": DeleteStatement(syntaxTree.root); break;
                    case "CREATE": CreateStatement(syntaxTree.root); break;
                    case "COMMIT": CommitStatement(syntaxTree.root); break;
                    case "DROP": DropStatement(syntaxTree.root); break;
                    case "ROLLBACK": RollbackStatement(syntaxTree.root); break;
                    case "SAVEPOINT": SavepointStatement(syntaxTree.root); break;
                    case "ALTER": AlterStatement(syntaxTree.root); break;
                    case "REVOKE": RevoleStatement(syntaxTree.root); break;
                    case "GRANT": GrantStatement(syntaxTree.root); break;
                    case "STORE": StoreStatement(syntaxTree.root); break;
                    case "LOCK": LockStatement(syntaxTree.root); break;
                    default:
                        {
                            OnAnalyzeError(); break;
                        }//error: Unknown statement 
                }
            }
            else
            {
                OnAnalyzeError();
            }
            if (token.LexicalCode != Tokens.Null)
            {
                if (syntaxTree.root.Children[0].Children[0].NodeValue.StartsWith("DROP"))
                {
                    token = lex.GetToken();
                    if (token.LexicalCode != Tokens.Null)
                    {
                        OnAnalyzeError();
                    }
                }
                else
                {
                    OnAnalyzeError();
                }
            }

            return syntaxTree;
        }

        void SelectStatement(TreeNode parent)
        {
            //The SELECT and FROM clauses must pe present;
            //The other clauses are optional but there must appear in order;
            //Exception is the INTO clause, which can appear anywhere in the statement
            TreeNode node = new TreeNode(Tokens.Select);
            parent.AddChild(node);

            bool insideSubSelect = subSelect.Count > 0;

            Select(node);

            if (token.LiteralValue == "INTO")
                Into(node);

            if (token.LiteralValue == "FROM")
                From(node);
            //else error: Keyword "FROM" expected

            if (token.LiteralValue == "INTO")
                Into(node);

            if (token.LiteralValue == "WHERE")
                WhereSelect(node);

            if (token.LiteralValue == "INTO" && (!insideSubSelect || (insideSubSelect && subSelect.Count > 0)))
                Into(node);

            if (token.LiteralValue == "GROUP" && (!insideSubSelect || (insideSubSelect && subSelect.Count > 0)))
                GroupBy(node);

            if (token.LiteralValue == "INTO" && (!insideSubSelect || (insideSubSelect && subSelect.Count > 0)))
                Into(node);

            if (token.LiteralValue == "HAVING" && (!insideSubSelect || (insideSubSelect && subSelect.Count > 0)))
                Having(node);

            if (token.LiteralValue == "INTO" && (!insideSubSelect || (insideSubSelect && subSelect.Count > 0)))
                Into(node);

            //ORDERY BY and UNION are not allowed in a subselect
            if (!insideSubSelect)
            {
                if (token.LiteralValue == "ORDER")
                    OrderBy(node);

                if (token.LiteralValue == "INTO")
                    Into(node);

                if (token.LiteralValue == "FOR")
                    ForUpdateOf(node);

                if (token.LiteralValue == "INTO")
                    Into(node);

                if (token.LiteralValue == "UNION")
                    Union(syntaxTree.root);

                if (token.LiteralValue == "INTO")
                    Into(node);
            }
        }


        void InsertStatement(TreeNode parent)
        {
            TreeNode node = new TreeNode(Tokens.Insert);
            parent.AddChild(node);

            Insert(node);

            if (token.LiteralValue == "VALUES")
                Values(node);

            else if (token.LiteralValue == "SELECT")
                SelectStatement(node);
            else
            {
                OnAnalyzeError();
            }
            //else error: Keyword "VALUES" or "SELECT" expected
        }

        void AlterStatement(TreeNode parent)
        {
            TreeNode node = new TreeNode(Tokens.Alter);
            parent.AddChild(node);

            AlterTable(node);
        }

        void GrantStatement(TreeNode parent)
        {
            // GRANT node
            TreeNode grantNode = new TreeNode(Tokens.Grant, "GRANT");
            parent.AddChild(grantNode);
            token = lex.GetToken();
            TreeNode node;

            switch (token.LiteralValue)
            {
                case "DBA":
                case "RESOURCE":
                case "SELECT":
                case "INSERT":
                case "ALTER":
                case "INDEX":
                case "DELETE":
                case "ALL":
                    {
                        //// ON node
                        //node = new TreeNode(Tokens.Keyword, token.LiteralValue);
                        //grantNode.AddChild(node);
                        //token = lex.GetToken();

                        //all resource nodes until TO node is found
                        while ((token.LexicalCode == Tokens.SysKeyword || token.LexicalCode == Tokens.Keyword) && token.LiteralValue != "ON")
                        {
                            node = new TreeNode(token.LexicalCode, token.LiteralValue);
                            grantNode.AddChild(node);
                            token = lex.GetToken();

                            if (token.LexicalCode == Tokens.Comma)
                            {
                                node = new TreeNode(Tokens.Comma, ",");
                                grantNode.AddChild(node);
                                token = lex.GetToken();
                            }
                        }

                        // ON node
                        node = new TreeNode(Tokens.Keyword, token.LiteralValue);
                        grantNode.AddChild(node);
                        token = lex.GetToken();

                        //all resource nodes until TO node is found
                        while ((token.LexicalCode == Tokens.SysKeyword || token.LexicalCode == Tokens.Identifier) && token.LiteralValue != "TO")
                        {
                            node = new TreeNode(Tokens.Name, token.LiteralValue);
                            grantNode.AddChild(node);
                            token = lex.GetToken();

                            if (token.LexicalCode == Tokens.Comma)
                            {
                                node = new TreeNode(Tokens.Comma, ",");
                                grantNode.AddChild(node);
                                token = lex.GetToken();
                            }
                        }

                        //add TO node
                        node = new TreeNode(Tokens.Keyword, token.LiteralValue);
                        grantNode.AddChild(node);
                        token = lex.GetToken();

                        //ad to whom nodes
                        while (token.LexicalCode == Tokens.SysKeyword || token.LexicalCode == Tokens.Keyword || token.LexicalCode == Tokens.StringConst || token.LexicalCode == Tokens.Identifier)
                        {
                            node = new TreeNode(Tokens.Identifier, token.LiteralValue);
                            grantNode.AddChild(node);
                            token = lex.GetToken();

                            if (token.LexicalCode == Tokens.Comma)
                            {
                                node = new TreeNode(Tokens.Comma, ",");
                                grantNode.AddChild(node);
                                token = lex.GetToken();
                            }
                        }

                        break;
                    }
                case "CONNECT":
                    {
                        node = new TreeNode(Tokens.Keyword, token.LiteralValue);
                        grantNode.AddChild(node);
                        token = lex.GetToken();

                        //TO node
                        node = new TreeNode(Tokens.Keyword, token.LiteralValue);
                        grantNode.AddChild(node);
                        token = lex.GetToken();

                        //all connect to nodes
                        while ((token.LexicalCode == Tokens.Identifier || token.LexicalCode == Tokens.StringConst) && token.LiteralValue != "IDENTIFIED")
                        {
                            node = new TreeNode(Tokens.Identifier, token.LiteralValue);
                            grantNode.AddChild(node);
                            token = lex.GetToken();

                            if (token.LexicalCode == Tokens.Comma)
                            {
                                node = new TreeNode(Tokens.Comma, ",");
                                grantNode.AddChild(node);
                                token = lex.GetToken();
                            }
                        }

                        //add IDENTIFIED node
                        node = new TreeNode(Tokens.Keyword, token.LiteralValue);
                        grantNode.AddChild(node);
                        token = lex.GetToken();

                        //add BY node
                        node = new TreeNode(Tokens.Keyword, token.LiteralValue);
                        grantNode.AddChild(node);
                        token = lex.GetToken();

                        //password nodes
                        while (token.LexicalCode == Tokens.Identifier || token.LexicalCode == Tokens.StringConst)
                        {
                            node = new TreeNode(Tokens.Identifier, token.LiteralValue);
                            if (!node.NodeValue.StartsWith("\"") && !node.NodeValue.EndsWith("\""))
                            {
                                node.NodeValue = "\"" + node.NodeValue + "\"";
                            }
                            grantNode.AddChild(node);
                            token = lex.GetToken();

                            if (token.LexicalCode == Tokens.Comma)
                            {
                                node = new TreeNode(Tokens.Comma, ",");
                                grantNode.AddChild(node);
                                token = lex.GetToken();
                            }
                        }

                        break;
                    }
            }
        }

        void LockStatement(TreeNode parent)
        {
            token = lex.GetToken();

            if (token.LiteralValue == "TABLE")
            {
                TreeNode lockNode = new TreeNode(Tokens.Lock, "LOCK TABLE");
                parent.AddChild(lockNode);

                token = lex.GetToken();
                lockNode.AddChild(new TreeNode(Tokens.Identifier, token.LiteralValue));

                token = lex.GetToken();
            }
        }

        void RevoleStatement(TreeNode parent)
        {
            TreeNode revokeNode = new TreeNode(Tokens.Revoke);
            parent.AddChild(revokeNode);

            TreeNode node = new TreeNode(Tokens.Keyword, "REVOKE");
            revokeNode.AddChild(node);
            token = lex.GetToken();
            switch (token.LiteralValue)
            {
                case "SYSADM":
                case "DBA":
                case "RESOURCE":
                case "CONNECT":
                    {
                        node = new TreeNode(Tokens.Keyword, token.LiteralValue);
                        revokeNode.AddChild(node);
                        token = lex.GetToken();

                        while (token.LexicalCode == Tokens.SysKeyword)
                        {
                            node = new TreeNode(Tokens.Keyword, token.LiteralValue);
                            revokeNode.AddChild(node);
                            token = lex.GetToken();

                            if (token.LexicalCode == Tokens.Comma)
                            {
                                node = new TreeNode(Tokens.Comma, ",");
                                revokeNode.AddChild(node);
                                token = lex.GetToken();
                            }
                        }

                        if (token.LiteralValue == "FROM")
                        {
                            node = new TreeNode(Tokens.Keyword, token.LiteralValue);
                            revokeNode.AddChild(node);
                            token = lex.GetToken();
                            if (token.LexicalCode == Tokens.Identifier)
                            {
                                while (token.LexicalCode == Tokens.Identifier)
                                {
                                    node = new TreeNode(Tokens.Keyword, token.LiteralValue);
                                    revokeNode.AddChild(node);
                                    token = lex.GetToken();

                                    if (token.LexicalCode == Tokens.Comma)
                                    {
                                        node = new TreeNode(Tokens.Comma, ",");
                                        revokeNode.AddChild(node);
                                        token = lex.GetToken();
                                    }
                                }
                            }
                            else
                            {
                                OnAnalyzeError();
                            }
                        }
                        else
                        {
                            OnAnalyzeError();
                        }
                        break;
                    }
                case "ALL":
                case "SELECT":
                case "INSERT":
                case "DELETE":
                case "INDEX":
                case "ALTER":
                case "UPDATE":
                case "EXECUTE":
                    {
                        node = new TreeNode(Tokens.Keyword, token.LiteralValue);
                        revokeNode.AddChild(node);
                        token = lex.GetToken();

                        if (node.NodeValue == "UPDATE")
                        {
                            //left paranthesis
                            if (token.LexicalCode == Tokens.LeftParant)
                            {
                                node = new TreeNode(Tokens.LeftParant, token.LiteralValue);
                                revokeNode.AddChild(node);
                                token = lex.GetToken();
                            }

                            //Columns
                            while (token.LexicalCode == Tokens.Identifier)
                            {
                                node = new TreeNode(Tokens.Identifier, token.LiteralValue);
                                revokeNode.AddChild(node);
                                token = lex.GetToken();

                                if (token.LexicalCode == Tokens.Comma)
                                {
                                    node = new TreeNode(Tokens.Comma, ",");
                                    revokeNode.AddChild(node);
                                    token = lex.GetToken();
                                }
                            }

                            //right paranthesis
                            if (token.LexicalCode == Tokens.RightParant)
                            {
                                node = new TreeNode(Tokens.RightParant, token.LiteralValue);
                                revokeNode.AddChild(node);
                                token = lex.GetToken();
                            }
                        }
                        else
                        {
                            while (token.LexicalCode == Tokens.SysKeyword)
                            {
                                node = new TreeNode(Tokens.Keyword, token.LiteralValue);
                                revokeNode.AddChild(node);
                                token = lex.GetToken();

                                if (token.LexicalCode == Tokens.Comma)
                                {
                                    node = new TreeNode(Tokens.Comma, ",");
                                    revokeNode.AddChild(node);
                                    token = lex.GetToken();
                                }
                            }
                        }

                        if (token.LiteralValue == "ON")
                        {
                            node = new TreeNode(Tokens.Keyword, token.LiteralValue);
                            revokeNode.AddChild(node);
                            token = lex.GetToken();
                            //The tables for which the roles neet to be revoked
                            while (token.LexicalCode == Tokens.Identifier)
                            {
                                node = new TreeNode(Tokens.Table, token.LiteralValue);
                                revokeNode.AddChild(node);
                                token = lex.GetToken();

                                if (token.LexicalCode == Tokens.Comma)
                                {
                                    node = new TreeNode(Tokens.Comma, ",");
                                    revokeNode.AddChild(node);
                                    token = lex.GetToken();
                                }
                            }
                            if (token.LiteralValue == "FROM")
                            {
                                node = new TreeNode(Tokens.Keyword, token.LiteralValue);
                                revokeNode.AddChild(node);
                                token = lex.GetToken();
                                //PUBLIC for example
                                if (token.LexicalCode == Tokens.Keyword)
                                {
                                    while (token.LexicalCode == Tokens.Keyword)
                                    {
                                        node = new TreeNode(Tokens.Keyword, token.LiteralValue);
                                        revokeNode.AddChild(node);
                                        token = lex.GetToken();

                                        if (token.LexicalCode == Tokens.Comma)
                                        {
                                            node = new TreeNode(Tokens.Comma, ",");
                                            revokeNode.AddChild(node);
                                            token = lex.GetToken();
                                        }
                                    }
                                }
                                //The users for which the roles need to be revoked
                                if (token.LexicalCode == Tokens.Identifier)
                                {
                                    while (token.LexicalCode == Tokens.Identifier)
                                    {
                                        node = new TreeNode(Tokens.Identifier, token.LiteralValue);
                                        revokeNode.AddChild(node);
                                        token = lex.GetToken();

                                        if (token.LexicalCode == Tokens.Comma)
                                        {
                                            node = new TreeNode(Tokens.Comma, ",");
                                            revokeNode.AddChild(node);
                                            token = lex.GetToken();
                                        }
                                    }
                                }
                                else
                                {
                                    OnAnalyzeError();
                                }
                            }
                        }
                        else
                        {
                            OnAnalyzeError();
                        }
                        break;
                    }
            }
        }


        void UpdateStatement(TreeNode parent)
        {
            TreeNode node = new TreeNode(Tokens.Update);
            parent.AddChild(node);

            if (lex.Peek().LiteralValue == "STATISTICS")
            {
                UpdateStatistics(node);
            }
            else
            {
                Update(node);
            }

            if (token.LiteralValue == "SET")
            {
                Set(node);
            }
            if (token.LiteralValue == "WHERE")
                WhereUpdate(node);
        }


        void DeleteStatement(TreeNode parent)
        {
            TreeNode node = new TreeNode(Tokens.Delete);
            parent.AddChild(node);

            Delete(node);

            if (token.LiteralValue == "WHERE")
                WhereDelete(node);
        }


        void CreateStatement(TreeNode parent)
        {
            parent.NodeInfo = "CREATE";

            TreeNode node = new TreeNode(Tokens.Create);
            parent.AddChild(node);

            token = lex.GetToken();
            switch (token.LiteralValue)
            {
                case "TABLE":
                case "TEMP":
                    CreateTable(node); break;
                case "VIEW":
                    CreateView(node); break;
                case "INDEX":
                case "UNIQUE":
                case "CLUSTERED":
                    CreateIndex(node); break;
                case "PUBLIC":
                case "SYNONYM":
                    CreateSynonym(node); break;
                case "USER":
                    CreateUser(node); break;
                default: break; //error
            }

        }


        void CommitStatement(TreeNode parent)
        {
            TreeNode node = new TreeNode(Tokens.Commit);
            parent.AddChild(node);

            Commit(node);
        }


        void Commit(TreeNode parent)
        {
            // COMMIT [WORK] [TRANSACTION id FORCE]
            TreeNode node = new TreeNode(Tokens.Keyword, token.LiteralValue);
            parent.AddChild(node);
            token = lex.GetToken();

            if (token.LiteralValue == "WORK")
            {
                node = new TreeNode(Tokens.Keyword, token.LiteralValue);
                parent.AddChild(node);
                token = lex.GetToken();
            }

            if (token.LiteralValue == "TRANSACTION")
            {
                node = new TreeNode(Tokens.Keyword, token.LiteralValue);
                parent.AddChild(node);
                token = lex.GetToken();

                if (token.LexicalCode == Tokens.Identifier)
                {
                    node = new TreeNode(Tokens.Identifier, token.LiteralValue);
                    parent.AddChild(node);
                    token = lex.GetToken();
                }

                if (token.LiteralValue == "FORCE")
                {
                    node = new TreeNode(Tokens.Keyword, token.LiteralValue);
                    parent.AddChild(node);
                }
            }
        }

        void RollbackStatement(TreeNode parent)
        {
            TreeNode node = new TreeNode(Tokens.Rollback);
            parent.AddChild(node);

            Rollback(node);
        }


        void Rollback(TreeNode parent)
        {
            // ROLLBACK [savepoint] [TRANSACTION id FORCE]
            TreeNode node = new TreeNode(Tokens.Keyword, token.LiteralValue);
            parent.AddChild(node);
            token = lex.GetToken();

            if (token.LexicalCode == Tokens.Identifier)
            {
                node = new TreeNode(Tokens.Identifier, token.LiteralValue);
                parent.AddChild(node);
                token = lex.GetToken();
            }

            if (token.LiteralValue == "TRANSACTION")
            {
                node = new TreeNode(Tokens.Keyword, token.LiteralValue);
                parent.AddChild(node);
                token = lex.GetToken();

                if (token.LexicalCode == Tokens.Identifier)
                {
                    node = new TreeNode(Tokens.Identifier, token.LiteralValue);
                    parent.AddChild(node);
                    token = lex.GetToken();
                }
                else
                {
                    OnAnalyzeError();
                }

                if (token.LiteralValue == "FORCE")
                {
                    node = new TreeNode(Tokens.Keyword, token.LiteralValue);
                    parent.AddChild(node);
                }
            }
        }

        void SavepointStatement(TreeNode parent)
        {
            TreeNode node = new TreeNode(Tokens.Savepoint);
            parent.AddChild(node);

            Savepoint(node);
        }

        void Savepoint(TreeNode parent)
        {
            // SAVEPOINT id
            TreeNode node = new TreeNode(Tokens.Keyword, token.LiteralValue);
            parent.AddChild(node);
            token = lex.GetToken();

            if (token.LexicalCode == Tokens.Identifier)
            {
                node = new TreeNode(Tokens.Identifier, token.LiteralValue);
                parent.AddChild(node);
            }
            else
            {
                OnAnalyzeError();
            }
        }

        void DropStatement(TreeNode parent)
        {

            TreeNode node = new TreeNode(Tokens.Drop);
            parent.AddChild(node);

            token = lex.GetToken();
            switch (token.LiteralValue)
            {
                case "TABLE": DropTable(node); break;
                case "VIEW": DropView(node); break;
                case "INDEX": DropIndex(node); break;
                case "PUBLIC":
                case "SYNONYM": DropSynonym(node); break;
                case "TRIGGER": DropTrigger(node); break;
                default: break; //error
            }
        }


        void DropTable(TreeNode parent)
        {
            //DROP TABLE ident
            TreeNode node = new TreeNode(Tokens.Keyword, "DROP TABLE");
            parent.AddChild(node);

            token = lex.GetToken();

            if (token.LexicalCode == Tokens.Identifier)
            {
                node = new TreeNode(Tokens.Table, token.LiteralValue);
                parent.AddChild(node);
            }
        }


        void DropView(TreeNode parent)
        {
            //DROP VIEW ident
            TreeNode node = new TreeNode(Tokens.Keyword, "DROP VIEW");
            parent.AddChild(node);

            token = lex.GetToken();

            if (token.LexicalCode == Tokens.Identifier)
            {
                node = new TreeNode(Tokens.View, token.LiteralValue);
                parent.AddChild(node);
            }
        }


        void DropIndex(TreeNode parent)
        {
            //DROP INDEX ident
            TreeNode node = new TreeNode(Tokens.Keyword, "DROP INDEX");
            parent.AddChild(node);

            token = lex.GetToken();

            if (token.LexicalCode == Tokens.Identifier)
            {
                node = new TreeNode(Tokens.Name, token.LiteralValue);
                parent.AddChild(node);
            }
        }

        void DropTrigger(TreeNode parent)
        {
            //DROP TRIGGER name
            TreeNode node = new TreeNode(Tokens.Keyword, "DROP TRIGGER");
            parent.AddChild(node);

            token = lex.GetToken();

            if (token.LexicalCode == Tokens.Identifier)
            {
                node = new TreeNode(Tokens.Name, token.LiteralValue);
                parent.AddChild(node);
            }
        }

        void DropSynonym(TreeNode parent)
        {
            //DROP [PUBLIC] SYNONYM ident [FOR TABLE|EXTERNAL FUNCTION|COMMAND|PROCEDURE]
            TreeNode node;
            if (token.LiteralValue == "PUBLIC")
            {
                token = lex.GetToken();
                node = new TreeNode(Tokens.Keyword, "DROP PUBLIC SYNONYM");
            }
            else
            {
                node = new TreeNode(Tokens.Keyword, "DROP SYNONYM");
            }
            parent.AddChild(node);

            token = lex.GetToken();

            node = new TreeNode(Tokens.Name, token.LiteralValue);
            parent.AddChild(node);

            token = lex.GetToken();
            if (token.LiteralValue == "FOR")
            {
                token = lex.GetToken();
                if (token.LiteralValue == "EXTERNAL")
                {
                    token = lex.GetToken();
                    node = new TreeNode(Tokens.Keyword, "FOR EXTERNAL FUNCTION");
                }
                else
                {
                    node = new TreeNode(Tokens.Keyword, "FOR " + token.LiteralValue);
                }
                parent.AddChild(node);
            }
        }

        void CreateTable(TreeNode parent)
        {
            // CREATE [TEMP] TABLE table ( {col data_type [NOT NULL| NOT NULL WITH DEFAULT | NOT NULL AUTO_INCREMENT(SEED,STEP)]}*
            // [PRIMARY KEY ( {col[,]}* )]
            // [FOREIGN KEY [key_name] ( {col[,]}* ) REFERENCES par_table [ON DELETE RESTRICT | CASCADE | SET NULL] ])
            // [IN [db_name] tblspace_name | IN DATABASE db_name]

            TreeNode node;
            TreeNode temp;
            if (token.LiteralValue == "TEMP")
            {
                temp = new TreeNode(Tokens.Keyword, "CREATE TEMP TABLE");
                token = lex.GetToken();
            }
            else
            {
                temp = new TreeNode(Tokens.Keyword, "CREATE TABLE");
            }
            parent.AddChild(temp);

            token = lex.GetToken();

            //if (token.LexicalCode == Tokens.Identifier) //table
            {
                node = new TreeNode(Tokens.Table, token.LiteralValue);
                temp.AddChild(node);
                token = lex.GetToken();
            }
            if (token.LexicalCode == Tokens.LeftParant) //'('
            {
                node = new TreeNode(Tokens.LeftParant, token.LiteralValue);
                temp.AddChild(node);
            }

            do
            {
                token = lex.GetToken();

                if (token.LiteralValue != "PRIMARY" && token.LiteralValue != "FOREIGN")
                {
                    node = new TreeNode(Tokens.Column, token.LiteralValue);
                    temp.AddChild(node);
                    token = lex.GetToken();
                }

                // data_type_keyword [(int_const [, int_const])]
                if (token.LexicalCode == Tokens.DataTypeKeyword)
                {
                    DataType(temp);
                }

                if (token.LexicalCode == Tokens.Comma)
                {
                    node = new TreeNode(Tokens.Comma, token.LiteralValue);
                    temp.AddChild(node);
                }

            } while (token.LexicalCode == Tokens.Comma);


            if (token.LiteralValue == "PRIMARY")
            {
                token = lex.GetToken();
                PrimaryKey(parent);
            }

            if (token.LexicalCode == Tokens.Comma)
            {
                node = new TreeNode(Tokens.Comma, token.LiteralValue);
                parent.AddChild(node);
                token = lex.GetToken();
            }

            if (token.LiteralValue == "FOREIGN")
            {
                token = lex.GetToken();
                ForeignKey(parent);
            }

            if (token.LexicalCode == Tokens.RightParant)
            {
                node = new TreeNode(Tokens.RightParant, token.LiteralValue);
                parent.AddChild(node);
                token = lex.GetToken();
            }
            else
            {
                OnAnalyzeError();
            }
            // [IN [db_name] tblspace_name | IN DATABASE db_name]
            if (token.LiteralValue == "IN")
            {
                token = lex.GetToken();
                if (token.LiteralValue == "DATABASE")
                {
                    temp = new TreeNode(Tokens.Keyword, "IN DATABASE", "CREATE");
                    parent.AddChild(temp);
                    token = lex.GetToken();
                }
                else
                {
                    temp = new TreeNode(Tokens.Keyword, "IN", "CREATE");
                    parent.AddChild(temp);
                }

                if (token.LexicalCode == Tokens.Identifier)
                {
                    node = new TreeNode(Tokens.Database, token.LiteralValue);
                    temp.AddChild(node);
                    token = lex.GetToken();
                }

                if (token.LexicalCode == Tokens.Identifier)
                {
                    node = new TreeNode(Tokens.Table, token.LiteralValue);
                    temp.AddChild(node);
                    token = lex.GetToken();
                }
            }

            //[PCTFREE int_const]
            if (token.LiteralValue == "PCTFREE")
            {
                temp = new TreeNode(Tokens.Keyword, token.LiteralValue);
                parent.AddChild(temp);
                token = lex.GetToken();
                if (token.LexicalCode == Tokens.NumericConst)
                {
                    node = new TreeNode(Tokens.NumericConst, token.LiteralValue);
                    temp.AddChild(node);
                    token = lex.GetToken();
                }
            }

            //asz:skip "lock mode row" at end
            if (token.LexicalCode == Tokens.Keyword && token.LiteralValue == "LOCK")
            {
                token = lex.GetToken();
                if (token.LexicalCode == Tokens.Keyword && token.LiteralValue == "MODE")
                {
                    token = lex.GetToken();
                    if (token.LexicalCode == Tokens.Keyword && token.LiteralValue == "ROW")
                    {
                        token = lex.GetToken();
                    }
                }
            }
        }

        void CreateUser(TreeNode parent)
        {
            TreeNode createNode = new TreeNode(Tokens.Grant, "CREATE USER");
            parent.AddChild(createNode);
            token = lex.GetToken();
            TreeNode node;

            //user name node
            node = new TreeNode(Tokens.Keyword, token.LiteralValue);
            createNode.AddChild(node);
            token = lex.GetToken();

            //IDENTIFIED node
            node = new TreeNode(Tokens.Keyword, token.LiteralValue);
            createNode.AddChild(node);
            token = lex.GetToken();

            //BY node
            node = new TreeNode(Tokens.Keyword, token.LiteralValue);
            createNode.AddChild(node);
            token = lex.GetToken();

            //password node
            node = new TreeNode(Tokens.Keyword, token.LiteralValue);
            createNode.AddChild(node);
            token = lex.GetToken();
        }

        void CreateView(TreeNode parent)
        {
            // CREATE VIEW view [({col}*)] AS select [WITH CHECK OPTION]
            TreeNode node = new TreeNode(Tokens.Keyword, "CREATE VIEW");
            parent.AddChild(node);
            parent = node;

            token = lex.GetToken();
            string viewName = "";
            if(token.LexicalCode == Tokens.StringConst) //owner
            {
                viewName = token.LiteralValue;
                token = lex.GetToken();
                if (token.LexicalCode == Tokens.Dot)
                {
                    viewName += token.LiteralValue;
                    token = lex.GetToken();
                }
            }
            //if (token.LexicalCode == Tokens.Identifier) //view
            {
                node = new TreeNode(Tokens.View, viewName + token.LiteralValue);
                parent.AddChild(node);
                token = lex.GetToken();
            }

            if (token.LexicalCode == Tokens.LeftParant) //'('
            {
                node = new TreeNode(Tokens.LeftParant, token.LiteralValue);
                parent.AddChild(node);
                token = lex.GetToken();
            }

            while ((token.LexicalCode == Tokens.Identifier
                || token.LexicalCode == Tokens.StringConst
                || token.LexicalCode == Tokens.Keyword) && token.LiteralValue != "AS")
            {
                node = new TreeNode(Tokens.Column, token.LiteralValue);
                parent.AddChild(node);
                token = lex.GetToken();

                if (token.LexicalCode == Tokens.Comma)
                {
                    node = new TreeNode(Tokens.Comma, token.LiteralValue);
                    parent.AddChild(node);
                    token = lex.GetToken();
                }
            }

            if (token.LexicalCode == Tokens.RightParant) //')'
            {
                node = new TreeNode(Tokens.RightParant, token.LiteralValue);
                parent.AddChild(node);
                token = lex.GetToken();
            }

            if (token.LiteralValue == "AS")
            {
                node = new TreeNode(Tokens.Keyword, token.LiteralValue);
                parent.AddChild(node);
                token = lex.GetToken();

                if (token.LiteralValue == "SELECT")
                {
                    SelectStatement(node);
                }
            }

            //[WITH CHECK OPTION]
            if (token.LiteralValue == "WITH")
            {
                token = lex.GetToken();
                if (token.LiteralValue == "CHECK")
                {
                    token = lex.GetToken();
                    if (token.LiteralValue == "OPTION")
                    {
                        node = new TreeNode(Tokens.Keyword, "WITH CHECK OPTION");
                        parent.AddChild(node);
                        token = lex.GetToken();
                    }
                    else
                    {
                        OnAnalyzeError();
                    }
                }
                else
                {
                    OnAnalyzeError();
                }
            }
        }


        void CreateIndex(TreeNode parent)
        {
            // CREATE [UNIQUE][CLUSTERED HASHED] INDEX index ON table ( {col [ASC|DESC]}* )
            // [PCTFREE int_const] [SIZE int_const ROWS|BUCKETS]
            TreeNode node;
            TreeNode temp = new TreeNode(Tokens.Keyword, "CREATE");
            parent.AddChild(temp);

            if (token.LiteralValue == "UNIQUE")
            {
                node = new TreeNode(Tokens.Keyword, token.LiteralValue);
                temp.AddChild(node);
                token = lex.GetToken();
            }
            if (token.LiteralValue == "CLUSTERED")
            {
                token = lex.GetToken();
                if (token.LiteralValue == "HASHED")
                {
                    node = new TreeNode(Tokens.Keyword, "CLUSTERED HASHED");
                    temp.AddChild(node);
                    token = lex.GetToken();
                }
            }

            if (token.LiteralValue == "INDEX")
            {
                node = new TreeNode(Tokens.Keyword, token.LiteralValue);
                temp.AddChild(node);
                token = lex.GetToken();
            }

            //TODO: check
            //if (token.LexicalCode == Tokens.Identifier)
            {
                node = new TreeNode(Tokens.Index, token.LiteralValue);
                temp.AddChild(node);
                token = lex.GetToken();
            }

            if (token.LiteralValue == "ON")
            {
                node = new TreeNode(Tokens.Keyword, token.LiteralValue);
                temp.AddChild(node);
                token = lex.GetToken();
            }

            //TODO: check
            //if (token.LexicalCode == Tokens.Identifier)
            {
                node = new TreeNode(Tokens.Table, token.LiteralValue);
                node.NodeInfo = "INDEX";
                temp.AddChild(node);
                token = lex.GetToken();
            }

            if (token.LexicalCode == Tokens.LeftParant) //'('
            {
                node = new TreeNode(Tokens.LeftParant, token.LiteralValue);
                temp.AddChild(node);
                token = lex.GetToken();
            }

            while (token.LexicalCode == Tokens.Identifier)
            {
                node = new TreeNode(Tokens.Column, token.LiteralValue);
                temp.AddChild(node);
                token = lex.GetToken();

                if (token.LiteralValue == "ASC" || token.LiteralValue == "DESC")
                {
                    node = new TreeNode(Tokens.Keyword, token.LiteralValue);
                    temp.AddChild(node);
                    token = lex.GetToken();
                }

                if (token.LexicalCode == Tokens.Comma)
                {
                    node = new TreeNode(Tokens.Comma, token.LiteralValue);
                    temp.AddChild(node);
                    token = lex.GetToken();
                }
            }

            if (token.LexicalCode == Tokens.RightParant) //')'
            {
                node = new TreeNode(Tokens.RightParant, token.LiteralValue);
                temp.AddChild(node);
                token = lex.GetToken();
            }

            // [PCTFREE int_const] 
            if (token.LiteralValue == "PCTFREE")
            {
                temp = new TreeNode(Tokens.Keyword, token.LiteralValue);
                parent.AddChild(temp);
                token = lex.GetToken();

                if (token.LexicalCode == Tokens.NumericConst)
                {
                    node = new TreeNode(Tokens.NumericConst, token.LiteralValue);
                    temp.AddChild(node);
                    token = lex.GetToken();
                }
            }

            //[SIZE int_const ROWS|BUCKETS]
            if (token.LiteralValue == "SIZE")
            {
                temp = new TreeNode(Tokens.Keyword, token.LiteralValue);
                parent.AddChild(temp);
                token = lex.GetToken();
                if (token.LexicalCode == Tokens.NumericConst)
                {
                    node = new TreeNode(Tokens.NumericConst, token.LiteralValue);
                    temp.AddChild(node);
                    token = lex.GetToken();
                }

                if (token.LiteralValue == "ROWS" || token.LiteralValue == "BUCKETS")
                {
                    node = new TreeNode(Tokens.Keyword, token.LiteralValue);
                    temp.AddChild(node);
                    token = lex.GetToken();
                }
            }

        }

        void CreateSynonym(TreeNode parent)
        {
            //CREATE [PUBLIC] SYNONYM ident FOR [TABLE|EXTERNAL FUNCTION|COMMAND|PROCEDURE] ident
            TreeNode node = new TreeNode(Tokens.Keyword, "CREATE");
            parent.AddChild(node);
            parent = node;

            if (token.LiteralValue == "PUBLIC")
            {
                node = new TreeNode(Tokens.Keyword, token.LiteralValue);
                parent.AddChild(node);
                token = lex.GetToken();
            }
            if (token.LiteralValue == "SYNONYM")
            {
                node = new TreeNode(Tokens.Keyword, token.LiteralValue);
                parent.AddChild(node);
                token = lex.GetToken();
            }
            if (token.LexicalCode == Tokens.Identifier)
            {
                node = new TreeNode(Tokens.Identifier, token.LiteralValue);
                parent.AddChild(node);
                token = lex.GetToken();
            }
            if (token.LexicalCode == Tokens.Keyword)
            {
                node = new TreeNode(Tokens.Keyword, token.LiteralValue);
                parent.AddChild(node);
                token = lex.GetToken();
            }
            if (token.LexicalCode == Tokens.Keyword)
            {
                if (token.LiteralValue == "EXTERNAL" && lex.Peek().LiteralValue == "FUNCTION")
                {
                    token = lex.GetToken();
                    node = new TreeNode(Tokens.Keyword, "EXTERNAL FUNCTION");
                }
                else
                {
                    node = new TreeNode(Tokens.Keyword, token.LiteralValue);
                }
                parent.AddChild(node);
                token = lex.GetToken();
            }
            if (token.LexicalCode == Tokens.Identifier)
            {
                node = new TreeNode(Tokens.Identifier, token.LiteralValue);
                parent.AddChild(node);
                token = lex.GetToken();
            }
        }
        
        void Select(TreeNode parent)
        {
            TreeNode node;

            //SELECT {[ALL|DISTINCT|*] [name =] expression| expression [AS name]}*
            node = new TreeNode(Tokens.Keyword, "SELECT");
            parent.AddChild(node);
            parent = node;
                                       
            token = lex.GetToken();

            if (token.LexicalCode == Tokens.Keyword) //ALL|DISTINCT
            {
                if (token is TokenAllowedRightAfterSELECT)//it's //ALL|DISTINCT
                {
                    //add it as it is
                    node = new TreeNode(Tokens.Keyword, token.LiteralValue);
                    parent.AddChild(node);
                    token = lex.GetToken();
                }
                else
                {
                    //let the normal parsing below do the rest
                    token.LexicalCode = Tokens.Identifier;
                }
            }
            else if (token.LiteralValue == "*")
            {
                node = new TreeNode(Tokens.Asterisk, "*");
                parent.AddChild(node);
                token = lex.GetToken();
            }

            if (token.LiteralValue != "FROM")
            {
                //expression
                do
                {
                    if (token.LexicalCode == Tokens.Comma)
                    {
                        node = new TreeNode(Tokens.Comma, ",");
                        parent.AddChild(node);
                        token = lex.GetToken();
                    }


                    Expression(parent, ExpressionRegion.Select);

                    //AS becomes last child of expression
                    if (token.LiteralValue == "AS")
                    {
                        TreeNode thisParent = parent.Children[parent.Children.Count - 1];

                        node = new TreeNode(Tokens.Keyword, "AS");
                        thisParent.AddChild(node);
                        token = lex.GetToken();
                        node = new TreeNode(Tokens.Column, token.LiteralValue, token.LiteralValue.ToUpper());
                        thisParent.AddChild(node);
                        token = lex.GetToken();
                    }

                } while (token.LexicalCode == Tokens.Comma);
            }
        }


        void Into(TreeNode parent)
        {
            TreeNode node;

            //INTO {bind variable}*
            node = new TreeNode(Tokens.Keyword, "INTO");
            parent.AddChild(node);
            parent = node;

            do
            {
                if (token.LexicalCode == Tokens.Comma)
                {
                    node = new TreeNode(Tokens.Comma, ",");
                    parent.AddChild(node);
                }
                token = lex.GetToken();
                if (token.LexicalCode == Tokens.BindVariable)
                {
                    node = new TreeNode(Tokens.BindVariable, token.LiteralValue);
                    parent.AddChild(node);
                    token = lex.GetToken();
                }

                if (token.LexicalCode == Tokens.BindVariable)
                {
                    do
                    {
                        node = new TreeNode(Tokens.Comma, ",");
                        parent.AddChild(node);

                        node = new TreeNode(Tokens.BindVariable, token.LiteralValue);
                        parent.AddChild(node);
                        token = lex.GetToken();
                    } while (token.LexicalCode == Tokens.BindVariable);
                }
            } while (token.LexicalCode == Tokens.Comma);
        }


        //asz:need to know till when can read after FROM clause 
        bool IsFromClauseEnd()
        {
            switch (token.LiteralValue)
            {
                case "WHERE":
                case "INTO":
                case "GROUP":
                case "HAVING":
                case "ORDER":
                case "FOR":
                case "UNION":
                    return true;
            }
            return false;                
        }

        //asz:ON clause can contain relation operators, columns, functions, parantheses
        void OnClause(TreeNode parent)
        {
            TreeNode node;

            while (token.LexicalCode != Tokens.Null && token.LexicalCode != Tokens.RightParant && !IsFromClauseEnd())
            {
                if (token.LexicalCode == Tokens.LeftParant)  //Process paratnheses recursively
                {
                    parent.AddChild(new TreeNode(token.LexicalCode, token.LiteralValue));
                    token = lex.GetToken();

                    OnClause(parent);

                    if (token.LexicalCode == Tokens.RightParant)
                    {
                        parent.AddChild(new TreeNode(token.LexicalCode, token.LiteralValue));
                        token = lex.GetToken();
                    }
                }
                else if (token.LexicalCode == Tokens.Function)
                {
                    if (lex.Peek().LexicalCode == Tokens.LeftParant)  
                    {
                        node = new TreeNode(Tokens.Expression);
                        node.AddChild(new TreeNode(token.LexicalCode, token.LiteralValue));
                        do
                        {
                            token = lex.GetToken();
                            node.AddChild(new TreeNode(token.LexicalCode, token.LiteralValue));
                        } while (token.LexicalCode != Tokens.RightParant);
                        parent.AddChild(node);
                        token = lex.GetToken();
                    }
                    else
                    {
                        parent.AddChild(new TreeNode(token.LexicalCode, token.LiteralValue));
                        token = lex.GetToken();
                    }                        
                }
                else if (token.LexicalCode == Tokens.Identifier)
                {
                    parent.AddChild(new TreeNode(Tokens.Column, token.LiteralValue));
                    token = lex.GetToken();
                }
                else
                {
                    parent.AddChild(new TreeNode(token.LexicalCode, token.LiteralValue));
                    token = lex.GetToken();
                }
            }
        }

        //ASZ: FROM clause
        void FromClause(TreeNode parent)
        {
            while (token.LexicalCode != Tokens.Null && token.LexicalCode != Tokens.RightParant && !IsFromClauseEnd())
            {
                if (token.LexicalCode == Tokens.LeftParant)  //Process paratnheses recursively
                {
                    TreeNode node = parent.AddChild(new TreeNode(Tokens.Expression));
                    node.AddChild(new TreeNode(token.LexicalCode, token.LiteralValue));
                    token = lex.GetToken();

                    FromClause(node);

                    if (token.LexicalCode == Tokens.RightParant)
                    {
                        node.AddChild(new TreeNode(token.LexicalCode, token.LiteralValue));
                        token = lex.GetToken();
                    }
                }
                else if (token.LexicalCode == Tokens.LeftParant)
                {
                    FromClause(parent);                                        
                }
                else if (token.LexicalCode == Tokens.Comma)  //if comma then comes next expression in FROM clause
                {
                    parent.AddChild(new TreeNode(token.LexicalCode, token.LiteralValue));
                    token = lex.GetToken();
                }
                else if (token.LexicalCode == Tokens.Keyword)
                {
                    if (token.LiteralValue == "ON")   //in FROM clause ON follows a Join
                    {
                        OnClause(parent);
                    }
                    else
                    {
                        TreeNode node = parent.AddChild(new TreeNode(token.LexicalCode, token.LiteralValue));
                        token = lex.GetToken();
                    }
                }
                else //table, or table with correlation                 
                {
                    string tableName = "";
                    if (token.LexicalCode == Tokens.StringConst) //owner
                    {
                        tableName = token.LiteralValue;
                        token = lex.GetToken();
                        if (token.LexicalCode == Tokens.Dot)
                        {
                            tableName += token.LiteralValue;
                            token = lex.GetToken();
                        }
                    }

                    TreeNode node = new TreeNode(Tokens.Table, token.LiteralValue, tableName + token.LiteralValue.Substring(token.LiteralValue.IndexOf('.') + 1).ToUpper());
                    parent.AddChild(node);
                    token = lex.GetToken();

                    //ASZ:test if next element is correlation
                    //lex.GetToken returns keyword T instead of identifier T, ex: select * from tbl T from...                    
                    if (token.LexicalCode == Tokens.Identifier || (token.LexicalCode == Tokens.Keyword && token.LiteralValue == "T")) //correlation
                    {
                        node.AddChild(new TreeNode(Tokens.Correlation, token.LiteralValue, token.LiteralValue));
                        token = lex.GetToken();
                    }
                }
            }            
        }

        void From(TreeNode parent)
        {
            TreeNode node;

            //FROM {table [correlation] [join specification]}*
            node = new TreeNode(Tokens.Keyword, "FROM");
            parent.AddChild(node);
            parent = node;
            token = lex.GetToken();

            FromClause(parent);
        }

        void WhereSelect(TreeNode parent)
        {
            //WHERE search condition
            TreeNode node = new TreeNode(Tokens.Keyword, "WHERE");
            parent.AddChild(node);

            token = lex.GetToken();
            SearchCondition(node);
        }


        void GroupBy(TreeNode parent)
        {
            TreeNode node;

            //GROUP BY {integer constant|column}*
            token = lex.GetToken();
            if (token.LiteralValue == "BY")
            {
                node = new TreeNode(Tokens.Keyword, "GROUP BY");
                parent.AddChild(node);
                parent = node;
            }
            //else error: Keyword "BY" expected
            else
            {
                OnAnalyzeError();
            }

            do
            {
                token = lex.GetToken();
                //if(token.LexicalCode == Tokens.Identifier) //column
                //{
                //    node = new TreeNode(Tokens.Column, token.LiteralValue, token.LiteralValue.ToUpper());
                //    parent.AddChild(node);
                //    token = lex.GetToken();
                //}
                if (token.LexicalCode == Tokens.NumericConst) //constant
                {
                    node = new TreeNode(Tokens.NumericConst, token.LiteralValue);
                    parent.AddChild(node);
                    token = lex.GetToken();
                }
                else
                {
                    Expression(parent);
                }

                if (token.LexicalCode == Tokens.Comma)
                {
                    node = new TreeNode(Tokens.Comma, ",");
                    parent.AddChild(node);
                }
            } while (token.LexicalCode == Tokens.Comma);
        }


        void Having(TreeNode parent)
        {
            //HAVING search condition
            TreeNode node = new TreeNode(Tokens.Keyword, "HAVING");
            parent.AddChild(node);

            token = lex.GetToken();
            SearchCondition(node);
        }


        void OrderBy(TreeNode parent)
        {
            TreeNode node;

            //ORDER BY {integer constant|column [ASC|DESC]}*
            token = lex.GetToken();
            if (token.LiteralValue == "BY")
            {
                node = new TreeNode(Tokens.Keyword, "ORDER BY");
                parent.AddChild(node);
                parent = node;
            }
            //else error: Keyword "BY" expected
            else
            {
                OnAnalyzeError();
            }

            do
            {
                token = lex.GetToken();
                //asz:select column as name from table order by name, (ame keyword in order by is column identifier)                
                if (token.LexicalCode == Tokens.Keyword && token.LiteralValue.ToUpper() == "NAME")
                {
                    token.LexicalCode = Tokens.Identifier;
                }
                if (token.LexicalCode == Tokens.Identifier
                    || token.LexicalCode == Tokens.DataTypeKeyword ||
                    (token.LexicalCode == Tokens.Keyword &&
                    !(token.LiteralValue == "ASC" || token.LiteralValue == "DESC"))
                    ) //column
                {
                        Expression(parent);
                }
                else
                {
                    if (token.LexicalCode == Tokens.NumericConst) //constant
                    {
                        node = new TreeNode(Tokens.NumericConst, token.LiteralValue);
                        parent.AddChild(node);
                    }
                    //else error: Constant or column name expected
                    else
                    {
                        OnAnalyzeError();
                    }
                    token = lex.GetToken();
                }

                if (token.LiteralValue == "NULLS")
                {
                    if (lex.Peek().LiteralValue == "FIRST")
                    {
                        token = lex.GetToken();
                        node = new TreeNode(Tokens.Keyword, "NULLS FIRST");
                        parent.AddChild(node);
                    }
                    else if (lex.Peek().LiteralValue == "LAST")
                    {
                        token = lex.GetToken();
                        node = new TreeNode(Tokens.Keyword, "NULLS LAST");
                        parent.AddChild(node);
                    }
                    else
                    {
                        OnAnalyzeError();
                    }
                    token = lex.GetToken();
                }
                if (token.LiteralValue == "ASC" || token.LiteralValue == "DESC") //ASC|DESC
                {
                    node = new TreeNode(Tokens.Keyword, token.LiteralValue);
                    parent.AddChild(node);
                    token = lex.GetToken();
                }
                if (token.LexicalCode == Tokens.Comma)
                {
                    node = new TreeNode(Tokens.Comma, ",");
                    parent.AddChild(node);
                }

            } while (token.LexicalCode == Tokens.Comma);
        }


        void ForUpdateOf(TreeNode parent)
        {
            TreeNode node;

            //FOR UPDATE OF {column}*
            token = lex.GetToken();
            if (token.LiteralValue == "UPDATE")
            {
                token = lex.GetToken();
                if (token.LiteralValue == "OF")
                {
                    node = new TreeNode(Tokens.Keyword, "FOR UPDATE OF");
                    parent.AddChild(node);
                    parent = node;
                    token = lex.GetToken();
                }
                else
                {
                    OnAnalyzeError();
                }
            }
            //else error: Keyword "UPDATE" expected
            else
            {
                OnAnalyzeError();
            }

            do
            {
                if (token.LexicalCode == Tokens.Identifier)
                {
                    node = new TreeNode(Tokens.Column, token.LiteralValue);
                    parent.AddChild(node);
                }
                //else error: Column name expected
                else
                {
                    OnAnalyzeError();
                }

                token = lex.GetToken();
                if (token.LexicalCode == Tokens.Comma)
                {
                    node = new TreeNode(Tokens.Comma, ",");
                    parent.AddChild(node);
                }
            } while (token.LexicalCode == Tokens.Comma);
        }


        void Union(TreeNode parent)
        {
            TreeNode node;

            // {select UNION [ALL]}* [ORDER BY {const [ASC|DESC]}*]
            node = new TreeNode(Tokens.Keyword, "UNION");
            parent.AddChild(node);

            token = lex.GetToken();
            if (token.LiteralValue == "ALL")
            {
                node = new TreeNode(Tokens.Keyword, "ALL");
                parent.AddChild(node);
                token = lex.GetToken();
            }

            if (token.LiteralValue == "SELECT")
                SelectStatement(syntaxTree.root);

            while (token.LiteralValue == "UNION")
                Union(parent);

            if (token.LiteralValue == "ORDER")
                OrderBy(parent);
        }


        void Insert(TreeNode parent)
        {
            TreeNode node;

            //INSERT INTO table [{column}*]
            token = lex.GetToken();
            if (token.LiteralValue == "INTO")
            {
                node = new TreeNode(Tokens.Keyword, "INSERT INTO");
                parent.AddChild(node);
                parent = node;
            }
            //else error: Keyword "INTO" expected
            else
            {
                OnAnalyzeError();
            }

            token = lex.GetToken();
            //if(token.LexicalCode == Tokens.Identifier)
            {
                node = new TreeNode(Tokens.Table, token.LiteralValue, token.LiteralValue.Substring(token.LiteralValue.IndexOf('.') + 1).ToUpper()); //table
                parent.AddChild(node);
            }
            //else error: Table name expected
            token = lex.GetToken();
            if (token.LexicalCode == Tokens.LeftParant)
            {
                node = new TreeNode(Tokens.LeftParant, "(");
                parent.AddChild(node);
                token = lex.GetToken();
                while (token.LexicalCode != Tokens.RightParant)
                {
                    //some columns might have keywords as names
                    if (token.LexicalCode == Tokens.Identifier ||
                        token.LexicalCode == Tokens.Keyword ||
                        token.LexicalCode == Tokens.DataTypeKeyword ||
                        token.LexicalCode == Tokens.SysKeyword)
                    {
                        node = new TreeNode(Tokens.Column, token.LiteralValue); //column
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
                node = new TreeNode(Tokens.RightParant, ")");
                parent.AddChild(node);
                token = lex.GetToken();
            }
        }


        void Values(TreeNode parent)
        {
            TreeNode node;                  

            //VALUES ({constant|bind variable}*)[\ value_list]
            node = new TreeNode(Tokens.Keyword, "VALUES");
            parent.AddChild(node);
            parent = node;

            int countBindVar = 0, index = 0;
            bool isCount = false;

            token = lex.GetToken();
            if (token.LexicalCode == Tokens.LeftParant)
            {
                node = new TreeNode(Tokens.LeftParant, "(");
                parent.AddChild(node);
                token = lex.GetToken();

                while (token.LexicalCode != Tokens.RightParant)
                {
                    switch (token.LexicalCode)
                    {
                        case Tokens.LeftParant:
                            {
                                if (lex.Peek().LiteralValue == "SELECT")
                                {                                    
                                    parent.AddChild(new TreeNode(Tokens.LeftParant, "("));
                                    token = lex.GetToken();
                                    subSelect.Push(parent);
                                    SelectStatement(parent);
                                    if (token.LexicalCode == Tokens.RightParant)
                                    {
                                        parent.AddChild(new TreeNode(Tokens.RightParant, ")"));
                                        //token = lex.GetToken();
                                    }
                                }

                                break;
                            }



                        case Tokens.MathOperator: //positive or negative number
                            {
                                if (token.LiteralValue == "+" || token.LiteralValue == "-")
                                {
                                    string unaryOp = token.LiteralValue;
                                    token = lex.GetToken();
                                    if (token.LexicalCode == Tokens.NumericConst)
                                    {
                                        node = new TreeNode(token.LexicalCode, unaryOp + token.LiteralValue, index.ToString());
                                        parent.AddChild(node);
                                    }
                                }
                                index++;
                                break;
                            }

                        case Tokens.NumericConst: //constant
                        case Tokens.StringConst:
                        case Tokens.DatetimeConst:
                            {
                                node = new TreeNode(token.LexicalCode, token.LiteralValue, index.ToString());
                                parent.AddChild(node);
                                index++;
                                break;
                            }

                        case Tokens.BindVariable: //bind variable
                            {
                                countBindVar++;

                                node = new TreeNode(Tokens.BindVariable, token.LiteralValue, index.ToString());
                                parent.AddChild(node);
                                index++;
                                break;
                            }

                        case Tokens.SysKeyword: //system keyword
                            {
                                node = new TreeNode(Tokens.SysKeyword, token.LiteralValue, index.ToString());
                                parent.AddChild(node);
                                index++;
                                break;
                            }

                        case Tokens.Keyword: //keyword
                            {
                                if (token.LiteralValue == "CURRENT")
                                {
                                    CurrentOperator(parent);
                                }
                                else
                                {
                                    node = new TreeNode(Tokens.Keyword, token.LiteralValue, index.ToString());
                                    parent.AddChild(node);
                                }
                                index++;
                                break;
                            }

                        #region UserDefinedFunction

                        case Tokens.UserDefinedFunction: //user defined function
                            {
                                node = new TreeNode(Tokens.UserDefinedFunction, token.LiteralValue, index.ToString());
                                parent.AddChild(node);
                                parent = node;

                                token = lex.GetToken();
                                if (token.LexicalCode == Tokens.LeftParant)
                                {
                                    node = new TreeNode(Tokens.LeftParant, "(");
                                    parent.AddChild(node);
                                    token = lex.GetToken();

                                    do
                                    {
                                        if (token.LexicalCode == Tokens.Comma)
                                        {
                                            node = new TreeNode(Tokens.Comma, ",");
                                            parent.AddChild(node);
                                            token = lex.GetToken();
                                        }

                                        Expression(parent);
                                    } while (token.LexicalCode == Tokens.Comma);

                                    if (token.LexicalCode == Tokens.RightParant)
                                    {
                                        node = new TreeNode(Tokens.RightParant, ")");
                                        parent.AddChild(node);
                                    }
                                    else
                                    {
                                        OnAnalyzeError();
                                    }
                                }
                                else
                                {
                                    OnAnalyzeError();
                                }
                                index++;
                                break;
                            }

                        #endregion

                        #region Function

                        case Tokens.Function:
                            {
                                //@RDU moved outside the case
                                //bool isCount = false;
                                node = new TreeNode(Tokens.Function, token.LiteralValue, index.ToString());
                                parent.AddChild(node);
                                TreeNode nodeFunction = node;

                                if (token.LiteralValue == "COUNT")
                                {
                                    isCount = true;
                                }

                                //if (token.LiteralValue != "CURRENT")//datetime value
                                //{
                                //    token = lex.GetToken();
                                //}

                                if (token.LexicalCode == Tokens.LeftParant)
                                {
                                    node = new TreeNode(Tokens.LeftParant, "(");
                                    nodeFunction.AddChild(node);
                                    token = lex.GetToken();

                                    if (isCount && token.LiteralValue == "*")
                                    {
                                        node = new TreeNode(Tokens.Asterisk, "*");
                                        nodeFunction.AddChild(node);
                                        token = lex.GetToken();
                                    }
                                    else
                                    {
                                        do
                                        {
                                            if (token.LexicalCode == Tokens.Comma)
                                            {
                                                node = new TreeNode(Tokens.Comma, ",");
                                                nodeFunction.AddChild(node);
                                                token = lex.GetToken();
                                            }

                                            //FIX 2006.04.14
                                            //aggregate functions: MAX, MIN, AVG, SUM, @MEDIAN and @SDV 
                                            //can contain the keywords 'DISTINCT' or 'ALL'
                                            //COUNT can only contain DISTINCT
                                            if (token.LiteralValue == "DISTINCT" || token.LiteralValue == "ALL")
                                            {
                                                node = new TreeNode(Tokens.Keyword, token.LiteralValue);
                                                nodeFunction.AddChild(node);
                                                token = lex.GetToken();
                                            }
                                            // 2006.09.27 CO: if in a function appears another keyword than "DISTINCT" or "ALL"
                                            // then we can assume that the token is a column.
                                            if (token.LexicalCode == Tokens.Keyword)
                                            {
                                                node = new TreeNode(Tokens.Column, token.LiteralValue, token.LiteralValue.ToUpper());
                                                nodeFunction.AddChild(node);
                                                token = lex.GetToken();
                                            }

                                            Expression(nodeFunction);
                                        } while (token.LexicalCode == Tokens.Comma);
                                    }

                                    if (token.LexicalCode == Tokens.RightParant)
                                    {
                                        node = new TreeNode(Tokens.RightParant, ")");
                                        nodeFunction.AddChild(node);
                                    }
                                    isCount = false;
                                }
                                //@RDU if the COUNT doesn't have parameters, we need to add (*)
                                else
                                {
                                    if (isCount)
                                    {
                                        nodeFunction.AddChild(new TreeNode(Tokens.LeftParant, "("));
                                        nodeFunction.AddChild(new TreeNode(Tokens.Asterisk, "*"));
                                        nodeFunction.AddChild(new TreeNode(Tokens.RightParant, ")"));
                                    }
                                }
                                index++;
                                break;
                            }
                        #endregion

                        default:
                            {
                                OnAnalyzeError();
                                break;
                            }
                    }
                    token = lex.GetToken();
                    if (token.LexicalCode == Tokens.Comma)
                    {
                        node = new TreeNode(Tokens.Comma, ",");
                        parent.AddChild(node);
                        token = lex.GetToken();
                    }
                }
                node = new TreeNode(Tokens.RightParant, ")");
                parent.AddChild(node);
                token = lex.GetToken();
            }
            //else error: '(' expected
            else
            {
                OnAnalyzeError();
            }

            TreeNode temp;

            int countRows = 0;

            while (token.LexicalCode == Tokens.NumericConst ||
                token.LexicalCode == Tokens.StringConst ||
                token.LexicalCode == Tokens.Identifier || //string constants not enclosed in ''
                token.LexicalCode == Tokens.DatetimeConst)
            {

                int countValues = 1;
                countRows++;

                temp = new TreeNode(Tokens.Row, "", countRows.ToString());
                parent.AddChild(temp);


                while (countValues <= countBindVar)
                {
                    if (token.LexicalCode == Tokens.Identifier)
                    {
                        node = new TreeNode(Tokens.StringConst, "\"" + token.LiteralValue + "\"");
                    }
                    else
                    {
                        node = new TreeNode(token.LexicalCode, token.LiteralValue);
                    }
                    temp.AddChild(node);
                    token = lex.GetToken();

                    countValues++;

                    if (token.LexicalCode == Tokens.Comma)
                    {
                        node = new TreeNode(Tokens.Comma, ",");
                        temp.AddChild(node);
                        token = lex.GetToken();
                    }
                }
            }
        }


        void Update(TreeNode parent)
        {
            TreeNode node;

            //UPDATE table [correlation]
            node = new TreeNode(Tokens.Keyword, "UPDATE");
            parent.AddChild(node);
            parent = node;

            token = lex.GetToken();
            node = new TreeNode(Tokens.Table, token.LiteralValue, token.LiteralValue.Substring(token.LiteralValue.IndexOf('.') + 1).ToUpper());
            parent.AddChild(node);
            token = lex.GetToken();
            if (token.LexicalCode == Tokens.Identifier)
            {
                node.AddChild(new TreeNode(Tokens.Correlation, token.LiteralValue));
                token = lex.GetToken();
            }
        }


        void Set(TreeNode parent)
        {
            // SET {column = expression|NULL|(subselect)}*
            // SET (column ,*) = (expression ,*)
            TreeNode setNode = new TreeNode(Tokens.Keyword, "SET");
            parent = parent.AddChild(setNode);

            TreeNode node;
            if (lex.Peek().LexicalCode == Tokens.Identifier) //columns
            {
                #region simple columns
                do
                {
                    TreeNode predicate = new TreeNode(Tokens.Predicate);
                    setNode.NodeInfo = "SINGLE FORMAT";
                    setNode.AddChild(predicate);
                    parent = predicate;
                    token = lex.GetToken();
                    //if(token.LexicalCode == Tokens.Identifier) //column
                    {
                        node = new TreeNode(Tokens.Column, token.LiteralValue);
                        parent.AddChild(node);
                    }
                    token = lex.GetToken();
                    if (token.LiteralValue == "=")
                    {
                        node = new TreeNode(Tokens.RelatOperator, "=");
                        parent.AddChild(node);
                    }
                    //else error: '=' expected
                    else
                    {
                        OnAnalyzeError();
                    }

                    token = lex.GetToken();
                    if (token.LiteralValue == "NULL") //NULL
                    {
                        node = new TreeNode(Tokens.SysKeyword, "NULL");
                        parent.AddChild(node);
                        token = lex.GetToken();
                    }
                    else if (token.LexicalCode == Tokens.LeftParant)
                    {
                        if (lex.Peek().LiteralValue == "SELECT")
                        {
                            node = new TreeNode(Tokens.LeftParant, "(");
                            parent.AddChild(node);
                            token = lex.GetToken();
                            subSelect.Push(parent);
                            SelectStatement(parent);
                            //if (subSelect.Peek() == parent)
                            //{
                            //    subSelect.Pop();
                            //}
                            if (token.LexicalCode == Tokens.RightParant)
                            {
                                node = new TreeNode(Tokens.RightParant, ")");
                                parent.AddChild(node);
                                token = lex.GetToken();
                            }
                        }
                        else
                        {
                            Expression(parent);
                        }
                    }
                    else
                    {
                        Expression(parent); //expression
                    }
                    if (token.LexicalCode == Tokens.Comma)
                    {
                        node = new TreeNode(Tokens.Comma, ",");
                        parent.AddChild(node);
                    }
                } while (token.LexicalCode == Tokens.Comma); 
                #endregion
            }
            else if(lex.Peek().LexicalCode == Tokens.LeftParant)
            {
                #region MultiFormat columns                
                parent = setNode.AddChild(new TreeNode(Tokens.Predicate));
                setNode.NodeInfo = "MULTI FORMAT";

                token = lex.GetToken();                
                parent.AddChild(new TreeNode(Tokens.LeftParant, token.LiteralValue));

                token = lex.GetToken();
                do
                {
                    if (token.LexicalCode == Tokens.Identifier) //column
                    {
                        parent.AddChild(new TreeNode(Tokens.Column, token.LiteralValue));
                    }
                    token = lex.GetToken();
                    if (token.LexicalCode == Tokens.Comma)
                    {
                        parent.AddChild(new TreeNode(Tokens.Comma, token.LiteralValue));
                    }
                } while (token.LexicalCode != Tokens.RightParant && token.LexicalCode != Tokens.Null);
                parent.AddChild(new TreeNode(Tokens.RightParant, token.LiteralValue));

                token = lex.GetToken();                 
                if (token.LiteralValue == "=")
                {
                    setNode.AddChild(new TreeNode(Tokens.RelatOperator, token.LiteralValue));
                    parent = setNode.AddChild(new TreeNode(Tokens.Predicate));
                    token = lex.GetToken();
                    if (token.LexicalCode == Tokens.LeftParant)
                    {
                        parent.AddChild(new TreeNode(Tokens.LeftParant, "("));
                        token = lex.GetToken();
                    }
                }
                else
                {
                    OnAnalyzeError();
                }

                while (token.LexicalCode != Tokens.RightParant && token.LexicalCode != Tokens.Null)
                {
                    if (token.LexicalCode == Tokens.Comma)
                    {
                        parent.AddChild(new TreeNode(token.LexicalCode, token.LiteralValue));
                        token = lex.GetToken();
                    }
                    else if (token.LexicalCode != Tokens.LeftParant || lex.Peek().LiteralValue != "SELECT")
                    {
                        Expression(parent);
                    }
                    else // (SELECT...
                    {
                        #region subselect(parent)
                        parent = parent.AddChild(new TreeNode(Tokens.Expression));
                        parent.AddChild(new TreeNode(Tokens.LeftParant, "("));
                        token = lex.GetToken();
                        subSelect.Push(parent);
                        SelectStatement(parent);
                        if (token.LexicalCode == Tokens.RightParant)
                        {
                            parent.AddChild(new TreeNode(Tokens.RightParant, ")"));
                            parent = parent.Parent;
                            token = lex.GetToken();
                        }
                        #endregion
                    }                                        
                }                

                if (token.LexicalCode == Tokens.RightParant)
                {
                    parent.AddChild(new TreeNode(token.LexicalCode, token.LiteralValue));
                    token = lex.GetToken();
                } 
                #endregion
            }
        }


        void WhereUpdate(TreeNode parent)
        {
            TreeNode node;

            //[WHERE search condition|CURRENT OF cursor] [CHECK EXISTS]
            node = new TreeNode(Tokens.Keyword, "WHERE");
            parent.AddChild(node);
            parent = node;

            token = lex.GetToken();
            if (token.LiteralValue == "CURRENT") //CURRENT OF
            {
                token = lex.GetToken();
                if (token.LiteralValue == "OF")
                {
                    node = new TreeNode(Tokens.Keyword, "CURRENT OF");
                    parent.AddChild(node);
                    parent = node;
                }
                //else error: Keyword "OF" expected
                else
                {
                    OnAnalyzeError();
                }

                token = lex.GetToken();
                if (token.LexicalCode == Tokens.Identifier) //cursor
                {
                    node = new TreeNode(Tokens.Cursor, token.LiteralValue);
                    parent.AddChild(node);
                    token = lex.GetToken();
                }
                //else error: Cursor name expected
                else
                {
                    OnAnalyzeError();
                }
            }
            else
                SearchCondition(parent); //search condition

            if (token.LiteralValue == "CHECK") //CHECK EXISTS
            {
                token = lex.GetToken();
                if (token.LiteralValue == "EXISTS")
                {
                    node = new TreeNode(Tokens.Keyword, "CHECK EXISTS");
                    parent.AddChild(node);
                    token = lex.GetToken();
                }
                //else error: Keyword "EXISTS" expected
                else
                {
                    OnAnalyzeError();
                }
            }
        }


        void Delete(TreeNode parent)
        {
            TreeNode node;

            //DELETE FROM table [correlation]

            token = lex.GetToken();
            node = new TreeNode(Tokens.Keyword, "DELETE FROM");
            parent.AddChild(node);
            parent = node;

            if (token.LiteralValue != "DELETE FROM" && token.LiteralValue != "FROM")
            {
                OnAnalyzeError();
            }

            token = lex.GetToken();

            //if(token.LexicalCode == Tokens.Identifier)
            {
                node = new TreeNode(Tokens.Table, token.LiteralValue, token.LiteralValue.Substring(token.LiteralValue.IndexOf('.') + 1).ToUpper());
                parent.AddChild(node);
                token = lex.GetToken();
            }
            //else error: Table name expected
            if (token.LexicalCode == Tokens.Identifier) //correlation
            {
                node.AddChild(new TreeNode(Tokens.Correlation, token.LiteralValue));
                token = lex.GetToken();
            }
        }


        void WhereDelete(TreeNode parent)
        {
            TreeNode node;

            //[WHERE search condition|CURRENT OF cursor]
            if (token.LiteralValue == "WHERE")
            {
                node = new TreeNode(Tokens.Keyword, "WHERE");
                parent.AddChild(node);
                parent = node;
            }
            else
            {
                OnAnalyzeError();
            }

            token = lex.GetToken();
            if (token.LiteralValue == "CURRENT") //CURRENT OF
            {
                token = lex.GetToken();
                if (token.LiteralValue == "OF")
                {
                    node = new TreeNode(Tokens.Keyword, "CURRENT OF");
                    parent.AddChild(node);
                    parent = node;
                }
                //else error: Keyword "OF" expected
                else
                {
                    OnAnalyzeError();
                }
                token = lex.GetToken();
                if (token.LexicalCode == Tokens.Identifier) //cursor
                {
                    node = new TreeNode(Tokens.Cursor, token.LiteralValue);
                    parent.AddChild(node);
                    token = lex.GetToken();
                }
                //else error: Cursor name expected
                else
                {
                    OnAnalyzeError();
                }
            }
            else
                SearchCondition(parent); //search condition
        }

        private void UpdateStatistics(TreeNode parent)
        {
            //UPDATE STATISTICS

            TreeNode node;
            if (token.LiteralValue == "UPDATE")
            {
                node = new TreeNode(Tokens.Keyword, "UPDATE");
                parent.AddChild(node);
                parent = node;
                token = lex.GetToken();
            }
            else
            {
                OnAnalyzeError();
            }

            if (token.LiteralValue == "STATISTICS")
            {
                token = lex.GetToken();
                node = new TreeNode(Tokens.Keyword, "STATISTICS");
                parent.AddChild(node);
            }
            else
            {
                OnAnalyzeError();
            }
        }

        void Expression(TreeNode parent)
        {
            Expression(parent, ExpressionRegion.Other);
        }

        void Expression(TreeNode parent, ExpressionRegion region)
        {            
            TreeNode node;
            TreeNode temp = null;
            //@RDU Moved here because the scope of the variable was restricted
            bool isCount = false;
            bool isStringIndexer = false;

            // {constant|column|function|(expression)|bind variable|system keyword [|| | / |* | + |- ]}*
            node = new TreeNode(Tokens.Expression);
            parent.AddChild(node);
            parent = node;

            if (token.LiteralValue == "SELECT")
            {
                subSelect.Push(parent);
                SelectStatement(parent);
                return;
            }

            int operands = 0;

            TreeNode nodeAddAliasNodeAtEnd = null;

            while (token.LexicalCode != Tokens.RightParant
                && token.LexicalCode != Tokens.RelatOperator
                && token.LexicalCode != Tokens.Null && token.LexicalCode != Tokens.JoinOperator
                && token.LiteralValue != "WHERE" && (token.LexicalCode != Tokens.Comma  || isStringIndexer))
            {
                operands++;

                if (token.LexicalCode == Tokens.Keyword)
                {
                    if (token.LiteralValue == "CURRENT")
                    {
                        CurrentOperator(parent);
                        token = lex.GetToken();
                    }
                    if ((region == ExpressionRegion.Select && !lex.IsKeywordBreakingSELECT(token.LiteralValue) && token.LiteralValue.ToUpper() != "AS")
                        || token.LiteralValue == "KEY" || token.LiteralValue == "SYSTEM" ||
                        ((token.LiteralValue == "ORDER" || token.LiteralValue == "GROUP") && lex.Peek().LiteralValue != "BY") || token.LiteralValue == "ROWID") //Hack for cases where KEY is a column name
                    {
                        token.LexicalCode = Tokens.Identifier;
                    }
                    else
                    {
                        break;
                    }
                }

                switch (token.LexicalCode)
                {
                    case Tokens.Identifier: //colum
                    //Treat datatype keyword as column names too
                    case Tokens.DataTypeKeyword:
                        {
                            //name =
                            if (region == ExpressionRegion.Select && lex.Peek().LiteralValue == "=")
                            {
                                //it's an alias
                                nodeAddAliasNodeAtEnd = new TreeNode(Tokens.Column, token.LiteralValue);
                                token = lex.GetToken();
                            }
                            else
                            {
                                node = new TreeNode(Tokens.Column, token.LiteralValue, token.LiteralValue.ToUpper());
                                parent.AddChild(node);
                            }
                            break;
                        }

                    #region UserDefinedFunction

                    case Tokens.UserDefinedFunction: //user defined function
                        {
                            node = new TreeNode(Tokens.UserDefinedFunction, token.LiteralValue);
                            parent.AddChild(node);
                            parent = node;

                            token = lex.GetToken();
                            if (token.LexicalCode == Tokens.LeftParant)
                            {
                                node = new TreeNode(Tokens.LeftParant, "(");
                                parent.AddChild(node);
                                token = lex.GetToken();

                                do
                                {
                                    if (token.LexicalCode == Tokens.Comma)
                                    {
                                        node = new TreeNode(Tokens.Comma, ",");
                                        parent.AddChild(node);
                                        token = lex.GetToken();
                                    }

                                    Expression(parent);
                                } while (token.LexicalCode == Tokens.Comma);

                                if (token.LexicalCode == Tokens.RightParant)
                                {
                                    node = new TreeNode(Tokens.RightParant, ")");
                                    parent.AddChild(node);
                                }
                                else
                                {
                                    OnAnalyzeError();
                                }
                            }
                            else
                            {
                                OnAnalyzeError();
                            }
                            break;
                        }

                    #endregion

                    #region Function
                    case Tokens.Function:
                        {
                            //@RDU moved outside the case
                            //bool isCount = false;
                            node = new TreeNode(Tokens.Function, token.LiteralValue);
                            parent.AddChild(node);
                            TreeNode nodeFunction = node;

                            if (token.LiteralValue == "COUNT")
                            {
                                isCount = true;
                            }

                            if (token.LiteralValue != "@NOW")
                            {
                                token = lex.GetToken();
                            }

                            if (token.LexicalCode == Tokens.LeftParant)
                            {        
                                node = new TreeNode(Tokens.LeftParant, "(");
                                nodeFunction.AddChild(node);
                                token = lex.GetToken();

                                if (isCount && token.LiteralValue == "*")
                                {
                                    node = new TreeNode(Tokens.Asterisk, "*");
                                    nodeFunction.AddChild(node);
                                    token = lex.GetToken();
                                }
                                else
                                {
                                    do
                                    {
                                        if (token.LexicalCode == Tokens.Comma)
                                        {
                                            node = new TreeNode(Tokens.Comma, ",");
                                            nodeFunction.AddChild(node);
                                            token = lex.GetToken();
                                        }

                                        //FIX 2006.04.14
                                        //aggregate functions: MAX, MIN, AVG, SUM, @MEDIAN and @SDV 
                                        //can contain the keywords 'DISTINCT' or 'ALL'
                                        //COUNT can only contain DISTINCT
                                        if (token.LiteralValue == "DISTINCT" || token.LiteralValue == "ALL")
                                        {
                                            node = new TreeNode(Tokens.Keyword, token.LiteralValue);
                                            nodeFunction.AddChild(node);
                                            token = lex.GetToken();
                                        }
                                        // 2006.09.27 CO: if in a function appears another keyword than "DISTINCT" or "ALL"
                                        // then we can assume that the token is a column if it's not a subSelect.
                                        if (token.LexicalCode == Tokens.Keyword)
                                        {
                                            if (token.LiteralValue != "SELECT")
                                            {
                                                node = new TreeNode(Tokens.Column, token.LiteralValue, token.LiteralValue.ToUpper());
                                                nodeFunction.AddChild(node);
                                                token = lex.GetToken();
                                            }
                                        }
                                        Expression(nodeFunction);

                                        //It's possible to have an outer join inside a function
                                        if (token.LexicalCode == Tokens.JoinOperator)
                                        {
                                            node = new TreeNode(Tokens.JoinOperator, "(+)", "RIGHT");
                                            nodeFunction.AddChild(node);
                                            token = lex.GetToken();
                                            TreeNode predicateNode = syntaxTree.FindParent(syntaxTree.root, parent);
                                            predicateNode.NodeInfo = "RIGHT OUTER JOIN";
                                        }
                                    } while (token.LexicalCode == Tokens.Comma);
                                }

                                if (token.LexicalCode == Tokens.RightParant)
                                {
                                    node = new TreeNode(Tokens.RightParant, ")");
                                    nodeFunction.AddChild(node);
                                }
                                isCount = false;
                            }
                            //@RDU if the COUNT doesn't have parameters, we need to add (*)
                            else
                            {
                                if (isCount)
                                {
                                    nodeFunction.AddChild(new TreeNode(Tokens.LeftParant, "("));
                                    nodeFunction.AddChild(new TreeNode(Tokens.Asterisk, "*"));
                                    nodeFunction.AddChild(new TreeNode(Tokens.RightParant, ")"));
                                }
                            }
                            break;
                        }
                    #endregion

                    case Tokens.NumericConst: //constant
                        {
                            node = new TreeNode(Tokens.NumericConst, token.LiteralValue);
                            if (isStringIndexer)
                            {
                                temp.AddChild(node);
                            }
                            else
                            {
                                parent.AddChild(node);
                            }
                            break;
                        }

                    case Tokens.StringConst: //constant
                        {
                            node = new TreeNode(Tokens.StringConst, token.LiteralValue);
                            parent.AddChild(node);
                            break;
                        }

                    case Tokens.DatetimeConst: //constant
                        {
                            node = new TreeNode(Tokens.DatetimeConst, token.LiteralValue);
                            parent.AddChild(node);
                            break;
                        }

                    case Tokens.LeftParant: //expression
                        {
                            node = new TreeNode(Tokens.LeftParant, "(");
                            parent.AddChild(node);
                            token = lex.GetToken();
                            Expression(parent);
                            if (token.LexicalCode == Tokens.RightParant)
                            {
                                node = new TreeNode(Tokens.RightParant, ")");
                                parent.AddChild(node);
                            }
                            else
                            {
                                OnAnalyzeError();
                            }
                            break;
                        }

                    case Tokens.BindVariable: //bind variable
                        {
                            node = new TreeNode(Tokens.BindVariable, token.LiteralValue);
                            parent.AddChild(node);
                            break;
                        }

                    case Tokens.SysKeyword: //system keyword
                        {
                            node = new TreeNode(Tokens.SysKeyword, token.LiteralValue);
                            parent.AddChild(node);
                            break;
                        }

                    case Tokens.BoolOperator:
                    case Tokens.Concatenate:
                        {
                            node = new TreeNode(token.LexicalCode, token.LiteralValue);
                            parent.AddChild(node);
                            break;
                        }

                    case Tokens.MathOperator: //operator
                        {
                            if (operands == 1 && (token.LiteralValue == "+" || token.LiteralValue == "-"))
                            {
                                string unaryOp = token.LiteralValue;
                                if (lex.Peek().LexicalCode == Tokens.NumericConst)
                                {
                                    token = lex.GetToken();
                                    node = new TreeNode(token.LexicalCode, unaryOp + token.LiteralValue);
                                    parent.AddChild(node);
                                }
                                else
                                {
                                    node = new TreeNode(token.LexicalCode, token.LiteralValue);
                                    parent.AddChild(node);
                                }
                            }
                            else
                            {
                                node = new TreeNode(token.LexicalCode, token.LiteralValue);
                                parent.AddChild(node);
                            }
                            break;
                        }
                    case Tokens.LeftBracket:
                        {
                            isStringIndexer = true;
                            temp = new TreeNode(Tokens.StringIndexer);
                            node = new TreeNode(token.LexicalCode, token.LiteralValue);
                            temp.AddChild(node);
                            break;
                        }
                    case Tokens.RightBracket:
                        {
                            isStringIndexer = false;                                                       
                            temp.AddChild(new TreeNode(token.LexicalCode, token.LiteralValue));
                            //string indexer together with the column must be treated as a whole
                            //"select LOWER(ba_bez || ba_bez[1, 3]) || ba_bez[1, 2] from at_berart_pass16"
                            int idx = parent.Children.Count - 1;
                            if (idx >= 0)                                  
                            {
                                node = new TreeNode(Tokens.Expression);
                                node.Parent = parent;
                                node.AddChild(parent.Children[idx]);
                                node.AddChild(temp);
                                parent.Children[idx] = node;
                            }
                            else  //???
                            {
                                parent.AddChild(temp);
                            }                                
                            break;
                        }
                    case Tokens.Comma:
                        {
                            if (isStringIndexer)
                            {
                                node = new TreeNode(token.LexicalCode, token.LiteralValue);
                                temp.AddChild(node);
                            }
                            break;
                        }
                    default:
                        {
                            OnAnalyzeError();
                            throw new Exception("Error in expresion");
                            break;
                        }//error: Error in expresion
                }
                //@RDU modified to solve the COUNT with no params problem
                //token = lex.GetToken();
                if (isCount)
                {
                    isCount = false;
                }
                else if (token.LexicalCode != Tokens.Keyword)
                {
                    token = lex.GetToken();
                }

            }

            if (nodeAddAliasNodeAtEnd != null)
            {
                parent.AddChild(new TreeNode(Tokens.Keyword, "AS"));
                parent.AddChild(nodeAddAliasNodeAtEnd);
            }
        }

        void OnAnalyzeError()
        {
            throw new Exception("Syntax error");
        }

        void JoinSpecification(TreeNode parent)
        {
            TreeNode node;

            //{[NATURAL][INNER|CROSS|[LEFT|RIGHT|FULL[OUTER]]] JOIN table [USING {column}*]|[ON search condition]}*
            //Note: only one of NATURAL, USING, ON keywords can appear in the join specification
            node = new TreeNode(Tokens.JoinSpecif);
            parent.AddChild(node);
            parent = node;

            do
            {
                while (token.LexicalCode == Tokens.Keyword)
                {
                    node = new TreeNode(Tokens.Keyword, token.LiteralValue);
                    parent.AddChild(node);
                    token = lex.GetToken();
                    if (node.NodeValue == "JOIN")
                    {
                        break;
                    }
                }
                //if(token.LexicalCode == Tokens.Identifier)
                {
                    node = new TreeNode(Tokens.Table, token.LiteralValue, token.LiteralValue.Substring(token.LiteralValue.IndexOf('.') + 1).ToUpper()); //table
                    parent.AddChild(node);
                }
                token = lex.GetToken();
                if (token.LexicalCode == Tokens.Identifier)
                {
                    node.AddChild(new TreeNode(Tokens.Correlation, token.LiteralValue)); //correlation
                    token = lex.GetToken();
                }

                if (token.LexicalCode == Tokens.Keyword)
                {
                    node = new TreeNode(Tokens.Keyword, token.LiteralValue);
                    parent.AddChild(node);
                    if (token.LiteralValue == "USING")
                    {
                        do
                        {
                            token = lex.GetToken();
                            if (token.LexicalCode == Tokens.Identifier)
                            {
                                node = new TreeNode(Tokens.Column, token.LiteralValue);
                                parent.AddChild(node);
                            }
                            //else error: Column name expected
                            else
                            {
                                OnAnalyzeError();
                            }

                            token = lex.GetToken();
                            if (token.LexicalCode == Tokens.Comma)
                            {
                                node = new TreeNode(Tokens.Comma, ",");
                                parent.AddChild(node);
                            }
                        } while (token.LexicalCode == Tokens.Comma);
                    }
                    else if (token.LiteralValue == "ON")
                    {
                        token = lex.GetToken();
                        SearchCondition(parent);
                    }
                }
            }
            while (token.LiteralValue == "NATURAL" || token.LiteralValue == "INNER" || token.LiteralValue == "CROSS" ||
                  token.LiteralValue == "LEFT" || token.LiteralValue == "RIGHT" || token.LiteralValue == "FULL" || token.LiteralValue == "JOIN");
        }


        void SearchCondition(TreeNode parent)
        {
            TreeNode node;
            int level = 0;

            //{[NOT] predicate [AND|OR]}*
            node = new TreeNode(Tokens.SearchCond);
            parent.AddChild(node);
            parent = node;

            while (((token.LiteralValue != "GROUP" && token.LiteralValue != "ORDER") || lex.Peek().LiteralValue != "BY") &&
                   token.LiteralValue != "INTO" && token.LiteralValue != "HAVING" &&
                   token.LiteralValue != "FOR" && token.LiteralValue != "CHECK" &&
                   token.LiteralValue != "UNION" && token.LiteralValue != "WHERE" && token.LiteralValue != "JOIN" && token.LiteralValue != "INNER" &&
                   token.LiteralValue != "LEFT" && token.LiteralValue != "RIGHT" && token.LiteralValue != "OUTER" &&
                   token.LexicalCode != Tokens.Comma &&
                   token.LexicalCode != Tokens.Null)
            {
                //moved the paranthesis outside the predicate in case more predicates are grouped together (Carddas #2, #5)
                while (token.LexicalCode == Tokens.LeftParant)
                {
                    node = new TreeNode(Tokens.LeftParant, "(");
                    parent.AddChild(node);
                    token = lex.GetToken();
                    if (level++ == 0)
                    {
                        node.NodeInfo = "GROUP START";
                    }
                }

                Predicate(parent);

                //when a subSelect returns, tree can contain equal Left and right paranthese pairs but next token could be another right paranthese generated by lex.GetToken()
                //in this case we don't need to add right paranthese node to the tree here but on an upper level
                while (level > 0 && token.LexicalCode == Tokens.RightParant)
                {
                    node = new TreeNode(Tokens.RightParant, ")");
                    parent.AddChild(node);
                    token = lex.GetToken();
                    if (--level == 0)
                    {
                        node.NodeInfo = "GROUP END";
                    }
                }

                if (subSelect.Count > 0 &&
                   (syntaxTree.FindAll(parent, ")") > syntaxTree.FindAll(parent, "(") || token.LexicalCode == Tokens.RightParant))
                {
                    //this is the end of the subselect
                    subSelect.Pop();
                    break;
                }

                if (token.LiteralValue == "AND" || token.LiteralValue == "OR")
                {
                    node = new TreeNode(Tokens.Keyword, token.LiteralValue);
                    parent.AddChild(node);
                    token = lex.GetToken();
                    if (token.LexicalCode == Tokens.Keyword && (token.LiteralValue == "END" || token.LiteralValue == "START"))
                    {
                        token.LexicalCode = Tokens.Identifier;
                    }
                }
            }
        }


        void Predicate(TreeNode parent)
        {
            TreeNode node;

            node = new TreeNode(Tokens.Predicate);
            parent.AddChild(node);
            parent = node;

            if (token.LexicalCode == Tokens.Keyword && token.LiteralValue == "NOT") //NOT
            {
                node = new TreeNode(Tokens.Keyword, token.LiteralValue);
                parent.AddChild(node);
                token = lex.GetToken();
            }
            //EXISTS predicate: EXISTS (subselect)
            if (token.LexicalCode == Tokens.Keyword && token.LiteralValue == "EXISTS")//EXISTS
            {
                node = new TreeNode(Tokens.Keyword, token.LiteralValue);
                parent.AddChild(node);
                token = lex.GetToken();
                if (token.LexicalCode == Tokens.LeftParant)
                {
                    node = new TreeNode(Tokens.LeftParant, "(");
                    parent.AddChild(node);
                    token = lex.GetToken();
                    subSelect.Push(parent);
                    SelectStatement(parent); //subselect
                    if (token.LexicalCode == Tokens.RightParant)
                    {
                        node = new TreeNode(Tokens.RightParant, ")");
                        parent.AddChild(node);
                        token = lex.GetToken();
                    }
                }
                else
                {
                    OnAnalyzeError();
                }
            }
            else
            {                
                while (token.LexicalCode == Tokens.LeftParant)
                {                    
                    node = new TreeNode(Tokens.LeftParant, "(");
                    parent.AddChild(node);
                    token = lex.GetToken();
                }

                Expression(parent);

                while (token.LexicalCode == Tokens.RightParant)
                {
                    node = new TreeNode(Tokens.RightParant, ")");
                    parent.AddChild(node);
                    token = lex.GetToken();
                }

                // RIGHT Outer join (native syntax)
                if (token.LexicalCode == Tokens.JoinOperator)
                {
                    node = new TreeNode(Tokens.JoinOperator, "(+)", "RIGHT");
                    parent.AddChild(node);
                    token = lex.GetToken();
                    parent.NodeInfo = "RIGHT OUTER JOIN";
                }
                //RELATIONAL predicate: expression relational operator expression|[ANY/SOME|ALL](subselect)
                if (token.LexicalCode == Tokens.RelatOperator)//relational operator
                {
                    node = new TreeNode(Tokens.RelatOperator, token.LiteralValue);
                    parent.AddChild(node);
                    token = lex.GetToken();

                    if (token.LexicalCode == Tokens.Keyword )
                    {
                        //ASZ:DECODE function
                        if (token.LiteralValue == "DECODE" && lex.Peek().LexicalCode == Tokens.LeftParant)
                        {
                            node = new TreeNode(Tokens.Function, token.LiteralValue);
                            do
                            {
                                token = lex.GetToken();
                                node.AddChild(new TreeNode(token.LexicalCode, token.LiteralValue));
                            } while (token.LexicalCode != Tokens.RightParant && token.LexicalCode != Tokens.Null);
                            parent.AddChild(node);
                            token = lex.GetToken();
                        }
                        else if (token.LiteralValue.In("ANY", "SOME", "ALL"))//[ANY/SOME|ALL]
                        {
                            node = new TreeNode(Tokens.Keyword);
                            parent.AddChild(node);
                            token = lex.GetToken();
                        }
                    }
                    if (token.LexicalCode == Tokens.LeftParant)
                    {
                        node = new TreeNode(Tokens.LeftParant, "(");
                        parent.AddChild(node);
                        if (lex.Peek().LiteralValue == "SELECT")
                        {
                            token = lex.GetToken();
                            subSelect.Push(parent);
                            SelectStatement(parent); //subselect
                            if (token.LexicalCode == Tokens.RightParant)
                            {
                                node = new TreeNode(Tokens.RightParant, ")");
                                parent.AddChild(node);
                                token = lex.GetToken();
                            }
                        }
                        else
                        {
                            token = lex.GetToken();
                            Expression(parent);
                        }
                    }
                    else
                    {
                        Expression(parent);

                        // LEFT Outer join (native syntax)
                        if (token.LexicalCode == Tokens.JoinOperator)
                        {
                            node = new TreeNode(Tokens.JoinOperator, "(+)", "LEFT");
                            parent.AddChild(node);
                            token = lex.GetToken();
                            parent.NodeInfo = "LEFT OUTER JOIN";
                        }
                        else if (token.LiteralValue != "LEFT" && token.LiteralValue != "RIGHT")
                        {
                            if (parent.Children.Count >= 3 &&
                                parent.Children[0].Children.Count > 0 &&
                                parent.Children[0].Children[0].NodeType == Tokens.Column &&
                                parent.Children[1].NodeValue == "=" &&
                                parent.Children[2].Children.Count > 0 &&
                                parent.Children[2].Children[0].NodeType == Tokens.Column)
                            {
                                parent.AddChild(new TreeNode(Tokens.JoinOperator, "", "JOIN"));
                                parent.NodeInfo = "JOIN";
                            }
                        }
                    }
                }
                else if (token.LexicalCode == Tokens.Keyword)
                {
                    if (token.LiteralValue == "NOT" || token.LiteralValue == "IS") //NOT|IS
                    {
                        node = new TreeNode(Tokens.Keyword, token.LiteralValue);
                        parent.AddChild(node);
                        token = lex.GetToken();
                    }


                    if (token.LiteralValue == "NOT")
                    {
                        node = new TreeNode(Tokens.Keyword, token.LiteralValue);
                        parent.AddChild(node);
                        token = lex.GetToken();
                    }

                    //NULL predicate: column IS [NOT] NULL
                    if (token.LiteralValue == "NULL")//NULL
                    {
                        node = new TreeNode(Tokens.Keyword, token.LiteralValue);
                        parent.AddChild(node);
                        token = lex.GetToken();
                    }

                    //LIKE predicate: column [NOT] LIKE string constant | bind variable **actually it can be an expression
                    else if (token.LiteralValue == "LIKE")
                    {
                        node = new TreeNode(Tokens.Keyword, token.LiteralValue);
                        parent.AddChild(node);
                        token = lex.GetToken();
                        //if (token.LexicalCode == Tokens.StringConst || token.LexicalCode == Tokens.BindVariable)
                        //{
                        //    node = new TreeNode(token.LexicalCode, token.LiteralValue);
                        //    parent.AddChild(node);
                        //    token = lex.GetToken();
                        //}
                        Expression(parent);
                    }

                    //IN predicate: expression [NOT] IN (subselect)|{(bind variable)|(constant)}*|expression
                    else if (token.LiteralValue == "IN")
                    {
                        node = new TreeNode(Tokens.Keyword, token.LiteralValue);
                        parent.AddChild(node);
                        token = lex.GetToken();
                        if (token.LexicalCode == Tokens.LeftParant)
                        {
                            node = new TreeNode(Tokens.LeftParant, "(");
                            parent.AddChild(node);
                            token = lex.GetToken();
                        }
                        if (token.LexicalCode == Tokens.Keyword)//subselect
                        {
                            subSelect.Push(parent);
                            SelectStatement(parent);
                        }
                        else
                        {
                            do
                            {
                                if (token.LexicalCode == Tokens.Comma)
                                {
                                    node = new TreeNode(Tokens.Comma, ",");
                                    parent.AddChild(node);
                                    token = lex.GetToken();
                                }
                                //node = new TreeNode(token.LexicalCode, token.LiteralValue);
                                //parent.AddChild(node);
                                //token = lex.GetToken();
                                Expression(parent);

                            } while (token.LexicalCode == Tokens.Comma);
                        }

                        if (token.LexicalCode == Tokens.RightParant)
                        {
                            node = new TreeNode(Tokens.RightParant, ")");
                            parent.AddChild(node);
                            token = lex.GetToken();
                        }
                    }
                    else if (token.LiteralValue == "BETWEEN")
                    {
                        node = new TreeNode(Tokens.Keyword, token.LiteralValue);
                        parent.AddChild(node);
                        token = lex.GetToken();

                        Expression(parent);

                        //BETWEEN predicate: expression [NOT] BETWEEN expression AND expression
                        if (token.LiteralValue == "AND")
                        {
                            node = new TreeNode(Tokens.Keyword, token.LiteralValue);
                            parent.AddChild(node);
                            token = lex.GetToken();
                            Expression(parent);
                        }
                        else
                        {
                            OnAnalyzeError();
                        }
                    }
                }
            }

            //while (token.LexicalCode == Tokens.RightParant)
            //{
            //    node = new TreeNode(Tokens.RightParant, ")");
            //    parent.AddChild(node);
            //    token = lex.GetToken();
            //}

        }


        void AlterTable(TreeNode parent)
        {
            // ALTER TABLE tablename

            // DROP {colname[,]}* | 
            // ADD colname datatype|
            // RENAME {colname newname[,]}* | TABLE newname |
            // MODIFY colname [datatype]

            // [DROP] PRIMARY KEY [{(column name[,]}*]
            // [DROP] FOREIGN KEY [keyname] [{(column name[,]}* REFERENCES tablename [ON DELETE RESTRICT | CASCADE | SET NULL]]

            TreeNode node;
            token = lex.GetToken();

            if (token.LiteralValue == "TABLE")
            {
                node = new TreeNode(Tokens.Keyword, "ALTER TABLE");
                parent.AddChild(node);
                token = lex.GetToken();


                node = new TreeNode(Tokens.Table, token.LiteralValue);
                parent.AddChild(node);
                token = lex.GetToken();

                //node = new TreeNode(Tokens.Keyword, token.LiteralValue);
                //parent.AddChild(node);
                //token = lex.GetToken();
                switch (token.LiteralValue)
                {
                    // DROP {colname[,]}*
                    case "DROP":
                        node = new TreeNode(Tokens.Keyword, token.LiteralValue);
                        parent.AddChild(node);
                        token = lex.GetToken();
                        if (token.LiteralValue == "PRIMARY")
                        {
                            token = lex.GetToken();
                            node = new TreeNode(Tokens.Keyword, "PRIMARY KEY");
                            parent.AddChild(node);
                            token = lex.GetToken();
                        }
                        else if (token.LiteralValue == "FOREIGN")
                        {
                            token = lex.GetToken();
                            node = new TreeNode(Tokens.Keyword, "FOREIGN KEY");
                            parent.AddChild(node);
                            token = lex.GetToken();

                            node.AddChild(new TreeNode(Tokens.KeyName, token.LiteralValue));
                            token = lex.GetToken();
                        }
                        else
                        {
                            while (token.LexicalCode == Tokens.Identifier)
                            {
                                node = new TreeNode(Tokens.Column, token.LiteralValue);
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
                        break;

                    // ADD colname datatype
                    case "ADD":
                        node = new TreeNode(Tokens.Keyword, token.LiteralValue);
                        parent.AddChild(node);
                        token = lex.GetToken();
                        if (token.LexicalCode == Tokens.Identifier)
                        {
                            node = new TreeNode(Tokens.Column, token.LiteralValue);
                            parent.AddChild(node);
                            token = lex.GetToken();
                        }
                        else
                        {
                            OnAnalyzeError();
                        }
                        DataType(parent);

                        break;

                    // RENAME {colname newname[,]}* | TABLE newname
                    case "RENAME":
                        node = new TreeNode(Tokens.Keyword, token.LiteralValue);
                        parent.AddChild(node);
                        token = lex.GetToken();
                        while (token.LexicalCode == Tokens.Identifier)
                        {
                            node = new TreeNode(Tokens.Column, token.LiteralValue);
                            parent.AddChild(node);
                            token = lex.GetToken();

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

                        if (token.LiteralValue == "TABLE")
                        {
                            node = new TreeNode(Tokens.Keyword, token.LiteralValue);
                            parent.AddChild(node);
                            token = lex.GetToken();

                            node = new TreeNode(Tokens.Identifier, token.LiteralValue);
                            parent.AddChild(node);
                            token = lex.GetToken();
                        }
                        else if (token.LexicalCode != Tokens.Null)
                        {
                            OnAnalyzeError();
                        }
                        break;

                    // MODIFY colname [datatype]
                    case "MODIFY":
                        node = new TreeNode(Tokens.Keyword, token.LiteralValue);
                        parent.AddChild(node);
                        token = lex.GetToken();
                        node = new TreeNode(Tokens.Column, token.LiteralValue);
                        parent.AddChild(node);
                        token = lex.GetToken();

                        DataType(parent);

                        break;
                }

                if (token.LiteralValue == "PRIMARY")
                {
                    token = lex.GetToken();
                    PrimaryKey(parent);
                }
                else if (token.LiteralValue == "FOREIGN")
                {
                    token = lex.GetToken();
                    ForeignKey(parent);
                }
            }
            else if (token.LiteralValue == "USER")
            {
                node = new TreeNode(Tokens.Keyword, "ALTER USER");
                parent.AddChild(node);
                token = lex.GetToken();

                //user name node
                node = new TreeNode(Tokens.Keyword, token.LiteralValue);
                parent.AddChild(node);
                token = lex.GetToken();

                //IDENTIFIED node
                node = new TreeNode(Tokens.Keyword, token.LiteralValue);
                parent.AddChild(node);
                token = lex.GetToken();

                //BY node
                node = new TreeNode(Tokens.Keyword, token.LiteralValue);
                parent.AddChild(node);
                token = lex.GetToken();

                //password node
                node = new TreeNode(Tokens.Keyword, token.LiteralValue);
                parent.AddChild(node);
                token = lex.GetToken();
            }
            else
            {
                OnAnalyzeError();
            }
        }

        void PrimaryKey(TreeNode parent)
        {
            // PRIMARY KEY ({col[,]}*)

            TreeNode node = new TreeNode(Tokens.Keyword, "PRIMARY KEY");
            parent.AddChild(node);
            token = lex.GetToken();

            if (token.LexicalCode == Tokens.LeftParant)
            {
                node.AddChild(new TreeNode(Tokens.LeftParant, token.LiteralValue));
                token = lex.GetToken();
            }
            else
            {
                OnAnalyzeError();
            }

            while (token.LexicalCode == Tokens.Identifier)
            {
                node.AddChild(new TreeNode(Tokens.Column, token.LiteralValue));
                token = lex.GetToken();

                if (token.LexicalCode == Tokens.Comma)
                {
                    node.AddChild(new TreeNode(Tokens.Comma, token.LiteralValue));
                    token = lex.GetToken();
                }
            }

            if (token.LexicalCode == Tokens.RightParant)
            {
                node.AddChild(new TreeNode(Tokens.RightParant, token.LiteralValue));
                token = lex.GetToken();
            }
            else
            {
                OnAnalyzeError();
            }
        }

        void ForeignKey(TreeNode parent)
        {
            //FOREIGN KEY [key_name] ( {col[,]}* ) REFERENCES par_table [ON DELETE RESTRICT | CASCADE | SET NULL] ]

            TreeNode node = new TreeNode(Tokens.Keyword, "FOREIGN KEY");
            parent.AddChild(node);
            token = lex.GetToken();

            // [key name]
            //The lexical analyzer is seing the key name as user defined function because of the following paranthesis
            if (token.LexicalCode == Tokens.Identifier || token.LexicalCode == Tokens.UserDefinedFunction)
            {
                node.AddChild(new TreeNode(Tokens.KeyName, token.LiteralValue));
                token = lex.GetToken();
            }
            else
            {
                OnAnalyzeError();
            }

            if (token.LexicalCode == Tokens.LeftParant)
            {
                node.AddChild(new TreeNode(Tokens.LeftParant, token.LiteralValue));
                token = lex.GetToken();
            }
            else
            {
                OnAnalyzeError();
            }

            while (token.LexicalCode == Tokens.Identifier)
            {
                node.AddChild(new TreeNode(Tokens.Column, token.LiteralValue));
                token = lex.GetToken();

                if (token.LexicalCode == Tokens.Comma)
                {
                    node.AddChild(new TreeNode(Tokens.Comma, token.LiteralValue));
                    token = lex.GetToken();
                }
            }

            if (token.LexicalCode == Tokens.RightParant)
            {
                node.AddChild(new TreeNode(Tokens.RightParant, token.LiteralValue));
                token = lex.GetToken();
            }
            else
            {
                OnAnalyzeError();
            }

            if (token.LiteralValue == "REFERENCES")
            {
                node.AddChild(new TreeNode(Tokens.Comma, token.LiteralValue));
                token = lex.GetToken();
            }

            node.AddChild(new TreeNode(Tokens.Table, token.LiteralValue));
            token = lex.GetToken();

            //[ON DELETE RESTRICT | [CASCADE |SET NULL]]
            if (token.LiteralValue == "ON")
            {
                token = lex.GetToken();
                if (token.LiteralValue == "DELETE")
                {
                    node.AddChild(new TreeNode(Tokens.Keyword, "ON DELETE"));
                    token = lex.GetToken();
                }

                if (token.LiteralValue == "RESTRICT")
                {
                    node.AddChild(new TreeNode(Tokens.Keyword, token.LiteralValue));
                    token = lex.GetToken();
                }
                else if (token.LiteralValue == "CASCADE")
                {
                    node.AddChild(new TreeNode(Tokens.Keyword, token.LiteralValue));
                    token = lex.GetToken();
                }
                else if (token.LiteralValue == "SET")
                {
                    token = lex.GetToken();
                    if (token.LiteralValue == "NULL")
                    {
                        node.AddChild(new TreeNode(Tokens.Keyword, "SET NULL"));
                        token = lex.GetToken();
                    }
                }
                else
                {
                    OnAnalyzeError();
                }
            }
        }

        void DataType(TreeNode parent)
        {
            // [data_type_keyword] [(int_const [, int_const])] [NULL | NOT NULL | NOT NULL WITH DEFAULT | NOT NULL AUTO_INCREMENT(SEED,STEP)]
            TreeNode node = null;
            TreeNode temp1 = new TreeNode(Tokens.DataType);
            parent.AddChild(temp1);

            if (token.LiteralValue != "NULL" && token.LiteralValue != "NOT")
            {
                string nextToken = lex.Peek().LiteralValue;

                if (nextToken != "VARCHAR" && nextToken != "PRECISION")
                {
                    node = new TreeNode(Tokens.DataTypeKeyword, token.LiteralValue);
                    //asz:try to handle "datetime year to fraction()" 
                    if (token.LiteralValue == "DATETIME" && nextToken == "YEAR")
                    {
                        token = lex.GetToken();
                        node.NodeValue += " " + token.LiteralValue;
                        if (lex.Peek().LiteralValue == "TO")
                        {
                            token = lex.GetToken();
                            node.NodeValue += " " + token.LiteralValue;
                            if (lex.Peek().LiteralValue == "FRACTION")
                            {
                                token = lex.GetToken();
                                node.NodeValue += " " + token.LiteralValue;
                            }
                        }
                    }
                    temp1.AddChild(node);
                    token = lex.GetToken();
                }
                else
                {
                    if (token.LiteralValue == "LONG")
                    {
                        token = lex.GetToken();
                        node = new TreeNode(Tokens.DataTypeKeyword, "LONG VARCHAR");
                    }
                    else if (token.LiteralValue == "DOUBLE")
                    {
                        token = lex.GetToken();
                        node = new TreeNode(Tokens.DataTypeKeyword, "DOUBLE PRECISION");
                    }
                    else
                    {
                        OnAnalyzeError();
                    }
                    temp1.AddChild(node);
                    token = lex.GetToken();
                }

                if (token.LexicalCode == Tokens.LeftParant)
                {
                    node = new TreeNode(Tokens.LeftParant, token.LiteralValue);
                    temp1.AddChild(node);
                    token = lex.GetToken();

                    //asz: varchar(-1) -> varchar(max)                   
                    if (token.LiteralValue == "-" && lex.Peek().LexicalCode == Tokens.NumericConst) 
                    {
                        token = lex.GetToken();
                        token.SetToken(token.LexicalCode, "-" + token.LiteralValue, token.IndentLevel);
                    }

                    if (token.LexicalCode == Tokens.NumericConst)
                    {
                        node = new TreeNode(Tokens.NumericConst, token.LiteralValue);
                        temp1.AddChild(node);
                        token = lex.GetToken();
                    }

                    if (token.LexicalCode == Tokens.Comma)
                    {
                        node = new TreeNode(Tokens.Comma, token.LiteralValue);
                        temp1.AddChild(node);
                        token = lex.GetToken();

                        if (token.LexicalCode == Tokens.NumericConst)
                        {
                            node = new TreeNode(Tokens.NumericConst, token.LiteralValue);
                            temp1.AddChild(node);
                            token = lex.GetToken();
                        }
                    }

                    if (token.LexicalCode == Tokens.RightParant)
                    {
                        node = new TreeNode(Tokens.RightParant, token.LiteralValue);
                        temp1.AddChild(node);
                        token = lex.GetToken();
                    }
                    else
                    {
                        OnAnalyzeError();
                    }
                }
            }

            // [NULL |NOT NULL | NOT NULL WITH DEFAULT | NOT NULL AUTO_INCREMENT(SEED,STEP)]
            if (token.LiteralValue == "NULL")
            {
                node = new TreeNode(Tokens.Keyword, "NULL");
                parent.AddChild(node);
                token = lex.GetToken();
            }
            else if (token.LiteralValue == "NOT")
            {
                token = lex.GetToken();
                if (token.LiteralValue == "NULL")
                    token = lex.GetToken();
                if (token.LiteralValue == "WITH")
                {
                    token = lex.GetToken();
                    if (token.LiteralValue == "DEFAULT")
                    {
                        node = new TreeNode(Tokens.Keyword, "NOT NULL WITH DEFAULT");
                        parent.AddChild(node);
                        token = lex.GetToken();
                    }
                }
                else
                {
                    node = new TreeNode(Tokens.Keyword, "NOT NULL");
                    parent.AddChild(node);
                    if (token.LiteralValue == "AUTO_INCREMENT")
                    {
                        node = new TreeNode(Tokens.Keyword, "AUTO_INCREMENT");
                        parent.AddChild(node);
                        token = lex.GetToken();
                        if (token.LexicalCode == Tokens.LeftParant)
                        {
                            node = new TreeNode(Tokens.LeftParant, "(");
                            parent.AddChild(node);
                        }
                        else
                        {
                            OnAnalyzeError();
                        }
                        token = lex.GetToken();
                        if (token.LexicalCode == Tokens.NumericConst)
                        {
                            node = new TreeNode(Tokens.NumericConst, token.LiteralValue);
                            parent.AddChild(node);
                        }
                        else
                        {
                            OnAnalyzeError();
                        }
                        token = lex.GetToken();
                        if (token.LexicalCode == Tokens.Comma)
                        {
                            node = new TreeNode(Tokens.Comma, ",");
                            parent.AddChild(node);
                        }
                        else
                        {
                            OnAnalyzeError();
                        }
                        token = lex.GetToken();
                        if (token.LexicalCode == Tokens.NumericConst)
                        {
                            node = new TreeNode(Tokens.NumericConst, token.LiteralValue);
                            parent.AddChild(node);
                        }
                        else
                        {
                            OnAnalyzeError();
                        }
                        token = lex.GetToken();
                        if (token.LexicalCode == Tokens.RightParant)
                        {
                            node = new TreeNode(Tokens.RightParant, ")");
                            parent.AddChild(node);
                        }
                        else
                        {
                            OnAnalyzeError();
                        }
                        token = lex.GetToken();
                    }
                }
            }
        }

        private void StoreStatement(TreeNode parent)
        {
            // STORE cmd_name cmd_text
            TreeNode node = new TreeNode(Tokens.StoreCommand, "STORE");
            parent.AddChild(node);
            token = lex.GetToken();

            if (token.LexicalCode == Tokens.Identifier)
            {
                node.AddChild(new TreeNode(Tokens.Identifier, token.LiteralValue));
                token = lex.GetToken();
            }
            else
            {
                OnAnalyzeError();
            }

            switch (token.LiteralValue)
            {
                case "SELECT": SelectStatement(node); break;
                case "INSERT": InsertStatement(node); break;
                case "UPDATE": UpdateStatement(node); break;
                case "DELETE": DeleteStatement(node); break;
                case "ROLLBACK": Rollback(node); break;
                default: OnAnalyzeError(); break;
            }
        }

        private void CurrentOperator(TreeNode parent)
        {
            // CURRENT 
            // [YEAR TO YEAR | TO MONTH | TO DAY | TO HOUR | TO MINUTE | TO SECOND | TO FRACTION [(scale)]]
            // [MONTH TO MONTH | TO DAY | TO HOUR | TO MINUTE | TO SECOND | TO FRACTION [(scale)]]
            // [DAY TO DAY | TO HOUR | TO MINUTE | TO SECOND | TO FRACTION [(scale)]]
            // [HOUR TO HOUR | TO MINUTE | TO SECOND | TO FRACTION [(scale)]]
            // [MINUTE TO MINUTE | TO SECOND | TO FRACTION [(scale)]]
            // [SECOND TO SECOND | TO FRACTION [(scale)]]
            // [FRACTION TO FRACTION [(scale)]]
            // scale = integer {1,5}
            TreeNode node = new TreeNode(Tokens.Keyword, "CURRENT");
            parent.AddChild(node);
            parent = node;
            token = lex.GetToken();
            if (token.LiteralValue.In("YEAR", "MONTH", "DAY", "HOUR", "MINUTE", "SECOND", "FRACTION"))
            {
                node = new TreeNode(Tokens.Keyword, token.LiteralValue);
                parent.AddChild(node);
                token = lex.GetToken();
                if (token.LiteralValue == "TO")
                {
                    node = new TreeNode(Tokens.Keyword, token.LiteralValue);
                    parent.AddChild(node);
                    token = lex.GetToken();
                    if (token.LexicalCode == Tokens.Keyword)
                    {
                        node = new TreeNode(Tokens.Keyword, token.LiteralValue);
                        parent.AddChild(node);
                        token = lex.GetToken();
                        //scale
                        if (token.LexicalCode == Tokens.LeftParant)
                        {
                            node = new TreeNode(Tokens.LeftParant, token.LiteralValue);
                            parent.AddChild(node);
                            token = lex.GetToken();
                            if (token.LexicalCode == Tokens.NumericConst)
                            {
                                node = new TreeNode(Tokens.NumericConst, token.LiteralValue);
                                parent.AddChild(node);
                                token = lex.GetToken();
                                if (token.LexicalCode == Tokens.RightParant)
                                {
                                    node = new TreeNode(Tokens.RightParant, token.LiteralValue);
                                    parent.AddChild(node);
                                }
                            }
                        }
                    }
                }
                else
                {
                    OnAnalyzeError();
                }
            }

        }
    }

}
