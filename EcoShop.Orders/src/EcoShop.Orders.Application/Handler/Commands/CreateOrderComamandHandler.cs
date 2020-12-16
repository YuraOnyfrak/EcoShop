using EcoShop.Orders.Application.Common.Interfaces;
using EcoShop.Orders.Application.Messages.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EcoShop.Orders.Application.Handler.Commands
{
    public class CreateOrderComamandHandler : IRequestHandler<CreateOrderComamand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateOrderComamandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateOrderComamand request, CancellationToken cancellationToken)
        {
            var order = new Domain.Entities.Order
            {
                Status = Domain.Enums.OrderStatus.Created,
                CreatedDate = DateTime.UtcNow,
                CustomerId =1,
                OrderItems = request.OrderItems.Select(s => new Domain.Entities.OrderItem
                {
                   Count = s.Count,
                   PricePerUnit = 100,
                   ProductId = 1,//s.ProductId,
                   SellerId = 11
                }).ToList()
            };

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync(cancellationToken);

            return order.Id;
        }
    }
}
