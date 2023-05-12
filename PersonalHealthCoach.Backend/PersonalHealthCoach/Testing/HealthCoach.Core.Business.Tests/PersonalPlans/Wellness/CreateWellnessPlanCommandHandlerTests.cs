using Moq;
using Xunit;
using FluentAssertions;
using HealthCoach.Shared.Web;
using HealthCoach.Core.Domain;
using CSharpFunctionalExtensions;
using HealthCoach.Core.Domain.Tests;
using HealthCoach.Shared.Infrastructure;

namespace HealthCoach.Core.Business.Tests;

public sealed class CreateWellnessPlanCommandHandlerTests
{
    private readonly Mock<IRepository> repositoryMock = new();
    private readonly Mock<IEfQueryProvider> queryProviderMock = new();
    private readonly Mock<IHttpClient> httpClientMock = new();
    private readonly Mock<IHttpClientFactory> httpClientFactoryMock = new();

    public CreateWellnessPlanCommandHandlerTests()
    {
        httpClientFactoryMock
            .Setup(f => f.OnBaseUrl(ExternalEndpoints.Ai.BaseUrl).OnRoute(ExternalEndpoints.Ai.Wellness))
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
        result.Error.Should().Be(BusinessErrors.WellnessPlan.Create.UserNotFound);

        httpClientMock.VerifyNoOtherCalls();
    }

    [Fact]
    public void When_UserExistsWithNoPersonalData_Then_ShouldSucceed()
    {
        // Arrange
        var user = UsersFactory.Any();
        var command = Command() with { UserId = user.Id };
        var apiResponse = new RequestWellnessPlanCommandResponse(new ApiActionResponse("desc", "title"), new List<string>());

        repositoryMock.Setup(r => r.Load<User>(command.UserId)).ReturnsAsync(user);
        queryProviderMock.Setup(q => q.Query<PersonalData>()).Returns(new List<PersonalData>().AsQueryable());
        httpClientMock
            .Setup(c => c.Post<RequestWellnessPlanCommand, RequestWellnessPlanCommandResponse>(It.IsAny<RequestWellnessPlanCommand>()))
            .ReturnsAsync(apiResponse);

        // Act
        var result = Sut().Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Title.Should().Be(apiResponse.Action.Title);
        result.Value.Description.Should().Be(apiResponse.Action.Description);
        result.Value.Categories.Should().BeEquivalentTo(apiResponse.Categories);
    }

    [Fact]
    public void When_UserExistsWithPersonalData_Then_ShouldSucceed()
    {
        // Arrange
        var user = UsersFactory.Any();
        var personalData = PersonalDataFactory.WithUserId(user.Id);
        var command = Command() with { UserId = user.Id };
        var apiResponse = new RequestWellnessPlanCommandResponse(new ApiActionResponse("desc", "title"), new List<string>());

        repositoryMock.Setup(r => r.Load<User>(command.UserId)).ReturnsAsync(user);
        queryProviderMock.Setup(q => q.Query<PersonalData>()).Returns(new List<PersonalData> { personalData }.AsQueryable());
        httpClientMock
            .Setup(c => c.Post<RequestWellnessPlanCommand, RequestWellnessPlanCommandResponse>(It.IsAny<RequestWellnessPlanCommand>()))
            .ReturnsAsync(apiResponse);

        // Act
        var result = Sut().Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Title.Should().Be(apiResponse.Action.Title);
        result.Value.Description.Should().Be(apiResponse.Action.Description);
        result.Value.Categories.Should().BeEquivalentTo(apiResponse.Categories);
    }

    private static CreateWellnessPlanCommand Command() => new(Guid.NewGuid());

    private CreateWellnessPlanCommandHandler Sut() => new(repositoryMock.Object, queryProviderMock.Object, httpClientFactoryMock.Object);
}