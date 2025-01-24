using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Specialized;
using fecher.Common;

namespace fecher.DataProvider
{
    public class SqlWrapperConnection : IDbConnection
    {
        /// <summary>
        /// Database connection.
        /// </summary>
        internal IDbConnection myConnection;

        

        #region Constructors
 
        public SqlWrapperConnection()
        {
            myConnection = new SqlConnection();
        }

        public SqlWrapperConnection(string sConnString)
        {
            myConnection = new SqlConnection(sConnString);
        }

        #endregion

        #region IDbConnection Members

        public IDbTransaction BeginTransaction(IsolationLevel il)
        {
            return myConnection.BeginTransaction(il);
        }

        public IDbTransaction BeginTransaction()
        {
            return myConnection.BeginTransaction();
        }

        public void ChangeDatabase(string databaseName)
        {
            myConnection.ChangeDatabase(databaseName);
        }

        public void Close()
        {
            myConnection.Close();
        }

        public string ConnectionString
        {
            get { return myConnection.ConnectionString; }
            set { myConnection.ConnectionString = value;}
        }

        public int ConnectionTimeout
        {
            get { return myConnection.ConnectionTimeout; }
        }

        public IDbCommand CreateCommand()
        {
            return new SqlWrapperCommand(String.Empty, this);
        }

        public string Database
        {
            get { return myConnection.Database; }
        }

        public void Open()
        {
            
            myConnection.Open();

            DatabaseSettings dbSettings = Globals.LoadSettings(myConnection.Database);

            if (!String.IsNullOrEmpty(dbSettings.ConcatNullYieldsNullOption))
            {
                IDbCommand command = myConnection.CreateCommand();
                command.CommandText = "SET CONCAT_NULL_YIELDS_NULL " + dbSettings.ConcatNullYieldsNullOption;
                command.ExecuteNonQuery();
            }

            lock (dbSettings)
            {
                if (dbSettings.ReadStructure && dbSettings.DbStructure == null)
                {
                    dbSettings.DbStructure = new ColumnCollection();

                    IDbCommand command = myConnection.CreateCommand();
                    command.CommandText = "SELECT table_name, column_name, data_type, collation_name, table_schema FROM INFORMATION_SCHEMA.COLUMNS";
                    IDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        dbSettings.DbStructure.Add((string)reader["column_name"], (string)reader["table_name"], (string)reader["table_schema"], (string)reader["data_type"], Convert.ToString(reader["collation_name"]));
                    }
                    reader.Close();

                    Globals.LoadSchema(dbSettings, command);
                }
            }

            if (!String.IsNullOrEmpty(dbSettings.DateFormat))
            {
                IDbCommand command = myConnection.CreateCommand();
                command.CommandText = "SET DATEFORMAT " + dbSettings.DateFormat;
                command.ExecuteNonQuery();
            }
        }

        public ConnectionState State
        {
            get { return myConnection.State; }
        }
        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            myConnection.Dispose();
        }

        #endregion
    }
}
