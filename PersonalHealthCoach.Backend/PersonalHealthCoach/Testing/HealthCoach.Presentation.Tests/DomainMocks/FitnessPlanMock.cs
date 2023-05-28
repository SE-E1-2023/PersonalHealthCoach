using HealthCoach.Core.Domain;

namespace HealthCoach.Presentation.Tests;

public sealed record FitnessPlanMock(Guid userId, IReadOnlyCollection<Workout> workout);
