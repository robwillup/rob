using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Rob.Api;
using System;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
using System.Net.Http;
using Azure.Security.KeyVault.Keys;
using Azure.Identity;
using Azure.Security.KeyVault.Keys.Cryptography;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddUserSecrets(Assembly.GetExecutingAssembly());
builder.Services.AddCors();
builder.Services.AddScoped<IJwtGenerator, JwtGenerator>();
builder.Services.AddScoped<IInstallationToken, InstallationTokenGetter>();
builder.Services.AddScoped<IHttpClientFactory, HttpClientFactory>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();

app.UseCors();

var scope = app.Services.CreateScope();

var jwtGenerator = scope.ServiceProvider.GetService<IJwtGenerator>();
var installationTokenGetter = scope.ServiceProvider.GetService<IInstallationToken>();
var httpClientFactory = scope.ServiceProvider.GetService<IHttpClientFactory>();

var keyVaultclient = new KeyClient(new Uri("https://rob.vault.azure.net/"), new DefaultAzureCredential());

KeyVaultKey key = keyVaultclient.GetKey("github-rob-api-private-key");

CryptographyClient cryptoClient = keyVaultclient.GetCryptographyClient(
    key.Name, key.Properties.Version);

JwtSecurityToken jwt = jwtGenerator.GenerateToken(cryptoClient, key);
InstallationToken token = await installationTokenGetter.GetInstallationTokenAsync(jwt);
HttpClient client = httpClientFactory.CreateClient();

app.MapGet("/articles", (async contex =>
{
    if (jwt.IssuedAt < DateTime.UtcNow.AddMinutes(-6))
    {
        jwt = new JwtSecurityToken();
        token = await installationTokenGetter.GetInstallationTokenAsync(jwt);
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
    return $"\"For even the very wise cannot see all ends.\" -- Gandalf\nThe JWT: {jwt}";
});

await app.RunAsync();
