using System;
using Core.Entities;

namespace Core.Specifications;

public class ProductSpecification :BaseSpecification<Product>
{
    public ProductSpecification(string? brand ,string? type ,string sort ) :base
    (x =>(string.IsNullOrWhiteSpace(brand) || x.ProductBrand == brand) && 
    (string.IsNullOrWhiteSpace(type) || x.ProductType == type)  )
    {
        switch (sort)
        {
            case "priceAsc":
               AddOrderBy(x=>x.Price);
               break;
             case "priceDesc":
                  AddOrderByDesc(x=>x.Price);
                  break;
            default:
            AddOrderBy(x=>x.Name);
            break;
        }
    }
}
