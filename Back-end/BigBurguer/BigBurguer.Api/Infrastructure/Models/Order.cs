using System;
using System.Collections.Generic;

namespace BigBurguer.Api.Infrastructure.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int OrderStatusId { get; set; }
        public DateTime OrderDate { get; set; }
        public int PaymentMethodId { get; set; }

        public Customer Customer { get; set; }
        public ICollection<OrderProduct> OrderProducts { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}
