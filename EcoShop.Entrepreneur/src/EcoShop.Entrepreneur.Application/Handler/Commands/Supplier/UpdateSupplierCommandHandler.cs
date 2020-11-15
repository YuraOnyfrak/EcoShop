using EcoShop.Entrepreneur.Application.Common.Interfaces;
using EcoShop.Entrepreneur.Application.Messages.Commands.Supplier;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EcoShop.Entrepreneur.Application.Handler.Commands.Supplier
{
    public class UpdateSupplierCommandHandler : IRequestHandler<UpdateSupplierCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public UpdateSupplierCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
        {
            var supplier = await _context.Suppliers.FindAsync(new { Id = request.Id }, cancellationToken);

            if (supplier is null)
                throw new Exception();//TODO

            supplier.LegalAddress = request.LegalAddress;
            supplier.Name = request.Name;
            request.SuppliersTrademark = request.SuppliersTrademark;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
