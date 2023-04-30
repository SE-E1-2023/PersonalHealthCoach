using CSharpFunctionalExtensions;
using FluentAssertions;
using HealthCoach.Core.Domain;
using HealthCoach.Core.Domain.Tests;
using HealthCoach.Shared.Infrastructure;
using Moq;
using Xunit;

namespace HealthCoach.Core.Business.Tests;

public class ReportPersonalTipCommandHandlerTests
{
    private readonly Mock<IRepository> repositoryMock = new();
    private readonly Mock<IEfQueryProvider> queryProviderMock = new();

    [Fact]
    public void When_PersonalTipDoesNotExist_Then_ShouldFail()
    {
        //Arrange
        var command = Command();
        repositoryMock.Setup(r => r.Load<PersonalTip>(command.PersonalTipId)).ReturnsAsync(Maybe<PersonalTip>.None);

        //Act
        var result = Sut().Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(BusinessErrors.PersonalTip.Report.PersonalTipDoesNotExist);

        repositoryMock.Verify(r => r.Store(It.IsAny<PersonalTip>()), Times.Never);
    }

    [Fact]
    public void When_PersonalTipExistsAndReportAlreadyExists_Then_ShouldFail()
    {
        //Arrange
        var personalTip = PersonalTipFactory.Any();
        var report = ReportFactory.Any();
        var command = Command() with { PersonalTipId = report.TargetId };

        repositoryMock.Setup(r => r.Load<PersonalTip>(command.PersonalTipId)).ReturnsAsync(personalTip);
        queryProviderMock.Setup(q => q.Query<Report>()).Returns(new List<Report>() { report }.AsQueryable());

        //Act
        var result = Sut().Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(BusinessErrors.PersonalTip.Report.ReportAlreadyExists);

        repositoryMock.Verify(r => r.Store(It.IsAny<PersonalTip>()), Times.Never);
    }

    [Fact]
    public void When_DomainFails_Then_ShouldFail()
    {
        //Arrange
        var personalTip = PersonalTipFactory.Any();
        var command = Command();

        repositoryMock.Setup(r => r.Load<PersonalTip>(command.PersonalTipId)).ReturnsAsync(personalTip);
        queryProviderMock.Setup(q => q.Query<Report>()).Returns(new List<Report>().AsQueryable());

        //Act
        var result = Sut().Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.Report.Create.ReasonNullOrEmpty);

        repositoryMock.Verify(r => r.Store(It.IsAny<FitnessPlan>()), Times.Never);
    }

    [Fact]
    public void When_DomainSucceeds_Then_ShouldSucceed()
    {
        //Arrange
        var personalTip = PersonalTipFactory.Any();
        var command = Command() with { Reason="reason" };

        repositoryMock.Setup(r => r.Load<PersonalTip>(command.PersonalTipId)).ReturnsAsync(personalTip);
        queryProviderMock.Setup(q => q.Query<Report>()).Returns(new List<Report>().AsQueryable());

        //Act
        var result = Sut().Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        //Assert
        result.IsSuccess.Should().BeTrue();

        repositoryMock.Verify(r => r.Store(It.IsAny<FitnessPlan>()), Times.Never);
    }

    private static ReportPersonalTipCommand Command() => new(Guid.NewGuid(), "");

    private ReportPersonalTipCommandHandler Sut() => new(repositoryMock.Object, queryProviderMock.Object);
}