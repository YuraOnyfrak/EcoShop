using Project.Common.Common;
using Project.Common.Handlers;
using Project.Common.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EcoShop.Products.Application.Handler.Commands
{
    public class A: ICommand 
    {
    }

    public class TestHandler : ICommandHandler<A>
    {
        public Task HandleAsync(A command, ICorrelationContext context)
        {
            throw new NotImplementedException();
        }
    }
}
