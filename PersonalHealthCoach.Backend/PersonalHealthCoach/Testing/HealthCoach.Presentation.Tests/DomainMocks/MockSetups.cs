using System.ComponentModel;
using System.Text;
using HealthCoach.Core.Business;
using HealthCoach.Core.Domain;
using Newtonsoft.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
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

    public static PersonalDataMock SetupPersonalData(Guid userId)
    {
        var client = new HttpClient();

        var addPersonalDataCommand = new AddPersonalDataCommand(userId, 
            PersonalDataConstants.MinimumDateOfBirth, 
            80.5f, 
            120, 
            null, 
            null, 
            PersonalDataConstants.AllowedGoals.First(), 
            null, 
            14, 
            6, 
            "M", true, 5, true, true, true, true, true, true, true, true, false, true, true, true, false);

        var json = JsonConvert.SerializeObject(addPersonalDataCommand);
        var content = new StringContent(json, Encoding .UTF8, "application/json");

        var response = client.PostAsync(string.Format(Routes.PersonalData.AddPersonalData, userId), content).GetAwaiter().GetResult();
        var respnseBody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

        var personalData = JsonConvert.DeserializeObject<PersonalDataMock>(respnseBody);

        return personalData;
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

    public static FitnessPlanMock SetupFitnessPlan(Guid id)
    {
        var client = new HttpClient();
        MockSetups.SetupPersonalData(id);
        var command = new CreateFitnessPlanCommand(id, 5);
        var json = JsonConvert.SerializeObject(command);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = client.PostAsync(string.Format(Routes.FitnessPlan.CreateFitnessPlan, id), content).GetAwaiter().GetResult();
        var responseBody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        var fitnessPlan = JsonConvert.DeserializeObject<FitnessPlanMock>(responseBody);
        return fitnessPlan;
    }

    public static DietPlanMock SetupDietPlan(Guid id)
    {
        var client = new HttpClient();
        MockSetups.SetupPersonalData(id);
        var command = new CreateDietPlanCommand(id);

        var json = JsonConvert.SerializeObject(command);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = client.PostAsync(string.Format(Routes.DietPlan.GetDietPlan, id), content).GetAwaiter().GetResult();
        var responseBody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        var dietPlan = JsonConvert.DeserializeObject<DietPlanMock>(responseBody);
        return dietPlan;
    }
}