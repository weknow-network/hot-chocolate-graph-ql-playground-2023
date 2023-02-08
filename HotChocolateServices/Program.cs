using Weknow.HotChocolatePlayground;
using HotChocolate.Execution.Configuration;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
IRequestExecutorBuilder requestBuilder = services
    .AddGraphQLServer();

services.RegisterLogic(requestBuilder);

var app = builder.Build();

app.MapGraphQL();
app.MapGet("/", () => "Hello World!");

app.Run();

// Why you should consider using persisted queries with GraphQL: https://www.youtube.com/watch?v=ZZ5PF3_P_r4
