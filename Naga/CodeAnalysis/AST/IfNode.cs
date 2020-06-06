

using System.Collections.Generic;

namespace Naga.CodeAnalysis.AST
{
	class IfNode : Node
	{
		Node _cond;
		Node _then;
		Node _else;

		public IfNode(Node cond, Node then, Node else_ = null)
			:base (NodeType.IfNodeType)
		{
			_cond = cond;
			_then = then;
			_else = else_;
		}

		public override string Stringify(string indent)
		{
			var str = "";
			str += $"{indent}<IfNode>";
			str += "\n";
			str += $"{indent} <Condition>";
			str += "\n";
			str += _cond.Stringify(indent+"  ");
			str += "\n";
			str += $"{indent} <Then-Block>";
			str += "\n";
			str += _then.Stringify(indent+"  ");

			if (_else != null)
			{
				str += "\n";
				str += $"{indent} <Else-Block>";
				str += "\n";
				str += _else.Stringify(indent+"  ");
			}
			str += "\n";
			return str;
		}
	}
}