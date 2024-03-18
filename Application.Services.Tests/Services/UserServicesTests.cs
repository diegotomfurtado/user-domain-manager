

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
            Assert.True(resultUserDto != null, "The result should not be null");
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
            var createdBy = this._fixture.Create<string>();
            var userDto = this._fixture.Build<DTO.Requests.User>()
                .With(x => x.FirstName, "Diego")
                .With(x => x.LastName, "Furtado")
                .With(x => x.emailAddress, "diegotomfurtado@gmail.com")
                .Create();

            this._userRepositoryMock.Setup(
                s => s.CheckExistenceOfUsersAsync(userDto.emailAddress))
                .ReturnsAsync(false)
                .Verifiable();

            this._userRepositoryMock.Setup(
                s => s.CheckExistenceOfEmailAddressAsync(userDto.emailAddress))
                .ReturnsAsync(false)
                .Verifiable();

            this._userRepositoryMock.Setup(
                s => s.CreateUserAsync(It.IsAny<User>()))
                .Verifiable();

            // Act
            await this._userServices.CreateUserAsync(userDto, createdBy);

            // Assert
            this._userRepositoryMock.Verify(
                v => v.CreateUserAsync(It.IsAny<User>()), Times.Once);

            this._userRepositoryMock.Verify(
                v => v.CreateUserAsync(
                    It.Is<User>(
                        x => x.UserCode == userDto.userCode &&
                             x.FirstName == userDto.FirstName &&
                             x.LastName == userDto.LastName &&
                             x.EmailAddress == userDto.emailAddress &&
                             x.NotesField == userDto.NotesField)),
                Times.Once);
        }

        [Fact]
        public async Task CreateUserAsync_ShouldNotCreateNullObject_Fail()
        {
            // Arrange
            var createdBy = this._fixture.Create<string>();
            DTO.Requests.User userDto = null;

            this._userRepositoryMock.Setup(
                s => s.CheckExistenceOfUsersAsync(userDto.emailAddress))
                .ReturnsAsync(false)
                .Verifiable();

            this._userRepositoryMock.Setup(
                s => s.CheckExistenceOfEmailAddressAsync(userDto.emailAddress))
                .ReturnsAsync(false)
                .Verifiable();

            this._userRepositoryMock.Setup(
                s => s.CreateUserAsync(It.IsAny<User>()))
                .Verifiable();

            // Act
            var exception = await Assert.ThrowsAsync<AggregateException>(async () =>
            {
                await this._userServices.CreateUserAsync(userDto, createdBy);
            });

            // Assert
            Assert.Equal("One or more errors occurred. (Objects should be filled.)", exception.Message);
        }

        [Fact]
        public async Task UpdateUserAsync_ShouldUpdate()
        {
            // Arrange
            var name = this._fixture.Create<string>();
            var userCode = this._fixture.Create<string>();
            var userModel = this._fixture.Create<User>();
            var userDto = this._fixture.Build<DTO.Requests.UserUpdate>()
                .With(x => x.FirstName, "Diego")
                .With(x => x.LastName, "Furtado")
                .With(x => x.EmailAddress, "diegotomfurtado@gmail.com")
                .Create();

            this._userRepositoryMock.Setup(
                s => s.GetUserByCodeAsync(userCode))
                .ReturnsAsync(userModel)
                .Verifiable();

            this._userRepositoryMock.Setup(
                s => s.CheckExistenceOfEmailAddressAsync(userDto.EmailAddress))
                .ReturnsAsync(false)
                .Verifiable();

            this._userRepositoryMock.Setup(
                s => s.UpdateUserByCodeAsync(It.IsAny<User>()))
                .Verifiable();

            // Act
            await this._userServices.UpdateUserByCodeAsync(userCode, userDto, name);

            // Assert
            this._userRepositoryMock.Verify(
                v => v.UpdateUserByCodeAsync(It.IsAny<User>()), Times.Once);

            this._userRepositoryMock.Verify(
                v => v.UpdateUserByCodeAsync(
                    It.Is<User>(
                        x => x.FirstName == userDto.FirstName &&
                             x.LastName == userDto.LastName &&
                             x.EmailAddress == userDto.EmailAddress &&
                             x.NotesField == userDto.NotesField)),
                Times.Once);
        }

        [Fact]
        public async Task DeleteUserAsync_ShouldDeleteUser()
        {
            // Arrange
            var userCode = this._fixture.Create<string>();
            var userModel = this._fixture.Create<User>();

            this._userRepositoryMock.Setup(
                s => s.GetUserByCodeAsync(userCode))
                .ReturnsAsync(userModel)
                .Verifiable();

            // Act
            await this._userServices.DeleteUserByCodeAsync(userCode);

            // Assert
            this._userRepositoryMock.Verify(
                v => v.DeleteUserByCodeAsync(It.IsAny<User>()), Times.Once);

            this._userRepositoryMock.VerifyAll();
            this._userRepositoryMock.VerifyNoOtherCalls();
        }
    }
}

