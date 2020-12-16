using EcoShop.Common.Domain;
using EcoShop.Products.Domain.Entity.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcoShop.Products.Domain.Entity
{
    public class Product : AggregateRoot
    {
        private bool _activated;
        private Guid _id;

        public override Guid Id
        {
            get { return _id; }
        }

        private void Apply(ProductCreated product)
        {
            _id = product.Id;
            _activated = true;
        }

        public Product()
        {
            // used to create in repository ... many ways to avoid this, eg making private constructor
        }

        public Product(Guid id, string name, Guid categoryId, int count, decimal pricePerUnit,  Guid supplierId, 
            DateTime createdDate, DateTime? expirationDate, string description)
        {
            ApplyChange(new ProductCreated(id, name, categoryId, count, pricePerUnit, supplierId, createdDate, expirationDate, description));
        }
    }
}
