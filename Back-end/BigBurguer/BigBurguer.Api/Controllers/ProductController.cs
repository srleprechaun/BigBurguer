using BigBurguer.Api.Views;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BigBurguer.Api.Controller
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        // GET: /api/v1/product
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var test = new Product();

            test.ImageUrl = "https://cdn4.bobsfa.com.br/assets/cardapio/produtos/double-cheese.png";
            test.Name = "Double Cheese";
            test.Ingredients = "Salt, pepper and milk.";
            test.Price = 13;
 
            return Ok(test);
        }

        // POST: /api/v1/product
        // PUT: /api/v1/product/<id>
        // DELETE: /api/v1/product/<id>
    }
}
