var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.CatalogAPI>("catalogapi");

builder.Build().Run();
