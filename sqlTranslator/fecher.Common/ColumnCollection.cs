using System;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Text;
using System.Linq;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.IO;

namespace fecher.Common
{
    public class ColumnCollection : List<Column>
    {
        private bool isInitialized = false;
        public bool IsInitialized
        {
            get { return isInitialized; }
        }

        public string this[string columnName]
        {
            get
            {
                var table =
                    from col in this
                    where col.Name.ToUpper() == columnName.ToUpper()
                    select col.Table;
                
                string tables = String.Empty;
                foreach (string t in table)
                {
                    if (tables != String.Empty)
                    {
                        tables += ",";
                    }
                    tables += t;
                }

                return tables;
            }
        }

        public string this[string columnName, string table]
        {
            get
            {
                if (!String.IsNullOrEmpty(table))
                {
                    table = table.RemoveBrackets().GetUnqualifiedValue().ToUpper();
                }

                string type = (
                    from col in this
                    where col.Name.ToUpper() == columnName.ToUpper().RemoveBrackets() && col.Table.ToUpper() == table
                    select col.Type).FirstOrDefault();

                return type;
            }
        }

        public string this[int columnIndex, string table]
        {
            get
            {
                string type = (
                    from col in this
                    where col.Table.ToUpper() == table.ToUpper()
                    select col.Type).ElementAtOrDefault(columnIndex);

                return type;
            }
        }

        public void Add(string columnName, string table, string type, string collation)
        {
            this.Add(new Column(columnName, table, "", type, collation));
        }

        public void Add(string columnName, string table, string schema, string type, string collation)
        {
            this.Add(new Column(columnName, table, schema, type, collation));
        }

        public void Add(string columnName, string table, string type, object dataPrecision, object dataScale, string tbCreator)
        {
            this.Add(new Column(columnName, table, type, dataPrecision, dataScale, tbCreator));
        }

        public void InitOracle(string database, string user, string password)
        {
            OracleConnection conn;
            string connectionString = String.Format("Data Source={0};User Id={1};Password={2};", database, user, password);
            string cmdTxt = "SELECT NAME, TBNAME, OCOLTYPE, LENGTH, SCALE, TBCREATOR FROM ";

            conn = new OracleConnection(connectionString);
            cmdTxt += "syscolumnstot WHERE tbcreator = 'SYSADM' OR tbcreator = 'WRZLBRMF'";

            try
            {
                conn.Open();
                OracleCommand command = conn.CreateCommand();
                command.CommandText = cmdTxt;
                OracleDataReader reader = command.ExecuteReader();

                this.Clear();

                while (reader.Read())
                {
                    this.Add((string)reader["NAME"], (string)reader["TBNAME"], (string)reader["OCOLTYPE"], reader["LENGTH"], reader["SCALE"], (string)reader["TBCREATOR"]);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                //System.IO.StreamWriter wr = new System.IO.StreamWriter("C:\\SqlTranlatorExc.txt", true);
                System.IO.StreamWriter wr = new System.IO.StreamWriter(Path.Combine(Path.GetTempPath(), "SqlTranlatorExc.txt"), true);
                wr.Write(ex.Message);
                wr.Close();
                isInitialized = false;
                return;
            }
            isInitialized = true;
        }

        public void InitSqlServer(string datasource, string database, string user, string password)
        { 
            SqlConnectionStringBuilder connectionString = new SqlConnectionStringBuilder();
            connectionString.DataSource = datasource;
            connectionString.InitialCatalog = database;
            connectionString.UserID = user;
            connectionString.Password = password;

            SqlConnection conn = new SqlConnection(connectionString.ConnectionString);

            string cmdTxt = "SELECT table_name, column_name, data_type, collation_name FROM INFORMATION_SCHEMA.COLUMNS ";

            try
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.CommandText = cmdTxt;
                SqlDataReader reader = command.ExecuteReader();

                this.Clear();

                while (reader.Read())
                {
                    this.Add((string)reader["column_name"], (string)reader["table_name"], (string)reader["data_type"], Convert.ToString(reader["collation_name"]));
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                //System.IO.StreamWriter wr = new System.IO.StreamWriter("C:\\SqlTranlatorExc.txt", true);
                System.IO.StreamWriter wr = new System.IO.StreamWriter(Path.Combine(Path.GetTempPath(), "SqlTranlatorExc.txt"), true);
                wr.Write(ex.Message);
                wr.Close();
                isInitialized = false;
                return;
            }
            isInitialized = true;
        }

        public bool GetColumnDataType(string columnName, ref string dataType)
        {
            string colType = (
                        from entry in this
                        where entry.Name.ToUpper() == columnName.ToUpper()
                        select entry.Type).FirstOrDefault();

            if (colType != null)
            {
                dataType = colType;
                return true;
            }
            else
            {
                dataType = "";
                return false;
            }
        }

        public bool GetNumericPrecision(string columnName, ref object dataPrecision, ref object dataScale)
        {
            var col = (
                        from entry in this
                        where entry.Name.ToUpper() == columnName.ToUpper()
                        select new { entry.DataPrecision, entry.DataScale }).FirstOrDefault();

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

        public List<Column> GetAllColumns(string table)
        {
            var rez = (
                    from col in this
                    where col.Table.ToUpper() == table.ToUpper()
                    select col).ToList();
            return rez;
        }

        public IQueryable<Column> GetColProps(string[] columns)
        {
            var rez = (from entry in this
                       where columns.Contains(entry.Name.ToUpper())
                       select entry).AsQueryable<Column>();

            return rez;
        }
    }

    public class Column
    {
        private string name;
        private string table;
        private string type;
        private object dataScale;
        private object dataPrecision;
        private string tbCreator;
        private string collation;

        public string TbCreator
        {
            get { return tbCreator; }
            set { tbCreator = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Table
        {
            get { return table; }
            set { table = value; }
        }

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public object DataScale
        {
            get
            {
                return dataScale;
            }
            set
            {
                dataScale = value;
            }
        }

        public object DataPrecision
        {
            get
            {
                return dataPrecision;
            }
            set
            {
                dataPrecision = value;
            }
        }

        public string Collation
        {
            get { return collation; }
            set { collation = value; }
        }

        public Column(string name, string table, string schema, string type, string collation)
        {
            this.name = name;
            this.table = table;
            this.type = type;
            this.collation = collation;
            this.tbCreator = schema;
        }

        public Column(string name, string table, string type, object dataPrecision, object dataScale, string tbCreator)
        {
            this.name = name;
            this.table = table;
            this.type = type;
            this.dataPrecision = dataPrecision;
            this.dataScale = dataScale;
            this.tbCreator = tbCreator;
        }
    }
}
