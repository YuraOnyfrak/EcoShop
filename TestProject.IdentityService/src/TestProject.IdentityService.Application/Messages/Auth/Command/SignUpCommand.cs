using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject.IdentityService.Application.Messages.Command
{
    public class SignUpCommand : IRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
