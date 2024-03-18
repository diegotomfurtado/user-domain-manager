using AutoMapper;
using DomainModel = Domain.Model;
using Dto = Application.DTO.Responses;

namespace Application.Services.Mappers
{
	public class DomainToDtoMapping : Profile
	{
		public DomainToDtoMapping()
		{
            CreateMap<DomainModel.User, Dto.User>();
        }
	}
}

