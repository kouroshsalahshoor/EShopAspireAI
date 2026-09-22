namespace CatalogAPI.Endpoints;

public static class ProductEndpoints
{
    public static void MapProductEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/products");

        // GET all
        group.MapGet("/", async (IProductService service) =>
        {
            var products = await service.Get();
            return Results.Ok(products);
        })
        .WithName("GetAll")
        .Produces<List<Product>>(StatusCodes.Status200OK);

        // GET by ID
        group.MapGet("/{id}", async (int id, IProductService service) =>
        {
            var product = await service.GetById(id);
            if (product is null) return Results.NotFound();

            return Results.Ok(product);
        })
        .WithName("GetById")
        .Produces<Product>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        // POST (Create)
        group.MapPost("/", async (Product product, IProductService service) =>
        {
            await service.Create(product);
            return Results.Created($"/products/{product.Id}", product);
        })
        .WithName("Create")
        .Produces<Product>(StatusCodes.Status201Created);

        // PUT (Update)
        group.MapPut("/{id}", async (int id, Product inputProduct, IProductService service) =>
        {
            var updatedProduct = await service.GetById(id);
            if (updatedProduct is null) return Results.NotFound();

            await service.Update(updatedProduct, inputProduct);
            return Results.NoContent();
        })
        .WithName("Update")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        // DELETE
        group.MapDelete("/{id}", async (int id, IProductService service) =>
        {
            var deletedProduct = await service.GetById(id);
            if (deletedProduct is null) return Results.NotFound();

            await service.Delete(deletedProduct);
            return Results.NoContent();
        })
        .WithName("Delete")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        // Traditional Search
        group.MapGet("search/{query}", async (string query, IProductService service) =>
        {
            var products = await service.Search(query);

            return Results.Ok(products);
        })
        .WithName("Search")
        .Produces<List<Product>>(StatusCodes.Status200OK);

        // Support AI
        group.MapGet("/support/{query}", async (string query, IProductAIService service) =>
        {
            var response = await service.SupportAsync(query);

            return Results.Ok(response);
        })
        .WithName("Support")
        .Produces(StatusCodes.Status200OK);

        // AI Search
        //group.MapGet("aisearch/{query}", async (string query, IProductAIService service) =>
        //{
        //    var products = await service.SearchProductsAsync(query);

        //    return Results.Ok(products);
        //})
        //.WithName("AISearchProducts")
        //.Produces<List<Product>>(StatusCodes.Status200OK);
    }
}