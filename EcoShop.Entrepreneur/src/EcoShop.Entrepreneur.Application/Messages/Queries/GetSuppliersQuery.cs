using EcoShop.Entrepreneur.Application.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcoShop.Entrepreneur.Application.Messages.Queries
{
    public class GetSuppliersQuery: IRequest<IEnumerable<SupplierDto>>
    {
    }
}
