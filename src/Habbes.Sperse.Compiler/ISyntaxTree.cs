using System.Collections.Generic;

namespace Habbes.Sperse.Compiler
{
    public interface ISyntaxTree
    {
        public IEnumerable<ISyntaxTree> Children { get; }
        public Token Token { get; }
    }
}