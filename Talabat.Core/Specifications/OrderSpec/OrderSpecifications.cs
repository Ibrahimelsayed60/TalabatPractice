using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.OrderAggregate;

namespace Talabat.Core.Specifications.OrderSpec
{
    public class OrderSpecifications:BaseSpecifications<Order>
    {
        // Get Orders For user
        public OrderSpecifications(string email):base(O => O.BuyerEmail == email)
        {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.Items);
            AddOrderByDescending(O => O.OrderDate);
        }

        // Used to Get Order For User
        public OrderSpecifications(string email, int id):base(O => O.BuyerEmail == email && O.Id == id)
        {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.Items);
        }
    }
}
