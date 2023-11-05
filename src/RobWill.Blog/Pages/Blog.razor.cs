using System.Text.Json;
using Microsoft.AspNetCore.Components;

namespace RobWill.Blog.Pages;

public partial class Blog : IComponent
{
    [Inject]
    protected Config Config {get;set;} = default;
    private List<GitHubItem> _items = new();

    protected override async Task OnInitializedAsync()
    {
        _items = await GetArticles();
        System.Console.WriteLine(_items.Where(n => n.Name == "2023-11-05-Streams.md").FirstOrDefault().Name);
    }

    public async Task<List<GitHubItem>> GetArticles()
    {
        HttpClient client = new();
        client.DefaultRequestHeaders.Add("Accept", "application/vnd.github+json");
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Config.mithrandirPat}");
        client.DefaultRequestHeaders.Add("X-GitHub-Api-Version", "2022-11-28");

        var res = await client.GetAsync("https://api.github.com/repos/robwillup/mithrandir/contents/docs/Languages_And_Frameworks/.NET");

        List<GitHubItem> gitHubItems = JsonSerializer.Deserialize<List<GitHubItem>>(await res.Content.ReadAsStringAsync());

        return gitHubItems;
    }
}