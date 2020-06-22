using BigBurguer.Api.Infrastructure.Models;
using BigBurguer.Api.Views;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Linq;

namespace BigBurguer.Api.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly AppDbContext _context;
        public CustomerService(AppDbContext context)
        {
            _context = context;
        }

        public List<Customer> GetAll()
        {
            return _context.Customers.ToList();
        }

        public Customer GetId(int customerId)
        {
            return _context.Customers.Find(customerId);
        }

        public EntityEntry<Customer> CreateCustomer(CustomerViewModel customerModel)
        {
            Customer customer = new Customer()
            {
                BirthDate = customerModel.BirthDate,
                Cpf = customerModel.Cpf,
                Name = customerModel.Name,
                Password = customerModel.Password
            };

            var result = _context.Customers.Add(customer);

            if (result != null)
            {
                _context.SaveChanges();

                return result;
            }
            return null;
        }

        public EntityEntry<Customer> EditCustomer(int customerId, CustomerViewModel customerModel)
        {
            var customer = _context.Customers.Find(customerId);

            if (customer != null)
            {
                customer.Name = customerModel.Name;
                customer.BirthDate = customerModel.BirthDate;
                customer.Cpf = customerModel.Cpf;
                customer.Password = customerModel.Password;

                var result = _context.Customers.Update(customer);

                _context.SaveChanges();
                return result;
            }
            return null;
        }

        public EntityEntry<Customer> DeleteCustomer(int productId)
        {
            var customer = _context.Customers.Where(i => i.Id == productId).FirstOrDefault();

            if (customer != null)
            {
                var result = _context.Customers.Remove(customer);
                _context.SaveChanges();

                return result;
            }

            return null;
        }
    }
}
