using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddInfraestructureServices(builder.Configuration)
    .AddApplicationServices()
    .AddApiServices();

var app = builder.Build();

app.UseApiServices();

app.Run();
