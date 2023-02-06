using Weknow.HotChocolatePlayground;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
services
    .AddGraphQLServer()
    .AddQueryType<Query>();

services.RegisterLogic();

var app = builder.Build();

app.MapGraphQL();
app.MapGet("/", () => "Hello World!");

app.Run();
