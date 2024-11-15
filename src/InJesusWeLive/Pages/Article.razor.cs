using System.Net;
using Microsoft.AspNetCore.Components;
using InJesusWeLive.Services;

namespace InJesusWeLive.Pages;

public partial class Article : IComponent
{
    [Inject]
    protected IBlogService? blogService {get;set;}

    public string Id { get; set; } = "";

    private string? _content;

    protected override async Task OnInitializedAsync()
    {
        Id = NavigationManager.ToAbsoluteUri(NavigationManager.Uri).Query.TrimStart('?');
        string id = WebUtility.UrlDecode(Id).Replace("Id=", "");
        _content = await blogService?.GetBlogPostAsync(id) ?? string.Empty;
    }
}