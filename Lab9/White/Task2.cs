using System;
using System.Text;

namespace Lab9.White
{
    public class Task2 : White
    {
        private int[,] _syllableStats;

        public Task2(string text) : base(text) 
        { 
            _syllableStats = new int[0, 0];
        }

        public override void Review()
        {
            _syllableStats = BuildSyllableMatrix(Input);
        }

        public int[,] Output => _syllableStats ?? new int[0, 2];

        private int[,] BuildSyllableMatrix(string source)
        {
            string[] extractedWords = ExtractValidWords(source);
            
            if (extractedWords.Length == 0)
                return new int[0, 2];

            char[] vowelLetters = GetVowelArray();
            
            int[] syllableCounts = new int[extractedWords.Length];
            int maximumSyllables = 0;

            for (int i = 0; i < extractedWords.Length; i++)
            {
                int vowelQuantity = 0;
                
                foreach (char letter in extractedWords[i])
                {
                    if (ContainsChar(vowelLetters, letter))
                        vowelQuantity++;
                }

                if (vowelQuantity == 0)
                    vowelQuantity = 1;

                syllableCounts[i] = vowelQuantity;

                if (vowelQuantity > maximumSyllables)
                    maximumSyllables = vowelQuantity;
            }

            int[,] tempStorage = new int[maximumSyllables, 2];

            for (int i = 0; i < extractedWords.Length; i++)
            {
                int syllables = syllableCounts[i];
                if (syllables > 0)
                {
                    tempStorage[syllables - 1, 0] = syllables;
                    tempStorage[syllables - 1, 1]++;
                }
            }

            int nonEmptyRows = 0;
            for (int i = 0; i < tempStorage.GetLength(0); i++)
            {
                if (tempStorage[i, 1] > 0)
                    nonEmptyRows++;
            }

            int[,] finalResult = new int[nonEmptyRows, 2];
            int targetIndex = 0;
            
            for (int i = 0; i < tempStorage.GetLength(0); i++)
            {
                if (tempStorage[i, 1] > 0)
                {
                    finalResult[targetIndex, 0] = tempStorage[i, 0];
                    finalResult[targetIndex, 1] = tempStorage[i, 1];
                    targetIndex++;
                }
            }

            return finalResult;
        }

        private string[] ExtractValidWords(string source)
        {
            StringBuilder wordBuffer = new StringBuilder();
            bool wordAfterDigit = false;
            bool previousWasDigit = false;
            int validWordCounter = 0;

            for (int position = 0; position < source.Length; position++)
            {
                char currentChar = source[position];
                
                if (char.IsLetter(currentChar) || currentChar == '-' || currentChar == '\'')
                {
                    if (wordBuffer.Length == 0)
                        wordAfterDigit = previousWasDigit;
                    wordBuffer.Append(currentChar);
                    previousWasDigit = false;
                }
                else
                {
                    if (wordBuffer.Length > 0)
                    {
                        if (!wordAfterDigit)
                            validWordCounter++;
                        wordBuffer.Clear();
                    }
                    previousWasDigit = char.IsDigit(currentChar);
                }
            }
            
            if (wordBuffer.Length > 0 && !wordAfterDigit)
                validWordCounter++;

            string[] wordArray = new string[validWordCounter];
            wordBuffer.Clear();
            wordAfterDigit = false;
            previousWasDigit = false;
            int currentWordIndex = 0;

            for (int position = 0; position < source.Length; position++)
            {
                char currentChar = source[position];
                
                if (char.IsLetter(currentChar) || currentChar == '-' || currentChar == '\'')
                {
                    if (wordBuffer.Length == 0)
                        wordAfterDigit = previousWasDigit;
                    wordBuffer.Append(currentChar);
                    previousWasDigit = false;
                }
                else
                {
                    if (wordBuffer.Length > 0)
                    {
                        if (!wordAfterDigit)
                            wordArray[currentWordIndex++] = wordBuffer.ToString();
                        wordBuffer.Clear();
                    }
                    previousWasDigit = char.IsDigit(currentChar);
                }
            }
            
            if (wordBuffer.Length > 0 && !wordAfterDigit)
                wordArray[currentWordIndex] = wordBuffer.ToString();

            return wordArray;
        }

        private char[] GetVowelArray()
        {
            return new char[] 
            { 
                'а', 'е', 'ё', 'и', 'о', 'у', 'ы', 'э', 'ю', 'я',
                'А', 'Е', 'Ё', 'И', 'О', 'У', 'Ы', 'Э', 'Ю', 'Я',
                'a', 'e', 'i', 'o', 'u', 'y', 'A', 'E', 'I', 'O', 'U', 'Y'
            };
        }

        private bool ContainsChar(char[] targetArray, char searchChar)
        {
            for (int i = 0; i < targetArray.Length; i++)
            {
                if (targetArray[i] == searchChar)
                    return true;
            }
            return false;
        }

        public override string ToString()
        {
            int[,] matrix = Output;
            
            if (matrix.GetLength(0) == 0) 
                return string.Empty;

            StringBuilder outputBuilder = new StringBuilder();

            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                outputBuilder.Append(matrix[row, 0]);
                outputBuilder.Append(':');
                outputBuilder.Append(matrix[row, 1]);
                
                if (row < matrix.GetLength(0) - 1)
                    outputBuilder.AppendLine();
            }

            return outputBuilder.ToString();
        }
    }
}
