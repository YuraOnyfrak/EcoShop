using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using EcoShop.ApiGateway.Models.CryptoWallet;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EcoShop.ApiGateway.Controllers
{
    public class CryptoWalletController : Controller
    {
        [HttpPost("create-transaction")]
        public async Task<ActionResult> CreateTransaction([FromBody]CreateTransactionModel model)
        {
            var json = JsonConvert.SerializeObject(model);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var url = "http://localhost:8000/transaction/add";

            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(url, data);

                string result = await response.Content.ReadAsStringAsync();
            }

            return Ok();            
        }

        [HttpGet("get-pool")]
        public async Task<ActionResult<dynamic>> Get()
        {
            using (var client = new HttpClient())
            {
                string url = "http://localhost:8000/transaction/pool";
                HttpResponseMessage response = await client.GetAsync(url);
                var resp = await response.Content.ReadAsStringAsync();

                return resp;
            }
        }
    }
}
