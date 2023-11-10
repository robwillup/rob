using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RobWill.Blog;
using MudBlazor.Services;
using RobWill.Blog.Services;
using MudBlazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var config = new Config();

builder.Configuration.Bind(config);
builder.Services.AddSingleton(config);
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();
builder.Services.AddMudMarkdownServices();
builder.Services.AddHttpClient<IBlogService, BlogService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["BaseUrl"]);
});

await builder.Build().RunAsync();
