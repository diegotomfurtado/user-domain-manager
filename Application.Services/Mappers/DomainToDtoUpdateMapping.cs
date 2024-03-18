using DomainModel = Domain.Model;
using Dto = Application.DTO.Requests;
using AutoMapper;

namespace Application.Services.Mappers
{
	public class DomainToDtoUpdateMapping : Profile
	{
		public DomainToDtoUpdateMapping()
		{
            CreateMap<Dto.UserUpdate, DomainModel.User>();
        }
	}
}

