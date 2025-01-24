using System;
using System.Collections.Generic;
using System.Text;

namespace SQLTranslatorTest
{
    class DataTablesException : ApplicationException
    {
        public DataTablesException()
            : base()
        {}

        public DataTablesException(string message)
            : base(message)
        {}

        public DataTablesException(string message, Exception ex)
            : base(message, ex)
        {}
    }
}
