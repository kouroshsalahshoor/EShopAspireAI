namespace CatalogAPI.Services;

public interface IProductService
{
    Task<IEnumerable<Product>> Get();
    Task<Product?> GetById(int id);
    Task<IEnumerable<Product>> Search(string query);
    Task Create(Product product);
    Task Update(Product model, Product dto);
    Task Delete(Product model);
}