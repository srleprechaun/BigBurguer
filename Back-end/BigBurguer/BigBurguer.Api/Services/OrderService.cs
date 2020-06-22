using BigBurguer.Api.Infrastructure.Models;
using BigBurguer.Api.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Linq;
using OrderStatus = BigBurguer.Api.Infrastructure.Enum.OrderStatus;

namespace BigBurguer.Api.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }

        public List<Order> GetAll()
        {
            return _context.Orders
                .Include(op => op.OrderProducts)
                .Include(c => c.Customer)
                .Include(os => os.OrderStatus)
                .Include(p => p.PaymentMethod)
                .ToList();
        }

        public List<Order> GetOrderById(int orderId)
        {
            return _context.Orders
                 .Include(op => op.OrderProducts)
                 .Include(c => c.Customer)
                 .Include(os => os.OrderStatus)
                 .Include(p => p.PaymentMethod)
                 .Where(o => o.Id == orderId)
                 .ToList();
        }

        public EntityEntry<Order> CreateOrder(OrderViewModel orderViewModel)
        {
            Order order = new Order()
            {
                OrderDate = orderViewModel.OrderDate,
                OrderStatusId = (int)OrderStatus.OrderReceived,
                CustomerId = orderViewModel.CustomerId,
                PaymentMethodId = (int) orderViewModel.PaymentMethodId
            };

            var result = _context.Orders.Add(order);

            if (result != null)
            {
                _context.SaveChanges();

                foreach (var orderProduct in orderViewModel.Products)
                {
                    OrderProduct orderProd = new OrderProduct();
                    orderProd.ProductId = orderProduct.ProductId;
                    orderProd.OrderId = result.Entity.Id;
                    orderProd.ProductName = _context.Products.Find(orderProduct.ProductId).Name;
                    orderProd.Price = orderProduct.Price;
                    orderProd.Quantity = orderProduct.Quantity;
                    orderProd.Discount = orderProduct.Discount;

                    _context.OrderProducts.Add(orderProd);
                    _context.SaveChanges();
                }

                return result;
            }
            return null;
        }

        public Order DeleteOrder(int orderId)
        {
            var order = _context.Orders.Where(o => o.Id == orderId).FirstOrDefault();

            if (order != null)
            {
                _context.Orders.Remove(order);
                _context.SaveChanges();

                return order;
            }

            return null;
        }

        public OrderProduct DeleteOrderProduct(int orderId, int orderProductId)
        {
            var order = _context.Orders.Include(op => op.OrderProducts).Where(o => o.Id == orderId).FirstOrDefault();

            if (order != null)
            {
                var orderProd = order.OrderProducts.First(op => op.Id == orderProductId);
                _context.OrderProducts.Remove(orderProd);
                _context.SaveChanges();

                return orderProd;
            }

            return null;
        }
    }
}
