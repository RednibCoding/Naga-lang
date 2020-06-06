

namespace Naga.CodeAnalysis.AST
{
	class NumberNode : Node
	{
		double _value;
		public NumberNode(double value)
			:base (NodeType.NumberNodeType)
		{
			_value = value;
		}

		public override string Stringify(string indent)
		{
			return $"{indent}<NumberNode> '{_value.ToString()}'";
		}
	}
}