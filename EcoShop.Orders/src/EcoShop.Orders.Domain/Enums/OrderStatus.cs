using System;
using System.Collections.Generic;
using System.Text;

namespace EcoShop.Orders.Domain.Enums
{
    public enum OrderStatus
    {
        Created = 0,
        Approved = 1,
        Completed = 2,
        Canceled = 3,
        Revoked = 4
    }
}
