using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace OrderService.Model.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Executor Executor { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
