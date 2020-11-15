using System;
using System.Collections.Generic;
using System.Text;

namespace EcoShop.Common.Domain
{
    public struct EventDescriptor
    {
        public readonly Event EventData;
        public readonly Guid Id;
        public readonly int Version;

        public EventDescriptor(Event eventData, Guid id, int version)
        {
            this.EventData = eventData;
            this.Id = id;
            this.Version = version;
        }
    }
}
