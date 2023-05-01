using CSharpFunctionalExtensions;
using FluentAssertions;
using HealthCoach.Core.Domain;
using HealthCoach.Core.Domain.Tests;
using HealthCoach.Shared.Infrastructure;
using Moq;
using Xunit;

namespace HealthCoach.Core.Business.Tests;

public class ReportFitnessPlanCommandHandlerTests
{
    private readonly Mock<IRepository> repositoryMock = new();
    private readonly Mock<IEfQueryProvider> queryProviderMock = new();

    [Fact]
    public void When_FitnessPlanDoesNotExist_Then_ShouldFail()
    {
        //Arrange
        var command = Command();
        repositoryMock.Setup(r => r.Load<FitnessPlan>(command.FitnessPlanId)).ReturnsAsync(Maybe<FitnessPlan>.None);

        //Act
        var result = Sut().Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(BusinessErrors.FitnessPlan.Report.FitnessPlanDoesNotExist);

        repositoryMock.Verify(r => r.Store(It.IsAny<Report>()), Times.Never);
    }

    [Fact]
    public void When_FitnessPlanExistsAndReportAlreadyExists_Then_ShouldFail()
    {
        //Arrange
        var fitnessPlan = PlansFactory.FitnessPlans.Any();
        var report = ReportFactory.Any();
        var command = Command() with { FitnessPlanId = report.TargetId };

        repositoryMock.Setup(r => r.Load<FitnessPlan>(command.FitnessPlanId)).ReturnsAsync(fitnessPlan);
        queryProviderMock.Setup(q => q.Query<Report>()).Returns(new List<Report>() { report }.AsQueryable());

        //Act
        var result = Sut().Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(BusinessErrors.FitnessPlan.Report.ReportAlreadyExists);

        repositoryMock.Verify(r => r.Store(It.IsAny<Report>()), Times.Never);
    }

    [Fact]
    public void When_DomainFails_Then_ShouldFail()
    {
        //Arrange
        var fitnessPlan = PlansFactory.FitnessPlans.Any();
        var command = Command();

        repositoryMock.Setup(r => r.Load<FitnessPlan>(command.FitnessPlanId)).ReturnsAsync(fitnessPlan);
        queryProviderMock.Setup(q => q.Query<Report>()).Returns(new List<Report>().AsQueryable());

        //Act
        var result = Sut().Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.Report.Create.ReasonNullOrEmpty);

        repositoryMock.Verify(r => r.Store(It.IsAny<Report>()), Times.Never);
    }

    [Fact]
    public void When_DomainSucceeds_Then_ShouldSucceed()
    {
        //Arrange
        var fitnessPlan = PlansFactory.FitnessPlans.Any();
        var command = Command() with { Reason = "reason" };

        repositoryMock.Setup(r => r.Load<FitnessPlan>(command.FitnessPlanId)).ReturnsAsync(fitnessPlan);
        queryProviderMock.Setup(q => q.Query<Report>()).Returns(new List<Report>().AsQueryable());

        //Act
        var result = Sut().Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        //Assert
        result.IsSuccess.Should().BeTrue();

        repositoryMock.Verify(r => r.Store(It.IsAny<Report>()), Times.Once);
    }

    private static ReportFitnessPlanCommand Command() => new(Guid.NewGuid(), "");

    private ReportFitnessPlanCommandHandler Sut() => new(repositoryMock.Object, queryProviderMock.Object);
}