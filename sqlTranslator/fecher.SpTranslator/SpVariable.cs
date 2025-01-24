using System;
using System.Collections.Generic;
using System.Text;

namespace fecher.SpTranslator
{
    public class SpVariable
    {
        private string name;
        private string type;

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public SpVariable(string name, string type)
        {
            this.name = name;
            this.type = type;
        }
    }
}
