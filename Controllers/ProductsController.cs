using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using مشروع_قبل_الشغل.Authorizetion;
using مشروع_قبل_الشغل.Data;

namespace مشروع_قبل_الشغل.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext dbContext;

        public ProductsController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpPost]
        public async Task<ActionResult<int>> CreateProducts(Product product)
        {
            product.Id = 0;
            dbContext.Set<Product>().Add(product);
            await dbContext.SaveChangesAsync();
            return Ok(product.Id);
        }
        [HttpGet]
        [Authorize(Roles ="admin")]
        [Authorize(Policy = "adminsOnly")]
        public async Task<IActionResult> GetallProducts()
        {
            var GetAll = dbContext.Set<Product>().ToList();
            await dbContext.SaveChangesAsync();
            return Ok(GetAll);
        }
        [HttpGet]
        [Route("id")]
        [CheckPermissionAtrribute(Permission.ReadProducts)]
        public async Task<IActionResult> GetByid(int id)
        {
            var getproduct = dbContext.Set<Product>().FindAsync(id);
            await dbContext.SaveChangesAsync();
            return Ok(getproduct);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProducts(Product product)
        {
            if (product.Id == 0)
                return NotFound();
                    var getproduct = dbContext.Set<Product>().Find(product.Id);
            getproduct.Id = product.Id;
                    getproduct.Name = product.Name;
                    getproduct.Sku = product.Sku;
                    dbContext.Set<Product>().Update(getproduct);
                    await dbContext.SaveChangesAsync();
                    return Ok(getproduct);
        }
        [HttpDelete]
        [Route("id")]
        public async Task<IActionResult> RemoveProducts(int Id)
        {
            var getproduct = dbContext.Set<Product>().Find(Id);
            dbContext.Set<Product>().Remove(getproduct);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
