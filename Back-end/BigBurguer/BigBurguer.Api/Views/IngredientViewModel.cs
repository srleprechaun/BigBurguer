using FluentValidation;

namespace BigBurguer.Api.Views
{
	public class IngredientViewModel
	{
		public string Name { get; set; }
		public int StockQuantity { get; set; }
	}

	public class IngredientValidator : AbstractValidator<IngredientViewModel>
	{
		public IngredientValidator()
		{
			RuleFor(x => x.Name).NotEmpty().WithMessage("Ingredient name is required");
			RuleFor(x => x.Name).Length(0, 50);
		}
	}
}
