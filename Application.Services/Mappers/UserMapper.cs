using DomainModel = Domain.Model;

namespace Application.Services.Mappers
{
	public static class UserMapper
	{
        public static DTO.Responses.User? ToDTO(this DomainModel.User user)
        {

            if (user == null)
            {
                return null;
            }

            return new DTO.Responses.User()
            {
                FullName = user.FirstName + " " + user.LastName,
                emailAddress = user.emailAddress,
                NotesField = user.NotesField,
                UserCode = user.UserCode,
                CreationTime = user.CreationTime,
                UpdatedBy = user.UpdatedBy,
                UpdatedTime = user.UpdatedTime
            };
        }
    }
}

