using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Ordering.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddCarter()
            .AddEndpointsApiExplorer()
            .AddSwaggerGen()
            .AddExceptionHandler<GlobalExceptionHandler>()
                .AddProblemDetails()
            .AddHealthChecks()
                .AddSqlServer(configuration.GetConnectionString("Database")!);

        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {

        app.MapCarter();

        app.UseSwagger()
           .UseSwaggerUI(options =>
           {
               options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
           })
           .UseExceptionHandler(options => { })
           .UseHealthChecks("/health", new HealthCheckOptions
           {
               ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
           });

        return app;
    }
}
