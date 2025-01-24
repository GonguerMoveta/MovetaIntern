using System;
using fecher.Common;
using System.Linq;

namespace fecher.SqlTranslator
{
	/// <summary>
	/// Contains all the function names in a specific database and their matches in a different database
	/// </summary>
	public static class FunctionTranslator
	{
        #region Enums
        private enum FunctionType
        {
            Date,
            Scalar,
            Other
        }
        #endregion

        #region Constants
        private static string[] DATEFUNCTIONS_SQLBase_MSSQL = { "TODATE", "DATEVALUE", "GETDATE", "QUARTERBEG", "YEARBEG" };

        private static string[] SCALARFUNCTIONS_SQLBase_MSSQL = { "MIN", "MAX" };

        private static string [] functions_SQLBase =		{		"AVG",			"COUNT",		"MAX",			"MIN",			"SUM",
															"@ABS",			"@ACOS",		"@ASIN",		"@ATAN",		"@ATAN2",
															"@CHAR",		"@CHOOSE",		"@COALESCE",	"@CODE",		"@COS",
															"@CTERM",		"@DATE",		"@DATETOCHAR",	"@DATEVALUE",	"@DAY",
															"@DECIMAL",		"@DECODE",		"@DIFFERENCE",	"@EXACT",		"@EXP",
															"@FACTORIAL",	"@FIND",		"@FV",			"@HEX",			"@HOUR",
															"@IF",			"@INT",			"@ISNA",		"@LEFT",		"@LENGTH",
															"@LICS",		"@LN",			"@LOG",			"@LOWER",		"@MEDIAN",  
															"@MICROSECOND",	"@MID",			"@MINUTE",		"@MOD",			"@MONTH",
															"@MONTHBEG",	"@NOW",			"@NULLVALUE",	"@PI",			"@PMT",
															"@PROPER",		"@PV",			"@QUARTER",		"@QUARTERBEG",	"@RATE",
															"@REPEAT",		"@REPLACE",		"@RIGHT",		"@ROUND",		"@SCAN",
															"@SDV",			"@SECOND",		"@SIN",			"@SLN",			"@SOUNDEX",
															"@SQRT",		"@SUBSTRING",	"@STRING",		"@SYD",			"@TAN",
															"@TERM",		"@TIME",		"@TIMEVALUE",	"@TRIM",		"@UPPER",
															"@VALUE",		"@WEEKBEG",		"@WEEKDAY",		"@YEAR",		"@YEARBEG",
															"@YEARNO",      "@DATETOFLOAT"
                                                    };
		
		
		private static string [] functions_SQLBase_MSSQL = {		"AVG",				"COUNT",		"MAX",				"MIN",			"SUM",
															"ABS",				"ACOS",			"ASIN",				"ATAN",			"ATN2",
															"CHAR",				"",				"COALESCE",			"ASCII",		"COS",
															"dbo.CTERM",		"dbo.TODATE",	"dbo.DATETOCHAR",	"dbo.DATEVALUE","DAY",
															"dbo.TODECIMAL",	"",				"DIFFERENCE",		"dbo.EXACT",	"EXP",
															"dbo.FACTORIAL",	"CHARINDEX",	"dbo.FV",			"dbo.HEX",		"dbo.GETHOUR",
															"",					"dbo.TOINT",	"",					"LEFT",			"dbo.LENGTH",
															"dbo.LICS",			"LOG",			"LOG10",			"LOWER",		"MEDIAN",  
															"dbo.MICROSECOND",	"SUBSTRING",	"dbo.GETMINUTE",	"dbo.MOD",		"MONTH",
															"dbo.MONTHBEG",		"GETDATE",		"",			        "PI",			"dbo.PMT",
															"dbo.PROPER",		"dbo.PV",		"dbo.GETQUARTER",	"dbo.QUARTERBEG","dbo.RATE",
															"REPLICATE",		"STUFF",		"RIGHT",			"ROUND",		"CHARINDEX",
															"STDEV",			"dbo.GETSECOND",	"SIN",				"dbo.SLN",		"SOUNDEX",
															"SQRT",				"SUBSTRING",	"dbo.STRING",		"dbo.SYD",		"TAN",
															"dbo.TERM",			"dbo.GETTIME",	"dbo.TIMEVALUE",	"dbo.TRIM",		"UPPER",
															"dbo.VALUE",		"dbo.WEEKBEG",	"dbo.GETWEEKDAY",	"dbo.YEARNO",	"dbo.YEARBEG",
															"YEAR",             "dbo.DATETOFLOAT"
                                                    };

		private static string [] functions_SQLBase_Oracle = {		"AVG",			"COUNT",		"MAX",				"MIN",			"SUM",
															"ABS",			"ACOS",			"ASIN",				"ATAN",			"ATAN2",
															"CHR",			"",				"COALESCE",			"ASCII",		"COS",
															"CTERM",		"TODATE",		"DATETOCHAR",		"TO_DATE",		"GETDAY",
															"TODECIMAL",	"DECODE",		"DIFFERENCE",		"EXACT",		"EXP",
															"FACTORIAL",	"FIND",		    "FV",				"HEX",			"GETHOUR",
															"",				"TRUNC",		"",					"STRLEFT",		"LEN",
															"LICS",			"LN",			"LOG10",			"LOWER",		"MEDIAN",  
															"",				"SUBSTR",		"GETMINUTE",		"MOD",			"GETMONTH",
															"MONTHBEG",		"SYSDATE",		"NVL",				"PI",			"PMT",
															"INITCAP",		"PV",			"QUARTER",			"QUARTERBEG",	"RATE",
															"REPEAT",		"REPL",			"STRRIGHT",			"ROUND",		"SCAN",
															"SDV",			"GETSECOND",	"SIN",				"SLN",			"SOUNDEX",
															"SQRT",			"SUBSTR",		"TOSTRING",			"SYD",			"TAN",
															"TERM",			"GETTIME",		"TIMEVALUE",		"TRIM",			"UPPER",
															"TO_NUMBER",	"WEEKBEG",		"WEEKDAY",			"GETYEAR",		"YEARBEG",
															"YEARNO"
                                                    };
       
        private static string[] functions_Informix_SqlServer ={    "ABS",          "CEILING",       "FLOOR",           "N/A",          "N/A",
                                                            "dbo.MOD",      "POWER",         "dbo.ROOT",        "SQRT",         "ROUND",
                                                            "TRUNCATE",     "N/A",           "dbo.DBINFO",      "N/A",          "N/A",
                                                            "N/A",          "N/A",           "EXP",             "LOG",          "LOG10",
                                                            "LOG",          "HEX",           "LEN",             "OCTET_LENGTH", "CHAR_LENGTH",
                                                            "ROW_NUMBER",   "LAG",           "LEAD",            "RANK",         "DENSE_RANK",
                                                            "PERCENT_RANK", "CUME_DIST",     "NTILE",           "FIRST_VALUE",  "LAST_VALUE",
                                                          "RATIO_TO_REPORT","AVG",           "COUNT",           "MAX",          "MIN",
                                                            "N/A",          "STDEV",         "SUM",             "VAR",          "N/A",
                                                            "N/A",          "N/A",           "N/A",             "N/A",          "N/A",
                                                            "N/A",          "dbo.ADD_MONTHS","GETDATE",         "DAY",          "MONTH",
                                                            "dbo.QUARTER",  "WEEKDAY",       "YEAR",            "dbo.MONTHS_BETWEEN",       "N/A",
                                                            "DBO.NEXT_DAY", "N/A",           "dbo.MDY",         "N/A",          "N/A",
                                                            "COS",          "COT",           "SIN",             "N/A",          "TAN",
                                                            "N/A",          "ACOS",          "N/A",             "ASIN",         "N/A",
                                                            "ATAN",         "N/A",           "ATN2",            "DEGREES",      "RADIANS",
                                                            "CONCAT",       "ASCII",         "dbo.TRIM",        "LTRIM",        "RTRIM",
                                                            "SPACE",        "REVERSE",       "REPLACE",         "N/A",          "N/A",
                                                            "CHAR",         "UPPER",         "LOWER",           "dbo.INITCAP",  "CHARINDEX",
                                                            "CHARINDEX",    "LEFT",          "RIGHT",           "dbo.SUBSTR",   "N/A",
                                                            "SUBSTRING",    "N/A",           "N/A",             "ISNULL"
                                                    };


        private static string[] functions_Informix =
        {
            "ABS",  "CEIL", "FLOOR", "GREATEST", "LEAST", "MOD", "POW", "ROOT", "SQRT", "ROUND", "TRUNC", //Algebraic functions
            "CARDINALITY",  //Cardinality function
            "DBINFO", //DBINFO function
            "ENCRYPT_AES", "ENCRYPT_TDES", "DECRYPT_CHAR", "DECRYPT_BINARY", //Encryption and decryption functions
            "EXP", "LN", "LOG10", "LOGN", //Exponential and Logarithmic Functions
            "HEX",//HEX Function
            "LENGTH", "OCTET_LENGTH", "CHAR_LENGTH",// Length functions
            "ROW_NUMBER",// OLAP numbering function expression
            "LAG", "LEAD", "RANK", "DENSE_RANK", "PERCENT_RANK", "CUME_DIST", "NTILE",//OLAP ranking functions assign a rank to each row
            "FIRST_VALUE", "LAST_VALUE", "RATIO_TO_REPORT", "AVG", "COUNT", "MAX", "MIN" ,"RANGE" , "STDEV" , "SUM" , "VARIANCE",//OLAP aggregation functions aggregate row data
            "SECLABEL_BY_NAME", "SECLABEL_BY_COMP", "SECLABEL_TO_CHAR",//Security Label Support Functions
            "FILETOBLOB", "FILETOCLOB", "LOTOFILE", "LOCOPY",//Smart-Large-Object Functions
            "ADD_MONTHS", "DATE", "DAY", "MONTH", "QUARTER", "WEEKDAY", "YEAR", "MONTHS_BETWEEN", "LAST_DAY", "NEXT_DAY", "EXTEND", "MDY", "TO_CHAR",
            "TO_DATE",//Time Functions
            "COS", "COSH", "SIN", "SINH", "TAN", "TANH", "ACOS", "ACOSH", "ASIN", "ASINH", "ATAN", "ATANH", "ATAN2", "DEGREES", "RADIANS",//Trigonometric Functions
            "CONCAT", "ASCII", "TRIM", "LTRIM", "RTRIM", "SPACE", "REVERSE", "REPLACE", "LPAD", "RPAD", "CHR", "UPPER", "LOWER",
            "INITCAP", "CHARINDEX","INSTR", "LEFT", "RIGHT", "SUBSTR", "SUBSTRB", "SUBSTRING", "SUBSTRING_INDEX",//String-Manipulation Functions
            "IFX_ALLOW_NEWLINE",//IFX_ALLOW_NEWLINE Function
            "NVL","DECODE"
        };
        #endregion

        public static bool IsScalarFunction(DatabaseBrand targetDB, TreeNode node)
        {
            return IsScalarFunction(DatabaseBrand.SqlBase, targetDB, node);
        }

        public static bool IsScalarFunction(DatabaseBrand sourceDB, DatabaseBrand targetDB, TreeNode node)
        {
            return IsFunctionOfType(sourceDB, targetDB, node, FunctionType.Scalar);
        }

        public static bool IsDateFunction(DatabaseBrand targetDB, TreeNode node)
        {
            return IsDateFunction(DatabaseBrand.SqlBase, targetDB, node);
        }

        public static bool IsDateFunction(DatabaseBrand sourceDB, DatabaseBrand targetDB, TreeNode node)
        {
            return IsFunctionOfType(sourceDB, targetDB, node, FunctionType.Date);
        }

        private static bool IsFunctionOfType(DatabaseBrand targetDB, TreeNode node, FunctionType functionType)
        {
            return IsFunctionOfType(DatabaseBrand.SqlBase, targetDB, node, functionType);
        }

        private static bool IsFunctionOfType(DatabaseBrand sourceDB, DatabaseBrand targetDB, TreeNode node, FunctionType functionType)
        {
            if (node == null ||
                node.NodeType != Tokens.Function)
            {
                return false;
            }

            string[] functions = GetFunctionsOfType(sourceDB, targetDB, functionType);
            if (functions == null)
            {
                return false;
            }

            return functions.Contains(node.GetUnqualifiedValue());
        }

        private static string[] GetFunctionsOfType(DatabaseBrand targetDB, FunctionType functionType)
        {
            return GetFunctionsOfType(DatabaseBrand.SqlBase, targetDB, functionType);
        }

        private static string[] GetFunctionsOfType(DatabaseBrand sourceDB, DatabaseBrand targetDB, FunctionType functionType)
        {
            switch (sourceDB)
            {
                case DatabaseBrand.SqlBase:
                    switch (targetDB)
                    {
                        case DatabaseBrand.SqlServer:
                            switch (functionType)
                            {
                                case FunctionType.Date:
                                    return DATEFUNCTIONS_SQLBase_MSSQL;

                                case FunctionType.Scalar:
                                    return SCALARFUNCTIONS_SQLBase_MSSQL;

                                default:
                                    return null;
                            }

                        default:
                            return null;
                    }

                default:
                    return null;
            }
        }

        /// <summary>
        /// Verifies if a given identifier is a valid function in the source database
        /// </summary>
        /// <param name="sourceDB"></param>
        /// <param name="identifier"></param>
        /// <returns></returns>
        public static bool IsFunction(DatabaseBrand sourceDB, string identifier)
		{
			string [] functionTable;

			switch (sourceDB)
			{
				case DatabaseBrand.SqlBase: functionTable = functions_SQLBase; break;
                case DatabaseBrand.Informix: functionTable = functions_Informix; break;
                default: functionTable = functions_SQLBase; break;
			}

            string ident = identifier.ToUpper();
            for (int i = 0; i < functionTable.Length; i++)
            {
                if (ident == functionTable[i])
                {
                    return true;
                }
            }

            return false;
		}

        /// <summary>
        /// Translates the function from sourceDB to targetDB
        /// </summary>
        /// <param name="sourceDB"></param>
        /// <param name="targetDB"></param>
        /// <param name="function"></param>
        /// <returns></returns>
        public static string TranslateFunction(DatabaseBrand sourceDB, DatabaseBrand targetDB, string function)
        {
            string[] sourceTable;
            string[] targetTable;

            switch (sourceDB)
            {
                case DatabaseBrand.SqlBase:
                    {
                        sourceTable = functions_SQLBase;
                        if (targetDB == DatabaseBrand.SqlServer)
                        {
                            targetTable = functions_SQLBase_MSSQL;
                        }
                        else if (targetDB == DatabaseBrand.Oracle)
                        {
                            targetTable = functions_SQLBase_Oracle;
                        }
                        else
                        {
                            targetTable = functions_SQLBase;
                        }
                        break;
                    }
                case DatabaseBrand.Informix:
                    {
                        sourceTable = functions_Informix;
                        if (targetDB == DatabaseBrand.SqlServer)
                        {
                            targetTable = functions_Informix_SqlServer;
                        }
                        else
                        {
                            targetTable = functions_SQLBase;
                        }
                        break;
                    }
                default:
                    {
                        sourceTable = functions_SQLBase;
                        targetTable = functions_SQLBase_MSSQL;
                        break;
                    }
            }

            string functionToCheck = function.ToUpper();
            for (int i = 0; i < sourceTable.Length; i++)
            {
                if (functionToCheck == sourceTable[i])
                {
                    return targetTable[i];
                }
            }

            return function;

        }

        /// <summary>
        /// Translates the function from sourceDB to targetDB
        /// </summary>
        /// <param name="targetDB"></param>
        /// <param name="function"></param>
        /// <returns></returns>
        public static string TranslateFunction(DatabaseBrand targetDB, string function)
        {
            return TranslateFunction(DatabaseBrand.SqlBase, targetDB, function);
        }
		
	}
}
