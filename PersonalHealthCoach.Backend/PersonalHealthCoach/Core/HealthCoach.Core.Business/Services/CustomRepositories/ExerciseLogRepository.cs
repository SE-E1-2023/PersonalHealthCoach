using HealthCoach.Core.Domain;
using HealthCoach.Shared.Core;
using Microsoft.EntityFrameworkCore;
using HealthCoach.Shared.Infrastructure;

namespace HealthCoach.Core.Business;

public class ExerciseLogRepository : IExerciseLogRepository
{
    private readonly GenericDbContext dbContext;

    public ExerciseLogRepository(GenericDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task Store(Guid userId, IReadOnlyCollection<string> exercises)
    {
        // Try to load the existing ExerciseLog from the database, including its CompletedExercises
        var existingExerciseLog = await dbContext.Set<ExerciseLog>()
            .AsNoTracking()
            .Include(e => e.CompletedExercises)
            .FirstOrDefaultAsync(e => e.Id == userId);

        if (existingExerciseLog == null)
        {
            // Create a new ExerciseLog using Instance method
            var newExerciseLogResult = ExerciseLog.Instance(userId);
            if (newExerciseLogResult.IsFailure)
            {
                // Handle the failure case if necessary, e.g., throw an exception or return an error
                throw new InvalidOperationException("Failed to create a new ExerciseLog instance.");
            }

            var newExerciseLog = newExerciseLogResult.Value;

            //create the new exercises
            var newExercises = new List<CompletedExercise>();
            foreach (var exercise in exercises)
            {
                var stringResult = exercise.EnsureNotNullOrEmpty("Empty");
                if (stringResult.IsFailure)
                {
                    continue;
                }

                var completedExercise = CompletedExercise.Create(stringResult.Value);
                newExercises.Add(completedExercise);
            }
            
            foreach (var completedExercise in newExercises)
            {
                completedExercise.ResetIsNew();
            }

            // Add the exercises to the new ExerciseLog
            newExerciseLog.AddExercises(newExercises);

            // Add the new ExerciseLog to the database
            dbContext.Set<ExerciseLog>().Add(newExerciseLog);
        }
        else
        {
            // Create the new exercises
            var newExercises = new List<CompletedExercise>();
            foreach (var exercise in exercises)
            {
                var stringResult = exercise.EnsureNotNullOrEmpty("Empty");
                if (stringResult.IsFailure)
                {
                    continue;
                }

                var completedExercise = CompletedExercise.Create(stringResult.Value);
                newExercises.Add(completedExercise);
            }

            // Add the exercises to the existing ExerciseLog
            existingExerciseLog.AddExercises(newExercises);

            // Attach the updated ExerciseLog to the dbContext
            dbContext.Set<ExerciseLog>().Attach(existingExerciseLog);

            // Mark the ExerciseLog as modified
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