using System;
using System.Collections.Generic;
using System.Text;

namespace fecher.SpTranslator
{
    public struct SpStatistics
    {
        private int statements;
        private int comments;
        private int whenSqlError;
        private int onProcedureFetch;

        public int Statements
        {
            get { return statements; }
            set { statements = value; }
        }

        public int Comments
        {
            get { return comments; }
            set { comments = value; }
        }

        public int WhenSqlError
        {
            get { return whenSqlError; }
            set { whenSqlError = value; }
        }

        public int OnProcedureFetch
        {
            get { return onProcedureFetch; }
            set { onProcedureFetch = value; }
        }


        public static SpStatistics operator +(SpStatistics s1, SpStatistics s2)
        {
            s1.Statements += s2.Statements;
            s1.Comments += s2.Comments;
            s1.WhenSqlError += s2.WhenSqlError;
            s1.OnProcedureFetch += s2.OnProcedureFetch;
            return s1;
        }
    }
}
