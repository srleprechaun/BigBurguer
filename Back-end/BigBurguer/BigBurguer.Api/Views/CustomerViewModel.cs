using FluentValidation;
using System;

namespace BigBurguer.Api.Views
{
    public class CustomerViewModel
    {
        public DateTime BirthDate { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
    }
    public class CustomerValidator : AbstractValidator<CustomerViewModel>
    {
        public CustomerValidator()
        {
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
            RuleFor(x => x.Password).Length(0, 50);

            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Name).Length(0, 100);

            RuleFor(x => x.Cpf).NotEmpty().WithMessage("Cpf is required");
            RuleFor(x => x.Name).Length(0, 15);
        }
    }
}
