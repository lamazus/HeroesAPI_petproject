using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Exceptions
{
    public class UserRegistrationException : Exception
    {
        public UserRegistrationException(string property) : base($"User with current {property} is already exists")
        {

        }
    }
}
