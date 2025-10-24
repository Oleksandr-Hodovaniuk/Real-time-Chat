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

builder.Services.AddApplicationServices();

builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddApiServices();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.UseSwaggerDocumentation();

await app.InitialiseDatabaseAsync();

app.MapHub<RealTimeChat.Hubs.ChatHub>("/chatHub");

app.MapControllers();

app.Run();
