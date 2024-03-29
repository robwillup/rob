using Microsoft.AspNetCore.Components;
using RobWill.Blog.Services;

namespace RobWill.Blog.Pages;

public partial class Index : IComponent
{
    [Inject]
    protected IBlogService? BlogService {get;set;}
    private List<GitHubItem>? _items;

    protected override async Task OnInitializedAsync()
    {
        _items = await BlogService.GetBlogPostsAsync();
    }
}