
using System.ComponentModel.DataAnnotations.Schema;

namespace BigBurguer.Api.Infrastructure.Models
{
    public class OrderProduct
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        [Column(TypeName = "numeric(10,2)")]
        public double Price { get; set; }
        [Column(TypeName = "numeric(10,2)")]
        public double? Discount { get; set; }

        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }
    }
}
