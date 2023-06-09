﻿using Capstone.DAO;

namespace Capstone.Models
{
    public class Item
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemType { get; set; }
        public decimal Price { get; set; }
        public bool Available { get; set; }
        public int EmployeeId { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public bool Availability { get; set; }
     //   public int ItemId { get; set; }


    }

    public class BeverageItem : Item { }
    public class SideItem : Item { }
}
