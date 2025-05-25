using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IProductRepository productRepository) : ControllerBase
    {
       

      

        [HttpGet]   
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts()
        {
           return Ok(await productRepository.GetProductsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            return await productRepository.GetProductByIdAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            productRepository.AddProduct(product);
            await productRepository.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateProduct(int id, Product product)
        {
            if (id != product.Id || !ProductExists(id))
            {
                return BadRequest("Could not update product");
            }

            productRepository.UpdateProduct(product);
            await productRepository.SaveChangesAsync();
            return NoContent();
        } 


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
           

            productRepository.DeleteProduct(id);
            await productRepository.SaveChangesAsync();
            return NoContent();
        }



        private bool ProductExists(int id)
        {
            return productRepository.ProductExists(id);
        }

        
    }
}
