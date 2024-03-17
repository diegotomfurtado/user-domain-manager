using System;
namespace Application.DTO.Requests
{
	public class UserFilter
	{
        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 100;

        public string? userCode { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? emailAddress { get; set; }

        public string? orderByProperty { get; set; }
    }
}

