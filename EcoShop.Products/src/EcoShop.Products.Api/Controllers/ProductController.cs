using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcoShop.Products.Application.Dto;
using EcoShop.Products.Application.Messages.Commands;
using EcoShop.Products.Application.Messages.Queries;
using Microsoft.AspNetCore.Mvc;
using TestProject.Common.Api;

namespace EcoShop.Products.Api.Controllers
{
    public class ProductController : BaseApiController
    {
        public ProductController(global::MediatR.IMediator mediator) : base(mediator)
        {
        }
                

        // <summary>
        /// Create product
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("create")]
        public async Task<ActionResult> Create([FromBody]CreateProductCommand command)
        {
            await _mediator.Send(command, HttpContext.RequestAborted);
            return Ok();
        }

        [HttpGet("get-products")]
        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            return await _mediator.Send(new GetProductsQuery(), HttpContext.RequestAborted);
        }
    }
}