using DotNetEnv;
using RealTimeChat;
using RealTimeChat.Extensions;

var builder = WebApplication.CreateBuilder(args);

Env.Load("../../.env");
builder.Configuration.AddEnvironmentVariables();

// Add services to the container.

builder.Services.AddApiServices();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwaggerDocumentation();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
