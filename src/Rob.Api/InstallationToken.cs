using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Rob.Api;

public class InstallationToken
{
    [JsonPropertyName("token")]
    public string Token { get; set; }

    [JsonPropertyName("expires_at")]
    public DateTime ExpiresAt { get; set; }

    [JsonPropertyName("permissions")]
    public Dictionary<string, string> Permissions { get; set; }

    [JsonPropertyName("repository_selection")]
    public string RepositorySelection { get; set; }
}
