﻿namespace HealthCoach.Core.Domain.Tests;

public static class PlansFactory
{
    public static class FitnessPlans
    {
        public static FitnessPlan Any() => FitnessPlan.Create(Guid.NewGuid(), Exercises.Any()).Value;
    }

    public static class Exercises
    {
        public static IReadOnlyCollection<Exercise> Any() => new List<Exercise>
        {
            Exercise.Create("Exercise 1", "Description 1", "1-2 min", 10, "Strength"),
            Exercise.Create("Exercise 1", "Description 1", "1-2 min", 10, "Strength"),
            Exercise.Create("Exercise 1", "Description 1", "1-2 min", 10, "Strength"),
            Exercise.Create("Exercise 1", "Description 1", "1-2 min", 10, "Strength"),
            Exercise.Create("Exercise 1", "Description 1", "1-2 min", 10, "Strength"),
            Exercise.Create("Exercise 1", "Description 1", "1-2 min", 10, "Strength"),
            Exercise.Create("Exercise 1", "Description 1", "1-2 min", 10, "Strength"),
            Exercise.Create("Exercise 1", "Description 1", "1-2 min", 10, "Strength")
        };
    }

    public static class DietPlans
    {
        public static DietPlan Any() => DietPlan.Create(Guid.NewGuid(), "name", "lol", new List<string>(), new List<string>(), new List<string>(), new Meal("a", new List<string>(), 100, "a"), new Meal("a", new List<string>(), 100, "a"), new Meal("a", new List<string>(), 100, "a"), new Meal("a", new List<string>(), 100, "a"), new Meal("a", new List<string>(), 100, "a"), new Meal("a", new List<string>(), 100, "a")).Value;
    }
}