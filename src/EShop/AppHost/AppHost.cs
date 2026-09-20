var builder = DistributedApplication.CreateBuilder(args);

//////////// Services ////////////////
var postgres = builder
        .AddPostgres("postgres")
        .WithPgAdmin()
        .WithDataVolume()
        .WithLifetime(ContainerLifetime.Persistent);

var catalogDb = postgres.AddDatabase("catalogdb");

var cache = builder
    .AddRedis("cache")
    .WithRedisInsight()
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent);

var rabbitmq = builder
    .AddRabbitMQ("rabbitmq")
    .WithManagementPlugin()
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent);

var keycloak = builder
    .AddKeycloak("keycloak", 8080)
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent);


//////////// Projects ////////////////
var catalogApi = builder
    .AddProject<Projects.CatalogAPI>("catalogapi")
    .WithReference(catalogDb)
    .WithReference(rabbitmq)
    .WaitFor(catalogDb)
    .WaitFor(rabbitmq);

var cartApi = builder
    .AddProject<Projects.CartAPI>("cartapi")
    .WithReference(cache)
    .WithReference(catalogApi)
    .WithReference(rabbitmq)
    .WithReference(keycloak)
    .WaitFor(cache)
    .WaitFor(catalogApi)
    .WaitFor(rabbitmq)
    .WaitFor(keycloak);

builder.Build().Run();
