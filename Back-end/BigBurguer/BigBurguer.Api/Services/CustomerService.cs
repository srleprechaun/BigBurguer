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
        IAuthService _authService;

        public CustomerService(AppDbContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        public List<Customer> GetAll()
        {
            return _context.Customers.ToList();
        }

        public Customer GetId(string customerId)
        {
            return _context.Customers.Find(customerId);
        }

        public EntityEntry<Customer> CreateCustomer(string id, CustomerViewModel customerModel)
        {
            Customer customer = new Customer()
            {
                Id = id,
                BirthDate = customerModel.BirthDate,
                Cpf = customerModel.Cpf,
                Name = customerModel.Name,
                Password = _authService.HashPassword(customerModel.Password),
                Email = customerModel.Email
            };

            var result = _context.Customers.Add(customer);

            if (result != null)
            {
                _context.SaveChanges();

                return result;
            }
            return null;
        }

        public EntityEntry<Customer> EditCustomer(string customerId, CustomerViewModel customerModel)
        {
            var customer = _context.Customers.Find(customerId);

            if (customer != null)
            {
                customer.Name = customerModel.Name;
                customer.BirthDate = customerModel.BirthDate;
                customer.Cpf = customerModel.Cpf;
                customer.Password = customerModel.Password;
                customer.Email = customerModel.Email;

                var result = _context.Customers.Update(customer);

                _context.SaveChanges();
                return result;
            }
            return null;
        }

        public EntityEntry<Customer> DeleteCustomer(string customerId)
        {
            var customer = _context.Customers.Where(i => i.Id == customerId).FirstOrDefault();

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
