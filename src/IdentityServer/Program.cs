using IdentityServer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddIdentityServer()
    .AddInMemoryClients(SeedData.Clients)
    .AddInMemoryApiScopes(SeedData.ApiScopes)
    .AddInMemoryApiResources(SeedData.ApiResources)
    .AddDeveloperSigningCredential();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseIdentityServer();

app.UseAuthorization();

app.MapControllers();

app.Run();
