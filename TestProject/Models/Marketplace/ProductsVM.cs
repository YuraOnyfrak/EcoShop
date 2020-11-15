using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoShop.ApiGateway.Models.Marketplace
{
    public class ProductsVM
    {
        public int IdProduct { get; set; }
        public int IdSupplier { get; set; }
        public int SupplierName { get; set; }
        public int ProductName { get; set; }
    }
}
