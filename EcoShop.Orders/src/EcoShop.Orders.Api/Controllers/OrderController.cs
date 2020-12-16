using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcoShop.Orders.Application.Messages.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TestProject.Common.Api;

namespace EcoShop.Orders.Api.Controllers
{
    public class OrderController : BaseApiController
    {
        public OrderController(IMediator mediator) : base(mediator)
        {
        }

        // <summary>
        /// Create supplier
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("create")]
        public async Task<int> CreateAsync([FromBody] CreateOrderComamand command)
        {
            return await _mediator.Send(command, HttpContext.RequestAborted);
           
        }
    }
}
