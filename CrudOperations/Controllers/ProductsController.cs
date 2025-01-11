using CrudOperations.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrudOperations.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController(ApplicationDbContext dbContext) : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        [HttpGet]
        [Route("")]
        public ActionResult<IEnumerable<Product>> GetAllProducts()
        {
            var allRecords = _dbContext.Set<Product>().ToList();
            return Ok(allRecords);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<IEnumerable<Product>> GetProductById(int id)
        {
            var record = _dbContext.Set<Product>().Find(id);
            return Ok(record);
        }


        [HttpPost]
        [Route("")]
        public async Task<ActionResult<int>> CreateProduct(Product product)
        {
            product.Id = 0;
            await _dbContext.Set<Product>().AddAsync(product);
            await _dbContext.SaveChangesAsync();
            return Ok(product.Id);
        }

        [HttpPut]
        [Route("")]
        public  ActionResult UpdateProduct(Product product) 
        {
            var existingProduct = _dbContext.Set<Product>().Find(product.Id);
            if (existingProduct == null) return BadRequest("The Request Product not existed");
            existingProduct.Name = product.Name;
            existingProduct.Sku = product.Sku;
            _dbContext.Set<Product>().Update(existingProduct);
            _dbContext.SaveChanges();
            return Ok(product.Id);
        }

        [HttpDelete]
        [Route("")]
        public ActionResult DeleteProduct(Product product)
        {
            var existingProduct = _dbContext.Set<Product>().Find(product.Id);
            if (existingProduct == null) return BadRequest("The Request Product not existed");
            _dbContext.Set<Product>().Remove(existingProduct);
            _dbContext.SaveChanges();
            return Ok(product.Id);
        }

    }
}
