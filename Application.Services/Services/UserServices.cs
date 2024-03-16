using System;
using Application.Services.Mappers;
using Application.Services.Services.Interface;
using Data.Repository.Repositories.Interfaces;
using Domain.Model;

namespace Application.Services.Services
{
	public class UserServices : IUserServices
	{

        private readonly IUserRepository userRepository;

        public UserServices(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task CreateUserAsync(DTO.Requests.User user, string createdBy)
        {

            if (await userRepository.GetUserByCodeAsync(user.userCode) != null)
            {
                throw new Exception("The user already exist!");
            }

            var date = DateTime.UtcNow;

            var domainUser = new User()
            {
                 FirstName = user.FirstName,
                 LastName = user.LastName,
                 emailAddress = user.emailAddress,
                 NotesField = user.NotesField,
                 CreationTime = date,
                 UserCode = user.userCode.Trim(),
                 CreatedBy = createdBy
            };

            await userRepository.CreateProductAsync(domainUser);
        }

        public async Task<List<User>> GetUserAsync()
        {
            List<User> users = await userRepository.GetUserAsync();
            return users;
        }

        public async Task<DTO.Responses.User> GetUserByCodeAsync(string userCode)
        {
            var userByCode = await userRepository.GetUserByCodeAsync(userCode);
            return userByCode.ToDTO();
        }

        public async Task UpdateUserByCodeAsync(string userCode, DTO.Requests.UserUpdate user, string userName)
        {
            User userUpdate = await userRepository.GetUserByCodeAsync(userCode);

            if (userUpdate == null)
            {
                throw new Exception("The user doesn't exist yet!");
            }

            var date = DateTime.UtcNow;

            userUpdate.FirstName = user.FirstName;
            userUpdate.LastName = user.LastName;
            userUpdate.emailAddress = user.emailAddress;
            userUpdate.NotesField = user.NotesField;
            userUpdate.UpdatedBy = userName;
            userUpdate.UpdatedTime = date;

            await userRepository.UpdateUserByCodeAsync(userUpdate);
        }

        public async Task DeleteUserByCodeAsync(string userCode)
        {
            User user = await userRepository.GetUserByCodeAsync(userCode);

            if (user == null)
            {
                throw new Exception("The product doesn't exist yet!");
            }

            await userRepository.DeleteUserByCodeAsync(user);
        }
    }
}

