namespace HealthCoach.Core.Domain.Tests;

public static class UsersFactory
{
    public static User Any() => User.Create("Doe", "John", "john.doe@gmail.com").Value;
}