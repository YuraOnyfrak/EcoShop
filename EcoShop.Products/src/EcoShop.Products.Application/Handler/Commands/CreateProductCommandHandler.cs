using EcoShop.Common.RabbitMq;
using EcoShop.Products.Application.Common.Interfaces;
using EcoShop.Products.Application.Messages.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Project.Common.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EcoShop.Products.Application.Handler.Commands
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Unit>
    {

        private readonly IApplicationDbContext _context;
        private readonly IBusPublisher _busPublisher;

        public CreateProductCommandHandler(IApplicationDbContext context, IBusPublisher busPublisher)
        {
            _context = context;
            _busPublisher = busPublisher;
        }

        public async Task<Unit> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            //var supplierExistWithTheSameName =
            //    await _context.Products.AnyAsync(s => s.Name == request.Name, cancellationToken);

            //if (supplierExistWithTheSameName)
            //    throw new Exception();//TODO

            //_context.Products.Add(new Domain.Entity.Product
            //{
            //    Name = request.Name
            //});
            //await _context.SaveChangesAsync(cancellationToken);

            await _busPublisher.SendAsync(new ProductCreatedCommand() { Id = 99 }, CorrelationContext.Empty);

            return Unit.Value;
        }
    }
}
