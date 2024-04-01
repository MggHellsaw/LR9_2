var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapGet("/weather", async context =>
{
    var city = "Odesa";

    var service = new WeatherService();
    double lat = 46.4825; 
    double lon = 30.7233; 
    WeatherResponse weather = await service.GetWeatherAsync(lat, lon);

    var weatherForecast = $@"
        <h1>Weather Forecast</h1>
        <h3>Current Weather in {city}</h3>
        <p>Temperature: {weather.Main.Temperature} Celsius</p>
    ";

    await context.Response.WriteAsync(weatherForecast);
});

app.MapRazorPages();

app.Run();