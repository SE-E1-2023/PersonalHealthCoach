using HealthCoach.Core.Domain;
using HealthCoach.Shared.Core;
using Microsoft.EntityFrameworkCore;
using HealthCoach.Shared.Infrastructure;

namespace HealthCoach.Core.Business;

public class ExerciseHistoryRepository : IExerciseHistoryRepository
{
    private readonly GenericDbContext dbContext;

    public ExerciseHistoryRepository(GenericDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task Store(Guid userId, IReadOnlyCollection<Exercise> exercises)
    {
        // Try to load the existing ExerciseHistory from the database, including its CompletedExercises
        var existingExerciseLog = await dbContext.Set<ExerciseHistory>()
            .AsNoTracking()
            .Include(e => e.CompletedExercises)
            .FirstOrDefaultAsync(e => e.Id == userId);

        if (existingExerciseLog == null)
        {
            // Create a new ExerciseHistory using Instance method
            var newExerciseLogResult = ExerciseHistory.Instance(userId);
            if (newExerciseLogResult.IsFailure)
            {
                // Handle the failure case if necessary, e.g., throw an exception or return an error
                throw new InvalidOperationException("Failed to create a new ExerciseHistory instance.");
            }

            var newExerciseLog = newExerciseLogResult.Value;

            //create the new exercises
            var newExercises = new List<CompletedExercise>();
            foreach (var exercise in exercises)
            {
                var stringResult = exercise.Title.EnsureNotNullOrEmpty("Empty");
                if (stringResult.IsFailure)
                {
                    continue;
                }

                var completedExercise = CompletedExercise.Create(exercise.Title, exercise.Calories, exercise.Duration);
                newExercises.Add(completedExercise);
            }
            
            foreach (var completedExercise in newExercises)
            {
                completedExercise.ResetIsNew();
            }

            // Add the exercises to the new ExerciseHistory
            newExerciseLog.AddExercises(newExercises);

            // Add the new ExerciseHistory to the database
            dbContext.Set<ExerciseHistory>().Add(newExerciseLog);
        }
        else
        {
            // Create the new exercises
            var newExercises = new List<CompletedExercise>();
            foreach (var exercise in exercises)
            {
                var stringResult = exercise.Title.EnsureNotNullOrEmpty("Empty");
                if (stringResult.IsFailure)
                {
                    continue;
                }

                var completedExercise = CompletedExercise.Create(exercise.Title, exercise.Calories, exercise.Duration);
                newExercises.Add(completedExercise);
            }

            // Add the exercises to the existing ExerciseHistory
            existingExerciseLog.AddExercises(newExercises);

            // Attach the updated ExerciseHistory to the dbContext
            dbContext.Set<ExerciseHistory>().Attach(existingExerciseLog);

            // Mark the ExerciseHistory as modified
            dbContext.Entry(existingExerciseLog).State = EntityState.Modified;

            // Mark new CompletedExercises as added
            foreach (var completedExercise in newExercises)
            {
                dbContext.Entry(completedExercise).State = EntityState.Added;
            }
        }

        await dbContext.SaveChangesAsync();
    }
}