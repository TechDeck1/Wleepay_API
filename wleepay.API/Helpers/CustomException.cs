using System;

namespace wleepay.API.Helpers
{
    public class CustomException : Exception
    {
        public CustomException(string message) : base(message)
        {
        }
    }

}
