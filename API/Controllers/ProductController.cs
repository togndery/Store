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
    public class ProductsController(IGenericRepository<Product> repo) : ControllerBase
    {
       

      

        [HttpGet]   
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts(string ? brand , string? type ,string? sort)
        {
           
           return Ok(await repo.ListAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            return await repo.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            repo.Add(product);
            await repo.SaveAllAsync();
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateProduct(int id, Product product)
        {
            if (id != product.Id || !ProductExists(id))
            {
                return BadRequest("Could not update product");
            }

            repo.Update(product);
            await repo.SaveAllAsync();
            return NoContent();
        } 


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            
            var prodcut = await repo.GetByIdAsync(id);

            if(prodcut == null)
                return NotFound();
            
             repo.Remove(prodcut);
             if(await repo.SaveAllAsync())
                return NoContent();

            
            return BadRequest("Problem to deleting the prodcut");
            
 
           
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetProdcutBrands()
        {
          //TODO : Implment 

           return Ok();
        }

         [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetProdctTypes()
        {
              //TODO : Implment 

           return Ok();
        }




        private bool ProductExists(int id)
        {
            return repo.Exists(id);
        }

        
    }
}
