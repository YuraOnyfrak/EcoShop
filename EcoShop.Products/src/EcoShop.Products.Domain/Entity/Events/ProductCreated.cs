using EcoShop.Common.Common.Attributes;
using EcoShop.Common.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcoShop.Products.Domain.Entity.Events
{
    [MessageNamespace("marketplace")]
    public class ProductCreated : Event
    {
        public readonly Guid Id;
        public readonly string Name;
        public readonly string Description;
        public readonly Guid CategoryId;
        public readonly int Count;
        public readonly decimal PricePerUnit;
        public readonly Guid SupplierId;
        public readonly DateTime CreatedDate;
        public readonly DateTime ExpirationDate;
        public ProductCreated(Guid id, string name, Guid categoryId, int count, decimal pricePerUnit, Guid supplierId,
            DateTime CreatedDate, DateTime? ExpirationDate, string description)
        {
            Id = id;
            Name = name;
            CategoryId = categoryId;
            Count = count;
            PricePerUnit = pricePerUnit;
            SupplierId = supplierId;
            Description = description;
        }
    }
}
