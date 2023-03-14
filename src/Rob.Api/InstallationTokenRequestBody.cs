using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Rob.Api;

public class InstallationTokenRequestBody
{
    [JsonPropertyName("repository")]
    public string Repository { get; set; }

    [JsonPropertyName("permissions")]
    public Dictionary<string, string> Permissions { get; set; }
}
