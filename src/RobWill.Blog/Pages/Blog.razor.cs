using Microsoft.AspNetCore.Components;

namespace RobWill.Blog.Pages;

public partial class Blog : IComponent
{
    [Inject]
    protected Config Config {get;set;} = default;

    public async Task GetArticles()
    {
        HttpClient client = new();
        client.DefaultRequestHeaders.Add("Accept", "application/vnd.github+json");
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Config.mithrandirPat}");
        client.DefaultRequestHeaders.Add("X-GitHub-Api-Version", "2022-11-28");

        var res = await client.GetAsync("https://api.github.com/repos/robwillup/mithrandir/contents/docs/Languages And Frameworks/.NET");

        System.Console.WriteLine(await res.Content.ReadAsStringAsync());
    }
}