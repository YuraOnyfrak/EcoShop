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
        public async Task<ActionResult<object>> SignUp([FromBody] SignUpModel model)
        {
            return await _identityService.SignUp(model);
        }

        [HttpPost("signin")]
        public async Task<ActionResult<object>> SignIn([FromBody] SignInModel model)
        {
            return await _identityService.SignIn(model);
        }

        [HttpGet("token")]
        public async Task<ActionResult<object>> GenerateToken()
        {
            return await _identityService.GenerateToken();
        }
    }
}
