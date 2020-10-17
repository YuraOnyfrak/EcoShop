﻿using RestEase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestProject.Services
{
    public interface IFirstService
    {
        [AllowAnyStatusCode]
        [Get("test")]
        Task<object> TestInvoke();
    }
}
