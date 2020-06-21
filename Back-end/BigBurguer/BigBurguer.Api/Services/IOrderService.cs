using BigBurguer.Api.Infrastructure.Models;
using BigBurguer.Api.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;

namespace BigBurguer.Api.Services
{
    public interface IOrderService
    {
        public List<OrderProduct> GetAll();
        public EntityEntry<Order> CreateOrder(OrderViewModel orderViewModel);
        public List<OrderProduct> GetOrderById(int orderId);
        public bool UpdateOrderProduct(int orderProductId, OrderProductViewModel orderProductViewModel);
        public bool DeleteOrderProduct(int orderProductId);
    }
}
