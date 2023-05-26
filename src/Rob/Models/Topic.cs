using System.Text.Json.Serialization;

namespace Rob.Models;

public class Topic
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("path")]
    public string Path { get; set; }

    [JsonPropertyName("sha")]
    public string Sha { get; }

    [JsonPropertyName("size")]
    public int Size { get; }

    [JsonPropertyName("url")]
    public Uri Url { get; }

    [JsonPropertyName("html_url")]
    public Uri HtmlUrl { get; }

    [JsonPropertyName("git_url")]
    public Uri GitUrl { get; }

    [JsonPropertyName("download_url")]
    public Uri DownloadUrl { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }
}
