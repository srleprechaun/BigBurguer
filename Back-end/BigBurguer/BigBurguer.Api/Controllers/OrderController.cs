using System.Collections.Generic;
using BigBurguer.Api.Infrastructure.Models;
using BigBurguer.Api.Views;
using Microsoft.AspNetCore.Mvc;
using BigBurguer.Api.Services;
using Microsoft.AspNetCore.Authorization;

namespace BigBurguer.Api.Controllers
{
    [Authorize]
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
        public ActionResult<IEnumerable<Order>> GetAll()
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
        public ActionResult<IEnumerable<Order>> Details([FromRoute]int orderId)
        {
            var result = _orderService.GetOrderById(orderId);

            if (result == null)
            {
                return BadRequest(ModelState);
            }

            return Ok(result);
        }

        [HttpGet("{userId}")]
        public ActionResult<IEnumerable<Order>> GetByCustomer([FromRoute]string userId)
        {
            var result = _orderService.GetByCustomerId(userId);

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
            return Created($"/api/Order/{result}", null);
        }

        [HttpPut("{orderId:int}/OrderStatus")]
        public IActionResult Post([FromRoute]int orderId)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            var result = _orderService.ChangeStatus(orderId);
            if(result == 0)
            {
                return BadRequest();
            }
            return StatusCode(200, result);
        }

        [HttpDelete("{orderId:int}")]
        public IActionResult Delete([FromRoute]int orderId)
        {
            var result = _orderService.DeleteOrder(orderId);
            if (result == null)
            {
                return BadRequest();
            }
            return StatusCode(204);
        }

        [HttpDelete("{orderId:int}/OrderProduct/{orderProdId:int}")]
        public IActionResult DeleteOrderProduct([FromRoute]int orderId, [FromRoute]int orderProdId)
        {
            var result = _orderService.DeleteOrderProduct(orderId, orderProdId);
            if (result == null)
            {
                return BadRequest();
            }
            return StatusCode(204);
        }
    }
}
