using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data;
using System.Data.OracleClient;
using Gupta.SQLBase.Data;

namespace SQLTranslatorTest
{
    class ExecuteCompareStatement
    {
        #region Members
        private SqlConnection sqlServerConn;
        private SQLBaseConnection sqlBaseConn;
        private OracleConnection oracleConn;
        #endregion

        #region Properties
        public SqlConnection SqlServerConn
        {
            get 
            {
                if (sqlServerConn == null)
                {
                    sqlServerConn = new SqlConnection(Globals.WkSpace.SqlServerConnectionString);
                    sqlServerConn.Open();
                }
                return sqlServerConn; 
            }
        }
        
        public SQLBaseConnection SqlBaseConn
        {
            get
            {
                if (sqlBaseConn == null)
                {
                    sqlBaseConn = new SQLBaseConnection(Globals.WkSpace.SqlBaseConnectionString);
                    sqlBaseConn.Open();
                }
                return sqlBaseConn;
            }
        }
        
        public OracleConnection OracleConn
        {
            get
            {
                if (oracleConn == null)
                {
                    oracleConn = new OracleConnection(Globals.WkSpace.OracleConnectionString);
                    oracleConn.Open();
                }
                return oracleConn;
            }
        }
        #endregion

        #region Methods
        public void ExecuteSQLBaseSQLServer(string statement)
        {
            SqlCommand sqlcmd = new SqlCommand(statement, SqlServerConn);
            sqlcmd.ExecuteNonQuery();
            sqlcmd.Dispose();            
        }

        public bool ExecuteCompareSQLBaseSQLServer(string statement1,string statement2)
        {
            if (statement1.Trim().Substring(0, 6).ToLower() == "select")
            {
                DataTable sqldt = new DataTable();
                DataTable sqlbasedt = new DataTable();
                SqlDataAdapter sqldataadapter = new SqlDataAdapter(statement1, SqlServerConn);
                SQLBaseDataAdapter sqlbasedataadapter = new SQLBaseDataAdapter(statement2, SqlBaseConn);
                sqldataadapter.Fill(sqldt);
                sqlbasedataadapter.Fill(sqlbasedt);
                sqldataadapter.Dispose();
                sqlbasedataadapter.Dispose();
                return CompareDataTable(sqldt, sqlbasedt);
            }
            else
            {
                ExecuteSQLBaseSQLServer(statement1);
                return true;
            }
        }

        public void ExecuteSQLBaseOracle(string statement)
        {
            OracleCommand oraclecmd = new OracleCommand(statement, OracleConn);
            oraclecmd.ExecuteNonQuery();
            oraclecmd.Dispose();
        }

        public bool ExecuteCompareSQLBaseOracle(string statement1, string statement2)
        {
            if (statement1.Trim().Substring(0, 6).ToLower() == "select")
            {
                DataTable oracledt = new DataTable();
                DataTable sqlbasedt = new DataTable();
                OracleDataAdapter oracledataadapter = new OracleDataAdapter(statement1, OracleConn);
                SQLBaseDataAdapter sqlbasedataadapter = new SQLBaseDataAdapter(statement2, SqlBaseConn);
                oracledataadapter.Fill(oracledt);
                sqlbasedataadapter.Fill(sqlbasedt);
                oracledataadapter.Dispose();
                sqlbasedataadapter.Dispose();
                return CompareDataTable(oracledt, sqlbasedt);
            }
            else
            {
                ExecuteSQLBaseOracle(statement1);
                return true;
            }
        }

        public bool CompareDataTable(DataTable dt1, DataTable dt2)
        {
            try
            {
                object type;

                if (dt1.Rows.Count == dt2.Rows.Count)
                {
                    //MU --> don't compare the columns with ROWID
                    if (dt1.Columns.Contains("ROWID"))
                    {
                        dt1.Columns.Remove("ROWID");
                    }

                    if (dt2.Columns.Contains("ROWID"))
                    {
                        dt2.Columns.Remove("ROWID");
                    }
                    //
                    for (int i = 0; i < dt1.Rows.Count; i++)
                        for (int j = 0; j < dt1.Columns.Count; j++)
                        {
                            type = dt1.Rows[i][j].GetType();
                            if (dt1.Rows[i][j] == null)
                            {
                                dt1.Rows[i][j] = "";
                            }
                            switch (type.ToString())
                            {
                                case "System.Decimal":
                                    if (Convert.ToDecimal(dt1.Rows[i][j]) != Convert.ToDecimal(dt2.Rows[i][j]))
                                    {
                                        return false;
                                    }
                                    break;
                                case "System.DateTime":
                                    if (Convert.ToDateTime(dt1.Rows[i][j]).Date != Convert.ToDateTime(dt2.Rows[i][j]).Date)
                                    {
                                        return false;
                                    }
                                    break;
                                case "System.Byte":
                                    if (Convert.ToByte(dt1.Rows[i][j]) != Convert.ToByte(dt2.Rows[i][j]))
                                    {
                                        return false;
                                    }
                                    break;
                                default:
                                    if (dt1.Rows[i][j].ToString() != dt2.Rows[i][j].ToString())
                                    {
                                        return false;
                                    }
                                    break;
                            }
                        }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                throw new DataTablesException(ex.Message, ex);
            }
        }
        #endregion
    }
}
