using CSharpFunctionalExtensions;
using HealthCoach.Core.Business;
using HealthCoach.Core.Domain;
using HealthCoach.Functions.Isolated;
using MediatR;
using Moq;
using Xunit;

namespace HealthCoach.Presentation.Tests;

public class UserFunctionsTests
{
    private readonly Mock<IMediator> mediatorMock = new();

    [Fact]
    public void Given_CreateUser_When_BusinessFails_Then_Should400()
    {
        // Arrange
        var command = Create();
        mediatorMock.Setup(m => m.Send(It.IsAny<CreateUserCommand>(), default)).ReturnsAsync(Result<User>);

        // Act

        // Assert
    }

    private CreateUserCommand Create() => new("Name", "First Name", "mail@test.com");

    private UserFunctions Sut() => new(mediatorMock.Object);
}