
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
            var regexItemName = new Regex(@"^[A-Za-zÀ-ÿ.'\s]+$");
            var regexItemEmail = new Regex(@"^\S+$");

            if (user == null)
            {
                exceptions.Add(new ArgumentException("Objects should be filled."));
            }
            else
            {

                if (string.IsNullOrEmpty(user.FirstName))
                {
                    exceptions.Add(new ArgumentException("The 'FirstName' field should not be null or empty."));
                }

                if (!regexItemName.IsMatch(user.FirstName))
                {
                    exceptions.Add(new ArgumentException("The 'FirstName' field cannot contain numeric characters."));
                }

                if (string.IsNullOrEmpty(user.LastName))
                {
                    exceptions.Add(new ArgumentException("The 'LastName' field should not be null or empty."));
                }

                if (!regexItemName.IsMatch(user.LastName))
                {
                    exceptions.Add(new ArgumentException("The 'LastName' field cannot contain numeric characters."));
                }

                if (string.IsNullOrEmpty(user.EmailAddress))
                {
                    exceptions.Add(new ArgumentException("The 'EmailAddress' field should not be null or empty."));
                }

                if (!IsValid(user.EmailAddress))
                {
                    exceptions.Add(new ArgumentException("Invalid email format. Please provide a valid email address."));
                }

            }

            if (exceptions.Any())
            {
                throw new AggregateException(exceptions);
            }
        }

        public static void ValidateInput(this Request.UserUpdate user)
        {
            var exceptions = new List<ArgumentException>();
            var regexItemName = new Regex(@"\d");
            var regexItemEmail = new Regex(@"^\S+$");

            if (user == null)
            {
                exceptions.Add(new ArgumentException("Objects should be filled."));
            }
            else
            {
                if (!string.IsNullOrEmpty(user.FirstName) && regexItemName.IsMatch(user.FirstName))
                {
                    exceptions.Add(new ArgumentException("The 'FirstName' field cannot contain numeric characters."));
                }

                if (!string.IsNullOrEmpty(user.LastName) && regexItemName.IsMatch(user.LastName))
                {
                    exceptions.Add(new ArgumentException("The 'LastName' field cannot contain numeric characters."));
                }

                if ((!string.IsNullOrEmpty(user.EmailAddress) && !IsValid(user.EmailAddress)) || !regexItemEmail.IsMatch(user.EmailAddress))
                {
                    exceptions.Add(new ArgumentException("Invalid email format. Please provide a valid email address."));
                }
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
                MailAddress email = new MailAddress(emailaddress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

    }
}

