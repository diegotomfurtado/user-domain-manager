using System;
namespace Application.DTO.Responses
{
    public class PagedBaseResponseDTO<T>
    {
        public PagedBaseResponseDTO(int totalItems, int totalPages, List<T> results)
        {
            TotalItems = totalItems;
            TotalPages = totalPages;
            Results = results;
        }

        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public List<T> Results { get; set; }
    }
}

