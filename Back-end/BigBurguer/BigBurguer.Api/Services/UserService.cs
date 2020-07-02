using BigBurguer.Api.Infrastructure.Models;
using BigBurguer.Api.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Linq;
using Enum = BigBurguer.Api.Infrastructure.Enum;

namespace BigBurguer.Api.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        IAuthService _authService;

        public UserService(AppDbContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        public List<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User GetId(string userId)
        {
            return _context.Users.Find(userId);
        }

        public string GetUserRole(string userId)
        {
            return _context.UserRoles.Where(u => u.UserId == userId).Include(x => x.Role).FirstOrDefault().Role.Name;
        }

        public EntityEntry<User> CreateUser(string id, UserViewModel userModel)
        {
            User user = new User()
            {
                Id = id,
                BirthDate = userModel.BirthDate,
                Cpf = userModel.Cpf,
                Name = userModel.Name,
                Password = _authService.HashPassword(userModel.Password),
                Email = userModel.Email
            };

            UserRole userRole = new UserRole()
            {
                RoleId = (int)Enum.Role.Customer,
                UserId = id
            };

            var result = _context.Users.Add(user);

            if (result != null)
            {
                _context.SaveChanges();

                _context.UserRoles.Add(userRole);
                _context.SaveChanges();

                return result;
            }
            return null;
        }

        public EntityEntry<User> EditUser(string userId, UserViewModel userModel)
        {
            var user = _context.Users.Find(userId);

            if (user != null)
            {
                user.Name = userModel.Name;
                user.BirthDate = userModel.BirthDate;
                user.Cpf = userModel.Cpf;
                user.Password = userModel.Password;
                user.Email = userModel.Email;

                var result = _context.Users.Update(user);

                _context.SaveChanges();
                return result;
            }
            return null;
        }

        public EntityEntry<User> DeleteUser(string userId)
        {
            var user = _context.Users.Where(i => i.Id == userId).FirstOrDefault();

            if (user != null)
            {
                var result = _context.Users.Remove(user);
                _context.SaveChanges();

                return result;
            }

            return null;
        }

        public EntityEntry<UserRole> AddRoleToUser(string userId, Enum.Role role)
        {
            var user = _context.UserRoles.FirstOrDefault(u => u.UserId == userId);

            if (user == null)
            {
                UserRole userRole = new UserRole
                {
                    UserId = userId,
                    RoleId = (int)role
                };

                var result = _context.UserRoles.Add(userRole);
                _context.SaveChanges();

                return result;
            }

            user.RoleId = (int)role;

            var upResult = _context.UserRoles.Update(user);
            _context.SaveChanges();

            return upResult;
        }
    }
}
