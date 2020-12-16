using EcoShop.ApiGateway.Models.Order;
using RestEase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoShop.ApiGateway.Services.Order
{
    public interface IOrderService
    {
        [AllowAnyStatusCode]
        [Post("/Order/create")]
        Task<int> CreateAsync([Body] CreateOrderModel model);
    }
}
