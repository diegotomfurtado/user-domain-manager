using Application.Services.Mappers;
using Application.Services.Services;
using Application.Services.Services.Interface;
using Data.Repository.Context;
using Data.Repository.Repositories;
using Data.Repository.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<UserDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DataBase"));
            });

            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(DomainToDtoMapping));
            services.AddAutoMapper(typeof(DtoToDomainMapping));
            services.AddAutoMapper(typeof(DtoToDomainUpdateMapping));

            services.AddScoped<IUserServices, UserServices>();
            return services;

        }
    }
}

