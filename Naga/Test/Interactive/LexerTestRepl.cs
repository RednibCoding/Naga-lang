using System;
using Naga.CodeAnalysis;


namespace Naga.Test.Interactive
{
	// Quick and dirty REPL implementation for testing the lexer
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
				var token = lexer.Next();
				while (token.Type != "eof")
				{
					Console.WriteLine("<"+token.Type + "> : "+token.Value);
					token = lexer.Next();
				}				
				Console.WriteLine();
			}
		}
	}
}