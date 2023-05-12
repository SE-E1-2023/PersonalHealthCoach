using HealthCoach.Core.Domain;
using HealthCoach.Shared.Core;
using Microsoft.EntityFrameworkCore;
using HealthCoach.Shared.Infrastructure;

namespace HealthCoach.Core.Business;

public class FoodHistoryRepository : IFoodHistoryRepository
{
    private readonly GenericDbContext dbContext;

    public FoodHistoryRepository(GenericDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task Store(Guid userId, IReadOnlyCollection<Food> foods)
    {
        // Try to load the existing FoodHistory from the database, including its Foods
        var existingFoodLog = await dbContext.Set<FoodHistory>()
            .AsNoTracking()
            .Include(e => e.ConsumedFoods)
            .FirstOrDefaultAsync(e => e.Id == userId);

        if (existingFoodLog == null)
        {
            // Create a new FoodHistory using Instance method
            var newFoodLogResult = FoodHistory.Instance(userId);
            if (newFoodLogResult.IsFailure)
            {
                // Handle the failure case if necessary, e.g., throw an exception or return an error
                throw new InvalidOperationException("Failed to create a new FoodHistory instance.");
            }

            var newFoodLog = newFoodLogResult.Value;

            //create the new foodsAndCalories
            var newFoods = new List<ConsumedFood>();
            foreach (var food in foods)
            {
                var stringResult = food.Title.EnsureNotNullOrEmpty("Empty");
                if (stringResult.IsFailure)
                {
                    continue;
                }

                var consumedFood = ConsumedFood.Create(food.Title, food.Meal, food.Calories, food.Quantity);
                newFoods.Add(consumedFood);
            }

            foreach (var consumedFood in newFoods)
            {
                consumedFood.ResetIsNew();
            }

            // Add the foodsAndCalories to the new FoodHistory
            newFoodLog.AddFoods(newFoods);

            // Add the new FoodHistory to the database
            dbContext.Set<FoodHistory>().Add(newFoodLog);
        }
        else
        {
            // Create the new foodsAndCalories
            var newFoods = new List<ConsumedFood>();
            foreach (var food in foods)
            {
                var stringResult = food.Title.EnsureNotNullOrEmpty("Empty");
                if (stringResult.IsFailure)
                {
                    continue;
                }

                var consumedFood = ConsumedFood.Create(food.Title, food.Meal, food.Calories, food.Quantity);
                newFoods.Add(consumedFood);
            }

            // Add the foodsAndCalories to the existing FoodHistory
            existingFoodLog.AddFoods(newFoods);

            // Attach the updated FoodHistory to the dbContext
            dbContext.Set<FoodHistory>().Attach(existingFoodLog);

            // Mark the FoodHistory as modified
            dbContext.Entry(existingFoodLog).State = EntityState.Modified;

            // Mark new Foods as added
            foreach (var consumedFood in newFoods)
            {
                dbContext.Entry(consumedFood).State = EntityState.Added;
            }
        }

        await dbContext.SaveChangesAsync();
    }
}