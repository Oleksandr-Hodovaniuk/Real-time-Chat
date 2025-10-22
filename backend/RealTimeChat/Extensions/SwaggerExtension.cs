namespace RealTimeChat.Extensions;

public static class SwaggerExtension
{
    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddSwaggerGen();

        return services;
    }

    public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
    {
        if (Environment.GetEnvironmentVariable("SWAGGER_ENABLED") == "true")
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        return app;
    }
}
