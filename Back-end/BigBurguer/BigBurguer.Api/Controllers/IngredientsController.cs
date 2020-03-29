using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BigBurguer.Api.Infrastructure.Models;

namespace BigBurguer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public IngredientsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Ingredients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ingredient>>> GetAll()
        {
             try
            {
                var result = await _context.Ingredient.ToListAsync();
                return Ok(result);
            }
            catch (System.Exception e)
            {
                throw e;

            }
        }

        // GET: api/Ingredients/<id:int>
        // PUT: api/Ingredients/<id:int>
        // POST: api/Ingredients
        // DELETE: api/Ingredients/<id:int>
    }
}
