using System;
using fecher.Common;
using System.Text;
using System.Collections.Generic;

namespace fecher.SqlTranslator
{
    /// <summary>
    /// Class for constructing the syntax tree
    /// </summary>
    public interface ISyntacticAnalyzer
    {
       Tree Parse(string sourceSql);
    }
}
