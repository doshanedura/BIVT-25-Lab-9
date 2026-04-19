using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Lab9.White
{
    public abstract class White
    {
        private string _input;
        private object? _output;

        public string Input => _input;
        public object? Output => _output;

        protected White(string text)
        {
            _input = text;
            _output = GetDefaultOutput();
        }

        protected virtual object? GetDefaultOutput()
        {
            return null;
        }

        public abstract void Review();

        public virtual void ChangeText(string text)
        {
            _input = text;
            Review();
        }

        protected void SetOutput(object value)
        {
            _output = value;
        }

        public override abstract string ToString();
    }
}
