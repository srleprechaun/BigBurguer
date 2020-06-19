using FluentValidation;

namespace BigBurguer.Api.Views
{
	public class OrderProductViewModel
	{
		public int ProductId { get; set; }
		public int Quantity { get; set; }
		public double Price { get; set; }
		public double? Discount { get; set; }
	}

	public class OrderProductValidator : AbstractValidator<OrderProductViewModel>
	{
		public OrderProductValidator()
		{
			RuleFor(x => x.ProductId).NotEmpty().WithMessage("ProductId is required");
			RuleFor(x => x.Quantity).NotEmpty().WithMessage("Quantity is required");
			RuleFor(x => x.Price).NotEmpty().WithMessage("Price is required");
			RuleFor(x => x.Price).LessThanOrEqualTo(28).ScalePrecision(3, 2).WithMessage("Price cannot be greater than 28 and must be a precison of 3");
		}
	}
}
