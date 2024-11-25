using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TextJustifier
{
    public class ParagraphJustifier : ITokenProcessor
    {
        private readonly int _maxWidth;
        private TextWriter _writer;
        private List<string> _currentLine;
        private int _currentLength;

        public ParagraphJustifier(TextWriter writer, int maxWidth)
        {
            _writer = writer;
            _maxWidth = maxWidth;
            _currentLength = 0;
            _currentLine = [];
        }

        public void ProcessToken(Token token)
        {
            if(token.Type == TokenType.EndOfLine)
            {
                //System.Console.Out.WriteLine("END OF LINE");
                return;
            }
            if(token.Type == TokenType.EndOfParagraph)
            {
                //System.Console.Out.WriteLine("END OF PARAGRAPH");
                ProcessEndOfParagraph(false);
                return;
            }
            //System.Console.Out.WriteLine(token.Word);
            if (_currentLength + _currentLine.Count + token.Word.Length > _maxWidth)
            {
                ProcessEndOfLine();
            }
            _currentLine.Add(token.Word);
            _currentLength += token.Word.Length;     
        }

        private void ProcessEndOfLine()
        {
            if (_currentLine.Count == 0)
                return;
            if (_currentLine.Count == 1)
            {
                _writer.WriteLine(_currentLine[0]);
            }
            else
            {
                int totalSpaceCount = _maxWidth - _currentLength;
                int spacesBetweenWords = totalSpaceCount / (_currentLine.Count - 1); // n-1 spaces between n words
                int spacesLeft = totalSpaceCount % (_currentLine.Count - 1);
                var currentLineBuilder = new StringBuilder();
                for (int i = 0; i < _currentLine.Count; i++)
                {
                    currentLineBuilder.Append(_currentLine[i]);
                    if (i < _currentLine.Count - 1)
                    {
                        int spaceCount = spacesBetweenWords + (i < spacesLeft ? 1 : 0);
                        currentLineBuilder.Append(new string(' ', spaceCount));
                    }
                }
                _writer.WriteLine(currentLineBuilder.ToString());
            }
            _currentLine.Clear();
            _currentLength = 0;
        }

        private void ProcessEndOfParagraph(bool isFinalParagraph)
        {
            if (_currentLine.Count > 0)
            {
                _writer.WriteLine(string.Join(" ", _currentLine));
                _currentLine.Clear();
                _currentLength = 0;
            }
            if (!isFinalParagraph)
            {
                _writer.WriteLine();
            }
        }

        public void ProcessEndOfInput()
        {
            if(_currentLength > 0)
            {
                ProcessEndOfParagraph(true);
            }
        }
    }
}
