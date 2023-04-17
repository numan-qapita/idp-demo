using Duende.IdentityServer.Models;
using IdentityModel;

namespace IdentityServer;

public static class SeedData
{
    private const string WeatherApiResource = "weather.api.resource";
    private const string WeatherApiScope = "weather.api.scope";

    public static IEnumerable<Client> Clients => new[]
    {
        new Client
        {
            ClientId = "weather-api-client",
            ClientSecrets = new[] { new Secret("super-secret-value".Sha256()) },
            AllowedGrantTypes = new[]
                { OidcConstants.GrantTypes.ClientCredentials },
            AllowedScopes = new[] { WeatherApiScope },
            //AccessTokenLifetime = 30
        }
    };

    public static IEnumerable<ApiScope> ApiScopes => new[]
    {
        new ApiScope(WeatherApiScope)
    };

    public static IEnumerable<ApiResource> ApiResources => new[]
    {
        new ApiResource
        {
            Name = WeatherApiResource,
            Scopes = new[] { WeatherApiScope }
        }
    };
}