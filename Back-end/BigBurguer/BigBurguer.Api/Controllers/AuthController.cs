using BigBurguer.Api.Infrastructure.Models;
using BigBurguer.Api.Services;
using BigBurguer.Api.Views;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BigBurguer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IAuthService _authService;
        ICustomerService _customerService;
        private readonly AppDbContext _context;

        public AuthController(IAuthService authService, AppDbContext context, ICustomerService customerService)
        {
            _authService = authService;
            _context = context;
            _customerService = customerService;
        }

        [HttpPost("login")]
        public ActionResult<AuthData> Post([FromBody]LoginViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = model.Cpf == null ?
                            _context.Customers.FirstOrDefault(c => c.Email == model.Email) :
                            _context.Customers.FirstOrDefault(c => c.Cpf == model.Cpf);


            if (user == null)
            {
                return BadRequest(new { email = "No user with this credencial" });
            }

            var passwordValid = _authService.VerifyPassword(model.Password, user.Password);
            if (!passwordValid)
            {
                return BadRequest(new { password = "invalid password" });
            }

            return _authService.GetAuthData(user.Id, user.Name);
        }

        [HttpPost("register")]
        public ActionResult<AuthData> Post([FromBody]CustomerViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var emailUniq = isEmailUniq(model.Email);
            if (!emailUniq) return BadRequest(new { email = "user with this email already exists" });
            var cpfUniq = IsCpfUniq(model.Cpf);
            if (!cpfUniq) return BadRequest(new { cpf = "user with this cpf already exists" });

            var id = Guid.NewGuid().ToString();

            _customerService.CreateCustomer(id, model);

            return _authService.GetAuthData(id, model.Name);
        }

        private bool isEmailUniq(string email)
        {
            var user = _context.Customers.FirstOrDefault(c => c.Email == email);
            return user == null;
        }

        private bool IsCpfUniq(string cpf)
        {
            var user = _context.Customers.FirstOrDefault(c => c.Cpf == cpf);
            return user == null;
        }
    }
}
