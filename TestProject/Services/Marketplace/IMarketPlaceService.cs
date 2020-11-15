using EcoShop.ApiGateway.Models.Marketplace;
using RestEase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoShop.ApiGateway.Services.Marketplace
{
    public interface IMarketPlaceService
    {
        [AllowAnyStatusCode]
        [Get("/MarketPlace/get-products")]
        Task<IEnumerable<ProductsVM>> GetProductsAsync();
    }
}
