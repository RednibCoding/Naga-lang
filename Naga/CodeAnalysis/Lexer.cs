using System;


namespace Naga.CodeAnalysis
{
	class Lexer
	{
		InputStream _stream;

		public Lexer(string text)
		{
			_stream = new InputStream(text);
		}

		public (string Type, string Value) Next()
		{

			var c = _stream.Peek;
			while(c == ' ' || c == '\n' || c == '#')
			{
				if (c == '#')
				{
					ScanEnclosed('#');
					c = _stream.Peek;
				}
				else
				{
					_stream.Next();
					c = _stream.Peek;
				}
			}
			if (c == '\0') return ("eof","\0");
			if ("(){},;=:".Contains(c)) {_stream.Next(); return (c.ToString(),"");}
			else if ("+-*/".Contains(c)) {_stream.Next(); return("operation", c.ToString());}
			else if ("\"'".Contains(c)) return("string", ScanEnclosed(c));
			else if (Char.IsDigit(c)) return("number", Scan(".0123456789"));
			else if (Char.IsLetter(c)) return("symbol", Scan("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ"));
			else Error($"Unexpected character: '{c}'");
			return ("","");
		}

		public (string Type, string Value) Peek()
		{
			var line = _stream.Line;
			var pos = _stream.Pos;
			var col = _stream.Col;
			var token =  Next();
			_stream.RevertTo(line, pos, col);
			return token;
		}

		// Scan a sequence of characters where each character must
		// be part of allowed characters.
		// This is usefull e.g. for scanning specifically for digits or letters etc.
		string Scan(string allowed)
		{
			var ret = "";
			var c = _stream.Peek;
			while (!_stream.Eof && allowed.Contains(c))
			{
				ret += c;
				_stream.Next();
				c = _stream.Peek;
			}
			return ret;
		}

		// Can be used to scan a text that is enclosed in two delimiters
		// e.g. a string (") or a comment (#) -> "this is a string" or # This is a comment #
		string ScanEnclosed(char delim)
		{
			Match(delim);
			var ret = "";
			while (_stream.Peek != delim && !_stream.Eof)
			{
				ret += _stream.Next();
			}
			Match(delim);
			return ret;
		}

		// Advance the read position by making sure the next character is the expected character
		void Match(char ch)
		{
			var next = _stream.Next();
			if (ch != next) Error($"Expecting '{ch}' got: '{next}'");
		}

		public void Error(string msg)
		{
			_stream.Error(msg);
		}
	}
}