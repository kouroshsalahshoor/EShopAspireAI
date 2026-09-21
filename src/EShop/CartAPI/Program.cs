var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
//swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.AddRedisDistributedCache(connectionName: "cache");
builder.Services.AddScoped<ICartService, CartService>();
//builder.Services.AddScoped<ICatalogApiClient, CatalogApiClient>();

builder.Services.AddHttpClient<ICatalogApiClient, CatalogApiClient>(client =>
{
    client.BaseAddress = new Uri("http+https://catalogapi");
});
//builder.Services.AddMassTransitWithAssemblies(Assembly.GetExecutingAssembly());

builder.Services.AddAuthentication()
                .AddKeycloakJwtBearer(
                    serviceName: "keycloak",
                    realm: "eshop",
                    configureOptions: options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.Audience = "account";
                    });
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.MapOpenApi();
    //swagger
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.MapDefaultEndpoints();
app.MapCartEndpoints();

app.Run();
