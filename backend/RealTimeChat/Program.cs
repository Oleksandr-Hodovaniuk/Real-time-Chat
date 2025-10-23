using DotNetEnv;
using RealTimeChat;
using RealTimeChat.Application;
using RealTimeChat.Extensions;
using RealTimeChat.Infrastructure;
using RealTimeChat.Middlewares;

var builder = WebApplication.CreateBuilder(args);

Env.Load("../../.env");
builder.Configuration.AddEnvironmentVariables();

// Add services to the container.

builder.Services.AddApiServices();

builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddApplicationServices();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseMiddleware<ExceptionHandlingMiddleware>();

await app.InitialiseDatabaseAsync();

app.UseSwaggerDocumentation();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
