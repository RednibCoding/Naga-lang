using System;
using System.Collections.Generic;

namespace Naga.CodeAnalysis
{
    class Lexer
	{
		Token _current;
		InputStream _stream;
		List<string> _keywords;
		List<char> _punc;
        List<char> _operators;

        public Lexer(string text)
		{
			_stream = new InputStream(text);
			_keywords = new List<string> {"if", "then", "else", "lambda", "true", "false"};
			_punc = new List<char> {',', ';', '(', ')','{', '}', '[', ']'};
			_operators = new List<char> {'+', '-', '*', '/', '%', '=', '&', '|', '<', '>', '!', '"'};
		}

		bool IsKeyword(string s)
		{
			return _keywords.Contains(s);
		}

		bool IsSymbolStart(char c)
		{
			return char.IsLetter(c) || c == '_';
		}

		bool IsSymbol(char c)
		{
			return char.IsDigit(c) || IsSymbolStart(c);
		}

		bool IsOperator(char c)
        {
			return _operators.Contains(c);
        }

		bool IsPunc(char c)
		{
			return _punc.Contains(c);
		}

		bool IsWhite(char c)
		{
			return char.IsWhiteSpace(c);
		}

		string ReadWhile(Func<char, bool> handlerFunc)
		{
			var str = "";
			while(!_stream.Eof && handlerFunc(_stream.Peek))
				str += _stream.Next();
			return str;
		}

		Token ReadNumber()
		{
			var hasDot = false;
			var number = ReadWhile((char c) => {
				if (c == '.')
				{
					if (hasDot) return false;
					hasDot = true;
					return true;
				}
				return char.IsDigit(c);
			});

			if (number != "")
			{
				return new NumberToken(number);
			}
			_stream.Error("Expecting number...");
			return null;
		}

		Token ReadSymbol()
		{
			var symbol = ReadWhile(IsSymbol);
			if (symbol != "")
			{
				if (IsKeyword(symbol)) return new KeywordToken(symbol);
				return new SymbolToken(symbol);
			}
			_stream.Error("Expecting symbol...");
			return null;
		}

		string ReadEscaped(char end)
		{
			var escaped = false;
			var str = "";
			_stream.Next();
			while (!_stream.Eof)
			{
				var c = _stream.Next();
				if (escaped)
				{
					str += c;
					escaped = false;
				}
				else if (c == '\\') escaped = true;
				else if (c == end) break;
				else str += c;
			}
			return str;
		}

		Token ReadString()
		{
			return new StringToken(ReadEscaped('"'));
		}

		void SkipComment()
		{
			ReadWhile((char c) => {
				return c != '\n';
			});
		}

		Token ReadNext()
		{
			ReadWhile(IsWhite);
			if (_stream.Eof) return null;
			var c = _stream.Peek;
			if (c == '#')
			{
				SkipComment();
				return ReadNext();
			}
			if (c == '"') return ReadString();
			if (char.IsDigit(c)) return ReadNumber();
			if (IsSymbolStart(c)) return ReadSymbol();
			if (IsPunc(c)) return new PuncToken(_stream.Next().ToString());
			if (IsOperator(c)) return new OperatorToken(ReadWhile(IsOperator));
			_stream.Error($"Invalid character: '{c}'");
			return null;
		}

		public Token Peek
		{
			get
			{
				if (_current != null)
					return _current;
				else
					_current = ReadNext(); return _current;
			}
		}

		public Token Next()
		{
			var token = _current;
			_current = null;

			if (token != null) return token;
			return  ReadNext();
		}

		public bool Eof
		{
			get {return Peek == null;}
		}

		public void Error(string msg)
		{
			_stream.Error(msg);
		}
    }
}