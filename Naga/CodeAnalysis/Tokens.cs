
namespace Naga.CodeAnalysis
{
	class PuncToken : Token
	{
		public PuncToken(string value)
			:base(TokenType.PuncTokenType)
		{
			_value = value;
		}
	}

	class NumberToken : Token
	{
		public NumberToken(string value)
			:base(TokenType.NumberTokenType)
		{
			_value = value;
		}
	}

	class StringToken : Token
	{
		public StringToken(string value)
			:base(TokenType.StringTokenType)
		{
			_value = value;
		}
	}

	class KeywordToken : Token
	{
		public KeywordToken(string value)
			:base(TokenType.KeywordTokenType)
		{
			_value = value;
		}
	}

	class SymbolToken : Token
	{
		public SymbolToken(string value)
			:base(TokenType.SymbolTokenType)
		{
			_value = value;
		}
	}

	class OperatorToken : Token
	{
		public OperatorToken(string value)
			:base(TokenType.OperatorTokenType)
		{
			_value = value;
		}
	}
}