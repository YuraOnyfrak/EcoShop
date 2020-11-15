using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcoShop.ApiGateway.Models.Marketplace;
using EcoShop.ApiGateway.Services.Marketplace;
using Microsoft.AspNetCore.Mvc;

namespace EcoShop.ApiGateway.Controllers
{
    public class MarketplaceController : Controller
    {
        private readonly IMarketPlaceService _marketPlaceService;

        public MarketplaceController(IMarketPlaceService marketPlaceService)
        {
            _marketPlaceService = marketPlaceService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("get-products")]
        public async Task<IEnumerable<ProductsVM>> GetProducts()
        {
            return await _marketPlaceService.GetProductsAsync();
        }
    }
}