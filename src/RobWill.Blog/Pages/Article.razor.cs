using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Components;

namespace RobWill.Blog.Pages;

public partial class Article : IComponent
{
    [Inject]
    protected Config Config {get;set;} = default;

    public string Id { get; set; }

    private string _content;

    public Article()
    {
    }

    protected override async Task OnInitializedAsync()
    {
        Id = NavigationManager.ToAbsoluteUri(NavigationManager.Uri).Query.TrimStart('?');
        _content = await GetArticle();
    }

    public async Task<string> GetArticle()
    {
        HttpClient client = new();
        client.DefaultRequestHeaders.Add("Accept", "application/vnd.github.raw");
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Config.mithrandirPat}");
        client.DefaultRequestHeaders.Add("X-GitHub-Api-Version", "2022-11-28");

        string decoded = WebUtility.UrlDecode(Id).Replace("Id=", "");

        System.Console.WriteLine($"https://api.github.com/repos/robwillup/mithrandir/contents/{decoded}");

        var res = await client.GetAsync($"https://api.github.com/repos/robwillup/mithrandir/contents/{decoded}");

        return await res.Content.ReadAsStringAsync();
    }
}