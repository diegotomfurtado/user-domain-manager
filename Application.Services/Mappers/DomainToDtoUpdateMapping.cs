using DomainModel = Domain.Model;
using Application.DTO.Requests;
using AutoMapper;

namespace Application.Services.Mappers
{
	public class DomainToDtoUpdateMapping : Profile
	{
		public DomainToDtoUpdateMapping()
		{
            CreateMap<UserUpdate, DomainModel.User>();
        }
	}
}

