using EcoShop.Common.Common.Extensions;
using EcoShop.Marketplace.Application.Options;
using EcoShop.Marketplace.Infrastructure.ElasticSearch.Indices;
using EcoShop.Marketplace.Infrastructure.ElasticSearch.Ingest;
using Elasticsearch.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EcoShop.Marketplace.Infrastructure.Extensions
{
    public static class ElasticSearch
    {
        public static void AddElasticSearch(this IServiceCollection services, IConfiguration configuration)
        {
            var sectionName = "elasticSearch";//nameof(ElasticSearchOptions).GetNameOptions()
            var options = configuration.GetSection(sectionName).Get<ElasticSearchOptions>();

            var uris = Enumerable.Range(options.Port, options.CountNode)
                .Select(port => new Uri($"{options.Address}:{port}"));

            var nodes = uris.Select(u => new Node(u));
            var pool = new StickyConnectionPool(nodes);

            //var pool = new StickySniffingConnectionPool(uris, node =>
            //{
            //    float weight = 0f;

            //    if (node.ClientNode)
            //        weight += 10f;

            //    if (node.Settings.TryGetValue("node.attr.rack_id", out var rackId) && rackId.ToString() == options.MainNodeName)
            //        weight += 10f;

            //    return weight;
            //});

            var settings = new ConnectionSettings(pool)
                //.NodePredicate(n => n.IngestEnabled)
                //This allows you to customise the cluster and not have to reconfigure the client.
                .DefaultIndex(options.DefaultIndex);
                //.DefaultMappingFor<Post>(m => m
                //    .Ignore(p => p.IsPublished)
                //    .PropertyName(p => p.ID, "id")
                //)
                //.DefaultMappingFor<Comment>(m => m
                //    .Ignore(c => c.Email)
                //    .Ignore(c => c.IsAdmin)
                //    .PropertyName(c => c.ID, "id")
                //);

            var client = new ElasticClient(settings);

            new ProductIngest().Init(client);
            new ProductIndice().Init(client);

            services.AddSingleton<IElasticClient>(client);

        }
    }
}
