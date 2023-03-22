using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Rob.Api;
using System;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddUserSecrets(Assembly.GetExecutingAssembly());
builder.Configuration.AddEnvironmentVariables();
builder.Services.AddCors();
builder.Services.AddScoped<IJwtGenerator, JwtGenerator>();
builder.Services.AddScoped<IInstallationToken, InstallationTokenGetter>();
builder.Services.AddScoped<IHttpClientFactory, HttpClientFactory>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();

app.UseCors();

Console.WriteLine(Environment.GetEnvironmentVariable("appid", EnvironmentVariableTarget.Process));
Console.WriteLine(app.Configuration["appid"]);

var scope = app.Services.CreateScope();

app.MapGet("/articles", (async contex =>
{
    var jwtGenerator = scope.ServiceProvider.GetService<IJwtGenerator>();
    var installationTokenGetter = scope.ServiceProvider.GetService<IInstallationToken>();
    var httpClientFactory = scope.ServiceProvider.GetService<IHttpClientFactory>();

    JwtSecurityToken jwt = jwtGenerator.GenerateToken();
    InstallationToken token = await installationTokenGetter.GetInstallationTokenAsync(jwt.RawData);
    HttpClient client = httpClientFactory.CreateClient();

    JwtSecurityTokenHandler tokenHandler = new();

    if (jwt.IssuedAt < DateTime.UtcNow.AddMinutes(-6))
    {
        jwt = new JwtSecurityToken();
        token = await installationTokenGetter.GetInstallationTokenAsync(jwt.RawData);
    }
    client.DefaultRequestHeaders.Clear();
    client.DefaultRequestHeaders.Add("User-Agent", "Rob.Api");
    client.DefaultRequestHeaders.Add("Accept", "application/vnd.github.raw");
    client.DefaultRequestHeaders.Add("X-GitHub-Api-Version", "2022-11-28");
    client.DefaultRequestHeaders.Add("Authorization", token.Token);

    var res = await client.GetAsync("https://api.github.com/repos/robwillup/mithrandir/contents/README.md");

    await contex.Response.WriteAsync(await res.Content.ReadAsStringAsync());
}));

app.MapGet("/articles/{id}", (async context =>
{
    await context.Response.WriteAsync("coming soon");
})).RequireCors(op => 
{
    op.AllowAnyOrigin();
});

app.MapGet("/", () =>
{
    return $"\"For even the very wise cannot see all ends.\" -- Gandalf";
});

await app.RunAsync();
