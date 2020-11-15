using EcoShop.Common.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcoShop.Orders.Domain.Repository
{
    public interface IEventStore
    {
        SaveEventsAsync(Guid aggregateId, IEnumerable<Event> events, int expectedVersion);
        IEnumerable<Event> GetEventByAggregate(Guid aggregateId);
    }
}
