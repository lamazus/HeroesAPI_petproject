

namespace Application.Common.Exceptions
{
    public class AuthenticationErrorException : Exception
    {
        public AuthenticationErrorException() :
            base("Pair Username-password doesn't match")
        {

        }
    }
}
