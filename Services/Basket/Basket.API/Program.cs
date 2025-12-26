var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly;
builder.Services
    .AddMediatR(config =>
    {
        config.RegisterServicesFromAssembly(assembly);
        config.LicenseKey = builder.Configuration.GetValue<string>("MediatR:LicenseKey");
        config.AddOpenBehavior(typeof(ValidationBehavior<,>));
        config.AddOpenBehavior(typeof(LoggingBehavior<,>));
    })
    .AddCarter()
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddValidatorsFromAssembly(assembly)
    .AddExceptionHandler<GlobalExceptionHandler>()
    .AddProblemDetails();

var app = builder.Build();

app.MapCarter();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
});

app.Run();
