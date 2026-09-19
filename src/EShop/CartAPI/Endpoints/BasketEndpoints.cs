namespace CartAPI.Endpoints;

public static class CartEndpoints
{
    public static void MapCartEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("cart");

        // GET by userName
        group.MapGet("/{userName}", async (string userName, ICartService service) =>
        {
            var shoppingCart = await service.Get(userName);

            if (shoppingCart is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(shoppingCart);
        })
        .WithName("Get")
        .Produces<Cart>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        // POST (Upsert)
        group.MapPost("/", async (Cart shoppingCart, ICartService service) =>
        {
            await service.Update(shoppingCart);
            return Results.Created("Get", shoppingCart);
        })
        .WithName("Update")
        .Produces<Cart>(StatusCodes.Status201Created);

        // DELETE
        group.MapDelete("/{userName}", async (string userName, ICartService service) =>
        {
            await service.Delete(userName);
            return Results.NoContent();
        })
        .WithName("Delete")
        .Produces(StatusCodes.Status204NoContent);
    }
}