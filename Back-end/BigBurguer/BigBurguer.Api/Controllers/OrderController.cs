using System.Collections.Generic;
using BigBurguer.Api.Infrastructure.Models;
using BigBurguer.Api.Views;
using Microsoft.AspNetCore.Mvc;
using BigBurguer.Api.Services;

namespace BigBurguer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<OrderProduct>> GetAll()
        {
             try
            {
                var result = _orderService.GetAll();
                return Ok(result);
            }
            catch (System.Exception e)
            {
                throw e;

            }
        }

        [HttpGet("{orderId:int}")]
        public ActionResult<IEnumerable<OrderProduct>> Details([FromRoute]int orderId)
        {
            var result = _orderService.GetOrderById(orderId);

            if (result == null)
            {
                return BadRequest(ModelState);
            }

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromBody]OrderViewModel orderViewModel)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            var result = _orderService.CreateOrder(orderViewModel);
            if (result == null)
            {
                return BadRequest(ModelState);
            }
            return Created($"/api/[controller]/{result}", null);
        }

        [HttpPut("{orderProductId:int}")]
        public ActionResult<Ingredient> Put([FromRoute]int orderProductId, [FromBody]OrderProductViewModel orderViewModel)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            var result = _orderService.UpdateOrderProduct(orderProductId, orderViewModel);
            if (result == false)
            {
                return BadRequest(ModelState);
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public ActionResult<Ingredient> Delete([FromRoute]int id)
        {
            var result = _orderService.DeleteOrderProduct(id);
            if (result == false)
            {
                return BadRequest();
            }
            return Ok(id);
        }
    }
}
