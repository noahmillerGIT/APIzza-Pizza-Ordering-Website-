using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO
{
    public interface IToppingDAO
    {
        public List<Item> GetItems();
        public Item ToggleAvailability(Item item);

    }
}
