using System.Text.Json.Serialization;

namespace RobWill.Blog;

public class GitHubItem
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("html_url")]
    public string Url { get; set; }
}