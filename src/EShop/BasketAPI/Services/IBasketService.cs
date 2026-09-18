namespace BasketAPI.Services;

public interface IBasketService
{
    Task<Cart?> Get(string userName);
    Task Update(Cart basket);
    Task Delete(string userName);
}