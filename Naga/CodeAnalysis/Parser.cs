
using System;
using System.Collections.Generic;
using Naga.CodeAnalysis.AST;

namespace Naga.CodeAnalysis
{
	class Parser
	{
		readonly Dictionary<string, byte> _precedence;
		Lexer _lexer;

		public Parser(string text)
		{
			_lexer = new Lexer(text);
			_precedence = new Dictionary<string, byte>
			{
				{"=", 1},
				{"||", 2},
				{"&&", 3},
				{"<", 7}, {">", 7}, {"<=", 7}, {">=", 7}, {"==", 7}, {"!=", 7},
				{"+", 10}, {"-", 10},
				{"*", 20}, {"/", 20}, {"%", 20}
			};
		}

		public RootNode Parse()
		{
			return ParseToplevel();
		}

		RootNode ParseToplevel()
		{
			var ast = new RootNode();
			while (!_lexer.Eof)
			{
				ast.AppendNode(ParseExpression());
				if (!_lexer.Eof) MatchPunc(";");
			}
			return ast;
		}

		Node ParseExpression()
		{
			return MaybeFunCall( () => {
				return MaybeBinary(ParseAtom(), 0);
			});
		}

		Node ParseAtom()
		{
			return MaybeFunCall( () => {
				if (IsPuncPeek("("))
				{
					_lexer.Next();
					var expr = ParseExpression();
					MatchPunc(")");
					return expr;
				}
				if (IsPuncPeek("{")) return ParseProg();
				if (IsKeywordPeek("if")) return ParseIf();
				if (IsKeywordPeek("true") || IsKeywordPeek("false")) return ParseBool();
				if (IsKeywordPeek("lambda")) {_lexer.Next(); return ParseLambda();}
				var token = _lexer.Next();
				if (token.Type == TokenType.SymbolTokenType) return new SymbolNode(token.Value);
				if (token.Type == TokenType.StringTokenType) return new StringNode(token.Value);
				if (token.Type == TokenType.NumberTokenType)
				{
					var success = double.TryParse(token.Value, out var value);
					if (!success) _lexer.Error($"Expecting number got '{token.Value}'");
					return new NumberNode(value);
				}
				unexpected();
				return null;
			});
		}

		Node MaybeFunCall(Func<Node> expr)
		{
			var exp = expr();
			return IsPuncPeek("(") ? ParseFuncCall(exp) : exp;
		}

		Node MaybeBinary(Node left, int thisPrec)
		{
			var token = _lexer.Peek;
			if (token == null || token.Type != TokenType.OperatorTokenType)
				return left;
			
			var hisPrec = _precedence[token.Value];
			if (hisPrec > thisPrec)
			{
				_lexer.Next();
				var right = MaybeBinary(ParseAtom(), hisPrec);
				if (token.Value == "=")
				{
					if (left.Type == NodeType.SymbolNodeType)
						return new AssignNode(left as SymbolNode, right);
					else
						_lexer.Error("Left operand of assignment must be an identifier");
				}
				else
				{
					return new BinaryNode(token.Value, left, right);
				}
			}
			return left;
		}

		Node ParseFuncCall(Node func)
		{
			if (func.Type == NodeType.SymbolNodeType)
				return new FunCallNode(func as SymbolNode, ParseArgs("(", ")", ",", ParseExpression));

			_lexer.Error("Expected function identifier");
			return null;
		}

		Node ParseSymbol()
		{
			var sym = _lexer.Next();
			if (sym.Type != TokenType.SymbolTokenType)
				_lexer.Error("Expecting identifier");
			return new SymbolNode(sym.Value);
		}

		Node ParseIf()
		{
			MatchKeyword("if");
			var cond = ParseExpression();
			if (!IsPuncPeek("{")) MatchKeyword("then");
			var then = ParseExpression();
			Node else_ = null;
			if (IsKeywordPeek("else"))
			{
				_lexer.Next();
				else_ = ParseExpression();
			}
			return new IfNode(cond, then, else_);
		}

		Node ParseLambda()
		{
			return new LambdaNode(ParseArgs("(", ")", ",", ParseSymbol), ParseExpression());
		}

		Node ParseBool()
		{
			return new BoolNode(_lexer.Next().Value == "true");
		}

		List<Node> ParseArgs(string startPunc, string stopPunc, string sepPunc, Func<Node> parserFunc)
		{
			List<Node> nodes = new List<Node>();
			var first = true;
			MatchPunc(startPunc);
			while (!_lexer.Eof)
			{
				if (IsPuncPeek(stopPunc)) break;
				if (first) first = false; else MatchPunc(sepPunc);
				if (IsPuncPeek(stopPunc)) break;
				nodes.Add(parserFunc());
			}
			MatchPunc(stopPunc);
			return nodes;
		}

		Node ParseProg()
		{
			var nodes = ParseArgs("{", "}", ";", ParseExpression);
			if (nodes.Count == 0) return new BoolNode(false);
			if (nodes.Count == 1) return nodes[0];

			return new RootNode(nodes);
		}

		bool IsPuncPeek(string ch)
		{
			var token = _lexer.Peek;
			if (token == null) return false;
			if (token.Type != TokenType.PuncTokenType) return false;
			if (ch == "") return false;
			if (token.Value == ch) return true;
			return false;
		}

		bool IsKeywordPeek(string kw)
		{
			var token = _lexer.Peek;
			if (token == null) return false;
			if (token.Type != TokenType.KeywordTokenType) return false;
			if (kw == "") return false;
			if (token.Value == kw) return true;
			return false;
		}

		bool IsOperatorPeek(string op)
		{
			var token = _lexer.Peek;
			if (token == null) return false;
			if (token.Type != TokenType.OperatorTokenType) return false;
			if (op == "") return false;
			if (token.Value == op) return true;
			return false;
		}

		void MatchPunc(string ch)
		{
			if (IsPuncPeek(ch)) _lexer.Next();
			else _lexer.Error($"Expecting punctuation '{ch}'");
		}

		void MatchKeyword(string kw)
		{
			if (IsKeywordPeek(kw)) _lexer.Next();
			else _lexer.Error($"Expecting keyword '{kw}'");
		}

		void MatchOperator(string op)
		{
			if (IsOperatorPeek(op)) _lexer.Next();
			else _lexer.Error($"Expecting operator '{op}'");
		}

		void unexpected()
		{
			_lexer.Error($"Unexpected token '{_lexer.Peek.ToString()}'");
		}
	}
}