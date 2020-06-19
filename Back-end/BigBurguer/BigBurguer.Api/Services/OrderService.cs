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

        public List<OrderProduct> GetAll()
        {
            return _context.OrderProducts.Include(p => p.Product).Include(o => o.Order).ToList();
        }

        public List<OrderProduct> GetOrderById(int orderId)
        {
            return _context.OrderProducts
                .Include(p => p.Product)
                .Include(o => o.Order)
                .Where(o => o.OrderId == orderId)
                .ToList();
        }

        public EntityEntry<Order> CreateOrder(OrderViewModel orderViewModel)
        {
            Order order = new Order()
            {
                OrderDate = orderViewModel.OrderDate,
                OrderStatusId = (int)OrderStatus.OrderReceived,
                CustomerId = orderViewModel.CustomerId
            };

            var result = _context.Orders.Add(order);

            if (result != null)
            {
                _context.SaveChanges();

                foreach (var orderProduct in orderViewModel.Products)
                {
                    OrderProduct orderProd = new OrderProduct();
                    orderProd.OrderId = result.Entity.Id;
                    orderProd.ProductId = orderProduct.ProductId;
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

        public bool UpdateOrderProduct(int orderProductId, OrderProductViewModel orderProductViewModel)
        {
            var orderProduct = _context.OrderProducts.Find(orderProductId);

            if (orderProduct != null)
            {
                orderProduct.ProductId = orderProduct.ProductId;
                orderProduct.Price = orderProductViewModel.Price;
                orderProduct.Quantity = orderProductViewModel.Quantity;
                orderProduct.Discount = orderProductViewModel.Discount;

                _context.OrderProducts.Update(orderProduct);
                _context.SaveChanges();

                return true;
            }
            return false;
        }

        public bool DeleteOrderProduct(int orderProductId)
        {
            var orderProduct = _context.OrderProducts.Find(orderProductId);

            if (orderProduct != null)
            {
                _context.OrderProducts.Remove(orderProduct);
                _context.SaveChanges();

                return true;
            }

            return false;
        }
    }
}
