using Microsoft.AspNetCore.Mvc;

namespace BigBurguer.Api.Controller
{
    [ApiController]
    public class ProductController : ControllerBase
    {
        // GET: /api/v1/product
        [HttpGet]
        public ActionResult<string> GetAll()
        {
            return "test";
        }

        // POST: /api/v1/product
        // PUT: /api/v1/product/<id>
        // DELETE: /api/v1/product/<id>
    }
}
