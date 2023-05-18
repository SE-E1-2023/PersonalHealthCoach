using HealthCoach.Shared.Core;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace HealthCoach.Core.Domain;

public sealed class Exercise : AggregateRoot
{
    public Exercise() { }

    private Exercise(string name, string repRange, string restTime, int sets, string type)
    {
        Name = name;
        RepRange = repRange;
        RestTime = restTime;
        Sets = sets;
        Type = type;
    }

    public static Exercise Create(string name, string repRange, string restTime, int sets, string type)
        => new(name, repRange, restTime, sets, type);

    [JsonPropertyName("exercise")]
    public string? Name { get; set; }

    [JsonPropertyName("rep_range")]
    public string? RepRange { get; set; }

    [JsonPropertyName("rest_time")]
    public string? RestTime { get; set; }

    [JsonPropertyName("sets")]
    public int? Sets { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }
}