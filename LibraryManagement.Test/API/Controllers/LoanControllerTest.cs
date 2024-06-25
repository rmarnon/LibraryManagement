using FluentAssertions;
using FluentResults;
using LibraryManagement.API.Controllers;
using LibraryManagement.Application.Commands.Loans;
using LibraryManagement.Application.Queries.Loans;
using LibraryManagement.Application.ViewModels;
using LibraryManagement.Test.Fixtures;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace LibraryManagement.Test.API.Controllers
{
    public class LoanControllerTest
    {
        private readonly Mock<IMediator> _mediatorMock = new();

        [Fact]
        public async Task Should_Create_Loan_With_Success()
        {
            // Arrange
            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateLoanCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Ok());

            var controller = new LoanController(_mediatorMock.Object);

            // Action
            var result = await controller.CreateLoan(new());

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<CreatedAtActionResult>();
        }

        [Fact]
        public async Task Should_Fail_To_Create_Invalid_Loan()
        {
            // Arrange
            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateLoanCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Fail("Fail"));

            var controller = new LoanController(_mediatorMock.Object);

            // Action
            var result = await controller.CreateLoan(new());

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task Should_Updte_Loan_With_Success()
        {
            // Arrange
            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateLoanCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Ok());

            var controller = new LoanController(_mediatorMock.Object);

            // Action
            var result = await controller.UpdateLoan(new());

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task Should_Fail_To_Updte_Invalid_Loan()
        {
            // Arrange
            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateLoanCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Fail("Fail"));

            var controller = new LoanController(_mediatorMock.Object);

            // Action
            var result = await controller.UpdateLoan(new());

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task Should_Return_Loan_By_Id()
        {
            // Arrange
            var Loan = DataGenerator.GetLoanViewModel();
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetLoanQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Ok(Loan));

            var controller = new LoanController(_mediatorMock.Object);

            // Action
            var result = await controller.GetLoanById(Guid.NewGuid());

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task Should_Return_All_Loans()
        {
            // Arrange
            var Loans = new List<LoanViewModel> { DataGenerator.GetLoanViewModel() };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllLoansQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Ok(Loans));

            var controller = new LoanController(_mediatorMock.Object);

            // Action
            var result = await controller.GetAllLoans(string.Empty, new());

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task Should_Delete_Loan()
        {
            // Arrange
            _mediatorMock.Setup(m => m.Send(It.IsAny<ReturnLoanCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Ok());

            var controller = new LoanController(_mediatorMock.Object);

            // Action
            var result = await controller.ReturnLoan(new());

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NoContentResult>();
        }
    }
}
