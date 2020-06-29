using BigBurguer.Api.Infrastructure.Models;
using BigBurguer.Api.Views;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;

namespace BigBurguer.Api.Services
{
    public interface IOrderService
    {
        public List<Order> GetAll();
        public EntityEntry<Order> CreateOrder(OrderViewModel orderViewModel);
        public List<Order> GetOrderById(int orderId);
        public List<Order> GetByCustomerId(string customerId);
        OrderProduct DeleteOrderProduct(int orderId, int orderProductId);
        Order DeleteOrder(int orderId);
        int ChangeStatus(int orderId);
    }
}
