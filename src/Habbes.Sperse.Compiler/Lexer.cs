using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Habbes.Sperse.Compiler
{
    public class Lexer
    {

        private static (Regex Rule, TokenType Type)[] tokenRules = new[]
        {
            (new Regex(@"^\s+"), TokenType.Whitespace),
            (new Regex(@"^,"), TokenType.Comma),
            (new Regex(@"^\("), TokenType.OpenParen),
            (new Regex(@"^\)"), TokenType.CloseParen),
            (new Regex(@"^[a-zA-Z]+"), TokenType.Identifier)
        };

        public Lexer(string source)
        {
            Source = source;
            CurrentIndex = 0;
        }

        public string Source { get; private set; }
        public int CurrentIndex { get; private set; }
        public string RemainingSource { get; private set; }

        public bool TryGetNextToken(out Token token)
        {
            token = new Token();
            if (CurrentIndex == Source.Length)
            {
                token.Type = TokenType.Eof;
                return true;
            }
            foreach (var rule in tokenRules)
            {
                var match = rule.Rule.Match(Source, CurrentIndex, Source.Length);
                if (!match.Success)
                {
                    continue;
                }

                token.Value = match.Value;
                token.Type = rule.Type;

                CurrentIndex = match.Index + match.Length;
                
                return true;
            }

            return false;
        }

        public IEnumerable<Token> GetTokens()
        {

            while (CurrentIndex < Source.Length)
            {
                var result = TryGetNextToken(out var token);
                if (!result)
                {
                    throw new Exception($"Invalid syntax near {Source.Substring(CurrentIndex)}");
                }
                if (token.Type == TokenType.Whitespace || token.Type == TokenType.Comma)
                {
                    continue;
                }
                yield return token;
            }
        }
    }
}
