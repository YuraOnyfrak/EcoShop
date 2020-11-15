using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcoShop.Products.Application.Messages.Commands
{
    public class CreateProductCommand : IRequest
    {
        public string Name { get; set; }
    }
}
