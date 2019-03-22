using System;

namespace lab4_cs.Exceptions
{
    class InvalidLastNameException : Exception
    {
        private static readonly string DefaultMessage = "Wrong format for last name: ";
        public string LastName { get; private set; }

        public InvalidLastNameException(string lastName) : base(DefaultMessage)
        {
            LastName = lastName;
        }
    }
}
