using System.Text.Json;

namespace RobWill.Blog.Services;

public class BlogService : IBlogService
{
    private readonly IConfiguration _config;
    private readonly HttpClient _httpClient;

    public BlogService(HttpClient httpClient, IConfiguration config)
    {
        _config = config;
        _httpClient = httpClient;
    }

    public async Task<List<GitHubItem>> GetBlogPostsAsync()
    {
        try
        {
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/vnd.github+json");
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_config["mithrandirPat"]}");
            _httpClient.DefaultRequestHeaders.Add("X-GitHub-Api-Version", "2022-11-28");

            HttpResponseMessage res = await _httpClient.GetAsync("docs/Languages_And_Frameworks/Go/Functions");

            res.EnsureSuccessStatusCode();

            string content = await res.Content.ReadAsStringAsync();

            List<GitHubItem> gitHubItems = JsonSerializer.Deserialize<List<GitHubItem>>(content) ?? new();

            return gitHubItems;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<string> GetBlogPostAsync(string id)
    {
        try
        {
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/vnd.github.raw");
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_config["mithrandirPat"]}");
            _httpClient.DefaultRequestHeaders.Add("X-GitHub-Api-Version", "2022-11-28");

            HttpResponseMessage res = await _httpClient.GetAsync($"{id}");

            res.EnsureSuccessStatusCode();

            return await res.Content.ReadAsStringAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }
}