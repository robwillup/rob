using System;
using static System.Console;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Rob.Api.Mongo;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();


var test = new GcpSecretManager();
WriteLine(test.AccessSecretVersion());

app.MapGet("/", (Func<string>)( () => 
{
    var dbConn = new DbConnector(app.Configuration["ConnStr"]);
    if(0 == 1)
        dbConn.InsertOneDocAsync("TestX", "ValueX");
    return "TestX";
}));

await app.RunAsync();
