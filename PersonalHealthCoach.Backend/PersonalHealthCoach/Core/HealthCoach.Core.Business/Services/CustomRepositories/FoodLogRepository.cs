using HealthCoach.Core.Domain;
using HealthCoach.Shared.Core;
using Microsoft.EntityFrameworkCore;
using HealthCoach.Shared.Infrastructure;

namespace HealthCoach.Core.Business;

public class FoodLogRepository : IFoodLogRepository
{
    private readonly GenericDbContext dbContext;

    public FoodLogRepository(GenericDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task Store(Guid userId, IReadOnlyCollection<string> foods)
    {
        // Try to load the existing FoodLog from the database, including its ConsumedFoods
        var existingFoodLog = await dbContext.Set<FoodLog>()
            .AsNoTracking()
            .Include(e => e.ConsumedFoods)
            .FirstOrDefaultAsync(e => e.Id == userId);

        if (existingFoodLog == null)
        {
            // Create a new FoodLog using Instance method
            var newFoodLogResult = FoodLog.Instance(userId);
            if (newFoodLogResult.IsFailure)
            {
                // Handle the failure case if necessary, e.g., throw an exception or return an error
                throw new InvalidOperationException("Failed to create a new FoodLog instance.");
            }

            var newFoodLog = newFoodLogResult.Value;

            //create the new foods
            var newFoods = new List<ConsumedFood>();
            foreach (var food in foods)
            {
                var stringResult = food.EnsureNotNullOrEmpty("Empty");
                if (stringResult.IsFailure)
                {
                    continue;
                }

                var consumedFood = ConsumedFood.Create(stringResult.Value);
                newFoods.Add(consumedFood);
            }

            foreach (var consumedFood in newFoods)
            {
                consumedFood.ResetIsNew();
            }

            // Add the foods to the new FoodLog
            newFoodLog.AddFoods(newFoods);

            // Add the new FoodLog to the database
            dbContext.Set<FoodLog>().Add(newFoodLog);
        }
        else
        {
            // Create the new foods
            var newFoods = new List<ConsumedFood>();
            foreach (var food in foods)
            {
                var stringResult = food.EnsureNotNullOrEmpty("Empty");
                if (stringResult.IsFailure)
                {
                    continue;
                }

                var consumedFood = ConsumedFood.Create(stringResult.Value);
                newFoods.Add(consumedFood);
            }

            // Add the foods to the existing FoodLog
            existingFoodLog.AddFoods(newFoods);

            // Attach the updated FoodLog to the dbContext
            dbContext.Set<FoodLog>().Attach(existingFoodLog);

            // Mark the FoodLog as modified
            dbContext.Entry(existingFoodLog).State = EntityState.Modified;

            // Mark new ConsumedFoods as added
            foreach (var consumedFood in newFoods)
            {
                dbContext.Entry(consumedFood).State = EntityState.Added;
            }
        }

        await dbContext.SaveChangesAsync();
    }
}