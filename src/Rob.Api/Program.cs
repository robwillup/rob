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

JwtSecurityToken jwt = jwtGenerator.GenerateToken();
InstallationToken token = await installationTokenGetter.GetInstallationTokenAsync(jwt);

app.MapGet("/articles", (async contex =>
{
    if (jwt.IssuedAt < DateTime.UtcNow.AddMinutes(-6))
    {
        jwt = new JwtSecurityToken();
        token = await installationTokenGetter.GetInstallationTokenAsync(jwt);
    }



    await contex.Response.WriteAsync($"Coming soon {token.Token}");
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
