using ApiProduct.Data;
using ApiProduct.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace clinical_inventory.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly DataContext _context;
        public ProductsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product is null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            var newProduct = new Product
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                HighDate = product.HighDate,
                Active = true,
            };

            _context.Products.Add(newProduct);
            await _context.SaveChangesAsync();

            return Ok(newProduct);
        }

        [HttpPut]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if(id != product.Id)
            {
                return BadRequest();
            }

            var productEdit = await _context.Products.FindAsync(id);
            if(productEdit is null)
            {
                return NotFound();
            }

            productEdit.Name = product.Name;
            productEdit.Description = product.Description;
            productEdit.Price = product.Price;
            productEdit.HighDate = product.HighDate;
            productEdit.Active = product.Active;

            await _context.SaveChangesAsync();

            return Ok(product);
        }
        
        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var productDelete = await _context.Products.FindAsync(id);
            if (productDelete is null)
            {
                return NotFound();
            }

            _context.Products.Remove(productDelete);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}