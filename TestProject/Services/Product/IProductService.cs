using EcoShop.ApiGateway.Models.Product;
using RestEase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoShop.ApiGateway.Services.Product
{
    public interface IProductService
    {
        [AllowAnyStatusCode]
        [Post("/Product/create")]
        Task<object> CreateAsync([Body] CreateProductModel model);
    }
}
