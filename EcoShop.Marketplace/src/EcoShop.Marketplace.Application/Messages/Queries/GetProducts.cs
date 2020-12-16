using EcoShop.Marketplace.Application.ViewModel;
using EcoShop.Marketplace.Domain.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcoShop.Marketplace.Application.Messages.Queries
{
    public class GetProductsQuery : IRequest<IEnumerable<Product>>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string SearchText { get; internal set; }
    }
}
