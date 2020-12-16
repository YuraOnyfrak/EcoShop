using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoShop.ApiGateway.Models.Marketplace
{
    public class ProductsVM
    {
        public Guid IdProduct { get; set; }
        public string ProductName { get; set; }
        public ManufacturerVM Manufacturer { get; set; }
    }
}
