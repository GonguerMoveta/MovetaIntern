using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using fecher.Common;
using System.Data;
using System.Data.SqlClient;
using System.Data.OracleClient;
using System.Collections.Specialized;

namespace SQLTranslatorTest
{
    public enum TranslatorType
    {
        SqlTranslator,
        SpTranslator
    }

    public class TranslateStatements
    {
        #region Class Variables
        fecher.SqlTranslator.SqlTranslator sqltranslate;
        fecher.SpTranslator.SpTranslatorSqlServer spTranslator;
        TranslatorType translatorType;
        #endregion

        public TranslateStatements()
        {
            this.translatorType = TranslatorType.SqlTranslator;
        }

        public TranslateStatements(TranslatorType type)
        {
            this.translatorType = type;
        }

        #region Class Methods

        public string[] TranslateSQLBaseToSQLServer(string[] initial)
        {
            Globals.Settings.ReadStructure = true;
            ReadColumns(fecher.Common.DatabaseBrand.SqlServer);
            Globals.Settings.TableOwner = "dbo";
            
            int i = 0;
            string[] translated = new string[initial.Length];
            try
            {
                for (i = 0; i < initial.Length; i++)
                {
                    if (translatorType == TranslatorType.SqlTranslator)
                    {
                        sqltranslate = new fecher.SqlTranslator.SqlTranslator();
                        translated[i] = sqltranslate.TranslateSql(fecher.Common.DatabaseBrand.SqlServer, initial[i], Globals.Settings);
                    }
                    else
                    {
                        spTranslator = new fecher.SpTranslator.SpTranslatorSqlServer();
                        translated[i] = spTranslator.Translate(initial[i]);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Translating statement " + initial[i] + " failed \r\n\r\nDetails: " + ex.Message);
                translated[i] = "";
            }
            return translated;
        }

        public string[] TranslateSQLBaseToOracle(string[] initial)
        {
            ReadColumns(fecher.Common.DatabaseBrand.Oracle);

            int i = 0;
            string[] translated = new string[initial.Length];
            try
            {
                for (i = 0; i < initial.Length; i++)
                {
                    sqltranslate = new fecher.SqlTranslator.SqlTranslator();
                    translated[i] = sqltranslate.TranslateSql(fecher.Common.DatabaseBrand.Oracle, initial[i], Globals.Settings);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Translating statement " + initial[i] + " failed \r\n\r\nDetails: " + ex.Message);
                translated[i] = "";
            }
            return translated;
        }

        public string[] TranslateInformixToSQLServer(string[] initial)
        {
            Globals.Settings.ReadStructure = true;
            ReadColumns(fecher.Common.DatabaseBrand.SqlServer);
            Globals.Settings.ConvertConcatOperandsToString = true;
            Globals.Settings.TableOwner = "dbo";

            int i = 0;
            string[] translated = new string[initial.Length];
            try
            {
                for (i = 0; i < initial.Length; i++)
                {
                    if (translatorType == TranslatorType.SqlTranslator)
                    {
                        sqltranslate = new fecher.SqlTranslator.SqlTranslator();
                        translated[i] = sqltranslate.TranslateSql(DatabaseBrand.Informix, DatabaseBrand.SqlServer, initial[i], Globals.Settings);
                    }
                    //else
                    //{
                    //    spTranslator = new fecher.SpTranslator.SpTranslatorSqlServer();
                    //    translated[i] = spTranslator.Translate(initial[i]);
                    //}
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Translating statement " + initial[i] + " failed \r\n\r\nDetails: " + ex.Message);
                translated[i] = "";
            }
            return translated;
        }

        public string[] TranslateAccessToSQLServer(string[] initial)
        {
            Globals.Settings.ReadStructure = true;
            ReadColumns(fecher.Common.DatabaseBrand.SqlServer);
            Globals.Settings.ConvertConcatOperandsToString = true;
            Globals.Settings.TableOwner = "dbo";

            int i = 0;
            string[] translated = new string[initial.Length];
            try
            {
                for (i = 0; i < initial.Length; i++)
                {
                    if (translatorType == TranslatorType.SqlTranslator)
                    {
                        sqltranslate = new fecher.SqlTranslator.SqlTranslator();
                        translated[i] = sqltranslate.TranslateSql(DatabaseBrand.Access, DatabaseBrand.SqlServer, initial[i], Globals.Settings);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Translating statement " + initial[i] + " failed \r\n\r\nDetails: " + ex.Message);
                translated[i] = "";
            }
            return translated;
        }

        public void ReadColumns(fecher.Common.DatabaseBrand dbBrand)
        {
            if (Globals.Settings.ReadStructure)
            {
                IDbConnection conn;
                Globals.Settings.DbStructure = new ColumnCollection();
                string cmdTxt = string.Empty;

                switch (dbBrand)
                {
                    case DatabaseBrand.SqlServer:
                        {
                            cmdTxt = @"SELECT table_name, column_name, 
case when data_type = 'nvarchar' and character_maximum_length > 0 then data_type + '(' + LTRIM(str(character_maximum_length)) + ')' else data_type end as data_type, collation_name
FROM INFORMATION_SCHEMA.COLUMNS";
                            conn = new SqlConnection(Globals.WkSpace.SqlServerConnectionString);
                            break;
                        }

                    case DatabaseBrand.Oracle:
                        {
                            conn = new OracleConnection(Globals.WkSpace.OracleConnectionString);
                            cmdTxt = "SELECT table_name, column_name, data_type FROM all_tab_columns";
                            break;
                        }

                    default:
                        {
                            conn = null;
                            break;
                        }
                }

                try
                {
                    conn.Open();
                    IDbCommand command = conn.CreateCommand();
                    command.CommandText = cmdTxt;
                    IDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Globals.Settings.DbStructure.Add(((string)reader["column_name"]).ToUpper(), ((string)reader["table_name"]).ToUpper(), (string)reader["data_type"], Convert.ToString(reader["collation_name"]));
                    }

                    reader.Close();

                    fecher.Common.Globals.LoadSchema(Globals.Settings, command);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error on ReadColumns", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion

    }
}
