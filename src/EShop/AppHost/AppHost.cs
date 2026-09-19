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

//////////// Projects ////////////////
var catalogApi = builder.AddProject<Projects.CatalogAPI>("catalogapi")
    .WithReference(catalogDb)
    .WaitFor(catalogDb);

var cartApi = builder.AddProject<Projects.CartAPI>("cartapi")
    .WithReference(cache)
    .WithReference(catalogApi)
    .WaitFor(cache)
    .WaitFor(catalogApi);

builder.Build().Run();
