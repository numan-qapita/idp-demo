using Bogus;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.Authority = configuration["IdP:Authority"];
        options.Audience = configuration["IdP:Audience"];

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            // ValidateLifetime = true,
            // ClockSkew = TimeSpan.Zero
        };
    });

services.AddAuthorization();

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", (Func<string>)(() => "Welcome to the Bogus Weather API Sample!"));

app.MapGet("/weather/{city}", (string city) =>
{
    var weatherFaker = new Faker<WeatherData>()
        .RuleFor(w => w.City, city)
        .RuleFor(w => w.Date, DateTime.Today)
        .RuleFor(w => w.Temperature, f => Math.Round(f.Random.Double(10, 50), 1))
        .RuleFor(w => w.Humidity, f => Math.Round(f.Random.Double(0, 100), 1))
        .RuleFor(w => w.Condition, f => f.PickRandom("Sunny", "Rainy", "Cloudy", "Windy", "Snowy", "Stormy"));

    return weatherFaker.Generate(1);
}).RequireAuthorization();

app.Run();

public record WeatherData
{
    public string City { get; init; }
    public DateTime Date { get; init; }
    public double Temperature { get; init; }
    public double Humidity { get; init; }
    public string Condition { get; init; }
}