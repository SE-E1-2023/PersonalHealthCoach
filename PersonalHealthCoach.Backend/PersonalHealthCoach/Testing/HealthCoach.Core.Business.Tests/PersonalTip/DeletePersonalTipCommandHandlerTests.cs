using Moq;
using Xunit;
using FluentAssertions;
using HealthCoach.Core.Domain;
using HealthCoach.Core.Domain.Tests;
using HealthCoach.Shared.Infrastructure;

namespace HealthCoach.Core.Business.Tests;

public class DeletePersonalTipCommandHandlerTests
{
    private readonly Mock<IRepository> repositoryMock = new();
    private readonly Mock<IEfQueryProvider> queryProviderMock = new();

    [Fact]
    public void When_PersonalTipDoesNotExist_Then_ShouldFail()
    {
        //Arrange
        var command = new DeletePersonalTipCommand(Guid.NewGuid());

        queryProviderMock.Setup(x => x.Query<PersonalTip>()).Returns(new List<PersonalTip>().AsQueryable());

        //Act
        var result = Sut().Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(BusinessErrors.PersonalTip.Get.PersonalTipNotFound);

        repositoryMock.Verify(r => r.Delete(It.IsAny<PersonalTip>()), Times.Never);
    }

    [Fact]
    public void When_PersonalTipExists_Then_ShouldDelete()
    {
        //Arrange
        var personalTip = PersonalTipFactory.Any();

        var command = Command() with { PersonalTipId = personalTip.Id };

        queryProviderMock.Setup(x => x.Query<PersonalTip>()).Returns(new List<PersonalTip> { personalTip }.AsQueryable());

        //Act
        var result = Sut().Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        //Assert
        result.IsSuccess.Should().BeTrue();

        repositoryMock.Verify(x => x.Delete(It.IsAny<PersonalTip>()), Times.Once);
    }

    private DeletePersonalTipCommand Command() => new(Guid.NewGuid());

    private DeletePersonalTipCommandHandler Sut() => new(repositoryMock.Object, queryProviderMock.Object);
}
