using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Habbes.Sperse.Compiler.Tests
{
    public class LexerTests
    {

        [Fact]
        public void GetNextTokenReturnsNextTokenFromSource()
        {
            string src = "add(one, two)";
            var lexer = new Lexer(src);

            var result = lexer.TryGetNextToken(out var token);

            var expectedToken = new Token("add", TokenType.Identifier);
            Assert.Equal(expectedToken, token);
            Assert.Equal(3, lexer.CurrentIndex);
            Assert.True(result);

            result = lexer.TryGetNextToken(out token);
            expectedToken = new Token("(", TokenType.OpenParen);
            Assert.Equal(expectedToken, token);
            Assert.Equal(4, lexer.CurrentIndex);
            Assert.True(result);
        }

        [Fact]
        public void GetNextTokenReturnsFalseInInvalidChar()
        {
            string src = ";(one, two)";
            var lexer = new Lexer(src);

            var result = lexer.TryGetNextToken(out var token);

            Assert.Equal(0, lexer.CurrentIndex);
            Assert.False(result);
        }

        [Fact]
        public void GetNextTokenReturnsEofIfEndOfString()
        {
            string src = "";
            var lexer = new Lexer(src);

            var result = lexer.TryGetNextToken(out var token);

            Assert.Equal(0, lexer.CurrentIndex);
            Assert.Equal(TokenType.Eof, token.Type);
            Assert.True(result);
        }

        [Fact]
        public void GetTokensReturnsTokensFromSource()
        {
            string src = "add(one, 1)";
            var lexer = new Lexer(src);
            var tokens = lexer.GetTokens();

            IEnumerable<Token> expected = new List<Token>()
            {
                new Token("add", TokenType.Identifier),
                new Token("(", TokenType.OpenParen),
                new Token("one", TokenType.Identifier),
                new Token("1", TokenType.IntConstant),
                new Token(")", TokenType.CloseParen),
                new Token(null, TokenType.Eof)
            };
            Assert.True(tokens.SequenceEqual<Token>(expected));
        }

        [Fact]
        public void GetTokensThrowsExceptionOnInvalidToken()
        {
            string src = "add(one<, two)";
            var lexer = new Lexer(src);
            try
            {
                lexer.GetTokens().ToList();
                throw new Exception("should fail");
            }
            catch (Exception e)
            {
                Assert.Equal("Invalid syntax at '<, two)'", e.Message);
            }
        }
    }
}
