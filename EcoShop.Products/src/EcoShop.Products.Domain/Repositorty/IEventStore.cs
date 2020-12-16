using EcoShop.Common.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EcoShop.Products.Domain.Repositorty
{
    public interface IEventStore
    {
        Task SaveEventsAsync(Guid aggregateId, IEnumerable<Event> events, int expectedVersion);
        IEnumerable<Event> GetEventByAggregate(Guid aggregateId);
    }
}
