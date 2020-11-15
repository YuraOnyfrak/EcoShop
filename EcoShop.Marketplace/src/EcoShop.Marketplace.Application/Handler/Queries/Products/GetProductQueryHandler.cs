using EcoShop.Common.Cache;
using EcoShop.Marketplace.Application.DTO.Entrepreneur;
using EcoShop.Marketplace.Application.DTO.Product;
using EcoShop.Marketplace.Application.Messages.Queries;
using EcoShop.Marketplace.Application.Services.Entrepreneur;
using EcoShop.Marketplace.Application.Services.Product;
using EcoShop.Marketplace.Application.ViewModel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EcoShop.Marketplace.Application.Handler.Queries.Products
{
    public class GetProductQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<ProductsVM>>
    {
        private readonly IDistributedCacheWrapper _distributedCache;
        private readonly IProductService _productService;
        private readonly IEntrepreneurService _entrepreneurService;

        public GetProductQueryHandler
            (
            IDistributedCacheWrapper distributedCache, 
            IProductService productService,
            IEntrepreneurService entrepreneurService
            )
        {
            _distributedCache = distributedCache;
            _productService = productService;
            _entrepreneurService = entrepreneurService;
        }

        public async Task<IEnumerable<ProductsVM>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<ProductDto> productsDto = await _productService.GetProductsAsync();
            IEnumerable<EntrepreneurDto> suppliersDto = await _entrepreneurService.GetSuppliersAsync();

            IEnumerable<ProductsVM> products =
                (from p in productsDto
                 join s in suppliersDto on p.SupplierId equals s.Id
                 select new ProductsVM
                 {
                     IdProduct = p.Id,
                     IdSupplier = s.Id,
                     ProductName = p.Name,
                     SupplierName = s.Name
                 }).ToList();

            return products;
        }
    }
}
