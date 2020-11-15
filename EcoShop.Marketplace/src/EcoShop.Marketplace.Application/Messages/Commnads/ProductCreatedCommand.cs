using EcoShop.Common.Common.Attributes;
using Project.Common.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcoShop.Marketplace.Application.Messages.Commnads
{
   
    public class ProductCreatedCommand : ICommand
    {
        public int Id { get; set; }
    }
}
