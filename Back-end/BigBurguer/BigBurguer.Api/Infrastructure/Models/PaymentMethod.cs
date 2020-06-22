using System.ComponentModel.DataAnnotations.Schema;

namespace BigBurguer.Api.Infrastructure.Models
{
    public class PaymentMethod
    {
        public int Id { get; set; }
        [Column(TypeName = "varchar(70)")]
        public string Name { get; set; }
    }
}
