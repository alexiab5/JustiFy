using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TextJustifier
{
    public enum TokenType { Word, EndOfInput, EndOfLine, EndOfParagraph };
    public readonly struct Token
    {
        public Token(TokenType type, string? word)
        {
            Type = type;
            Word = word;
        }
        public required TokenType Type { get; init; }
        public required string? Word { get; init; }
    }
}
