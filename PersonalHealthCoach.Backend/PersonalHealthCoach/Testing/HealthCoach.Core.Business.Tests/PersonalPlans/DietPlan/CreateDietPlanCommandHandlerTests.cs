
using CSharpFunctionalExtensions;
using FluentAssertions;
using HealthCoach.Core.Domain.Tests;
using HealthCoach.Core.Domain;
using HealthCoach.Shared.Core;
using HealthCoach.Shared.Infrastructure;
using HealthCoach.Shared.Web;
using Moq;
using Xunit;

namespace HealthCoach.Core.Business.Tests;

public sealed class CreateDietPlanCommandHandlerTests
{
    private readonly Mock<IRepository> repositoryMock = new();
    private readonly Mock<IEfQueryProvider> queryProviderMock = new();
    private readonly Mock<IHttpClient> httpClientMock = new();
    private readonly Mock<IHttpClientFactory> httpClientFactoryMock = new();

    public CreateDietPlanCommandHandlerTests()
    {
        httpClientFactoryMock
            .Setup(f => f.OnBaseUrl(ExternalEndpoints.Ai.BaseUrl).OnRoute(ExternalEndpoints.Ai.DietPlanner))
            .Returns(httpClientMock.Object);
    }

    [Fact]
    public void When_UserDoesNotExist_Then_ShouldFail()
    {
        //Arrange
        var command = Command();
        repositoryMock.Setup(r => r.Load<User>(command.UserId)).ReturnsAsync(Maybe<User>.None);

        //Act
        var result = Sut().Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(BusinessErrors.DietPlan.Create.UserNotFound);

        repositoryMock.Verify(r => r.Store(It.IsAny<DietPlan>()), Times.Never);
    }

    [Fact]
    public void When_UserDoesNotHavePersonalData_Then_ShouldFail()
    {
        //Arrange
        var command = Command();
        var user = UsersFactory.Any();

        repositoryMock.Setup(r => r.Load<User>(command.UserId)).ReturnsAsync(user);
        queryProviderMock.Setup(q => q.Query<PersonalData>()).Returns(new List<PersonalData>().AsQueryable());

        //Act
        var result = Sut().Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(BusinessErrors.DietPlan.Create.PersonalDataNotFound);

        repositoryMock.Verify(r => r.Store(It.IsAny<DietPlan>()), Times.Never);
    }

    [Fact]
    public void When_HttpClientFails_Then_ShouldFail()
    {
        //Arrange
        var user = UsersFactory.Any();
        var command = Command() with { UserId = user.Id };
        var personalDataList = new List<PersonalData> { PersonalDataFactory.WithUserId(user.Id) };

        repositoryMock.Setup(r => r.Load<User>(command.UserId)).ReturnsAsync(user);
        queryProviderMock.Setup(q => q.Query<PersonalData>()).Returns(personalDataList.AsQueryable());
        httpClientMock.Setup(h => h.Post<RequestDietPlanCommand, RequestDietPlanCommandResponse>(It.IsAny<RequestDietPlanCommand>())).ReturnsAsync(Result.Failure<RequestDietPlanCommandResponse>("failure"));

        //Act
        var result = Sut().Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be("failure");

        repositoryMock.Verify(r => r.Store(It.IsAny<DietPlan>()), Times.Never);
    }

    [Fact]
    public void When_DomainSucceeds_Then_ShouldSucceed()
    {
        //Arrange
        var now = TimeProviderContext.AdvanceTimeToNow();

        var user = UsersFactory.Any();
        var command = Command() with { UserId = user.Id };
        var personalDataList = new List<PersonalData> { PersonalDataFactory.WithUserId(user.Id) };

        var apiResponse = new RequestDietPlanCommandResponse
        {
            breakfast = new DietPlannerApiResponseMeal(1, "s", new List<string>() { "s" }, 100, "title"),
            drink = new DietPlannerApiResponseMeal(1, "s", new List<string>() { "s" }, 100, "title"),
            mainCourse = new DietPlannerApiResponseMeal(1, "s", new List<string>() { "s" }, 100, "title"),
            sideDish = new DietPlannerApiResponseMeal(1, "s", new List<string>() { "s" }, 100, "title"),
            snack = new DietPlannerApiResponseMeal(1, "s", new List<string>() { "s" }, 100, "title"),
            soup = new DietPlannerApiResponseMeal(1, "s", new List<string>() { "s" }, 100, "title"),
            NOP = 0,
            diet = new DietPlannerApiResponseDiet(new List<string>() { "s" }, new List<string>() { "s" }, 12, "name", new List<string>() { "s" }, "s")
        };

        repositoryMock.Setup(r => r.Load<User>(command.UserId)).ReturnsAsync(user);
        queryProviderMock.Setup(q => q.Query<PersonalData>()).Returns(personalDataList.AsQueryable());
        httpClientMock.Setup(h => h.Post<RequestDietPlanCommand, RequestDietPlanCommandResponse>(It.IsAny<RequestDietPlanCommand>())).ReturnsAsync(Result.Success(apiResponse));

        //Act
        var result = Sut().Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.CreatedAt.Should().Be(now);

        repositoryMock.Verify(r => r.Store(It.IsAny<DietPlan>()), Times.Once);
    }

    private static CreateDietPlanCommand Command() => new(Guid.NewGuid());

    private CreateDietPlanCommandHandler Sut() => new(repositoryMock.Object, queryProviderMock.Object, httpClientFactoryMock.Object);
}
