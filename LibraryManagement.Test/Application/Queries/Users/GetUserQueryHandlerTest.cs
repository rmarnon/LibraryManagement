using FluentAssertions;
using LibraryManagement.Application.Queries.Users;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Repositories;
using LibraryManagement.Test.Fixtures;
using Moq;

namespace LibraryManagement.Test.Application.Queries.Users
{
    public class GetUserQueryHandlerTest
    {
        private readonly Mock<IUserRepository> _userRepositoryMock = new();

        [Fact]
        public async Task Should_Return_User_By_Id()
        {
            // Arrange
            var query = UserDataGenerator.GetUserQueryFake();
            var user = UserDataGenerator.GetUserFake();

            _userRepositoryMock.Setup(x => x.GetOneAsync(It.IsAny<Guid>())).ReturnsAsync(user);

            var handler = new GetUserQueryHandler(_userRepositoryMock.Object);

            // Act
            var result = await handler.Handle(query, new());

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Name.Should().Be(user.Name);
            result.Value.Email.Should().Be(user.Email);
        }

        [Fact]
        public async Task Should_Cant_Find_User()
        {
            // Arrange
            var query = UserDataGenerator.GetUserQueryFake();

            _userRepositoryMock.Setup(x => x.GetOneAsync(It.IsAny<Guid>())).ReturnsAsync((User)null!);

            var handler = new GetUserQueryHandler(_userRepositoryMock.Object);

            // Act
            var result = await handler.Handle(query, new());

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Errors.Should().HaveCount(1)
                .And.ContainSingle(x => x.Message.Equals("User not found"));
        }
    }
}
