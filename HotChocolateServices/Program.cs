using Weknow.HotChocolatePlayground;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var requestBuilder = services
    .AddGraphQLServer();

services.RegisterLogic(requestBuilder);

var app = builder.Build();

app.MapGraphQL();
app.MapGet("/", () => "Hello World!");

app.Run();
