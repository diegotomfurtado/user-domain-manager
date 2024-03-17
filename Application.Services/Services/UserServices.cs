
using Application.Services.Services.Interface;
using AutoMapper;
using Data.Repository.Repositories.GenericFilter;
using Data.Repository.Repositories.Interfaces;
using Domain.Model;
using Domain.Model.Validation;
using Dto = Application.DTO.Responses;

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
                userDto.ValidateInput();

                if (await userRepository.CheckExistenceOfUsersAsync(userDto))
                {
                    throw new UserAlreadyExistsException();
                }

                if (await userRepository.CheckExistenceOfEmailAddressAsync(userDto))
                {
                    throw new EmailAddressAlreadyExistsException();
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

        public async Task<Dto.User> GetUserByCodeAsync(string userCode)
        {
            User userByCode = await userRepository.GetUserByCodeAsync(userCode);
            
            return mapper.Map<Dto.User>(userByCode);
        }

        public async Task UpdateUserByCodeAsync(string userCode, DTO.Requests.UserUpdate userDto, string userName)
        {
            try
            {
                userDto.ValidateInput();

                var userToUpdate = await userRepository.GetUserByCodeAsync(userCode);

                if (userToUpdate == null)
                {
                    throw new UserNotFoundException();
                }

                if (await userRepository.CheckExistenceOfEmailAddressAsync(userDto))
                {
                    throw new EmailAddressAlreadyExistsException();
                }

                var date = DateTime.UtcNow;

                userToUpdate = mapper.Map(userDto, userToUpdate);
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

        public async Task<Dto.PagedBaseResponseDTO<Dto.User>> GetPagedAsync(UserFilterDb userFilterDb)
        {
            var usersPaged = await userRepository.GetPagedAsync(userFilterDb);
            var result = new Dto.PagedBaseResponseDTO<Dto.User>(usersPaged.TotalItems, mapper.Map<List<Dto.User>>(usersPaged.Results));

            return result;
        }

        public async Task CheckExistenceOfUserAsync(DTO.Requests.User user)
        {
            try
            {
                if ((await this.userRepository.CheckExistenceOfUsersAsync(user)))
                {
                    throw new UserAlreadyExistsException();
                }
            }
            catch (UserAlreadyExistsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    "[UserService] - Failed to check existence of users.",
                    ex,
                    () => new
                    {
                        user
                    });
                throw;
            }
        }

        public async Task CheckExistenceOfEmailAddressAsync(DTO.Requests.User user)
        {
            try
            {
                if ((await this.userRepository.CheckExistenceOfEmailAddressAsync(user)))
                {
                    throw new EmailAddressAlreadyExistsException();
                }
            }
            catch (EmailAddressAlreadyExistsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    "[UserService] - Failed to check existence of users.",
                    ex,
                    () => new
                    {
                        user
                    });
                throw;
            }
        }

        public async Task CheckExistenceOfEmailAddressAsync(DTO.Requests.UserUpdate user)
        {
            try
            {
                if ((await this.userRepository.CheckExistenceOfEmailAddressAsync(user)))
                {
                    throw new EmailAddressAlreadyExistsException();
                }
            }
            catch (EmailAddressAlreadyExistsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    "[UserService] - Failed to check existence of users.",
                    ex,
                    () => new
                    {
                        user
                    });
                throw;
            }
        }
    }
}

