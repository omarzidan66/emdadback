using Emdad.Models;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<EmdadContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlCon").ToString());
});
var app = builder.Build();
app.UseRouting();
app.UseStaticFiles();

app.MapGet("/", () => "Hello World!");

app.Run();
