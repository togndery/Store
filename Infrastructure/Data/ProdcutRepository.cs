using System;
using System.Security.Cryptography.X509Certificates;
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

    public async Task<IReadOnlyList<string>> GetProdcutBrandsAsync()
    {
       var barnds = await storeContext.Products.Select(x=>x.ProductBrand).Distinct().ToListAsync();
       return barnds;
    }

    public async Task<IReadOnlyList<string>> GetProdcutTypeAsync()
    {
       var types = await storeContext.Products.Select(x=>x.ProductType).Distinct().ToListAsync();
       return types;
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
       return await storeContext.Products.FindAsync(id);
    }

    public async Task<IReadOnlyList<Product>> GetProductsAsync(string? brand , string? types , string? sort)
    {

       var query  = storeContext.Products.AsQueryable();

       if(!string.IsNullOrWhiteSpace(brand))
          query = query.Where(x =>x.ProductBrand ==brand);


         if(!string.IsNullOrWhiteSpace(types))
          query = query.Where(x =>x.ProductType ==types);  

             query = sort switch
             {
               "priceAsc" => query.OrderBy(x=>x.Price),
               "priceDesc" => query.OrderByDescending(x=>x.Price),
               _ => query.OrderBy(x=>x.Name)
             };
         

       return await query.ToListAsync();
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
