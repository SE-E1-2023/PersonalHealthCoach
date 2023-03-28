using FluentAssertions;
using HealthCoach.Core.Domain;
using HealthCoach.Core.Domain.Tests;
using HealthCoach.Shared.Infrastructure;
using Moq;
using Xunit;

namespace HealthCoach.Core.Business.Tests;

public class CreateUserCommandHandlerTests
{
    private readonly Mock<IRepository> repositoryMock = new();
    private readonly Mock<IEfQueryProvider> queryProviderMock = new();

    [Fact]
    public void When_UserWithSameEmailAddressExists_Then_ShouldFail()
    {
        //Arrange
        var user = UsersFactory.Any();
        var command = Command() with { EmailAddress = user.EmailAddress };
        queryProviderMock
            .Setup(x => x.Query<User>())
            .Returns(new List<User>{ user }.AsQueryable());

        //Act
        var result = Sut().Handle(command, CancellationToken.None).Result;

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(BusinessErrors.User.Create.EmailAddressAlreadyInUse);
        
        repositoryMock.Verify(x => x.Store(It.IsAny<User>()), Times.Never);
    }

    [Fact]
    public void When_DomainFails_Then_ShouldFail()
    {
        //Arrange
        var command = Command() with { EmailAddress = "this is definitely a bad email address" };

        //Act
        var result = Sut().Handle(command, CancellationToken.None).Result;

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.User.Create.InvalidEmailAddressFormat);

        repositoryMock.Verify(x => x.Store(It.IsAny<User>()), Times.Never);
    }

    [Fact]
    public void When_UserDoesNotExist_Then_ShouldSucceed()
    {
        //Arrange
        var command = Command();

        //Act
        var result = Sut().Handle(command, CancellationToken.None).Result;

        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();

        repositoryMock.Verify(x => x.Store(It.IsAny<User>()), Times.Once);
    }

    private CreateUserCommand Command() => new(
        "Last name example",
        "First name example",
        "test@gmail.com"
    );

    private CreateUserCommandHandler Sut() => new(
        repositoryMock.Object,
        queryProviderMock.Object
    );
}