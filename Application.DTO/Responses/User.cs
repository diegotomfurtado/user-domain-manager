using System;
namespace Application.DTO.Responses
{
	public class User
	{
        public string UserCode { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string emailAddress { get; set; }

        public string NotesField { get; set; }

        public DateTime CreationTime { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? UpdatedTime { get; set; }

        public string? UpdatedBy { get; set; }
    }
}

