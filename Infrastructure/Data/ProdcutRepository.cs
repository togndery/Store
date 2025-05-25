using System;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ProdcutRepository(StoreContext storeContext) : IProductRepository
{
   

    public void AddProduct(Product product)
    {
       storeContext.Products.Add(product);
    }

    public void DeleteProduct(int id)
    { 
        var product = storeContext.Products.Find(id);   
        storeContext.Products.Remove(product);
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
       return await storeContext.Products.FindAsync(id);
    }

    public async Task<IReadOnlyList<Product>> GetProductsAsync()
    {
       return await storeContext.Products.ToListAsync();
    }

    public bool ProductExists(int id)
    {
        return storeContext.Products.Any(p => p.Id == id);
    }

    public async Task<bool> SaveChangesAsync()
    {
       return await storeContext.SaveChangesAsync() > 0;
    }

    public void UpdateProduct(Product product)
    {
       storeContext.Entry(product).State = EntityState.Modified;
    }
}
