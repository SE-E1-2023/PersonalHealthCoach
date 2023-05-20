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

        repositoryMock
            .Setup(r => r.Load<User>(command.UserId)).ReturnsAsync(Maybe<User>.None);
        queryProviderMock
            .Setup(x => x.Query<DietPlan>()).Returns(new List<DietPlan>().AsQueryable());

        //Act
        var result = Sut().Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(BusinessErrors.DietPlan.Get.UserNotFound);

    }

    [Fact]
    public void When_DietPlanDoesNotExist_Then_ShouldFail()
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
        result.Error.Should().Be(BusinessErrors.DietPlan.Get.DietPlanDoesNotExist);
    }

    [Fact]
    public void When_DietPlanDoesExist_Then_ShouldSucceed()
    {
        //Arrange
        var user = UsersFactory.Any();
        var dietPlan = PlansFactory.DietPlans.WithUserId(user.Id);

        var command = new GetDietPlanCommand(user.Id);
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

    private GetDietPlanCommandHandler Sut() => new(queryProviderMock.Object);

}

