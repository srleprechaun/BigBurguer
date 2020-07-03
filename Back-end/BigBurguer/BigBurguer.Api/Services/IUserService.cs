using BigBurguer.Api.Infrastructure.Models;
using BigBurguer.Api.Views;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using Enum = BigBurguer.Api.Infrastructure.Enum;

namespace BigBurguer.Api.Services
{
    public interface IUserService
    {
        public List<User> GetAll();
        public User GetId(string customerId);
        public EntityEntry<User> CreateUser(string id, UserViewModel customerModel);   
        public EntityEntry<User> EditUser(string id, UserViewModel customerModel);
        public EntityEntry<UserRole> AddRoleToUser(string userId, Enum.Role role);
        public string GetUserRole(string userId);
    }
}
