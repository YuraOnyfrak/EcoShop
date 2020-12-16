using EcoShop.Common.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EcoShop.Products.Domain.Repositorty
{    
    public interface IAggregateRootRepository<T> where T : AggregateRoot, new()
    {
        Task SaveAsync(AggregateRoot aggregate, int expectedVersion);
        T GetById(Guid id);
    }
}
