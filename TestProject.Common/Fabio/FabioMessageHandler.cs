using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Polly;

namespace TestProject.Common.Fabio
{
    public class FabioMessageHandler : DelegatingHandler
    {
        private readonly FabioOptions _options;
        private readonly string _servicePath;

        public FabioMessageHandler(IConfiguration configuration, string serviceName = null)
        {            
            var options = configuration.GetSection("fabio").Get<FabioOptions>();           

            if (string.IsNullOrWhiteSpace(options.Url))
            {
                throw new InvalidOperationException("Fabio URL was not provided.");
            }

            _options = options;
            _servicePath = string.IsNullOrWhiteSpace(serviceName) ? string.Empty : $"{serviceName}/";
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            request.RequestUri = GetRequestUri(request);

            return await Policy.Handle<Exception>()
                .WaitAndRetryAsync(RequestRetries, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
                .ExecuteAsync(async () => await base.SendAsync(request, cancellationToken));
        }


       //HttpClient client = new HttpClient("localhost:1234");
        private Uri GetRequestUri(HttpRequestMessage request)
            =>  new Uri($"{_options.Url}/{_servicePath}{request.RequestUri.Host}{request.RequestUri.PathAndQuery}");
        
        private int RequestRetries => _options.RequestRetries <= 0 ? 3 : _options.RequestRetries;
    }
}