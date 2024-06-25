using FluentAssertions;
using LibraryManagement.Application.Commands.Users;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces;
using LibraryManagement.Core.Repositories;
using LibraryManagement.Test.Fixtures;
using Moq;

namespace LibraryManagement.Test.Application.Commands.Users
{
    public class UpdateUserCommandHandlerTest
    {
        private readonly Mock<IAuthService> _authServiceMock = new();
        private readonly Mock<IUserRepository> _useRepositoryMock = new();

        [Fact]
        public async Task Should_Update_User_with_Success()
        {
            // Arrange
            var user = UserDataGenerator.GetUserFake();
            var command = UserDataGenerator.UpdateUserCommandFake();

            _authServiceMock.Setup(m => m.GenerateSha256Hash(It.IsAny<string>()))
                .Returns(command.Password);

            _useRepositoryMock.Setup(m => m.GetOneAsync(It.IsAny<Guid>()))
                .ReturnsAsync(user);

            var handler = new UpdateUserCommandHandler(_useRepositoryMock.Object, _authServiceMock.Object);

            // Act
            var result = await handler.Handle(command, new());

            // Assert
            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async Task Should_Fail_To_Update_Invalid_User()
        {
            // Arrange
            var command = UserDataGenerator.UpdateUserCommandFake();

            _authServiceMock.Setup(m => m.GenerateSha256Hash(It.IsAny<string>()))
                .Returns(command.Password);

            _useRepositoryMock.Setup(m => m.GetOneAsync(It.IsAny<Guid>()))
                .ReturnsAsync((User)null!);

            var handler = new UpdateUserCommandHandler(_useRepositoryMock.Object, _authServiceMock.Object);

            // Act
            var result = await handler.Handle(command, new());

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Errors.Should().HaveCount(1)
                .And.ContainSingle(x => x.Message.Equals("User not found"));
        }
    }
}
