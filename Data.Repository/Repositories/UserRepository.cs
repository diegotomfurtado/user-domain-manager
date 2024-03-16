using System;
using Data.Repository.Context;
using Data.Repository.Repositories.Interfaces;
using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository.Repositories
{
	public class UserRepository : IUserRepository
    {
		private readonly UserDbContext userDbContext;

		public UserRepository(UserDbContext userDbContext)
		{
            this.userDbContext = userDbContext;
        }

        public async Task CreateProductAsync(User user)
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
            return await userDbContext.Users.FirstOrDefaultAsync(x => x.UserCode == userCode);
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
    }
}

