using EcoShop.ApiGateway.Models.Entrepreneur;
using EcoShop.ApiGateway.Services.Entrepreneur;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoShop.ApiGateway.Controllers
{
    public class EntrepreneurController : Controller
    {
        private readonly IEntrepreneurService _supplier;

        public EntrepreneurController(IEntrepreneurService supplier)
        {
            _supplier = supplier;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("create")]
        public async Task<ActionResult<object>> CreateAsync([FromBody] CreateSupplierModel model)
        {
            return await _supplier.CreateAsync(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("update")]
        public async Task<ActionResult<object>> UpdateAsync([FromBody] UpdateSupplierModel model)
        {
            return await _supplier.UpdateAsync(model);
        }
    }
}
