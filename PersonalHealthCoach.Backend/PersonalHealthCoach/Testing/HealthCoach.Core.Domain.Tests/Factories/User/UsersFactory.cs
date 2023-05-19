namespace HealthCoach.Core.Domain.Tests;

public static class UsersFactory
{
    public static User Any() => User.Create("Doe", "John", "john.doe@gmail.com").Value;

    public static User AnyManager() => User.Create("Mister", "Manager", "manager@business.com", true).Value;
}