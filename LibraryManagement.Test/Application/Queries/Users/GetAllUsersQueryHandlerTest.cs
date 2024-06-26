using FluentAssertions;
using LibraryManagement.Application.Queries.Users;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Repositories;
using LibraryManagement.Test.Fixtures;
using Moq;

namespace LibraryManagement.Test.Application.Queries.Users
{
    public class GetAllUsersQueryHandlerTest
    {
        private readonly Mock<IUserRepository> _userRepositoryMock = new();

        [Fact]
        public async Task Should_Return_All_Users()
        {
            // Arrange
            var query = UserDataGenerator.GetAllUsersQueryFake();
            var user = UserDataGenerator.GetUserFake();
            var users = new List<User> { user, user, user };

            _userRepositoryMock.Setup(x => x.GetAllAsync(query.Pagination)).ReturnsAsync(users);

            var handler = new GetAllUsersQueryHandler(_userRepositoryMock.Object);

            // Act
            var result = await handler.Handle(query, new());

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().HaveCount(3);
        }

        [Fact]
        public async Task Should_Return_Empty_Users_List()
        {
            // Arrange
            var query = UserDataGenerator.GetAllUsersQueryFake();

            _userRepositoryMock.Setup(x => x.GetAllAsync(query.Pagination)).ReturnsAsync((List<User>)null!);

            var handler = new GetAllUsersQueryHandler(_userRepositoryMock.Object);

            // Act
            var result = await handler.Handle(query, new());

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Errors.Should().HaveCount(1)
                .And.ContainSingle(x => x.Message.Equals("Empty user list"));
        }
    }
}
