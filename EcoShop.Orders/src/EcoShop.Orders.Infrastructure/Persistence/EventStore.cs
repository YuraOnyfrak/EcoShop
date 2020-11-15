﻿using EcoShop.Common.Domain;
using EcoShop.Common.RabbitMq;
using EcoShop.Orders.Domain.Repository;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoShop.Orders.Infrastructure.Persistence
{
    public class EventStore : IEventStore
    {
        private IDictionary<Guid, List<EventDescriptor>> _current = new ConcurrentDictionary<Guid, List<EventDescriptor>>();
        public IBusPublisher BusPublisher { get; set; }

        public EventStore()
        {
                
        }

        public IEnumerable<Event> GetEventByAggregate(Guid aggregateId)
        {
            if (!_current.TryGetValue(aggregateId, out List<EventDescriptor> eventDescriptor))
            {
                throw new Exception();//AggregateNotFoundException
            }

            return eventDescriptor.Select(s => s.EventData).ToList();
        }

        public async Task SaveEventsAsync(Guid aggregateId, IEnumerable<Event> events, int expectedVersion)
        {
            if(!_current.TryGetValue(aggregateId, out List<EventDescriptor> eventDescriptor))
            {
                eventDescriptor = new List<EventDescriptor>();
                _current.Add(aggregateId, eventDescriptor);
            }
            else if(eventDescriptor[eventDescriptor.Count -1].Version != expectedVersion && expectedVersion != -1)
            {
                throw new Exception();//ConcurrencyException
            }
            int i = expectedVersion;

            foreach (var @event in events)
            {
                i++;
                @event.Version = i;
                eventDescriptor.Add(new EventDescriptor(@event, aggregateId, i));
                await BusPublisher.PublishAsync(@event, null);
            }
        }
    }
}
