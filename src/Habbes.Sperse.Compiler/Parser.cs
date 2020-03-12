using System.Collections.Generic;
using System.Text;

namespace Habbes.Sperse.Compiler
{
    public class Parser
    {
        private ITokenStream tokens;

        public Parser(ITokenStream tokens)
        {
            this.tokens = tokens;
        }

        public ISyntaxTree Parse()
        {
            var tree = ParseExpression();
            tokens.ConsumeNextType(TokenType.Eof);
            return tree;
        }

        private ISyntaxTree ParseExpression()
        {
            var token = tokens.ConsumeNextType(TokenType.Identifier, TokenType.IntConstant);
            var tree = new SyntaxTree(token);
            if (token.Type == TokenType.IntConstant)
            {
                return tree;
            }
            
            // check if has children
            if (!tokens.IsNextType(TokenType.OpenParen))
            {
                return tree;
            }
            // has children
            foreach (var child in ParseChildren())
            {
                tree.AddChild(child);
            }

            return tree;
        }

        private IEnumerable<ISyntaxTree> ParseChildren()
        {
            tokens.ConsumeNextType(TokenType.OpenParen);
            while (!tokens.IsNextType(TokenType.CloseParen))
            {
                yield return ParseExpression();
            }

            tokens.ConsumeNextType(TokenType.CloseParen);
        }
    }
}

