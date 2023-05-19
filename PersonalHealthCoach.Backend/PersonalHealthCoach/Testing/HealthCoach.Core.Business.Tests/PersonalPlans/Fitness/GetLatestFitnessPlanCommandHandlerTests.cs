using Moq;
using Xunit;
using FluentAssertions;
using HealthCoach.Core.Domain;
using HealthCoach.Core.Domain.Tests;
using HealthCoach.Shared.Infrastructure;

namespace HealthCoach.Core.Business.Tests;

public class GetLatestFitnessPlanCommandHandlerTests
{
    private readonly Mock<IEfQueryProvider> queryProviderMock = new();
    private readonly Mock<IFitnessPlanRepository> fitnessPlanRepositoryMock = new();

    [Fact]
    public void When_FitnessPlanDoesNotExist_Then_ShouldFail()
    {
        //Arrange
        var command = Command();
        queryProviderMock.Setup(x => x.Query<FitnessPlan>()).Returns(new List<FitnessPlan>().AsQueryable());

        //Act
        var result = Sut().Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(BusinessErrors.FitnessPlan.Get.FitnessPlanNotFound);
    }

    [Fact]
    public void When_FitnessPlanDoesExist_Then_ShouldSucceed()
    {
        //Arrange
        var fitnessPlan = PlansFactory.FitnessPlans.Any();
        var command = Command() with { UserId = fitnessPlan.UserId };
        queryProviderMock
            .Setup(x => x.Query<FitnessPlan>())
            .Returns(new List<FitnessPlan> { fitnessPlan }.AsQueryable());

        //Act
        var result = Sut().Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        //Assert
        result.IsSuccess.Should().BeTrue();
    }

    private GetLatestFitnessPlanCommand Command() => new(Guid.NewGuid());

    private GetLatestFitnessPlanCommandHandler Sut() => new(queryProviderMock.Object, fitnessPlanRepositoryMock.Object);
}
