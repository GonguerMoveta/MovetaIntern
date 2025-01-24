using System;
using fecher.Common;
using System.Text.RegularExpressions;

namespace fecher.SpTranslator
{
    /// <summary>
    /// This class is used to delimitate the tokens in the string representing the SQL statement.
    /// It is used by the SyntacticalAnalyzer class.
    /// </summary>
    public class LexicalAnalyzer
    {
        #region Private Members
        private string[] keywords = {"ACTIONS",		"AND",			"BEGIN",
									 "BREAK",		"CALL",			"CLOSE",
									 "ELSE",		"END",		    "EXECUTE",		
									 "FETCH",		"IF",		    "LOCAL",			
									 "LOOP",		"NOT",			"ON",	
									 "OR",			"PARAMETERS",	"PROCEDURE",
									 "RECEIVE",		"RETURN",		"SET",
									 "SQLERROR",	"STARTUP",		"TRACE", 
									 "VARIABLES",	"WHEN",			"WHILE",
                                     "DYNAMIC",     "STATIC",
									 //System keywords
									 "DATETIME_NULL",			"DBP_AUTOCOMMIT",	    "DBP_BRAND",
									 "DBP_LOCKWAITTIMEOUT",		"DBP_LOCKWAITTIMEOUT",	"DBP_PRESERVE",
									 "DBP_FETCHTHROUGH",        "DBP_NOPREBUILD",       "DBP_ISOLEVEL",
                                     "DBP_ROLLBACKONTIMEOUT",	"DBP_VERSION",	        "DBV_BRAND_DB2",
									 "DBV_BRAND_ORACLE",		"DBV_BRAND_SQL",		"FALSE",
									 "FETCH_DELETE",			"FETCH_EOF",		    "FETCH_OK",
									 "FETCH_UPDATE",			"NUMBER_NULL",		    "TRUE",
                                     "STRING_NULL",             "STRNULL",
                                     //Data types
									 "BOOLEAN",			"DATE/TIME",		"FILE",
									 "HANDLE",			"LONG",		        "NUMBER",
									 "SQL",		        "STRING",		    "WINDOW",
									};

        private string[] functions = {"SQLCLEARIMMEDIATE",      "SQLCLOSE",             "SQLCOMMIT",
                                      "SQLCONNECT",             "SQLDISCONNECT",        "SQLDROPSTOREDCMD",
                                      "SQLERROR",               "SQLEXECUTE",           "SQLEXISTS",
                                      "SQLFETCHNEXT",           "SQLFETCHPREVIOUS",     "SQLFETCHROW",
                                      "SQLGETERRORPOSITION",    "SQLGETERRORTEXT",      "SQLGETMODIFIEDROWS",
                                      "SQLGETPARAMETER",        "SQLGETPARAMETERALL",   "SQLGETRESULTSETCOUNT",
                                      "SQLGETROLLBACKFLAG",     "SQLIMMEDIATE",         "SQLOPEN",
                                      "SQLPREPARE",             "SQLPREPAREANDEXECUTE", "SQLRETRIEVE",
                                      "SQLSETISOLATIONLEVEL",   "SQLSETLOCKTIMEOUT",    "SQLSETPARAMETER",
                                      "SQLSETPARAMETERALL",     "SQLSETRESULTSET",      "SQLSTORE"};

        private static string[] months =  {"JAN", "FEB", "MAR", "APR", "MAY", "JUN",
                                           "JUL", "AUG", "SEP", "OCT", "NOV", "DEC"};
        private char[] germanCharset = { 'ä', 'Ä', 'ö', 'Ö', 'ü', 'Ü', 'ß' };
        private int cursorPosition = 0;
        private char c = ' ';
        private string sourceSql;
        private int indentLevel = 0;
        private int CursorPosition = 0;
        #endregion

        #region Properties
        public string SourceSql
        {
            set { sourceSql = value; }
        }
        #endregion

        #region Private Methods
        public bool IsKeyword(string word)
        {
            word = word.ToUpper();

            foreach (string s in keywords)
                if (s == word)
                    return true;
            return false;
        }

        private bool IsTokenKeyword(int keywordIndex)
        {
            return keywordIndex >= 0 && keywordIndex < 29;
        }

        private bool IsTokenSysKeyword(int keywordIndex)
        {
            return keywordIndex >= 29 && keywordIndex < 52;
        }

        private bool IsTokenDataTypeKeyword(int keywordIndex)
        {
            return keywordIndex >= 52 && keywordIndex < keywords.Length;
        }

        private bool IsTokenSqlFunction(string token)
        {
            int i;

            for (i = 0; (i < functions.Length) && (token.ToUpper() != functions[i]); i++) ;
            if (i < functions.Length)
                return true;
            return false;
        }

        private bool IsGermanChar(char c)
        {
            for (int i = 0; i < germanCharset.Length; i++)
            {
                if (c == germanCharset[i])
                {
                    return true;
                }
            }
            return false;
        }

        //Reads the next character from the input string
        private char GetChar()
        {
            char[] c;
            if (cursorPosition < sourceSql.Length)
            {
                c = sourceSql.ToCharArray(cursorPosition, 1);
                cursorPosition++;
                return c[0];
            }
            else
                return '\0';
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Returns the next token
        /// </summary>
        /// <returns></returns>
        public IToken GetToken()
        {
            IToken newToken = new Token();
            bool foundToken = false;
            int state = 0;
            string tempString = "";

            do
            {
                switch (state)
                {
                    case 0:
                        {
                            while ((c == ' ') || (c == 13) || (c == 10) || (c == 9)) //set the indentation level
                            {
                                if (c == 13 || c == 10)
                                {
                                    indentLevel = 0;
                                }
                                else if ((c == ' ') || (c == 9))
                                {
                                    indentLevel++;
                                }
                                c = GetChar();
                            }

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
                                newToken.SetToken(Tokens.Colon, ":");
                                foundToken = true;
                                c = GetChar();
                                break;
                            }

                            else if (c == ',')
                            {
                                newToken.SetToken(Tokens.Comma, ",");
                                foundToken = true;
                                c = GetChar();
                                break;
                            }

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
                                else //comment
                                {
                                    while (c != 13 && c != 10 && c != '\0')
                                    {
                                        tempString += c;
                                        c = GetChar();
                                    }
                                    newToken.SetToken(Tokens.Comment, tempString, indentLevel);
                                    foundToken = true;
                                    //c = GetChar();
                                    break;
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

                            else if (c >= '0' && c <= '9')
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

                            else if (c == '\\') //delimiter for multiline statements
                            {
                                c = GetChar();
                                break;
                            }

                            else if (c == '\0') //end of string
                            {
                                newToken.SetToken(Tokens.Null);
                                foundToken = true;
                                break;
                            }

                            else //error: unknown character
                            {
                                throw new NotSupportedException("Character " + c + " is not supported by SpTranslator");
                            }
                        }

                    case 1: //keyword, function or identifier
                        {
                            while (((c >= 'a') && (c <= 'z')) || ((c >= 'A') && (c <= 'Z')) || IsGermanChar(c) || ((c >= '0') &&
                                (c <= '9')) || (c == '@') || (c == '#') || (c == '$') || (c == '_') || (c == '.'))
                            {
                                tempString += c;
                                c = GetChar();
                            }

                            if (c == '/') //check if it's the DATE/TIME reserved word
                            {
                                if (tempString.ToUpper() == "DATE")
                                {
                                    c = GetChar();
                                    if (Peek().LiteralValue.ToUpper() == "TIME")
                                    {
                                        //consume the token
                                        GetToken();
                                        newToken.SetToken(Tokens.DataTypeKeyword, "DATE/TIME");
                                        foundToken = true;
                                    }
                                }
                            }

                            if (!foundToken)
                            {
                                int i;
                                for (i = 0; (i < keywords.Length) && (tempString.ToUpper() != keywords[i]); i++) ;

                                //we only need the indentation level for keywords because
                                //every statement begins with a keyword
                                //SQLERROR is both a keyword and a function
                                if (IsTokenKeyword(i) &&
                                    (tempString.ToUpper() != "SQLERROR" ||
                                    (tempString.ToUpper() == "SQLERROR" && Peek().LexicalCode != Tokens.LeftParant)))
                                    newToken.SetToken(Tokens.Keyword, keywords[i], indentLevel);
                                else if (IsTokenSysKeyword(i))
                                    newToken.SetToken(Tokens.SysKeyword, keywords[i]);
                                else if (IsTokenDataTypeKeyword(i))
                                    newToken.SetToken(Tokens.DataTypeKeyword, keywords[i]);
                                else if (IsTokenSqlFunction(tempString))
                                    newToken.SetToken(Tokens.Function, tempString.ToUpper());
                                else if (Peek().LexicalCode == Tokens.LeftParant)
                                    newToken.SetToken(Tokens.UserDefinedFunction, tempString);
                                else
                                    newToken.SetToken(Tokens.Identifier, tempString);
                            }

                            foundToken = true;
                            state = 0;
                            break;
                        }

                    case 3: //numeric constants
                        {
                            //integer constant
                            while ((c >= '0') && (c <= '9'))
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
                                    if (c == ' ' || c == '-' || c == ':')
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
                            if (tempString == "0" && (c == 'x' || c == 'X'))
                            {
                                tempString += c;
                                c = GetChar();
                                while (((c >= '0') && (c <= '9')) || ((c >= 'A') && (c <= 'F')))
                                {
                                    tempString += c;
                                    c = GetChar();
                                }
                            }

                            //numeric constant with decimal point
                            else if (c == '.')
                            {
                                tempString += '.';
                                c = GetChar();
                                while ((c >= '0') && (c <= '9'))
                                {
                                    tempString += c;
                                    c = GetChar();
                                }
                                //float constant (E notation)
                                if ((c == 'E') || (c == 'e'))
                                {
                                    tempString += c;
                                    c = GetChar();
                                    if (c == '+')
                                    {
                                        tempString += c;
                                        c = GetChar();
                                    }
                                    while ((c >= '0') && (c <= '9'))
                                    {
                                        tempString += c;
                                        c = GetChar();
                                    }
                                }
                            }
                            newToken.SetToken(Tokens.NumericConst, tempString);
                            foundToken = true;
                            state = 0;
                            break;
                        }

                    case 4: //string constants (also includes date/time constants)
                        {
                            while (c != '\'')
                            {
                                //check for escaped '
                                if (c == '\\')
                                {
                                    tempString += c;
                                    c = GetChar();
                                }
                                tempString += c;
                                c = GetChar();
                            }
                            tempString += '\'';
                            newToken.SetToken(Tokens.StringConst, tempString);
                            foundToken = true;
                            c = GetChar();
                            state = 0;
                            break;
                        }

                }
            } while (!foundToken);

            return newToken;
        }

        /// <summary>
        /// Returns the next token but it doesn't consume it (the cursor will be reestablished to the beginning of the token
        /// </summary>
        /// <returns></returns>
        public IToken Peek()
        {
            int tempCursorPosition = cursorPosition;
            IToken token = new Token();
            token = GetToken();

            //restor cursor position
            //TODO AV should not move back if reached EOL
            if (!(token.LexicalCode == Tokens.Null))
            {
                cursorPosition = tempCursorPosition - 1;
                c = GetChar();
            }

            return token;
        }

        private bool IsDigit(char c)
        {
            return (c >= '0') && (c <= '9');
        }

        private bool IsAlphabetic(char c)
        {
            return ((c >= 'a') && (c <= 'z')) || ((c >= 'A') && (c <= 'Z'));
        }
        #endregion
    }
}

