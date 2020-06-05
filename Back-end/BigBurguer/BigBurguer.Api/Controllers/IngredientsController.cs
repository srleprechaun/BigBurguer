using System.Collections.Generic;
using BigBurguer.Api.Infrastructure.Models;
using BigBurguer.Api.Views;
using Microsoft.AspNetCore.Mvc;
using BigBurguer.Api.Services;

namespace BigBurguer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        private readonly IIngredientService _ingredientService;

        public IngredientsController(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Ingredient>> GetAll()
        {
             try
            {
                var result = _ingredientService.GetAll();
                return Ok(result);
            }
            catch (System.Exception e)
            {
                throw e;

            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]IngredientViewModel ingredientModel)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            var result = _ingredientService.CreateIngredient(ingredientModel);
            if (result == null)
            {
                return BadRequest(ModelState);
            }
            return Created($"/api/[controller]/{result}", null);
        }

        [HttpGet("{id}")]
        public ActionResult<Ingredient> Get([FromRoute]int id)
        {
            var result = _ingredientService.GetId(id);

            if (result == null)
            {
                return BadRequest(ModelState);
            }

            return Ok(result);
        }

        [HttpPut("{id}")]
        public ActionResult<Ingredient> Put([FromRoute]int id, [FromBody]IngredientViewModel ingredientModel)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            var result = _ingredientService.EditIngredient(id, ingredientModel);
            if (result == false)
            {
                return BadRequest(ModelState);
            }
            return Ok(id);
        }

        [HttpDelete("{id}")]
        public ActionResult<Ingredient> Delete([FromRoute]int id)
        {
            var result = _ingredientService.DeleteIngredient(id);
            if (result == false)
            {
                return BadRequest();
            }
            return Ok(id);
        }
    }
}
