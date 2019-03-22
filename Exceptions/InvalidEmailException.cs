using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4_cs.Exceptions
{
    class InvalidEmailAdressException : Exception
    {

        public InvalidEmailAdressException(string message) : base($"invalid email adress: {message}")
        {

        }

    }
}
