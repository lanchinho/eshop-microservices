var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

string gateway = builder.Configuration["ApiSettings:GatewayAddress"]!;
Uri gatewayUri = new(gateway);

void AddApiClient<T>(IServiceCollection services, Uri baseAddress)
    where T : class
{
    services.AddRefitClient<T>()
            .ConfigureHttpClient(c => c.BaseAddress = baseAddress);
}

AddApiClient<ICatalogService>(builder.Services, gatewayUri);
AddApiClient<IBasketService>(builder.Services, gatewayUri);
AddApiClient<IOrderingService>(builder.Services, gatewayUri);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
