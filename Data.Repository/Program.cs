using Data.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddEntityFrameworkSqlServer()
            .AddDbContext<UserDbContext>(
            option => option.UseSqlServer(builder.Configuration.GetConnectionString("DataBase"))
        );

        var app = builder.Build();
        app.Run();
    }
}