using System;
namespace Application.DTO.Responses
{
	public class PagedBaseResponseDTO<T>
	{
        public PagedBaseResponseDTO(int totalItems, List<T> results)
        {
            TotalItems = totalItems;
            Results = results;
        }

        public int TotalItems { get; set; }
        public List<T> Results { get; set; }
    }
}

