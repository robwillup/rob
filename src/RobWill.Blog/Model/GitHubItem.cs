using System.Text.Json.Serialization;

namespace RobWill.Blog;

public class GitHubItem
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; }

    [JsonPropertyName("html_url")]
    public string HtmlUrl { get; set; }

    [JsonPropertyName("path")]
    public string Path { get; set; }
}