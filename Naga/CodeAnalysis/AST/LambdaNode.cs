

using System.Collections.Generic;

namespace Naga.CodeAnalysis.AST
{
	class LambdaNode : Node
	{
		readonly List<Node> _params;
		readonly Node _body;

		public LambdaNode(List<Node> params_, Node body)
			:base (NodeType.LambdaNodeType)
		{
			_params = params_;
			_body = body;
		}

		public override string Stringify(string indent)
		{
			var str = "";
			str += $"{indent}<LambdaNode>";
			str += "\n";
			if (_params.Count > 0)
			{
				str += $"{indent} <Params>";
				str += "\n";
				foreach (Node param in _params)
				{
					str += param.Stringify(indent+"  ");
					str += "\n";
				}
			}
			str += $"{indent} <Body>";
			str += "\n";
			str += _body.Stringify(indent+"  ");
			str += "\n";
			return str;
		}
	}
}