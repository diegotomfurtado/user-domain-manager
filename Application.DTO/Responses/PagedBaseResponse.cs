
namespace Application.DTO.Responses
{
	public class PagedBaseResponse<T>
    {
        public int TotalPages { get; set; }

        public int TotalItems { get; set; }

        public List<T> Results { get; set; }
    }
}

