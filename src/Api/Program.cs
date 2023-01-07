using Api;

var builder = WebApplication.CreateBuilder(args);

Startup startup = new();

startup.ConfigureServices(builder.Services);

var app = builder.Build();

startup.Configure(app, app.Environment);

app.Run();
