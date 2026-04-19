using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace White
{
    public class Task1 : White
    {
        private double _averageComplexity;

        public Task1(string text) : base(text)
        {
        }

        protected override object GetDefaultOutput()
        {
            return 0.0;
        }

        public override void Review()
        {
            if (string.IsNullOrWhiteSpace(Input))
            {
                SetOutput(0.0);
                return;
            }

            string[] sentences = SplitIntoSentences(Input);
            if (sentences.Length == 0)
            {
                SetOutput(0.0);
                return;
            }

            int totalComplexity = 0;
            foreach (string sentence in sentences)
            {
                totalComplexity += GetSentenceComplexity(sentence);
            }

            _averageComplexity = (double)totalComplexity / sentences.Length;
            SetOutput(_averageComplexity);
        }

        private string[] SplitIntoSentences(string text)
        {
            List<string> sentences = new List<string>();
            int start = 0;
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                if (c == '.' || c == '!' || c == '?')
                {
                    string sentence = text.Substring(start, i - start + 1).Trim();
                    if (!string.IsNullOrWhiteSpace(sentence))
                    {
                        sentences.Add(sentence);
                    }
                    start = i + 1;
                }
            }

            if (start < text.Length)
            {
                string lastSentence = text.Substring(start).Trim();
                if (!string.IsNullOrWhiteSpace(lastSentence))
                {
                    sentences.Add(lastSentence);
                }
            }

            return sentences.ToArray();
        }

        private int GetSentenceComplexity(string sentence)
        {
            int wordCount = CountWords(sentence);
            int punctuationCount = CountPunctuation(sentence);
            return wordCount + punctuationCount;
        }

        private int CountWords(string text)
        {
            int count = 0;
            bool inWord = false;

            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                if (char.IsLetter(c) || c == '-' || c == '\'')
                {
                    if (!inWord)
                    {
                        inWord = true;
                        count++;
                    }
                }
                else
                {
                    inWord = false;
                }
            }

            return count;
        }

        private int CountPunctuation(string text)
        {
            int count = 0;
            foreach (char c in text)
            {
                if (char.IsPunctuation(c) && c != '-' && c != '\'')
                {
                    count++;
                }
            }
            return count;
        }

        public override string ToString()
        {
            Review();
            return _averageComplexity.ToString(CultureInfo.InvariantCulture);
        }
    }
}
