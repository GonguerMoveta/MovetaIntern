using System;
using System.Collections.Generic;
using System.Text;
using fecher.Common;

namespace fecher.SpTranslator
{
    public class Cursor
    {
        private CursorStatus status;
        private SqlStatement sql;

        public CursorStatus Status
        {
            get { return status; }
            set { status = value; }
        }

        public SqlStatement Sql
        {
            get { return sql; }
            set { sql = value; }
        }

        public Cursor()
        {
            status = CursorStatus.None;
            sql = new SqlStatement();
        }

        public Cursor(CursorStatus status)
        {
            this.status = status;
            this.sql = new SqlStatement();
        }

        public Cursor(CursorStatus status, SqlStatement sql)
        {
            this.status = status;
            this.sql = sql;
        }
    }
}
