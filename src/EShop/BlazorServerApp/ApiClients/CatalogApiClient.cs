using CatalogAPI.Models;

namespace BlazorServerApp.ApiClients;

public class CatalogApiClient(HttpClient httpClient) : ICatalogApiClient
{
    public async Task<List<Product>> Get()
    {
        var response = await httpClient.GetFromJsonAsync<List<Product>>($"/products");
        return response!;
    }

    public async Task<Product> GetById(int id)
    {
        var response = await httpClient.GetFromJsonAsync<Product>($"/products/{id}");
        return response!;
    }

    //public async Task<string> Support(string query)
    //{
    //    var response = await httpClient.GetFromJsonAsync<string>($"/products/support/{query}");
    //    return response!;
    //}

    //public async Task<List<Product>?> Search(string query, bool aiSearch)
    //{
    //    if (aiSearch)
    //    {
    //        return await httpClient.GetFromJsonAsync<List<Product>>($"/products/aisearch/{query}");
    //    }
    //    else
    //    {
    //        return await httpClient.GetFromJsonAsync<List<Product>>($"/products/search/{query}");
    //    }
    //}
}