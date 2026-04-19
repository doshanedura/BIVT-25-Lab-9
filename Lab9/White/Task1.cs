using System;
using System.Text;

namespace Lab9.White
{
    public class Task1 : White
    {
        private readonly char[] _punctuationSymbols =
        {
            '.', '!', '?', ',', ':', '"', ';', '–', '\'', '(', ')', '[', ']', '{', '}', '/'
        };

        private readonly char[] _sentenceEndings = { '.', '!', '?' };

        private double _complexityAverage;

        public Task1(string text) : base(text)
        {
        }

        public override void Review()
        {
            _complexityAverage = ComputeAverageComplexity();
            Output = _complexityAverage;
        }

        private double ComputeAverageComplexity()
        {
            if (string.IsNullOrEmpty(Input))
                return 0;

            string[] sentences = DivideIntoSentences(Input);

            if (sentences.Length == 0)
                return 0;

            double totalComplexity = 0;
            int actualSentenceCount = 0;

            foreach (string rawSentence in sentences)
            {
                string trimmedSentence = rawSentence.Trim();
                if (string.IsNullOrWhiteSpace(trimmedSentence))
                    continue;

                int wordAmount = CalculateWords(trimmedSentence);
                int punctuationAmount = CalculatePunctuation(trimmedSentence);

                totalComplexity += wordAmount + punctuationAmount;
                actualSentenceCount++;
            }

            return actualSentenceCount == 0 ? 0 : totalComplexity / actualSentenceCount;
        }

        private string[] DivideIntoSentences(string source)
        {
            int sentenceCounter = 0;
            StringBuilder tempBuilder = new StringBuilder();

            for (int i = 0; i < source.Length; i++)
            {
                tempBuilder.Append(source[i]);
                if (IsSentenceEnding(source[i]))
                {
                    bool isLastPosition = i == source.Length - 1;
                    bool nextIsWhitespace = !isLastPosition && char.IsWhiteSpace(source[i + 1]);
                    if (isLastPosition || nextIsWhitespace)
                    {
                        sentenceCounter++;
                        tempBuilder.Clear();
                    }
                }
            }
            if (tempBuilder.Length > 0)
                sentenceCounter++;

            string[] resultSentences = new string[sentenceCounter];
            tempBuilder.Clear();
            int currentIndex = 0;

            for (int i = 0; i < source.Length; i++)
            {
                tempBuilder.Append(source[i]);
                if (IsSentenceEnding(source[i]))
                {
                    bool isLastPosition = i == source.Length - 1;
                    bool nextIsWhitespace = !isLastPosition && char.IsWhiteSpace(source[i + 1]);
                    if (isLastPosition || nextIsWhitespace)
                    {
                        resultSentences[currentIndex++] = tempBuilder.ToString();
                        tempBuilder.Clear();
                    }
                }
            }
            if (tempBuilder.Length > 0)
                resultSentences[currentIndex] = tempBuilder.ToString();

            return resultSentences;
        }

        private bool IsSentenceEnding(char symbol)
        {
            foreach (char ending in _sentenceEndings)
            {
                if (symbol == ending)
                    return true;
            }
            return false;
        }

        private int CalculateWords(string text)
        {
            StringBuilder cleanBuffer = new StringBuilder();

            foreach (char symbol in text)
            {
                if (!IsPunctuationSymbol(symbol))
                    cleanBuffer.Append(symbol);
            }

            string cleanedText = cleanBuffer.ToString().Replace('-', ' ');
            string[] words = cleanedText.Split(new[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            return words.Length;
        }

        private int CalculatePunctuation(string text)
        {
            int counter = 0;

            foreach (char symbol in text)
            {
                if (IsPunctuationSymbol(symbol))
                    counter++;
            }

            return counter;
        }

        private bool IsPunctuationSymbol(char symbol)
        {
            foreach (char punct in _punctuationSymbols)
            {
                if (symbol == punct)
                    return true;
            }
            return false;
        }

        public override string ToString()
        {
            return _complexityAverage.ToString();
        }
    }
}
