var builder = DistributedApplication.CreateBuilder(args);

//////////// Services ////////////////
var postGres = builder
        .AddPostgres("postgres")
        .WithPgAdmin()
        .WithDataVolume()
        .WithLifetime(ContainerLifetime.Persistent);

var catalogDb = postGres.AddDatabase("catalogdb");

var cache = builder
    .AddRedis("cache")
    .WithRedisInsight()
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent);

//var rabbitMq = builder
//    .AddRabbitMQ("rabbitmq")
//    .WithManagementPlugin()
//    .WithDataVolume()
//    .WithLifetime(ContainerLifetime.Persistent);

var keyCloak = builder
    .AddKeycloak("keycloak", 8080)
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent);

var ollama = builder
    .AddOllama("ollama", 11434)
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent)
    .WithOpenWebUI();

var llama = ollama.AddModel("llama3.2");

//////////// Projects ////////////////
var catalogApi = builder
    .AddProject<Projects.CatalogAPI>("catalogapi")
    .WithReference(catalogDb)
    .WithReference(llama)
    //.WithReference(rabbitMq)
    .WaitFor(catalogDb)
    //.WaitFor(rabbitMq)
    .WaitFor(llama);

var cartApi = builder
    .AddProject<Projects.CartAPI>("cartapi")
    .WithReference(cache)
    .WithReference(catalogApi)
    //.WithReference(rabbitMq)
    .WithReference(keyCloak)
    .WaitFor(cache)
    .WaitFor(catalogApi)
    //.WaitFor(rabbitMq)
    .WaitFor(keyCloak);

var blazorServerApp = builder
    .AddProject<Projects.BlazorServerApp>("blazorserverapp")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WithReference(catalogApi)
    .WithReference(cartApi)
    .WaitFor(cache)
    .WaitFor(catalogApi)
    .WaitFor(cartApi);

builder.Build().Run();
