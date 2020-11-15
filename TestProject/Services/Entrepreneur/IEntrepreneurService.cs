using EcoShop.ApiGateway.Models.Entrepreneur;
using RestEase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoShop.ApiGateway.Services.Entrepreneur
{
    public interface IEntrepreneurService
    {
        [AllowAnyStatusCode]
        [Post("/Supplier/create")]
        Task<object> CreateAsync([Body] CreateSupplierModel model);

        [AllowAnyStatusCode]
        [Put("/Supplier/update")]
        Task<object> UpdateAsync([Body] UpdateSupplierModel model);
    }
}
