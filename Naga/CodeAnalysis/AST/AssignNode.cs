
namespace Naga.CodeAnalysis.AST
{
	class AssignNode : Node
	{
		string _operator;
		SymbolNode _left;
		Node _right;

		public AssignNode(SymbolNode left, Node right)
			:base (NodeType.AssignNodeType)
		{
			_operator = "=";
			_left = left;
			_right = right;
		}

		public override string Stringify(string indent)
		{
			var str = "";
			str += $"{indent}<AssignNode> '{_operator}'";
			str += "\n";
			str += _left.Stringify(indent+" ");
			str += "\n";
			str += _right.Stringify(indent+" ");
			return str;
		}
	}
}