using BigBurguer.Api.Infrastructure.Models;
using BigBurguer.Api.Services;
using BigBurguer.Api.Views;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BigBurguer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public ActionResult<List<Customer>> Index()
        {
            try
            {
                var result = _customerService.GetAll();
                return Ok(result);
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }

        [HttpGet("{customerId}")]
        public ActionResult<Customer> Details([FromRoute]string customerId)
        {
            var result = _customerService.GetId(customerId);

            if (result == null)
            {
                return BadRequest(ModelState);
            }

            return Ok(result);
        }

        [HttpPut("{customerId}")]
        public IActionResult Edit([FromRoute]string customerId, [FromBody]CustomerViewModel customerModel)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            var result = _customerService.EditCustomer(customerId, customerModel);
            if (result == null)
            {
                return BadRequest(ModelState);
            }
            return Ok();
        }

        [HttpDelete("{customerId:int}")]
        public IActionResult Delete([FromRoute]string customerId)
        {
            var result = _customerService.DeleteCustomer(customerId);
            if (result == null)
            {
                return BadRequest();
            }
            return StatusCode(204);
        }
    }
}
