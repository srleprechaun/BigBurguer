using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BigBurguer.Api.Infrastructure.Models
{
    public class OrderProduct
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        [Column(TypeName = "numeric(10,2)")]
        public double Price { get; set; }
        [Column(TypeName = "numeric(10,2)")]
        public double? Discount { get; set; }

        [JsonIgnore]
        public Product Product { get; set; }
    }
}
