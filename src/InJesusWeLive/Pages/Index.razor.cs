using Microsoft.AspNetCore.Components;
using InJesusWeLive.Services;

namespace InJesusWeLive.Pages;

public partial class Index : IComponent
{
    [Inject]
    protected IBlogService? BlogService {get;set;}
}