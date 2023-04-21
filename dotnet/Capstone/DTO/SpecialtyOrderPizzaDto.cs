using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DTO
{
    public class SpecialtyOrderPizzaDto
    {
        public Address Address { get; set; }
        public Order CustomerOrder { get; set; }
        public List<Pizza> Pizzas { get; set; } = new List<Pizza>();
        public List<int> Id { get; set; }

    }
}
