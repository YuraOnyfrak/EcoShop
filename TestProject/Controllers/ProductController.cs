using EcoShop.ApiGateway.Models.Product;
using EcoShop.ApiGateway.Services.Product;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoShop.ApiGateway.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _product;

        public ProductController(IProductService product)
        {
            _product = product;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("insert")]
        public async Task<ActionResult<object>> CreateAsync([FromBody] CreateProductModel model)
        {
            return await _product.CreateAsync(model);
        }
    }
}
