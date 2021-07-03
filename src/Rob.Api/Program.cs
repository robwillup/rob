using System;
using static System.Console;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Rob.Api.Mongo;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

string connStr = app.Configuration["ConnStr"];;

if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();    

app.MapGet("/", (Func<string>)( () => 
{
    var dbConn = new DbConnector(connStr);    
    return dbConn.GetOneDoc("TestX", "ValueX").ToString();
}));

await app.RunAsync();
