using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BigBurguer.Api.Infrastructure.Models
{
    public class User
    {
        public int Id { get; set; }
        public DateTime BirthDate { get; set; }
        public string Password { get; set; }
        public string  Name { get; set; }
        public string Cpf { get; set; }
    }
}
