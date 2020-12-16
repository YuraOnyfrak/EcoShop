using EcoShop.Common.Common;
using EcoShop.Common.RabbitMq;
using EcoShop.Products.Application.Common.Interfaces;
using EcoShop.Products.Application.Messages.Commands;
using EcoShop.Products.Domain.Entity;
using EcoShop.Products.Domain.Repositorty;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Project.Common.Handlers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EcoShop.Products.Application.Handler.Commands
{
    public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand>
    {
        private readonly IAggregateRootRepository<Product> _productRepository; 

        public CreateProductCommandHandler(IAggregateRootRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task HandleAsync(CreateProductCommand command, ICorrelationContext context)
        {
            var product = new Product
                (
                    Guid.NewGuid(),
                    command.Name,
                    command.SupplierId,
                    command.Count,
                    command.PricePerUnit,
                    command.SupplierId,
                    command.CreatedDate,
                    command.ExpirationDate,
                    command.Description
                );
            await _productRepository.SaveAsync(product, -1);
        }
    }
}
