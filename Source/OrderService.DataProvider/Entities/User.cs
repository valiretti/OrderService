using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace OrderService.DataProvider.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Executor Executor { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
