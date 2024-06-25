using FluentAssertions;
using FluentResults;
using LibraryManagement.API.Controllers;
using LibraryManagement.Application.Commands.Users;
using LibraryManagement.Application.Queries.Users;
using LibraryManagement.Application.ViewModels;
using LibraryManagement.Test.Fixtures;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace LibraryManagement.Test.API.Controllers
{
    public class UserControllerTest
    {
        private readonly Mock<IMediator> _mediatorMock = new();

        [Fact]
        public async Task Should_Create_User_With_Success()
        {
            // Arrange
            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Ok());

            var controller = new UserController(_mediatorMock.Object);

            // Action
            var result = await controller.CreateUser(new());

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<CreatedAtActionResult>();
        }

        [Fact]
        public async Task Should_Fail_To_Create_Invalid_User()
        {
            // Arrange
            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Fail("Fail"));

            var controller = new UserController(_mediatorMock.Object);

            // Action
            var result = await controller.CreateUser(new());

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task Should_Updte_User_With_Success()
        {
            // Arrange
            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateUserCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Ok());

            var controller = new UserController(_mediatorMock.Object);

            // Action
            var result = await controller.UpdateUser(new());

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task Should_Fail_To_Updte_Invalid_User()
        {
            // Arrange
            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateUserCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Fail("Fail"));

            var controller = new UserController(_mediatorMock.Object);

            // Action
            var result = await controller.UpdateUser(new());

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task Should_Return_User_By_Id()
        {
            // Arrange
            var user = DataGenerator.GetUserViewModel();
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetUserQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Ok(user));

            var controller = new UserController(_mediatorMock.Object);

            // Action
            var result = await controller.GetUserById(Guid.NewGuid());

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task Should_Return_All_Users()
        {
            // Arrange
            var users = new List<UserViewModel> { DataGenerator.GetUserViewModel() };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllUsersQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Ok(users));

            var controller = new UserController(_mediatorMock.Object);

            // Action
            var result = await controller.GetAllUsers(string.Empty, new());

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task Should_Login_With_Valid_User()
        {
            // Arrange
            var loginUser = DataGenerator.GetLoginUserViewModel();
            _mediatorMock.Setup(m => m.Send(It.IsAny<LoginUserCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Ok(loginUser));

            var controller = new UserController(_mediatorMock.Object);

            // Action
            var result = await controller.Login(new());

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task Should_Delete_User()
        {
            // Arrange
            _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteUserCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Ok());

            var controller = new UserController(_mediatorMock.Object);

            // Action
            var result = await controller.DeleteUser(Guid.NewGuid());

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NoContentResult>();
        }
    }
}
