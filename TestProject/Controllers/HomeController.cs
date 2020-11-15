using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project.Common.Messages;
using TestProject.Models;
using TestProject.Services;

namespace TestProject.Controllers
{
   
    //[Route("[controller]")]
    //[ApiController]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFirstService _firstService;

        public HomeController
            (
                ILogger<HomeController> logger,
                IFirstService firstService
            )
        {
            _logger = logger;
            _firstService = firstService;
        }
          
        [HttpGet]
        public IActionResult Index()
        {
            return Ok();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("test")]
        public async Task<ActionResult<object>> Test()
        {
           return await _firstService.TestInvoke();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
