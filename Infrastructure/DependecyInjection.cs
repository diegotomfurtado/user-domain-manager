using Application.Services.Mappers;
using Application.Services.Services;
using Application.Services.Services.Interface;
using Data.Repository.Context;
using Data.Repository.Repositories;
using Data.Repository.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<UserDbContext>(options =>
            {
                var absolutePath = AppDomain.CurrentDomain.BaseDirectory;
                var fatherPath = Directory.GetParent(absolutePath)?.Parent.Parent.Parent.Parent.Parent?.FullName;

                
                new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"{fatherPath}/Data.Repository/conf/appsettings.json")
                .Build();

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

