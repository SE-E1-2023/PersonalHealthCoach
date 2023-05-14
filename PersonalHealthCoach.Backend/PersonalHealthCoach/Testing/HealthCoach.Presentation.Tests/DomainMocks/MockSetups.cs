using System.Text;
using HealthCoach.Core.Business;
using Newtonsoft.Json;

namespace HealthCoach.Presentation.Tests;

public static class MockSetups
{

    public static UserMock SetupUser()
    {
        var client = new HttpClient();
        var guidPrefix = Guid.NewGuid().ToString().Substring(0, 8);
        var randomEmail = $"{guidPrefix}@EMAIL.com";

        var createUserCommand = new CreateUserCommand("Name", "FirstName", randomEmail);
        var json = JsonConvert.SerializeObject(createUserCommand);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = client.PostAsync(Routes.User.CreateUser, content).GetAwaiter().GetResult();

        var responseBody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        var user = JsonConvert.DeserializeObject<UserMock>(responseBody);

        return user;
    }

    public static FoodHistoryMock SetupFoodHistory()
    {
        var client = new HttpClient();
        var user = SetupUser();
        var command = new UpdateFoodHistoryCommand(user.Id, new List<Food>()
        {
            new Food("Lunch","Pizza",100,1)
        });

        var json = JsonConvert.SerializeObject(command);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = client.PostAsync(Routes.FoodHistory.GetFoodHistory, content).GetAwaiter().GetResult();
        var responseBody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        var foodHistory = JsonConvert.DeserializeObject<FoodHistoryMock>(responseBody);
        return foodHistory;
    }   
}