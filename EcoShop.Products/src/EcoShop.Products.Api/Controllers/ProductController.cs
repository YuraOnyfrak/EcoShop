using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcoShop.Common.Dispatchers;
using EcoShop.Products.Application.Dto;
using EcoShop.Products.Application.Messages.Commands;
using EcoShop.Products.Application.Messages.Queries;
using Microsoft.AspNetCore.Mvc;
using TestProject.Common.Api;

namespace EcoShop.Products.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;

        public ProductController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }
        //public ProductController(global::MediatR.IMediator mediator) : base(mediator)
        //{
        //}
                

        // <summary>
        /// Create product
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("create")]
        public async Task<ActionResult> Create([FromBody]CreateProductCommand command)
        {
            await _dispatcher.SendAsync(command);
            return Ok();
        }
    }
}