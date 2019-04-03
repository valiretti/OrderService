using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService.DataProvider.Entities
{
   public enum Status
    {
        Active = 0,
        Confirmed = 1,
        Completed = 2,
        Unfulfilled = 3
    }
}
