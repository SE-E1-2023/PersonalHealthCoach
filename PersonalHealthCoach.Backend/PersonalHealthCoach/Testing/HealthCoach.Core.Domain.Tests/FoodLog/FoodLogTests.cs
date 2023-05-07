﻿using Xunit;
using FluentAssertions;
using HealthCoach.Shared.Core;

namespace HealthCoach.Core.Domain.Tests;

public sealed class FoodLogTests
{
    [Fact]
    public void Given_Instance_Then_ShouldCreateInstance()
    {
        //Arrange
        var id = Guid.NewGuid();

        //Act
        var result = FoodLog.Instance(id);

        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Id.Should().Be(id);
        result.Value.ConsumedFoods.Should().BeEmpty();
    }

    [Fact]
    public void Given_AddFoods_Then_ShouldAddFoods()
    {
        //Arrange
        var foodsList = new List<ConsumedFood>
        {
            ConsumedFood.Create("Food no. 1"),
            ConsumedFood.Create("Food no. 2"),
            ConsumedFood.Create("Food no. 3"),
            ConsumedFood.Create("Food no. 4")
        };
        var foodLog = FoodLogsFactory.Any();
        var now = TimeProviderContext.AdvanceTimeToNow();

        //Act
        foodLog.AddFoods(foodsList);

        //Assert
        foodLog.ConsumedFoods.Should().HaveCount(4);
        foodLog.ConsumedFoods.Should().BeEquivalentTo(foodsList);
    }
}