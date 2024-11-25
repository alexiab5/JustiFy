using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TextJustifier
{
    public interface ITokenProcessor
    {
        public void ProcessToken(Token token);
        public void ProcessEndOfInput();
    }
}
