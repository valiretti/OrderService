using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService.Model
{
    public class UpdateExecutorModel : CreateExecutorModel
    {
        public int Id { get; set; }

        public string[] ExistingPhotos { get; set; }
    }
}
