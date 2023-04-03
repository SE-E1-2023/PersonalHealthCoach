using MediatR;
using HealthCoach.Core.Domain;
using CSharpFunctionalExtensions;

namespace HealthCoach.Core.Business;

public sealed record CreateUserCommand(string Name, string FirstName, string EmailAddress) : IRequest<Result<User>>;