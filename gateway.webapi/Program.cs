
using gateway.webapi;
using Microsoft.AspNetCore.Authentication;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

IConfiguration configuration = new ConfigurationBuilder()
                            .AddJsonFile("ocelot.json")
                            .Build();
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOcelot(configuration);
builder.Services.AddAuthentication("TestKey")
        .AddScheme<AuthenticationSchemeOptions, CustomAuthenticationHandler>("TestKey", options => { });

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.UseOcelot();
app.UseAuthentication();
app.Run();
