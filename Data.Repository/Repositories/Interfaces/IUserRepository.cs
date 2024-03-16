using Domain.Model;

namespace Data.Repository.Repositories.Interfaces
{
	public interface IUserRepository
	{
        Task CreateProductAsync(User user);
        Task<List<User>> GetUserAsync();
        Task<User> GetUserByCodeAsync(string userCode);
        Task UpdateUserByCodeAsync(User user);
        Task DeleteUserByCodeAsync(User user);
    }
}