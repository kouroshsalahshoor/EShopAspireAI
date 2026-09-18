using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace BasketAPI.Services;

public class BasketService(IDistributedCache cache) : IBasketService
{
    public async Task<Cart?> Get(string userName)
    {
        var basket = await cache.GetStringAsync(userName);
        return string.IsNullOrEmpty(basket)
            ? null :
            JsonSerializer.Deserialize<Cart>(basket);
    }
    public async Task Update(Cart basket)
    {
        await cache.SetStringAsync(basket.UserName, JsonSerializer.Serialize(basket));
    }
    public async Task Delete(string userName)
    {
        await cache.RemoveAsync(userName);
    }
}
