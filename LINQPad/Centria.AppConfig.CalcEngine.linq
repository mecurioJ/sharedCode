<Query Kind="Program">
  <NuGetReference>morelinq</NuGetReference>
  <Namespace>MoreLinq</Namespace>
  <Namespace>System.IO</Namespace>
  <Namespace>System.Text</Namespace>
</Query>

void Main()
{
	var Directory = @"B:\Projects\Plus\Centria\PDAPWPF\PDAPWPF\";
	var FileName = @"App.Config";
	//XName xCgf = ""
	
	var CalcLookup = XElement.Load(Directory+FileName)	.Element("Calculations").Element("CalculationKeys")	.Elements()	.Select(xe => new CalculationKey (xe))	.Select(p => p);
	
	CalcLookup.Where(cc => cc.CalculationName.Equals("ac"))	.Dump();
	
//	CalcLookup
//		.Select(c => 
//			new{
//				c.CalculationName,
//				ParameterTokens = c.CalculationTokens.Where(tk => tk.Kind == TokenKind.Word)
//					//.Where (v => !NotVals.Contains(v.Value))
//					.Select(v => v.Value).Distinct()
//				.Where(t => t.Length > 2)
//				}).Distinct()
//		//.Select(t => {t.ParameterTokens)
//		.ToLookup(t => t.CalculationName,t => t.ParameterTokens.Distinct())
////    ["yrev"] 
////    ["icrev"] 
////    ["sbbrev"] 
////    ["acrev"] 
////    ["sbtrev"] 
////    ["yflat"] 
////    ["icflat"] 
////    ["sbbflat"] 
////    ["sbtflat"] 
////    ["acflat"] 
////    ["ic"] 
////    ["sbb"] 
////    ["sbt"] 
////    ["panelweight"] 
////    ["fb"] 
////    ["gc"] 
////    ["fv"] 
////    ["endclip"] 
////    ["interiorclip"] 
////    ["panelname"] 
////    ["ybar"] 
//    ["ac"] 
////    ["thicknessplusrib"] 
////    ["subtitle"] 
////    ["reveal"] 
//.SelectMany(t => t.Select(tt => tt)).Distinct().Aggregate((p,n) => String.Format("{0}, object {1}",p,n))
//		.Dump()
//		;
//	
//	CalcLookup.Select(nm => 	String.Format(@"public object define_{0}(){1}",nm.CalculationName,"{return new object();}")).Distinct().Dump();
	
/*
	PanelModel
	PanelThickness
	LinerThickness
	Reveal
	HasRibToClipFasteners
	TokenKeys = {==, &&, >, <,|}
*/
	
}

// Define other methods and classes here

public class CalculationKey
{
	public CalculationKey() {}
	public CalculationKey(XElement xe) 
	{
		Identity = int.Parse(xe.Attribute("CalcId").Value);
		CalculationName = xe.Attribute("VariableName").Value;
		Calculation = xe.Attribute("Calculation").Value;
		Parameters = xe.Attribute("Parameters").Value.Replace("/",string.Empty)
		.Replace("\"",string.Empty)
		.Replace("|"," | PanelModel == ")
		.Replace("==","=")
		.Replace("&&","&");
		ParameterTokens = buildTokenizer(xe.Attribute("Parameters").Value.Replace("/",string.Empty)
		.Replace("\"",string.Empty)
		.Replace("|"," | PanelModel == ")
		.Replace("==","=")
		.Replace("&&","&"));
		CalculationTokens = buildTokenizer(xe.Attribute("Calculation").Value);
	}
	
	private IEnumerable<Token> buildTokenizer(string eval)
	{
		List<Token> Tokens = new List<Token>();
		Token token;
		Tokenizer tk = new Tokenizer(eval);
		do{
			token = tk.Next();
			//if(token.Kind == TokenKind.WhiteSpace) ;
			Tokens.Add(token);
		}
		while(token.Kind != TokenKind.EOF);
		
		return Tokens
			.Where(tt => tt.Kind != TokenKind.WhiteSpace)
			.Where(tt => tt.Kind != TokenKind.EOF);
	}
	
	
	private IEnumerable<Token> _parameterTokens;
	public IEnumerable<Token> ParameterTokens 
	{
		get
			{ 
				return _parameterTokens; 
			}
		private set 
			{
				_parameterTokens = buildTokenizer(Parameters);
			}
	}
	private IEnumerable<Token> _calcTokens;
	
	public IEnumerable<Token> CalculationTokens 
	{
		get
			{ 
				return _calcTokens; 
			}
		private set 
			{
				_calcTokens = buildTokenizer(Calculation);
			}
	}
	
	public int Identity {get;set;}
	public String CalculationName {get;set;}
	public String Calculation {get;set;}
	public String Parameters {get;set;}
}



/*--------------------------------------------------------------*/

public class Tokenizer
	{
		const char EOF = (char)0;

		int line;
		int column;
		int pos;	// position within data

		string data;

		bool ignoreWhiteSpace;
		char[] symbolChars;

		int saveLine;
		int saveCol;
		int savePos;

		public Tokenizer(TextReader reader)
		{
			if (reader == null)
				throw new ArgumentNullException("reader");

			data = reader.ReadToEnd();

			Reset();
		}

		public Tokenizer(string data)
		{
			if (data == null)
				throw new ArgumentNullException("data");

			this.data = data;

			Reset();
		}

		/// <summary>
		/// gets or sets which characters are part of TokenKind.Symbol
		/// </summary>
		public char[] SymbolChars
		{
			get { return this.symbolChars; }
			set { this.symbolChars = value; }
		}

		/// <summary>
		/// if set to true, white space characters will be ignored,
		/// but EOL and whitespace inside of string will still be tokenized
		/// </summary>
		public bool IgnoreWhiteSpace
		{
			get { return this.ignoreWhiteSpace; }
			set { this.ignoreWhiteSpace = value; }
		}

		private void Reset()
		{
			this.ignoreWhiteSpace = false;
			this.symbolChars = new char[]{'=', '+', '-', '/', ',', '.', '*', '~', '!', '@', '#', '$', '%', '^', '&', '(', ')', '{', '}', '[', ']', ':', ';', '<', '>', '?', '|', '\\'};

			line = 1;
			column = 1;
			pos = 0;
		}

		protected char LA(int count)
		{
			if (pos + count >= data.Length)
				return EOF;
			else
				return data[pos+count];
		}

		protected char Consume()
		{
			char ret = data[pos];
			pos++;
			column++;

			return ret;
		}

		protected Token CreateToken(TokenKind kind, string value)
		{
			return new Token(kind, value, line, column);
		}

		protected Token CreateToken(TokenKind kind)
		{
			string tokenData = data.Substring(savePos, pos-savePos);
			return new Token(kind, tokenData, saveLine, saveCol);
		}

		public Token Next()
		{
			ReadToken:

			char ch = LA(0);
			switch (ch)
			{
				case EOF:
					return CreateToken(TokenKind.EOF, string.Empty);

				case ' ':
				case '\t':
				{
					if (this.ignoreWhiteSpace)
					{
						Consume();
						goto ReadToken;
					}
					else
						return ReadWhitespace();
				}
				case '0':
				case '1':
				case '2':
				case '3':
				case '4':
				case '5':
				case '6':
				case '7':
				case '8':
				case '9':
					return ReadNumber();

				case '\r':
				{
					StartRead();
					Consume();
					if (LA(0) == '\n')
						Consume();	// on DOS/Windows we have \r\n for new line

					line++;
					column=1;

					return CreateToken(TokenKind.EOL);
				}
				case '\n':
				{
					StartRead();
					Consume();
					line++;
					column=1;
					
					return CreateToken(TokenKind.EOL);
				}

				case '"':
				{
					return ReadString();
				}

				default:
				{
					if (Char.IsLetter(ch) || ch == '_')
						return ReadWord();
					else if (IsSymbol(ch))
					{
						StartRead();
						Consume();
						return CreateToken(TokenKind.Symbol);
					}
					else
					{
						StartRead();
						Consume();
						return CreateToken(TokenKind.Unknown);						
					}
				}

			}
		}

		/// <summary>
		/// save read point positions so that CreateToken can use those
		/// </summary>
		private void StartRead()
		{
			saveLine = line;
			saveCol = column;
			savePos = pos;
		}

		/// <summary>
		/// reads all whitespace characters (does not include newline)
		/// </summary>
		/// <returns></returns>
		protected Token ReadWhitespace()
		{
			StartRead();

			Consume(); // consume the looked-ahead whitespace char

			while (true)
			{
				char ch = LA(0);
				if (ch == '\t' || ch == ' ')
					Consume();
				else
					break;
			}

			return CreateToken(TokenKind.WhiteSpace);
			
		}

		/// <summary>
		/// reads number. Number is: DIGIT+ ("." DIGIT*)?
		/// </summary>
		/// <returns></returns>
		protected Token ReadNumber()
		{
			StartRead();

			bool hadDot = false;

			Consume(); // read first digit

			while (true)
			{
				char ch = LA(0);
				if (Char.IsDigit(ch))
					Consume();
				else if (ch == '.' && !hadDot)
				{
					hadDot = true;
					Consume();
				}
				else
					break;
			}

			return CreateToken(TokenKind.Number);
		}

		/// <summary>
		/// reads word. Word contains any alpha character or _
		/// </summary>
		protected Token ReadWord()
		{
			StartRead();

			Consume(); // consume first character of the word

			while (true)
			{
				char ch = LA(0);
				if (Char.IsLetter(ch) || ch == '_')
					Consume();
				else
					break;
			}

			return CreateToken(TokenKind.Word);
		}

		/// <summary>
		/// reads all characters until next " is found.
		/// If "" (2 quotes) are found, then they are consumed as
		/// part of the string
		/// </summary>
		/// <returns></returns>
		protected Token ReadString()
		{
			StartRead();

			Consume(); // read "

			while (true)
			{
				char ch = LA(0);
				if (ch == EOF)
					break;
				else if (ch == '\r')	// handle CR in strings
				{
					Consume();
					if (LA(0) == '\n')	// for DOS & windows
						Consume();

					line++;
					column = 1;
				}
				else if (ch == '\n')	// new line in quoted string
				{
					Consume();

					line++;
					column = 1;
				}
				else if (ch == '"')
				{
					Consume();
					if (LA(0) != '"')
						break;	// done reading, and this quotes does not have escape character
					else
						Consume(); // consume second ", because first was just an escape
				}
				else
					Consume();
			}

			return CreateToken(TokenKind.QuotedString);
		}

		/// <summary>
		/// checks whether c is a symbol character.
		/// </summary>
		protected bool IsSymbol(char c)
		{
			for (int i=0; i<symbolChars.Length; i++)
				if (symbolChars[i] == c)
					return true;

			return false;
		}
	}

	public enum TokenKind
	{
		Unknown,
		Word,
		Number,
		QuotedString,
		WhiteSpace,
		Symbol,
		EOL,
		EOF
	}

	public class Token
	{
		int line;
		int column;
		string value;
		TokenKind kind;

		public Token(TokenKind kind, string value, int line, int column)
		{
			this.kind = kind;
			this.value = value;
			this.line = line;
			this.column = column;
		}

		public int Column
		{
			get { return this.column; }
		}

		public TokenKind Kind
		{
			get { return this.kind; }
		}

		public int Line
		{
			get { return this.line; }
		}

		public string Value
		{
			get { return this.value; }
		}
	}