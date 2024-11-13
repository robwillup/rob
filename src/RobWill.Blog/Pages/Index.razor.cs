using Microsoft.AspNetCore.Components;
using RobWill.Blog.Services;

namespace RobWill.Blog.Pages;

public partial class Index : IComponent
{
    [Inject]
    protected IBlogService? BlogService {get;set;}
}