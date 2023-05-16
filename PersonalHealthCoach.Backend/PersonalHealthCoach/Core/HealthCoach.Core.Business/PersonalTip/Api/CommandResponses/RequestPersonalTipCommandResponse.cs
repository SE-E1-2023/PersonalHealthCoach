using Newtonsoft.Json;

namespace HealthCoach.Core.Business;

internal class RequestPersonalTipCommandResponse
{
    public string Type { get; set; }

    [JsonProperty("Importance Level")]
    public string ImportanceLevel { get; set; }

    public string Tip { get; set; } 
}
