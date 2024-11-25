using System;
using System.IO;
using System.Text;

namespace TextJustifier
{
    public class TokenReaderByChars : ITokenReader
    {
        private readonly TextReader _reader;
        private readonly StringBuilder _currentWord;
        private bool _endOfLinePending;
        private bool _endOfInputReached;

        public TokenReaderByChars(TextReader reader)
        {
            _reader = reader;
            _currentWord = new StringBuilder();
            _endOfLinePending = false;
            _endOfInputReached = false;
        }

        public Token ReadToken()
        {
            if (_endOfLinePending)
            {
                _endOfLinePending = false;
                return new Token { Type = TokenType.EndOfLine, Word = null };
            }

            if (_endOfInputReached)
            {
                return new Token { Type = TokenType.EndOfInput, Word = null };
            }

            int currentChar;
            while ((currentChar = _reader.Read()) != -1)
            {
                char character = (char)currentChar;
                if (character == '\r')
                    continue;
                if (character == ' ' || character == '\t' || character == '\n')
                {
                    if (_currentWord.Length > 0)
                    {
                        string word = _currentWord.ToString();
                        _currentWord.Clear();
                        if (character == '\n')
                        {
                            _endOfLinePending = true; 
                        }
                        return new Token { Type = TokenType.Word, Word = word };
                    }
                    if (character == '\n')
                    {
                        return new Token { Type = TokenType.EndOfLine, Word = null };
                    }
                }
                else
                    _currentWord.Append(character);
            }
            if (_currentWord.Length > 0)
            {
                string word = _currentWord.ToString();
                _currentWord.Clear();
                _endOfInputReached = true;
                return new Token { Type = TokenType.Word, Word = word };
            }

            _endOfInputReached = true;
            return new Token { Type = TokenType.EndOfInput, Word = null };
        }
    }
}