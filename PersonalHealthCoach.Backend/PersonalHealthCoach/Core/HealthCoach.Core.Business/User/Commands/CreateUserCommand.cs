using CSharpFunctionalExtensions;
using HealthCoach.Core.Domain;
using MediatR;

namespace HealthCoach.Core.Business;

public sealed record CreateUserCommand(string Name, string FirstName, string EmailAddress) : IRequest<Result<User>>;