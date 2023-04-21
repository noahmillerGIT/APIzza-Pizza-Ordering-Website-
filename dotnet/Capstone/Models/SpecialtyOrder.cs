using System.Collections.Generic;

namespace Capstone.Models
{
    public class SpecialtyOrder : Order
    {
        public SpecialtyPizza SpecialtyPizza { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Address Address { get; set; }
    }
}
