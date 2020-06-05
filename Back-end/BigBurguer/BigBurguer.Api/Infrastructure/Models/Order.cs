using System;

namespace BigBurguer.Api.Infrastructure.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int OrderStatusId { get; set; }
        public DateTime OrderDate { get; set; }

        public Customer Customer { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}
