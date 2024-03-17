using AutoMapper;
using DomainModel = Domain.Model;
using Application.DTO.Responses;

namespace Application.Services.Mappers
{
	public class DomainToDtoMapping : Profile
	{
		public DomainToDtoMapping()
		{
            CreateMap<DomainModel.User, User>();
        }
	}
}

