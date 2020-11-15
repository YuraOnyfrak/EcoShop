using System;
using System.Collections.Generic;
using System.Text;

namespace EcoShop.Marketplace.Application.ViewModel
{
    public class ProductsVM
    {
        public int IdProduct { get; set; }
        public int IdSupplier { get; set; }
        public string SupplierName { get; set; }
        public string ProductName { get; set; }
    }
}
