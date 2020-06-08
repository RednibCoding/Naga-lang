using System.Collections.Generic;


namespace Naga.CodeAnalysis
{
	class Parser
	{
		Lexer _lexer;
		string _stopAt;

		public Parser(Lexer lexer, string stopAt)
		{
			_lexer = lexer;
			_stopAt = stopAt;
		}

		public List<AstNode> Parse()
		{
			var nodes = new List<AstNode>();
			while(true)
			{
				var node = ParseExpression(null);
				if (node != null) nodes.Add(node);
				_lexer.Next();
				var next = _lexer.Peek();
				if (next.Type == "eof") break;
			}
			return nodes;
		}

		AstNode ParseExpression(AstNode prev)
		{
			var token = _lexer.Peek();
			if (_stopAt.Contains(token.Type))
			{
				return prev;
			}
			if (token.Type == "eof") _lexer.Error("Unexpected end of file");
			_lexer.Next();

			// Variable types
			if ("number string symbol".Contains(token.Type) && prev == null)
			{	// After number, string, symbol cannot follow a function declaration
				// without assign symbol
				if (":{".Contains(_lexer.Peek().Type))
					_lexer.Error($"Invalid syntax: Function declaration after {token.Type} '{token.Value}'");
				return ParseExpression(new AstNode(token.Type, token.Value, null));
			}
			// Binary operation (+-*/)
			else if (token.Type == "operation")
			{
				if (":{".Contains(_lexer.Peek().Type) || prev.Type == "function_decl")
					_lexer.Error("Anonymous functions are not allowed in operation");
				var next = ParseExpression(null);
				return ParseExpression(new AstNode(token.Type, token.Value, prev, next));
			}
			// Function call
			else if (token.Type == "(")
			{
				var args = ParseExpressions(",", ")");
				var argsNode = new AstNode("function_args", args.Count.ToString(), args.ToArray());
				return new AstNode("function_call", "", prev, argsNode);
			}
			// Function declaration
			else if ("{:".Contains(token.Type))
			{
				List<AstNode> params_ = new List<AstNode>();
				if (token.Type == ":")
				{
					params_ = ParseParams();
					token = _lexer.Peek();
					_lexer.Next();
				}

				if (token.Type != "{") _lexer.Error("Expecting function declaration '"+"{'");
				var body = ParseExpressions(";", "}");
				var paramsNode = new AstNode("function_params", params_.Count.ToString(), params_.ToArray());
				var	bodyNode = new AstNode("function_body", body.Count.ToString(), body.ToArray());

				return ParseExpression(new AstNode("function_decl", "", paramsNode, bodyNode));
			}
			// Assignment
			else if (token.Type == "=")
			{
				if (prev.Type != "symbol") _lexer.Error("Left operand of assignment must be a symbol");
				var next = ParseExpression(null);
				return ParseExpression(new AstNode("assignment", token.Type, prev, next));
			}
			else _lexer.Error($"Unexpected token: <{token.Type}> '{token.Value}'");
			return null;
		}

		// Parse parameters in a function declaration
		List<AstNode> ParseParams()
		{
			var token = _lexer.Peek();
			if (token.Type != "(") _lexer.Error("':' must be followed by '(' in a function declaration");
			_lexer.Next();
			var params_ = ParseExpressions(",", ")");
			foreach (AstNode node in params_)
			{
				if (node.Type != "symbol") _lexer.Error("Only symbols are allowed as function parameter");
			}
			return params_;
		}

		// Parse multiple expressions seperated by "sep" (e.g. "," or ";") until end (e.g. ")" or "}") has been reached
		List<AstNode> ParseExpressions(string sep, string end)
		{
			List<AstNode> nodes = new List<AstNode>();
			var token = _lexer.Peek();
			if (token.Type == end)
				_lexer.Next();
			else
			{
				var argsParser = new Parser(_lexer, $"{sep} {end}");
				while (token.Type != end)
				{
					var expr = argsParser.ParseExpression(null);
					if (expr != null) nodes.Add(expr);
					token = _lexer.Peek();
					_lexer.Next();
					if (token.Type == "eof") _lexer.Error("Unexpected end of file");
				}
			}
			return nodes;
		}
	}
}