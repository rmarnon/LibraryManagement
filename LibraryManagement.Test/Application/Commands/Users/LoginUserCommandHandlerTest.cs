using FluentAssertions;
using LibraryManagement.Application.Commands.Users;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Enums;
using LibraryManagement.Core.Interfaces;
using LibraryManagement.Core.Repositories;
using LibraryManagement.Test.Fixtures;
using Moq;

namespace LibraryManagement.Test.Application.Commands.Users
{
    public class LoginUserCommandHandlerTest
    {
        private readonly Mock<IAuthService> _authServiceMock = new();
        private readonly Mock<IUserRepository> _useRepositoryMock = new();

        [Fact]
        public async Task Should_Login_User_with_Success()
        {
            // Arrange
            var user = UserDataGenerator.GetUserFake();
            var command = UserDataGenerator.LoginUserCommandFake();

            _authServiceMock.Setup(m => m.GenerateSha256Hash(It.IsAny<string>()))
                .Returns(command.Password);

            _authServiceMock.Setup(m => m.GenerateJwtToken(It.IsAny<string>(), It.IsAny<Role>()))
                .Returns(string.Empty);

            _useRepositoryMock.Setup(m => m.GetUserByEmailAndPasswordHashAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(user);

            var handler = new LoginUserCommandHandler(_useRepositoryMock.Object, _authServiceMock.Object);

            // Act
            var result = await handler.Handle(command, new());

            // Assert
            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async Task Should_Fail_To_Login_Invalid_User()
        {
            // Arrange
            var command = UserDataGenerator.LoginUserCommandFake();

            _authServiceMock.Setup(m => m.GenerateSha256Hash(It.IsAny<string>()))
                .Returns(command.Password);

            _useRepositoryMock.Setup(m => m.GetOneAsync(It.IsAny<Guid>()))
                .ReturnsAsync((User)null!);

            var handler = new LoginUserCommandHandler(_useRepositoryMock.Object, _authServiceMock.Object);

            // Act
            var result = await handler.Handle(command, new());

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Errors.Should().HaveCount(1)
                .And.ContainSingle(x => x.Message.Equals("User email not found"));
        }
    }
}
