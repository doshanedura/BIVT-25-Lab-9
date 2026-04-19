using System;
using System.Text;

namespace Lab9.White
{
    public class Task3 : White
    {
        private string[,] _replacementTable;
        private string _processedText;

        public Task3(string text, string[,] codes) : base(text)
        {
            _replacementTable = codes;
            _processedText = string.Empty;
        }

        public string ResultText
        {
            get
            {
                if (string.IsNullOrEmpty(_processedText))
                    return Input;
                return _processedText;
            }
        }

        public override void Review()
        {
            _processedText = TransformTextWithCodes(Input);
        }

        private string TransformTextWithCodes(string source)
        {
            StringBuilder outputBuilder = new StringBuilder();
            StringBuilder wordBuffer = new StringBuilder();

            for (int position = 0; position < source.Length; position++)
            {
                char currentChar = source[position];

                if (char.IsLetter(currentChar) || currentChar == '\'' || currentChar == '-')
                {
                    wordBuffer.Append(currentChar);
                }
                else
                {
                    if (wordBuffer.Length > 0)
                    {
                        string currentWord = wordBuffer.ToString();
                        string replacementCode = LookupCode(currentWord);
                        outputBuilder.Append(replacementCode);
                        wordBuffer.Clear();
                    }
                    outputBuilder.Append(currentChar);
                }
            }

            if (wordBuffer.Length > 0)
            {
                string currentWord = wordBuffer.ToString();
                string replacementCode = LookupCode(currentWord);
                outputBuilder.Append(replacementCode);
            }

            return outputBuilder.ToString();
        }

        private string LookupCode(string searchWord)
        {
            for (int row = 0; row < _replacementTable.GetLength(0); row++)
            {
                if (string.Equals(_replacementTable[row, 0], searchWord, StringComparison.Ordinal))
                {
                    return _replacementTable[row, 1];
                }
            }
            return searchWord;
        }

        public override string ToString()
        {
            return ResultText;
        }
    }
}
