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

					var example = "square = :(x){ x * x;};\n";
					example += "num1 = 3;\n";
					example += "num2 = square( num1 );\n";
					example += "\n";
					example += "if( equals( num1, num2 ),\n";
					example += "{\n";
					example += "print( );\n";
					example += "},\n";
					example += "{\n";
					example += "print( );\n";
					example += "}\n";
					example += ");\n";

					var parser = new Parser(new Lexer(example), ";");
					var ast = parser.Parse();
					if (ast == null) break;
					foreach (AstNode node in ast)
						Console.WriteLine(node.Stringify());
				}
			}
		}
	}
}