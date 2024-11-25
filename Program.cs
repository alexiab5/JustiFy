using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TextJustifier
{
    public class Program
    {
        static void Main(string[] args)
        {
            if (!Validator.ValidateCommandLineArguments(args))
            {
                ErrorHandler.printErrorMessage("cmd exception");
                return;
            }

            string inputFileName = args[0];
            if (!Validator.ValidateFile(inputFileName, FileAccess.Read))
            {
                ErrorHandler.printErrorMessage("file credentials exception");
                return;
            }

            string outputFileName = args[1];
            int maxWidth;
            try
            {
                maxWidth = int.Parse(args[2]); //?
                if (maxWidth <= 0)
                {
                    throw new ArgumentException();
                }
            }
            catch (Exception)
            {
                ErrorHandler.printErrorMessage("cmd exception");
                return;
            }
            using (var reader = new StreamReader(inputFileName))
            {
                ITokenReader tokenReader = new TokenReaderByChars(reader);
                var paragraphReader = new ParagraphDetectingTokenReaderDecorator(tokenReader);
                try
                {
                    using (var writer = new StreamWriter(outputFileName))
                    {
                        ITokenProcessor tokenProcessor = new ParagraphJustifier(writer, maxWidth);
                        var processor = new TextJustifier(paragraphReader, tokenProcessor);
                        processor.ProcessAllTokens();
                    }
                }
                catch (Exception)
                {
                    ErrorHandler.printErrorMessage("file credentials exception"); 
                    return;
                }
            }
         }
    }
}
   