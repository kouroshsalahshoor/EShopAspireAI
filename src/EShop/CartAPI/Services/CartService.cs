using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace CartAPI.Services;

public class CartService(IDistributedCache cache, ICatalogApiClient catalogApiClient) : ICartService
{
    public async Task<Cart?> Get(string userName)
    {
        var cart = await cache.GetStringAsync(userName);
        return string.IsNullOrEmpty(cart)
            ? null :
            JsonSerializer.Deserialize<Cart>(cart);
    }
    public async Task Update(Cart cart)
    {
        foreach (var item in cart.Items)
        {
            var product = await catalogApiClient.GetProductById(item.ProductId);
            item.Price = product.Price;
            item.ProductName = product.Name;
        }

        await cache.SetStringAsync(cart.UserName, JsonSerializer.Serialize(cart));
    }
    public async Task Delete(string userName)
    {
        await cache.RemoveAsync(userName);
    }
    //public async Task UpdateCartItemProductPrices(int productId, decimal price)
    //{
    //    // IDistributedCache not supported list of keys function
    //    // https://github.com/dotnet/runtime/issues/36402

    //    // should be done for all users!!!
    //    var cart = await Get("xxx");

    //    var item = cart!.Items.FirstOrDefault(x => x.ProductId == productId);
    //    if (item != null)
    //    {
    //        item.Price = price;
    //        await cache.SetStringAsync(cart.UserName, JsonSerializer.Serialize(cart));
    //    }
    //}
}
