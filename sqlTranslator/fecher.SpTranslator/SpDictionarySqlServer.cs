using System;
using System.Collections.Specialized;
using System.Text;

namespace fecher.SpTranslator
{
    internal static class SpDictionarySqlServer
    {
        private const string CONST_NUMBER_NULL =        "NULL";
        private const string CONST_DATETIME_NULL =      "NULL";
        private const string CONST_STRING_NULL =        "NULL";
        private const string CONST_TRUE =               "1";
        private const string CONST_FALSE =              "0";
        private const string CONST_FETCH_DELETE =       "-2";
        private const string CONST_FETCH_EOF =          "-1";
        private const string CONST_FETCH_OK =           "0";
        private const string CONST_FETCH_UPDATE =       "-1";
        
        private const string DATA_BOOLEAN =             "int";
        private const string DATA_SQLHANDLE =           "cursor";
        private const string DATA_DATETIME =            "datetime";
        private const string DATA_STRING =              "nvarchar(255)";
        private const string DATA_LONGSTRING =          "ntext";
        private const string DATA_NUMBER =              "float";
        private const string DATA_WINDOWHANDLE =        "not supported";
        private const string DATA_FILEHANDLE =          "not supported";
        
        private const string FUN_SQLCLEARIMMEDIATE =    "";
        private const string FUN_SQLCLOSE =             "DEALLOCATE {0}";
        private const string FUN_SQLCOMMIT =            "COMMIT";
        private const string FUN_SQLCONNECT =           "";
        private const string FUN_SQLDISCONNECT =        "CLOSE {0}";
        private const string FUN_SQLDROPSTOREDCMD =     "DROP PROCEDURE {0}";
        private const string FUN_SQLERROR =             "ERROR_NUMBER()";
        private const string FUN_SQLEXECUTE =           "OPEN {0}";
        private const string FUN_SQLEXISTS =            @"{0}
IF @@ROWCOUNT > 0 SET {1} = 1
ELSE SET {1} = 0";
        private const string FUN_SQLFETCHNEXT =         "FETCH NEXT FROM {0}";
        private const string FUN_SQLFETCHPREVIOUS =     "FETCH PRIOR FROM {0}";
        private const string FUN_SQLFETCHROW =          "FETCH ABSOLUTE {0} FROM {1}";
        private const string FUN_SQLFETCH =             "SET {0} = @@FETCH_STATUS";
        private const string FUN_SQLGETERRORPOSITION =  "not supported";
        private const string FUN_SQLGETERRORTEXT =      "SET {0} = FORMATMESSAGE ({1})";
        private const string FUN_SQLGETMODIFIEDROWS =   "SET {0} = @@ROWCOUNT";
        private const string FUN_SQLGETPARAMETER1 =     "SET {0} = @@LOCK_TIMEOUT";
        private const string FUN_SQLGETPARAMETER2 =     "SET {0} = @@VERSION";
        private const string FUN_SQLGETPARAMETERALL =   "not supported";
        private const string FUN_SQLGETRESULTSETCOUNT = "SET {0} = @@ROWCOUNT";
        private const string FUN_SQLGETROLLBACKFLAG =   "not supported";
        private const string FUN_SQLIMMEDIATE =         "{0}";
        private const string FUN_SQLOPEN =              "OPEN {0}";
        private const string FUN_SQLPREPARE =           "DECLARE {0} CURSOR FOR {1}";
        private const string FUN_SQLPREPAREANDEXECUTE = @"DECLARE {0} CURSOR FOR {1}
OPEN {0}";
        private const string FUN_SQLRETRIEVE =          "EXECUTE {0} {1}";
        private const string FUN_SQLSETISOLATIONLEVEL = @"SET TRANSACTION ISOLATION LEVEL {0}";
        private const string FUN_SQLSETLOCKTIMEOUT =    "SET LOCK_TIMEOUT {0} * 1000";
        private const string FUN_SQLSETPARAMETER =      "SET LOCK_TIMEOUT {0} * 1000";
        private const string FUN_SQLSETPARAMETERALL =   "not supported";
        private const string FUN_SQLSETRESULTSET =      "not supported";
        private const string FUN_SQLSTORE1 =            "EXECUTE( 'CREATE PROCEDURE {0} AS {1}' )";
        private const string FUN_SQLSTORE2 =            "EXECUTE( {0} )"; 

        private static StringDictionary dict;

        static SpDictionarySqlServer()
        {
            dict = new StringDictionary();

            dict.Add("NUMBER_NULL", CONST_NUMBER_NULL);
            dict.Add("DATETIME_NULL", CONST_DATETIME_NULL);
            dict.Add("STRING_NULL", CONST_STRING_NULL);
            dict.Add("STRNULL", CONST_STRING_NULL); //???
            dict.Add("FALSE", CONST_FALSE);
            dict.Add("TRUE", CONST_TRUE);
            dict.Add("FETCH_DELETE", CONST_FETCH_DELETE);
            dict.Add("FETCH_EOF", CONST_FETCH_EOF);
            dict.Add("FETCH_OK", CONST_FETCH_OK);
            dict.Add("FETCH_UPDATE", CONST_FETCH_UPDATE);

            dict.Add("BOOLEAN", DATA_BOOLEAN);
            dict.Add("DATE/TIME", DATA_DATETIME);
            dict.Add("FILE HANDLE", DATA_FILEHANDLE);
            dict.Add("LONG STRING", DATA_LONGSTRING);
            dict.Add("NUMBER", DATA_NUMBER);
            dict.Add("SQL HANDLE", DATA_SQLHANDLE);
            dict.Add("STRING", DATA_STRING);
            dict.Add("WINDOW HANDLE", DATA_WINDOWHANDLE);
            
            dict.Add("SQLCLEARIMMEDIATE", FUN_SQLCLEARIMMEDIATE);
            dict.Add("SQLCLOSE", FUN_SQLCLOSE);
            dict.Add("SQLCOMMIT", FUN_SQLCOMMIT);
            dict.Add("SQLCONNECT", FUN_SQLCONNECT);
            dict.Add("SQLDISCONNECT", FUN_SQLDISCONNECT);
            dict.Add("SQLDROPSTOREDCMD", FUN_SQLDROPSTOREDCMD);
            dict.Add("SQLERROR", FUN_SQLERROR);
            dict.Add("SQLEXECUTE", FUN_SQLEXECUTE);
            dict.Add("SQLEXISTS", FUN_SQLEXISTS);
            dict.Add("SQLFETCHNEXT", FUN_SQLFETCHNEXT);
            dict.Add("SQLFETCHPREVIOUS", FUN_SQLFETCHPREVIOUS);
            dict.Add("SQLFETCHROW", FUN_SQLFETCHROW);
            dict.Add("SQLGETERRORPOSITION", FUN_SQLGETERRORPOSITION);
            dict.Add("SQLGETERRORTEXT", FUN_SQLGETERRORTEXT);
            dict.Add("SQLGETMODIFIEDROWS", FUN_SQLGETMODIFIEDROWS);
            dict.Add("SQLGETPARAMETER1", FUN_SQLGETPARAMETER1);
            dict.Add("SQLGETPARAMETER2", FUN_SQLGETPARAMETER2);
            dict.Add("SQLGETPARAMETERALL", FUN_SQLGETPARAMETERALL);
            dict.Add("SQLGETRESULTSETCOUNT", FUN_SQLGETRESULTSETCOUNT);
            dict.Add("SQLGETROLLBACKFLAG", FUN_SQLGETROLLBACKFLAG);
            dict.Add("SQLIMMEDIATE", FUN_SQLIMMEDIATE);
            dict.Add("SQLOPEN", FUN_SQLOPEN);
            dict.Add("SQLPREPARE", FUN_SQLPREPARE);
            dict.Add("SQLPREPAREANDEXECUTE", FUN_SQLPREPAREANDEXECUTE);
            dict.Add("SQLRETRIEVE", FUN_SQLRETRIEVE);
            dict.Add("SQLSETISOLATIONLEVEL", FUN_SQLSETISOLATIONLEVEL);
            dict.Add("SQLSETLOCKTIMEOUT", FUN_SQLSETLOCKTIMEOUT);
            dict.Add("SQLSETPARAMETER", FUN_SQLSETPARAMETER);
            dict.Add("SQLSETPARAMETERALL", FUN_SQLSETPARAMETERALL);
            dict.Add("SQLSETRESULTSET", FUN_SQLSETRESULTSET);
            dict.Add("SQLSTORE1", FUN_SQLSTORE1);
            dict.Add("SQLSTORE2", FUN_SQLSTORE2);
            dict.Add("SQLFETCH", FUN_SQLFETCH);
        }

        public static string GetValue(string key)
        {
            return dict[key];
        }

        public static string GetValue(string key, params string[] paramList)
        {
            try
            {
                return String.Format(dict[key], paramList);
            }
            catch (FormatException ex)
            {
                //TODO: handle exception
                throw;
            }
        }
    }
}


