using BuildingBlocks.Exceptions.Handler;

namespace Ordering.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services
            .AddCarter()
            .AddEndpointsApiExplorer()
            .AddSwaggerGen()
            .AddExceptionHandler<GlobalExceptionHandler>()
            .AddProblemDetails();

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
           .UseExceptionHandler(options => { });

        return app;
    }
}
