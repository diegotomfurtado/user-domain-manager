﻿using Application.Services.Core;
using Microsoft.OpenApi.Models;
using Application.Services.Services.Interface;
using Data.Repository.Repositories.Interfaces;
using Application.Services.Services;
using Data.Repository.Repositories;
using Infrastructure;
using Data.Repository.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc(
        "v1",
        new OpenApiInfo { Title = "User Domain Manager ", Version = "v1", Description = "Capgemini Code Challange" });
    c.CustomSchemaIds(s => s.FullName);
});

builder.Services.AddTransient<IApplicationContext, ApplicationContext>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint(
            "/swagger/v1/swagger.json",
            "User Domain Manager");
        c.DefaultModelsExpandDepth(0);
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();