using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcoShop.Marketplace.Application.Messages.Queries;
using EcoShop.Marketplace.Application.ViewModel;
using EcoShop.Marketplace.Domain.Entity;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TestProject.Common.Api;

namespace EcoShop.Marketplace.Api.Controllers
{
    public class MarketPlaceController : BaseApiController
    {
        public MarketPlaceController(IMediator mediator) : base(mediator)
        {
        }

        public IActionResult Index()
        {
            return Ok();
        }

        [HttpGet("get-products")]
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return  await _mediator.Send(new GetProductsQuery(), HttpContext.RequestAborted);
        }
    }
}