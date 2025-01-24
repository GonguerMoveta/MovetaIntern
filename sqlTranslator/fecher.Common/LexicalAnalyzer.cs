using System;
using fecher.Common;
using System.Collections;
using System.Text.RegularExpressions;

namespace fecher.SqlTranslator
{
	/// <summary>
	/// This class is used to delimitate the tokens in the string representing the SQL statement.
	/// It is used by the SyntacticalAnalyzer class.
	/// </summary>
	public class LexicalAnalyzer
	{
				
		/// <summary>
		/// Return true if the keyword is an end to a SELECT clause. Keywords are: FROM, INTO
		/// </summary>
		/// <param name="keyword"></param>
		/// <returns></returns>
		public bool IsKeywordBreakingSELECT(string keyword)
		{
			keyword = keyword.ToUpper();

			return (keyword == "FROM" || keyword == "INTO");
		}

        public static string[] months =  {"JAN", "FEB", "MAR", "APR", "MAY", "JUN",
                                           "JUL", "AUG", "SEP", "OCT", "NOV", "DEC"};

        private char[] germanCharset = { '�', '�', '�', '�', '�', '�', '�' };

		protected int CursorPosition = 0;

		protected char c = ' ';

		private string sourceSql; 
		
		public string SourceSql 
		{
			set 
			{
				sourceSql = value;
			}			
		}
				
		//Reads the next character from the input string
		protected char GetChar()
		{
			char [] c;
			if(CursorPosition < sourceSql.Length)
			{
				c = sourceSql.ToCharArray(CursorPosition,1);
				CursorPosition++;
				return c[0];
			}
			else 
				return '\0';
		}

		
		//Returns the next token
		public virtual IToken GetToken()
		{
            return new Token();
		}


		//Returns the next token but it doesn't consume it (the cursor will be reestablished to the beginning of the token
		public IToken Peek()
		{
			int tempCursorPosition = CursorPosition;
			IToken token = new Token();
			token = GetToken();
			
			//restore cursor position
			if (!(token.LexicalCode == Tokens.Null))
			{
				CursorPosition = tempCursorPosition - 1  ;
				c = GetChar();
			}
			
			return token;
		}

		public bool IsGermanChar(char c)
		{
			for(int i = 0; i < germanCharset.Length; i++)
			{
				if(c == germanCharset[i])
				{
					return true;
				}
			}
			return false;
		}

        public bool IsDigit(char c)
        {
            return (c >= '0') && (c <= '9');
        }

        public bool IsAlphabetic(char c)
        {
            return ((c >= 'a')&&(c <= 'z')) || ((c >= 'A')&&(c <= 'Z'));
        }
	}
}
