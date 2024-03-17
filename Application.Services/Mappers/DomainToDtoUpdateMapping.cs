using DomainModel = Domain.Model;
using Dto = Application.DTO.Requests;
using AutoMapper;

namespace Application.Services.Mappers
{
	public class DomainToDtoUpdateMapping : Profile
	{
		public DomainToDtoUpdateMapping()
		{
            CreateMap<DomainModel.User, Dto.UserUpdate>();
        }
	}
}

