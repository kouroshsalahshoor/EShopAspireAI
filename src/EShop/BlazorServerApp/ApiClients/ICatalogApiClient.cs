using CatalogAPI.Models;

namespace BlazorServerApp.ApiClients;

public interface ICatalogApiClient
{
    Task<List<Product>> Get();
    Task<Product> GetById(int id);
}