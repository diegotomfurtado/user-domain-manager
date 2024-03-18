using Data.Repository.Context;
using Data.Repository.Repositories.Interfaces;
using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Data.Repository.Repositories.GenericFilter;
using Dto = Application.DTO.Responses;

namespace Data.Repository.Repositories
{
	public class UserRepository : IUserRepository
    {
		private readonly UserDbContext userDbContext;

		public UserRepository(UserDbContext userDbContext)
		{
            this.userDbContext = userDbContext;
        }

        public async Task CreateUserAsync(User user)
        {
            await userDbContext.Users.AddAsync(user);
            await userDbContext.SaveChangesAsync();
        }

        public async Task<List<User>> GetUserAsync()
        {
            return await userDbContext.Users.ToListAsync();
        }

        public async Task<User> GetUserByCodeAsync(string userCode)
        {
            return await userDbContext.Users.FirstOrDefaultAsync(x => x.UserCode.Equals(userCode));
        }

        public async Task UpdateUserByCodeAsync(User user)
        {
            userDbContext.Users.Update(user);
            await userDbContext.SaveChangesAsync();
        }

        public async Task DeleteUserByCodeAsync(User user)
        {
            userDbContext.Users.Remove(user);
            await userDbContext.SaveChangesAsync();
        }

        public async Task<Dto.PagedBaseResponse<User>> GetPagedAsync(UserFilterDb userFilterDb)
        {
            var user = userDbContext.Users.AsQueryable();

            if (!string.IsNullOrEmpty(userFilterDb.UserCode))
                user = user.Where(x => x.UserCode.Contains(userFilterDb.UserCode));

            if (!string.IsNullOrEmpty(userFilterDb.FirstName))
                user = user.Where(x => x.FirstName.Contains(userFilterDb.FirstName));

            if (!string.IsNullOrEmpty(userFilterDb.LastName))
                user = user.Where(x => x.LastName.Contains(userFilterDb.LastName));

            if (!string.IsNullOrEmpty(userFilterDb.EmailAddress))
                user = user.Where(x => x.EmailAddress.Contains(userFilterDb.EmailAddress));

            return await PagedBaseResponseHelper
                .GetResponseAsync <Dto.PagedBaseResponse <User>, User > (user, userFilterDb);
        }

        public async Task<Boolean> CheckExistenceOfUsersAsync(string userCode)
        {
            var userExists = await userDbContext.Users
                .AnyAsync(x => x.UserCode == userCode);

            return userExists;
        }

        public async Task<bool> CheckExistenceOfEmailAddressAsync(string emailAddress)
        {
            var emailAddressExists = await userDbContext.Users
                .AnyAsync(x => x.EmailAddress == emailAddress);

            return emailAddressExists;
        }
    }
}

