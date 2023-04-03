using CSharpFunctionalExtensions;
using FluentAssertions;
using HealthCoach.Core.Domain;
using HealthCoach.Core.Domain.Tests;
using HealthCoach.Shared.Core;
using HealthCoach.Shared.Infrastructure;
using Moq;
using Xunit;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace HealthCoach.Core.Business.Tests;

public class GetAllPersonalDataCommandHandlerTests
{
    private readonly Mock<IEfQueryProvider> queryProviderMock = new();

    [Fact]
    public void When_UserIdDoesNotExist_Then_ShouldFail()
    {
        var command = new GetAllPersonalDataCommand(Guid.NewGuid());

        queryProviderMock.Setup(x => x.Query<PersonalData>()).Returns(new List<PersonalData>().AsQueryable());

        var result = Sut().Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(BusinessErrors.PersonalData.Get.UserIdNotFound);
    }

    [Fact]
    public void When_UserIdExists_Then_ShouldSucceed()
    {
        var personalData = PersonalData.Create(Guid.NewGuid(),
                                                PersonalDataConstants.MinimumDateOfBirth,
                                                70,
                                                170,
                                                null,
                                                null,
                                                "Slabire",
                                                null).Value;

        var command = new GetAllPersonalDataCommand(personalData.UserId);

        queryProviderMock.Setup(x => x.Query<PersonalData>()).Returns(new List<PersonalData> { personalData }.AsQueryable());

        var result = Sut().Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(personalData);
    }

    private GetAllPersonalDataCommandHandler Sut() => new(queryProviderMock.Object);
}
