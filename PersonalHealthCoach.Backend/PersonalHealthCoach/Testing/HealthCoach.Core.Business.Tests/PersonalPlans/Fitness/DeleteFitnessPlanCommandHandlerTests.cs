using Moq;
using Xunit;
using FluentAssertions;
using HealthCoach.Shared.Web;
using HealthCoach.Core.Domain;
using HealthCoach.Shared.Core;
using CSharpFunctionalExtensions;
using HealthCoach.Core.Domain.Tests;
using HealthCoach.Shared.Infrastructure;

namespace HealthCoach.Core.Business.Tests;

public class DeleteFitnessPlanCommandHandlerTests
{
    private readonly Mock<IRepository> repositoryMock = new();
    private readonly Mock<IEfQueryProvider> queryProviderMock = new();

    [Fact]
    public void When_FitnessPlan_DoesNotExist_Then_ShouldFail()
    {
        var command = new DeleteFitnessPlanCommand(Guid.NewGuid());
        var commandHandler = new DeleteFitnessPlanCommandHandler(repositoryMock.Object, queryProviderMock.Object);

        queryProviderMock.Setup(x => x.Query<FitnessPlan>()).Returns(new List<FitnessPlan>().AsQueryable());

        var result = commandHandler.Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(BusinessErrors.FitnessPlan.Get.FitnessPlanNotFound);
    }

    [Fact]
    public void When_FitnessPlan_Exists_Then_ShouldSucceed()
    {
        var fitnessPlan = PlansFactory.FitnessPlans.Any();

        var command = new DeleteFitnessPlanCommand(fitnessPlan.Id);
        var commandHandler = new DeleteFitnessPlanCommandHandler(repositoryMock.Object, queryProviderMock.Object);

        queryProviderMock.Setup(x => x.Query<FitnessPlan>()).Returns(new List<FitnessPlan> { fitnessPlan }.AsQueryable());
        repositoryMock.Setup(x => x.Delete(It.IsAny<Exercise>())).Returns(Task.CompletedTask);
        repositoryMock.Setup(x => x.Delete(It.IsAny<FitnessPlan>())).Returns(Task.CompletedTask);

        var result = commandHandler.Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        repositoryMock.Verify(x => x.Delete(It.IsAny<FitnessPlan>()), Times.Once);
        repositoryMock.Verify(x => x.Delete(It.IsAny<Exercise>()));

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(fitnessPlan);
    }
}
