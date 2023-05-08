using CSharpFunctionalExtensions;
using FluentAssertions;
using HealthCoach.Core.Domain;
using HealthCoach.Core.Domain.Tests;
using HealthCoach.Shared.Infrastructure;
using Moq;
using Xunit;

namespace HealthCoach.Core.Business.Tests;

public class ReportDietPlanCommandHandlerTests
{
    private readonly Mock<IRepository> repositoryMock = new();
    private readonly Mock<IEfQueryProvider> queryProviderMock = new();

    [Fact]
    public void When_DietPlanDoesNotExist_Then_ShouldFail()
    {
        //Arrange
        var command = Command();
        repositoryMock.Setup(r => r.Load<DietPlan>(command.DietPlanId)).ReturnsAsync(Maybe<DietPlan>.None);

        //Act
        var result = Sut().Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(BusinessErrors.DietPlan.Report.DietPlanDoesNotExist);

        repositoryMock.Verify(r => r.Store(It.IsAny<Report>()), Times.Never);
    }

    [Fact]
    public void When_DietPlanExistsAndReportAlreadyExists_Then_ShouldFail()
    {
        //Arrange
        var dietPlan = PlansFactory.DietPlans.Any();
        var report = ReportFactory.WithTargetId(dietPlan.Id);
        var command = Command() with { DietPlanId = report.TargetId };

        repositoryMock.Setup(r => r.Load<DietPlan>(command.DietPlanId)).ReturnsAsync(dietPlan);
        queryProviderMock.Setup(q => q.Query<Report>()).Returns(new List<Report> { report }.AsQueryable());

        //Act
        var result = Sut().Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(BusinessErrors.DietPlan.Report.ReportAlreadyExists);

        repositoryMock.Verify(r => r.Store(It.IsAny<Report>()), Times.Never);
    }

    [Fact]
    public void When_DomainFails_Then_ShouldFail()
    {
        //Arrange
        var dietPlan = PlansFactory.DietPlans.Any();
        var command = Command() with { Reason = string.Empty };

        repositoryMock.Setup(r => r.Load<DietPlan>(command.DietPlanId)).ReturnsAsync(dietPlan);
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
        var dietPlan = PlansFactory.DietPlans.Any();
        var command = Command() with { Reason = "reason" };

        repositoryMock.Setup(r => r.Load<DietPlan>(command.DietPlanId)).ReturnsAsync(dietPlan);
        queryProviderMock.Setup(q => q.Query<Report>()).Returns(new List<Report>().AsQueryable());

        //Act
        var result = Sut().Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        //Assert
        result.IsSuccess.Should().BeTrue();

        repositoryMock.Verify(r => r.Store(It.IsAny<Report>()), Times.Once);
    }

    private static ReportDietPlanCommand Command() => new(Guid.NewGuid(), "report reason");

    private ReportDietPlanCommandHandler Sut() => new(repositoryMock.Object, queryProviderMock.Object);
}