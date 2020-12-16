using System;
using System.Collections.Generic;
using System.Text;

namespace EcoShop.Orders.Domain.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public decimal PricePerUnit { get; set; }
        public int Count { get; set; }
        public int ProductId { get; set; }
       // public string Name { get; set; } //TODO: remove
        public int SellerId { get; set; }

        public Order Order { get; set; }
    }
}
