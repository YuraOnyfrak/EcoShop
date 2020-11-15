using EcoShop.Entrepreneur.Application.Common.Interfaces;
using EcoShop.Entrepreneur.Application.Messages.Commands.Supplier;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EcoShop.Entrepreneur.Application.Handler.Commands.Supplier
{
    public class CreateSupplierCommandHandler : IRequestHandler<CreateSupplierCommand, Unit>
    {

        private readonly IApplicationDbContext _context;

        public CreateSupplierCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
        {
            var supplierExistWithTheSameName = 
                await _context.Suppliers.AnyAsync(s=>s.Name == request.Name, cancellationToken);

            if(supplierExistWithTheSameName)
                throw new Exception();//TODO

            _context.Suppliers.Add(new Domain.Entity.Supplier
            {
                Name = request.Name
            });
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
