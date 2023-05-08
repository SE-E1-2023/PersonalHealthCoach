using Moq;
using Xunit;
using FluentAssertions;
using HealthCoach.Core.Domain;
using CSharpFunctionalExtensions;
using HealthCoach.Core.Domain.Tests;
using HealthCoach.Shared.Infrastructure;

namespace HealthCoach.Core.Business.Tests;

public sealed class UpdateExerciseLogCommandHandlerTests
{
    private readonly Mock<IRepository> repositoryMock = new();
    private readonly Mock<IExerciseLogRepository> exerciseLogRepositoryMock = new();

    [Fact]
    public void When_UserNotFound_Then_ShouldFail()
    {
        //Arrange
        var command = Command();

        repositoryMock
            .Setup(x => x.Load<User>(command.UserId))
            .ReturnsAsync(Maybe.None);

        //Act
        var result = Sut().Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(BusinessErrors.ExerciseLog.AddExercises.UserNotFound);
    }

    [Fact]
    public void When_DomainSucceeds_Then_ShouldSucceed()
    {
        //Arrange
        var user = UsersFactory.Any();
        var command = Command() with { UserId = user.Id };

        repositoryMock.Setup(x => x.Load<User>(command.UserId)).ReturnsAsync(user);

        //Act
        var result = Sut().Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        //Assert
        result.IsSuccess.Should().BeTrue();

        exerciseLogRepositoryMock.Verify(r => r.Store(user.Id, command.Exercises), Times.Once);
    }

    private UpdateExerciseLogCommand Command() => new(Guid.NewGuid(), new List<string> { "Exercise 1", "Exercise 2" });

    private UpdateExerciseLogCommandHandler Sut() => new(repositoryMock.Object, exerciseLogRepositoryMock.Object);
}