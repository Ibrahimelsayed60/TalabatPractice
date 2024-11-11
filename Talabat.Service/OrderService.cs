using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.OrderAggregate;
using Talabat.Core.Services;

namespace Talabat.Service
{
    public class OrderService : IOrderService
    {
        public Task<Order> CreateOrderAsync(string BuyerEmail, int BasketId, int DeliveryMethodId, Address ShippingAddress)
        {
            throw new NotImplementedException();
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
