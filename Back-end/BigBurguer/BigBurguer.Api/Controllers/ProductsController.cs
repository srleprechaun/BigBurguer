using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BigBurguer.Api.Infrastructure.Models;

namespace BigBurguer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
             try
            {
                var result = await _context.Products.ToListAsync();
                return Ok(result);
            }
            catch (System.Exception e)
            {
                throw e;

            }
        }

        // GET: api/Products/<id:int>
        // PUT: api/Products/<id:int>
        // POST: api/Products
        // DELETE: api/Products/<id:int>
    }
}
