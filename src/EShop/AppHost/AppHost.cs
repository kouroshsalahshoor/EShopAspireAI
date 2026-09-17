var builder = DistributedApplication.CreateBuilder(args);

//////////// Services ////////////////
var postgres = builder
        .AddPostgres("postgres")
        .WithPgAdmin()
        .WithDataVolume()
        .WithLifetime(ContainerLifetime.Persistent);

var catalogDb = postgres.AddDatabase("catalogdb");

//////////// Projects ////////////////
builder.AddProject<Projects.CatalogAPI>("catalogapi")
    .WithReference(catalogDb)
    .WaitFor(catalogDb);

builder.Build().Run();
