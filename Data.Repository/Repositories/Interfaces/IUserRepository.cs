using Domain.Model;
using Dto = Application.DTO.Responses;
using Data.Repository.Repositories.GenericFilter;

namespace Data.Repository.Repositories.Interfaces
{
	public interface IUserRepository
	{
        Task CreateProductAsync(User user);
        Task<User> GetUserByCodeAsync(string userCode);
        Task UpdateUserByCodeAsync(User user);
        Task DeleteUserByCodeAsync(User user);
        Task<Dto.PagedBaseResponse<User>> GetPagedAsync(UserFilterDb userFilterDb);

        //To improve - Create a generic interface
        Task<Boolean> CheckExistenceOfUsersAsync(Application.DTO.Requests.User user);
        Task<bool> CheckExistenceOfEmailAddressAsync(Application.DTO.Requests.User user);
        Task<bool> CheckExistenceOfEmailAddressAsync(Application.DTO.Requests.UserUpdate user);
    }
}