﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.OrderAggregate;

namespace Talabat.Core.Services
{
    public interface IOrderService
    {

        Task<Order> CreateOrderAsync(string BuyerEmail, int BasketId, int DeliveryMethodId, Address ShippingAddress);

        Task<IReadOnlyList<Order>> GetOrdersForSpecificUserAsync(string BuyerEmail);

        Task<Order> GetOrderBtIdForSpecificUserAsync(string BuyerEmail, int OrderId);

    }
}