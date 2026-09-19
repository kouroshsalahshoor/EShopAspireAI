using CatalogAPI.Models;

namespace CartAPI.ApiClients
{
    public interface ICatalogApiClient
    {
        Task<Product> GetProductById(int id);
    }
}