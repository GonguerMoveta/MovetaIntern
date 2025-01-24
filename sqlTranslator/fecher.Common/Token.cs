using System;

namespace fecher.Common
{
	public interface IToken
	{
		Tokens LexicalCode
		{
			get; set;
		}

		string LiteralValue 
		{
			get;		
		}

        int IndentLevel
        {
            get;
        }

		void SetToken(Tokens code);

		void SetToken(Tokens code, string val);

        void SetToken(Tokens code, string val, int indentLevel);
	}

	/// <summary>
	/// This class is used to retain information about the tokens (keywords, identifiers etc.)
	/// that can be found in a SQL statement.
	/// </summary>
	public class Token : IToken
	{
		private Tokens lexicalCode;
		private string literalValue;
        private int indentLevel;

		public Tokens LexicalCode 
		{
			get { return lexicalCode; }	
			set {lexicalCode = value; }
		}

		public string LiteralValue 
		{
			get { return literalValue; }			
		}

        public int IndentLevel
        {
            get { return indentLevel; }
        }

		public void SetToken(Tokens code)
		{
			this.lexicalCode = code;
			this.literalValue = "";
            this.indentLevel = 0;
		}

		public void SetToken(Tokens code, string val)
		{
			this.lexicalCode = code;
			this.literalValue = val;
            this.indentLevel = 0;
		}

        public void SetToken(Tokens code, string val, int indentLevel)
        {
            this.lexicalCode = code;
            this.literalValue = val;
            this.indentLevel = indentLevel;
        }
	}

	public class TokenAllowedRightAfterSELECT : Token
	{
		public TokenAllowedRightAfterSELECT()
		{
		}
	}
}
