using System.Collections.Generic;
using BigBurguer.Api.Infrastructure.Models;
using BigBurguer.Api.Views;
using Microsoft.AspNetCore.Mvc;
using BigBurguer.Api.Services;


namespace BigBurguer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        // GET: api/Products
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAll()
        {
             try
            {
                var result = _productService.GetAll();
                return Ok(result);
            }
            catch (System.Exception e)
            {
                throw e;

            }
        }
        // POST: api/Products
        
        [HttpPost]
        public IActionResult Post([FromBody]ProductViewModel productModel)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            var result = _productService.CreateProductAsync(productModel);
            if (result == null)
            {
                return BadRequest(ModelState);
            }
            return Created($"/api/[controller]/{result}", null);
        }

        // GET: api/Products/<id:int>
        [HttpGet("{id}")]
        public ActionResult<Product> Get([FromRoute]int id)
        {
            var result = _productService.GetId(id);

            if (result == null)
            {
                return BadRequest(ModelState);
            }

            return Ok(result);
        }

        // PUT: api/Products/<id:int>
        [HttpPut("{id}")]
        public ActionResult<Product> Put([FromRoute]int id, [FromBody]ProductViewModel productModel)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            var result = _productService.EditProductAsync(id, productModel);
            if (result == false)
            {
                return BadRequest(ModelState);
            }
            return Ok(id);
        }

        // DELETE: api/Products/<id:int>
        [HttpDelete("{id}")]
        public ActionResult<Product> Delete([FromRoute]int id)
        {
            var result = _productService.DeleteProductAsync(id);
            if (result == false)
            {
                return BadRequest();
            }
            return Ok(id);
        }
    }
}
