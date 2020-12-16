using EcoShop.Common.Domain;
using EcoShop.Products.Domain.Repositorty;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EcoShop.Products.Infastructure.Repository
{
    public class AggregateRootRepository<T> : IAggregateRootRepository<T> where T : AggregateRoot, new() //shortcut you can do as you see fit with new()
    {
        private readonly IEventStore _storage;

        public AggregateRootRepository(IEventStore storage)
        {
            _storage = storage;
        }

        public async Task SaveAsync(AggregateRoot aggregate, int expectedVersion)
        {
           await _storage.SaveEventsAsync(aggregate.Id, aggregate.GetUncommittedChanges(), expectedVersion);
        }

        public T GetById(Guid id)
        {
            var obj = new T();//lots of ways to do this
            var e = _storage.GetEventByAggregate(id);
            obj.LoadsFromHistory(e);
            return obj;
        }
    }

}
