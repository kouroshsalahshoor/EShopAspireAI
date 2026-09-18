namespace BasketAPI.Endpoints;

public static class BasketEndpoints
{
    public static void MapBasketEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("basket");

        // GET by userName
        group.MapGet("/{userName}", async (string userName, IBasketService service) =>
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
        group.MapPost("/", async (Cart shoppingCart, IBasketService service) =>
        {
            await service.Update(shoppingCart);
            return Results.Created("Get", shoppingCart);
        })
        .WithName("Update")
        .Produces<Cart>(StatusCodes.Status201Created);

        // DELETE
        group.MapDelete("/{userName}", async (string userName, IBasketService service) =>
        {
            await service.Delete(userName);
            return Results.NoContent();
        })
        .WithName("Delete")
        .Produces(StatusCodes.Status204NoContent);
    }
}