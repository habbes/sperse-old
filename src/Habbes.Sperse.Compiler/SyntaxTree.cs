using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Habbes.Sperse.Compiler
{
    public class SyntaxTree : ISyntaxTree
    {
        private List<ISyntaxTree> children = new List<ISyntaxTree>();

        public SyntaxTree(Token token)
        {
            Token = token;
        }
        
        public SyntaxTree(Token token, params ISyntaxTree[] children): this(token)
        {
            foreach (var child in children)
            {
                AddChild(child);
            }
        }
        
        public IEnumerable<ISyntaxTree> Children => children;
        public Token Token { get; private set; }
        public void AddChild(ISyntaxTree node)
        {
            children.Add(node);
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append(Token.Value);
            if (children.Count > 0)
            {
                builder.Append('(');
                builder.Append(children.First());
                foreach (var child in children.Skip(1))
                {
                    builder.Append($", {child}");
                }

                builder.Append(')');
            }

            return builder.ToString();
        }
    }
}