
using AutoMapper;
using DomainModel = Domain.Model;
using Application.DTO.Requests;

namespace Application.Services.Mappers
{
	public class DtoToDomainMapping : Profile
	{
		public DtoToDomainMapping()
		{
			CreateMap<User, DomainModel.User>();
		}
	}
}

