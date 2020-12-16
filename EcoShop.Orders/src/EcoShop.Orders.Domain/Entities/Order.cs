using EcoShop.Orders.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcoShop.Orders.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CustomerId { get; set; }
        public OrderStatus Status { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
