using System;

namespace Services.Interfaces.Exceptions
{
    public class InaccessibleResourceException : Exception
    {
        public InaccessibleResourceException(string message)
        {
            Message = message;
        }
        public string Message { get; }
    }
}
