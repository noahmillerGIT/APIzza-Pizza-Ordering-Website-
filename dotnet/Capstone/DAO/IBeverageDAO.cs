using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO
{
    public interface IBeverageDAO
    {
        public List<Item> GetItems();
        public Item ToggleAvailability(Item item);

    }
}

