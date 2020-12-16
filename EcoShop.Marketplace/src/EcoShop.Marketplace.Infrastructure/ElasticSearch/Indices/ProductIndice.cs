using EcoShop.Marketplace.Domain.Entity;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcoShop.Marketplace.Infrastructure.ElasticSearch.Indices
{
    public class ProductIndice
    {
        public void Init(IElasticClient client)
        {
            client.Indices.Create("product", c => c
            .Map<Product>(p => p
                .AutoMap()
                .Properties(props => props
                        .Keyword(t => t.Name("initials"))
                        .Object<Manufacturer>(t => t.Name(dv => dv.Manufacturer))
                    )
                .NumericDetection(true)
                )
            );

        }
    }
}
