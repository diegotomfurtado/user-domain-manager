using Domain.Model;
using Dto = Application.DTO.Responses;
using Data.Repository.Repositories.GenericFilter;

namespace Data.Repository.Repositories.Interfaces
{
	public interface IUserRepository
	{
        Task CreateUserAsync(User user);
        Task<User> GetUserByCodeAsync(string userCode);
        Task UpdateUserByCodeAsync(User user);
        Task DeleteUserByCodeAsync(User user);
        Task<Dto.PagedBaseResponse<User>> GetPagedAsync(UserFilterDb userFilterDb);

        Task<Boolean> CheckExistenceOfUsersAsync(string userCode);
        Task<bool> CheckExistenceOfEmailAddressAsync(string emailAddress);
    }
}