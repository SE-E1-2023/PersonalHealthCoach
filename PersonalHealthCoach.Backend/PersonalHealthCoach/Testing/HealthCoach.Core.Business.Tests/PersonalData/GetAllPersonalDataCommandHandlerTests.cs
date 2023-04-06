using CSharpFunctionalExtensions;
using FluentAssertions;
using HealthCoach.Core.Domain;
using HealthCoach.Core.Domain.Tests;
using HealthCoach.Shared.Infrastructure;
using Moq;
using Xunit;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace HealthCoach.Core.Business.Tests;

public class GetAllPersonalDataCommandHandlerTests
{
    private readonly Mock<IEfQueryProvider> queryProviderMock = new();
    private readonly Mock<IRepository> repositoryMock = new();

    [Fact]
    public void When_UserIdDoesNotExist_Then_ShouldFail()
    {
        var command = new GetAllPersonalDataCommand(Guid.NewGuid());

        repositoryMock.Setup(r => r.Load<User>(command.UserId)).ReturnsAsync(Maybe<User>.None);
        queryProviderMock.Setup(x => x.Query<PersonalData>()).Returns(new List<PersonalData>().AsQueryable());

        var result = Sut().Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(BusinessErrors.PersonalData.Get.UserNotFound);
    }

    [Fact]
    public void When_PersonalDataDoesNotExist_Then_ShoudFail()
    {
        var command = new GetAllPersonalDataCommand(Guid.NewGuid());
        var user = UsersFactory.Any();

        repositoryMock.Setup(r => r.Load<User>(command.UserId)).ReturnsAsync(user);
        queryProviderMock.Setup(x => x.Query<PersonalData>()).Returns(new List<PersonalData>().AsQueryable());


        var result = Sut().Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(BusinessErrors.PersonalData.Get.PersonalDataNotFound);
    }

    [Fact]
    public void When_NotViolatingConstraints_Then_ShouldSucceed()
    {
        //var personalData = PersonalDataFactory.Any();
        var personalDataList = new List<PersonalData>();
        var user = UsersFactory.Any();

        foreach (int value in Enumerable.Range(1, 5))
            personalDataList.Add(PersonalDataFactory.WithUserId(user.Id));

        var command = new GetAllPersonalDataCommand(user.Id);

        repositoryMock.Setup(r => r.Load<User>(user.Id)).ReturnsAsync(user);
        queryProviderMock.Setup(x => x.Query<PersonalData>()).Returns(personalDataList.AsQueryable());

        var result = Sut().Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(personalDataList);
    }

    private GetAllPersonalDataCommandHandler Sut() => new(queryProviderMock.Object, repositoryMock.Object);
}
