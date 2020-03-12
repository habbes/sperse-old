namespace Habbes.Sperse.Compiler
{
    public interface ITokenStream
    {
        Token ConsumeNext();
        Token PeekNext();
        bool IsNextType(params TokenType[] expectedTypes);
        Token ConsumeNextType(params TokenType[] expectedTypes);
        bool Eof { get; }
    }
}