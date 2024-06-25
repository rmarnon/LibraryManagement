using FluentAssertions;
using LibraryManagement.Application.Commands.Users;
using LibraryManagement.Core.Interfaces;
using LibraryManagement.Core.Repositories;
using LibraryManagement.Test.Fixtures;
using Moq;

namespace LibraryManagement.Test.Application.Commands.Users
{
    public class CreateUserCommandHandlerTest
    {
        private readonly Mock<IAuthService> _authServiceMock = new();
        private readonly Mock<IUserRepository> _useRepositoryMock = new();

        [Fact]
        public async Task Should_Create_User_with_Success()
        {
            // Arrange
            var command = DataGenerator.CreateUserCommandFake();
            _authServiceMock.Setup(m => m.GenerateSha256Hash(It.IsAny<string>()))
                .Returns(command.Password);

            _useRepositoryMock.Setup(m => m.CheckEmailExsistsAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            var handler = new CreateUserCommandHandler(_useRepositoryMock.Object, _authServiceMock.Object);

            // Act
            var result = await handler.Handle(command, new());

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Name.Should().Be(command.Name);
            result.Value.Email.Should().Be(command.Email);
        }

        [Fact]
        public async Task Should_Failt_To_Create_User()
        {
            // Arrange
            var command = DataGenerator.CreateUserCommandFake();
            _authServiceMock.Setup(m => m.GenerateSha256Hash(It.IsAny<string>()))
                .Returns(string.Empty);

            _useRepositoryMock.Setup(m => m.CheckEmailExsistsAsync(It.IsAny<string>()))
                .ReturnsAsync(true);

            var handler = new CreateUserCommandHandler(_useRepositoryMock.Object, _authServiceMock.Object);

            // Act
            var result = await handler.Handle(command, new());

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Errors.Should().HaveCount(1)
                .And.ContainSingle(x => x.Message.Equals("Email alredy exists"));
        }
    }
}
