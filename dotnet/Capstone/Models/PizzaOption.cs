using Capstone.DAO;

namespace Capstone.Models
{
    public class PizzaOption 
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal AdditionalCharge { get; set; }
        public bool IsAvailable { get; set; }
    }
}
