using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using Rob.Api.Mongo;
using System;
using System.Net;
using Rob.Api.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();    

app.MapGet("/posts/{id}", (async context => 
{
    var dbConn = new DbConnector(app.Configuration["ConnStr"]);
    string id = context.Request.RouteValues["id"].ToString();
    var result = dbConn.GetOneDocAsync(id);
    await context.Response.WriteAsJsonAsync(result);
}));

app.MapPost("/posts", async context => 
{    
    if(!context.Request.HasJsonContentType())
    {
        context.Response.StatusCode = (int) HttpStatusCode.UnsupportedMediaType;
        return;
    }
    var post = await context.Request.ReadFromJsonAsync<Post>();
    var dbConn = new DbConnector(app.Configuration["ConnStr"]);    
    await dbConn.InsertOneDocAsync(post);
    context.Response.StatusCode = (int) HttpStatusCode.Accepted;
});

app.MapGet("/", (Func<string>)( () => 
{
    return "\"For even the very wise cannot see all ends.\" -- Gandalf";
}));

await app.RunAsync();
