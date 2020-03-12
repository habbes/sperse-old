using System.Linq;
using Xunit;

namespace Habbes.Sperse.Compiler.Tests
{
    public class ParserTests
    {
        [Fact]
        public void ParseReturnsSyntaxTree()
        {
            var src = "add(x, 2)";
            var lexer = new Lexer(src);
            var parser = new Parser(lexer.GetTokenStream());
            
            var tree = parser.Parse();
            
            Assert.Equal(Token.Identifier("add"), tree.Token);
            Assert.Equal(Token.Identifier("x"), tree.Children.First().Token);
            Assert.Empty(tree.Children.First().Children);
            Assert.Equal(Token.IntConstant("2"), tree.Children.ElementAt(1).Token);
            Assert.Empty(tree.Children.ElementAt(1).Children);
        }
    }
}