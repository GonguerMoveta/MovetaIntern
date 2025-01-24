using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using fecher.Common;

namespace SQLTranslatorTest
{
    public static class Globals
    {
        public static WorkSpace WkSpace;

        public static StringDictionary SqlBaseConnProp;
        public static StringDictionary SqlServerConnProp;
        public static StringDictionary OracleConnProp;

        public static DatabaseSettings Settings = new DatabaseSettings();
    }
}
