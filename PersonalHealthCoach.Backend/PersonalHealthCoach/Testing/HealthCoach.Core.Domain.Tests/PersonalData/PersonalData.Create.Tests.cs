using Xunit;
using FluentAssertions;
using HealthCoach.Shared.Core;

namespace HealthCoach.Core.Domain.Tests;
public partial class PersonalDataTests
{
    private readonly Guid goodUserId = Guid.NewGuid();
    private readonly DateTime goodDateOfBirth = new DateTime(1980, 10, 23);
    private readonly float goodWeight = 70;
    private readonly float goodHeight = 170;
    private readonly string goodGoal = PersonalDataConstants.AllowedGoals.First();
    private readonly int goodDailySteps = 12333;
    private readonly double goodHoursOfSleep = 8.5;
    private readonly string goodGender = PersonalDataConstants.AllowedGenders.First();

    [Fact]
    public void Given_Create_When_DateOfBirthIsNull_Then_ShouldFail()
    {
        //Act
        var result = PersonalData.Create(goodUserId, null, goodWeight, goodHeight, null, null, goodGoal, null,
            goodDailySteps, goodHoursOfSleep, goodGender);

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.PersonalData.Create.DateOfBirthNull);
    }

    [Fact]
    public void Given_Create_When_UserNotOldEnough_ShouldFail()
    {
        //Arrange
        var badDateOfBirth = TimeProviderContext.AdvanceTimeToNow();

        //Act
        var result = PersonalData.Create(goodUserId, badDateOfBirth, goodWeight, goodHeight, null, null, goodGoal, null,
            goodDailySteps, goodHoursOfSleep, goodGender);

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.PersonalData.Create.UserNotOldEnough);
    }

    [Fact]
    public void Given_Create_When_WeightIsInvalid_Then_ShouldFail()
    {
        //Arrange
        var badWeight = -100.0f;

        //Act
        var result = PersonalData.Create(goodUserId, goodDateOfBirth, badWeight, goodHeight, null, null, goodGoal, null,
            goodDailySteps, goodHoursOfSleep, goodGender);

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.PersonalData.Create.InvalidWeight);
    }

    [Fact]
    public void Given_Create_When_HeightIsInvalid_Then_ShouldFail()
    {
        //Arrange
        var badHeight = -100.0f;

        //Act
        var result = PersonalData.Create(goodUserId, goodDateOfBirth, goodWeight, badHeight, null, null, goodGoal, null,
            goodDailySteps, goodHoursOfSleep, goodGender);

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.PersonalData.Create.InvalidHeight);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("     ")]
    public void Given_Create_When_GoalIsNullOrEmpty_Then_ShouldFail(string badGoal)
    {
        //Act
        var result = PersonalData.Create(goodUserId, goodDateOfBirth, goodWeight, goodHeight, null, null, badGoal, null,
            goodDailySteps, goodHoursOfSleep, goodGender);

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.PersonalData.Create.GoalIsNullOrEmpty);
    }

    [Fact]
    public void Given_Create_When_GoalIsUnrecognized_Then_ShouldFail()
    {
        //Arrange
        var badGoal = PersonalDataConstants.AllowedGoals.First() + "bad";

        //Act
        var result = PersonalData.Create(goodUserId, goodDateOfBirth, goodWeight, goodHeight, null, null, badGoal, null,
            goodDailySteps, goodHoursOfSleep, goodGender);

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.PersonalData.Create.GoalIsUnrecognized);
    }

    [Fact]
    public void Given_Create_When_StepsIsNegative_Then_ShouldFail()
    {
        //Arrange
        var badSteps = -1;

        //Act
        var result = PersonalData.Create(goodUserId, goodDateOfBirth, goodWeight, goodHeight, null, null, goodGoal, null,
            badSteps, goodHoursOfSleep, goodGender);

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.PersonalData.Create.InvalidDailySteps);
    }

    [Fact]
    public void Given_Create_When_HoursOfSleepIsNegative_Then_ShouldFail()
    {
        //Arrange
        var badHoursOfSleep = -1.0;

        //Act
        var result = PersonalData.Create(goodUserId, goodDateOfBirth, goodWeight, goodHeight, null, null, goodGoal, null,
            goodDailySteps, badHoursOfSleep, goodGender);

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.PersonalData.Create.InvalidHoursOfSleep);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("     ")]
    [InlineData("bad")]
    public void Given_Create_When_GenderIsInvalid_Then_ShouldFail(string badGender)
    {
        //Act
        var result = PersonalData.Create(goodUserId, goodDateOfBirth, goodWeight, goodHeight, null, null, goodGoal, null,
            goodDailySteps, goodHoursOfSleep, badGender);

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.PersonalData.Create.InvalidGender);
    }

    [Fact]
    public void Given_Create_When_NotViolatingConstraints_Then_ShouldSucceed()
    {
        //Act
        var result = PersonalData.Create(goodUserId, goodDateOfBirth, goodWeight, goodHeight, null, null, goodGoal, null,
            goodDailySteps, goodHoursOfSleep, goodGender);

        //Assert
        result.IsSuccess.Should().BeTrue();

        result.Value.Should().NotBeNull();
        result.Value.UserId.Should().Be(goodUserId);
        result.Value.DateOfBirth.Should().Be(goodDateOfBirth);
        result.Value.Weight.Should().Be(goodWeight);
        result.Value.Height.Should().Be(goodHeight);
        result.Value.Goal.Should().Be(goodGoal);
    }
}

