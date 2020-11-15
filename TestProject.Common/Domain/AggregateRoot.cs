using System;
using System.Collections.Generic;
using System.Text;

namespace EcoShop.Common.Domain
{
    public class AggregateRoot
    {
        private readonly List<Event> _changes = new List<Event>();

        public Guid Id { get; protected set; }
        public int Version { get; internal set; } = -1;

        public List<Event> GetUncommittedChanges()
            => _changes;

        public void MarkChangesAsCommitted()
            => _changes.Clear();

        public void LoadFromHistory(IEnumerable<Event> history)
        {
            foreach (var @event in history)
            {

            }
        }

        protected void ApplyChange(Event @event)
        {
            ApplyChange(@event, true);
        }

        private void ApplyChange(Event @event, bool isNew)
        {
            this.AsDynamic().Apply(@event);

            if (isNew)
                _changes.Add(@event);
        }
    }
}
