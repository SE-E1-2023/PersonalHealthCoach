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

public class CreateFitnessPlanCommandHandlerTests
{
    private readonly Mock<IRepository> repositoryMock = new();
    private readonly Mock<IEfQueryProvider> queryProviderMock = new();
    private readonly Mock<IHttpClient> httpClientMock = new();
    private readonly Mock<IHttpClientFactory> httpClientFactoryMock = new();

    public CreateFitnessPlanCommandHandlerTests()
    {
        httpClientFactoryMock
            .Setup(f => f.OnBaseUrl(ExternalEndpoints.Ai.BaseUrl).OnRoute(ExternalEndpoints.Ai.FitnessPlanner))
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
        result.Error.Should().Be(BusinessErrors.FitnessPlan.Create.UserNotFound);

        repositoryMock.Verify(r => r.Store(It.IsAny<FitnessPlan>()), Times.Never);
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
        result.Error.Should().Be(BusinessErrors.FitnessPlan.Create.PersonalDataNotFound);

        repositoryMock.Verify(r => r.Store(It.IsAny<FitnessPlan>()), Times.Never);
    }

    [Fact]
    public void When_HttpClientFails_Then_ShouldFail()
    {
        //Arrange
        var user = UsersFactory.Any();
        var command = Command() with { UserId = user.Id };
        var personalDataList = new List<PersonalData> { PersonalDataFactory.WithUserId(user.Id) };

        var apiRequest = new RequestFitnessPlanCommand(1);

        repositoryMock.Setup(r => r.Load<User>(command.UserId)).ReturnsAsync(user);
        queryProviderMock.Setup(q => q.Query<PersonalData>()).Returns(personalDataList.AsQueryable());
        httpClientMock.Setup(h => h.Post<RequestFitnessPlanCommand, RequestFitnessPlanCommandResponse>(apiRequest)).ReturnsAsync(Result.Failure<RequestFitnessPlanCommandResponse>("failure"));

        //Act
        var result = Sut().Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be("failure");

        repositoryMock.Verify(r => r.Store(It.IsAny<FitnessPlan>()), Times.Never);
    }

    [Fact]
    public void When_DomainFails_Then_ShouldFail()
    {
        //Arrange
        var user = UsersFactory.Any();
        var command = Command() with { UserId = user.Id };
        var personalDataList = new List<PersonalData> { PersonalDataFactory.WithUserId(user.Id) };

        var apiRequest = new RequestFitnessPlanCommand(1);
        var apiResponse = new RequestFitnessPlanCommandResponse
        {
            message = "success",
            status = "ok",
            workout = new(new List<FitnessPlannerApiResponseExercise>())
        };

        repositoryMock.Setup(r => r.Load<User>(command.UserId)).ReturnsAsync(user);
        queryProviderMock.Setup(q => q.Query<PersonalData>()).Returns(personalDataList.AsQueryable());
        httpClientMock.Setup(h => h.Post<RequestFitnessPlanCommand, RequestFitnessPlanCommandResponse>(apiRequest)).ReturnsAsync(Result.Success(apiResponse));

        //Act
        var result = Sut().Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.FitnessPlan.Create.NoExercises);

        repositoryMock.Verify(r => r.Store(It.IsAny<FitnessPlan>()), Times.Never);
    }

    [Fact]
    public void When_DomainSucceeds_Then_ShouldSucceed()
    {
        //Arrange
        var now = TimeProviderContext.AdvanceTimeToNow();

        var user = UsersFactory.Any();
        var command = Command() with { UserId = user.Id };
        var personalDataList = new List<PersonalData> { PersonalDataFactory.WithUserId(user.Id) };

        var apiRequest = new RequestFitnessPlanCommand(1);
        var apiResponse = new RequestFitnessPlanCommandResponse
        {
            message = "success",
            status = "ok",
            workout = new(new List<FitnessPlannerApiResponseExercise> { new("exercise", "1-2", "23", 4, "strength") })
        };

        repositoryMock.Setup(r => r.Load<User>(command.UserId)).ReturnsAsync(user);
        queryProviderMock.Setup(q => q.Query<PersonalData>()).Returns(personalDataList.AsQueryable());
        httpClientMock.Setup(h => h.Post<RequestFitnessPlanCommand, RequestFitnessPlanCommandResponse>(apiRequest)).ReturnsAsync(Result.Success(apiResponse));

        //Act
        var result = Sut().Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        //Assert
        result.IsSuccess.Should().BeTrue();

        result.Value.UserId.Should().Be(user.Id);
        result.Value.Exercises.Should().HaveCount(1);
        result.Value.Exercises.First().Name.Should().Be("exercise");
        result.Value.Exercises.First().RepRange.Should().Be("1-2");
        result.Value.Exercises.First().RestTime.Should().Be("23");
        result.Value.Exercises.First().Sets.Should().Be(4);
        result.Value.Exercises.First().Type.Should().Be("strength");
        result.Value.CreatedAt.Should().Be(now);

        repositoryMock.Verify(r => r.Store(It.IsAny<FitnessPlan>()), Times.Once);
    }

    private static CreateFitnessPlanCommand Command() => new(Guid.NewGuid());

    private CreateFitnessPlanCommandHandler Sut() => new(repositoryMock.Object, queryProviderMock.Object, httpClientFactoryMock.Object);
}