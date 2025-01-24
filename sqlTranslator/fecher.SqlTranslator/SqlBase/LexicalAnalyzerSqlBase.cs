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
    public class LexicalAnalyzerSqlBase : LexicalAnalyzer
    {
        public bool IsTokenKeyword(int keywordIndex)
        {
            return keywordIndex >= 0 && keywordIndex <= 97;
        }

        public bool IsTokenSysKeyword(int keywordIndex)
        {
            return keywordIndex > 97 && keywordIndex <= 119;
        }

        public bool IsTokenDataTypeKeyword(int keywordIndex)
        {
            return keywordIndex > 119 && keywordIndex < keywords.Length;
        }

        /// <summary>
        /// keywords allowed right after SELECT token: ALL|DINSTINCT|TOP
        /// </summary>
        /// <param name="keywordIndex"></param>
        /// <returns></returns>
        public bool IsTokenKeywordAllowedAfterSELECT(int keywordIndex)
        {
            return keywordIndex == 1 || keywordIndex == 40 || keywordIndex == 85;
        }


        public static bool IsKeyword(string word)
        {
            word = word.ToUpper();

            foreach (string s in keywords)
                if (s == word)
                    return true;
            return false;
        }

        /// <summary>
        /// Return true if the keyword is an end to a SELECT clause. Keywords are: FROM, INTO
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public bool IsKeywordBreakingSELECT(string keyword)
        {
            keyword = keyword.ToUpper();

            return (keyword == "FROM" || keyword == "INTO");
        }

        private static string[] keywords = {"ADJUSTING",       "ALL",          "AND",
                                     "AS",              "ASC",          "BETWEEN",
                                     "BY",              "CHECK",        "CROSS",
                                     "CURRENT",         "DELETE",       "DESC",
                                     "EXISTS",          "FOR",          "FROM",
                                     "FULL",            "GROUP",        "HAVING",
                                     "IN",              "INNER",        "INSERT",
                                     "INTO",            "IS",           "JOIN",
                                     "LEFT",            "LIKE",         "NATURAL",
                                     "NOT",             "ON",           "UNION",
                                     "OR",              "OF",           "ORDER",
                                     "OUTER",           "SELECT",       "SET",
                                     "UPDATE",          "USING",        "VALUES",
                                     "WHERE",           "DISTINCT",     "CREATE",
                                     "INDEX",           "UNIQUE",       "CLUSTERED",
                                     "HASHED",          "PCTFREE",      "SIZE",
                                     "ROWS",            "BUCKETS",      "TABLE",
                                     "WITH",            "DEFAULT",      "PRIMARY",
                                     "KEY",             "FOREIGN",      "REFERENCES",
                                     "RESTRICT",        "CASCADE",      "DATABASE",
                                     "VIEW",            "OPTION",       "DROP",
                                     "COMMIT",          "WORK",         "TRANSACTION",
                                     "FORCE",           "END",          "CONVERT",
                                     "ROLLBACK",        "SAVEPOINT",    "STATISTICS",
                                     "DISTINCTCOUNT",   "SYSTEM",       "ONLY",
                                     "PUBLIC",          "SYNONYM",      "EXTERNAL",
                                     "FUNCTION",        "COMMAND",      "PROCEDURE",
                                     "ALTER",           "ADD",          "RENAME",
                                     "MODIFY",          "TOP",          "REVOKE",
                                     "CONNECT",         "DBA",          "RESOURCE",
                                     "SYSADM",          "EXECUTE",      "GRANT",
                                     "IDENTIFIED",      "TO",           "STORE",
                                     "TRIGGER",         "AUTO_INCREMENT", "LOCK",

									 //System keywords
									 "NULL",            "SYSDATETIME",  "SYSDATE",
                                     "SYSTIME",         "SYSTIMEZONE",  "USER",
                                     "MICROSECOND",     "MICROSECONDS", "SECOND",
                                     "SECONDS",         "MINUTE",       "MINUTES",
                                     "HOUR",            "HOURS",        "DAY",
                                     "DAYS",            "MONTH",        "MONTHS",
                                     "YEAR",            "YEARS",        //"ROWID",
                                     "SYSDBSEQUENCE.CURRVAL", "SYSDBSEQUENCE.NEXTVAL",
									 //Data types
									 "CHAR",            "VARCHAR",      "DECIMAL",
                                     "FLOAT",           "INTEGER",      "LONG",
                                     "NUMBER",          "SMALLINT",     "DATE",
                                     "DATETIME",        "TIME",         "TIMESTAMP",
                                     "DEC",             "INT",          "DOUBLE",
                                     "PRECISION",       "REAL"
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
                switch (state)
                {
                    case 0:
                        {
                            //ignore spaces, tabs and new lines + backslashes
                            while ((c == ' ') || (c == 13) || (c == 10) || (c == 9) || (c == '\\'))
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
                                c = GetChar();
                                while (c == ' ')
                                {
                                    c = GetChar();
                                }
                                if (c == '+')
                                {
                                    c = GetChar();
                                    while (c == ' ')
                                    {
                                        c = GetChar();
                                    }
                                    if (c == ')')
                                    {
                                        newToken.SetToken(Tokens.JoinOperator, "(+)");
                                        c = GetChar();
                                    }
                                }
                                else
                                    newToken.SetToken(Tokens.LeftParant, "(");
                                foundToken = true;
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

                            else if (c == '|' || c == '\\')
                            {
                                c = GetChar();
                                if (c == '\\')
                                {
                                    c = GetChar();
                                }
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

                            else if ((c >= '0' && c <= '9') || c == '.')
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
                                //newToken.SetToken(Tokens.Keyword, keywords[i], i);
                                newToken = new TokenAllowedRightAfterSELECT();
                                newToken.SetToken(Tokens.Keyword, keywords[i]);
                            }
                            else if (IsTokenKeyword(i))
                                newToken.SetToken(Tokens.Keyword, keywords[i]);
                            else if (IsTokenSysKeyword(i))
                                newToken.SetToken(Tokens.SysKeyword, keywords[i]);
                            else if (IsTokenDataTypeKeyword(i))
                                newToken.SetToken(Tokens.DataTypeKeyword, keywords[i]);
                            else if (FunctionTranslator.IsFunction(DatabaseBrand.SqlBase, tempString))
                                newToken.SetToken(Tokens.Function, tempString.ToUpper());
                            else if (Peek().LexicalCode == Tokens.LeftParant)
                                newToken.SetToken(Tokens.UserDefinedFunction, tempString.ToUpper());
                            //If the statement is already prepared then the prefix for bind variables can also be '@'
                            else if (tempString.StartsWith("@"))
                                newToken.SetToken(Tokens.BindVariable, tempString);
                            else
                                newToken.SetToken(Tokens.Identifier, tempString);

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

                            while (((c >= 'a') && (c <= 'z')) || ((c >= 'A') && (c <= 'Z')) || IsGermanChar(c) || ((c >= '0') &&
                                (c <= '9')) || (c == '@') || (c == '#') || (c == '$') || (c == '_') || (c == '.') ||
                                (c == '(') || (c == '[') || (c == ']') || (c == '"') ||
                                (operatorsEnabled && ((c == ' ') || (c == ',') ||
                                (c == '+') || (c == '-') || (c == '*') || (c == '/') || (c == ')'))))
                            {
                                tempString += c;

                                if ((c == '(') || (c == '['))
                                {
                                    operatorsEnabled = true;
                                    nestingLevel++;
                                }
                                else if ((c == ')') || (c == ']'))
                                {
                                    nestingLevel--;
                                    if (nestingLevel == 0)
                                        operatorsEnabled = false;
                                }

                                c = GetChar();
                            }
                            newToken.SetToken(Tokens.BindVariable, tempString);
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
                                    //RHE:
                                    Regex reg = null;
                                    Match match = null;
                                    reg = new Regex(@"^\d{2}?$");
                                    match = reg.Match(time);
                                    if (match.Success)
                                    {
                                        //we got the time part too; next check for AM/PM
                                        cursorPositionToRestore = CursorPosition - 1;
                                        datetime = tempDatetime + time + ":00";
                                        valid = true;
                                    }
                                    else
                                    {
                                        reg = new Regex(@"^\d{1,2}(:|.)?\d{1,2}?(:|.)?\d{1,2}?(:|.)?\d{1,6}?$");
                                        match = reg.Match(time);
                                        if (match.Success)
                                        {
                                            //we got the time part too; next check for AM/PM
                                            cursorPositionToRestore = CursorPosition - 1;
                                            datetime = tempDatetime + time;
                                            valid = true;
                                        }
                                    }                                    
                                }
                                if (!valid)
                                {
                                    //CursorPosition = cursorPositionToRestore;
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
                                    bool valueLonger = CursorPosition > cursorPositionToRestore;

                                    //BCU - if we have a valid datetime and we are already at the end of the statement, do not restore cursor position.
                                    if (String.IsNullOrEmpty(datetime) || !(c == '\0' && cursorPositionToRestore == CursorPosition - 1))
                                    {
                                        CursorPosition = cursorPositionToRestore;
                                    }

                                    if (c != '\0' || valueLonger)
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
                                if (tempString.StartsWith("\'") && tempString.EndsWith("\'\'") && tempString.Length > 2)
                                {
                                    tempString += '\'';
                                }
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
