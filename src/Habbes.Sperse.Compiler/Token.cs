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
    }
}
