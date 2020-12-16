using EcoShop.Common.Cache;
using EcoShop.Marketplace.Application.Messages.Queries;
using EcoShop.Marketplace.Domain.Entity;
using MediatR;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EcoShop.Marketplace.Application.Handler.Queries.Products
{
    public class GetProductQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<Product>>
    {
        private readonly IDistributedCacheWrapper _distributedCache;
        private readonly IElasticClient _elasticClient;

        public GetProductQueryHandler
            (
            IDistributedCacheWrapper distributedCache,
            IElasticClient elasticClient
            )
        {
            _distributedCache = distributedCache;
            _elasticClient = elasticClient;
        }

        public async Task<IEnumerable<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {           
            int numberOfSlices = Environment.ProcessorCount;
            List<Product> results = new List<Product>();

            var searchResponse = await _elasticClient.SearchAsync<Product>(s =>
                           s.Source(sf => sf
                                .Includes(i => i
                                    //.Fields(f => f.ProductName,
                                    //       f => f.Manufacturer.Name
                                    //)
                                )
                           )
                           .Query(q => q
                           //.Match(m => m
                           //      .Field(s => s.ProductName)
                           //      .Query(request.SearchText)
                           //) || q
                           //.Match(m => m
                           //      .Field(s => s.Description)
                           //      .Query(request.SearchText)
                           // ) && +q //A query can be transformed into a bool query with a filter clause using the unary + operator
                           //          //This runs the query in a filter context, which can be useful in improving performance
                           //          //where the relevancy score for the query is not required to affect the order of results.
                           // .DateRange(r =>
                           //      r.Field(s => s.StartDate)
                           //      .GreaterThanOrEquals(request.StartDate)
                           //      .LessThanOrEquals(request.StartDate)
                           // )
                           )
                           //.Scroll("10s")
                           //.From(0)
                           //.Take(15)
                       ); 


            //if (!searchResponse.IsValid)
            //{
            //    //TODO: throw exception
            //}

            if(searchResponse.Documents.Any())
                results.AddRange(searchResponse.Documents);

            //string scrollId = searchResponse.ScrollId;

            //ISearchResponse<Product> loopingResponse;

            //while (searchResponse.Documents.Any())
            //{
            //    loopingResponse = _elasticClient.Scroll<Product>("2s", scrollId);

            //    if(loopingResponse.IsValid)
            //    {
            //        results.AddRange(loopingResponse.Documents);
            //        scrollId = loopingResponse.ScrollId;
            //    }
            //}

            //_elasticClient.ClearScroll(new ClearScrollRequest(scrollId));

            return results.Skip(2);
        }
    }
}
