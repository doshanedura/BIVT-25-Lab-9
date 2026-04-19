using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace White
{
    public class Task2 : White
    {
        private int[,] _syllableMatrix;

        public Task2(string text) : base(text)
        {
        }

        protected override object GetDefaultOutput()
        {
            return new int[0, 0];
        }

        public override void Review()
        {
            if (string.IsNullOrWhiteSpace(Input))
            {
                SetOutput(new int[0, 0]);
                return;
            }

            string[] words = ExtractWords(Input);
            Dictionary<int, int> syllableCount = new Dictionary<int, int>();

            foreach (string word in words)
            {
                int syllables = CountSyllables(word);
                if (syllableCount.ContainsKey(syllables))
                    syllableCount[syllables]++;
                else
                    syllableCount[syllables] = 1;
            }

            var sorted = syllableCount.OrderBy(kvp => kvp.Key).ToList();
            int[,] result = new int[sorted.Count, 2];

            for (int i = 0; i < sorted.Count; i++)
            {
                result[i, 0] = sorted[i].Key;
                result[i, 1] = sorted[i].Value;
            }

            SetOutput(result);
        }

        private string[] ExtractWords(string text)
        {
            List<string> words = new List<string>();
            StringBuilder currentWord = new StringBuilder();

            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                if (char.IsLetter(c) || c == '-' || c == '\'')
                {
                    currentWord.Append(c);
                }
                else
                {
                    if (currentWord.Length > 0)
                    {
                        words.Add(currentWord.ToString());
                        currentWord.Clear();
                    }
                }
            }

            if (currentWord.Length > 0)
            {
                words.Add(currentWord.ToString());
            }

            return words.ToArray();
        }

        private int CountSyllables(string word)
        {
            int count = 0;
            bool prevIsVowel = false;

            foreach (char c in word.ToLower())
            {
                if (IsVowel(c))
                {
                    if (!prevIsVowel)
                    {
                        count++;
                        prevIsVowel = true;
                    }
                }
                else
                {
                    prevIsVowel = false;
                }
            }

            return count == 0 ? 1 : count;
        }

        private bool IsVowel(char c)
        {
            char lower = char.ToLower(c);
            return lower == 'a' || lower == 'e' || lower == 'i' || lower == 'o' || lower == 'u' ||
                   lower == 'y' || lower == 'а' || lower == 'е' || lower == 'ё' || lower == 'и' ||
                   lower == 'о' || lower == 'у' || lower == 'ы' || lower == 'э' || lower == 'ю' ||
                   lower == 'я';
        }

        public override string ToString()
        {
            Review();
            int[,] matrix = (int[,])Output;
            if (matrix.GetLength(0) == 0) return "";

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                sb.Append($"{matrix[i, 0]}:{matrix[i, 1]}");
                if (i < matrix.GetLength(0) - 1)
                    sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}
