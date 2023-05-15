using System.Text;
using HealthCoach.Core.Business;
using HealthCoach.Core.Domain;
using Newtonsoft.Json;
using Exercise = HealthCoach.Core.Business.Exercise;

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

    public static ExerciseHistoryMock SetupExerciseMistory(Guid id)
    {
        var client = new HttpClient();
        var completedExercise = new Exercise("exercise 1", 100, 10);
        IReadOnlyCollection<Exercise> randomExercises = new List<Exercise>() { completedExercise };

        var updateExerciseHistoryCommand = new UpdateExerciseHistoryCommand(id, randomExercises);
        var json = JsonConvert.SerializeObject(updateExerciseHistoryCommand);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = client.PostAsync(string.Format(Routes.ExerciseHistory.UpdateExerciseHistory,id), content).GetAwaiter().GetResult();

        var responseBody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        var exerciseHistory = JsonConvert.DeserializeObject<ExerciseHistoryMock>(responseBody);

        return exerciseHistory;
    }

    public static FoodHistoryMock SetupFoodHistory(Guid id)
    {
        var client = new HttpClient();
        var command = new UpdateFoodHistoryCommand(id, new List<Food>()
        {
            new Food("Lunch","Pizza",100,1)
        });

        var json = JsonConvert.SerializeObject(command);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = client.PostAsync(string.Format(Routes.FoodHistory.GetFoodHistory, id), content).GetAwaiter().GetResult();
        var responseBody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        var foodHistory = JsonConvert.DeserializeObject<FoodHistoryMock>(responseBody);
        return foodHistory;
    }   
}