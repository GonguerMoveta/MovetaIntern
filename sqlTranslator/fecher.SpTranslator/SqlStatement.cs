using System;
using System.Collections.Generic;
using System.Text;

namespace fecher.SpTranslator
{
    public class SqlStatement
    {
        private bool isQuery;
        private string value;
        private string into;

        public string Into
        {
            get { return into; }
            set { into = value; }
        }

        public string Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        public bool IsQuery
        {
            get { return isQuery; }
            set { isQuery = value; }
        }

        public SqlStatement()
        {
            isQuery = false;
            value = String.Empty;
            into = String.Empty;
        }

        public SqlStatement(bool isQuery, string value, string into)
        {
            this.isQuery = isQuery;
            this.value = value;
            this.into = into;
        }
    }
}
