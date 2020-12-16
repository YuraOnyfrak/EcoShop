using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoShop.ApiGateway.Models.Order
{
    public class CreateOrderModel
    {
        public IEnumerable<OrderItem> OrderItems { get; set; }
    }

    public class OrderItem
    {
        public int Count { get; set; }
        public Guid ProductId { get; set; }
    }
}
