using System;
using Core.Entities;

namespace Core.Interfaces;

public interface IProductRepository
{
    Task<Product> GetProductByIdAsync(int id);
    Task<IReadOnlyList<Product>> GetProductsAsync(string? brand , string? type , string? sort);

    Task <IReadOnlyList<string>> GetProdcutBrandsAsync();

    Task <IReadOnlyList<string>> GetProdcutTypeAsync();

    void AddProduct(Product product);
    void UpdateProduct(Product product);
    void DeleteProduct(int id);

    bool ProductExists(int id);

    Task<bool> SaveChangesAsync();
    

}
