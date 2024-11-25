using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TextJustifier
{
    public class ParagraphDetectingTokenReaderDecorator : ITokenReader
    {
        private readonly ITokenReader _tokenReader;
        string? _pendingWord;
        public ParagraphDetectingTokenReaderDecorator(ITokenReader tokenReader)
        {
            _tokenReader = tokenReader;
            _pendingWord = null;
        }
        public Token ReadToken()
        {
            if (_pendingWord != null)
            {
                Token t = new Token { Type = TokenType.Word, Word = _pendingWord };
                _pendingWord = null;
                return t;
            }
            var token = _tokenReader.ReadToken();
            if (token.Type == TokenType.EndOfLine)
            {
                int endOfLinesEncountered = 1;
                while ((token = _tokenReader.ReadToken()).Type == TokenType.EndOfLine)
                    endOfLinesEncountered++;
                if (token.Type == TokenType.EndOfInput)
                {
                    return token;
                }
                if (endOfLinesEncountered > 1)
                {
                    _pendingWord = token.Word;
                    return new Token { Type = TokenType.EndOfParagraph, Word = null };
                }
                else
                {
                    _pendingWord = token.Word;
                    return new Token { Type = TokenType.EndOfLine, Word = null };
                }
            }
            return token;
        }
    }
}
