using System;
using Application.Services.Mappers;
using AutoMapper;

namespace Application.Services.Tests
{
    public class BaseTest
    {
        public IMapper GetMapper()
        {
            var config = new MapperConfiguration(op =>
            {
                op.AddProfile<DomainToDtoMapping>();
                op.AddProfile<DtoToDomainMapping>();
                op.AddProfile<DomainToDtoUpdateMapping>();
            });

            return config.CreateMapper();
        }
    }
}

