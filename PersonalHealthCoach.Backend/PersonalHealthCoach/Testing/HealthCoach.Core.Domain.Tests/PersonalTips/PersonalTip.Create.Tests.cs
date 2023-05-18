using Xunit;
using FluentAssertions;

namespace HealthCoach.Core.Domain.Tests;

public partial class PersonalTipTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("     ")]
    public void Given_Create_When_TypeIsNotProvided_Then_ShouldFail(string badType)
    {
        //Act
        var result = PersonalTip.Create(Guid.NewGuid(), badType, "test");

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.PersonalTip.Create.TipTypeNullOrEmpty);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("     ")]
    public void Given_Create_When_TipTextIsNotProvided_Then_ShouldFail(string badTipText)
    {
        //Act
        var result = PersonalTip.Create(Guid.NewGuid(), "general", badTipText);

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.PersonalTip.Create.TipNullOrEmpty);
    }

    [Fact]
    public void Given_Create_When_NotViolatingConstraints_Then_ShouldSucceed()
    {
        //Act
        var result = PersonalTip.Create(Guid.NewGuid(), "general", "test");

        //Assert
        result.IsSuccess.Should().BeTrue();
    }
}

