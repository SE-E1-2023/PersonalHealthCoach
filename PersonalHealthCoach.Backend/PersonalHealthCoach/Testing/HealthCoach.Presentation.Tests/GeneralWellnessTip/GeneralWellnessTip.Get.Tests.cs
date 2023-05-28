using CSharpFunctionalExtensions;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HealthCoach.Presentation.Tests;

public partial class GeneralWellnessTip
{
    private readonly HttpClient client = new();
    [Fact]
    public void Given_GetGeneralWellnessTip_Then_ShouldSendSuccessAndGeneralWellnessTip()
    {
        //Arrange

        //Act
        var response = client.GetAsync(Routes.GeneralWellnessTip.GetGeneralWellnessTip).GetAwaiter().GetResult();

        //Assert
        response.IsSuccessStatusCode.Should().BeTrue();
    }
}
