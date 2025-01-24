using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.OracleClient;
using Gupta.SQLBase.Data;
using fecher.Common;

namespace SQLTranslatorTest
{
    public partial class TestConnectionForm : Form
    {
        private DatabaseBrand dbBrand;
        
        public TestConnectionForm(DatabaseBrand db)
        {
            InitializeComponent();
            dbBrand = db;
        }

        #region Form Events

        private void Form2_Load(object sender, EventArgs e)
        {
            //ConnectionStrings.database = "";
            switch (dbBrand)
            {
                case DatabaseBrand.SqlBase:
                    serverinstancetbx.Enabled = false;
                    label4.Text = "SQLBase Server";
                    //MU -->
                    databasetbx.Text = Globals.SqlBaseConnProp.ContainsKey("DATA SOURCE") ? Globals.SqlBaseConnProp["DATA SOURCE"] : String.Empty;
                    usertbx.Text = Globals.SqlBaseConnProp.ContainsKey("USERID") ? Globals.SqlBaseConnProp["USERID"] : String.Empty;
                    passwordtbx.Text = Globals.SqlBaseConnProp.ContainsKey("PASSWORD") ? Globals.SqlBaseConnProp["PASSWORD"] : String.Empty;
                    //<--
                    break;

                case DatabaseBrand.SqlServer:
                    label4.Text = "SQL Server";
                    //MU -->
                    serverinstancetbx.Text = Globals.SqlServerConnProp.ContainsKey("DATA SOURCE") ? Globals.SqlServerConnProp["DATA SOURCE"] : String.Empty;
                    databasetbx.Text = Globals.SqlServerConnProp.ContainsKey("INITIAL CATALOG") ? Globals.SqlServerConnProp["INITIAL CATALOG"] : String.Empty;
                    usertbx.Text = Globals.SqlServerConnProp.ContainsKey("USER ID") ? Globals.SqlServerConnProp["USER ID"] : String.Empty;
                    passwordtbx.Text = Globals.SqlServerConnProp.ContainsKey("PASSWORD") ? Globals.SqlServerConnProp["PASSWORD"] : String.Empty;
                    //<--
                    serverinstancetbx.Text = "NB-PB-RH06";
                    databasetbx.Text = "TVN1MSSQL";
                    break;

                case DatabaseBrand.Oracle:
                    serverinstancetbx.Enabled = false;
                    label4.Text = "Oracle Server";
                    //MU -->
                    databasetbx.Text = Globals.OracleConnProp.ContainsKey("DATA SOURCE") ? Globals.OracleConnProp["DATA SOURCE"] : String.Empty;
                    usertbx.Text = Globals.OracleConnProp.ContainsKey("USER ID") ? Globals.OracleConnProp["USER ID"] : String.Empty;
                    passwordtbx.Text = Globals.OracleConnProp.ContainsKey("PASSWORD") ? Globals.OracleConnProp["PASSWORD"] : String.Empty;
                    //<--
                    break;
            }
        }

        #endregion

        #region Buttons Click

        private void TestConnectionbtn_Click(object sender, EventArgs e)
        {
            switch (dbBrand)
            {
                case DatabaseBrand.SqlBase:
                    try
                    {
                        if (databasetbx.Text.Trim().Length == 0)
                        {
                            MessageBox.Show("Please set the database", "Error", MessageBoxButtons.OK);
                        }
                        else
                        {
                            if ((usertbx.Text.Trim().Length == 0) && (passwordtbx.Text.Trim().Length == 0))
                            {
                                Globals.WkSpace.SqlBaseConnectionString = "Data Source=" + databasetbx.Text + ";Persist Security Info=False";
                            }
                            else
                                Globals.WkSpace.SqlBaseConnectionString = "Data Source=" + databasetbx.Text + ";Persist Security Info=False;" + "UserId=" + usertbx.Text + ";Password=" + passwordtbx.Text; 
                            SQLBaseConnection sqlBaseConn = new SQLBaseConnection(Globals.WkSpace.SqlBaseConnectionString);
                            sqlBaseConn.Open();
                            sqlBaseConn.Close();
                            Globals.SqlBaseConnProp["DATA SOURCE"] = databasetbx.Text;
                            MessageBox.Show("Connection succesed", "Connection established", MessageBoxButtons.OK);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Connection failed. " + ex.Message, "Error", MessageBoxButtons.OK);
                    }
                    break;

                case DatabaseBrand.SqlServer:
                    try
                    {
                        if ((serverinstancetbx.Text.Trim().Length == 0) || (databasetbx.Text.Trim().Length == 0))
                        {
                            MessageBox.Show("Please set the server instance and the database", "Error", MessageBoxButtons.OK);
                        }
                        else
                        {
                            if ((usertbx.Text.Trim().Length == 0) && (passwordtbx.Text.Trim().Length == 0))
                            {
                                Globals.WkSpace.SqlServerConnectionString = @"Data Source=" + serverinstancetbx.Text + ";Initial Catalog=" + databasetbx.Text + ";Integrated Security=SSPI";
                            }
                            else
                            {
                                Globals.WkSpace.SqlServerConnectionString = @"Data Source=" + serverinstancetbx.Text + ";Initial Catalog=" + databasetbx.Text + ";User ID=" + usertbx.Text + ";Password=" + passwordtbx.Text;
                            }
                            SqlConnection sqlConn = new SqlConnection(Globals.WkSpace.SqlServerConnectionString);
                            sqlConn.Open();
                            sqlConn.Close();
                            Globals.SqlServerConnProp["INITIAL CATALOG"] = databasetbx.Text;
                            //MessageBox.Show("Connection succesed", "Connection established", MessageBoxButtons.OK);
                            this.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Connection failed. ", "Error", MessageBoxButtons.OK);
                    }
                    break;

                case DatabaseBrand.Oracle:
                    try
                    {
                        if(databasetbx.Text.Trim().Length == 0)
                        {
                            MessageBox.Show("Please set the database", "Error", MessageBoxButtons.OK);
                        }
                        else
                        {
                            Globals.WkSpace.OracleConnectionString = "Data Source=" + databasetbx.Text + ";User ID=" + usertbx.Text + ";Password=" + passwordtbx.Text + ";Integrated Security=no";
                            OracleConnection oracleConn = new OracleConnection(Globals.WkSpace.OracleConnectionString);
                            oracleConn.Open();
                            oracleConn.Close();
                            Globals.OracleConnProp["DATA SOURCE"] = databasetbx.Text;
                            MessageBox.Show("Connection succesed", "Connection established", MessageBoxButtons.OK);
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Connection failed. ", "Error", MessageBoxButtons.OK);
                    }
                    break;
            }
        }

        #endregion 
    }
}