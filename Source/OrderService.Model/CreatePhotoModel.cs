using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace OrderService.Model
{
    public class CreatePhotoModel
    {
        public IFormFileCollection Files { get; set; }
    }
}
