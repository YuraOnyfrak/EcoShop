using EcoShop.Entrepreneur.Application.Common.Interfaces;
using EcoShop.Entrepreneur.Application.Dto;
using EcoShop.Entrepreneur.Application.Messages.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EcoShop.Entrepreneur.Application.Handler.Queries
{
    public class GetSuppliersQueryHandler : IRequestHandler<GetSuppliersQuery, IEnumerable<SupplierDto>>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetSuppliersQueryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public  async Task<IEnumerable<SupplierDto>> Handle(GetSuppliersQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Suppliers
               .Select(s => new SupplierDto
               {
                   Id = s.Id,
                   Name = s.Name,
                   Description = s.Description,
                   ActualAddress = s.ActualAddress,
                   Code = s.Code,
                   Email = s.Email,
                   LegalAddress = s.LegalAddress,
                   SuppliersTrademark = s.SuppliersTrademark,
                   WebsiteUrl = s.WebsiteUrl                   
               }).ToListAsync();
        }
    }
}
