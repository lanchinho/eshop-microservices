using JasperFx;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container.
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
	config.RegisterServicesFromAssembly(typeof(Program).Assembly);
	config.LicenseKey = builder.Configuration.GetValue<string>("MediatR:LicenseKey");
});

builder.Services.AddMarten(opts =>
{
	opts.Connection(builder.Configuration.GetConnectionString("Database")!);	
}).UseLightweightSessions();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapCarter();

app.UseSwagger();
app.UseSwaggerUI(options =>
{	
	options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
});


app.Run();
