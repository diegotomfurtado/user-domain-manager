using DomainModel = Domain.Model;
using Dto = Application.DTO.Requests;
using AutoMapper;

namespace Application.Services.Mappers
{
	public class DtoToDomainUpdateMapping : Profile
	{

		public DtoToDomainUpdateMapping()
		{
            CreateMap<Dto.UserUpdate, DomainModel.User>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null)); ;
        }
	}
}

