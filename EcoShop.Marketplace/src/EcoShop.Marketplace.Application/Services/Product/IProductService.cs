using EcoShop.Marketplace.Application.DTO.Product;
using RestEase;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EcoShop.Marketplace.Application.Services.Product
{
    public interface IProductService
    {
        [AllowAnyStatusCode]
        [Get("/Product/get-products")]
        Task<IEnumerable<ProductDto>> GetProductsAsync();
    }
}
