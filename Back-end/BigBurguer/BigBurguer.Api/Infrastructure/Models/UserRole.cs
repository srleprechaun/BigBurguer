using System.ComponentModel.DataAnnotations.Schema;

namespace BigBurguer.Api.Infrastructure.Models
{
    public class UserRole
    {
        public int Id { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string UserId { get; set; }
        public int RoleId { get; set; }

        public User User { get; set; }
        public Role Role { get; set; }
    }
}
