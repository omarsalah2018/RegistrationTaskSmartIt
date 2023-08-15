using Core.Application.Services.HangfireJobs;
using Core.Application.Services.IServices;
using Hangfire;
using Infrastructure.Data.Contexts;
using Infrastructure.Shared;
using Microsoft.Extensions.DependencyInjection;
using RegistrationTask;
using System.Numerics;

var builder = WebApplication.CreateBuilder(args);
var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{

    var loggerFactory = app.Services.GetService<ILoggerFactory>();
   

    //var _hangFireJobs = app.Services.GetService<IHangFireJobs>();
    loggerFactory.AddFile(builder.Configuration["Logging:LogFilePath"].ToString());

    //var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
    //dbInitializer.Initialize();//to update our data base from pending migrations

    var context = scope.ServiceProvider.GetService<RegistrationDbContext>();
    DataSeeder.SeedRoles(context);


}
// Configure the HTTP request pipeline.
startup.Configure(app, builder.Environment);

