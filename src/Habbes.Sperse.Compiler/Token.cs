using System;
namespace Habbes.Sperse.Compiler
{
    public struct Token
    {
        public Token(string value, TokenType type)
        {
            Value = value;
            Type = type;
        }

        public string Value { get; set; }
        public TokenType Type { get; set; }

        public static Token Identifier(string value)
        {
            return new Token(value, TokenType.Identifier);
        }

        public static Token IntConstant(string value)
        {
            return new Token(value, TokenType.IntConstant);
        }

        public override string ToString()
        {
            return $"<{Value}>";
        }
    }
}
