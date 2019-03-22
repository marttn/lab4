using System;

namespace lab4_cs.Exceptions
{
    class InvalidNameException : Exception
    {
        private static readonly string DefaultMessage = "Wrong format for name: ";
        public string Name { get; private set; }

        public InvalidNameException(string name) : base(DefaultMessage)
        {
            Name = name;
        }


    }
}
