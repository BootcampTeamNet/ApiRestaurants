using System;

namespace Services.Interfaces.Exceptions
{
    public class EntityBadRequestException : Exception
    {
        public EntityBadRequestException(string message)
        {
            Message = message;
        }
        public string Message { get; }
    }
}
