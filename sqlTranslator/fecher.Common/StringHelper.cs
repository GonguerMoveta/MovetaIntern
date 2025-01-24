using System;

namespace fecher.Common
{
    public static class StringHelper
    {
        //Extension method for removing [] from columns and tables
        public static string RemoveBrackets(this string value)
        {
            return value.Replace("[", "").Replace("]", "");
        }

        public static string GetUnqualifiedValue(this string value)
        {
            if (!String.IsNullOrEmpty(value))
            {
                if (value.Contains("."))
                {
                    value = value.Substring(value.LastIndexOf(".") + 1);
                }
            }

            return value;
        }
    }
}
