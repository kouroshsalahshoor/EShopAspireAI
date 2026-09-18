namespace BasketAPI.Models;

public class CartItem
{
    public int Quantity { get; set; } = default!;
    public string Color { get; set; } = default!;
    public int ProductId { get; set; } = default!;

    // comes from Catalog module
    public decimal Price { get; set; } = default!;
    public string ProductName { get; set; } = default!;
}
