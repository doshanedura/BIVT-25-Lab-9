using System;

namespace Lab9.White
{
    public abstract class White
    {
        public string Input { get; private set; }

        public virtual object? Output { get; protected set; }

        protected White(string input)
        {
            Input = input;
            Output = GetDefaultOutput();
        }

        protected virtual object? GetDefaultOutput()
        {
            return null;
        }

        public abstract void Review();

        public virtual void ChangeText(string text)
        {
            Input = text;
            Review();
        }

        public override abstract string ToString();
    }
}
