

using Application.Services.Services;
using AutoFixture;
using AutoMapper;
using Data.Repository.Repositories.Interfaces;
using Domain.Model;
using Microsoft.Extensions.Logging;
using Moq;
using Dto = Application.DTO.Responses;

namespace Application.Services.Tests.Services
{
    public class UserServicesTests
    {

        private readonly Fixture _fixture;
        private readonly UserServices _userServices;
        private readonly Mock<IUserRepository> _userRepositoryMock;

        private readonly Mock<ILogger<UserServices>> _logger;
        private readonly Mock<IMapper> _mapper;

        public UserServicesTests()
        {
            this._fixture = new Fixture();
            this._userRepositoryMock = new Mock<IUserRepository>();
            this._mapper = new Mock<IMapper>();
            this._logger = new Mock<ILogger<UserServices>>();

            this._userServices = new UserServices(
                this._userRepositoryMock.Object,
                new BaseTest().GetMapper(),
                this._logger.Object);
        }

        [Fact]
        public async Task GetUserAsync_ShouldReturnUser()
        {
            // Arrange
            var userCode = this._fixture.Create<string>();
            var userModel = this._fixture.Create<User>();

            this._userRepositoryMock.Setup(
                s => s.GetUserByCodeAsync(userCode))
                .ReturnsAsync(userModel)
                .Verifiable();

            // Act
            var resultUserDto = await this._userServices.GetUserByCodeAsync(userCode);

            // Assert
            Assert.True(resultUserDto != null, "O resultado não deveria ser nulo.");
            Assert.Equal(resultUserDto.UserCode,userModel.UserCode);
            Assert.Equal(resultUserDto.FirstName, userModel.FirstName);
            Assert.Equal(resultUserDto.LastName, userModel.LastName);
            Assert.Equal(resultUserDto.emailAddress, userModel.EmailAddress);
            Assert.Equal(resultUserDto.NotesField, userModel.NotesField);
            Assert.Equal(resultUserDto.CreatedBy, userModel.CreatedBy);
            Assert.Equal(resultUserDto.CreationTime, userModel.CreationTime);
            Assert.Equal(resultUserDto.UpdatedBy, userModel.UpdatedBy);
            Assert.Equal(resultUserDto.UpdatedTime, userModel.UpdatedTime);

            this._userRepositoryMock.VerifyAll();
            this._userRepositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task CreateUserAsync_ShouldCreate()
        {
            // Arrange

            //DTO.Requests.User userDto, string createdBy
            var createdBy = this._fixture.Create<string>();
            var userDto = this._fixture.Create<DTO.Requests.User>();

            this._userRepositoryMock.Setup(
                s => s.CheckExistenceOfUsersAsync(userDto))
                .ReturnsAsync(false)
                .Verifiable();

            this._userRepositoryMock.Setup(
                s => s.CheckExistenceOfEmailAddressAsync(userDto))
                .ReturnsAsync(false)
                .Verifiable();

            //Parei aqui - mockei os valores acima para FALSE - significa que nao possui no banco para poder cadastrar



            // Act
            var resultUserDto = await this._userServices.GetUserByCodeAsync(userCode);

            // Assert
            Assert.True(resultUserDto != null, "O resultado não deveria ser nulo.");
            Assert.Equal(resultUserDto.UserCode, userModel.UserCode);
            Assert.Equal(resultUserDto.FirstName, userModel.FirstName);
            Assert.Equal(resultUserDto.LastName, userModel.LastName);
            Assert.Equal(resultUserDto.emailAddress, userModel.EmailAddress);
            Assert.Equal(resultUserDto.NotesField, userModel.NotesField);
            Assert.Equal(resultUserDto.CreatedBy, userModel.CreatedBy);
            Assert.Equal(resultUserDto.CreationTime, userModel.CreationTime);
            Assert.Equal(resultUserDto.UpdatedBy, userModel.UpdatedBy);
            Assert.Equal(resultUserDto.UpdatedTime, userModel.UpdatedTime);

            this._userRepositoryMock.VerifyAll();
            this._userRepositoryMock.VerifyNoOtherCalls();
        }
    }
}

