using EcoShop.Marketplace.Application.Messages.Commnads;
using Project.Common.Common;
using Project.Common.Handlers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EcoShop.Marketplace.Application.Handler.Commands
{
    public class ProductCreatedCommandHandler : ICommandHandler<ProductCreatedCommand>
    {
        public Task HandleAsync(ProductCreatedCommand command, ICorrelationContext context)
        {
            int a = 2;
            return Task.CompletedTask;
        }
    }
}
