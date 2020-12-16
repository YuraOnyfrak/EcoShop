using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestProject.Common.Api;
using TestProject.IdentityService.Application.Messages.Token;

namespace TestProject.IdentityService.Api.Controllers
{
    [ApiController]
    public class TokenController : BaseApiController
    {
        public TokenController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-token")]
        public async Task<ActionResult> GenereteToken()
        {
            var c = HttpContext.User.Claims;
            string token = await _mediator.Send(new TokenGenerator(), HttpContext.RequestAborted);
            return new JsonResult(token) { };// Ok(token);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("refresh-token")]
        public async Task<ActionResult> GenereterefreshToken()
        {
            await _mediator.Send(new RefreshTokenGenerator(), HttpContext.RequestAborted);
            return Ok();
        }
    }
}