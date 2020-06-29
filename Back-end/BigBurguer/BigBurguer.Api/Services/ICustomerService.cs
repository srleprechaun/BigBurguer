﻿using BigBurguer.Api.Infrastructure.Models;
using BigBurguer.Api.Views;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;

namespace BigBurguer.Api.Services
{
    public interface ICustomerService
    {
        public List<Customer> GetAll();
        public Customer GetId(string customerId);
        public EntityEntry<Customer> CreateCustomer(string id, CustomerViewModel customerModel);   
        public EntityEntry<Customer> EditCustomer(string id, CustomerViewModel customerModel);
        public EntityEntry<Customer> DeleteCustomer(string customerId);
    }
}
