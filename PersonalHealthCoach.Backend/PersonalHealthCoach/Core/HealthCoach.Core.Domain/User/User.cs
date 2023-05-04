using EmailValidation;
using HealthCoach.Shared.Core;
using CSharpFunctionalExtensions;

using Errors = HealthCoach.Core.Domain.DomainErrors.User.Create;

namespace HealthCoach.Core.Domain;

public sealed class User : AggregateRoot
{
    public User() { }

    private User(string name, string firstName, string emailAddress)
    {
        this.Name = name;
        this.FirstName = firstName;
        this.EmailAddress = emailAddress;
    }

    public static Result<User> Create(string name, string firstName, string emailAddress)
    {
        var nameResult = name.EnsureNotNullOrEmpty(Errors.NameNullOrEmpty);
        var firstNameResult = firstName.EnsureNotNullOrEmpty(Errors.FirstNameNullOrEmpty);
        var emailAddressResult = emailAddress
            .EnsureNotNullOrEmpty(Errors.EmailAddressNullOrEmpty)
            .Ensure(e => EmailValidator.Validate(e), Errors.InvalidEmailAddressFormat);

        return Result.FirstFailureOrSuccess(nameResult, firstNameResult, emailAddressResult)
            .Map(() => new User(name, firstName, emailAddress));
    }

    public string Name { get; private set; }

    public string FirstName { get; private set; }

    public string EmailAddress { get; private set; }

    public bool HasElevatedRights { get;  set; }
}