using System.Runtime.Serialization;

namespace Application.Services.Services
{
    [Serializable]
    internal class UserNotFoundException : Exception
    {
        public UserNotFoundException()
            : base("The user doesn't exist yet.")
        {
        }

        public UserNotFoundException(string? message) : base(message)
        {
        }

        public UserNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UserNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}