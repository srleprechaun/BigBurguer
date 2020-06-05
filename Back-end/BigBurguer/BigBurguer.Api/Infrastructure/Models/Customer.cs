using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BigBurguer.Api.Infrastructure.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public DateTime BirthDate { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string Password { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string  Name { get; set; }
        [Column(TypeName = "varchar(15)")]
        public string Cpf { get; set; }
    }
}
