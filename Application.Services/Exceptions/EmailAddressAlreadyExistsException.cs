using System.Runtime.Serialization;

namespace Application.Services.Services
{
    [Serializable]
    internal class EmailAddressAlreadyExistsException : Exception
    {
        public EmailAddressAlreadyExistsException()
            : base("The EmailAddress already exist!")
        {
        }

        public EmailAddressAlreadyExistsException(string? message) : base(message)
        {
        }

        public EmailAddressAlreadyExistsException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected EmailAddressAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}