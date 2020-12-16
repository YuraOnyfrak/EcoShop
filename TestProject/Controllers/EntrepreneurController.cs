using EcoShop.ApiGateway.Models.Entrepreneur;
using EcoShop.ApiGateway.Services.Entrepreneur;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcoShop.ApiGateway.Controllers
{
    [ApiController]
    [Route("supplier")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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

        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-suppliers")]
        public async Task<IEnumerable<SupplierModel>> GetAsync()
        {
            return await _supplier.GetAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("get-supplier/{id}")]
        public async Task<SupplierModel> GetAsync(Guid id)
        {
            return await _supplier.GetAsync(id);
        }
    }
}
