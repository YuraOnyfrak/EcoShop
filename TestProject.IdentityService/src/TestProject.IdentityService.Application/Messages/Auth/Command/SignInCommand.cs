using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject.IdentityService.Application.Messages.Command
{
    public class SignInCommand : IRequest<string>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}
