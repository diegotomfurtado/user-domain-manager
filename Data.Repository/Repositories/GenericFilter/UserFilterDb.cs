using System;
using Application.DTO.Requests;

namespace Data.Repository.Repositories.GenericFilter
{
	public class UserFilterDb : PagedBaseRequest
	{

        public string? UserCode { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? EmailAddress { get; set; }

    }
}

