
using Data.Repository.Repositories.GenericFilter;
using Dto = Application.DTO.Responses;
using Domain.Model;

namespace Application.Services.Services.Interface
{
	public interface IUserServices
	{
        Task CreateUserAsync(DTO.Requests.User user, string userName);
        Task<Dto.User> GetUserByCodeAsync(string userCode);
        Task UpdateUserByCodeAsync(string userCode, DTO.Requests.UserUpdate user, string userName);
        Task DeleteUserByCodeAsync(string userCode);
        Task<Dto.PagedBaseResponseDTO<Dto.User>> GetPagedAsync(UserFilterDb userFilterDb);

        //To improve - Create a generic interface
        Task CheckExistenceOfUserAsync(DTO.Requests.User user);
        Task CheckExistenceOfEmailAddressAsync(DTO.Requests.User user);
        Task CheckExistenceOfEmailAddressAsync(DTO.Requests.UserUpdate user);
    }
}

