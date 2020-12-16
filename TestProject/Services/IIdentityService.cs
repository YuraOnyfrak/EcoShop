using Microsoft.AspNetCore.Mvc;
using RestEase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject.Models.IdentityService.Auth;

namespace TestProject.Services
{
    public interface IIdentityService
    {
        [AllowAnyStatusCode]
        [Post("/Auth/signin")]
        Task<string> SignIn([Body] SignInModel model);

        [AllowAnyStatusCode]
        [Post("/Auth/signup")]
        Task<ActionResult> SignUp([Body] SignUpModel model);

        [AllowAnyStatusCode]
        [Get("/Token/get-token")]
        Task<object> GenerateToken();
    }
}
