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

var basketApi = builder.AddProject<Projects.BasketAPI>("basketapi")
    .WithReference(cache)
    .WaitFor(cache);

builder.Build().Run();
