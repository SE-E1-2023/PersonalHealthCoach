using FluentAssertions;
using HealthCoach.Core.Domain;
using HealthCoach.Core.Domain.Tests;
using HealthCoach.Shared.Infrastructure;
using Moq;
using Xunit;

namespace HealthCoach.Core.Business.Tests;

public class GetAllPersonalDataCommandHandlerTests
{
    private readonly Mock<IEfQueryProvider> queryProviderMock = new();
    private readonly Mock<IRepository> repositoryMock = new();

    [Fact]
    public void When_UserIdDoesNotExist_Then_ShouldFail()
    {
        var command = new GetAllPersonalDataCommand(Guid.NewGuid());

        queryProviderMock.Setup(x => x.Query<PersonalData>()).Returns(new List<PersonalData>().AsQueryable());

        var result = Sut().Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(BusinessErrors.PersonalData.Get.UserNotFound);
    }

    [Fact]
    public void When_NotViolatingConstraints_Then_ShouldSucceed()
    {
        //var personalData = PersonalDataFactory.Any();
        var personalDataList = new List<PersonalData>();
        Guid guid = Guid.NewGuid();
        var user = UsersFactory.Any();

        foreach (int value in Enumerable.Range(1, 5))
            personalDataList.Add(PersonalDataFactory.WithUserId(guid));

        var command = new GetAllPersonalDataCommand(guid);

        repositoryMock.Setup(r => r.Load<User>(guid)).ReturnsAsync(user);
        queryProviderMock.Setup(x => x.Query<User>()).Returns(new List<User> { user }.AsQueryable());

        var result = Sut().Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(personalDataList);
    }

    private GetAllPersonalDataCommandHandler Sut() => new(queryProviderMock.Object, repositoryMock.Object);
}
