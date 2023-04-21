using System;

namespace Capstone.Models
{
    public class PendingOrders
    {
        public int OrderId { get; set; }
        public string OrderStatus { get; set; }
        public string OrderType { get; set; } 
        public decimal OrderCost { get; set; }
        public DateTime OrderTime { get; set; }
        public string Toppings { get; set; }
        public string Size { get; set; }
        public string CrustName { get; set; }
        public string SauceName { get; set; }
    }
}
