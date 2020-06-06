using System;
using Naga.CodeAnalysis;

namespace Naga.Test.Interactive
{
	// Quick and dirty REPL implementation for testing
	class LexerTestRepl
	{
		public static void Run()
		{
			while(true)
			{
				Console.Write("> ");
				var line = Console.ReadLine();
				if (string.IsNullOrWhiteSpace(line))
					return;

				var lexer = new Lexer(line);
				while(true)
				{
					var token = lexer.Next();
					if (token == null)
						break;

					Console.WriteLine(token.ToString());
				}
				Console.WriteLine();
			}
		}
	}
}
