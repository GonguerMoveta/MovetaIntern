using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace fecher.Common
{
    /// <summary>
    /// Comparer class for comparing two strings with numbers in it
    /// For example param10 > param2
    /// </summary>
    public class StringNumberComparer : Comparer<string>
    {
        public override int Compare(string x, string y)
        {
            int? nr1 = null, nr2 = null;
            Regex reg = new Regex(@"(\d+)");
            Match match = reg.Match(x);
            if (match.Success)
            {
                nr1 = Convert.ToInt32(match.Groups[1].Value);
            }
            match = reg.Match(y);
            if (match.Success)
            {
                nr2 = Convert.ToInt32(match.Groups[1].Value);
            }
            if (nr1 != null && nr2 != null)
            {
                return (nr1 - nr2).Value;
            }
            else
            {
                return Default.Compare(x, y);
            }
        }
    }
}
