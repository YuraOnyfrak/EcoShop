using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject.Models.IdentityService.Auth;
using TestProject.Services;

namespace TestProject.Controllers
{
    public class AuthController : Controller
    {
        private readonly IIdentityService _identityService;

        public AuthController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost("signup")]
        public async Task<ActionResult> SignUpAsync([FromBody] SignUpModel model)
        {
            var res =  await _identityService.SignUp(model);
            var c = HttpContext.User.Claims;
            return Ok();
        }

        [HttpPost("signin")]
        public async Task<ActionResult<string>> SignInAsync([FromBody] SignInModel model)
        {
            var result = await _identityService.SignIn(model);
            return result;
        }

        [HttpGet("token")]
        public async Task<ActionResult<object>> GenerateTokenAsync()
        {
            var c = HttpContext.User.Claims;
            var res =  await _identityService.GenerateToken();
            return res;
        }
    }
}
