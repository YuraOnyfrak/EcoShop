using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoShop.Entrepreneur.Api.Controllers
{
    [ApiController]
    public class HomeController : Controller
    {
        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok();
        }
    }
}
