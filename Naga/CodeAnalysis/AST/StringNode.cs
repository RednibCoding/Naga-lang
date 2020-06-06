

namespace Naga.CodeAnalysis.AST
{
    class StringNode : Node
    {
        string _value;
        public StringNode(string value)
         :base (NodeType.StringNodeType)
        {
            _value = value;
        }

        public override string Stringify(string indent)
		{
			return $"{indent}<StringNode> '{_value}'";
		}
    }
}