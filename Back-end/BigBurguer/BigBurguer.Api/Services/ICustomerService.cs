using BigBurguer.Api.Infrastructure.Models;
using BigBurguer.Api.Views;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;

namespace BigBurguer.Api.Services
{
    public interface ICustomerService
    {
        public List<Customer> GetAll();
        public Customer GetId(int customerId);
        public EntityEntry<Customer> CreateCustomer(CustomerViewModel customerModel);   
        public EntityEntry<Customer> EditCustomer(int id, CustomerViewModel customerModel);
        public EntityEntry<Customer> DeleteCustomer(int customerId);
    }
}
