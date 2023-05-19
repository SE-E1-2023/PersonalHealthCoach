using CSharpFunctionalExtensions;
using Moq;
using Xunit;
using FluentAssertions;
using HealthCoach.Core.Domain;
using HealthCoach.Core.Domain.Tests;
using HealthCoach.Shared.Infrastructure;

namespace HealthCoach.Core.Business.Tests;
public class GetDietPlanCommandHandlerTests
{
    private readonly Mock<IEfQueryProvider> queryProviderMock = new();
    private readonly Mock<IRepository> repositoryMock = new();
    [Fact]
    public void When_UserIsNotFound_Then_ShouldFail()
    {
        //Arrange
        
        var command = new GetDietPlanCommand(Guid.NewGuid());

        queryProviderMock
            .Setup(x => x.Query<DietPlan>())
            .Returns(new List<DietPlan>().AsQueryable());

        //Act
        var result = Sut().Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(BusinessErrors.DietPlan.Get.UserNotFound);

    }

    [Fact]
    public void When_DietPlanDoesNotExist_Then_ShouldFail()
    {
        var command = new GetDietPlanCommand(Guid.NewGuid());
        var user = UsersFactory.Any();

        repositoryMock.Setup(r => r.Load<User>(command.UserId)).ReturnsAsync(user);
        queryProviderMock.Setup(x => x.Query<DietPlan>()).Returns(new List<DietPlan>().AsQueryable());


        var result = Sut().Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(BusinessErrors.DietPlan.Get.DietPlanDoesNotExist);
    }

    [Fact]
    public void When_DietPlanDoesExist_Then_ShouldSucced()
    {
        //Arrange
        var userId = Guid.NewGuid();
        var dietPlan = PlansFactory.DietPlans.Any();

        var command = new GetDietPlanCommand(userId);
        queryProviderMock
            .Setup(x => x.Query<DietPlan>())
            .Returns(new List<DietPlan> { dietPlan }.AsQueryable());

        //Act
        var result = Sut().Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Should().Be(dietPlan);
    }

    private GetDietPlanCommand Command() => new(Guid.NewGuid());

    private GetDietPlanCommandHandler Sut() => new(queryProviderMock.Object);

}

