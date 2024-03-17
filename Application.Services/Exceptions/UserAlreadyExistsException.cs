using System.Runtime.Serialization;

namespace Application.Services.Services
{
    [Serializable]
    internal class UserAlreadyExistsException : Exception
    {
        public UserAlreadyExistsException()
            : base("The user already exist!")
        {
        }

        public UserAlreadyExistsException(string? message) : base(message)
        {
        }

        public UserAlreadyExistsException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UserAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}