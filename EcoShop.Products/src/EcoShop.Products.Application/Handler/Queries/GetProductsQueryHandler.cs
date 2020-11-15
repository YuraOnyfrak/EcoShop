using EcoShop.Products.Application.Common.Interfaces;
using EcoShop.Products.Application.Dto;
using EcoShop.Products.Application.Messages.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EcoShop.Products.Application.Handler.Queries
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<ProductDto>>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetProductsQueryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Products
                .Select(s => new ProductDto
                {
                    Id = s.Id,
                    Name = s.Name
                }).ToListAsync();
        }
    }
}
