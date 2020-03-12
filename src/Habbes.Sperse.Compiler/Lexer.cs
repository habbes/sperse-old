using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Habbes.Sperse.Compiler
{
    public class Lexer
    {

        private static readonly (Regex Rule, TokenType Type)[] TokenRules = new[]
        {
            (new Regex(@"^\s+"), TokenType.Whitespace),
            (new Regex(@"^,"), TokenType.Comma),
            (new Regex(@"^\("), TokenType.OpenParen),
            (new Regex(@"^\)"), TokenType.CloseParen),
            (new Regex(@"^[a-zA-Z\.]+"), TokenType.Identifier),
            (new Regex(@"^[1-9]\d*"), TokenType.IntConstant)
        };

        private static readonly TokenType[] SkippedTokens = new[]
        {
            TokenType.Whitespace,
            TokenType.Comma
        };

        public Lexer(string source)
        {
            Source = source;
            CurrentIndex = 0;
        }

        public string Source { get; private set; }
        public int CurrentIndex { get; private set; }

        public bool TryGetNextToken(out Token token)
        {
            token = new Token();
            if (CurrentIndex == Source.Length)
            {
                token.Type = TokenType.Eof;
                return true;
            }
            foreach (var rule in TokenRules)
            {
                var match = rule.Rule.Match(Source, CurrentIndex, Source.Length);
                if (!match.Success)
                {
                    continue;
                }
                
                token.Value = match.Value;
                token.Type = rule.Type;

                CurrentIndex = match.Index + match.Value.Length;
                
                return true;
            }

            return false;
        }
        public IEnumerable<Token> GetTokens()
        {

            bool isDone = false;
            while (!isDone)
            {
                var result = TryGetNextToken(out var token);
                if (!result)
                {
                    throw new Exception($"Invalid syntax at '{Source.Substring(CurrentIndex, Math.Min(10, Source.Length - CurrentIndex))}'");
                }
                if (token.Type == TokenType.Whitespace || token.Type == TokenType.Comma)
                {
                    continue;
                }

                isDone = token.Type == TokenType.Eof;
                yield return token;
            }
        }

        public ITokenStream GetTokenStream()
        {
            return new TokenStream(GetTokens().GetEnumerator());
        }
    }
}
