using EcoShop.Marketplace.Domain.Entity;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcoShop.Marketplace.Infrastructure.ElasticSearch.Ingest
{
    public class ProductIngest
    {
        public void Init(IElasticClient client)
        {
            client.Ingest.PutPipeline("product-pipeline", p => p
                .Processors(ps => ps
                    .Uppercase<Product>(s => s
                        .Field(t => t.ProductName)
                    )
                    //.Script(s => s
                    //    .Lang("painless")
                    //    .Source("ctx.initials = ctx.firstName.substring(0,1) + ctx.lastName.substring(0,1)")
                    //)
                    //.GeoIp<Person>(s => s
                    //    .Field(i => i.IpAddress)
                    //    .TargetField(i => i.GeoIp)
                    //)
                )
            );
        }
    }
}
