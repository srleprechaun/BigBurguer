using FluentValidation;

namespace BigBurguer.Api.Views
{
    public class ProductViewModel
    {
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int? IngredientId { get; set; }

    }
    public class ProductValidator : AbstractValidator<ProductViewModel>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Product name is required");
            RuleFor(x => x.Name).Length(0, 50);
        }
    }
}
