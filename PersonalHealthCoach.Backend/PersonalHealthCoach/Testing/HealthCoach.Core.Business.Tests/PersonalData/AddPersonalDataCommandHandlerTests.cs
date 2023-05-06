using Moq;
using Xunit;
using FluentAssertions;
using HealthCoach.Core.Domain;
using HealthCoach.Shared.Core;
using CSharpFunctionalExtensions;
using HealthCoach.Core.Domain.Tests;
using HealthCoach.Shared.Infrastructure;

namespace HealthCoach.Core.Business.Tests;

public class AddPersonalDataCommandHandlerTests
{
    private readonly Mock<IRepository> repositoryMock = new();

    [Fact]
    public void When_UserDoesNotExist_Then_ShouldFail()
    {
        var command = Command();
        repositoryMock.Setup(r => r.Load<User>(command.UserId)).ReturnsAsync(Maybe<User>.None);

        //Act
        var result = Sut().Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(BusinessErrors.PersonalData.Create.UserNotFound);

        repositoryMock.Verify(r => r.Store(It.IsAny<PersonalData>()), Times.Never);
    }

    [Fact]
    public void When_DomainFails_Then_ShouldFail()
    {
        //Arrange
        var user = UsersFactory.Any();
        var command = Command() with { UserId = user.Id, Height = -100.0f };

        repositoryMock.Setup(r => r.Load<User>(user.Id)).ReturnsAsync(user);

        //Act
        var result = Sut().Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.PersonalData.Create.InvalidHeight);

        repositoryMock.Verify(r => r.Store(It.IsAny<PersonalData>()), Times.Never);
    }

    [Fact]
    public void When_DomainSucceeds_Then_ShouldSucceed()
    {
        //Arrange
        var user = UsersFactory.Any();
        var command = Command() with { UserId = user.Id };
        var now = TimeProviderContext.AdvanceTimeToNow();

        repositoryMock.Setup(r => r.Load<User>(user.Id)).ReturnsAsync(user);

        //Act
        var result = Sut().Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        //Assert
        result.IsSuccess.Should().BeTrue();

        result.Value.Should().NotBeNull();
        result.Value.CreatedAt.Should().Be(now);
        result.Value.UserId.Should().Be(command.UserId);
        result.Value.DateOfBirth.Should().Be(command.DateOfBirth);
        result.Value.Weight.Should().Be(command.Weight);
        result.Value.Height.Should().Be(command.Height);
        result.Value.Goal.Should().Be(command.Goal);

        repositoryMock.Verify(r => r.Store(It.Is<PersonalData>(p => p.Id == result.Value.Id)), Times.Once);
    }

    private AddPersonalDataCommand Command() => new(
        Guid.NewGuid(),
        PersonalDataConstants.MinimumDateOfBirth,
        70,
        170,
        null,
        null,
        PersonalDataConstants.AllowedGoals.First(),
        null,
        12333,
        7.5,
        "M");

    private AddPersonalDataCommandHandler Sut() => new(repositoryMock.Object);
}
