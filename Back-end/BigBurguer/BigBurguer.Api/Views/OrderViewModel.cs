using FluentValidation;
using System;
using System.Collections.Generic;

namespace BigBurguer.Api.Views
{
	public class OrderViewModel
	{
		public DateTime OrderDate { get; set; }
		public string CustomerId { get; set; }
		public int PaymentMethodId { get; set; }

		public ICollection<OrderProductViewModel> Products { get; set; }
	}

	public class OrderValidator : AbstractValidator<OrderViewModel>
	{
		public OrderValidator()
		{
			RuleFor(x => x.OrderDate)
				.NotEmpty()
				.WithMessage("Order date is required")
				.LessThan(p => DateTime.Now)
				.WithMessage("Order date must be in the past");
		}
	}
}
