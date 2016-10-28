using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meow.Shared.Infrastructure.Exceptions
{
    public class InvalidInputException: Exception
    {
        private string _inputName;

        public InvalidInputException(string inputName)
        {
            _inputName = inputName;
        }

        public override string Message
        {
            get
            {
                return $"Invalid input found: {_inputName}";
            }
        }
    }
}
