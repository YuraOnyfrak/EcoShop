using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcoShop.Orders.Application.Messages.Commands
{
    public class CreateOrderComamand : IRequest<int>
    {
        public IEnumerable<OrderItem> OrderItems { get; set; }
    }

    public class OrderItem
    {
        public int Count { get; set; }
        public Guid ProductId { get; set; }
    }
}
