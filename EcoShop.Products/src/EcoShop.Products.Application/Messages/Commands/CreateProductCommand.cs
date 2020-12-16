using EcoShop.Common.Messages;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcoShop.Products.Application.Messages.Commands
{
    public class CreateProductCommand : ICommand
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
        public int Count { get; set; }
        public decimal PricePerUnit { get; set; }
        public Guid SupplierId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }
}
