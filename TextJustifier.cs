using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TextJustifier
{
    public class TextJustifier
    {
        private readonly ITokenReader _tokenReader;
        private ITokenProcessor _tokenProcessor;
        public TextJustifier(ITokenReader reader, ITokenProcessor tokenProcessor) 
        {
            _tokenReader = reader;
            _tokenProcessor = tokenProcessor;
        }
        public void ProcessAllTokens() 
        {
            Token currentToken = _tokenReader.ReadToken();
            while(currentToken.Type != TokenType.EndOfInput)
            {
                _tokenProcessor.ProcessToken(currentToken);
                currentToken = _tokenReader.ReadToken();
            }
            _tokenProcessor.ProcessEndOfInput();
        }
    }
}
