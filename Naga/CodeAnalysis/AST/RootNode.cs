
using System.Collections.Generic;

namespace Naga.CodeAnalysis.AST
{
	class RootNode : Node
	{
		List<Node> _nodes;

		public RootNode()
			:base (NodeType.RootNodeType)
		{
			_nodes = new List<Node>();
		}

		public RootNode(List<Node> nodes)
			:base (NodeType.RootNodeType)
		{
			_nodes = nodes;
		}

		public void AppendNode(Node node)
		{
			_nodes.Add(node);
		}

		public override string Stringify(string indent)
		{
			indent += " ";
			var str = "<Root>\n";
			foreach (Node node in _nodes)
			{
				str += node.Stringify(indent);
				str += "\n";
			}
			return str;
		}
	}
}