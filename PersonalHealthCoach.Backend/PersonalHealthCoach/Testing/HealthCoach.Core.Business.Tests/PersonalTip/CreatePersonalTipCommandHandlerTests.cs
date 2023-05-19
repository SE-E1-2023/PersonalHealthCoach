using Moq;
using Xunit;
using FluentAssertions;
using HealthCoach.Shared.Web;
using HealthCoach.Core.Domain;
using CSharpFunctionalExtensions;
using HealthCoach.Core.Domain.Tests;
using HealthCoach.Shared.Infrastructure;

namespace HealthCoach.Core.Business.Tests;

public class CreatePersonalTipCommandHandlerTests
{
    private readonly Mock<IRepository> repositoryMock = new();
    private readonly Mock<IEfQueryProvider> queryProviderMock = new();
    private readonly Mock<IHttpClient> httpClientMock = new();
    private readonly Mock<IHttpClientFactory> httpClientFactoryMock = new();
    private readonly Mock<IFoodHistoryRepository> foodHistoryRepositoryMock = new();
    private readonly Mock<IExerciseHistoryRepository> exerciseHistoryRepositoryMock = new();

    public CreatePersonalTipCommandHandlerTests()
    {
        httpClientFactoryMock
            .Setup(f => f.OnBaseUrl(ExternalEndpoints.Ai.BaseUrl).OnRoute(ExternalEndpoints.Ai.TipGenerator))
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
        result.Error.Should().Be(BusinessErrors.PersonalTip.Create.UserNotFound);

        repositoryMock.Verify(r => r.Store(It.IsAny<PersonalTip>()), Times.Never);
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
        result.Error.Should().Be(BusinessErrors.PersonalTip.Create.PersonalDataNotFound);

        repositoryMock.Verify(r => r.Store(It.IsAny<PersonalTip>()), Times.Never);
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
        httpClientMock.Setup(h => h.Post<RequestPersonalTipCommand, RequestPersonalTipCommandResponse>(It.IsAny<RequestPersonalTipCommand>()))
            .ReturnsAsync(Result.Failure<RequestPersonalTipCommandResponse>("failure"));

        //Act
        var result = Sut().Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be("failure");

        repositoryMock.Verify(r => r.Store(It.IsAny<PersonalTip>()), Times.Never);
    }

    [Fact]
    public void When_DomainFails_Then_ShouldFail()
    {
        //Arrange
        var user = UsersFactory.Any();
        var command = Command() with { UserId = user.Id };
        var personalDataList = new List<PersonalData> { PersonalDataFactory.WithUserId(user.Id) };

        var apiResponse = new RequestPersonalTipCommandResponse
        {
            ImportanceLevel = "big",
            Tip = "",
            Type = "type"
        };

        repositoryMock.Setup(r => r.Load<User>(command.UserId)).ReturnsAsync(user);
        queryProviderMock.Setup(q => q.Query<PersonalData>()).Returns(personalDataList.AsQueryable());
        httpClientMock.Setup(h => h.Post<RequestPersonalTipCommand, RequestPersonalTipCommandResponse>(It.IsAny<RequestPersonalTipCommand>())).ReturnsAsync(Result.Success(apiResponse));

        //Act
        var result = Sut().Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.PersonalTip.Create.TipNullOrEmpty);

        repositoryMock.Verify(r => r.Store(It.IsAny<FitnessPlan>()), Times.Never);
    }

    [Fact]
    public void When_DomainSucceeds_Then_ShouldSucceed()
    {
        //Arrange
        var user = UsersFactory.Any();
        var command = Command() with { UserId = user.Id };
        var personalDataList = new List<PersonalData> { PersonalDataFactory.WithUserId(user.Id) };

        var apiResponse = new RequestPersonalTipCommandResponse
        {
            ImportanceLevel = "big",
            Tip = "test",
            Type = "general"
        };

        repositoryMock.Setup(r => r.Load<User>(command.UserId)).ReturnsAsync(user);
        queryProviderMock.Setup(q => q.Query<PersonalData>()).Returns(personalDataList.AsQueryable());
        httpClientMock.Setup(h => h.Post<RequestPersonalTipCommand, RequestPersonalTipCommandResponse>(It.IsAny<RequestPersonalTipCommand>())).ReturnsAsync(Result.Success(apiResponse));

        //Act
        var result = Sut().Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        //Assert
        result.IsSuccess.Should().BeTrue();

        result.Value.UserId.Should().Be(user.Id);
        result.Value.Type.Should().Be("general");
        result.Value.TipText.Should().Be("test");

        repositoryMock.Verify(r => r.Store(It.IsAny<PersonalTip>()), Times.Once);
    }

    private static CreatePersonalTipCommand Command() => new(Guid.NewGuid());

    private CreatePersonalTipCommandHandler Sut() => new(repositoryMock.Object, queryProviderMock.Object, httpClientFactoryMock.Object, foodHistoryRepositoryMock.Object, exerciseHistoryRepositoryMock.Object);
}