using CatalogAPI.Models;

namespace CartAPI.ApiClients;

public class CatalogApiClient(HttpClient httpClient) : ICatalogApiClient
{
    public async Task<Product> GetProductById(int id)
    {
        var response = await httpClient.GetFromJsonAsync<Product>($"/products/{id}");
        return response!;
    }
}