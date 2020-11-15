using EcoShop.Common.Common.Attributes;
using Project.Common.Messages;
using System;
using System.Collections.Generic;
using System.Text;


namespace EcoShop.Products.Application.Messages.Commands
{
    [MessageNamespace("marketplace")]
    public class ProductCreatedCommand : ICommand
    {
        public int Id { get; set; } 
    }
}
