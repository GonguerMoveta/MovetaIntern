using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Data;

namespace fecher.Common
{
    public static class Globals
    {
        private static Dictionary<string, DatabaseSettings> settings;

        public static Dictionary<string, DatabaseSettings> Settings
        {
            get { return settings; }
        }

        private static TableInformation tableInformation;

        public static TableInformation TableInformation
        {
            get { return tableInformation; }
        }

        static Globals()
        {
            settings = new Dictionary<string, DatabaseSettings>();
            DatabaseSettings.Default.DbStructure = new ColumnCollection();
            tableInformation = new TableInformation();
        }

        #region Tracing
        public static void TraceStatement(string db, string original, string translated)
        {
            if (Settings[db].Tracing.Enabled)
            {
                Trace.WriteLine("-----------------------------------------");
                Trace.WriteLine(original, "Original statement");
                Trace.WriteLine(translated, "Translated statement");
            }
        }

        public static void TraceParameters(string db, IDataParameterCollection parameters)
        {
            if (Settings[db].Tracing.Enabled)
            {
                foreach (IDataParameter param in parameters)
                {
                    Trace.Write(param.ParameterName, "Parameter Name");
                    Trace.WriteLine(param.Value, " Value");
                }
            }
        }
        #endregion

        /// <summary>
        /// Loads schemas and tables
        /// </summary>
        /// <param name="dbSettings"></param>
        /// <param name="command"></param>
        public static void LoadSchema(DatabaseSettings dbSettings, IDbCommand command)
        {
            if (dbSettings != null
                && dbSettings.ReadStructure)
            {
                dbSettings.Schemas.Clear();
                MsSqlSchema schema = null;
                //command.CommandText = "select * FROM INFORMATION_SCHEMA.TABLES ORDER BY TABLE_SCHEMA, TABLE_NAME";
                command.CommandText = @"SELECT 
    t.TABLE_SCHEMA,
    t.TABLE_NAME,
	t.TABLE_TYPE,
    CASE WHEN s.name IS NULL THEN '' ELSE s.name END AS [Synonym]
FROM 
    INFORMATION_SCHEMA.TABLES t
LEFT JOIN 
    sys.synonyms s ON QUOTENAME(t.TABLE_SCHEMA) + '.' + QUOTENAME(t.TABLE_NAME) = s.base_object_name

order by t.TABLE_SCHEMA,t.TABLE_NAME";
                IDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string table_schema = ((string)reader["TABLE_SCHEMA"]).ToUpper();
                    string table_name = ((string)reader["TABLE_NAME"]).ToUpper();
                    string table_type = ((string)reader["TABLE_TYPE"]).ToUpper();
                    string synonym = ((string)reader["Synonym"]).ToUpper();

                    schema = dbSettings.Schemas[table_schema];
                    if (schema == null)
                    {
                        schema = new MsSqlSchema(table_schema);
                        dbSettings.Schemas.Add(schema);
                    }

                    switch (table_type)
                    {
                        case "BASE TABLE":
                            schema.Tables.Add(new MsSqlTable(table_name, synonym));
                            break;

                        case "VIEW":
                            schema.Tables.Add(new MsSqlTable(table_name, synonym));
                            break;
                    }
                    
                }
                reader.Close();
            }
        }


        /// <summary>
        /// Load the additional settings for the current connection
        /// </summary>
        public static DatabaseSettings LoadSettings(string db)
        {
            lock (Settings)
            {
                if (Settings.ContainsKey(db))
                {
                    return Settings[db];
                }
                else
                {
                    XmlNode database = LoadSqlConfig(db);
                    if (database != null)
                    {
                        DatabaseSettings dbSettings = new DatabaseSettings();
                        XmlNode dateformat = database.SelectSingleNode("date_format");
                        if (dateformat != null && dateformat.Attributes["value"] != null)
                        {
                            dbSettings.DateFormat = dateformat.Attributes["value"].Value;
                        }

                        XmlNode numberformat = database.SelectSingleNode("number_format");
                        if (numberformat != null && dateformat.Attributes["value"] != null)
                        {
                            dbSettings.NumberFormat = numberformat.Attributes["value"].Value;
                        }

                        XmlNode sourceDatabase = database.SelectSingleNode("source_database");
                        if (sourceDatabase != null && sourceDatabase.Attributes["value"] != null)
                        {
                            dbSettings.SourceDatabase = sourceDatabase.Attributes["value"].Value.ToUpper() == "INFORMIX" ? DatabaseBrand.Informix : DatabaseBrand.SqlBase;
                        }

                        XmlNode sqlBaseVersion = database.SelectSingleNode("sqlbase_version");
                        if (sqlBaseVersion != null && sqlBaseVersion.Attributes["value"] != null)
                        {
                            Regex reg = new Regex(@"(?<major_version>\d{1,2})\.?\d?\.?\d?");
                            Match match = reg.Match(sqlBaseVersion.Attributes["value"].Value);
                            if (match.Success)
                            {
                                string version = match.Groups["major_version"].Value;
                                if (Convert.ToInt32(version) < 9)
                                {
                                    dbSettings.SqlBaseVersion = DatabaseBrand.SqlBaseOld;
                                }
                            }
                        }

                        XmlNode concatNullYieldsNullOption = database.SelectSingleNode("concat_null_yields_null");
                        if (concatNullYieldsNullOption != null && concatNullYieldsNullOption.Attributes["value"] != null)
                        {
                            dbSettings.ConcatNullYieldsNullOption = concatNullYieldsNullOption.Attributes["value"].Value;
                        }

                        XmlNode readStructure = database.SelectSingleNode("read_structure");
                        if (readStructure != null && readStructure.Attributes["value"] != null)
                        {
                            dbSettings.ReadStructure = readStructure.Attributes["value"].Value.ToUpper() == "TRUE";
                        }

                        XmlNode rowidType = database.SelectSingleNode("rowid_type");
                        if (rowidType != null && rowidType.Attributes["value"] != null)
                        {
                            dbSettings.Rowid = rowidType.Attributes["value"].Value.ToUpper() == "TIMESTAMP" ? RowidType.Timestamp : RowidType.UniqueIdentifier;
                        }

                        XmlNode unicode = database.SelectSingleNode("unicode");
                        if (unicode != null && unicode.Attributes["value"] != null)
                        {
                            dbSettings.Unicode = unicode.Attributes["value"].Value.ToUpper() == "TRUE";
                        }

                        XmlNode parseError = database.SelectSingleNode("on_parse_error");
                        if (parseError != null && parseError.Attributes["value"] != null)
                        {
                            try
                            {
                                dbSettings.OnParseError = (ParseErrorAction)Enum.Parse(typeof(ParseErrorAction), parseError.Attributes["value"].Value, true);
                            }
                            catch
                            {
                            }
                        }

                        XmlNode trace = database.SelectSingleNode("trace");
                        if (trace != null && trace.Attributes["value"] != null)
                        {
                            dbSettings.Tracing.Enabled = trace.Attributes["value"].Value.ToUpper() == "TRUE";
                            //define the listener
                            if (dbSettings.Tracing.Enabled)
                            {
                                Trace.Listeners.Add(new System.Diagnostics.TextWriterTraceListener("Trace.log"));
                            }
                        }

                        XmlNode tableOwner = database.SelectSingleNode("table_owner");
                        if (tableOwner != null && tableOwner.Attributes["value"] != null)
                        {
                            dbSettings.TableOwner = tableOwner.Attributes["value"].Value;
                        }

                        XmlNode casing = database.SelectSingleNode("casing");
                        if (casing != null && casing.Attributes["value"] != null)
                        {
                            try
                            {
                                dbSettings.Casing = (Casing)Enum.Parse(typeof(Casing), casing.Attributes["value"].Value, true);
                            }
                            catch
                            {
                            }
                        }

                        XmlNode addAlias = database.SelectSingleNode("add_alias");
                        if (addAlias != null && addAlias.Attributes["value"] != null)
                        {
                            dbSettings.AddAlias = addAlias.Attributes["value"].Value.ToUpper() == "TRUE";
                        }

                        XmlNode convertConcatOperandsToString = database.SelectSingleNode("convert_concat_operands_tostring");
                        if (convertConcatOperandsToString != null && convertConcatOperandsToString.Attributes["value"] != null)
                        {
                            dbSettings.ConvertConcatOperandsToString = convertConcatOperandsToString.Attributes["value"].Value.ToUpper() == "TRUE";
                        }

                        Settings.Add(db, dbSettings);
                        return dbSettings;
                    }
                    else
                    {
                        return DatabaseSettings.Default;
                    }
                }
            }
        }

        /// <summary>
        /// Load the sql.config file and search for 
        /// the database node that matches the current connection string
        /// </summary>
        /// <returns></returns>
        private static XmlNode LoadSqlConfig(string database)
        {
            Stream stream = null;
            string filePath = null;
            XmlDocument xdoc = new XmlDocument();
            XmlNode node = null;
            bool isCloud = false;

            try
            {
                // try to load from current directory first
                filePath = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "sql.config");
                if (!File.Exists(filePath))
                {
                    // try to load from local file
                    filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "sql.config");
                    if (!File.Exists(filePath))
                    {
                        //look also for sys.config for PPJ Cloud
                        filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "sys.config");
                        if (File.Exists(filePath))
                        {
                            isCloud = true;
                        }
                    }
                    if (!File.Exists(filePath))
                    {
                        // try to load from location in application's config
                        string filePathInConfig = ConfigurationManager.AppSettings["sqlconfig"];
                        if (filePathInConfig != null)
                            filePath = filePathInConfig;
                    }
                }

                stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            }
            catch (FileNotFoundException)
            {
                //try to load the sql.config resource stream from the embedded resources.
                Assembly asm = Assembly.GetEntryAssembly();

                if (asm != null)
                {
                    string[] names = asm.GetManifestResourceNames();
                    string sqlConfigName = ".sql.config";
                    if (names != null && names.Length > 0)
                    {
                        for (int i = 0; i < names.Length; i++)
                        {
                            if (names[i].EndsWith(sqlConfigName))
                                stream = asm.GetManifestResourceStream(names[i]);
                        }
                    }
                }
            }

            try
            {
                if (stream == null)
                    throw new FileNotFoundException("The file " + filePath + " was not found.");

                xdoc.Load(stream);
                string xpath = isCloud ? "databases/database" : "database";
                //make a case sensitive comparison by translating all upper case chars to lower case
                //node = xdoc.DocumentElement.SelectSingleNode(xpath + "[translate(attribute::name, 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz') = '" + database.ToLower() + "']");
                node = xdoc.DocumentElement.SelectSingleNode(xpath + "/connection_string[contains(translate(@value, 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz'), '" + database.ToLower() + "')]");
                if (node != null)
                {
                    node = node.ParentNode;
                }
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            return node;
        }

        //Extension method for supporting "In" operator
        public static bool In<T>(this T obj, params T[] values)
        {
            for (int i = 0; i < values.Length; i++)
            {
                if (Object.Equals(obj, values[i]))
                    return true;
            }
            return false;
        }
    }
    
    public class DatabaseSettings
    {
        /// <summary>
        /// Date format read from sql.config
        /// </summary>
        public string DateFormat;

        /// <summary>
        /// Number format read from sql.config
        /// </summary>
        public string NumberFormat;

        /// <summary>
        /// SqlBase version read from sql.config;
        /// </summary>
        public DatabaseBrand SqlBaseVersion = DatabaseBrand.SqlBase;

        /// <summary>
        /// Concat null yields null option from sql.config
        /// </summary>
        public string ConcatNullYieldsNullOption;

        /// <summary>
        /// Read structure from sql.config
        /// If true, the structure of the tables is read and stored when a new connection is made
        /// Used to translate outer joins without fully qualifying the column names
        /// </summary>
        public bool ReadStructure = false;

        /// <summary>
        /// Stores the columns and the coressponding tables
        /// Used together with ReadStructure
        /// </summary>
        public ColumnCollection DbStructure;

        public MsSqlSchemaCollection Schemas = new MsSqlSchemaCollection();

        /// <summary>
        /// Rowid type from sql.config
        /// The ROWID from SqlBase can be translated to either timestamp or uniqueidentifier in SqlServer
        /// </summary>
        public RowidType Rowid = RowidType.Timestamp;

        /// <summary>
        /// Unicode support read from sql.config
        /// If true the char and varchar data types will be translated to 
        /// nchar/nvarchar in Sqlserver and nchar/nvarchar2 in Oracle
        /// </summary>
        public bool Unicode = false;

        /// <summary>
        /// Specifies what action to take in case of a parser error
        /// The default is to ignore the error and continue with the translation
        /// </summary>
        public ParseErrorAction OnParseError = ParseErrorAction.Ignore;

        /// <summary>
        /// Switch for enabling/disabling the tracing of the sql statements
        /// </summary>
        public BooleanSwitch Tracing = new BooleanSwitch("Tracing", "enable/disable tracing");

        /// <summary>
        /// Table Owner from sql.config
        /// This will replace SYSADM in the statements
        /// The default is dbo
        /// </summary>
        public string TableOwner = String.Empty;

        /// <summary>
        /// Casing used for identifiers (tables, columns, views...)
        /// The default is Proper
        /// Use Upper or Lower for case sensitive databases
        /// </summary>
        public Casing Casing = Casing.Proper;

        /// <summary>
        /// Specifies wether to add an alias to every expression in the select list
        /// The alias will be the exact expression surrounded by ""
        /// This is needed because SqlServer returns "No Column name" for expressions without an alias
        /// The default value is false
        /// </summary>
        public bool AddAlias = false;

        /// <summary>
        /// Bind variable into which the row count will be stored
        /// When using Translator without ResultSet SqlGetRowCount is not available
        /// Implemented for Oracle a method that translates the SELECT statement
        /// and removes all columns and bindVars and replaces them with COUNT(*) INTO :<BindVar>
        /// </summary>
        public string BindVar = "";

        /// <summary>
        /// Specifies the string to use as a separator when there are more than one statements returned
        /// </summary>
        public string Separator = "#&#";

        /// <summary>
        /// If set to true the operands of the concatenate operator are converted to nvarchar
        /// This is needed if e.g. a number is concatenated with a string; in SqlBase the number is automatically converted to string
        /// In SqlServer the string will be converted to number resulting in a conversion error
        /// </summary>
        public bool ConvertConcatOperandsToString = false;

        /// <summary>
        /// Convert DATE bindvariables using the format 120 - yyyy-mm-dd hh:mi:ss(24h)
        /// This is only needed when the sqlTranslator is used from older Gupta applications (prior to TD 6)
        /// Due to a bug in the ODBC driver, the date values can't be inserted into the database
        /// </summary>
        public bool ConvertDateValues = false;

        /// <summary>
        /// Specify the collation to use when creating character columns
        /// E.g. 
        /// create table table1
        ///(
        ///   id int not null,
        ///   name varchar(50)
        ///)
        /// If the Collation is set to Latin1_General_CS_AS the statement will be modified like this:
        /// CREATE TABLE [TABLE1] 
        ///( 
        ///  [id] INT NOT NULL , 
        ///  [name] VARCHAR ( 50 ) ?collate Latin1_General_CS_AS , 
        ///  [ROWID] timestamp 
        ///)
        /// </summary>
        public string Collation = String.Empty;

        /// <summary>
        /// Replace empty statements with a stored procedure that returns no results
        /// </summary>
        public bool ReplaceEmptyStatements = false;

        public DatabaseBrand SourceDatabase = DatabaseBrand.SqlBase;
        
        private static DatabaseSettings defaultSettings;
        
        public static DatabaseSettings Default
        {
            get
            {
                if (defaultSettings == null)
                {
                    defaultSettings = new DatabaseSettings();
                    defaultSettings.DbStructure = new ColumnCollection();
                }
                return defaultSettings;
            }
        }
    }
}
