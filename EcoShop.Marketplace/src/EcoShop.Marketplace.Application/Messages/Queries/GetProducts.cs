using EcoShop.Marketplace.Application.ViewModel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcoShop.Marketplace.Application.Messages.Queries
{
    public class GetProductsQuery : IRequest<IEnumerable<ProductsVM>>
    {

    }
}
