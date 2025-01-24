using System;
using System.Collections;
using fecher.Common;
using System.Text;
using System.Text.RegularExpressions;

namespace fecher.SpTranslator
{
	/// <summary>
	/// Base class for the stored procedures translation
	/// </summary>
	public class SpTranslator
	{
        private string source;

        public string Source
        {
            get { return source; }
            set { source = value; }
        }
        
        public string Translate(string sourceSp)
        {
            source = sourceSp;
            
            SyntacticAnalyzer parser = new SyntacticAnalyzer();

            Tree parseTree = parser.Parse(DatabaseBrand.SqlBase, sourceSp);

            return Format(ProcessTree(parseTree));
        }

        public static SpStatistics Analyze(string sourceSp)
        {
            SyntacticAnalyzer parser = new SyntacticAnalyzer();
            Tree parseTree = parser.Parse(DatabaseBrand.SqlBase, sourceSp);

            SpStatistics stats = new SpStatistics();
            stats.Statements = parseTree.FindAllNested(parseTree.root, Tokens.Statement);
            stats.Comments = parseTree.FindAll(parseTree.root, Tokens.Comment);
            stats.OnProcedureFetch = parseTree.FindAll(parseTree.root, "FETCH");
            stats.WhenSqlError = parseTree.FindAll(parseTree.root, "WHEN SQLERROR");

            return stats;
        }


        protected virtual string ProcessTree(Tree parseTree)
        {
            throw new NotImplementedException("Use one of the derived classes");
        }

        private string Format(string code)
        {
            string indent = "";
            string result = code;
            //Regex reg = new Regex(@"(\bBEGIN\b|\bEND\b)([\s|\S]*?)(?=(\bBEGIN\b|\bEND\b))");
            Regex reg = new Regex(@"(\bBEGIN\r\n|\bEND\r\n)([\s|\S]+?)(?=(\bBEGIN\r\n|\bEND\r\n))");
            Match match = reg.Match(code);
            while (match.Success)
            {
                if (match.Groups[1].Value == "END\r\n" && indent.Contains("\t"))
                {
                    indent = indent.Remove(indent.LastIndexOf("\t"));
                }
                else
                {
                    indent += "\t";
                }

                string subCode = match.Groups[2].Value;
                if (subCode != "\r\n")
                {
                    subCode = Regex.Replace(subCode, "^", indent, RegexOptions.Multiline);
                    if (match.Groups[3].Value == "END\r\n" && subCode.Contains("\t"))
                    {
                        subCode = subCode.Remove(subCode.LastIndexOf("\t"));
                    }
                    //result = result.Replace(match.Groups[2].Value, subCode);
                    if (result.IndexOf(match.Groups[2].Value) != result.LastIndexOf(match.Groups[2].Value))
                    {
                        string allText = "";
                        int index = result.LastIndexOf("\t" + match.Groups[2].Value);

                        if (index != -1)
                        {
                            index += ("\t" + match.Groups[2].Value).Length;
                            allText = result.Substring(0, index);
                        }

                        string textToSearch = result.Substring(index + 1);
                        int index2 = textToSearch.IndexOf(match.Groups[2].Value) + match.Groups[2].Value.Length;

                        allText += textToSearch.Substring(0, index2).Replace(match.Groups[2].Value, subCode) + result.Substring(index + 1 + index2);

                        result = allText;
                    }
                    else
                    {
                        result = result.Replace(match.Groups[2].Value, subCode);
                    }
                }
                match = match.NextMatch();
            }

            return result;
        }

        /// <summary>
        /// Returns a value indicating whether the source string contains the specified substring;
        /// The substring has to be delimited by white spaces
        /// </summary>
        /// <param name="source"></param>
        /// <param name="word"></param>
        /// <returns></returns>
        protected static bool ContainsWord(string source, string word)
        {
            Regex reg = new Regex("\\b" + word + "\\b", RegexOptions.IgnoreCase);
            return reg.IsMatch(source);
        }
	}
}
