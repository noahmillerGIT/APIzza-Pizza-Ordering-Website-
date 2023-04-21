using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO
{
    public interface ISideDAO
    {
        public List<Item> GetItems();
        public Item ToggleAvailability(Item item);

    }
}
