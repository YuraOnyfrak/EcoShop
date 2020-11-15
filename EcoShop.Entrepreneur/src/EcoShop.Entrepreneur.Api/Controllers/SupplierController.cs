using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcoShop.Entrepreneur.Application.Dto;
using EcoShop.Entrepreneur.Application.Messages.Commands;
using EcoShop.Entrepreneur.Application.Messages.Commands.Supplier;
using EcoShop.Entrepreneur.Application.Messages.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TestProject.Common.Api;

namespace EcoShop.Entrepreneur.Api.Controllers
{
    public class SupplierController : BaseApiController
    {
        public SupplierController(IMediator mediator) : base(mediator)
        {
        }

        public IActionResult Index()
        {
            return Ok();
        }


        // <summary>
        /// Create supplier
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("create")]
        public async Task<ActionResult> CreateAsync([FromBody]CreateSupplierCommand command)
        {
            await _mediator.Send(command, HttpContext.RequestAborted);
            return Ok();
        }

        /// <summary>
        /// Update supplier
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("update")]
        public async Task<ActionResult> UpdateAsync([FromBody]UpdateSupplierCommand command)
        {
            await _mediator.Send(command, HttpContext.RequestAborted);
            return NoContent();
        }

        [HttpGet("get-suppliers")]
        public async Task<IEnumerable<SupplierDto>> GetProducts()
        {
            return await _mediator.Send(new GetSuppliersQuery(), HttpContext.RequestAborted);
        }

    }
}