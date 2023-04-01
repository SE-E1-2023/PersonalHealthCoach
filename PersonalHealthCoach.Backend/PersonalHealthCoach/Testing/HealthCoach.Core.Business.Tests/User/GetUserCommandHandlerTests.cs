using FluentAssertions;
using HealthCoach.Core.Domain;
using HealthCoach.Core.Domain.Tests;
using HealthCoach.Shared.Infrastructure;
using Moq;
using Xunit;

namespace HealthCoach.Core.Business.Tests;

public class GetUserCommandHandlerTests
{
    private readonly Mock<IRepository> repositoryMock = new();
    private readonly Mock<IEfQueryProvider> queryProviderMock = new();

    [Fact]
    public void When_UserDoesNotExist_Then_ShouldFail()
    {
        //Arrange
        var command = new GetUserCommand("email_de_test_ce_nu_exista@gmail.com");
        queryProviderMock
            .Setup(x => x.Query<User>())
            .Returns(new List<User>().AsQueryable());

        //Act
        var result = Sut().Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(BusinessErrors.User.Get.EmailAddressDoesntExist);
    }

    [Fact]
    public void When_UserDoesExist_Then_ShouldSucceed()
    {
        //Arrange
        var user = UsersFactory.Any();
        var command = new GetUserCommand(user.EmailAddress);
        queryProviderMock
            .Setup(x => x.Query<User>())
            .Returns(new List<User> { user }.AsQueryable());

        //Act
        var result = Sut().Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(user.Id);
    }

    private GetUserCommandHandler Sut() => new(
        repositoryMock.Object,
        queryProviderMock.Object
    );
}

