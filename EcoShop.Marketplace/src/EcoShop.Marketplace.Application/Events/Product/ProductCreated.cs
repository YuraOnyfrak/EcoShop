using EcoShop.Common.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcoShop.Marketplace.Application.Events.Product
{
    public class ProductCreated : IEvent
    {
        public readonly Guid Id;
        public readonly string Name;
        public readonly string Description;
        public readonly Guid CategoryId;
        public readonly int Count;
        public readonly decimal PricePerUnit;
        public readonly Guid SupplierId;
        public readonly DateTime CreatedDate;
        public readonly DateTime? ExpirationDate;

        public ProductCreated(Guid id, string name, Guid categoryId, int count, decimal pricePerUnit, 
            Guid supplierId, DateTime createdDate, DateTime? expirationDate, string description)
        {
            Id = id;
            Name = name;
            CategoryId = categoryId;
            Count = count;
            PricePerUnit = pricePerUnit;
            SupplierId = supplierId;
            CreatedDate = createdDate;
            ExpirationDate = expirationDate;
            Description = description;
        }
    }
}
