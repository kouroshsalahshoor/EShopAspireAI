namespace CatalogAPI.Services;

public class ProductService(ApplicationDbContext db) : IProductService
{
    public async Task<IEnumerable<Product>> Get()
    {
        return await db.Products.ToListAsync();
    }

    public async Task<Product?> GetById(int id)
    {
        return await db.Products.FindAsync(id);
    }

    public async Task Create(Product product)
    {
        db.Products.Add(product);
        await db.SaveChangesAsync();
    }

    public async Task Update(Product model, Product dto)
    {
        // if price has changed, raise ProductPriceChanged integration event
        if (model.Price != dto.Price)
        {
            //// Publish product price changed integration event for update basket prices
            //var integrationEvent = new ProductPriceChangedIntegrationEvent
            //{
            //    ProductId = updatedProduct.Id, // Id only comes from db entity
            //    Name = inputProduct.Name,
            //    Description = inputProduct.Description,
            //    Price = inputProduct.Price, //set updated product price
            //    ImageUrl = inputProduct.ImageUrl
            //};
            //await bus.Publish(integrationEvent);
        }

        // update product with new values
        model.Name = dto.Name;
        model.Description = dto.Description;
        model.ImageUrl = dto.ImageUrl;
        model.Price = dto.Price;

        db.Products.Update(model);
        await db.SaveChangesAsync();
    }

    public async Task Delete(Product model)
    {
        db.Products.Remove(model);
        await db.SaveChangesAsync();
    }

    public async Task<IEnumerable<Product>> Search(string query)
    {
        return await db.Products
            .Where(p => p.Name.Contains(query))
            .ToListAsync();
    }
}