using System;
using System.Collections.Generic;
using System.Linq;

namespace Habbes.Sperse.Compiler
{
    public class TokenStream: ITokenStream, IDisposable
    {
        private IEnumerator<Token> iterator;
        private bool hasPeeked;
        private Token peeked;
        
        public TokenStream(IEnumerator<Token> tokensIterator)
        {
            iterator = tokensIterator;
        }
        
        public bool Eof { get; private set; }

        public void Dispose()
        {
            iterator.Dispose();
        }

        public Token ConsumeNext()
        {
            Token token;
            if (hasPeeked)
            {
                hasPeeked = false;
                token = peeked;
            }
            else
            {
                iterator.MoveNext();
                token = iterator.Current;
            }

            Eof = token.Type == TokenType.Eof;
            return token;
        }

        public Token PeekNext()
        {
            if (hasPeeked)
            {
                // since no token has been consumed since the last peek,
                // peeking should return the previously peeked token
                return peeked;
            }
            iterator.MoveNext();
            peeked = iterator.Current;
            hasPeeked = true;
            return peeked;
        }

        public bool IsNextType(params TokenType[] expectedTypes)
        {
            var token = PeekNext();
            return expectedTypes.Contains(token.Type);
        }

        public Token ConsumeNextType(params TokenType[] expectedTypes)
        {
            var token = ConsumeNext();
            if (expectedTypes.Contains(token.Type))
            {
                return token;
            }
            throw new Exception($"Unexpected token of type '{token.Type.ToString()}'");
        }
    }
}