using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Common.Messages
{
    public interface IRejectedEvent : IEvent
    {
        string Reason { get; }
        string Code { get; }
    }
}
