using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using fecher.SqlTranslator;
using fecher.Common;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace fecher.DataProvider
{
    public class SqlWrapperCommand : IDbCommand
    {
        /// <summary>
        /// Current IDbCommand.
        /// </summary>
        internal IDbCommand myCommand;

        internal SqlTranslator.SqlTranslator mySqlTranslator;

        #region Constructors
        
        public SqlWrapperCommand()
        {
            myCommand = new SqlCommand();
        }

        public SqlWrapperCommand(string cmdText)
        {
            myCommand = new SqlCommand(cmdText);
        }

        public SqlWrapperCommand(string cmdText, SqlWrapperConnection connection)
        {
            myCommand = new SqlCommand(cmdText, (SqlConnection)connection.myConnection);
        }

        public SqlWrapperCommand(string cmdText, SqlWrapperConnection connection, SqlTransaction txn)
        {
            myCommand = new SqlCommand(cmdText, (SqlConnection)connection.myConnection, txn);
        }
        #endregion

        #region IDbCommand Members

        public void Cancel()
        {
            myCommand.Cancel();
        }

        public string CommandText
        {
            get { return myCommand.CommandText; }
            set 
            { 
                myCommand.CommandText = value;
                Translate();
                Globals.TraceStatement(myCommand.Connection.Database, value, myCommand.CommandText);
            }
        }

        public int CommandTimeout
        {
            get { return myCommand.CommandTimeout; }
            set { myCommand.CommandTimeout = value; }
        }

        public CommandType CommandType
        {
            get { return myCommand.CommandType; }
            set { myCommand.CommandType = value; }
        }

        public IDbConnection Connection
        {
            get { return myCommand.Connection; }
            set { myCommand.Connection = value; }
        }

        public IDbDataParameter CreateParameter()
        {
            return myCommand.CreateParameter();
        }

        public int ExecuteNonQuery()
        {
            Globals.TraceParameters(myCommand.Connection.Database, myCommand.Parameters);
            //Sajo.CmdString(myCommand, true);
            int affectedRows = myCommand.ExecuteNonQuery();
            switch (affectedRows)
            {
                case 0:
                    string parameter = String.Empty;
                    //SqlBase throws an exception when an invalid ROWID is given in the where clause, except when ROWID is null
                    if (HasConditionOnROWID(myCommand.CommandText, ref parameter))
                    {
                        if (parameter != String.Empty && myCommand.Parameters.Contains(parameter))
                        {
                            IDataParameter dataParam = (IDataParameter)myCommand.Parameters[parameter];
                            if (dataParam.Value == System.DBNull.Value)
                            {
                                return affectedRows;
                            }
                        }
                        //throw new PPJ.Runtime.Sql.SalSqlError(806, 0, "Invalid ROWID");
                    }
                    break;

                case -1:
                    string commandText = myCommand.CommandText.ToUpper();
                    if (commandText.StartsWith("CREATE TABLE") ||
                       commandText.StartsWith("DROP TABLE"))
                    {

                        Globals.LoadSchema(Globals.LoadSettings(myCommand.Connection.Database), myCommand);
                    }
                    break;
            }
            return affectedRows;
        }

        public IDataReader ExecuteReader(CommandBehavior behavior)
        {
            Globals.TraceParameters(myCommand.Connection.Database, myCommand.Parameters);
            //Sajo.CmdString(myCommand, true);
            return myCommand.ExecuteReader(behavior);
        }

        public IDataReader ExecuteReader()
        {
            Globals.TraceParameters(myCommand.Connection.Database, myCommand.Parameters);
            return myCommand.ExecuteReader();
        }

        public object ExecuteScalar()
        {
            Globals.TraceParameters(myCommand.Connection.Database, myCommand.Parameters);
            return myCommand.ExecuteScalar();
        }

        public IDataParameterCollection Parameters
        {
            get { return myCommand.Parameters; }
        }

        public void Prepare()
        {
            myCommand.Prepare();
        }

        public IDbTransaction Transaction
        {
            get { return myCommand.Transaction; }
            set { myCommand.Transaction = value; }
        }

        public UpdateRowSource UpdatedRowSource
        {
            get { return myCommand.UpdatedRowSource; }
            set { myCommand.UpdatedRowSource = value; }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            myCommand.Dispose();
        }

        #endregion

        #region SqlTranslator
        private void Translate()
        {
            mySqlTranslator = new fecher.SqlTranslator.SqlTranslator();

            //PPJ:TODO:PJ: CA-Kommentare entfernen, da sonst Fehler auftreten; Am Ende nicht mehr benötigt
            //myCommand.CommandText = Regex.Replace(myCommand.CommandText, @"\/\/CA\:.*\r\n", "");
            //if (myCommand.CommandText.Contains(@"//CA:"))
            //{
            //    ;
            //}
            
            string sql = mySqlTranslator.TranslateSql(Globals.Settings[myCommand.Connection.Database].SourceDatabase, DatabaseBrand.SqlServer, myCommand.CommandText, Globals.Settings[myCommand.Connection.Database]);
            if (!String.IsNullOrEmpty(sql))
            {
                myCommand.CommandText = sql;
            }

            //if (!String.IsNullOrEmpty(Settings.DateFormat))
            //{
            //    myCommand.CommandText = "SET DATEFORMAT " + Settings.DateFormat + "\r\nGO\r\n " + myCommand.CommandText;
            //}
        }
        #endregion

        private bool HasConditionOnROWID(string sql, ref string parameter)
        {
            //Regex reg1 = new Regex(".*(WHERE.*?ROWID.*?)(GROUP|HAVING|ORDER|FOR|UNION|INTO|$)", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            Regex reg = new Regex(@".*WHERE.*?ROWID\s*=\s*(?<param>@\w*).*", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            Match match = reg.Match(sql);
            if (match.Success)
            {
                parameter = match.Groups["param"].Value;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
