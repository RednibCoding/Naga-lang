

namespace Naga.CodeAnalysis.AST
{
	abstract class Node
	{
		readonly NodeType _type;
		public NodeType Type { get { return _type; } } 

		public Node(NodeType type)
		{
			_type = type;
		}

		public virtual string Stringify(string indent)
		{
			return "Node";
		}
	}
}