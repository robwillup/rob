using System;
using static System.Console;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Rob.Api.Mongo;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();    

app.MapGet("/", (Func<string>)( () => 
{
    WriteLine(app.Configuration["ConnStr"]);
    var dbConn = new DbConnector(app.Configuration["ConnStr"]);
    return dbConn.GetOneDoc("TestX", "ValueX").ToString();
}));

await app.RunAsync();
