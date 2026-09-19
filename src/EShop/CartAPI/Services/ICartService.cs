namespace CartAPI.Services;

public interface ICartService
{
    Task<Cart?> Get(string userName);
    Task Update(Cart cart);
    Task Delete(string userName);
}