
using System;
using System.Net.Mail;
using System.Text.RegularExpressions;
using Request = Application.DTO.Requests;

namespace Domain.Model.Validation
{
	public static class UsertValidation
    {
        public static void ValidateInput(this Request.User user)
        {
            var exceptions = new List<ArgumentException>();
            var regexItem = new Regex(@"\d");

            if (regexItem.IsMatch(user.FirstName))
            {
                exceptions.Add(new ArgumentException("The 'FirstName' field cannot contain numeric characters."));
            }

            if (string.IsNullOrEmpty(user.FirstName))
            {
                exceptions.Add(new ArgumentException("The 'FirstName' field should not be null or empty."));
            }

            if (regexItem.IsMatch(user.LastName))
            {
                exceptions.Add(new ArgumentException("The 'LastName' field cannot contain numeric characters."));
            }

            if (string.IsNullOrEmpty(user.LastName))
            {
                exceptions.Add(new ArgumentException("The 'LastName' field should not be null or empty."));
            }

            if (!IsValid(user.emailAddress))
            {
                exceptions.Add(new ArgumentException("Invalid email format. Please provide a valid email address."));
            }

            if (string.IsNullOrEmpty(user.emailAddress))
            {
                exceptions.Add(new ArgumentException("The 'EmailAddress' field should not be null or empty."));
            }

            if (exceptions.Any())
            {
                throw new AggregateException(exceptions);
            }
        }

        public static void ValidateInput(this Request.UserUpdate user)
        {
            var exceptions = new List<ArgumentException>();
            var regexItem = new Regex(@"\d");

            if (user == null)
            {
                exceptions.Add(new ArgumentException("Objects should be filled."));
            }

            if (regexItem.IsMatch(user.FirstName))
            {
                exceptions.Add(new ArgumentException("The 'FirstName' field cannot contain numeric characters."));
            }

            if (regexItem.IsMatch(user.LastName))
            {
                exceptions.Add(new ArgumentException("The 'LastName' field cannot contain numeric characters."));
            }

            if (!IsValid(user.emailAddress))
            {
                exceptions.Add(new ArgumentException("Invalid email format. Please provide a valid email address."));
            }

            if (exceptions.Any())
            {
                throw new AggregateException(exceptions);
            }
        }

        private static bool IsValid(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}

