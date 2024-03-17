using System;
namespace Application.DTO.Responses
{
	public class PageResult<T>
    {
        public PageResult()
        {
            this.Number = 0;
            this.PageSize = 100;
            this.TotalPages = 0;
            this.TotalItems = 0;
            this.Results = new List<T>();
        }

        public int Number { get; set; }

        public int PageSize { get; set; }

        public long TotalPages { get; set; }

        public long TotalItems { get; set; }

        public IEnumerable<T> Results { get; set; }
    }
}

