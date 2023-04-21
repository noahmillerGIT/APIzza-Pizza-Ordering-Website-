using Capstone.Models;
using System.Collections.Generic;
using System.Linq;

namespace Capstone.DTO
{
    public class CustomOrderDto
    {
        public List<Pizza> Pizzas { get; set; } = new List<Pizza>();
        public Address Address { get; set; }
        public  Order CustomerOrder { get; set; }

        public decimal OrderCost { 
            get
            {
                return Pizzas.Sum(pizza => pizza.Price * pizza.PizzaQuantity);
            } 
        }
    }
}
