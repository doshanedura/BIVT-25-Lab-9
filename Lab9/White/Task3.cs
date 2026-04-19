using System;
using System.Collections.Generic;
using System.Text;

namespace White
{
    public class Task3 : White
    {
        private string[,] _codeTable;
        private string _resultText;

        public Task3(string text, string[,] codeTable) : base(text)
        {
            _codeTable = codeTable;
        }

        protected override object GetDefaultOutput()
        {
            return string.Empty;
        }

        public override void Review()
        {
            if (string.IsNullOrWhiteSpace(Input))
            {
                SetOutput(string.Empty);
                return;
            }

            Dictionary<string, string> replacements = BuildReplacementDictionary();
            _resultText = ReplaceWords(Input, replacements);
            SetOutput(_resultText);
        }

        private Dictionary<string, string> BuildReplacementDictionary()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            if (_codeTable == null) return dict;

            for (int i = 0; i < _codeTable.GetLength(0); i++)
            {
                string word = _codeTable[i, 0];
                string code = _codeTable[i, 1];
                if (!string.IsNullOrEmpty(word))
                {
                    dict[word] = code;
                }
            }
            return dict;
        }

        private string ReplaceWords(string text, Dictionary<string, string> replacements)
        {
            StringBuilder result = new StringBuilder();
            StringBuilder currentWord = new StringBuilder();
            bool inWord = false;

            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                if (char.IsLetter(c) || c == '-' || c == '\'')
                {
                    currentWord.Append(c);
                    inWord = true;
                }
                else
                {
                    if (inWord)
                    {
                        string word = currentWord.ToString();
                        if (replacements.ContainsKey(word))
                        {
                            result.Append(replacements[word]);
                        }
                        else
                        {
                            result.Append(word);
                        }
                        currentWord.Clear();
                        inWord = false;
                    }
                    result.Append(c);
                }
            }

            if (inWord)
            {
                string word = currentWord.ToString();
                if (replacements.ContainsKey(word))
                {
                    result.Append(replacements[word]);
                }
                else
                {
                    result.Append(word);
                }
            }

            return result.ToString();
        }

        public override string ToString()
        {
            Review();
            return (string)Output;
        }
    }
}
