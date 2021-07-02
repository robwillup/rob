using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Rob.Api.Mongo;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();

app.MapGet("/", (Func<string>)( () => 
{
    var dbConn = new DbConnector(app.Configuration["ConnStr"]);
    dbConn.InsertOneDocAsync("TestX", "ValueX");
    return "TestX";
}));

await app.RunAsync();
