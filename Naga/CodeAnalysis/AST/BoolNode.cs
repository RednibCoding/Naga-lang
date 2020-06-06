

namespace Naga.CodeAnalysis.AST
{
	class BoolNode : Node
	{
		bool _value;
		public BoolNode(bool value)
			:base (NodeType.BoolNodeType)
		{
			_value = value;
		}

		public override string Stringify(string indent)
        {
            return $"{indent}<BoolNode> '{_value.ToString()}'";
        }
	}
}