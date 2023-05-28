using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

namespace HealthCoach.Presentation.Tests
{
    public partial class DietPlanTests
    {
        [Fact]
        public void Given_GetDietPlan_When_UserFoundAndNoDietPlan_Then_ShouldSendOKButEmptyJSON()
        {
            //Arrange
            var user = MockSetups.SetupUser();

            //Act
            var response = client.GetAsync(string.Format(Routes.DietPlan.GetDietPlan, user.Id)).GetAwaiter().GetResult();
            //Assert
            response.IsSuccessStatusCode.Should().BeTrue();
            response.ReasonPhrase.Should().Be("OK");

            var responseBody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var dietPlan = JsonConvert.DeserializeObject<DietPlanMock>(responseBody);
            dietPlan.Should().Be(null);
        }

        [Fact]
        public void Given_GetDietPlan_When_UserFoundAndHasDietPlan_Then_ShouldSuccessAndDietPlan()
        {
            //Arrange
            var user = MockSetups.SetupUser();
            var dietPlanMock = MockSetups.SetupDietPlan(user.Id);

            //Act
            var response = client.GetAsync(string.Format(Routes.DietPlan.GetDietPlan, user.Id)).GetAwaiter().GetResult();
            //Assert
            response.IsSuccessStatusCode.Should().BeTrue();
            response.ReasonPhrase.Should().Be("OK");

            var responseBody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var dietPlan = JsonConvert.DeserializeObject<DietPlanMock>(responseBody);
            dietPlan.Should().NotBeNull();
        }
    }
}
