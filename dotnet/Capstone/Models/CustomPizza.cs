using Microsoft.AspNetCore.Builder;
using System.Collections.Generic;

namespace Capstone.Models
{
    public class CustomPizza : Pizza
    {
        public Address Address { get; set; }
        public CustomOrder CustomOrder { get; set; }
    }  
}
