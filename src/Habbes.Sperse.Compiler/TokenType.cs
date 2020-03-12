using System;
namespace Habbes.Sperse.Compiler
{
    public enum TokenType
    {
        Identifier,
        IntConstant,
        StringConstant,
        OpenParen,
        CloseParen,
        Comma,
        Whitespace,
        Eof
    }
}
