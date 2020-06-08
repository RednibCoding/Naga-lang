using System;
using Naga.CodeAnalysis;


namespace Naga.Test.Interactive
{
	// Quick and dirty REPL implementation for testing the parser
	class ParserTestRepl
	{
		public static void Run()
		{
			while(true)
			{
				while (true)
				{
					Console.Write("> ");
					var line = Console.ReadLine();
					if (string.IsNullOrWhiteSpace(line))
						return;

					var parser = new Parser(new Lexer(line), ";");
					var ast = parser.Parse();
					if (ast == null) break;
					Console.WriteLine(ast);
				}
			}
		}
	}
}