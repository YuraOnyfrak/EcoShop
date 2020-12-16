using EcoShop.Common.Types;
using EcoShop.Products.Application.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcoShop.Products.Application.Messages.Queries
{
    public class GetProductsQuery : IQuery<IEnumerable<ProductDto>>
    {
    }
}
