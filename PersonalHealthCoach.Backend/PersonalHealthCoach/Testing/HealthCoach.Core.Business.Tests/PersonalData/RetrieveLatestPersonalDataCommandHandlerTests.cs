using FluentAssertions;
using HealthCoach.Core.Domain;
using HealthCoach.Core.Domain.Tests;
using HealthCoach.Shared.Infrastructure;
using Moq;
using Xunit;

namespace HealthCoach.Core.Business.Tests;

public class RetrieveLatestPersonalDataCommandHandlerTests
{
    private readonly Mock<IEfQueryProvider> queryProviderMock = new();

    [Fact]
    public void When_PersonalDataDoesNotExist_Then_ShouldFail()
    {
        //Arrange
        var command = Command();
        queryProviderMock.Setup(x => x.Query<PersonalData>()).Returns(new List<PersonalData>().AsQueryable());

        //Act
        var result = Sut().Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(BusinessErrors.PersonalData.Get.PersonalDataNotFound);
    }

    [Fact]
    public void When_PersonalDataDoesExist_Then_ShouldSucceed()
    {
        //Arrange
        var personalData = PersonalDataFactory.Any();
        var command = Command(personalData.UserId);
        queryProviderMock
            .Setup(x => x.Query<PersonalData>())
            .Returns(new List<PersonalData> { personalData }.AsQueryable());

        //Act
        var result = Sut().Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(personalData);
    }

    private RetrieveLatestPersonalDataCommand Command() => new(Guid.NewGuid());
    private RetrieveLatestPersonalDataCommand Command(Guid id) => new(id);

    private RetrieveLatestPersonalDataCommandHandler Sut() => new(queryProviderMock.Object);
}