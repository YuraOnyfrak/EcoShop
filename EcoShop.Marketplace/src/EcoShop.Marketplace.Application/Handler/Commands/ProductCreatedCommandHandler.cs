using EcoShop.Common.Common;
using EcoShop.Marketplace.Application.Messages.Commnads;
using EcoShop.Marketplace.Application.ViewModel;
using EcoShop.Marketplace.Domain.Entity;
using Microsoft.Extensions.Logging;
using Nest;
using Project.Common.Handlers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EcoShop.Marketplace.Application.Handler.Commands
{
    public class ProductCreatedCommandHandler : ICommandHandler<ProductCreatedCommand>
    {
        private readonly IElasticClient _elasticClient;
        private readonly ILogger<ProductCreatedCommandHandler> _logger;

        public ProductCreatedCommandHandler
            (
            IElasticClient elasticClient,
            ILogger<ProductCreatedCommandHandler> logger
            )
        {
            _elasticClient = elasticClient;
            _logger = logger;
        }

        public async Task HandleAsync(ProductCreatedCommand command, ICorrelationContext context)
        {
            var indexResponse = await _elasticClient.IndexAsync<Product>(
                new Product
                {
                   // IdProduct = command.Id,
                    //IdSupplier = command.Id
                }, p => p.Index("people").Pipeline("person-pipeline"));
            
            if(!indexResponse.IsValid)
            {
                _logger.LogError(@$"{indexResponse.ServerError.Status} 
                                    {indexResponse.ServerError.Error.Reason} 
                                    {indexResponse.ServerError.Error.Index}");
            }
        }
    }
}
