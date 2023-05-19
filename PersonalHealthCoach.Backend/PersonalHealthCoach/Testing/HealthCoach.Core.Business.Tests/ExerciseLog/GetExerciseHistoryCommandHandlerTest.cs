using Moq;
using Xunit;
using FluentAssertions;
using HealthCoach.Core.Domain;
using HealthCoach.Shared.Infrastructure;
using HealthCoach.Core.Domain.Tests;

namespace HealthCoach.Core.Business.Tests;
public class GetExerciseHistoryCommandHandlerTest
{
    private readonly Mock<IEfQueryProvider> queryProviderMock = new();

    [Fact]
    public void When_LogIsEmpty_Then_ShouldFail()
    {
        //Arrange
        
        var command = new GetExerciseHistoryCommand(Guid.NewGuid());
        queryProviderMock
            .Setup(x => x.Query<ExerciseHistory>())
            .Returns(new List<ExerciseHistory>().AsQueryable());

        //Act
        var result = Sut().Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(BusinessErrors.ExerciseHistory.GetExercices.LogIsEmpty);
    }

    [Fact]
    public void When_ExerciseHistoryDoesExist_Then_ShouldSucceed()
    {
        //Arrange
        var userId = Guid.NewGuid();
        var history = ExerciseHistoryFactory.WithId(userId);
        var command = new GetExerciseHistoryCommand(userId);
        queryProviderMock
            .Setup(x => x.Query<ExerciseHistory>())
            .Returns(new List<ExerciseHistory> { history }.AsQueryable());

        //Act
        var result = Sut().Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Should().Be(history);
    }

    private GetExerciseHistoryCommandHandler Sut() => new(
        queryProviderMock.Object
    );
}

