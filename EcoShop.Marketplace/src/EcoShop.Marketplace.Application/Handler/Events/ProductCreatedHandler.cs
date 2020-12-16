using EcoShop.Common.Common;
using EcoShop.Marketplace.Application.Events.Product;
using EcoShop.Marketplace.Application.Services.Entrepreneur;
using EcoShop.Marketplace.Domain.Entity;
using Microsoft.Extensions.Logging;
using Nest;
using Project.Common.Handlers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EcoShop.Marketplace.Application.Handler.Events
{
    public class ProductCreatedHandler : IEventHandler<ProductCreated>
    {
        private readonly IElasticClient _elasticClient;
        private readonly ILogger<ProductCreatedHandler> _logger;
        private readonly IEntrepreneurService _entrepreneurService;

        public ProductCreatedHandler
            (
            IElasticClient elasticClient,
            ILogger<ProductCreatedHandler> logger,
            IEntrepreneurService entrepreneurService
            )
        {
            _elasticClient = elasticClient;
            _logger = logger;
            _entrepreneurService = entrepreneurService;
        }

        public async Task HandleAsync(ProductCreated @event, ICorrelationContext context)
        {
            var supplier = await _entrepreneurService.GetSupplierByIdAsync(@event.SupplierId);

            var indexResponse = await _elasticClient.IndexAsync<Product>(
               new Product
               {
                   IdProduct = @event.Id,
                   ExpirationDate = @event.ExpirationDate,
                   CreatedDate = @event.CreatedDate,
                   Description = @event.Description,
                   ProductName = @event.Name,
                   Count = @event.Count,
                   PricePerUnit = @event.PricePerUnit,
                   Manufacturer = new Manufacturer
                   {
                       Id = @event.SupplierId,
                       Name = supplier.Name,
                       ActualAddress = supplier.ActualAddress,
                       Code = supplier.Code,
                       Description = supplier.Description,
                       Email = supplier.Email,
                       LegalAddress = supplier.LegalAddress,
                       SuppliersTrademark = supplier.SuppliersTrademark,
                       WebsiteUrl = supplier.WebsiteUrl
                   }
               }, p => p.Index("product").Pipeline("product-pipeline"));

            if (!indexResponse.IsValid)
            {
                _logger.LogError(@$"{indexResponse.OriginalException?.Message}");
            }
        }
    }
}
