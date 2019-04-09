using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService.Model.Entities
{
    public enum RequestStatus : byte
    {
        New = 0,
        Read = 1,
        Accepted = 2,
        Rejected = 3
    }
}
