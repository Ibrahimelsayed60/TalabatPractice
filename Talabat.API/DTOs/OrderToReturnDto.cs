﻿using Talabat.Core.Entities.OrderAggregate;

namespace Talabat.API.DTOs
{
    public class OrderToReturnDto
    {
        public int Id { get; set; }
        public string BuyerEmail { get; set; }

        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;

        public string Status { get; set; }

        public Address ShippingAddress { get; set; }

        //public int DeliveryMethodId { get; set; } // FK
        public string DeliveryMethod { get; set; }
        public decimal DeliveryMethodCost { get; set; }

        public ICollection<OrderItemDto> Items { get; set; } = new HashSet<OrderItemDto>();

        public decimal SubTotal { get; set; }

        //[NotMapped]
        public decimal Total { get; set; }


        public string PaymentIntentId { get; set; } = string.Empty;

    }
}
