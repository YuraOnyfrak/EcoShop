using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestProject.Common.Api;
using TestProject.IdentityService.Application.Messages.Command;

namespace TestProject.IdentityService.Api.Controllers
{    
    [AllowAnonymous]    
    public class AuthController : BaseApiController
    {
        public AuthController(IMediator mediator) : base(mediator)
        {
        }         

        /// <summary>
        /// Register
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("signup")]
        public async Task<ActionResult> SignUp([FromBody] SignUpCommand command)
        {
            await _mediator.Send(command, HttpContext.RequestAborted);
            return Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("signin")]
        public async Task<ActionResult> SignIn([FromBody]SignInCommand command)
        {
            await _mediator.Send(command, HttpContext.RequestAborted);
            return Ok();
        }
    }
}