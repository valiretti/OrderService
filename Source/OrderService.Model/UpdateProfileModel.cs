using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService.Model
{
    public class UpdateProfileModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string UserId { get; set; }
    }
}
