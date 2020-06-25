using FluentValidation;
using System.ComponentModel.DataAnnotations.Schema;

namespace BigBurguer.Api.Views
{
	public class LoginViewModel
	{
		public string Password { get; set; }
		public string Email { get; set; }
		public string Cpf { get; set; }
	}

	public class LoginValidator : AbstractValidator<LoginViewModel>
	{
		public LoginValidator()
		{
			RuleFor(x => x.Password).NotEmpty().WithMessage("Password name is required");
			RuleFor(x => x.Password).Length(0, 50);
		}
	}
}
