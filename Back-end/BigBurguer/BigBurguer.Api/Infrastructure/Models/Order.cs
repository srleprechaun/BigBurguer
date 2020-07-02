using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BigBurguer.Api.Infrastructure.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string CustomerId { get; set; }
        public int OrderStatusId { get; set; }
        public DateTime OrderDate { get; set; }
        public int PaymentMethodId { get; set; }

        public User Customer { get; set; }
        public ICollection<OrderProduct> OrderProducts { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}
