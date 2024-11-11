using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.OrderAggregate;
using Talabat.Core.Repositories;
using Talabat.Core.Services;

namespace Talabat.Service
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<DeliveryMethod> _deliveryMethodRepo;
        private readonly IGenericRepository<Order> _orderRepo;

        public OrderService(IBasketRepository basketRepository, 
            IGenericRepository<Product> ProductRepo,
            IGenericRepository<DeliveryMethod> DeliveryMethodRepo,
            IGenericRepository<Order> OrderRepo)
        {
            _basketRepository = basketRepository;
            _productRepo = ProductRepo;
            _deliveryMethodRepo = DeliveryMethodRepo;
            _orderRepo = OrderRepo;
        }
        public async Task<Order> CreateOrderAsync(string BuyerEmail, string BasketId, int DeliveryMethodId, Address ShippingAddress)
        {
            var Basket = await _basketRepository.GetBasketAsync(BasketId);

            var OrderItems = new List<OrderItem>();

            if (Basket?.Items.Count > 0)
            {
                foreach(var item in Basket.Items)
                {
                    var Product = await _productRepo.GetByIdAsync(item.Id);
                    var ProductItemOrdered = new ProductItemOrdered(Product.Id, Product.Name, Product.PictureUrl);
                    var OrderItem = new OrderItem(ProductItemOrdered, item.Quantity, Product.Price);
                    OrderItems.Add(OrderItem);
                }
            }

            var SubTotal = OrderItems.Sum(item => item.Price * item.Quantity);  

            var DeliveryMethod = await _deliveryMethodRepo.GetByIdAsync(DeliveryMethodId);

            var Order = new Order(BuyerEmail, ShippingAddress, DeliveryMethod, OrderItems, SubTotal);

            await _orderRepo.AddAsync(Order);

            // Save Order in Database

        }

        public Task<Order> GetOrderBtIdForSpecificUserAsync(string BuyerEmail, int OrderId)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Order>> GetOrdersForSpecificUserAsync(string BuyerEmail)
        {
            throw new NotImplementedException();
        }
    }
}
