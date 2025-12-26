var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly;

builder.Services
    .AddScoped<IBasketRepository, BasketRepository>()
    .Decorate<IBasketRepository, CachedBasketRepository>()
    .AddStackExchangeRedisCache(options =>
    {
        options.Configuration = builder.Configuration.GetConnectionString("Redis");
    })
    .AddMarten(opts =>
     {
         opts.Connection(builder.Configuration.GetConnectionString("Database")!);
         opts.Schema.For<ShoppingCart>().Identity(x => x.UserName);
     }).UseLightweightSessions();

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

app.UseExceptionHandler(options => { });

app.Run();
