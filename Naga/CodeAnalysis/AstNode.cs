using System.Collections.Generic;


namespace Naga.CodeAnalysis
{
	class AstNode
	{
		public string Type { get; }
		public string Value { get; private set; }
		List<AstNode> _children;

		public AstNode(string type, string value, params AstNode[] children)
		{
			_children = new List<AstNode>();
			Type = type;
			Value = value;
			if (children != null)
			{
				for (var i = 0; i < children.Length; i++)
				{
					_children.Add(children[i]);
				}
			}
		}

		public void AppendChild(AstNode child)
		{
			_children.Add(child);
		}

		public AstNode GetChild(int index)
		{
			return _children[index];
		}
	}
}