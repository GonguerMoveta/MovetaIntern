using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OracleClient;
using System.IO;
using System.Xml;

namespace fecher.Common
{
    public class TableInformation : List<TableEntry>
    {
        private bool isInitialized = false;
        public bool IsInitialized
        { 
            get { return isInitialized; } 
        }
        
        public void Init(string database, string user, string password)
        {
            this.Clear();      
            string connectionString = String.Format("Data Source={0};User Id={1};Password={2};", database, user, password);
            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    OracleCommand command = conn.CreateCommand();
                    command.CommandText = "SELECT SCHEMANAME, TABLENAME, COLUMNNAME, ISBINARY, ISLONG, ISEXTERNAL, DATETYPE, DATATYPE FROM TABLEINFORMATION";
                    OracleDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        TableEntry entry = new TableEntry();
                        entry.Schema = (string)reader["SCHEMANAME"];
                        entry.Table = (string)reader["TABLENAME"];
                        entry.Column = (string)reader["COLUMNNAME"];
                        entry.DataType = (string)reader["DATATYPE"];
                        entry.IsBinary = (decimal)reader["ISBINARY"] == 1;
                        entry.IsLong = (decimal)reader["ISLONG"] == 1;
                        entry.IsExternal = (decimal)reader["ISEXTERNAL"] == 1;
                        if (entry.DataType != "TIMESTMP")
                        {
                            entry.IsDate = (decimal)reader["DATETYPE"] == 1;
                            entry.IsTime = (decimal)reader["DATETYPE"] == 2;
                        }
                        else
                        {
                            entry.Milliseconds = (int)(decimal)reader["DATETYPE"];
                        }

                        this.Add(entry);
                    }
                }
                catch
                {
                    isInitialized = false;
                    return;
                }
            }

            isInitialized = true;
        }

        public void InitFromXML()
        {
            this.Clear();
            string filePath = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "tableinformation.xml");
            if (File.Exists(filePath))
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(fs);

                    XmlNodeList nodelist = xmlDoc.SelectNodes("TableInformation/TableEntry");
                    foreach (XmlNode node in nodelist)
                    {
                        TableEntry entry = new TableEntry();
                        entry.Schema = node.SelectSingleNode("Schema").InnerText;
                        entry.Table = node.SelectSingleNode("Table").InnerText;
                        entry.Column = node.SelectSingleNode("Column").InnerText;
                        entry.IsBinary = node.SelectSingleNode("IsBinary").InnerText == "true";
                        entry.IsLong = node.SelectSingleNode("IsLong").InnerText == "true";
                        entry.IsExternal = node.SelectSingleNode("IsExternal").InnerText == "true";
                        entry.IsDate = node.SelectSingleNode("IsDate").InnerText == "true";

                        this.Add(entry);
                    }

                    isInitialized = true;
                }
            }
        }

        public bool IsExternalColumn(string column, string table)
        {
            bool isExternal = (
                    from entry in this
                    where entry.Column.ToUpper() == column.ToUpper() && entry.Table.ToUpper() == table.ToUpper()
                    select entry.IsExternal).FirstOrDefault();

            return isExternal;
        }

        public bool IsExternalColumn(string column, string table, out bool isBinary)
        {
            var extColumn = (
                    from entry in this
                    where entry.Column.ToUpper() == column.ToUpper() && entry.Table.ToUpper() == table.ToUpper()
                    select new { entry.IsExternal, entry.IsBinary }).FirstOrDefault();

            if (extColumn != null)
            {
                isBinary = extColumn.IsBinary;
                return extColumn.IsExternal;
            }
            else
            {
                isBinary = false;
                return false;
            }
        }

        public bool IsExternalColumn(string column)
        {
            bool isExternal = (
                    from entry in this
                    where entry.Column.ToUpper() == column.ToUpper() 
                    select entry.IsExternal).FirstOrDefault();

            return isExternal;
        }

        public bool IsBinaryColumn(string column, string table)
        {
            bool isBinary = (
                    from entry in this
                    where entry.Column.ToUpper() == column.ToUpper() && entry.Table.ToUpper() == table.ToUpper()
                    select entry.IsBinary).FirstOrDefault();

            return isBinary;
        }

        public bool GetColumnDateTimeProperties(string column, string table, out bool isDate, out bool isTime, out bool isTimestamp)
        {
            var col = (from entry in this
                       where entry.Column.ToUpper() == column.ToUpper() && entry.Table.ToUpper() == table.ToUpper()
                       select new { entry.IsDate, entry.IsTime, entry.DataType }).FirstOrDefault();
            if (col != null)
            {
                isDate = col.IsDate;
                isTime = col.IsTime;
                if (col.DataType == "TIMESTMP")
                {
                    if (!col.IsDate && !col.IsTime)
                    {
                        isTimestamp = true;
                    }
                    else
                    {
                        isTimestamp = false;
                    }
                }
                else
                {
                    isTimestamp = false;
                }
                return true;
            }
            else
            {
                isDate = false;
                isTime = false;
                isTimestamp = false;
                return false;
            }
        }

        public bool GetColumnProperties(string column, string table, out bool isBinary, out bool isLong, out bool isExternal,
            out bool isDate, out bool isTime, out bool isTimestamp)
        {
            
            var col = (from entry in this
                       where entry.Column.ToUpper() == column.ToUpper() && entry.Table.ToUpper() == table.ToUpper()
                       select new { entry.IsBinary, entry.IsLong, entry.IsExternal, entry.IsDate, entry.IsTime, entry.DataType }).FirstOrDefault();
            if (col != null)
            {
                isExternal = col.IsExternal;
                isLong = col.IsLong;
                isBinary = col.IsBinary;
                isDate = col.IsDate;
                isTime = col.IsTime;
                if (col.DataType == "TIMESTMP")
                {
                    if (!col.IsDate && !col.IsTime)
                    {
                        isTimestamp = true;
                    }
                    else
                    {
                        isTimestamp = false;
                    }
                }
                else
                {
                    isTimestamp = false;
                }
                return true;
            }
            else
            {
                isExternal = false;
                isLong = false;
                isBinary = false;
                isDate = false;
                isTime = false;
                isTimestamp = false;
                return false;
            }
        }

        public IQueryable<TableEntry> GetColProps(string[] columns)
        {
            var rez = (from entry in this
                      where columns.Contains(entry.Column.ToUpper())
                      select entry).AsQueryable<TableEntry>();

            return rez;            
        }

        public bool GetColumnProperties(string column, string table, out bool isBinary, out bool isLong, out bool isExternal)
        {
            var col = (from entry in this
                       where entry.Column.ToUpper() == column.ToUpper() && entry.Table.ToUpper() == table.ToUpper()
                       select new { entry.IsBinary, entry.IsLong, entry.IsExternal }).FirstOrDefault();
            if (col != null)
            {
                isExternal = col.IsExternal;
                isLong = col.IsLong;
                isBinary = col.IsBinary;
                return true;
            }
            else
            {
                isExternal = false;
                isLong = false;
                isBinary = false;
                return false;
            }
        }

        public bool IsColumnLong(string column, string table, out bool isLong)
        {
            var col = (from entry in this
                       where entry.Column.ToUpper() == column.ToUpper() && entry.Table.ToUpper() == table.ToUpper()
                       select new { entry.IsBinary, entry.IsLong, entry.IsExternal, entry.IsDate, entry.IsTime }).FirstOrDefault();
            if (col != null)
            {
                isLong = !(col.IsBinary && col.IsLong && col.IsDate && col.IsTime);
                return true;
            }
            else
            {
                isLong = false;
                return false;
            }
        }

        public bool IsDateColumn(string column, string table)
        {
            bool isDate = (
                    from entry in this
                    where entry.Column.ToUpper() == column.ToUpper() && entry.Table.ToUpper() == table.ToUpper()
                    select entry.IsDate).FirstOrDefault();

            return isDate;
        }

        public bool IsDateColumn(string column)
        {
            bool isDate = (
                    from entry in this
                    where entry.Column.ToUpper() == column.ToUpper() 
                    select entry.IsDate).FirstOrDefault();

            return isDate;
        }

        public bool IsTimeColumn(string column)
        {
            bool isTime = (
                    from entry in this
                    where entry.Column.ToUpper() == column.ToUpper()
                    select entry.IsTime).FirstOrDefault();

            return isTime;
        }

        public bool IsTimeStampColumn(string column)
        {
            var isTimestamp = (from entry in this
                               where entry.Column.ToUpper() == column.ToUpper() && entry.DataType == "TIMESTMP"
                               select new { entry.IsDate, entry.IsTime }).FirstOrDefault();

            if (isTimestamp != null)
            {
                if (!isTimestamp.IsDate && !isTimestamp.IsTime)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
                                 
        }

        public bool IsLongColumn(string column, string table)
        {
            bool isLong = (
                    from entry in this
                    where entry.Column.ToUpper() == column.ToUpper() && entry.Table.ToUpper() == table.ToUpper() && !entry.IsExternal
                    select entry.IsLong).FirstOrDefault();

            return isLong;
        }

        public bool HasExternalColumns(string table)
        {
            bool hasExternals = (
                    from entry in this
                    where entry.Table.ToUpper() == table.ToUpper() && entry.IsExternal
                    select true).FirstOrDefault();

            return hasExternals;
        }

        public bool HasExternalLongOrBinaryColumns(string table)
        {
            bool hasExternals = (
                    from entry in this
                    where entry.Table.ToUpper() == table.ToUpper() && (entry.IsExternal || entry.IsBinary || entry.IsLong)
                    select true).FirstOrDefault();

            return hasExternals;
        }
    }

    public class TableEntry
    {
        public string Schema { get; set; }
        public string Table { get; set; }
        public string Column { get; set; }
        public string DataType { get; set; }
        public int Milliseconds { get; set; }
        public bool IsBinary { get; set; }
        public bool IsLong { get; set; }
        public bool IsExternal { get; set; }
        public bool IsDate { get; set; }
        public bool IsTime { get; set; }
    }

    public class ExternalColumn
    {
        public string Column { get; set; }
        public int Index { get; set; }
        public bool IsBinary { get; set; }
        public string Value { get; set; }
        public string GUID { get; set; }
    }
}
