using System.ComponentModel;
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
            "M");

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

    public static void SetupPersonalData(Guid id)
    {
        var personalDataCommand = new AddPersonalDataCommand(id,PersonalDataConstants.MinimumDateOfBirth,80.5f,195f, new List<string> { "Asthma", "Allergies" },
                new List<string> { "Acne" }, PersonalDataConstants.AllowedGoals.ElementAt(0),
                new List<string> { "Boxing", "Cycling" }, 100 , 8, PersonalDataConstants.AllowedGenders.ElementAt(0));

        var json = JsonConvert.SerializeObject(personalDataCommand);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var client = new HttpClient();
        var response = client.PostAsync(string.Format(Routes.PersonalData.AddPersonalData, id), content).GetAwaiter().GetResult();
        var responseBody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

    }
}