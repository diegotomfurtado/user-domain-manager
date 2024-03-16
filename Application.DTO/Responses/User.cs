using System;
namespace Application.DTO.Responses
{
	public class User
	{
        public string UserCode { get; set; }

        public string FullName { get; set; }

        public string emailAddress { get; set; }

        public string NotesField { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime? UpdatedTime { get; set; }

        public string? UpdatedBy { get; set; }
    }
}

