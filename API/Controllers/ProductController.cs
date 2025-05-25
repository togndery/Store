using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly StoreContext storeContext;

        public ProductsController(StoreContext storeContext)
        {
            this.storeContext = storeContext;
        }

        [HttpGet]   
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await storeContext.Products.ToListAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            return await storeContext.Products.FindAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            storeContext.Products.Add(product);
            await storeContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateProduct(int id, Product product)
        {
            if (id != product.Id || !ProductExists(id))
            {
                return BadRequest("Could not update product");
            }

            storeContext.Entry(product).State = EntityState.Modified;
            await storeContext.SaveChangesAsync();
            return NoContent();
        } 


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await storeContext.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound("Could not find product");
            }

            storeContext.Products.Remove(product);
            await storeContext.SaveChangesAsync();
            return NoContent();
        }



        private bool ProductExists(int id)
        {
            return storeContext.Products.Any(e => e.Id == id);
        }

        
    }
}
