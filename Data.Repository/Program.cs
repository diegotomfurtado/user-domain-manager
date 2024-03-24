using Data.Repository.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//It will be used to process migration
builder.Services.AddDbContext<UserDbContext>(
    option => option.UseSqlite(builder.Configuration.GetConnectionString("DataBase"))
);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();

