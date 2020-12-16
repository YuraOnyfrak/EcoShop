using System;
using System.Collections.Generic;
using System.Text;

namespace EcoShop.Marketplace.Domain.Entity
{
    public class Product
    {
        public Guid IdProduct { get; set; }
        public string ProductName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }
        public decimal PricePerUnit { get; set; }
    }
}
