﻿using HealthCoach.Core.Domain;
using CSharpFunctionalExtensions;
using HealthCoach.Shared.Core;
using HealthCoach.Shared.Infrastructure;

namespace HealthCoach.Core.Business;

public class DbPopulationService
{
    private readonly IRepository repository;
    private readonly IEfQueryProvider queryProvider;
    private readonly DateTime now = TimeProvider.Instance().UtcNow;

    public DbPopulationService(IRepository repository, IEfQueryProvider queryProvider)
    {
        this.repository = repository;
        this.queryProvider = queryProvider;
    }

    public async Task PopulateDb()
    {
        await DeleteAllFromDatabase();

        await PopulateUsers();
        await PopulatePersonalData();
        await PopulateWellnessTips();
    }

    private async Task DeleteAllFromDatabase()
    {
        var users = queryProvider.Query<User>().ToList();
        var personalData = queryProvider.Query<PersonalData>().ToList();
        var wellnessTips = queryProvider.Query<WellnessTip>().ToList();

        foreach (var user in users)
        {
            await repository.Delete(user);
        }

        foreach (var data in personalData)
        {
            await repository.Delete(data);
        }

        foreach (var tip in wellnessTips)
        {
            await repository.Delete(tip);
        }
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
            User.Create("Popa","Stefan","popastefan@gmail.com"),
            User.Create("Application","Manager","applicationmanager@gmail.com", true)
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
            PersonalData.Create(users[0].Id, PersonalDataConstants.MinimumDateOfBirth, 70.0f, 175f,
                new List<string> {"Asthma", "Allergies", "High blood pressure"},
                new List<string> {"Anxiety"},PersonalDataConstants.AllowedGoals.ElementAt(0),
                new List<string> { "Weight lifting", "Cycling"}, 10000, 8.5, "M", now,
                true, 5, true, true, true, true, true, true, true, true, false, true, true, true, false),
            PersonalData.Create(users[0].Id,PersonalDataConstants.MinimumDateOfBirth,75.0f,175f,
                new List<string> {"Asthma", "Allergies", "High blood pressure"},
                new List<string> {"Anxiety"},PersonalDataConstants.AllowedGoals.ElementAt(0),
                new List<string> { "Weight lifting", "Cycling"}, 100200, 4.5, "M", now.AddDays(-1),
                true, 5, true, true, true, true, true, true, true, true, false, true, true, true, false),
            PersonalData.Create(users[0].Id,PersonalDataConstants.MinimumDateOfBirth,80.0f,175f,
                new List<string> {"Asthma", "Allergies", "High blood pressure"},
                new List<string> {"Anxiety"},PersonalDataConstants.AllowedGoals.ElementAt(0),
                new List<string> { "Weight lifting", "Cycling"}, 16543, 12, "M", now.AddDays(-2),
                true, 5, true, true, true, true, true, true, true, true, false, true, true, true, false),
            PersonalData.Create(users[0].Id,PersonalDataConstants.MinimumDateOfBirth,85.0f,175f,
                new List<string> {"Asthma", "Allergies", "High blood pressure"},
                new List<string> {"Anxiety"},PersonalDataConstants.AllowedGoals.ElementAt(0),
                new List<string> { "Weight lifting", "Cycling"}, 1000, 8.5, "M", now.AddDays(-3),
                true, 5, true, true, true, true, true, true, true, true, false, true, true, true, false),
            PersonalData.Create(users[0].Id,PersonalDataConstants.MinimumDateOfBirth,85.5f,175f,
                new List<string> {"Asthma", "Allergies", "High blood pressure"},
                new List<string> {"Anxiety"},PersonalDataConstants.AllowedGoals.ElementAt(0),
                new List<string> { "Weight lifting", "Cycling"}, 20000, 9, "M", now.AddDays(-4),
                true, 5, true, true, true, true, true, true, true, true, false, true, true, true, false),
            PersonalData.Create(users[0].Id,PersonalDataConstants.MinimumDateOfBirth,80.0f,175f,
                new List<string> {"Asthma", "Allergies", "High blood pressure"},
                new List<string> {"Anxiety"},PersonalDataConstants.AllowedGoals.ElementAt(0),
                new List<string> { "Weight lifting", "Cycling"}, 16543, 12, "M", now.AddDays(-5),
                true, 5, true, true, true, true, true, true, true, true, false, true, true, true, false),
            PersonalData.Create(users[0].Id,PersonalDataConstants.MinimumDateOfBirth,85.0f,175f,
                new List<string> {"Asthma", "Allergies", "High blood pressure"},
                new List<string> {"Anxiety"},PersonalDataConstants.AllowedGoals.ElementAt(0),
                new List<string> { "Weight lifting", "Cycling"}, 1000, 8.5, "M", now.AddDays(-6),
                true, 5, true, true, true, true, true, true, true, true, false, true, true, true, false),


            PersonalData.Create(users[1].Id,PersonalDataConstants.MinimumDateOfBirth,75.5f,185f,
                new List<string> {"High blood pressure"},
                new List<string> {"Cancer"},PersonalDataConstants.AllowedGoals.ElementAt(0),
                new List<string> {"Running"}, 20000, 9, "M", now.AddDays(0),
                true, 5, true, true, true, true, true, true, true, true, false, true, true, true, false),
            PersonalData.Create(users[1].Id,PersonalDataConstants.MinimumDateOfBirth,70.0f,185f,
                new List<string> {"High blood pressure"},
                new List<string> {"Cancer"},PersonalDataConstants.AllowedGoals.ElementAt(0),
                new List<string> {"Running"}, 10000, 9, "M", now.AddDays(-1),
                true, 5, true, true, true, true, true, true, true, true, false, true, true, true, false),
            PersonalData.Create(users[1].Id,PersonalDataConstants.MinimumDateOfBirth,65.5f,185f,
                new List<string> {"High blood pressure"},
                new List<string> {"Cancer"},PersonalDataConstants.AllowedGoals.ElementAt(0),
                new List<string> {"Running"}, 2000, 9, "M", now.AddDays(-2),
                true, 5, true, true, true, true, true, true, true, true, false, true, true, true, false),
            PersonalData.Create(users[1].Id,PersonalDataConstants.MinimumDateOfBirth,60.0f,185f,
                new List<string> {"High blood pressure"},
                new List<string> {"Cancer"},PersonalDataConstants.AllowedGoals.ElementAt(0),
                new List<string> {"Running"}, 20000, 9, "M", now.AddDays(-3),
                true, 5, true, true, true, true, true, true, true, true, false, true, true, true, false),
            PersonalData.Create(users[1].Id,PersonalDataConstants.MinimumDateOfBirth,55.5f,185f,
                new List<string> {"High blood pressure"},
                new List<string> {"Cancer"},PersonalDataConstants.AllowedGoals.ElementAt(0),
                new List<string> {"Running"}, 10000, 9, "M", now.AddDays(-4),
                true, 5, true, true, true, true, true, true, true, true, false, true, true, true, false),
            PersonalData.Create(users[1].Id,PersonalDataConstants.MinimumDateOfBirth,50.0f,185f,
                new List<string> {"High blood pressure"},
                new List<string> {"Cancer"},PersonalDataConstants.AllowedGoals.ElementAt(0),
                new List<string> {"Running"}, 2000, 9, "M", now.AddDays(-5),
                true, 5, true, true, true, true, true, true, true, true, false, true, true, true, false),
            PersonalData.Create(users[1].Id,PersonalDataConstants.MinimumDateOfBirth,45.5f,185f,
                new List<string> {"High blood pressure"},
                new List<string> {"Cancer"},PersonalDataConstants.AllowedGoals.ElementAt(0),
                new List<string> {"Running"}, 20000, 9, "M", now.AddDays(-6),
                true, 5, true, true, true, true, true, true, true, true, false, true, true, true, false),


            PersonalData.Create(users[2].Id,PersonalDataConstants.MinimumDateOfBirth,80.0f,165f,
                new List<string> { "Diabetes", "Depression", "High blood pressure"},
                new List<string> {"Back pain"},PersonalDataConstants.AllowedGoals.ElementAt(0),
                new List<string> {"Running", "Swimming"}, 2000, 5, "F", now.AddDays(0),                true, 5, true, true, true, true, true, true, true, true, false, true, true, true, false),

            PersonalData.Create(users[2].Id,PersonalDataConstants.MinimumDateOfBirth,75.5f,165f,
                new List<string> { "Diabetes", "Depression", "High blood pressure"},
                new List<string> {"Back pain"},PersonalDataConstants.AllowedGoals.ElementAt(0),
                new List<string> {"Running", "Swimming"}, 12000, 9, "F", now.AddDays(-1),                true, 5, true, true, true, true, true, true, true, true, false, true, true, true, false),

            PersonalData.Create(users[2].Id,PersonalDataConstants.MinimumDateOfBirth,70.5f,165f,
                new List<string> { "Diabetes", "Depression", "High blood pressure"},
                new List<string> {"Back pain"},PersonalDataConstants.AllowedGoals.ElementAt(0),
                new List<string> {"Running", "Swimming"}, 7000, 9, "F", now.AddDays(-2),                true, 5, true, true, true, true, true, true, true, true, false, true, true, true, false),

            PersonalData.Create(users[2].Id,PersonalDataConstants.MinimumDateOfBirth,75.5f,165f,
                new List<string> { "Diabetes", "Depression", "High blood pressure"},
                new List<string> {"Back pain"},PersonalDataConstants.AllowedGoals.ElementAt(0),
                new List<string> {"Running", "Swimming"}, 12000, 5, "F", now.AddDays(-3),                true, 5, true, true, true, true, true, true, true, true, false, true, true, true, false),

            PersonalData.Create(users[2].Id,PersonalDataConstants.MinimumDateOfBirth,75.0f,165f,
                new List<string> { "Diabetes", "Depression", "High blood pressure"},
                new List<string> {"Back pain"},PersonalDataConstants.AllowedGoals.ElementAt(0),
                new List<string> {"Running", "Swimming"}, 10000, 10, "F", now.AddDays(-4),                true, 5, true, true, true, true, true, true, true, true, false, true, true, true, false),


            PersonalData.Create(users[3].Id,PersonalDataConstants.MinimumDateOfBirth,85.5f,175f,
                new List<string> {"Asthma", "Diabetes", "High blood pressure"},
                new List<string> {"Anxiety"},PersonalDataConstants.AllowedGoals.ElementAt(0),
                new List<string> { "Boxing", "Cycling"}, 10000, 10, "F", now.AddDays(0),
                true, 5, true, true, true, true, true, true, true, true, false, true, true, true, false),
            PersonalData.Create(users[3].Id,PersonalDataConstants.MinimumDateOfBirth,80.5f,175f,
                new List<string> {"Asthma", "Diabetes", "High blood pressure"},
                new List<string> {"Anxiety"},PersonalDataConstants.AllowedGoals.ElementAt(0),
                new List<string> { "Boxing", "Cycling"}, 12000, 10, "F", now.AddDays(-1),
                true, 5, true, true, true, true, true, true, true, true, false, true, true, true, false),
            PersonalData.Create(users[3].Id,PersonalDataConstants.MinimumDateOfBirth,80.0f,175f,
                new List<string> {"Asthma", "Diabetes", "High blood pressure"},
                new List<string> {"Anxiety"},PersonalDataConstants.AllowedGoals.ElementAt(0),
                new List<string> { "Boxing", "Cycling"}, 13200, 10, "F", now.AddDays(-2),
                true, 5, true, true, true, true, true, true, true, true, false, true, true, true, false),
            PersonalData.Create(users[3].Id,PersonalDataConstants.MinimumDateOfBirth,80.0f,175f,
                new List<string> {"Asthma", "Diabetes", "High blood pressure"},
                new List<string> {"Anxiety"},PersonalDataConstants.AllowedGoals.ElementAt(0),
                new List<string> { "Boxing", "Cycling"}, 10020, 10, "F", now.AddDays(-3),
                true, 5, true, true, true, true, true, true, true, true, false, true, true, true, false),
            PersonalData.Create(users[3].Id,PersonalDataConstants.MinimumDateOfBirth,80.5f,175f,
                new List<string> {"Asthma", "Diabetes", "High blood pressure"},
                new List<string> {"Anxiety"},PersonalDataConstants.AllowedGoals.ElementAt(0),
                new List<string> { "Boxing", "Cycling"}, 20000, 10, "F", now.AddDays(-4),
                true, 5, true, true, true, true, true, true, true, true, false, true, true, true, false),

            PersonalData.Create(users[4].Id,PersonalDataConstants.MinimumDateOfBirth,75.5f,155f,
                new List<string> { "Depression", "Allergies", "High blood pressure"},
                new List<string> {"Cancer"},PersonalDataConstants.AllowedGoals.ElementAt(0),
                new List<string> {"Running", "Weight lifting"}, 20000, 9, "M",
                true, 5, true, true, true, true, true, true, true, true, false, true, true, true, false),
            PersonalData.Create(users[4].Id,PersonalDataConstants.MinimumDateOfBirth,80.5f,155f,
                new List<string> { "Depression", "Allergies", "High blood pressure"},
                new List<string> {"Cancer"},PersonalDataConstants.AllowedGoals.ElementAt(0),
                new List<string> {"Running", "Weight lifting"}, 20000, 9, "M",
                true, 5, true, true, true, true, true, true, true, true, false, true, true, true, false),
            PersonalData.Create(users[4].Id,PersonalDataConstants.MinimumDateOfBirth,75.5f,155f,
                new List<string> { "Depression", "Allergies", "High blood pressure"},
                new List<string> {"Cancer"},PersonalDataConstants.AllowedGoals.ElementAt(0),
                new List<string> {"Running", "Weight lifting"}, 10000, 9, "M",
                true, 5, true, true, true, true, true, true, true, true, false, true, true, true, false),
            PersonalData.Create(users[4].Id,PersonalDataConstants.MinimumDateOfBirth,75.0f,155f,
                new List<string> { "Depression", "Allergies", "High blood pressure"},
                new List<string> {"Cancer"},PersonalDataConstants.AllowedGoals.ElementAt(0),
                new List<string> {"Running", "Weight lifting"}, 20000, 9, "M",
                true, 5, true, true, true, true, true, true, true, true, false, true, true, true, false),
            PersonalData.Create(users[4].Id,PersonalDataConstants.MinimumDateOfBirth,75.5f,155f,
                new List<string> { "Depression", "Allergies", "High blood pressure"},
                new List<string> {"Cancer"},PersonalDataConstants.AllowedGoals.ElementAt(0),
                new List<string> {"Running", "Weight lifting"}, 20000, 9, "M",
                true, 5, true, true, true, true, true, true, true, true, false, true, true, true, false),
        };

        // Query the db for each PersonalData class and check if the objects above exist and if not, add just the missing ones
        var dbPersonalData = queryProvider.Query<PersonalData>().ToList();
        foreach (var user in users)
        {
            var userData = dbPersonalData.Where(p => p.UserId == user.Id).ToList();
            if (userData.Count > 15)
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

    private async Task PopulateWellnessTips()
    {
        var wellnessTipList = new List<Result<WellnessTip>>
        {
            WellnessTip.Create("Your body goes quite a few hours without hydration as you sleep. Drinking a full glass of water in the morning can aid digestion, flush out toxins, enhance skin health and give you an energy boost."),
            WellnessTip.Create("Wake up and do something that inspires you like journaling, walking in nature, or other hobbies. Whether it’s productive or relaxing, beginning your morning on the right foot can cultivate a positive mindset and set the tone for the entire day."),
            WellnessTip.Create("Sleep is just as important as eating healthy and exercising. From improving your productivity and concentration to helping support your overall health, getting the recommended hours of sleep per night can have a major impact on your wellbeing."),
            WellnessTip.Create("Whether you get outside for some exercise or to read a book in the sunshine, you should take at least 30 minutes a day to get some vitamin D."),
            WellnessTip.Create("Try the stairs instead of the elevator, take short walks around your office or ride a bike instead of driving. Vigorous exercise is essential but moving throughout the day will keep you energized, as well as benefit your mind and body."),
            WellnessTip.Create("Tracking your steps will help you see how much you’ve actually moved throughout the day and may even encourage you to challenge yourself to reach a certain amount of steps every day."),
            WellnessTip.Create("Eyes become easily strained when you’re constantly focused on your computer screen. Reduce the risk of tired eyes by looking away from your computer for at least 20 seconds in 20-minute intervals."),
            WellnessTip.Create("Real food is whole, single-ingredient foods that are unprocessed and free of additives. Incorporating these foods into your day can help improve your health, manage your weight and give you energy."),
            WellnessTip.Create("It’s easy to mindlessly snack throughout the day, so make sure your snack choices aren’t weighing you down. Mixed nuts, veggies, Greek yogurt or even a piece of dark chocolate are all great options that will keep you feeling satisfied."),
            WellnessTip.Create("Multivitamins contain vitamins and minerals that are essential to your health. No matter how healthy you eat or what diet you follow, it can be difficult for your body to get all of the nutrients it needs from food. Research your options and find a multivitamin that fits your needs and supports your health."),
            WellnessTip.Create("Spending time alone can be extremely beneficial for your mental health. Get to know yourself, figure out what you want and start living your most purposeful life."),
            WellnessTip.Create("Join a book club, sign up for a class, start cooking. Try something new at least once a month. Making a point to keep learning throughout your life can keep your mind lively and engaged."),
            WellnessTip.Create("Visit your dentist twice a year, make sure you’re getting those annual checkups at the doctor and schedule the recommended screenings for your age group. Make your health a number one priority."),
            WellnessTip.Create("Spending time with your friends and family may not seem like a top wellness tip but it is vital. Humans are social beings and rely on other humans to maintain their mental, emotional and physical help. Setting time aside to spend with your loved ones can help relieve stress, increase self-esteem and lead you to make more positive choices."),
            WellnessTip.Create("Building a daily skin care routine can help you maintain overall skin health or improve concerns like acne, scarring or dark spots. Find a cleanser, serum, moisturizer and sunscreen and give your skin the love it deserves."),
            WellnessTip.Create("The blue light emitted by your tech devices may be the cause of those restless nights. Put your phone or laptop away at least an hour before bed to set yourself up for a good night of sleep."),
            WellnessTip.Create("Money worries are oftentimes a big source of stress for some people. Saving for the future, home mortgages and paying off loans—it all adds up. Create a priority list for yourself and determine a realistic budget to provide yourself some relief."),
            WellnessTip.Create("Oftentimes, we tend to focus on what we’re lacking in life instead of focusing on the things that we do have. Start measuring your worth by your successes rather than your deficits by keeping track of the things that go well in your life."),
        };

        var dbWellnessTipList = queryProvider.Query<WellnessTip>().ToList();
        foreach (var tip in wellnessTipList)
        {
            if (tip.IsSuccess && dbWellnessTipList.Any(dbTip => dbTip.TipText == tip.Value.TipText))
            {
                continue;
            }

            await tip.Tap(t => repository.Store(t));
        }
    }
}