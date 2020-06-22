﻿using BigBurguer.Api.Infrastructure.Models;
using BigBurguer.Api.Services;
using BigBurguer.Api.Views;
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

        [HttpGet("{customerId:int}")]
        public ActionResult<Customer> Details([FromRoute]int customerId)
        {
            var result = _customerService.GetId(customerId);

            if (result == null)
            {
                return BadRequest(ModelState);
            }

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create([FromBody]CustomerViewModel customerModel)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            var result = _customerService.CreateCustomer(customerModel);
            if (result == null)
            {
                return BadRequest(ModelState);
            }
            return Created($"/api/Customer/{result}", null);
        }

        [HttpPut("{customerId:int}")]
        public IActionResult Edit([FromRoute]int customerId, [FromBody]CustomerViewModel customerModel)
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
        public IActionResult Delete([FromRoute]int customerId)
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
