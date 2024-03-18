using AutoMapper;
using DomainModel = Domain.Model;
using Dto = Application.DTO.Requests;

namespace Application.Services.Mappers
{
	public class DtoToDomainMapping : Profile
	{
		public DtoToDomainMapping()
		{
			CreateMap<Dto.User, DomainModel.User>();
		}
	}
}

