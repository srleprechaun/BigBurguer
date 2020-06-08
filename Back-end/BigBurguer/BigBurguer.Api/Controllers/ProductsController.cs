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

        [HttpGet]
        public ActionResult<List<Product>> Index()
        {
             try
            {
                var result = _productService.Get();
                return Ok(result);
            }
            catch (System.Exception e)
            {
                throw e;

            }
        }

        [HttpGet("{productId}")]
        public ActionResult<Product> Details([FromRoute]int productId)
        {
            var result = _productService.GetId(productId);

            if (result == null)
            {
                return BadRequest(ModelState);
            }

            return Ok(result);
        }

        [HttpGet("{productId}/ingredients")]
        public ActionResult<Product> IngredientsDetails([FromRoute]int productId)
        {
            var result = _productService.GetIngredientsDetails(productId);

            if (result == null)
            {
                return BadRequest(ModelState);
            }

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create([FromBody]ProductViewModel productModel)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            var result = _productService.CreateProduct(productModel);
            if (result == null)
            {
                return BadRequest(ModelState);
            }
            return Created($"/api/Products/{result}", null);
        }

        [HttpPut("{productId}")]
        public ActionResult<Product> Edit([FromRoute]int productId, [FromBody]ProductViewModel productModel)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            var result = _productService.EditProduct(productId, productModel);
            if (result == false)
            {
                return BadRequest(ModelState);
            }
            return Ok(productId);
        }

        [HttpDelete("{productId}")]
        public ActionResult<Product> Delete([FromRoute]int productId)
        {
            var result = _productService.DeleteProduct(productId);
            if (result == false)
            {
                return BadRequest();
            }
            return Ok(productId);
        }
    }
}
