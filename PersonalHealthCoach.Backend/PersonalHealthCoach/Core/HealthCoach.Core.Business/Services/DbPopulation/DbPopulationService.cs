using System.Data.Common;
using System.Security.Cryptography;
using HealthCoach.Core.Domain;
using CSharpFunctionalExtensions;
using HealthCoach.Shared.Core;
using Microsoft.EntityFrameworkCore;
using HealthCoach.Shared.Infrastructure;

namespace HealthCoach.Core.Business;

public class DbPopulationService
{
    private readonly IRepository repository;
    private readonly IEfQueryProvider queryProvider;

    public DbPopulationService(IRepository repository, IEfQueryProvider queryProvider)
    {
        this.repository = repository;
        this.queryProvider = queryProvider;
    }

    public async Task PopulateDb()
    {
        await PopulateUsers();
        await PopulatePersonalData();
    }

    private async Task PopulateUsers()
    {
        var users = new List<Result<User>>
        {
            User.Create("Tescu", "Tudor", "tudor.tescu@hotmail.com"),
            User.Create("Pintea", "Fabian", "pintea2002@gmail.com"),
            User.Create("Stirbu","Larisa","stirbularisa@gmail.com"),
            User.Create("Radeanu","Roxana","roxanaradeanu@gmail.com"),
            User.Create("Rusu","Vlad","rusuvlad@gmail.com"),
            User.Create("Popa","Stefan","popastefan@gmail.com")
        };

        var dbUsers = queryProvider.Query<User>().ToList();

        foreach (var user in users)
        {
            if (user.IsSuccess && dbUsers.Any(u => u.EmailAddress == user.Value.EmailAddress))
            {
                continue;
            }
            
            await user.Tap(u => repository.Store(u));
        }
    }

    private async Task PopulatePersonalData()
    {
        
        var users = queryProvider.Query<User>().ToList();
        var personalDataList = new List<Result<PersonalData>>
        {
            PersonalData.Create(users[0].Id,PersonalDataConstants.MinimumDateOfBirth,70.0f,175f,
                new List<string> {"Asthma", "Allergies", "High blood pressure"},
                new List<string> {"Anxiety"},PersonalDataConstants.AllowedGoals.ElementAt(0),
                new List<string> { "Weight lifting", "Cycling"}),
            //multiple personal data for the same user
            PersonalData.Create(users[0].Id,PersonalDataConstants.MinimumDateOfBirth,75.0f,175f,
                new List<string> {"Asthma", "Allergies", "High blood pressure"},
                new List<string> {"Anxiety"},PersonalDataConstants.AllowedGoals.ElementAt(0),
                new List<string> { "Weight lifting", "Cycling"}),
            PersonalData.Create(users[0].Id,PersonalDataConstants.MinimumDateOfBirth,80.0f,175f,
                new List<string> {"Asthma", "Allergies", "High blood pressure"},
                new List<string> {"Anxiety"},PersonalDataConstants.AllowedGoals.ElementAt(0),
                new List<string> { "Weight lifting", "Cycling"}),
            PersonalData.Create(users[0].Id,PersonalDataConstants.MinimumDateOfBirth,85.0f,175f,
                new List<string> {"Asthma", "Allergies", "High blood pressure"},
                new List<string> {"Anxiety"},PersonalDataConstants.AllowedGoals.ElementAt(0),
                new List<string> { "Weight lifting", "Cycling"}),
            PersonalData.Create(users[0].Id,PersonalDataConstants.MinimumDateOfBirth,85.5f,175f,
                new List<string> {"Asthma", "Allergies", "High blood pressure"},
                new List<string> {"Anxiety"},PersonalDataConstants.AllowedGoals.ElementAt(0),
                new List<string> { "Weight lifting", "Cycling"}),


            PersonalData.Create(users[1].Id,PersonalDataConstants.MinimumDateOfBirth,75.5f,185f,
                new List<string> {"High blood pressure"},
                new List<string> {"Cancer"},PersonalDataConstants.AllowedGoals.ElementAt(0),
                new List<string> {"Running"}),
            PersonalData.Create(users[1].Id,PersonalDataConstants.MinimumDateOfBirth,70.0f,185f,
                new List<string> {"High blood pressure"},
                new List<string> {"Cancer"},PersonalDataConstants.AllowedGoals.ElementAt(0),
                new List<string> {"Running"}),
            PersonalData.Create(users[1].Id,PersonalDataConstants.MinimumDateOfBirth,65.5f,185f,
                new List<string> {"High blood pressure"},
                new List<string> {"Cancer"},PersonalDataConstants.AllowedGoals.ElementAt(0),
                new List<string> {"Running"}),
            PersonalData.Create(users[1].Id,PersonalDataConstants.MinimumDateOfBirth,60.0f,185f,
                new List<string> {"High blood pressure"},
                new List<string> {"Cancer"},PersonalDataConstants.AllowedGoals.ElementAt(0),
                new List<string> {"Running"}),


            PersonalData.Create(users[2].Id,PersonalDataConstants.MinimumDateOfBirth,80.0f,165f,
                new List<string> { "Diabetes", "Depression", "High blood pressure"},
                new List<string> {"Back pain"},PersonalDataConstants.AllowedGoals.ElementAt(0),
                new List<string> {"Running", "Swimming"}),
            PersonalData.Create(users[2].Id,PersonalDataConstants.MinimumDateOfBirth,75.5f,165f,
                new List<string> { "Diabetes", "Depression", "High blood pressure"},
                new List<string> {"Back pain"},PersonalDataConstants.AllowedGoals.ElementAt(0),
                new List<string> {"Running", "Swimming"}),
            PersonalData.Create(users[2].Id,PersonalDataConstants.MinimumDateOfBirth,70.5f,165f,
                new List<string> { "Diabetes", "Depression", "High blood pressure"},
                new List<string> {"Back pain"},PersonalDataConstants.AllowedGoals.ElementAt(0),
                new List<string> {"Running", "Swimming"}),
            PersonalData.Create(users[2].Id,PersonalDataConstants.MinimumDateOfBirth,75.5f,165f,
                new List<string> { "Diabetes", "Depression", "High blood pressure"},
                new List<string> {"Back pain"},PersonalDataConstants.AllowedGoals.ElementAt(0),
                new List<string> {"Running", "Swimming"}),
            PersonalData.Create(users[2].Id,PersonalDataConstants.MinimumDateOfBirth,75.0f,165f,
                new List<string> { "Diabetes", "Depression", "High blood pressure"},
                new List<string> {"Back pain"},PersonalDataConstants.AllowedGoals.ElementAt(0),
                new List<string> {"Running", "Swimming"}),


            PersonalData.Create(users[3].Id,PersonalDataConstants.MinimumDateOfBirth,85.5f,175f,
                new List<string> {"Asthma", "Diabetes", "High blood pressure"},
                new List<string> {"Anxiety"},PersonalDataConstants.AllowedGoals.ElementAt(0),
                new List<string> { "Boxing", "Cycling"}),
            PersonalData.Create(users[3].Id,PersonalDataConstants.MinimumDateOfBirth,80.5f,175f,
                new List<string> {"Asthma", "Diabetes", "High blood pressure"},
                new List<string> {"Anxiety"},PersonalDataConstants.AllowedGoals.ElementAt(0),
                new List<string> { "Boxing", "Cycling"}),
            PersonalData.Create(users[3].Id,PersonalDataConstants.MinimumDateOfBirth,80.0f,175f,
                new List<string> {"Asthma", "Diabetes", "High blood pressure"},
                new List<string> {"Anxiety"},PersonalDataConstants.AllowedGoals.ElementAt(0),
                new List<string> { "Boxing", "Cycling"}),
            PersonalData.Create(users[3].Id,PersonalDataConstants.MinimumDateOfBirth,80.0f,175f,
                new List<string> {"Asthma", "Diabetes", "High blood pressure"},
                new List<string> {"Anxiety"},PersonalDataConstants.AllowedGoals.ElementAt(0),
                new List<string> { "Boxing", "Cycling"}),
            PersonalData.Create(users[3].Id,PersonalDataConstants.MinimumDateOfBirth,80.5f,175f,
                new List<string> {"Asthma", "Diabetes", "High blood pressure"},
                new List<string> {"Anxiety"},PersonalDataConstants.AllowedGoals.ElementAt(0),
                new List<string> { "Boxing", "Cycling"}),


            PersonalData.Create(users[4].Id,PersonalDataConstants.MinimumDateOfBirth,75.5f,155f,
                new List<string> { "Depression", "Allergies", "High blood pressure"},
                new List<string> {"Cancer"},PersonalDataConstants.AllowedGoals.ElementAt(0),
                new List<string> {"Running", "Weight lifting"}),
            PersonalData.Create(users[4].Id,PersonalDataConstants.MinimumDateOfBirth,80.5f,155f,
                new List<string> { "Depression", "Allergies", "High blood pressure"},
                new List<string> {"Cancer"},PersonalDataConstants.AllowedGoals.ElementAt(0),
                new List<string> {"Running", "Weight lifting"}),
            PersonalData.Create(users[4].Id,PersonalDataConstants.MinimumDateOfBirth,75.5f,155f,
                new List<string> { "Depression", "Allergies", "High blood pressure"},
                new List<string> {"Cancer"},PersonalDataConstants.AllowedGoals.ElementAt(0),
                new List<string> {"Running", "Weight lifting"}),
            PersonalData.Create(users[4].Id,PersonalDataConstants.MinimumDateOfBirth,75.0f,155f,
                new List<string> { "Depression", "Allergies", "High blood pressure"},
                new List<string> {"Cancer"},PersonalDataConstants.AllowedGoals.ElementAt(0),
                new List<string> {"Running", "Weight lifting"}),
            PersonalData.Create(users[4].Id,PersonalDataConstants.MinimumDateOfBirth,75.5f,155f,
                new List<string> { "Depression", "Allergies", "High blood pressure"},
                new List<string> {"Cancer"},PersonalDataConstants.AllowedGoals.ElementAt(0),
                new List<string> {"Running", "Weight lifting"}),


            PersonalData.Create(users[5].Id,PersonalDataConstants.MinimumDateOfBirth,80.5f,195f,
                new List<string> {"Asthma", "Allergies"},
                new List<string> {"Acne"},PersonalDataConstants.AllowedGoals.ElementAt(0),
                new List<string> { "Boxing", "Cycling"}),
            PersonalData.Create(users[5].Id,PersonalDataConstants.MinimumDateOfBirth,75.5f,195f,
                            new List<string> {"Asthma", "Allergies"},
                            new List<string> {"Acne"},PersonalDataConstants.AllowedGoals.ElementAt(0),
                            new List<string> { "Boxing", "Cycling"}),
            PersonalData.Create(users[5].Id,PersonalDataConstants.MinimumDateOfBirth,80.5f,195f,
                            new List<string> {"Asthma", "Allergies"},
                            new List<string> {"Acne"},PersonalDataConstants.AllowedGoals.ElementAt(0),
                            new List<string> { "Boxing", "Cycling"}),
            PersonalData.Create(users[5].Id,PersonalDataConstants.MinimumDateOfBirth,80.0f,195f,
                            new List<string> {"Asthma", "Allergies"},
                            new List<string> {"Acne"},PersonalDataConstants.AllowedGoals.ElementAt(0),
                            new List<string> { "Boxing", "Cycling"})

        };

        // Query the db for each PersonalData class and check if the objects above exist and if not, add just the missing ones
        var dbPersonalData = queryProvider.Query<PersonalData>().ToList();
        foreach (var user in users)
        {
            var userdata = dbPersonalData.Where(p => p.UserId == user.Id).ToList();
            if (userdata.Count > 15)
            {
                continue;
            }

            foreach (var personalData in personalDataList)
            {
                if (personalData.IsFailure)
                {
                    continue;
                }

                if (personalData.Value.UserId == user.Id)
                {
                    await personalData.Tap(p => repository.Store(p));
                }
            }
        }
    }

}