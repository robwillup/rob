using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors();
var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();

app.UseCors();

app.MapGet("/articles", (async contex =>
{
    await contex.Response.WriteAsync("Coming soon");
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
    return "\"For even the very wise cannot see all ends.\" -- Gandalf";
});

await app.RunAsync();
