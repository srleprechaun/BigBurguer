using System.Collections.Generic;
using BigBurguer.Api.Infrastructure.Models;
using BigBurguer.Api.Views;
using Microsoft.AspNetCore.Mvc;
using BigBurguer.Api.Services;
using Microsoft.AspNetCore.Authorization;

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
            return Created($"/api/Ingredients/{result}", null);
        }

        [HttpGet("{ingredientId:int}")]
        public ActionResult<Ingredient> Get([FromRoute]int ingredientId)
        {
            var result = _ingredientService.GetId(ingredientId);

            if (result == null)
            {
                return BadRequest(ModelState);
            }

            return Ok(result);
        }

        [HttpPut("{ingredientId:int}")]
        public ActionResult<Ingredient> Put([FromRoute]int ingredientId, [FromBody]IngredientViewModel ingredientModel)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            var result = _ingredientService.EditIngredient(ingredientId, ingredientModel);
            if (result == false)
            {
                return BadRequest(ModelState);
            }
            return Ok(ingredientId);
        }

        [HttpDelete("{ingredientId:int}")]
        public ActionResult<Ingredient> Delete([FromRoute]int ingredientId)
        {
            var result = _ingredientService.DeleteIngredient(ingredientId);
            if (result == false)
            {
                return BadRequest();
            }
            return StatusCode(204);
        }
    }
}
