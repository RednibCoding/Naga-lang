using System.Collections.Generic;

namespace Naga.CodeAnalysis.AST
{
	class FunCallNode : Node
	{
		SymbolNode _symbol;
		List<Node> _args;

		public FunCallNode(SymbolNode symbol, List<Node> args)
			:base (NodeType.FunCallNodeType)
		{
			_symbol = symbol;
			_args = args;
		}

		public override string Stringify(string indent)
		{
			var str = "";
			str += $"{indent}<FunCallNode>";
			str += "\n";
			str += _symbol.Stringify(indent+"  ");
			str += "\n";
			if (_args.Count > 0)
			{
				str += $"{indent} <Args>";
				str += "\n";
				foreach (Node arg in _args)
				{
					str += arg.Stringify(indent+"   ");
					str += "\n";
				}
			}
			return str;
		}
	}
}