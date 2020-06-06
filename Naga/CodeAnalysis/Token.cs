
namespace Naga.CodeAnalysis
{
	class Token
	{
		TokenType _type;
		public TokenType Type { get { return _type; } }

		// Values getting converted by the parser
		// for now, everything is stored as a string
		protected string _value;
		public string Value { get { return _value; } }

		public Token(TokenType type)
		{
			_type = type;
		}

		public override string ToString()
		{
			var str = "";
			switch(_type)
			{
				case TokenType.PuncTokenType:
					str = "Type: <punc>    ";
					break;
				case TokenType.NumberTokenType:
					str = "Type: <number>  ";
					break;
				case TokenType.StringTokenType:
					str = "Type: <string>  ";
					break;
				case TokenType.KeywordTokenType:
					str = "Type: <keyword> ";
					break;
				case TokenType.SymbolTokenType:
					str = "Type: <symbol>  ";
					break;
				case TokenType.OperatorTokenType:
					str = "Type: <operator>";
					break;
				default:
					str = "Type: <unknown> ";
					break;
			}

			str += $"\tValue: '{Value}'";
			return str;
		}
	}
}