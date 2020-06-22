using FluentValidation;
using System.Collections.Generic;

namespace BigBurguer.Api.Views
{
    public class ProductViewModel
    {
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; }
        public List<ProductIngredientViewModel> Ingredients { get; set; }

    }
    public class ProductValidator : AbstractValidator<ProductViewModel>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Product name is required");
            RuleFor(x => x.Name).Length(0, 100);
            RuleFor(x => x.Type).NotEmpty().WithMessage("Product name is required");
            RuleFor(x => x.Type).Length(0, 50);
        }
    }
}
