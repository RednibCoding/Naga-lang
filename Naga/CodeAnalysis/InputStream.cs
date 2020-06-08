using System;


namespace Naga.CodeAnalysis
{
	class InputStream
	{
		readonly string _input;
		int _line;
		int _pos;
		int _col;
		public int Line { get { return _line; } }
		public int Pos { get { return _pos; } }
		public int Col { get { return _col; } }
		public bool Eof { get { return Peek == '\0'; } }
		public char Peek { get { return _pos >= _input.Length ? '\0' : _input[_pos]; } }

		public InputStream(string input)
		{
			_input = input;
			_pos = 0;
			_line = 1;
			_col = 0;
		}

		public char Next()
		{
			if (_pos >= _input.Length) return '\0';
			var chr = _input[_pos++];
			if (chr == '\n') { _line++; _col = 0; }
			else _col++;
			return chr;
		}

		public void RevertTo(int line, int pos, int col)
		{
			_line = line;
			_pos = pos;
			_col = col;
		}

		public void Error(string msg)
		{
			Console.WriteLine($"ERROR on line {_line}({_col}): {msg}");
			Console.Write("Press any key to exit...");
			Console.ReadKey();
			Environment.Exit(0);
		}
	}
}