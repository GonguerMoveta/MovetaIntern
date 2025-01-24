using System;
using fecher.Common;
using System.Collections;
using System.Text.RegularExpressions;

namespace fecher.SqlTranslator
{
	/// <summary>
	/// This class is used to delimitate the tokens in the string representing the SQL statement.
	/// It is used by the SyntacticalAnalyzer class.
	/// </summary>
	public class LexicalAnalyzerInformix : LexicalAnalyzer
	{
		private bool IsTokenKeyword(int keywordIndex)
		{
            return keywordIndex >= 0 && keywordIndex < keywords.Length; 
		}

		private bool IsTokenDataTypeKeyword(string token)
		{
			return token.ToUpper().In("BIGINT", "BIGSERIAL", "BSON", "BYTE", "CHAR", "CHARACTER", "DATE", "DATETIME", "DEC", "DECIMAL",
                "FLOAT", "INT", "INT8", "INTEGER", "INTERVAL", "MONEY", "NCHAR", "NUMERIC", "NVARCHAR", "REAL", "SERIAL",
                "SERIAL8", "SMALLFLOAT", "SMALLINT", "TEXT", "VARCHAR");
		}

		/// <summary>
		/// keywords allowed right after SELECT token: ALL|DINSTINCT|TOP
		/// </summary>
		/// <param name="keywordIndex"></param>
		/// <returns></returns>
		private bool IsTokenKeywordAllowedAfterSELECT(int keywordIndex)
		{
			return keywordIndex == 1 || keywordIndex == 40 || keywordIndex == 85 || keywordIndex == 203;
		}

		private static string [] keywords = {

			"AAO", "ABS", "ABSOLUTE", "ACCELERATE", "ACCESS", "ACCESS_METHOD", "ACCOUNT", "ACOS", "ACOSH", "ACTIVE", "ADD", "ADDRESS", "ADD_MONTHS", "ADMIN", "AFTER",
			"AGGREGATE", "ALIGNMENT", "ALL", "ALL_ROWS", "ALLOCATE", "ALTER", "AND", "ANSI", "ANY", "APPEND", "AQT", "ARRAY", "AS", "ASC", "ASCII", "ASIN", "ASINH",
			"ASYNC", "AT", "ATAN", "ATAN2", "ATANH", "ATTACH", "ATTRIBUTES", "AUDIT", "AUTHENTICATION", "AUTHID", "AUTHORIZATION", "AUTHORIZED", "AUTO", "AUTOFREE",
			"AUTOLOCATE", "AUTO_READAHEAD", "AUTO_REPREPARE", "AUTO_STAT_MODE", "AVG", "AVOID_EXECUTE", "AVOID_FACT", "AVOID_FULL", "AVOID_HASH", "AVOID_INDEX", "AVOID_INDEX_SJ",
			"AVOID_MULTI_INDEX", "AVOID_NL", "AVOID_STAR_JOIN", "BARGROUP", "BASED", "BEFORE", "BEGIN", "BETWEEN", "BIGINT", "BIGSERIAL", "BINARY", "BITAND", "BITANDNOT",
			"BITNOT", "BITOR", "BITXOR", "BLOB", "BLOBDIR", "BOOLEAN", "BOTH", "BOUND_IMPL_PDQ", "BSON", "BUCKETS", "BUFFERED", "BUILTIN", "BY", "BYTE", "CACHE", "CALL",
			"CANNOTHASH", "CARDINALITY", "CASCADE", "CASE", "CAST", "CEIL", "CHAR", "CHAR_LENGTH", "CHARACTER", "CHARACTER_LENGTH", "CHARINDEX", "CHECK", "CHR", "CLASS",
			"CLASS_ORIGIN", "CLEANUP", "CLIENT", "CLOB", "CLOBDIR", "CLOSE", "CLUSTER", "CLUSTER_TXN_SCOPE", "COBOL", "CODESET", "COLLATION", "COLLECTION", "COLUMN",
			"COLUMNS", "COMMIT", "COMMITTED", "COMMUTATOR", "COMPONENT", "COMPONENTS", "COMPRESSED", "CONCAT", "CONCURRENT", "CONNECT", "CONNECTION", "CONNECTION_NAME",
			"CONNECT_BY_ISCYCLE", "CONNECT_BY_ISLEAF", "CONNECT_BY_ROOT", "CONST", "CONSTRAINT", "CONSTRAINTS", "CONSTRUCTOR", "CONTEXT", "CONTINUE", "COPY", "COS",
			"COSH", "COSTFUNC", "COUNT", "CRCOLS", "CREATE", "CROSS", "CUME_DIST", "CURRENT", "CURRENT_ROLE", "CURRENT_USER", "CURRVAL", "CURSOR", "CYCLE", "DATA",
			"DATABASE", "DATAFILES", "DATASKIP", "DATE", "DATETIME", "DAY", "DBA", "DBDATE", "DBINFO", "DBPASSWORD", "DBSA", "DBSERVERNAME", "DBSECADM", "DBSSO", "DEALLOCATE",
			"DEBUG", "DEBUGMODE", "DEBUG_ENV", "DEC", "DECIMAL", "DECLARE", "DECRYPT_BINARY", "DECRYPT_CHAR", "DEC_T", "DEFAULT", "DEFAULTESCCHAR", "DEFAULT_ROLE",
			"DEFAULT_USER", "DEFERRED", "DEFERRED_PREPARE", "DEFINE", "DEGREES", "DELAY", "DELETE", "DELETING", "DELIMITED", "DELIMITER", "DELUXE", "DENSERANK", "DENSE_RANK",
			"DESC", "DESCRIBE", "DESCRIPTOR", "DETACH", "DIAGNOSTICS", "DIRECTIVES", "DIRTY", "DISABLE", "DISABLED", "DISCARD", "DISCONNECT", "DISK", "DISTINCT", "DISTRIBUTEBINARY",
			"DISTRIBUTESREFERENCES", "DISTRIBUTIONS", "DOCUMENT", "DOMAIN", "DONOTDISTRIBUTE", "DORMANT", "DOUBLE", "DROP", "DTIME_T", "EACH", "ELIF", "ELSE", "ENABLE",
			"ENABLED", "ENCRYPT_AES", "ENCRYPT_TDES", "ENCRYPTION", "END", "ENUM", "ENVIRONMENT", "ERKEY", "ERROR", "ESCAPE", "EXCEPT", "EXCEPTION", "EXCLUSIVE", "EXEC",
			"EXECUTE", "EXECUTEANYWHERE", "EXEMPTION", "EXISTS", "EXIT", "EXP", "EXPLAIN", "EXPLICIT", "EXPRESS", "EXPRESSION", "EXTDIRECTIVES", "EXTEND", "EXTENT",
			"EXTERNAL", "EXTYPEID", "EXTYPELENGTH", "EXTYPENAME", "EXTYPEOWNERLENGTH", "EXTYPEOWNERNAME", "FACT", "FALLBACK", "FALSE", "FAR", "FETCH", "FILE", "FILETOBLOB",
			"FILETOCLOB", "FILLFACTOR", "FILTERING", "FINAL", "FIRST", "FIRST_ROWS", "FIRST_VALUE", "FIXCHAR", "FIXED", "FLOAT", "FLOOR", "FLUSH", "FOLLOWING", "FOR",
			"FORCE", "FORCED", "FORCE_DDL_EXEC", "FOREACH", "FOREIGN", "FORMAT", "FORMAT_UNITS", "FORTRAN", "FOUND", "FRACTION", "FRAGMENT", "FRAGMENTS", "FREE", "FROM",
			"FULL", "FUNCTION", "G", "GB", "GENBSON", "GENERAL", "GET", "GETHINT", "GIB", "GLOBAL", "GO", "GOTO", "GRANT", "GREATERTHAN", "GREATERTHANOREQUAL", "GRID",
			"GRID_NODE_SKIP", "GROUP", "HANDLESNULLS", "HASH", "HAVING", "HDR", "HDR_TXN_SCOPE", "HEX", "HIGH", "HINT", "HOLD", "HOME", "HOUR", "IDATA", "IDSLBACREADARRAY",
			"IDSLBACREADSET", "IDSLBACREADTREE", "IDSLBACRULES", "IDSLBACWRITEARRAY", "IDSLBACWRITESET", "IDSLBACWRITETREE", "IDSSECURITYLABEL", "IF", "IFX_*", "ILENGTH",
			"IMMEDIATE", "IMPLICIT", "IMPLICIT_PDQ", "IN", "INACTIVE", "INCREMENT", "INDEX", "INDEXES", "INDEX_ALL", "INDEX_SJ", "INDICATOR", "INFORMIX", "INFORMIXCONRETRY",
			"INFORMIXCONTIME", "INIT", "INITCAP", "INLINE", "INNER", "INOUT", "INSENSITIVE", "INSERT", "INSERTING", "INSTEAD", "INSTR", "INT", "INT8", "INTEG", "INTEGER",
			"INTERNAL", "INTERNALLENGTH", "INTERSECT", "INTERVAL", "INTO", "INTRVL_T", "IS", "ISCANONICAL", "ISOLATION", "ITEM", "ITERATOR", "ITYPE", "JAVA", "JOIN",
			"JSON", "KB", "KEEP", "KEY", "KIB", "LABEL", "LABELEQ", "LABELGE", "LABELGLB", "LABELGT", "LABELLE", "LABELLT", "LABELLUB", "LABELTOSTRING", "LAG", "LANGUAGE",
			"LAST", "LAST_DAY", "LAST_VALUE", "LATERAL", "LEAD", "LEADING", "LEFT", "LEN", "LENGTH", "LESSTHAN", "LESSTHANOREQUAL", "LET", "LEVEL", "LIKE", "LIMIT",
			"LIST", "LISTING", "LOAD", "LOCAL", "LOCATOR", "LOCK", "LOCKS", "LOCOPY", "LOC_T", "LOG", "LOG10", "LOGN", "LONG", "LOOP", "LOTOFILE", "LOW", "LOWER",
			"LPAD", "LTRIM", "LVARCHAR", "M", "MATCHED", "MATCHES", "MAX", "MAXERRORS", "MAXLEN", "MAXVALUE", "MB", "MDY", "MEDIAN", "MEDIUM", "MEMORY", "MEMORY_RESIDENT",
			"MERGE", "MESSAGE_LENGTH", "MESSAGE_TEXT", "MIB", "MIN", "MINUS", "MINUTE", "MINVALUE", "MOD", "MODE", "MODERATE", "MODIFY", "MODULE", "MONEY", "MONTH",
			"MONTHS_BETWEEN", "MORE", "MULTISET", "MULTI_INDEX", "NAME", "NCHAR", "NEAR_SYNC", "NEGATOR", "NEW", "NEXT", "NEXT_DAY", "NEXTVAL", "NLSCASE", "NO", "NOCACHE",
			"NOCYCLE", "NOMAXVALUE", "NOMIGRATE", "NOMINVALUE", "NONE", "NON_RESIDENT", "NON_DIM", "NOORDER", "NORMAL", "NOT", "NOTEMPLATEARG", "NOTEQUAL|", "NOVALIDATE",
			"NTILE", "NULL", "NULLABLE", "NULLIF", "NULLS", "NUMBER", "NUMERIC", "NUMROWS", "NUMTODSINTERVAL", "NUMTOYMINTERVAL", "NVARCHAR", "NVL", "OCTET_LENGTH",
			"OF", "OFF", "OLD", "ON", "ONLINE", "ONLY", "OPAQUE", "OPCLASS", "OPEN", "OPTCOMPIND", "OPTIMIZATION", "OPTION", "OR", "ORDER", "ORDERED", "OUT", "OUTER",
			"OUTPUT", "OVER", "OVERRIDE", "PAGE", "PARALLELIZABLE", "PARAMETER", "PARTITION", "PASCAL", "PASSEDBYVALUE", "PASSWORD", "PDQPRIORITY", "PERCALL_COST",
			"PERCENT_RANK", "PIPE", "PLI", "PLOAD", "POLICY", "POW", "POWER", "PRECEDING", "PRECISION", "PREPARE", "PREVIOUS", "PRIMARY", "PRIOR", "PRIVATE", "PRIVILEGES",
			"PROBE", "PROCEDURE", "PROPERTIES", "PUBLIC", "PUT", "QUARTER", "RADIANS", "RAISE", "RANGE", "RANK", "RATIOTOREPORT", "RATIO_TO_REPORT", "RAW", "READ",
			"REAL", "RECORDEND", "REFERENCES", "REFERENCING", "REGISTER", "REJECTFILE", "RELATIVE", "RELEASE", "REMAINDER", "RENAME", "REOPTIMIZATION", "REPEATABLE",
			"REPLACE", "REPLICATION", "RESOLUTION", "RESOURCE", "RESTART", "RESTRICT", "RESUME", "RETAIN", "RETAINUPDATELOCKS", "RETURN", "RETURNED_SQLSTATE", "RETURNING",
			"RETURNS", "REUSE", "REVERSE", "REVOKE", "RIGHT", "ROBIN", "ROLE", "ROLLBACK", "ROLLFORWARD", "ROLLING", "ROOT", "ROUND", "ROUTINE", "ROW", "ROWID", "ROWIDS",
			"ROWNUMBER", "ROWS", "ROW_COUNT", "ROW_NUMBER", "RPAD", "RTRIM", "RULE", "SAMEAS", "SAMPLES", "SAMPLING", "SAVE", "SAVEPOINT", "SCHEMA", "SCALE", "SCROLL",
			"SECLABEL_BY_COMP", "SECLABEL_BY_NAME", "SECLABEL_TO_CHAR", "SECOND", "SECONDARY", "SECURED", "SECURITY", "SECTION", "SELCONST", "SELECT", "SELECTING",
			"SELECT_GRID", "SELECT_GRID_ALL", "SELFUNC", "SELFUNCARGS", "SENSITIVE", "SEQUENCE", "SERIAL", "SERIAL8", "SERIALIZABLE", "SERVER", "SERVER_NAME", "SERVERUUID",
			"SESSION", "SET", "SETSESSIONAUTH", "SHARE", "SHORT", "SIBLINGS", "SIGNED", "SIN", "SITENAME", "SIZE", "SKIP", "SMALLFLOAT", "SMALLINT", "SOME", "SOURCEID",
			"SOURCETYPE", "SPACE", "SPECIFIC", "SQL", "SQLCODE", "SQLCONTEXT", "SQLERROR", "SQLSTATE", "SQLWARNING", "SQRT", "STABILITY", "STACK", "STANDARD", "START",
			"STAR_JOIN", "STATCHANGE", "STATEMENT", "STATIC", "STATISTICS", "STATLEVEL", "STATUS", "STDEV", "STEP", "STOP", "STORAGE", "STORE", "STRATEGIES", "STRING",
			"STRINGTOLABEL", "STRUCT", "STYLE", "SUBCLASS_ORIGIN", "SUBSTR", "SUBSTRING", "SUBSTRING_INDEX", "SUM", "SUPPORT", "SYNC", "SYNONYM", "SYS*", "T", "TABLE",
			"TABLES", "TAN", "TASK", "TB", "TEMP", "TEMPLATE", "TEST", "TEXT", "THEN", "TIB", "TIME", "TO", "TODAY", "TO_CHAR", "TO_DATE", "TO_DSINTERVAL", "TO_NUMBER",
			"TO_YMINTERVAL", "TRACE", "TRAILING", "TRANSACTION", "TRANSITION", "TREE", "TRIGGER", "TRIGGERS", "TRIM", "TRUE", "TRUNC", "TRUNCATE", "TRUSTED", "TYPE",
			"TYPEDEF", "TYPEID", "TYPENAME", "TYPEOF", "UID", "UNBOUNDED", "UNCOMMITTED", "UNDER", "UNION", "UNIQUE", "UNIQUECHECK", "UNITS", "UNKNOWN", "UNLOAD",
			"UNLOCK", "UNSIGNED", "UPDATE", "UPDATING", "UPON", "UPPER", "USAGE", "USE", "USELASTCOMMITTED", "USER", "USE_DWA", "USE_HASH", "USE_NL", "USING", "USTLOW_SAMPLE",
			"VALUE", "VALUES", "VAR", "VARCHAR", "VARIABLE", "VARIANCE", "VARIANT", "VARYING", "VERCOLS", "VIEW", "VIOLATIONS", "VOID", "VOLATILE", "WAIT", "WARNING",
			"WEEKDAY", "WHEN", "WHENEVER", "WHERE", "WHILE", "WITH", "WITHOUT", "WORK", "WRITE", "WRITEDOWN", "WRITEUP", "XADATASOURCE", "XID", "XLOAD", "XUNLOAD", "YEAR"
									};


        /// <summary>
        /// Returns the next token
        /// </summary>
        /// <returns></returns>
        public override IToken GetToken()
		{
			IToken newToken = new Token();
			bool foundToken = false;
			int state = 0;
			string tempString = "";

			do 
			{
				switch(state)
				{
					case 0: 
					{
                        //ignore spaces, tabs and new lines + backslashes
                        while((c == ' ')||(c == 13)||(c == 10) || (c == 9) || (c == '\\')) 
							c = GetChar();

                            if (((c >= 'a') && (c <= 'z')) || ((c >= 'A') && (c <= 'Z')) || IsGermanChar(c) ||
                                (c == '@') || (c == '#') || (c == '$') || (c == '_'))
                            {
                                tempString += c;
                                c = GetChar();
                                state = 1;
                                break;
                            }

                            else if (c == ':')
                            {
                                tempString += c;
                                c = GetChar();
                                state = 2;
                                break;
                            }

                            else if (c == ',')
                            {
                                newToken.SetToken(Tokens.Comma, ",");
                                foundToken = true;
                                c = GetChar();
                                break;
                            }

                            //else if (c == '\\')
                            //{
                            //    newToken.SetToken(Tokens.Backslash, "\\");
                            //    foundToken = true;
                            //    c = GetChar();
                            //    break;
                            //}

                            else if (c == '+')
                            {
                                newToken.SetToken(Tokens.MathOperator, "+");
                                foundToken = true;
                                c = GetChar();
                                break;
                            }

                            else if (c == '-')
                            {
                                newToken.SetToken(Tokens.MathOperator, "-");
                                foundToken = true;
                                c = GetChar();
                                break;
                            }

                            else if (c == '*')
                            {
                                newToken.SetToken(Tokens.MathOperator, "*");
                                foundToken = true;
                                c = GetChar();
                                break;
                            }

                            else if (c == '/')
                            {
                                newToken.SetToken(Tokens.MathOperator, "/");
                                foundToken = true;
                                c = GetChar();
                                break;
                            }

                            else if (c == '(')
                            {
                                newToken.SetToken(Tokens.LeftParant, "(");
                                foundToken = true;
                                c = GetChar();
                                break;
                            }

                            else if (c == ')')
                            {
                                newToken.SetToken(Tokens.RightParant, ")");
                                foundToken = true;
                                c = GetChar();
                                break;
                            }

                            else if (c == '<')
                            {
                                c = GetChar();
                                if (c == '=')
                                {
                                    newToken.SetToken(Tokens.RelatOperator, "<=");
                                    c = GetChar();
                                }
                                else if (c == '>')
                                {
                                    newToken.SetToken(Tokens.RelatOperator, "<>");
                                    c = GetChar();
                                }
                                else
                                    newToken.SetToken(Tokens.RelatOperator, "<");
                                foundToken = true;
                                break;
                            }

                            else if (c == '>')
                            {
                                c = GetChar();
                                if (c == '=')
                                {
                                    newToken.SetToken(Tokens.RelatOperator, ">=");
                                    c = GetChar();
                                }
                                else
                                    newToken.SetToken(Tokens.RelatOperator, ">");
                                foundToken = true;
                                break;
                            }

                            else if (c == '=')
                            {
                                newToken.SetToken(Tokens.RelatOperator, "=");
                                foundToken = true;
                                c = GetChar();
                                break;
                            }

                            else if (c == '!')
                            {
                                c = GetChar();
                                if (c == '=')
                                {
                                    newToken.SetToken(Tokens.RelatOperator, "!=");
                                    foundToken = true;
                                    c = GetChar();
                                }
                                break;
                            }

                            else if (c == '&')
                            {
                                newToken.SetToken(Tokens.BoolOperator, "&");
                                foundToken = true;
                                c = GetChar();
                                break;
                            }

                            else if (c == '|')
                            {
                                c = GetChar();
                                if (c == '|')
                                {
                                    newToken.SetToken(Tokens.Concatenate, "||");
                                    c = GetChar();
                                }
                                else
                                    newToken.SetToken(Tokens.BoolOperator, "|");
                                foundToken = true;
                                break;
                            }

                            else if (c == '.')
                            {
                                newToken.SetToken(Tokens.Dot, ".");
                                foundToken = true;
                                c = GetChar();
                                break;
                            }

                            else if ((c >= '0' && c <= '9'))
                            {
                                tempString += c;
                                c = GetChar();
                                state = 3;
                                break;
                            }

                            else if (c == '\'')
                            {
                                tempString += c;
                                c = GetChar();
                                state = 4;
                                break;
                            }
                            else if (c == '\"')
                            {
                                tempString += c;
                                c = GetChar();
                                state = 4;
                                break;
                            }
                            else if (c == '\0') //end of string
                            {
                                newToken.SetToken(Tokens.Null);
                                foundToken = true;
                                break;
                            }
                            else if (c == '[')
                            {
                                newToken.SetToken(Tokens.LeftBracket, "[");
                                foundToken = true;
                                c = GetChar();
                                break;
                            }
                            else if (c == ']')
                            {
                                newToken.SetToken(Tokens.RightBracket, "]");
                                foundToken = true;
                                c = GetChar();
                                break;
                            }

                            else //error: unknown character
                            {
                                throw new NotSupportedException("Character " + c + " is not supported by SqlTranslator");
                            }
					}
					
					case 1: //keyword, function or identifier
                        {
                            while (((c >= 'a') && (c <= 'z')) || ((c >= 'A') && (c <= 'Z')) || IsGermanChar(c) || ((c >= '0') &&
                                (c <= '9')) || (c == '@') || (c == '#') || (c == '$') || (c == '_') || (c == '.') || (c == '*'))
                            {
                                //if we have constructs like identifier.* then we have to threat it at as a single token
                                if (c == '*' && tempString.Substring(tempString.Length - 1) != ".")
                                {
                                    //do nothing; in this case * is a math operator
                                    break;
                                }

                                //there can be white spaces between tables/aliases and column names e.g a. col
                                if (c == '.')
                                {
                                    tempString += c;
                                    c = GetChar();
                                    while ((c == ' ') || (c == 13) || (c == 10) || (c == 9))
                                    {
                                        c = GetChar();
                                    }
                                }
                                else
                                {
                                    tempString += c;
                                    c = GetChar();
                                }
                            }

                            int i;
                            for (i = 0; (i < keywords.Length) && (tempString.ToUpper() != keywords[i]); i++) ;

                            if (IsTokenKeywordAllowedAfterSELECT(i))
                            {
                                newToken = new TokenAllowedRightAfterSELECT();
                                newToken.SetToken(Tokens.Keyword, keywords[i]);
                            }                                                                             //asz:function must follow by Left paranthese, otherwise LEFT JOIN will not be detected as keyword
                            else if (FunctionTranslator.IsFunction(DatabaseBrand.Informix, tempString) && Peek().LexicalCode == Tokens.LeftParant)
                            {
                                newToken.SetToken(Tokens.Function, tempString.ToUpper());
                            }
                            else if (IsTokenDataTypeKeyword(tempString))
                            {
                                newToken.SetToken(Tokens.DataTypeKeyword, tempString.ToUpper());
                            }
                            else if (IsTokenKeyword(i))
                            {
                                newToken.SetToken(Tokens.Keyword, keywords[i]);
                            }                          
                            else if (Peek().LexicalCode == Tokens.LeftParant)
                            {
                                newToken.SetToken(Tokens.UserDefinedFunction, tempString.ToUpper());
                            }
                            //If the statement is already prepared then the prefix for bind variables can also be '@'
                            else if (tempString.StartsWith("@"))
                            {
                                newToken.SetToken(Tokens.BindVariable, tempString);
                            }
                            else
                            {
                                newToken.SetToken(Tokens.Identifier, tempString);
                            }

                            foundToken = true;
                            state = 0;
                            break;
                        }
					
					
					//FIX 2006.03.06
					//bind variables can also contain '.', '(' , ')', '[', ']'
					//inside paranthesis ',' , ' ', '*', '/', '+', '-' can also occur
					case 2: //bind variable
					{
						bool operatorsEnabled = false;
						int nestingLevel = 0;

						while ( ((c >= 'a')&&(c <= 'z')) || ((c >= 'A')&&(c <= 'Z'))  || IsGermanChar(c) || ((c >= '0') && 
							(c <= '9'))|| (c == '@') || (c == '#') || (c == '$') || (c == '_') || (c == '.') ||
							(c == '(') || (c == '[') || (c == ']') || (c == '"') || 
							(operatorsEnabled && ((c == ' ') || (c == ',') || 
							(c == '+') || (c == '-') || (c == '*') || (c == '/') || (c == ')') ))) 
						{
							tempString += c;

							if((c == '(') || (c == '['))
							{
								operatorsEnabled = true;
								nestingLevel++;
							}
							else if((c == ')') || (c == ']'))
							{
								nestingLevel--;
								if(nestingLevel == 0)
									operatorsEnabled = false;
							}

							c = GetChar();
						}
						newToken.SetToken(Tokens.BindVariable,tempString);
						foundToken = true;
						state = 0;
						break;
					}

					case 3: //numeric constants
					{
						//integer constant
						while ( (c >= '0') && (c <= '9') )
						{
							tempString += c;
							c = GetChar();
						}

                        //datetime constants
                        //([d]d|dddd)(-|/|.)([d]d|www)(-|/|.)([d]d|dddd)[-][dd(:|.)dd(:|.)dd(:|.)dddddd][ |:|.][AM|PM]
                        //!!! In SqlBase if any of these formats is found the expression is evaluated as a datetime constant even if it takes part in a numeric expression
                        //e.g 10+1900-1-1 will return 11-JAN-1900 (10 days are added to the date 1900-1-1) or if it is in a condition:
                        //numeric_field = 10+1900-1-1 then it will return 12 (because 11-JAN-1900 means 12 days from 30-DEC-1899, which is the default date)
                        string datetime = string.Empty;
                        string time = string.Empty;
                        string tempDatetime = tempString;
                        string ampm = string.Empty;
                        bool valid = false;
                        //in case the constant is not a valid datetime constant, the CursorPosition will be restored to this position
                        int cursorPositionToRestore = CursorPosition - 1;
                        if ((tempDatetime.Length == 1 || tempDatetime.Length == 2 || tempDatetime.Length == 4) &&
                            (c == '-' || c == '/' || c == '.' || c == ':')) 
                        {
                            if (c == '-' || c == '/' || c == '.')
                            {
                                char separator = c;
                                tempDatetime += c;
                                c = GetChar();
                                if (IsDigit(c) || IsAlphabetic(c))
                                {
                                    if (IsDigit(c))
                                    {
                                        valid = true;
                                        tempDatetime += c;
                                        c = GetChar();
                                        if (IsDigit(c))
                                        {
                                            tempDatetime += c;
                                            c = GetChar();
                                        }
                                    }
                                    else
                                    {
                                        string month = string.Empty;
                                        month += c;
                                        c = GetChar();
                                        if (IsAlphabetic(c))
                                        {
                                            month += c;
                                            c = GetChar();
                                        }
                                        if (IsAlphabetic(c))
                                        {
                                            month += c;
                                            c = GetChar();
                                        }
                                        if (Array.IndexOf(months, month.ToUpper()) != -1)
                                        {
                                            tempDatetime += month;
                                            valid = true;
                                        }
                                    }
                                    if (valid && c == separator)
                                    {
                                        valid = false;
                                        tempDatetime += c;
                                        c = GetChar();
                                        int yearCount = 0;
                                        while (IsDigit(c))
                                        {
                                            yearCount++;
                                            tempDatetime += c;
                                            c = GetChar();
                                        }
                                        if (yearCount > 0 && yearCount <= 4)
                                        {
                                            //at this point we got a valid date constant; next we check if there is a time part too
                                            cursorPositionToRestore = CursorPosition - 1;
                                            datetime = tempDatetime;
                                            valid = true;
                                        }
                                    }
                                }
                                if (!valid)
                                {
                                    CursorPosition = cursorPositionToRestore;
                                }
                            }
                            else if (c == ':')
                            {
                                time = tempDatetime;
                                tempDatetime = String.Empty;
                                valid = true;
                            }
                            if (valid)
                            {
                                //time part
                                valid = false;
                                if (c == ' ' || c == '-')
                                {
                                    if (c == '-')
                                    {
                                        //don't add the - char because it's not supported in SqlServer (it's easier to remove it here)
                                        tempDatetime += c;
                                        c = GetChar();
                                    }
                                    else
                                    {
                                        while (c == ' ')
                                        {
                                            tempDatetime += c;
                                            c = GetChar();
                                        }
                                    }
                                }

                                //the time part can be determined with a regular expression
                                while (c != ' ' && c != ')' && c != 13 && c != 10 && c != 9 && c != '\0' && c != ',')
                                {
                                    time += c;
                                    c = GetChar();
                                }
                                Regex reg = new Regex(@"^\d{1,2}(:|.)?\d{1,2}?(:|.)?\d{1,2}?(:|.)?\d{1,6}?$");
                                Match match = reg.Match(time);
                                if (match.Success)
                                {
                                    //we got the time part too; next check for AM/PM
                                    cursorPositionToRestore = CursorPosition - 1;
                                    datetime = tempDatetime + time;
                                    valid = true;
                                }
                            }
                            if (!valid)
                            {
                                CursorPosition = cursorPositionToRestore;
                            }
                            else
                            {
                                //AM/PM
                                valid = false;
                                if (c == ' ' || c == '-' || c== ':')
                                {
                                    if (c == '-' || c == ':')
                                    {
                                        ampm += c;
                                    }
                                    else
                                    {
                                        while (c == ' ')
                                        {
                                            ampm += c;
                                            c = GetChar();
                                        }
                                    }

                                    if (c == 'A' || c == 'a' || c == 'P' || c == 'p')
                                    {
                                        ampm += c;
                                        c = GetChar();
                                        if (c == 'M' || c == 'm')
                                        {
                                            ampm += c;
                                            c = GetChar();
                                            if (c == ' ' || c == ')' || c == ',')
                                            {
                                                datetime += ampm;
                                                valid = true;
                                            }
                                        }
                                    }
                                }
                            }
                            if (!valid)
                            {
                                CursorPosition = cursorPositionToRestore;
                                if (c != '\0')
                                {
                                    c = GetChar();
                                }
                            }
                            if (!String.IsNullOrEmpty(datetime))
                            {
                                newToken.SetToken(Tokens.DatetimeConst, datetime);
                                foundToken = true;
                                state = 0;
                                break;
                            }
                        }

						//hexa constants
						if(tempString == "0" && (c == 'x' || c== 'X'))
						{
							tempString += c;
							c = GetChar();
							while(((c >= '0') && (c <= '9')) || ((c >= 'A') && (c <= 'F')))
							{
								tempString += c;
								c = GetChar();
							}
						}

						//numeric constant with decimal point
						else if(c == '.')
						{
							tempString += '.';
							c = GetChar();
							while ( (c >= '0') && (c <= '9') )
							{
								tempString += c;
								c = GetChar();
							}
							//float constant (E notation)
							if( (c == 'E') || (c == 'e') )
							{
								tempString += c;
								c = GetChar();
								if(c == '+')
								{
									tempString += c;
									c = GetChar();
								}
								while ( (c >= '0') && (c <= '9') )
								{
									tempString += c;
									c = GetChar();
								}
							}
						}
						newToken.SetToken(Tokens.NumericConst,tempString);
						foundToken = true;
						state = 0;
						break;
					}

					case 4: //string constants (also includes date/time constants)
					{
                        while (c != '\'' && c != '\0' && c != '\"')
						{
							tempString += c;
							c = GetChar();
						}

                        if (c == '\"')
                        {
                            tempString += '\"';
                        }
                        else if (!tempString.EndsWith("\""))
                        {
                            tempString += '\'';
                        }
                        
                        c = GetChar();
                        if (c == '\'')
                        {
                            //Escaped quote
                            tempString += c;
                            c = GetChar();
                            state = 4;
                        }
                        else if (c == '\"')
                        {
                            tempString += c;
                            c = GetChar();
                            state = 4;
                        }

                        if (c == '\'')
                        {
                            //Escaped quote
                            tempString += c;
                            c = GetChar();
                            state = 4;
                        }

                        if (c == '\'')
                        {
                            //Escaped quote
                            tempString += c;
                            c = GetChar();
                            state = 4;
                        }
                        else if ((tempString.StartsWith("\'") && tempString.EndsWith("\"")) ||
                            (tempString.StartsWith("\"") && tempString.EndsWith("\'")) ||
                            (tempString.StartsWith("\'") && tempString.EndsWith("\'\'") && tempString.Length > 2) ||
                            ((tempString.Split('\'').Length - 1) == tempString.Length && (tempString.Split('\'').Length - 1) % 2 != 0))
                        {
                            // If the string starts with a quotation mark and ends with a different type of quotation mark
                            // or if the string starts with single quotation mark and ends with two single quotation marks
                            // ex: 'string that''s quoted' continue to read because the quotes are escaped
                            // If the string starts with quotation mark and ends with two quotation marks and has a length 
                            // greater than 2 continue reading. If the length is 2 the string is empty
                            // If the string contains just quotation marks but the number of quotation marks is not even
                            // keep reading until the quotation marks are matched
                            tempString += c;
                            c = GetChar();
                            state = 4;
                        }
                        else
                        {
                            newToken.SetToken(Tokens.StringConst, tempString);
                            foundToken = true;
                            state = 0;
                        }
						break;
					}
					
				}
			} while (!foundToken);	
			
			return newToken;
		}
	}
}
