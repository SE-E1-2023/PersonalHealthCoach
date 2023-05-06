using FluentAssertions;
using HealthCoach.Shared.Core;
using Xunit;

namespace HealthCoach.Core.Domain.Tests;

public partial class ReportTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("    ")]
    public void Given_Create_When_TargetIsNullOrEmpty_Then_ShouldFail(string badTarget)
    {
        //Act
        var result = Report.Create(Guid.NewGuid(), badTarget, "reason");

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.Report.Create.TargetNullOrEmpty);
    }

    [Theory]
    [InlineData("notAllowed")]
    [InlineData("notAllowed2")]
    [InlineData("bad target")]
    public void Given_Create_When_TargetIsUnrecognized_Then_ShouldFail(string badTarget)
    {
        //Act
        var result = Report.Create(Guid.NewGuid(), badTarget, "reason");

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.Report.Create.InvalidTarget);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("    ")]
    public void Given_Create_When_ReasonIsNullOrEmpty_Then_ShouldFail(string badReason)
    {
        //Act
        var result = Report.Create(Guid.NewGuid(), ReportConstants.AllowedTargets.First(), badReason);

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.Report.Create.ReasonNullOrEmpty);
    }

    [Fact]
    public void Given_Create_When_NotViolatingConstraints_Then_ShouldSucceed()
    {
        //Arrange
        var now = TimeProviderContext.AdvanceTimeToNow();
        var targetId = Guid.NewGuid();
        var reason = "some reason";

        //Act
        var result = Report.Create(targetId, ReportConstants.AllowedTargets.First(), reason);

        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.TargetId.Should().Be(targetId);
        result.Value.Target.Should().Be(ReportConstants.AllowedTargets.First());
        result.Value.ReportedAt.Should().Be(now);
    }
}