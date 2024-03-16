
using Domain.Model;

namespace Application.Services.Services.Interface
{
	public interface IUserServices
	{
        Task CreateUserAsync(DTO.Requests.User user, string userName);
        Task<List<User>> GetUserAsync();
        Task<DTO.Responses.User> GetUserByCodeAsync(string userCode);
        Task UpdateUserByCodeAsync(string userCode, DTO.Requests.UserUpdate user, string userName);
        Task DeleteUserByCodeAsync(string userCode);
    }
}

