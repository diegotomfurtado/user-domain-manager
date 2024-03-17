using System;
namespace Application.DTO.Requests
{
	public class PagedBaseRequest
	{
        public int Page { get; set; }

        public int PageSize { get; set; }

        public string? orderByProperty { get; set; }

        public PagedBaseRequest()
        {
            Page = 1;
            PageSize = 10;
            orderByProperty = "Id";
        }
    }
}

