

namespace Naga.CodeAnalysis.AST
{
	class SymbolNode : Node
	{
		string _value;
		public SymbolNode(string value)
			:base (NodeType.SymbolNodeType)
		{
			_value = value;
		}

		public override string Stringify(string indent)
		{
			return $"{indent}<SymbolNode> '{_value}'";
		}
	}
}