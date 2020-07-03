using BigBurguer.Api.Infrastructure.Models;
using BigBurguer.Api.Services;
using BigBurguer.Api.Views;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Enum = BigBurguer.Api.Infrastructure.Enum;

namespace BigBurguer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService customerService)
        {
            _userService = customerService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Employee")]
        public ActionResult<List<User>> Index()
        {
            try
            {
                var result = _userService.GetAll();
                return Ok(result);
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }

        [HttpGet("{userId}")]
        [Authorize(Roles = "Admin, Employee")]
        public ActionResult<User> Details([FromRoute]string userId)
        {
            var result = _userService.GetId(userId);

            if (result == null)
            {
                return BadRequest(ModelState);
            }

            return Ok(result);
        }

        [HttpGet("{userId}/role")]
        [Authorize]
        public ActionResult<string> UserRole([FromRoute]string userId)
        {
            var result = _userService.GetUserRole(userId);

            if (result == null)
            {
                return BadRequest(ModelState);
            }

            return Ok(result);
        }

        [HttpPut("{userId}")]
        [Authorize(Roles = "Admin, Employee")]
        public IActionResult Edit([FromRoute]string userId, [FromBody]UserViewModel userModel)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            var result = _userService.EditUser(userId, userModel);
            if (result == null)
            {
                return BadRequest(ModelState);
            }
            return Ok();
        }

        [HttpPut("{userId}/role")]
        [Authorize(Roles = "Admin")]
        public IActionResult AddRole([FromRoute]string userId, [FromBody]Enum.Role role)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            var result = _userService.AddRoleToUser(userId, role);
            if (result == null)
            {
                return BadRequest(ModelState);
            }
            return StatusCode(200);
        }
    }
}
