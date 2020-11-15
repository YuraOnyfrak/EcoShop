using EcoShop.Common.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcoShop.Common.Domain
{
    public class Event : IEvent
    {
        public Guid Id;
        public int Version;
        public Event()
        {
            Id = Guid.NewGuid();
        }
    }
}
