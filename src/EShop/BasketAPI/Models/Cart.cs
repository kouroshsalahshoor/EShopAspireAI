namespace BasketAPI.Models;

public class Cart
{
    public string UserName { get; set; } = default!;
    public List<CartItem> Items { get; set; } = new();
    public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);
}
