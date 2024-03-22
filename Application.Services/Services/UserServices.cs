
using Application.Services.Services.Interface;
using AutoMapper;
using Data.Repository.Repositories.GenericFilter;
using Data.Repository.Repositories.Interfaces;
using Domain.Model;
using Domain.Model.Validation;
using DtoResponse = Application.DTO.Responses;
using DtoRequest = Application.DTO.Requests;

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

        public async Task CreateUserAsync(DtoRequest.User userDto, string createdBy)
        {
            try
            {
                userDto.ValidateInput();

                if (await userRepository.CheckExistenceOfUsersAsync(userDto.userCode))
                {
                    throw new UserAlreadyExistsException();
                }

                var emailFormatted = userDto.EmailAddress.Replace(" ", "");
                if (await userRepository.CheckExistenceOfEmailAddressAsync(emailFormatted))
                {
                    throw new EmailAddressAlreadyExistsException();
                }

                var date = DateTime.UtcNow;

                var domainUser = mapper.Map<User> (userDto);
                domainUser.CreationTime = date;
                domainUser.CreatedBy = createdBy;
                domainUser.EmailAddress = emailFormatted;

                await userRepository.CreateUserAsync(domainUser);
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

        public async Task<DtoResponse.User> GetUserByCodeAsync(string userCode)
        {
            Domain.Model.User userByCode = await userRepository.GetUserByCodeAsync(userCode);
            
            return mapper.Map<DtoResponse.User>(userByCode);
        }

        public async Task UpdateUserByCodeAsync(string userCode, DtoRequest.UserUpdate userDto, string userName)
        {
            try
            {
                userDto.ValidateInput();

                var userToUpdate = await userRepository.GetUserByCodeAsync(userCode);

                if (userToUpdate == null)
                {
                    throw new UserNotFoundException();
                }

                var emailFormatted = userDto.EmailAddress.Replace(" ", "");
                if (await userRepository.CheckExistenceOfEmailAddressAsync(emailFormatted) && !userToUpdate.EmailAddress.Equals(emailFormatted))
                {
                    throw new EmailAddressAlreadyExistsException();
                }

                var date = DateTime.UtcNow;

                userToUpdate = mapper.Map(userDto, userToUpdate);
                userToUpdate.UpdatedTime = date;
                userToUpdate.UpdatedBy = userName;
                userToUpdate.EmailAddress = emailFormatted;

                await userRepository.UpdateUserByCodeAsync(userToUpdate);

            }
            catch (Exception ex)
            {
                _logger.LogError("[UserService] - Failure to update a user.", ex,
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
                throw new UserNotFoundException();
            }

            await userRepository.DeleteUserByCodeAsync(user);
        }

        public async Task<DtoResponse.PagedBaseResponseDTO<DtoResponse.User>> GetPagedAsync(UserFilterDb userFilterDb)
        {
            var usersPaged = await userRepository.GetPagedAsync(userFilterDb);
            var result = new DtoResponse.PagedBaseResponseDTO<DtoResponse.User>(usersPaged.TotalItems, mapper.Map<List<DtoResponse.User>>(usersPaged.Results));

            return result;
        }

        public async Task CheckExistenceOfUserAsync(string userCode)
        {
            try
            {
                if ((await this.userRepository.CheckExistenceOfUsersAsync(userCode)))
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
                        userCode
                    });
                throw;
            }
        }

        public async Task CheckExistenceOfEmailAddressAsync(string emailAddress)
        {
            try
            {
                if ((await this.userRepository.CheckExistenceOfEmailAddressAsync(emailAddress)))
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
                        emailAddress
                    });
                throw;
            }
        }
    }
}

