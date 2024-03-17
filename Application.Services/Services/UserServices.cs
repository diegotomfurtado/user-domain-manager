using Application.Services.Services.Interface;
using AutoMapper;
using Data.Repository.Repositories.Interfaces;
using Domain.Model;
using Domain.Model.Validation;

namespace Application.Services.Services
{
	public class UserServices : IUserServices
	{

        private readonly IUserRepository userRepository;
        private readonly ILogger<UserServices> _logger;
        private readonly IMapper mapper;

        public UserServices(IUserRepository userRepository,IMapper mapper, ILogger<UserServices> logger)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
            this._logger = logger;
        }

        public async Task CreateUserAsync(DTO.Requests.User userDto, string createdBy)
        {
            try
            {
                userDto.Validate();

                if (await userRepository.GetUserByCodeAsync(userDto.userCode) != null)
                {
                    throw new Exception("The user already exist!");
                }

                var date = DateTime.UtcNow;

                var domainUser = mapper.Map<User> (userDto);
                domainUser.CreationTime = date;
                domainUser.CreatedBy = createdBy;

                await userRepository.CreateProductAsync(domainUser);
            }
            catch (Exception ex)
            {
                _logger.LogError("[UserService] - Failure to create a user.", ex,
                    () => new
                    {
                        userDto.userCode,
                        userDto.FirstName
                    });
                throw;
            }
        }

        public async Task<List<User>> GetUserAsync()
        {
            List<User> users = await userRepository.GetUserAsync();
            return users;
        }

        public async Task<DTO.Responses.User> GetUserByCodeAsync(string userCode)
        {
            User userByCode = await userRepository.GetUserByCodeAsync(userCode);
            
            return mapper.Map<DTO.Responses.User>(userByCode);
        }

        public async Task UpdateUserByCodeAsync(string userCode, DTO.Requests.UserUpdate userDto, string userName)
        {
            try
            {
                userDto.Validate();

                var userToUpdate = await userRepository.GetUserByCodeAsync(userCode);

                if (userToUpdate == null)
                {
                    throw new Exception("The user doesn't exist yet!");
                }

                var date = DateTime.UtcNow;

                userToUpdate = mapper.Map<DTO.Requests.UserUpdate, User>(userDto, userToUpdate);
                userToUpdate.UpdatedTime = date;
                userToUpdate.UpdatedBy = userName;

                await userRepository.UpdateUserByCodeAsync(userToUpdate);

            }
            catch (Exception ex)
            {
                _logger.LogError("[UserService] - Failure to create a user.", ex,
                    () => new
                    {
                        userCode
                    });
                throw;
            }
        }

        public async Task DeleteUserByCodeAsync(string userCode)
        {
            User user = await userRepository.GetUserByCodeAsync(userCode);

            if (user == null)
            {
                throw new Exception("The user doesn't exist yet!");
            }

            await userRepository.DeleteUserByCodeAsync(user);
        }
    }
}

