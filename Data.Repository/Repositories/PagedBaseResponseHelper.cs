
using Application.DTO.Requests;
using Application.DTO.Responses;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository.Repositories
{
	public static class PagedBaseResponseHelper
	{
		public static async Task<TResponse> GetResponseAsync<TResponse, T>
			(IQueryable<T> query, PagedBaseRequest request)
			where TResponse : PagedBaseResponse<T>, new()
		{
			var response = new TResponse();
			var count = await query.CountAsync();
            response.TotalPages = (int)Math.Ceiling((double)count / request.PageSize);

            response.TotalItems = count;

			if (string.IsNullOrEmpty(request.orderByProperty))
				response.Results = await query.ToListAsync();
			
			else
				response.Results = query.OrderByDynamic(request.orderByProperty)
					.Skip((request.Page - 1) * request.PageSize)
					.Take(request.PageSize)
					.ToList();

			return response;
		}

		private static IEnumerable<T> OrderByDynamic<T>(this IEnumerable<T> query, string propertyName)
		{
			return query.OrderBy(x => x.GetType().GetProperty(propertyName).GetValue(x, null));
		}

    }
}

