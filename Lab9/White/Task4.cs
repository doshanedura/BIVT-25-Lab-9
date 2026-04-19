using System;

namespace Lab9.White
{
    public class Task4 : White
    {
        private int _digitTotal;

        public Task4(string text) : base(text)
        {
            _digitTotal = 0;
        }

        public override void Review()
        {
            _digitTotal = ComputeDigitSum(Input);
            Output = _digitTotal;
        }

        private int ComputeDigitSum(string source)
        {
            if (string.IsNullOrEmpty(source))
                return 0;

            int accumulatedSum = 0;

            for (int position = 0; position < source.Length; position++)
            {
                char currentSymbol = source[position];

                if (currentSymbol >= '0' && currentSymbol <= '9')
                {
                    accumulatedSum += currentSymbol - '0';
                }
            }

            return accumulatedSum;
        }

        public override string ToString()
        {
            return _digitTotal.ToString();
        }
    }
}
