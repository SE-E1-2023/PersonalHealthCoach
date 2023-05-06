using Xunit;
using FluentAssertions;

namespace HealthCoach.Core.Domain.Tests;

public partial class UserTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("     ")]
    public void Given_Create_When_NameIsNotProvided_Then_ShouldFail(string badName)
    {
        //Act
        var result = User.Create(badName, "John", "test@mail.com");

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.User.Create.NameNullOrEmpty);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("     ")]
    public void Given_Create_When_FirstNameIsNotProvided_Then_ShouldFail(string badFirstName)
    {
        //Act
        var result = User.Create("Doe", badFirstName, "test@gmail.com");

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.User.Create.FirstNameNullOrEmpty);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("     ")]
    public void Given_Create_When_EmailAddressIsNotProvided_Then_ShouldFail(string badEmailAddress)
    {
        //Act
        var result = User.Create("Doe", "John", badEmailAddress);

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.User.Create.EmailAddressNullOrEmpty);
    }

    [Theory]
    [InlineData("test")]
    [InlineData("test@")]
    [InlineData("test@mail")]
    [InlineData("test@mail.")]
    public void Given_Create_When_EmailAddressIsNotValid_Then_ShouldFail(string badEmailAddress)
    {
        //Act
        var result = User.Create("Doe", "John", badEmailAddress);

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.User.Create.InvalidEmailAddressFormat);
    }

    [Fact]
    public void Given_Create_When_NotViolatingConstraints_Then_ShouldSucceed()
    {
        //Act
        var result = User.Create("Doe", "John", "test@gmail.com");

        //Assert
        result.IsSuccess.Should().BeTrue();
    }
}