using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcoShop.ApiGateway.Models.Order;
using EcoShop.ApiGateway.Services.Order;
using Microsoft.AspNetCore.Mvc;

namespace EcoShop.ApiGateway.Controllers
{
    [ApiController]
    [Route("order")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("create")]
        public async Task<int> CreateAsync([FromBody] CreateOrderModel model)
        {
            return await _orderService.CreateAsync(model);
        }
    }
}
