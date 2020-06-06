namespace Naga.CodeAnalysis.AST
{
	class BinaryNode : Node
	{
		string _operator;
		Node _left;
		Node _right;

		public BinaryNode(string operator_, Node left, Node right)
			:base (NodeType.BinaryNodeType)
		{
			_operator = operator_;
			_left = left;
			_right = right;
		}

		public override string Stringify(string indent)
		{
			var str = "";
			str += $"{indent}<BinaryNode> '{_operator}'";
			str += "\n";
			str += _left.Stringify(indent+" ");
			str += "\n";
			str += _right.Stringify(indent+" ");
			return str;
		}
	}
}